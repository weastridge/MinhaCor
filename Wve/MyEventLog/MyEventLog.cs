using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Wve
{
    /// <summary>
    /// outputs errors
    /// </summary>
    public static class MyEventLog
    {
        /// <summary>
        /// size  log can reach before being truncated
        /// </summary>
        public static int EventLogFileSize = 32765; //32767 capacity of text box

        private static string _eventLogFilenameBase = string.Empty;
        /// <summary>
        /// get or set name to which .myeventlog.txt will be added
        /// </summary>
        public static string EventLogFilenameBase
        {
            get
            {
                if ((_eventLogFilenameBase == null) ||
                (_eventLogFilenameBase.Trim() == string.Empty))
                {
                    _eventLogFilenameBase =
                        System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
                }
                return _eventLogFilenameBase;
            }
            set
            {
                _eventLogFilenameBase = value;
            }
        }

        private static string _basePath = null;
        /// <summary>
        /// base path to append Subfolder names and file name to, 
        /// (defaults to local application data folder if not changed)
        /// </summary>
        public static string BasePath
        {
            get
            {
                if ((_basePath == null) ||
                    (_basePath.Trim() == string.Empty))
                {
                    _basePath = System.Environment.GetFolderPath(
                        Environment.SpecialFolder.LocalApplicationData);
                }
                return _basePath;
            }
            set
            {
                _basePath = value;
            }
        }

        /// <summary>
        /// names for subfolders tree inside of common data area to store settings,
        /// e.g. CompanyName, ProgramName for CompanyName\ProgramName\ structure
        /// </summary>
        public static string[] SubfolderNames = new string[0];

        /// <summary>
        /// Name of event log file
        /// </summary>
        public static string FilenameWithPath
        {
            get
            {
                //get name for file
                StringBuilder sb = new StringBuilder();
                //don't use, because makes subfolder for each compilation
                //sb.Append(Application.CommonAppDataPath);

                sb.Append(BasePath);
                sb.Append(System.IO.Path.DirectorySeparatorChar);
                //(add subfolders as requested)
                if (SubfolderNames.Length > 0)
                {
                    //if can see or create the desired path...
                    if (createSubFolders(
                        BasePath,
                        SubfolderNames))
                    {
                        //then append that path
                        foreach (string subfolderName in SubfolderNames)
                        {
                            sb.Append(subfolderName);
                            sb.Append(System.IO.Path.DirectorySeparatorChar);
                        }
                    }
                }//from if subfolder names
                sb.Append(EventLogFilenameBase);
                sb.Append(".EventLog.txt");
                return sb.ToString();
            }
        }

        /// <summary>
        /// log event to file then return without notifying user,
        /// returns true if success; RAISES NO ERRORS!
        /// </summary>
        /// <param name="sender">object generating the call or null </param>
        /// <param name="message">description</param>
        public static bool LogEventQuietly(object sender,
            string message)
        {
            bool success = true;
            try
            {
                //log to disk
                logEventToFile(sender, message, null);
            }
            catch (Exception ex)
            {
                success = false;
            }
            return success;
        }

        /// <summary>
        /// log event to file then return without notifying user
        /// (include Esception, or null if none)
        /// returns true if successful, and RAISES NO ERRORS itself
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        /// <param name="er"></param>
        public static bool LogEventQuietly(object sender,
            string message,
            Exception er)
        {
            bool success = true;
            try
            {
                //log to disk
                logEventToFile(sender, message, er);
            }
            catch (Exception ex)
            {
                success = false;
            }
            return success;
        }

        /// <summary>
        /// log error to file named
        /// But do not raise error if fails.
        /// Returns true if thinks it succeeded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        /// <param name="er">exception or null if none</param>
        private static bool logEventToFile(
            object sender,
            string message,
            Exception er)
        {
            bool success = false; // unless succeeds
            try
            {
                StringBuilder sb;
                //create log entry text
                sb = new StringBuilder();
                sb.Append(">> ");
                sb.Append(DateTime.Now.ToString());
                sb.Append("; Source:  ");
                sb.Append(sender.ToString());
                sb.Append(Environment.NewLine);
                sb.Append(message);
                sb.Append(Environment.NewLine);
                if (er != null)
                {
                    sb.Append("<ERROR>");
                    sb.Append(Environment.NewLine);
                    sb.Append(er.ToString());
                    sb.Append(Environment.NewLine);
                    sb.Append("</ERROR>");
                    sb.Append(Environment.NewLine);
                }

                //append to the file
                //open file, true to append data
                using (StreamWriter sw = new StreamWriter(FilenameWithPath, true))
                {
                    sw.Write(sb.ToString());
                }

                //now truncate if necessary
                // (reread file info)
                FileInfo fi = new FileInfo(FilenameWithPath);
                int excessLength;
                char[] tempBuffer;
                if ((fi.Exists) &&
                (fi.Length > EventLogFileSize))
                {
                    FileInfo fiNew = new FileInfo(FilenameWithPath + ".new");
                    excessLength = (int)fi.Length - EventLogFileSize;
                    //copy file to new file, skipping the excess
                    using (StreamReader sr = new StreamReader(fi.OpenRead()))
                    {
                        //set pointer in old log file
                        tempBuffer = new char[excessLength];
                        sr.Read(tempBuffer, 0, excessLength);
                        //and read the rest of it into new file
                        using (StreamWriter sw = fiNew.AppendText())
                        {
                            while (sr.Peek() != -1)
                            {
                                sw.WriteLine(sr.ReadLine());
                            }
                        }
                    }//from using stream reader 
                    //rename new file to original name
                    fi.Delete();
                    fiNew.MoveTo(FilenameWithPath);
                }//from if file too big
                //if got here must be success
                success = true;
            }//from try
            catch (Exception localEr)
            {
                Wve.MyEr.LogErrorQuietly("logEventToFile()", 
                    "Error writing to event log file.", 
                    localEr);
            }
            return success;
        }//from logErToFile

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
        private static bool createSubFolders(string parentFolderPath, string[] folderNames)
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
        /// read the whole log file
        /// </summary>
        /// <returns></returns>
        public static string ReadLogFile()
        {
            using (StreamReader sr = File.OpenText(FilenameWithPath))
            {
                return sr.ReadToEnd();
            }
        }

    }
}
