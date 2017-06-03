using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Wve
{
    /// <summary>
    /// outputs errors
    /// </summary>
    public static class MyEr
    {
        /// <summary>
        /// size error log can reach before being truncated
        /// </summary>
        public static int ErrLogFileSize = 300000; // was 32765; //32767 capacity of text box

        /// <summary>
        /// names for subfolders tree inside of common data area to store settings,
        /// e.g. CompanyName, ProgramName for CompanyName\ProgramName\ structure
        /// </summary>
        public static string[] SubfolderNames = new string[0];

        private static string basePath = null;
        /// <summary>
        /// base path to append Subfolder names and file name to, 
        /// (defaults to local application data folder if not changed)
        /// </summary>
        public static string BasePath
        {
            get
            {
                if ((basePath == null) ||
                    (basePath.Trim() == string.Empty))
                {
                    basePath = System.Environment.GetFolderPath(
                        Environment.SpecialFolder.LocalApplicationData);
                }
                return basePath;
            }
            set
            {
                basePath = value;
            }
        }

        /// <summary>
        /// Name of error log file
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
                sb.Append(System.Reflection.Assembly.GetEntryAssembly().GetName().Name);
                sb.Append(".ErrLog.txt");
                return sb.ToString();
            }
        }

        /// <summary>
        /// Show error
        /// </summary>
        /// <param name="sender">object generating call or null</param>
        /// <param name="er"></param>
        /// <param name="logError">logs to file ErrorLog.txt if true</param>
        public static void Show(object sender,
            Exception er,
            bool logError)
        {
            string message; //to log
            //find name of application:
            StringBuilder sb = new StringBuilder(Application.ProductName);
            sb.Append(" encountered an error:");
            DisplayErrorForm dlg = new DisplayErrorForm();
            dlg.Text = sb.ToString();
            //make message string
            sb = new StringBuilder();
            //;sb.Append(er.ToString());
            //;sb.Append(Environment.NewLine);
            sb.Append("Sorry, programming error in ");
            sb.Append(sender);
            dlg.TextBoxMessage.Text = sb.ToString();
            message = sb.ToString();
            dlg.TextBoxDetails.Text = er.ToString();
            dlg.ShowDialog();
            //log to disk
            sb = new StringBuilder();
          
            if (logError)
            {
                logErrToFile(sender, message, er);
            }
        }

        /// <summary>
        /// this overloaded version includes message string
        /// </summary>
        /// <param name="sender">object generating call or null</param>
        /// <param name="message"></param>
        /// <param name="er"></param>
        /// <param name="logError">logs to file ErrorLog.txt if true</param>
        public static void Show(object sender,
            string message,
            Exception er,
            bool logError)
        {
            StringBuilder sb;
            //find name of application:
            sb = new StringBuilder(Application.ProductName);
            sb.Append(" encountered an error:");
            DisplayErrorForm dlg = new DisplayErrorForm();
            dlg.Text = sb.ToString();
            dlg.TextBoxMessage.Text = message;
            //make details string
            sb = new StringBuilder();
            sb.Append(er.ToString());
            sb.Append(Environment.NewLine);
            sb.Append("Source: ");
            sb.Append(sender);
            dlg.TextBoxDetails.Text = sb.ToString();
            dlg.ShowDialog();
            //log to disk
            if (logError)
            {
                logErrToFile(sender, message, er);
            }
        }

        /// <summary>
        /// log error to file then return without notifying user
        /// </summary>
        /// <param name="sender">object generating the call or null </param>
        /// <param name="message">description</param>
        /// <param name="er"></param>
        public static void LogErrorQuietly(object sender,
            string message,
            Exception er)
        {
            //log to disk
            logErrToFile(sender, message, er);
        }

        /// <summary>
        /// log error to file named
        /// But do not raise error if fails.
        /// Returns true if thinks it succeeded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        /// <param name="er"></param>
        private static bool logErrToFile(
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
                sb.Append("|");
                sb.Append(Environment.NewLine);
                sb.Append("Err on ");
                sb.Append(DateTime.Now.ToString());
                sb.Append(Environment.NewLine);
                sb.Append(message);
                sb.Append(Environment.NewLine);
                if (er != null)
                {
                    sb.Append("Details:  ");
                    sb.Append(er.ToString());
                    sb.Append(Environment.NewLine);
                }
                if (sender != null)
                {
                    sb.Append("Source:  ");
                    sb.Append(sender.ToString());
                    sb.Append(Environment.NewLine);
                }
                sb.Append(Environment.NewLine);

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
                (fi.Length > ErrLogFileSize))
                {
                    FileInfo fiNew = new FileInfo(FilenameWithPath + ".new");
                    excessLength = (int)fi.Length - ErrLogFileSize;
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
                MessageBox.Show(localEr.ToString());
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
            //start with parent folder path, and create it if not there
            if(!Directory.Exists(parentFolderPath))
            {
                Directory.CreateDirectory(parentFolderPath);
            }
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
        /// read the whole log file or return empty string
        /// if log file empty or not existing
        /// </summary>
        /// <returns></returns>
        public static string ReadLogFile()
        {
            if (File.Exists(FilenameWithPath))
            {
                using (StreamReader sr = File.OpenText(FilenameWithPath))
                {
                    return sr.ReadToEnd();
                }
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
