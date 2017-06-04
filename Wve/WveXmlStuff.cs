using System;
using System.Collections.Specialized;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Xsl;
using System.IO;

namespace Wve
{
	/// <summary>
	/// XML related utilities
	/// </summary>
	public static class WveXml
	{

        /// <summary>
        /// create an xml element with the given text node
        /// </summary>
        /// <param name="xDoc"></param>
        /// <param name="elementName"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public static XmlElement CreateElementWithText(
            XmlDocument xDoc,
            string elementName,
            string text)
        {
            XmlElement result = xDoc.CreateElement(elementName);
            result.AppendChild(xDoc.CreateTextNode(text));
            return result;
        }

		/// <summary>
		/// Clones nodes from first treeview to the clone.
		/// Note they share the same Tag objects
		/// Returns number of nodes cloned
		/// Thanks to George Shepherd at syncfusion.com
		/// </summary>
		/// <param name="tvDonor"></param>
		/// <param name="tvClone"></param>
		/// <returns></returns>
		public static int CloneTreeView( TreeView tvDonor,  TreeView tvClone)
		{
			int nodesCount = 0;
			//clear clone to be
			tvClone.Nodes.Clear();
			//make first order nodes
			foreach(TreeNode tnDonor in tvDonor.Nodes)
			{
				TreeNode tnClone = new TreeNode(tnDonor.Text);
				tnClone.Tag = tnDonor.Tag;
				tvClone.Nodes.Add(tnClone);
				nodesCount++;
				nodesCount = iterateCloneTreeNodes(tnDonor, tnClone, nodesCount);
			}
			return nodesCount;
		}

		//for use with CloneTreeView()
		private static int iterateCloneTreeNodes (TreeNode tnDonor, TreeNode tnClone, 
			int nodesCount)
		{ 
			foreach( TreeNode childNode in tnDonor.Nodes) 
			{ 
				TreeNode newNode = new TreeNode(childNode.Text);           
				newNode.Tag = childNode.Tag; 
				tnClone.Nodes.Add(newNode);
				nodesCount++;
				nodesCount = iterateCloneTreeNodes(childNode, newNode, nodesCount); 
			} 
			return nodesCount;
		} 
 


		/// <summary>
		/// returns a string collection of names of nodes in the 
		///  hierarchy (ancestors) of the given node in its XmlDocument.
		/// Calls local method getNextNameInHierarchy()
		/// Begins collection with name of node itself (reverse order)
		/// </summary>
		/// <param name="xNode"></param>
		/// <returns></returns>
		public static StringCollection XNodeHierarchy(XmlNode xNode)
		{
			StringCollection sc = new StringCollection();
			if(xNode != null)
				getHierarchyOfNames(xNode,sc);
			return sc;
		}
		private static void getHierarchyOfNames(XmlNode xNode, StringCollection sc)
		{
			sc.Add(xNode.Name);
			if(xNode.ParentNode != null)
				getHierarchyOfNames(xNode.ParentNode,sc);
		}

		/// <summary>
		/// replaces single quotes with two in a row.
		/// Generates exception if contains the words INSERT, DELETE, UPDATE
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public static string GroomQueryText(string input)
		{
			string output = Regex.Replace(input,@"'","''");
			if((output.ToLower().IndexOf("insert")>-1)||
				(output.ToLower().IndexOf("delete")>-1)||
				(output.ToLower().IndexOf("update")>-1))
				throw new Exception("Query may not contain reserved words " +
					"such as INSERT, DELETE or UPDATE.");
			return output;
		}

        /// <summary>
        /// groom text for insertion into a text node in xml, 
        /// replacing less than and ampersand in simple mode, 
        /// and replacing greater than, apostrophe and quote
        /// otherwise.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="simple"></param>
        /// <returns></returns>
        public static string GroomTextNodeText(string input, bool simple)
        {
            string result = input.Replace("&", "&amp;"); //must do first!
            result = result.Replace("<", "&lt;");
            if(!simple)
            {
                result = result.Replace(">", "&gt;");
                result = result.Replace("'", "&apos;");
                result = result.Replace("\"","$quot;");
            }
            return result;
        }

		/// <summary>
		/// This overload also restricts length
		/// replaces single quotes with two in a row.
		/// Generates exception if contains the words INSERT, DELETE, UPDATE
		/// </summary>
		/// <param name="input"></param>
		/// <param name="maxLength"></param>
		/// <returns></returns>
		public static string GroomQueryText(string input, int maxLength)
		{
			if(input.Length > maxLength)
				throw new Exception("Query string is too long.");
			string output = Regex.Replace(input,@"'","''");
			if((output.ToLower().IndexOf("insert")>-1)||
				(output.ToLower().IndexOf("delete")>-1)||
				(output.ToLower().IndexOf("update")>-1))
				throw new Exception("Query may not contain reserved words " +
					"such as INSERT, DELETE or UPDATE.");
			return output;
		}

		/// <summary>
		/// returns a new node belonging to newHostDoc,
		///  having name, attributes and text of original node
		/// </summary>
		/// <param name="node">node to splice, 
		///  belonging to different XmlDocument</param>
		/// <param name="newXDoc">XmlDocument to splice node into</param>
		/// <param name="includeDescendants">include child nodes if true</param>
		/// <returns></returns>
		public static XmlElement CopyNode (XmlElement node, XmlDocument newXDoc,
			bool includeDescendants)
		{
			XmlAttribute att;
			//create node with same name
			XmlElement newNode = newXDoc.CreateElement(node.Name);
			//add attributes
			for(int i=0; i<node.Attributes.Count; i++)
			{
				att = newXDoc.CreateAttribute(node.Attributes[i].Name);
				att.Value = node.Attributes[i].Value;
				newNode.Attributes.Append(att);
			}
			//add innerXml if includeDescendants is true
			if(includeDescendants)
				newNode.InnerXml = node.InnerXml;
			else
			{
				//add the text node only, not the descendents
				newNode.InnerXml = "";
				for(int i=0; i<node.ChildNodes.Count; i++)
				{
					if(node.ChildNodes[i].NodeType == XmlNodeType.Text)
					{
						newNode.InnerText += node.ChildNodes[i].Value;
						//; not->newNode.AppendChild(node.ChildNodes[i]);
					}
				}
			}
			//and return the node
			return newNode;
		}

		/// <summary>
		/// returns a new XMlElement belonging to newXDoc,
		///  having given name, but OuterXML of fragment's first child,
		///  or null if XmlFragment isn't made of an XmlElement
		/// </summary>
		/// <param name="frag">fragment to splice, 
		///  possibly belonging to different XmlDocument</param>
		/// <param name="newXDoc">XmlDocument to splice node into</param>
		/// <param name="newName">name for XmlElement</param>
		/// <param name="includeDescendants">include child nodes if true</param>
		/// <returns></returns>
		public static XmlElement XmlElementFromFragment (XmlDocumentFragment frag, 
			XmlDocument newXDoc, string newName,
			bool includeDescendants)
		{
			XmlElement newNode = newXDoc.CreateElement(newName);
			//create node to hold our new node
			XmlElement holder = newXDoc.CreateElement("Holder");
			XmlDocumentFragment xfNew = newXDoc.CreateDocumentFragment();
			xfNew.InnerXml = frag.InnerXml;
			
			//add the frag as its child
			holder.AppendChild(xfNew);
			
			//and make a node with our name out of the node if it exists
			if(holder.FirstChild.NodeType == XmlNodeType.Element)
			{
				//copy the frag's attributes and inner xml (true)
				WveXml.ApplyAttrAndInner((XmlElement)holder.FirstChild,
					newNode,true);
				return newNode;
			}
			else
				return null;
		}

		/// <summary>
		/// apply a source node's attributes and (if desired) inner xml
		///  to target node
		/// </summary>
		/// <param name="source"></param>
		/// <param name="target"></param>
		/// <param name="includeInnerXML"></param>
		public static void ApplyAttrAndInner(XmlElement source, 
			XmlElement target, bool includeInnerXML)
		{
			XmlAttribute att;
			//add attributes
			for(int i=0; i<source.Attributes.Count; i++)
			{
				att = source.Attributes[i];
				target.Attributes.SetNamedItem(att);
			}
			//add text node if indicated
			if(includeInnerXML)
			{
				target.InnerXml = source.InnerXml;
			}
		}

        /// <summary>
        /// apply source node's attributes to target and replaces
        /// target's text nodes with the source node's text node(s).
        /// Does not change target's inner xml otherwise.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public static void ApplyAttrAndText(XmlElement source,
            XmlElement target)
        {
            //apply attributes
            for (int i = 0; i < source.Attributes.Count; i++)
            {
                target.Attributes.SetNamedItem(
                    source.Attributes[i]);
            }
            //remove any existing text nodes in target
            for (int i = 0; i < target.ChildNodes.Count; i++)
            {
                if (target.ChildNodes[i].NodeType == XmlNodeType.Text)
                {
                    target.RemoveChild(target.ChildNodes[i]);
                }
            }
            //add any text nodes existing in source
            for (int i = 0; i < source.ChildNodes.Count; i++)
            {
                if (source.ChildNodes[i].NodeType == XmlNodeType.Text)
                {
                    target.AppendChild(target.OwnerDocument.CreateTextNode(
                        source.ChildNodes[i].Value));
                }
            }
        }

        /// <summary>
        /// Apply the text string to the element's text
        /// nodes, or create text node if none.  
        /// This is obsolete.  The SetTextValue
        /// method is preferred because this one could generate
        /// duplicate text nodes if multiple ones exist.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="text"></param>
        public static void ApplyText(XmlElement element, string text)
		{
			//add the text node only, not the descendents
			bool foundTextNode = false;
			for(int i=0; i<element.ChildNodes.Count; i++)
			{
				if(element.ChildNodes[i].NodeType == XmlNodeType.Text)
				{
					element.ChildNodes[i].Value = text;
					foundTextNode = true;
					break;
				}
			}
			if(!foundTextNode)
			{
				//make one
				element.AppendChild(element.OwnerDocument.CreateTextNode(text));
			}
		}

		/// <summary>
		/// check that the xml document meets basic standards for a chart
		///  note template
		/// </summary>
		/// <param name="xDoc"></param>
		/// <returns></returns>
		public static bool CheckSyntax(XmlDocument xDoc)
		{
			//has to at leat have <NoteTemplate\> with child
			// <NoteElements\> under it.  
			bool foundElements = false;
			if((xDoc.DocumentElement == null)||
				(xDoc.DocumentElement.LocalName != "NoteTemplate") )
			{
				return false;
			}
			else
			{
				//find NoteElements
				for(int i=0; i<xDoc.DocumentElement.ChildNodes.Count; i++)
				{
					if(xDoc.DocumentElement.ChildNodes[i].LocalName == "NoteElements")
					{
						foundElements = true;
					}
				}
			}
			return foundElements;
		}

        
        /// <summary>
        /// returns true and assigns attributeValule if attribute attributeName
        /// exists in the element; returns false and assigns empty string if not.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="attributeName"></param>
        /// <param name="caseSensitive"></param>
        /// <param name="attributeValue"></param>
        /// <returns></returns>
        public static bool HasAttribute(XmlElement element, string attributeName,
            bool caseSensitive, out string attributeValue)
        {
            bool foundAttribute = false; // unless found below
            attributeValue = ""; //unless found below

            for (int i = 0; i < element.Attributes.Count; i++)
            {
                //apply case sensitivity as desired

                if ((element.Attributes[i].Name == attributeName) ||
                    (!caseSensitive && (element.Attributes[i].Name.ToLower() == attributeName.ToLower())))
                {
                    foundAttribute = true;
                    attributeValue = element.Attributes[i].Value;
                    break;
                }
            }
            return foundAttribute;
        }
        /// <summary>
        /// true if given element has a child element with given name and 
        /// sends out the first matching child element; returns false and 
        /// sends out null object if not
        /// </summary>
        /// <param name="elementToSearch"></param>
        /// <param name="name"></param>
        /// <param name="caseSensitive">specifies whether search should be case sensitive</param>
        /// <param name="firstMatchingChild"></param>
        /// <returns></returns>
        public static bool HasElement(XmlElement elementToSearch, 
            string name, 
            bool caseSensitive,
            out XmlElement firstMatchingChild)
        {
            bool foundElement = false; //unless found below
            firstMatchingChild = null;  //unless assigned below
            for (int i = 0; i < elementToSearch.ChildNodes.Count; i++)
            {
                if((elementToSearch.ChildNodes[i].GetType() == typeof(XmlElement)) &&
                    ((elementToSearch.ChildNodes[i].Name == name) ||
                    ((!caseSensitive) && 
                    (elementToSearch.ChildNodes[i].Name.ToLower() == name.ToLower()))))
                {
                    foundElement = true;
                    firstMatchingChild = (XmlElement)elementToSearch.ChildNodes[i];
                    break;
                }
            }
            return foundElement;
        }

        /// <summary>
        /// sets value of requested attribute and returns true if that
        /// attribute was found in the element, false if not; Optionally
        /// creates attribute with that value if not found.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="attributeName"></param>
        /// <param name="attributeValue"></param>
        /// <param name="caseSensitive"></param>
        /// <param name="createIfNotFound">create an attribute with that value if
        /// that attribute doesn't exist - still returns false to indicate wasn't 
        /// present initially</param>
        /// <returns></returns>
        public static bool SetAttribute(XmlElement element, string attributeName,
            string attributeValue, bool caseSensitive, bool createIfNotFound)
        {
            bool foundAttribute = false; //unless found
            for (int i = 0; i < element.Attributes.Count; i++)
            {
                //apply case sensitivity as desired
                if ((element.Attributes[i].Value == attributeName) ||
                    (!caseSensitive && (element.Attributes[i].Value.ToLower() == attributeName.ToLower())))
                {
                    foundAttribute = true;
                    element.Attributes[i].Value = attributeValue;
                    break;
                }
            }
            //now create an attribute with that value if one didn't exist
            if (!foundAttribute)
            {
                XmlAttribute newAtt = element.OwnerDocument.CreateAttribute(attributeName);
                newAtt.Value = attributeValue;
                element.Attributes.Append(newAtt);
            }
            return foundAttribute;
        }
        /// <summary>
        /// returns the concactenated values of the given xmlElement's text nodes
        /// but not those of its child nodes.  It seems this would be the logical
        /// result of XmlElement.Value but it isn't
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static string GetTextValue(XmlElement element)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < element.ChildNodes.Count; i++)
            {
                if (element.ChildNodes[i].NodeType == XmlNodeType.Text)
                {
                    sb.Append(element.ChildNodes[i].Value);
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// Create a text node for given element
        /// replacing any existing text nodes.
        /// Any existing child elements are left unchanged.
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetTextValue(XmlElement element, string value)
        {
            //first delete any existing text nodes
            for (int i = 0; i < element.ChildNodes.Count; i++)
            {
                if (element.ChildNodes[i].NodeType == XmlNodeType.Text)
                {
                    element.RemoveChild(element.ChildNodes[i]);
                }
            }
            //and now prepend the text
            XmlNode newChild = element.OwnerDocument.CreateTextNode(value);
            element.PrependChild(newChild);
        }
        /// <summary>
        /// groom string for use as an xml node name.
        /// Name can't begin with a number, so prepends "A" if it does
        /// Whitespace is removed.
        /// (overloaded)
        /// </summary>
        /// <param name="rawName"></param>
        /// <returns></returns>
        public static string GroomNodeName(string rawName)
        {
            StringBuilder sb = new StringBuilder();
            //get rid of white space
            rawName = Regex.Replace(rawName, @"\W", "");
            //and append a character if first char is digit
            if ((rawName.Length == 0) ||
                (Regex.IsMatch(rawName.Substring(0, 1), @"\d")))
                sb.Append("A");
            sb.Append(rawName);
            return sb.ToString();
        }
        /// <summary>
        /// groom string for use as an xml node name.
        /// Name can't begin with a number, so prepends 
        /// the text given in textToPrependIF if raw name
        /// begins with number (or is empty)
        /// Whitespace is removed.
        /// (overloaded)
        /// </summary>
        /// <param name="rawName"></param>
        /// <param name="textToPrependIF"></param>
        /// <returns></returns>
        public static string GroomNodeName(string rawName, string textToPrependIF)
        {
            StringBuilder sb = new StringBuilder();
            //get rid of white space
            rawName = Regex.Replace(rawName, @"\W", "");
            //and append a character if first char is digit
            if ((rawName.Length == 0) ||
                (Regex.IsMatch(rawName.Substring(0, 1), @"\d")))
                sb.Append("textToPrependIF");
            sb.Append(rawName);
            return sb.ToString();
        }

        /// <summary>
        /// transform an XML CCDA (Consolidated Clinical Document Architecture)
        /// document to an html page using XSL transform
        /// </summary>
        /// <param name="xmlFile">the xml file contents</param>
        /// <param name="xslFile">The XSL transform file contents</param>
        /// <returns></returns>
        public static string TransformXMLtoHTML(string xmlFile, string xslFile)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlFile);
            XmlDocument xslFileobj = new XmlDocument();
            xslFileobj.LoadXml(xslFile);
            XslCompiledTransform xslDocument = new XslCompiledTransform();
            xslDocument.Load(xslFileobj);
            using (StringWriter stringWriter = new StringWriter())
            {
                string source = doc.InnerXml;
                using (StringReader xml = new StringReader(source))
                {
                    using (XmlReader reader = XmlReader.Create(xml))
                    {
                        XmlWriter xmlWriter = new XmlTextWriter(stringWriter);
                        xslDocument.Transform(reader, xmlWriter);
                    }
                }
                return stringWriter.ToString();
            }//using stringwriter
        }

    } //class

    /// <summary>
    /// object for reading XmlReader streams relative
    /// to a specified place holder element
    /// </summary>
    public class XmlPlaceHolder
    {
        private XmlReader _reader;
        private string _placeElementName;
        private int _placeElementDepth;
        private bool _isAtEndOfTheElement;


        /// <summary>
        /// construct XmlPlaceHolder on the XmlElement the 
        /// given reader is currently positioned on.  Throws 
        /// error if not on an XmlElement node
        /// </summary>
        /// <param name="reader"></param>
        public XmlPlaceHolder(XmlReader reader)
        {
            if (reader.NodeType == XmlNodeType.Element)
            {
                _reader = reader;
                _placeElementDepth = reader.Depth;
                _placeElementName = reader.Name;
                _isAtEndOfTheElement = reader.IsEmptyElement;
            }
            else
            {
                throw new Exception("Cannot construct XmlPlaceHolder " +
                    "because reader is not positioned on an XmlElement.");
            }
        }

        /// <summary>
        /// move reader to the next child element of the original placeholder node
        /// and return true if child element found or false if got to end 
        /// of the original parent node
        /// WARNING if use use reader.ReadElementString() in subsequent
        /// code you will move to end of element and next ReadToNextChild() will
        /// skip an element!! use reader.ReadString() instead.
        /// </summary>
        /// <returns></returns>
        public bool ReadToNextChild()
        {
            bool foundChild = false; //unless found
            //return false if reader sitting on end of the original element
            if ((_isAtEndOfTheElement) ||
                ((_reader.Name == _placeElementName) &&
                        (_reader.Depth == _placeElementDepth) &&
                        (_reader.NodeType == XmlNodeType.EndElement)))
            {
                foundChild = false;
            }
            else
            {
                while (_reader.Read())
                {
                    //read until get to end element of parent or til we find a child element
                    if ((_reader.Name == _placeElementName) &&
                        (_reader.Depth == _placeElementDepth) &&
                        (_reader.NodeType == XmlNodeType.EndElement))
                    {
                        foundChild = false;
                        _isAtEndOfTheElement = true;
                        break;
                    }
                    else if ((_reader.Depth == _placeElementDepth + 1) &&
                        (_reader.NodeType == XmlNodeType.Element))
                    {
                        foundChild = true;
                        break;
                    }
                }
            }
            return foundChild;
        }

        /// <summary>
        /// read to end of the PlaceHolder Element.  Note: unpredictible
        /// behavior if this is called when reader had read beyond
        /// end of the element by methods outside of XmlPlaceHolder.
        /// </summary>
        public void SkipToEndOfElement()
        {
            //do nothing if already at the end
            if ((_isAtEndOfTheElement) ||
                ((_reader.Name == _placeElementName) &&
                        (_reader.Depth == _placeElementDepth) &&
                        (_reader.NodeType == XmlNodeType.EndElement)))
            {
                while (_reader.Read())
                {
                    if ((_reader.Name == _placeElementName) &&
                        (_reader.Depth == _placeElementDepth) &&
                        (_reader.NodeType == XmlNodeType.EndElement))
                    {
                        _isAtEndOfTheElement = true;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// safely skip over the Element the reader is positioned on, moving
        /// to the end of the element;  does nothing if 
        /// current node isn't an element node;  
        /// </summary>
        /// <param name="reader"></param>
        public static void SkipElement(XmlReader reader)
        {
            string name;
            if ((reader != null) && (!reader.EOF))
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (!reader.IsEmptyElement) //do nothing if is empty
                    {
                        //read through to end of element
                        name = reader.Name;
                        int depth = reader.Depth;
                        while (reader.Read())
                        {
                            //if we get to end element
                            if ((reader.Depth == depth) &&
                                (reader.NodeType == XmlNodeType.EndElement) &&
                                    (reader.Name == name))
                            {
                                break; //from reader read
                            }
                        }//from while reader read
                    }//if not empty element
                }//from if is element
            }//from if not eof
        }

        
    } //class
}
