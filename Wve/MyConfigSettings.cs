using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace Wve
{
    /// <summary>
    /// Configuration Settings utility
    /// </summary>
    public class MyConfigSettings
    {
        /// <summary>
        /// the name of the config file; typically the assembly name, 
        /// without the .myconfig extension that will be added
        /// </summary>
        private string configFileNameBase = null;
        /// <summary>
        /// the beginning location, to which subfolderNames are appended to 
        /// make the location of the config file; normally is local application data
        /// </summary>
        private string pathBase = null;
        /// <summary>
        /// names for subfolders tree inside of common data area to store settings,
        /// e.g. CompanyName, ProgramName for CompanyName\ProgramName\ structure
        /// </summary>
        private string[] subfolderNames = new string[0];
        /// <summary>
        /// constructs new config settings utility
        /// </summary>
        public MyConfigSettings()
        {
            //plain constructor
            this.pathBase = System.Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData);
        }
        /// <summary>
        /// puts config settings in same folder as executing assembly
        /// </summary>
        /// <param name="useAssemblyWorkingFolder"></param>
        public MyConfigSettings(bool useAssemblyWorkingFolder)
        {
            this.pathBase = Directory.GetCurrentDirectory();
        }

        /// <summary>
        /// Configuration settings utility
        /// </summary>
        /// <param name="subfolderNames">array of names of successive childfolders 
        /// to put under the common data folder to locate the settings</param>
        public MyConfigSettings(string[] subfolderNames)
        {
            this.subfolderNames = subfolderNames;
            this.pathBase = System.Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData);
        }

        /// <summary>
        /// Configuration setting utility
        /// </summary>
        /// <param name="subfolderNames">array of names of successive childfolders 
        /// to put under the common data folder to locate the settings</param>
        /// <param name="configFileNameBase">base name to which the .myconfig extension
        /// will be added;  commonly is the assembly name</param>
        public MyConfigSettings(string[] subfolderNames, string configFileNameBase)
        {
            this.pathBase = System.Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData);
            this.subfolderNames = subfolderNames;
            this.configFileNameBase = configFileNameBase;
        }

        /// <summary>
        /// Configuration setting utility
        /// </summary>
        /// <param name="subfolderNames">array of names of successive childfolders 
        /// to put under the common data folder to locate the settings</param>
        /// <param name="configFileNameBase">base name to which the .myconfig extension
        /// will be added;  commonly is the assembly name</param>
        /// <param name="pathBase">base location to which subfolderNames are  appended
        /// to make location for config file, e.g. 'c:'
        /// WARNING!! normally you should use another constructor without specifying 
        /// the pathBase so it can default to Local Application Data folder.  Also
        /// the folder needs to already exist.</param>
        public MyConfigSettings(string[] subfolderNames, string configFileNameBase,
            string pathBase)
        {
            this.pathBase = pathBase;
            this.subfolderNames = subfolderNames;
            this.configFileNameBase = configFileNameBase;
        }

        /// <summary>
        /// gets name if assigned, or defaults to (assemblyname).myconfig, 
        /// and optionally makes a config file if none exists
        /// </summary>
        /// <param name="MakeFileIfNone">create a config file if none exists</param>
        /// <returns></returns>
        public string GetMyConfigFileNameWithPath(bool MakeFileIfNone)
        {
            string configFileNameWithPath;
            //  put it in the common program data area 
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //;sb.Append(System.Windows.Forms.Application.ExecutablePath); -->no, Vista won't let us write there
            //;sb.Append(System.Windows.Forms.Application.CommonAppDataPath);-->no, requres new file every build
            sb.Append(pathBase);
            sb.Append(System.IO.Path.DirectorySeparatorChar);
            //(add subfolders as requested)
            if (subfolderNames.Length > 0)
            {
                //if can see or create the desired path...
                if(createSubFolders(
                    pathBase,
                    subfolderNames))
                {
                    //then append that path
                    foreach (string subfolderName in subfolderNames)
                    {
                        sb.Append(subfolderName);
                        sb.Append(System.IO.Path.DirectorySeparatorChar);
                    }
                }
              }
            //default to assemblyname.myconfig...
            if (configFileNameBase == null)
            {
                configFileNameBase = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
            }
            sb.Append(configFileNameBase);
            sb.Append(".myconfig");
            configFileNameWithPath = sb.ToString();

            //make new file if none exists.
            try
            {
                if ((!(File.Exists(configFileNameWithPath))) &&
                    MakeFileIfNone)
                {
                    XmlDocument xDoc = new XmlDocument();
                    XmlDeclaration decl = xDoc.CreateXmlDeclaration("1.0", "utf-8", "");
                    xDoc.AppendChild(decl);
                    XmlElement docElem = xDoc.CreateElement("configuration");
                    XmlElement firstChild = xDoc.CreateElement("appSettings");
                    docElem.AppendChild(firstChild);
                    xDoc.AppendChild(docElem);
                    //save it
                    xDoc.Save(configFileNameWithPath);
                }
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Warning, didn't make config file");
            }
            return configFileNameWithPath;
        }

        /// <summary>
        /// create set of subfolders in parent folder and return true if 
        /// the parentFolderPath contains the succession of requested
        /// child folderNames at the end of the operation.  (false thus
        /// represents an error state).
        /// </summary>
        /// <param name="parentFolderPath">the path of parent folder, without
        /// a trailing directory separator character</param>
        /// <param name="folderNames"></param>
        /// <returns></returns>
        private bool createSubFolders(string parentFolderPath, string[] folderNames)
        {
            bool pathExists = false;// unless we determine it is true
            DirectoryInfo diParent = new DirectoryInfo(parentFolderPath);
            bool thisFolderMatches;
            DirectoryInfo diNewChild = null;

            foreach (string folderName in folderNames)
            {
                thisFolderMatches = false;// reset 
                foreach (DirectoryInfo subFolder in diParent.GetDirectories())
                {
                    if (subFolder.Name.ToLower() == folderName.ToLower())
                    {
                        //found match
                        thisFolderMatches = true;
                        //make this folder the parent and look for next child
                        diParent = subFolder;
                        break; //from for each sub folder; get next name
                    }
                }//from for each sub folder in diParent
                if (!thisFolderMatches)
                {
                    //didn't find match so we need to make this folder
                    diNewChild = diParent.CreateSubdirectory(folderName);
                    //and now make it the new parent and look for next child
                    diParent = diNewChild;
                }
            }//from for each folder name in parameters
            //if we got here without errors then the requested directory tree now exists
            pathExists = true;
            return pathExists;
        }

        /// <summary>
        /// write value as XmlElement for given config setting
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="value"></param>
        public void SetValueAsXml(string keyName, XmlElement value)
        {
            // Get the location of the config file, making one if none exists
            string configFile = GetMyConfigFileNameWithPath(true);

            // Load the settings xml document. 
            XmlDocument doc = new XmlDocument();
            doc.Load(configFile);

            XmlNodeList list = doc.SelectNodes(
                "configuration/appSettings/add[@key='" + keyName + "']");

            XmlNode node;
            if (list.Count == 0)
            {
                // Create the a new node. 
                node = doc.CreateElement("add", "");

                // Set the attributes of the node. 
                XmlAttribute attribute;
                attribute = doc.CreateAttribute("key");
                attribute.Value = keyName;
                node.Attributes.Append(attribute);

                // indicate that this is an xml value
                attribute = doc.CreateAttribute("value");
                attribute.Value = "xml";
                node.Attributes.Append(attribute);

                // add the value as an xml (element) node
                node.AppendChild(doc.ImportNode(value, true));

                // Append the new node to the appsettings section. 
                //;doc.SelectNodes("configuration/appSettings").Item(0).AppendChild(node); 
                XmlNodeList xnl = doc.SelectNodes("configuration/appSettings");
                XmlElement xe = (XmlElement)xnl.Item(0);
                xe.AppendChild(node);
            }
            else
            {
                // Just get the first node and update it. 
                node = list.Item(0);

                // Update the value of the node. 
                foreach (XmlNode oldChild in node.ChildNodes)
                {
                    node.RemoveChild(oldChild);
                }
                //indicate this is xml value
                node.Attributes.GetNamedItem("value").Value = "xml";
                node.AppendChild(doc.ImportNode(value, true));
            }

            // save the modified xml document. 
            doc.Save(configFile);
        }

        /// <summary>
        /// write value for that config setting (making file if needed)
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="value"></param>
        public void SetValue(string keyName, string value)
        {
            // Get the location of the config file, making one if none exists
            string configFile = GetMyConfigFileNameWithPath(true);

            // Load the settings xml document. 
            XmlDocument doc = new XmlDocument();
            doc.Load(configFile);

            XmlNodeList list = doc.SelectNodes(
                "configuration/appSettings/add[@key='" + keyName + "']");

            XmlNode node;
            if (list.Count == 0)
            {
                // Create the a new node. 
                node = doc.CreateElement("add", "");

                // Set the attributes of the node. 
                XmlAttribute attribute;
                attribute = doc.CreateAttribute("key");
                attribute.Value = keyName;
                node.Attributes.Append(attribute);

                attribute = doc.CreateAttribute("value");
                attribute.Value = value;
                node.Attributes.Append(attribute);

                // Append the new node to the appsettings section. 
                //;doc.SelectNodes("configuration/appSettings").Item(0).AppendChild(node); 
                XmlNodeList xnl = doc.SelectNodes("configuration/appSettings");
                XmlElement xe = (XmlElement)xnl.Item(0);
                xe.AppendChild(node);
            }
            else
            {
                // Just get the first node and update it. 
                node = list.Item(0);

                // Update the value of the node. 
                node.Attributes.GetNamedItem("value").Value = value;
            }

            // save the modified xml document. 
            doc.Save(configFile);
        }



        /// <summary>
        /// returns nodes in Config file under configuration/appSettings
        /// </summary>
        /// <returns></returns>
        public XmlNodeList GetMyConfigList()
        {
            // Get the location of the config file, making one if none exists
            string configFile = GetMyConfigFileNameWithPath(true);

            // Load the settings xml document. 
            XmlDocument doc = new XmlDocument();
            doc.Load(configFile);

            XmlNodeList list = doc.SelectNodes("configuration/appSettings/add");
            return list;
        }


        /// <summary>
        /// delete key with given name, returns true if found and removed
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public bool DeleteKey(string keyName)
        {
            // Get the location of the config file, making one if none exists
            string configFile = GetMyConfigFileNameWithPath(true);

            // Load the settings xml document. 
            XmlDocument doc = new XmlDocument();
            doc.Load(configFile);

            XmlNodeList list = doc.SelectNodes("configuration/appSettings/add[@key='" + keyName + "']");
            XmlNode node;
            if (list.Count == 0)
            {
                // nothing to do
                return false;
            }
            else
            {
                // delete them
                for (int i = 0; i < list.Count; i++)
                {
                    node = list.Item(i);
                    // Delete the node
                    node.ParentNode.RemoveChild(node);
                }
            }

            // save the modified xml document. 
            doc.Save(configFile);
            return true;
        }

        /// <summary>
        /// retrieves value of named config setting as an XmlElement node,
        /// or null if not found or error
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public XmlElement GetValueAsXml(string keyName)
        {
            XmlElement result = null; //unless successfully found
            // Get the location of the config file, making one if none exists
            string configFile = GetMyConfigFileNameWithPath(true);

            // Load the settings xml document. 
            XmlDocument doc = new XmlDocument();
            doc.Load(configFile);

            XmlNodeList list = doc.SelectNodes("configuration/appSettings/add[@key='" + keyName + "']");
            XmlNode node;
            if (list.Count != 0)
            {
                // Just get the first node and update it. 
                node = list.Item(0);
                foreach (XmlNode child in node.ChildNodes)
                {
                    if (child.NodeType == XmlNodeType.Element)
                    {
                        result = (XmlElement)child;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// return value for key or null if none
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        public string GetValue(string keyName)
        {

            // Get the location of the config file, making one if none exists
            string configFile = GetMyConfigFileNameWithPath(true);
            string result = null; //unless found below

            // Load the settings xml document. 
            XmlDocument doc = new XmlDocument();
            doc.Load(configFile);

            XmlNodeList list = doc.SelectNodes("configuration/appSettings/add[@key='" + keyName + "']");
            XmlNode node;
            if (list.Count != 0)
            {
                // Just get the first node and update it. 
                node = list.Item(0);
                result = node.Attributes.GetNamedItem("value").Value;
            }
            return result;
        }
    }


}
