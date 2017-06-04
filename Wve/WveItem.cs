using System;
using System.Collections.Generic;
using System.Text;

namespace Wve
{

        /// <summary>
        /// Generic item containing text, 3 tags (strings) and one object
        /// </summary>
    public class WveItem
    {
        string text;
        string tag;
        string tag2;
        string tag3;
        object wObject;

        //first of four constructor overloads:
        //one text, one tag
        /// <summary>
        /// first of four constructor overloads:  one text, one tag
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="Tag"></param>
        public WveItem(string Text, string Tag)
        {
            text = Text;
            tag = Tag;
            tag2 = null;
            tag3 = null;
            wObject = null;
        }

        /// <summary>
        /// second of four contstructor overloads: one text, two tags
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="Tag"></param>
        /// <param name="Tag2"></param>
        public WveItem(string Text, string Tag, string Tag2)
        {
            text = Text;
            tag = Tag;
            tag2 = Tag2;
            tag3 = null;
            wObject = null;
        }

        /// <summary>
        /// third of four constructor overloads: one text, three tags
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="Tag"></param>
        /// <param name="Tag2"></param>
        /// <param name="Tag3"></param>
        public WveItem(string Text, string Tag, string Tag2, string Tag3)
        {
            text = Text;
            tag = Tag;
            tag2 = Tag2;
            tag3 = Tag3;
            wObject = null;
        }

        /// <summary>
        /// fourth of four constructor overloads: one text, one object
        /// </summary>
        /// <param name="Text"></param>
        /// <param name="WObject"></param>
        public WveItem(string Text, object WObject)
        {
            text = Text;
            tag = null;
            tag2 = null;
            tag3 = null;
            wObject = WObject;
        }

        /// <summary>
        /// text
        /// </summary>
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
            }
        }

        /// <summary>
        /// first string tag
        /// </summary>
        public string Tag
        {
            get { return tag; }
            set { tag = value; }
        }

        /// <summary>
        /// second string tag
        /// </summary>
        public string Tag2
        {
            get { return tag2; }
            set { tag2 = value; }
        }

        /// <summary>
        /// third string tag
        /// </summary>
        public string Tag3
        {
            get { return tag3; }
            set { tag3 = value; }
        }

        /// <summary>
        /// an object tag
        /// </summary>
        public object WObject
        {
            get { return wObject; }
            set { wObject = WObject; }
        }

        /// <summary>
        /// returns text property
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return text;
        }
    }
    
}
