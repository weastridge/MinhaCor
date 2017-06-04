using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Wve.WveHL7
{
    /// <summary>
    /// args for the HL7Reader HeaderSegmentRead event
    /// </summary>
    public class HL7HeaderSegmentReadEventArgs : EventArgs
    {
        private Segment _seg = null;
        /// <summary>
        /// the segment that was read
        /// </summary>
        public Segment seg
        {
            get { return _seg; }
        }

        /// <summary>
        /// set this to true to cancel further reading
        /// </summary>
        public bool Cancel = false;

        /// <summary>
        /// handler should sset to true if succeeded without 
        /// serious error
        /// </summary>
        public bool Success = false;

        /// <summary>
        /// args for the HL7Reader HeaderSegmentRead event
        /// </summary>
        /// <param name="seg"></param>
        public HL7HeaderSegmentReadEventArgs(Segment seg)
        {
            _seg = seg;
        }
    }


    /// <summary>
    /// args for HL7Reader MessageRead event
    /// </summary>
    public class HL7MessageReadEventArgs: EventArgs
    {
        private Message _msg = null;
        /// <summary>
        /// The HL7 Message involved
        /// </summary>
        public Message Msg { get { return _msg; } }

        /// <summary>
        /// set to true to cancel further reading 
        /// </summary>
        public bool Cancel = false;

        /// <summary>
        /// handler should sset to true if succeeded without 
        /// serious error
        /// </summary>
        public bool Success = false;

        /// <summary>
        /// comments may be sent from sender or receiver back to sender
        /// </summary>
        public string Comments = string.Empty;

        /// <summary>
        /// args for HL7Reader MessgeRead event
        /// </summary>
        /// <param name="msg"></param>
        public HL7MessageReadEventArgs(Message msg)
        {
            _msg = msg;
        }
    }

    /// <summary>
    /// characters used to separate parts of an HL7 Message
    /// </summary>
    public class HL7SeparatorChars
    {
        /// <summary>
        /// character to separate fields within a segment
        /// </summary>
        public char FieldSeparator = '|';
        /// <summary>
        /// character to separate components within a field
        /// </summary>
        public char ComponentSeparator = '^';
        /// <summary>
        /// character to separate repetition in fields
        /// </summary>
        public char RepetitionCharacter = '~';
        /// <summary>
        /// preceeds special characters to make them interpreted literally
        /// </summary>
        public char EscapeCharacter = '\\';
        /// <summary>
        /// character to separate sub-components
        /// </summary>
        public char SubComponentSeparator = '&';
    }

    /// <summary>
    /// the basic transmission unit in HL7, made up of Segments
    /// </summary>
    public class Message
    {
        /// <summary>
        /// the whole message string, or empty if not set
        /// </summary>
        public string Value
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (Segment s in Segments)
                {
                    sb.Append(s.Value(false));
                    sb.Append(Environment.NewLine);
                }
                return sb.ToString();
            }
        }
        
        /// <summary>
        /// the segments or lines of text in a message
        /// </summary>
        public List<Segment> Segments = new List<Segment>();

        /// <summary>
        /// strip unpaired escape characters from string
        /// </summary>
        /// <param name="rawValue"></param>
        /// <param name="escapeCharacter"></param>
        /// <returns></returns>
        public static string StripEscapes(string rawValue,
            char escapeCharacter)
        {
            bool isEscaped = false;
            StringBuilder sb = new StringBuilder();
            using (StringReader sr = new StringReader(rawValue))
            {
                //while not end of stream...
                while (sr.Peek() != -1)
                {
                    int i = sr.Read();
                    //skip escape chars unless they are the second consecutive one
                    if((Convert.ToChar(i) == escapeCharacter) && (!isEscaped))
                    {
                        isEscaped = true;
                    }
                    else
                    {
                        sb.Append(Convert.ToChar(i));
                        isEscaped = false;//reset
                    }
                }
            }
            return sb.ToString();
        }
    }

    /// <summary>
    /// the HL7 Segement is a line of text in a message
    /// </summary>
    public class Segment
    {
        /// <summary>
        /// 3 letter type of segment or null if not set
        /// (essentially always the first 3 chars of the line of text)
        /// </summary>
        public string SegmentType
        {
            get
            {
                if (_value.Length > 2)
                {
                    return _value.Substring(0, 3);
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// chars that separate parts of hl7 data
        /// </summary>
        public HL7SeparatorChars SepChars;

        private string _value = string.Empty;
        /// <summary>
        /// the whole text  which may  include escape characters
        /// unless stripped
        /// </summary>
        /// <param name="stripEscChars">normally set to true unless you
        /// plan to manually subdivide by special characters</param>
        public string Value(bool stripEscChars)
        {
            if (stripEscChars)
            {
                return Message.StripEscapes(_value, SepChars.EscapeCharacter);
            }
            else
            {
                return _value;
            }
        }

        private FieldGroup[] _fieldGroups = null;
        /// <summary>
        /// ZERO-BASED Array of arrays of Fields which may be repeating.  Note 
        /// the Segment's Field x is FieldGroups[x-1] in the MSH segment!.
        /// </summary>
        public FieldGroup[] FieldGroups
        {
            get 
            {
                //make FieldGroups if not done yet
                if (_fieldGroups == null)
                {
                    // divide into groups separated by fields separator
                    FieldGroup[] tempArray;
                    StringReader _inputStream = new StringReader(_value);
                    //read a char
                    int c;
                    StringBuilder sb = new StringBuilder();
                    bool isEscaped = false;
                    int fldNdx = 0;
                    while ((c = _inputStream.Read()) != -1)
                    {
                        //check for escape
                        if((Convert.ToChar(c) == SepChars.EscapeCharacter)&& (!isEscaped))
                        {
                            isEscaped = true;
                        }
                        else if((Convert.ToChar(c) == SepChars.FieldSeparator) &&
                            (!isEscaped))
                        {
                            //save this  field group and get ready for more
                            if (fldNdx == 0)
                                _fieldGroups = new FieldGroup[1];
                            else
                            {
                                tempArray = new FieldGroup[fldNdx + 1];
                                Array.Copy(_fieldGroups, tempArray, _fieldGroups.Length);
                                _fieldGroups = tempArray;
                            }
                            _fieldGroups[fldNdx] = new FieldGroup(sb.ToString(), SepChars);
                            fldNdx++;
                            sb = new StringBuilder();
                        }
                        else
                        {
                            //append char
                            //replace esc if any
                            if (isEscaped)
                            {
                                sb.Append(SepChars.EscapeCharacter);
                                //reset
                                isEscaped = false;
                            }
                            sb.Append(Convert.ToChar(c));
                        }
                    }//from while reading input
                    //save last one
                    //save this repeating field and get ready for more
                    if (fldNdx == 0)
                        _fieldGroups = new FieldGroup[1];
                    else
                    {
                        tempArray = new FieldGroup[fldNdx + 1];
                        Array.Copy(_fieldGroups, tempArray, _fieldGroups.Length);
                        _fieldGroups = tempArray;
                    }
                    _fieldGroups[fldNdx] = new FieldGroup(sb.ToString(), SepChars);
                    fldNdx++;
                    sb = new StringBuilder();
                }//from if _FieldGroups hasn't been built yet
                return _fieldGroups;
            }//get
        }

        /// <summary>
        /// segment (line) of data from hl7 data
        /// </summary>
        /// <param name="value"></param>
        /// <param name="sepChars"></param>
        public Segment(string value, HL7SeparatorChars sepChars)
        {
            _value = value;
            SepChars = sepChars;
        }
    }

    /// <summary>
    /// group of fields occupying a single sequence number in a segment;
    /// i.e. a single field or group of repeating fields
    /// </summary>
    public class FieldGroup
    {
        private string _value = string.Empty;
        /// <summary>
        /// the whole text  which may  include escape characters
        /// unless stripped
        /// </summary>
        /// <param name="stripEscChars">normally set to true unless you
        /// plan to manually subdivide by special characters</param>
        public string Value(bool stripEscChars)
        {
            if (stripEscChars)
            {
                return Message.StripEscapes(_value, SepChars.EscapeCharacter);
            }
            else
            {
                return _value;
            }
        }

        /// <summary>
        /// separation chars
        /// </summary>
        public HL7SeparatorChars SepChars;

        private Field[] _fields = null;
        /// <summary>
        /// zero based array of fields in the group
        /// </summary>
        public Field[] Fields
        {
            get
            {
                if (_fields == null)
                {
                    //get fields from group
                    // divide into fields separated by repeating field char
                    Field[] tempArray;
                    StringReader _inputStream = new StringReader(_value);
                    //read a char
                    int c;
                    StringBuilder sb = new StringBuilder();
                    bool isEscaped = false;
                    int repNdx = 0;
                    while ((c = _inputStream.Read()) != -1)
                    {
                        //check for escape
                        if ((Convert.ToChar(c) == SepChars.EscapeCharacter) && (!isEscaped))
                        {
                            isEscaped = true;
                        }
                        else if((Convert.ToChar(c) == SepChars.RepetitionCharacter) &&
                            (!isEscaped))
                        {
                            //save this repeating field and get ready for more
                            if (repNdx == 0)
                                _fields = new Field[1];
                            else
                            {
                                tempArray = new Field[repNdx + 1];
                                Array.Copy(_fields, tempArray, _fields.Length);
                                _fields = tempArray;
                            }
                            _fields[repNdx] = new Field(sb.ToString(), SepChars);
                            repNdx++;
                            sb = new StringBuilder();
                        }
                        else
                        {
                            //append char
                            //replace esc if any
                            if (isEscaped)
                            {
                                sb.Append(SepChars.EscapeCharacter);
                                //reset 
                                isEscaped = false;
                            }
                            sb.Append(Convert.ToChar(c));
                        }
                    }//from while reading input
                    //save last one
                    //save this repeating field and get ready for more
                    if (repNdx == 0)
                        _fields = new Field[1];
                    else
                    {
                        tempArray = new Field[repNdx + 1];
                        Array.Copy(_fields, tempArray, _fields.Length);
                        _fields = tempArray;
                    }
                    _fields[repNdx] = new Field(sb.ToString(), SepChars);
                    repNdx++;
                    sb = new StringBuilder();
                }//from if _FieldGroups hasn't been built yet
                return _fields;
            }
        }

        /// <summary>
        /// group of fields in one sequence position in a segment (line);
        /// may be one field or repeating fields.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="sepChars"></param>
        public FieldGroup(string value, HL7SeparatorChars sepChars)
        {
            _value = value;
            SepChars = sepChars;
        }
    }

    /// <summary>
    /// a subdivision of a Message Segment
    /// </summary>
    public class Field
    {
        private string _value = string.Empty;
        /// <summary>
        /// the whole text  which may  include escape characters
        /// unless stripped
        /// </summary>
        /// <param name="stripEscChars">normally set to true unless you
        /// plan to manually subdivide by special characters</param>
        public string Value(bool stripEscChars)
        {
            if (stripEscChars)
            {
                return Message.StripEscapes(_value, SepChars.EscapeCharacter);
            }
            else
            {
                return _value;
            }

        }
        /// <summary>
        /// separation chars
        /// </summary>
        public HL7SeparatorChars SepChars;


        /// <summary>
        /// zero-based array of components, empty if not set
        /// </summary>
        private Component[] _components = null;
        /// <summary>
        /// zero based array of components in the field
        /// </summary>
        public Component[] Components
        { 
            get
            {
                if (_components == null)
                {
                    //get components from field
                    // divide into components separated by component char
                    Component[] tempArray;
                    StringReader _inputStream = new StringReader(_value);
                    //read a char
                    int c;
                    StringBuilder sb = new StringBuilder();
                    bool isEscaped = false;
                    int cmpNdx = 0;
                    while ((c = _inputStream.Read()) != -1)
                    {
                        //check for escape
                        if ((Convert.ToChar(c) == SepChars.EscapeCharacter) && (!isEscaped))
                        {
                            isEscaped = true;
                        }
                        else if((Convert.ToChar(c) == SepChars.ComponentSeparator) &&
                            (!isEscaped))
                        {
                            //save this component and get ready for more
                            if (cmpNdx == 0)
                                _components = new Component[1];
                            else
                            {
                                tempArray = new Component[cmpNdx + 1];
                                Array.Copy(_components, tempArray, _components.Length);
                                _components = tempArray;
                            }
                            _components[cmpNdx] = new Component(sb.ToString(), SepChars);
                            cmpNdx++;
                            sb = new StringBuilder();
                        }
                        else
                        {
                            //append char
                            //replace esc if any
                            if (isEscaped)
                            {
                                sb.Append(SepChars.EscapeCharacter);
                                //reset 
                                isEscaped = false;
                            }
                            sb.Append(Convert.ToChar(c));
                        }
                    }//from while reading input
                    //save last one
                    //save this component and get ready for more
                    if (cmpNdx == 0)
                        _components = new Component[1];
                    else
                    {
                        tempArray = new Component[cmpNdx + 1];
                        Array.Copy(_components, tempArray, _components.Length);
                        _components = tempArray;
                    }
                    _components[cmpNdx] = new Component(sb.ToString(), SepChars);
                    cmpNdx++;
                    sb = new StringBuilder();
                }//from if _Components hasn't been built yet
                return _components;
            }
        }


        /// <summary>
        /// create field with given value;  
        /// </summary>
        /// <param name="rawValue">the string value of whole field, including
        /// any components and escape chars</param>
        ///<param name="sepChars"></param>
        public Field(string rawValue, 
            HL7SeparatorChars sepChars)
        {
            //separate out the components
            // got here...
            //save the plain value
            this._value = rawValue;
            this.SepChars = sepChars;
        }
    }

    /// <summary>
    /// a subdivision of a Message Segment Field
    /// </summary>
    public class Component
    {
        private string _value = string.Empty;
        /// <summary>
        /// the whole text  which may  include escape characters
        /// unless stripped
        /// </summary>
        /// <param name="stripEscChars">normally set to true unless you
        /// plan to manually subdivide by special characters</param>
        public string Value(bool stripEscChars)
        {
            if (stripEscChars)
            {
                return Message.StripEscapes(_value, SepChars.EscapeCharacter);
            }
            else
            {
                return _value;
            }

        }
        /// <summary>
        /// separation chars
        /// </summary>
        public HL7SeparatorChars SepChars;

        /// <summary>
        /// zero-based array of sub components,  empty if not set
        /// </summary>
        private SubComponent[] _subComponents = null;
        /// <summary>
        /// zero based array of sub components in the component
        /// </summary>
        public SubComponent[] SubComponents
        {
            get
            {
                if (_subComponents == null)
                {
                    //get sub components from component
                    // divide into sub components separated by subcomponent char
                    SubComponent[] tempArray;
                    StringReader _inputStream = new StringReader(_value);
                    //read a char
                    int c;
                    StringBuilder sb = new StringBuilder();
                    bool isEscaped = false;
                    int scNdx = 0;
                    while ((c = _inputStream.Read()) != -1)
                    {
                        //check for escape
                        if ((Convert.ToChar(c) == SepChars.EscapeCharacter) && (!isEscaped))
                        {
                            isEscaped = true;
                        }
                        else if((Convert.ToChar(c) == SepChars.SubComponentSeparator) &&
                            (!isEscaped))
                        {
                            //save this sub component and get ready for more
                            if (scNdx == 0)
                                _subComponents = new SubComponent[1];
                            else
                            {
                                tempArray = new SubComponent[scNdx + 1];
                                Array.Copy(_subComponents, tempArray, _subComponents.Length);
                                _subComponents = tempArray;
                            }
                            _subComponents[scNdx] = new SubComponent(sb.ToString(), SepChars);
                            scNdx++;
                            sb = new StringBuilder();
                        }
                        else
                        {
                            //append char
                            //replace esc if any
                            if (isEscaped)
                            {
                                sb.Append(SepChars.EscapeCharacter);
                                //reset 
                                isEscaped = false;
                            }
                            sb.Append(Convert.ToChar(c));
                        }
                    }//from while reading input
                    //save last one
                    //save this sub component and get ready for more
                    if (scNdx == 0)
                        _subComponents = new SubComponent[1];
                    else
                    {
                        tempArray = new SubComponent[scNdx + 1];
                        Array.Copy(_subComponents, tempArray, _subComponents.Length);
                        _subComponents = tempArray;
                    }
                    _subComponents[scNdx] = new SubComponent(sb.ToString(), SepChars);
                    scNdx++;
                    sb = new StringBuilder();
                }//from if _subComponents hasn't been built yet
                return _subComponents;
            }
        }

        /// <summary>
        /// component of a field
        /// </summary>
        /// <param name="value"></param>
        /// <param name="sepChars"></param>
        public Component(string value, HL7SeparatorChars sepChars)
        {
            this._value = value;
            this.SepChars = sepChars;
        }
    }

    /// <summary>
    /// a subdivision of a Message Segment Field Component
    /// </summary>
    public class SubComponent
    {
        private string _value = string.Empty;
        /// <summary>
        /// the whole text  which may  include escape characters
        /// unless stripped
        /// </summary>
        /// <param name="stripEscChars">normally set to true unless you
        /// plan to manually subdivide by special characters</param>
        public string Value(bool stripEscChars)
        {
            if (stripEscChars)
            {
                return Message.StripEscapes(_value, SepChars.EscapeCharacter);
            }
            else
            {
                return _value;
            }
        }
        /// <summary>
        /// separation chars
        /// </summary>
        public HL7SeparatorChars SepChars;

        /// <summary>
        /// sub component of component
        /// </summary>
        /// <param name="value"></param>
        /// <param name="sepChars"></param>
        public SubComponent(string value, HL7SeparatorChars sepChars)
        {
            this._value = value;
            this.SepChars = sepChars;
        }
    }
}
