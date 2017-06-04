using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Wve.WveHL7
{
    /// <summary>
    /// object to read Health Level 7 "vertical bar" formatted messages
    /// </summary>
    public class HL7Reader : IDisposable
    {
        #region fields

        private string[] _headerSegmentTypes = new string[] { "MSH", "FHS", "BHS" };
        /// <summary>
        /// array of three character strings representing segment types that should 
        /// trigger a HeaderSegmentRead event rather than being assigned to any message,
        /// e.g. 'FHS' and 'BHS' for file and batch header segments or 'MSH' for new
        /// messages...
        /// </summary>
        public string[] HeaderSegmentTypes
        {
            get { return _headerSegmentTypes; }
            set { _headerSegmentTypes = value; }
        }

        private TextReader _inputStream = null;
        /// <summary>
        /// the stream to read from
        /// </summary>
        public TextReader InputStream
        {
            get { return _inputStream; }
        }

        //start with standard values
        private HL7SeparatorChars sepChars = new HL7SeparatorChars();

        private int _linesRead = 0;
        /// <summary>
        /// number of lines read from the stream and assigned to
        /// HL7 messages.  This does not include the first line of the
        /// next message to be processed even though it may have been 
        /// read from the stream to find out it begins a new msg
        /// </summary>
        public int LinesRead
        {
            get { return _linesRead; }
        }

        private int _messagesRead = 0;
        /// <summary>
        /// number of messages being read and sent to the MessageRead handlers.
        /// This is set upon reading the first line (segment) of a message
        /// </summary>
        public int MessagesRead
        {
            get { return _messagesRead; }
        }

        /// <summary>
        /// set to true to interrupt reading before next line is processed
        /// </summary>
        public bool Cancel = false;

        /// <summary>
        /// Event raised after each message is completely read
        /// </summary>
        public event EventHandler<HL7MessageReadEventArgs> MessageRead;

        /// <summary>
        /// evnet raised after reading a segment that's not involved in a message,
        /// which is presumably a file header or batch header segment
        /// </summary>
        public event EventHandler<HL7HeaderSegmentReadEventArgs> HeaderSegmentRead;

        #endregion fields

        #region constructors
        /// <summary>
        /// object to read HL7
        /// </summary>
        /// <param name="inputStream">a streamreader or stringreader</param>
        /// <returns></returns>
        public HL7Reader(TextReader inputStream)
        {
            _inputStream = inputStream;
        }

        /// <summary>
        /// object to read HL7
        /// </summary>
        /// <param name="hl7File"></param>
        public HL7Reader(string hl7File)
        {
            _inputStream = new StreamReader(hl7File);
        }

        #endregion constructors

        #region methods

        /// <summary>
        /// read from the HL7Reader's data stream and call the HeaderRead event after
        /// any header or non-message segements are read, and MessageRead event  after
        /// each message is completely read; returning true if read to 
        /// end of stream without major errors
        /// </summary>
        /// <param name="errors">messages generated from reading, including exceptions</param>
        /// <returns></returns>
        public bool ReadFile(out string errors)
        {
            StringBuilder sbErrors = new StringBuilder();
            bool readToEndSuccessfully = true;// unlesss errors or interrupted
            Message msg = null;
            _linesRead = 0;
            _messagesRead = 0;

            //read from stream
            if (_inputStream != null)
            {
                //read a line 
                while (_inputStream.Peek() != -1)
                {
                    if (Cancel)
                    {
                        sbErrors.Append("Interrupted by Cancel.  ");
                        break;
                    }
                    //read a line from stream here
                    Segment seg = new Segment(_inputStream.ReadLine(), sepChars);
                    if (seg.Value(false).Trim() != string.Empty)
                    {
                        _linesRead++;
                        if (isHeaderSegment(seg))
                        {
                            //first handle any existing messages we were working on
                            if (_messagesRead > 0)
                            {
                                // message is completed; raise event
                                //Jeffrey Richter's book says to save delegate field
                                // to temporary field for thread safety p 229 CLR viaC# 2
                                EventHandler<HL7MessageReadEventArgs> tempHandler = MessageRead;
                                if (tempHandler != null)
                                {
                                    //raise event to handle message
                                    HL7MessageReadEventArgs e = new HL7MessageReadEventArgs(msg);
                                    tempHandler(this, e);
                                    if (!e.Success)
                                    {
                                        //;sbErrors.Append(e.Msg + ".  ");
                                        readToEndSuccessfully = false;
                                    }
                                }
                            }//from if any preexisting messages exist...
                            //now start new message if is new message header
                            if (seg.SegmentType == "MSH")
                            {
                                //read separator chars
                                if (seg.Value(false).Length > 7)
                                {
                                    sepChars.FieldSeparator = char.Parse(seg.Value(false).Substring(3, 1));
                                    sepChars.ComponentSeparator =
                                        char.Parse(seg.Value(false).Substring(4, 1));
                                    sepChars.RepetitionCharacter =
                                        char.Parse(seg.Value(false).Substring(5, 1));
                                    sepChars.EscapeCharacter =
                                        char.Parse(seg.Value(false).Substring(6, 1));
                                    sepChars.SubComponentSeparator =
                                        char.Parse(seg.Value(false).Substring(7, 1));
                                }
                                //now create new message
                                msg = new Message();
                                _messagesRead++;
                                msg.Segments.Add(seg);
                            }
                            //now raise event for the header segment
                            //Jeffrey Richter's book says to save delegate field
                            // to temporary field for thread safety p 229 CLR viaC# 2
                            EventHandler<HL7HeaderSegmentReadEventArgs> tempHeaderHandler = HeaderSegmentRead;
                            if (tempHeaderHandler != null)
                            {
                                //raise event to handle message
                                HL7HeaderSegmentReadEventArgs e = new HL7HeaderSegmentReadEventArgs(seg);
                                tempHeaderHandler(this, e);
                                if (!e.Success)
                                {
                                    //sbErrors.Append(e.Msg + ".  ");
                                    readToEndSuccessfully = false;
                                }
                            }
                        }//from if is header segment
                        else
                        {
                            //not a new message so add segment to current message
                            if (msg == null)
                                msg = new Message();
                            msg.Segments.Add(seg);
                        }//from if segment doesn't begin a new message, etc
                    }//from if seg not empty
                }//from if peek isn't empty (end of stream)

                //either got to end of stream or was cancelled
                if (readToEndSuccessfully) //ie if no errors already reset to false
                    readToEndSuccessfully = (_inputStream.Peek() == -1);
                //call event one last time
                if ((readToEndSuccessfully) && (msg != null))
                {
                    //Jeffrey Richter's book says to save delegate field
                    // to temporary field for thread safety p 229 CLR viaC# 2
                    EventHandler<HL7MessageReadEventArgs> tempHandler = MessageRead;
                    if (tempHandler != null)
                    {
                        //raise event to handle message
                        HL7MessageReadEventArgs e = new HL7MessageReadEventArgs(msg);
                        tempHandler(this, e);
                        if (!e.Success)
                        {
                            //;sbErrors.Append(e.Msg + ".  ");
                            readToEndSuccessfully = false;
                        }
                    }
                }

            }//from if input stream isn't null
            else
            {
                throw new Exception("Tried to read HL7 data from stream which didn't exist");
            }
            //return
            errors = sbErrors.ToString();
            return readToEndSuccessfully;
        }

        //return true if segment type is in the headerSegmentTypes array
        private bool isHeaderSegment(Segment seg)
        {
            for(int i=0; i< _headerSegmentTypes.Length; i++)
            {
                if (_headerSegmentTypes[i] == seg.SegmentType)
                {
                    return true;
                }
            }
            //if got here must not be in the list
            return false;
        }

        /*
        /// <summary>
        /// read a line from stream and create a Segment object out of it
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="seg"></param>
        /// <returns></returns>
        private Segment readSegment( TextReader inputStream)
        {
            bool isEscaped = false; //note that next char should be treated as escaped
            //;bool replaceEscChar = false; //put back in an escape char we removed last step
            //;bool currentIsRepeating = false;//indicates if current newField is part rpting flds
            StringBuilder newField = new StringBuilder() ;
            //zero based field ordinal
            int fieldNdx = 0;
            Segment seg = null;
            string line = inputStream.ReadLine();
            //ignore empty lines
            if ((line != null) &&(line.Trim().Length > 0))
            {
                seg = new Segment();
                seg.Value = line;
                //create spot for first field group
                
                //read segment for fields, which may be repeated
                for (int i = 0; i < line.Length; i++)
                {
                    //replace esc character if previous char was the esc char
                    // but this one is a character that should be passed on
                    // as escaped (e.g. Component or Subcomponent separators)
                    replaceEscChar = ((isEscaped) && 
                        ((line.Substring(i, 1) == ComponentSeparator) ||
                        (line.Substring(i, 1) == SubComponentSeparator)));
                    //set esc flag if this is an escape character
                    if ((line.Substring(i, 1) == EscapeCharacter) && (!isEscaped))
                    {
                        isEscaped = true;
                    }
                    //or if is field separator (|)
                    else if((line.Substring(i,1) == FieldSeparator) && (!isEscaped))
                    {
                        // new field
                        if (!currentIsRepeating)
                        {
                            //simple field
                            Field[] newFields = new Field[] { new Field(newField.ToString(),
                                ComponentSeparator, SubComponentSeparator, EscapeCharacter) };
                            //append to array
                            appendFieldsArray(newFields, seg);
                        }
                        else
                        {
                            //save current field as a repeating field
                            appendRepeatingField(new Field(newField.ToString(),
                                ComponentSeparator, SubComponentSeparator, EscapeCharacter),
                                seg, fieldNdx);
                        }
                        //increment index of next field
                        fieldNdx++;
                        currentIsRepeating = false;//unless repetition char is found later
                        isEscaped = false;//reset
                    }
                    //or if is repetition character... (~)
                    else if ((line.Substring(i, 1) == RepetitionCharacter) && (!isEscaped))
                    {
                        //new repeating field
                        currentIsRepeating = true;
                        appendRepeatingField(new Field(newField.ToString(),
                            ComponentSeparator, SubComponentSeparator, EscapeCharacter),
                            seg, fieldNdx);
                        isEscaped = false;//reset
                    }
                    //otherwise just a regular character
                    else
                    {
                        //just add the character
                        // (but prepend the escape character if directed to)
                        if (replaceEscChar)
                        {
                            newField.Append(EscapeCharacter);
                        }
                        //(and now the character)
                        newField.Append(line.Substring(1,1));
                        isEscaped = false;//reset
                    }
                }//from for each character...
            }//from if there's a nonzero line
            return seg;
        }

        /// <summary>
        /// append given fields array to segment's array of fields arrays
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="seg"></param>
        private void appendFieldsArray(Field[] fields, Segment seg)
        {
            Field[][] newFieldsArray = new Field[seg.FieldsArray.Length + 1][];
            Array.Copy(seg.FieldsArray, newFieldsArray, seg.FieldsArray.Length);
            newFieldsArray[newFieldsArray.Length - 1] = fields;
            seg.FieldsArray = newFieldsArray;
        }

        /// <summary>
        /// append a field as a repeating field to FieldsArray
        /// </summary>
        /// <param name="field"></param>
        /// <param name="seg"></param>
        /// <param name="fieldNdx"></param>
        private void appendRepeatingField(Field field, Segment seg, int fieldNdx)
        {
            //is a repeating field so append to array of fields in current field position
            //(which we may need to create)
            if (seg.FieldsArray.Length < fieldNdx + 1)
            {
                //make new empty array to append to
                Field[][] newFieldsArray = new Field[fieldNdx + 1][];
                Array.Copy(seg.FieldsArray, newFieldsArray, seg.FieldsArray.Length);
                newFieldsArray[fieldNdx] = new Field[0];
                seg.FieldsArray = newFieldsArray;
            }
            //append to FieldsArray
            Field[] newFields = new Field[seg.FieldsArray[fieldNdx].Length + 1];
            Array.Copy(seg.FieldsArray[fieldNdx],
                newFields,
                seg.FieldsArray[fieldNdx].Length);
            newFields[newFields.Length - 1] = field;
            seg.FieldsArray[fieldNdx] = newFields;
        }

        */
        #endregion methods



        #region IDisposable Members

        /// <summary>
        /// explicitly close (dispose)
        /// </summary>
        public void Close()
        {
            Dispose(true);
        }

        /// <summary>
        /// explicitly dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// destructor
        /// </summary>
        ~HL7Reader()
        {
            Dispose(false);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing">true if code is calling Dispose() or 
        /// Close() but false if CLR is finalizing</param>
        protected virtual void Dispose (bool disposing)
        {
            if (disposing)
            {
                //optional code here if code calls Close() rather than 
                // from the garbabe collector's finalizing
            }
            //close input stream
            if (_inputStream != null) 
            {
                _inputStream.Close();
            }
        }

        #endregion
    }
}
