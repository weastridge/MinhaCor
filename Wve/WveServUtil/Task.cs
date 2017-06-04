using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;


namespace Wve.WveServUtil
{
    /// <summary>
    /// database server utilities task to be 
    /// done by one (and only one) of three schedules: 
    /// by time interval,
    /// time of day,
    /// or by day of week
    /// </summary>
    public class Task
    {
        /// <summary>
        /// short name to identify the task
        /// </summary>
        public string Name = string.Empty;
        /// <summary>
        /// optional description of task;  
        /// Plain tasks can use 
        /// run="programName" to run a program
        /// </summary>
        public string Comment = string.Empty;
        /// <summary>
        /// Time before which task will not run, or 
        /// DateTime.FromDays(0) to ignore timeToEnable
        /// </summary>
        public TimeSpan TimeToEnable = TimeSpan.FromDays(0);
        /// <summary>
        /// Time of day after which task won't run, or
        /// DateTime.FromDays(1) to ignore TimeToDisable
        /// </summary>
        public TimeSpan TimeToDisable = TimeSpan.FromDays(1);
        /// <summary>
        /// If true task is to be done when interval from 
        /// WhenLastDone to now is greater than 
        /// IntervalToDo 
        /// (and time to enable/disable allows)
        /// </summary>
        public bool DoByInterval = false;
        /// <summary>
        /// When Task was last done, or DateTime.MinValue if not
        /// </summary>
        public DateTime WhenLastDone = DateTime.MinValue;
        /// <summary>
        /// interval from WhenLastDone when task should be done
        /// again (if DoByInterval is true and 
        /// time to enable/disable allows)
        /// </summary>
        public TimeSpan IntervalToDo = TimeSpan.FromHours(0);
        /// <summary>
        /// if true task is to be done daily at TimeToDo 
        /// (if time to enable/disable allows)
        /// </summary>
        public bool DoDaily = false;
        /// <summary>
        /// Time of day task is to be done 
        /// (if DoDaily or DoWeekly is true 
        /// and time to enable/disable allows)
        /// </summary>
        public TimeSpan TimeToDo = TimeSpan.FromDays(0);
        /// <summary>
        /// if true task is to be done weekly on the day
        /// DayToDo and time TimeToDo
        /// </summary>
        public bool DoWeekly = false;
        /// <summary>
        /// Day of week to do task if DoWeekly is true
        /// </summary>
        public DayOfWeek DayToDo = DayOfWeek.Sunday;

        /// <summary>
        /// returns the name of the task
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// text description of when should be done
        /// </summary>
        /// <returns></returns>
        public string ScheduleDescription
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (DoByInterval)
                {
                    sb.Append("Do every ");
                    sb.Append(IntervalToDo.ToString());
                    sb.Append(" HH:MM:SS  ");
                }
                else if (DoDaily)
                {
                    sb.Append("Do daily at ");
                    sb.Append(TimeToDo.ToString());
                    sb.Append(".  ");
                }
                else if (DoWeekly)
                {
                    sb.Append("Do weekly on ");
                    sb.Append(DayToDo.ToString());
                    sb.Append(" after ");
                    sb.Append(TimeToDo.ToString());
                }
                else
                {
                    //if got here neither of the three schedules are 
                    // enabled
                    return "Not scheduled.";
                }

                //for all schedules
                if (TimeToEnable != TimeSpan.FromDays(0))
                {
                    sb.Append(" Only if time is after ");
                    sb.Append(TimeToEnable.ToString());
                    sb.Append(".  ");
                }
                if (TimeToDisable != TimeSpan.FromDays(1))
                {
                    sb.Append(" Only if time is before ");
                    sb.Append(TimeToDisable.ToString());
                    sb.Append(".  ");
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// serialize members of this base class to xml format
        /// </summary>
        /// <returns></returns>
        public virtual XmlElement ToXml()
        {
            XmlDocument xDoc = new XmlDocument();
            XmlElement docElm = xDoc.CreateElement("Task");
            xDoc.AppendChild(docElm);
            appendNode(docElm, "Name", Name);
            appendNode(docElm, "Comment", Comment);
            appendNode(docElm, "TimeToEnable", TimeToEnable.ToString());
            appendNode(docElm, "TimeToDisable", TimeToDisable.ToString());
            appendNode(docElm, "DoByInterval", DoByInterval.ToString());
            //don't save WhenLastDone
            appendNode(docElm, "IntervalToDo", IntervalToDo.ToString());
            appendNode(docElm, "DoDaily", DoDaily.ToString());
            appendNode(docElm, "TimeToDo", TimeToDo.ToString());
            appendNode(docElm, "DoWeekly", DoWeekly.ToString());
            appendNode(docElm, "DayToDo", DayToDo.ToString());
            return xDoc.DocumentElement;
        }
        /// <summary>
        /// creates an XmlElement with name and innertext given
        /// and appends it to the parent
        /// WARNING: nodeName must be a legal XML element name
        /// with no spaces or punctuation
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="nodeName"></param>
        /// <param name="nodeValue"></param>
        protected static void appendNode(XmlElement parentNode,
            string nodeName,
            string nodeValue)
        {
            XmlElement xeChild = 
                parentNode.OwnerDocument.CreateElement(nodeName);
            xeChild.InnerText = nodeValue;
            parentNode.AppendChild(xeChild);
        }

        /// <summary>
        /// read task from the XmlDocument 
        /// </summary>
        /// <returns></returns>
        public static Task FromXml(XmlElement elm)
        {
            Task result = null; //unless created below
            //determine type
            if (elm != null)
            {
                //give alias
                XmlDocument xDoc = elm.OwnerDocument;
                //get derived class members first
                switch(elm.GetAttribute("Type"))
                {
                    case "SQLCmdTask":
                        result = new SQLCmdTask();
                        if (elm.GetElementsByTagName("CommandText").Count > 0)
                        {
                            ((SQLCmdTask)result).CommandText =
                               elm.GetElementsByTagName("CommandText")[0].InnerText;
                        }
                        if (elm.GetElementsByTagName("TypeOfCommand").Count > 0)
                        {
                            string typeofCommand =
                                elm.GetElementsByTagName("TypeOfCommand")[0].InnerText;
                            if (typeofCommand == "Text")
                                ((SQLCmdTask)result).TypeOfCommand =
                                    System.Data.CommandType.Text;
                            else if (typeofCommand == "StoredProcedure")
                                ((SQLCmdTask)result).TypeOfCommand =
                                    System.Data.CommandType.StoredProcedure;
                        }
                        break;
                    case "FileXferTask":
                        result = new FileXferTask();
                        if (elm.GetElementsByTagName("SourcePathAndName").Count > 0)
                        {
                            ((FileXferTask)result).SourcePathAndName =
                                elm.GetElementsByTagName(
                                "SourcePathAndName")[0].InnerText;
                        }
                        if (elm.GetElementsByTagName("DestinationPathAndName").Count > 0)
                        {
                            ((FileXferTask)result).DestinationPathAndName =
                                elm.GetElementsByTagName(
                                "DestinationPathAndName")[0].InnerText;
                        }
                        break;
                    default:
                        result = new Task();
                        break;
                }//from switch
                //get base members now
                if (elm.GetElementsByTagName("Name").Count > 0)
                {
                    result.Name = elm.GetElementsByTagName("Name")[0].InnerText;
                }
                if (elm.GetElementsByTagName("Comment").Count > 0)
                {
                    result.Comment = elm.GetElementsByTagName("Comment")[0].InnerText;
                }
                if (elm.GetElementsByTagName("TimeToEnable").Count > 0)
                {
                    TimeSpan.TryParse(elm.GetElementsByTagName(
                        "TimeToEnable")[0].InnerText,
                        out result.TimeToEnable);
                }
                if (elm.GetElementsByTagName("TimeToDisable").Count > 0)
                {
                    TimeSpan.TryParse(elm.GetElementsByTagName(
                        "TimeToDisable")[0].InnerText,
                        out result.TimeToDisable);
                }
                if (elm.GetElementsByTagName("DoByInterval").Count > 0)
                {
                    bool.TryParse(elm.GetElementsByTagName(
                        "DoByInterval")[0].InnerText,
                        out result.DoByInterval);
                }
                if (elm.GetElementsByTagName("IntervalToDo").Count > 0)
                {
                    TimeSpan.TryParse(elm.GetElementsByTagName(
                        "IntervalToDo")[0].InnerText,
                        out result.IntervalToDo);
                }
                if (elm.GetElementsByTagName("DoDaily").Count > 0)
                {
                    bool.TryParse(elm.GetElementsByTagName(
                        "DoDaily")[0].InnerText,
                        out result.DoDaily);
                }
                if (elm.GetElementsByTagName("TimeToDo").Count > 0)
                {
                    TimeSpan.TryParse(elm.GetElementsByTagName(
                        "TimeToDo")[0].InnerText,
                        out result.TimeToDo);
                }
                if (elm.GetElementsByTagName("DoWeekly").Count > 0)
                {
                    bool.TryParse(elm.GetElementsByTagName(
                        "DoWeekly")[0].InnerText,
                        out result.DoWeekly);
                }
                if (elm.GetElementsByTagName("DayToDo").Count > 0)
                {
                    try
                    {
                        result.DayToDo = (DayOfWeek) Enum.Parse(typeof(DayOfWeek),
                            elm.GetElementsByTagName("DayToDo")[0].InnerText,
                            true); //true to ignore case
                    }
                    catch
                    {
                        //couldn't read DayToDo so leave the default.
                    }
                }
            }//from if legitimate xDoc with document element and type attr

            return result;
        }

        /// <summary>
        /// looks at only one of three schedule types and 
        /// returns true if task is due at the datetime 'now', 
        /// according to that schedule.
        /// Looks for DoByInterval first, then DoDaily and DoWeekly.
        /// Will always return false if now is before TimeToEnable 
        /// or after TimeToDisable.
        /// </summary>
        /// <param name="now">The time we are asking if task is due for,
        /// which typically should be DateTime.Now</param>
        /// <param name="resetWhenLastDone">if true calling this method
        /// will reset WhenLastDone to now, 
        /// which affects future calls to this 
        /// method.  
        /// If false, just peeks without changing it</param>
        /// <returns></returns>
        public bool IsDueNow(DateTime now, bool resetWhenLastDone)
        {
            bool result = false;
            if ((now.TimeOfDay < TimeToEnable) ||
                (now.TimeOfDay > TimeToDisable))
            {
                return false;
            }

            if (DoByInterval)
            {
                if((now - WhenLastDone) > IntervalToDo)
                {
                    if(resetWhenLastDone)
                    {
                        WhenLastDone = now;
                    }
                    return true;
                }
            }
            else if (DoDaily)
            {
                //return true if now is within an hour of due time
                // and if it hasn't been done yet today
                if ((now.TimeOfDay > TimeToDo) &&
                    (now.TimeOfDay - TimeToDo < TimeSpan.FromMinutes(60)) &&
                    (WhenLastDone.Date < now.Date))
                {
                    if (resetWhenLastDone)
                    {
                        WhenLastDone = now;
                    }
                    return true;
                }
            }
            else if (DoWeekly)
            {
                //return true if is this day of week
                // and is any time after due time
                // and hasn't already been done today
                if ((now.DayOfWeek == DayToDo) &&
                    (now.TimeOfDay > TimeToDo) &&
                    (WhenLastDone.Date < now.Date))
                {
                    if (resetWhenLastDone)
                    {
                        WhenLastDone = now;
                    }
                    return true;
                }
            }
            //if got here result is still false
            return result;
        }

        /// <summary>
        /// provides a deep (same as shallow) clone of this object
        /// </summary>
        /// <returns></returns>
        public virtual Task Clone()
        {
            return (Task)this.MemberwiseClone();
        }
    }

    /// <summary>
    /// a databse task consisting of a T-SQL Command to issue
    /// </summary>
    public class SQLCmdTask : Task
    {
        /// <summary>
        /// the T-SQL command text to execute
        /// </summary>
        public string CommandText = string.Empty;
        /// <summary>
        /// What CommandText is, e.g. t-SQL Text or Stored Procedure name
        /// </summary>
        public System.Data.CommandType TypeOfCommand = System.Data.CommandType.Text;
        /// <summary>
        /// serialize the task to xml format
        /// </summary>
        /// <returns></returns>
        public override System.Xml.XmlElement ToXml()
        {
            XmlDocument xDoc = (base.ToXml()).OwnerDocument;
            XmlAttribute typeAttribute = xDoc.CreateAttribute("Type");
            typeAttribute.Value = "SQLCmdTask";
            xDoc.DocumentElement.Attributes.Append(typeAttribute);
            appendNode(xDoc.DocumentElement, "CommandText", CommandText);
            appendNode(xDoc.DocumentElement, "TypeOfCommand", TypeOfCommand.ToString());
            return xDoc.DocumentElement;
        }

        /// <summary>
        /// provides deep (same as shallow) clone of this object
        /// </summary>
        /// <returns></returns>
        public override Task Clone()
        {
            return (SQLCmdTask)this.MemberwiseClone();
        }
    }

    /// <summary>
    /// a task consisting of a source and destination to copy a
    /// file from and to.
    /// </summary>
    public class FileXferTask : Task
    {
        /// <summary>
        /// filename including path of source to copy from
        /// </summary>
        public string SourcePathAndName = string.Empty;
        /// <summary>
        /// filename including path of destination to copy to
        /// </summary>
        public string DestinationPathAndName = string.Empty;
        /// <summary>
        /// serialize the task to xml format
        /// </summary>
        /// <returns></returns>
        public override System.Xml.XmlElement ToXml()
        {
            XmlDocument xDoc = (base.ToXml()).OwnerDocument;
            XmlAttribute typeAttribute = xDoc.CreateAttribute("Type");
            typeAttribute.Value = "FileXferTask";
            xDoc.DocumentElement.Attributes.Append(typeAttribute);
            appendNode(xDoc.DocumentElement, 
                "SourcePathAndName", SourcePathAndName);
            appendNode(xDoc.DocumentElement, 
                "DestinationPathAndName", DestinationPathAndName);
            return xDoc.DocumentElement;
        }

        /// <summary>
        /// provides deep (same as shallow) clone of this object
        /// </summary>
        /// <returns></returns>
        public override Task Clone()
        {
            return (FileXferTask)this.MemberwiseClone();
        }
    }
}
