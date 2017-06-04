using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Wve.WveXML
{
    ///// <summary>
    ///// XML attribute
    ///// </summary>
    //public class WAttribute
    //{
    //    /// <summary>
    //    /// null, or prefix for localname
    //    /// </summary>
    //    public string Prefix;
    //    /// <summary>
    //    /// name of the attribute
    //    /// </summary>
    //    public string LocalName;
    //    /// <summary>
    //    /// null, or the namespace for the name
    //    /// </summary>
    //    public string Namespace;
    //    /// <summary>
    //    /// value of the attribute
    //    /// </summary>
    //    public string Value;
    //    /// <summary>
    //    /// returns a WCDAAtribute structure with given values
    //    /// </summary>
    //    /// <param name="prefix">null or prefix to localname</param>
    //    /// <param name="localname">name</param>
    //    /// <param name="ns">null or namespace for name</param>
    //    /// <param name="value">value of attribute</param>
    //    /// <returns></returns>
    //    public WAttribute(string prefix, string localname, string ns, string value)
    //    {
    //        this.Prefix = prefix;
    //        this.LocalName = localname;
    //        this.Namespace = ns;
    //        this.Value = value;
    //    }

    //    /// <summary>
    //    /// returns a WCDAATribute structure with given values
    //    /// </summary>
    //    /// <param name="localname"></param>
    //    /// <param name="value"></param>
    //    /// <returns></returns>
    //    public WAttribute(string localname, string value)
    //    {
    //        this.LocalName = localname;
    //        this.Value = value;
    //    }
    //}


    ///// <summary>
    ///// base class for Clinical Document elements
    ///// </summary>
    //public class WElement
    //{
    //    #region fields

    //    /// <summary>
    //    /// name of the xml element
    //    /// </summary>
    //    public string Name;

    //    /// <summary>
    //    /// null, or the namespace to associate with 
    //    /// Name, if any
    //    /// </summary>
    //    public string Namespace = null;

    //    /*
    //     * should we define these?
    //    /// <summary>
    //    /// null, or the prefix for this element name,
    //    /// which must have a prefixNamespace if used
    //    /// </summary>
    //    public string Prefix = null;
    //    /// <summary>
    //    /// the namespace for the Prefix if it is used
    //    /// </summary>
    //    public string PrefixNamespace = null;
    //    */
    //    /// <summary>
    //    /// the text value of the xml element
    //    /// </summary>
    //    public string TextValue;

    //    protected List<string> _comments = null;
    //    /// <summary>
    //    /// optional array of comment strings to 
    //    /// preceed the element with in the xml doument
    //    /// </summary>
    //    public List<string> Comments
    //    {
    //        get
    //        {
    //            if (_comments == null)
    //            {
    //                _comments = new List<string>();
    //            }
    //            return _comments;
    //        }
    //        set { _comments = value; }
    //    }

    //    /// <summary>
    //    /// xml attributes for this element if any
    //    /// </summary>
    //    public WAttribute[] Attributes = null;

    //    /// <summary>
    //    /// CDA Elements that are children of this element if any
    //    /// </summary>
    //    public WElement[] ChildElements = null;


    //    #endregion fields

    //    #region constructors

    //    /// <summary>
    //    /// create CDA Element without specifying name or text value
    //    /// </summary>
    //    public WElement()
    //    {
    //    }

    //    /// <summary>
    //    /// create CDA Element
    //    /// </summary>
    //    /// <param name="name">name of the xml element</param>
    //    /// <param name="textValue">null, or text for its textNode value</param>
    //    public WElement(string name, string textValue)
    //    {
    //        Name = name;
    //        TextValue = textValue;
    //    }

    //    /// <summary>
    //    /// create CDA Element
    //    /// </summary>
    //    /// <param name="name"></param>
    //    /// <param name="textValue"></param>
    //    /// <param name="comments"></param>
    //    public WElement(string name, string textValue, string[] comments)
    //    {
    //        Name = name;
    //        TextValue = textValue;
    //        if ((comments != null) && (comments.Length > 0))
    //        {
    //            for (int i = 0; i < comments.Length; i++)
    //            {
    //                Comments.Add(comments[i]);
    //            }
    //        }
    //    }

    //    #endregion constructors

    //    #region methods

    //    /// <summary>
    //    /// append to this element's attributes
    //    /// and return the new attribute in case 
    //    /// there is further work to do on it
    //    /// </summary>
    //    /// <param name="att"></param>
    //    public WAttribute AddAttribute(WAttribute att)
    //    {
    //        WAttribute[] tempArray = null;
    //        if (Attributes == null)
    //        {
    //            tempArray = new WAttribute[1];
    //        }
    //        else
    //        {
    //            tempArray = new WAttribute[Attributes.Length + 1];
    //            Array.Copy(Attributes, tempArray, Attributes.Length);
    //        }
    //        tempArray[tempArray.Length - 1] = att;
    //        Attributes = tempArray;
    //        return att;
    //    }

    //    /// <summary>
    //    /// delete all attributes with given (case-insensitive) name
    //    /// and return number of deletions made
    //    /// </summary>
    //    /// <param name="name"></param>
    //    /// <returns></returns>
    //    public int DeleteAttributes(string name)
    //    {
    //        int result = 0;
    //        if (Attributes != null)
    //        {
    //            for (int i = 0; i < Attributes.Length; i++)
    //            {
    //                if (Attributes[i].LocalName.ToLower() == name.ToLower())
    //                {
    //                    Attributes = Wve.WveTools.RemoveArrayItem(
    //                        Attributes, i);
    //                    result++;
    //                }
    //            }
    //        }
    //        return result;
    //    }
    //    /// <summary>
    //    /// append to this element's child elements
    //    /// and returns the new child in case there is further 
    //    /// work to do on it
    //    /// </summary>
    //    /// <param name="child"></param>
    //    public WElement AddChildElement(WElement child)
    //    {
    //        WElement[] tempArray = null;
    //        if (ChildElements == null)
    //        {
    //            tempArray = new WElement[1];
    //        }
    //        else
    //        {
    //            tempArray = new WElement[ChildElements.Length + 1];
    //            Array.Copy(ChildElements, tempArray, ChildElements.Length);
    //        }
    //        tempArray[tempArray.Length - 1] = child;
    //        ChildElements = tempArray;
    //        return child;
    //    }


    //    /// <summary>
    //    /// delete all this element's child elements with given 
    //    /// (case-insensitive) name.  Returns number of deletions
    //    /// made
    //    /// </summary>
    //    /// <param name="childName"></param>
    //    /// <returns>number of deletions made</returns>
    //    public int DeleteChildElements(string childName)
    //    {
    //        int result = 0;
    //        if (ChildElements != null)
    //        {
    //            for (int i = 0; i < ChildElements.Length; i++)
    //            {
    //                if (ChildElements[i].Name.ToLower() == childName.ToLower())
    //                {
    //                    ChildElements = Wve.WveTools.RemoveArrayItem(
    //                        ChildElements, i);
    //                    result++;
    //                }
    //            }
    //        }
    //        return result;
    //    }

    //    /// <summary>
    //    /// xml representation of the element
    //    /// </summary>
    //    /// <param name="settings">null, or else specify 
    //    /// your own XmlWriterSettings</param>
    //    /// <returns></returns>
    //    public string ToXML(XmlWriterSettings settings)
    //    {
    //        if (settings == null)
    //        {
    //            settings = new XmlWriterSettings();
    //            settings.Indent = true;
    //            settings.NewLineChars = Environment.NewLine;
    //            settings.NewLineOnAttributes = true;
    //        }

    //        StringBuilder sb = new StringBuilder();
    //        using (XmlWriter xw = XmlWriter.Create(sb, settings))
    //        {
    //            this.WriteTo(xw);
    //        }
    //        return sb.ToString();
    //    }

    //    /// <summary>
    //    /// write xml to XmlWriter
    //    /// </summary>
    //    /// <param name="xw"></param>
    //    public virtual void WriteTo(XmlWriter xw)
    //    {
    //        if (_comments != null)
    //        {
    //            foreach (string comment in _comments)
    //            {
    //                xw.WriteComment(comment);
    //            }
    //        }
    //        if (Namespace == null)
    //            xw.WriteStartElement(Name);
    //        else
    //            xw.WriteStartElement(Name, Namespace);
    //        if (Attributes != null)
    //        {
    //            foreach (WAttribute att in Attributes)
    //            {
    //                if (att.Prefix != null)
    //                    xw.WriteAttributeString(att.Prefix, att.LocalName, att.Namespace, att.Value);
    //                else if (att.Namespace != null)
    //                    xw.WriteAttributeString(att.LocalName, att.Namespace, att.Value);
    //                else
    //                    xw.WriteAttributeString(att.LocalName, att.Value);
    //            }
    //        }
    //        if (TextValue != null)
    //        {
    //            xw.WriteString(TextValue);
    //        }
    //        if (ChildElements != null)
    //        {
    //            foreach (WElement elem in ChildElements)
    //            {
    //                elem.WriteTo(xw);
    //            }
    //        }
    //        xw.WriteEndElement();//name
    //    }
    //    #endregion methods
    //}

}
