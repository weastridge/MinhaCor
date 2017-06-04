using System;
using System.Xml;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Binary; //for DeepCloneObject()
using System.Runtime.Serialization;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;

namespace Wve
{

	

    /// <summary>
    /// simple object to save current cursor, show WaitCursor,
    /// and restore prior cursor when disposed. 
    /// By Francesco Balena 
    /// </summary>
    public class HourglassCursor : IDisposable
    {
        //published in Practical Guidelines and Best Practices
        //Microsoft Press 2005
        private Cursor savedCursor;
        /// <summary>
        /// simple object to save current cursor, show WaitCursor,
        /// and restore prior cursor when disposed. 
        /// adapted from Francesco Balena 
        /// </summary>
        public HourglassCursor()
        {
            savedCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
        }
        /// <summary>
        /// resets cursor to prior value
        /// </summary>
        public void Dispose()
        {
            Cursor.Current = savedCursor;
        }
    }

    /// <summary>
    /// object to box a boolean value that
    /// can be referenced by different pieces of code
    /// for use with BoolCache
    /// </summary>
    public class BoolBoxed
    {
        /// <summary>
        /// true or false
        /// </summary>
        public bool Value;
        /// <summary>
        /// an object just holding a boolian value
        /// so it can be passed as object
        /// </summary>
        /// <param name="value"></param>
        public BoolBoxed(bool value)
        {
            Value = value;
        }
    }

    /// <summary>
    /// Sets boolean value 
    /// (boxed in BoolBoxed object)
    /// to new value, saving existing 
    /// value temporarily.  Later
    /// resets it to saved value when
    /// BoolCache is disposed
    /// </summary>
    public class BoolCache : IDisposable
    {
        private bool originalValue;
        BoolBoxed variableRef;
        /// <summary>
        /// Saves old value of a BoolBoxed object while it
        /// changes it to new value; Then restores it
        /// when disposed.
        /// </summary>
        /// <param name="variable"></param>
        /// <param name="newValue"></param>
        public BoolCache(ref BoolBoxed variable, bool newValue)
        {
            originalValue = variable.Value;
            variableRef = variable;
            variable.Value = newValue;
        }
        /// <summary>
        /// restores original value to BoolBoxed object
        /// </summary>
        public void Dispose()
        {
            variableRef.Value = originalValue;
        }
    }

	/// <summary>
	/// simple object reference to an integer
	/// </summary>
	public class IntRef
	{
		/// <summary>
		/// integer value to reference
		/// </summary>
		public int Value;
	}

	
    


    /// <summary>
    /// simple array of objects, exposes 
    /// Count, Add and Array members.
    /// </summary>
    internal class SimpleObjectArray
    {
        /// <summary>
        /// size of array to start with, and by which to grow
        /// array each time it reaches capacity
        /// </summary>
        private int startSize;

        private int count;
        /// <summary>
        /// the number of objects actually assigned in the array 
        /// (which may be less than the size of the array)
        /// </summary>
        internal int Count
        {
            get { return count; }
        }

        /// <summary>
        /// the array of objects.  Do not try to
        /// access members above index of Count-1
        /// </summary>
        internal object[] ObjArray;

        /// <summary>
        /// simple array of objects, exposes 
        /// Count, Add, Clear and objArray members.
        /// </summary>
        /// <param name="startSize"></param>
        internal SimpleObjectArray(int startSize)
        {
            ObjArray = new object[startSize];
            count = startSize;
            this.startSize = startSize;
        }

        /// <summary>
        /// add object to the array
        /// </summary>
        /// <param name="o"></param>
        internal void Add(object o)
        {
            //check array size
            if (ObjArray.Length < count + 1)
            {
                //grow the array
                object[] tempArray = new object[ObjArray.Length + startSize];
                ObjArray.CopyTo(tempArray, 0);
                ObjArray = tempArray;
            }
            //add new member
            ObjArray[count] = o;
            count++;
        }

        /// <summary>
        /// resets count to zero and builds new array
        /// </summary>
        internal void Clear()
        {
            //resize to original size in case has grown large
            ObjArray = new object[startSize];
            count = 0;
        }
    }

    /// <summary>
    /// a debugging tool to write timed notes to console
    /// </summary>
    public static class WveDebugLog
    {
        private static DateTime lastLogTime = DateTime.MinValue;
        /// <summary>
        /// log message to console
        /// </summary>
        /// <param name="message"></param>
        /// <param name="resetLastLogTime">if true, starts over with time 
        /// interval of zero; otherwise reports time span since last time
        /// LogEvent() was called.</param>
        public static void LogEvent(string message, bool resetLastLogTime)
        {
            StringBuilder sb = new StringBuilder();
            DateTime logTime = DateTime.Now;
            //time since last log
            TimeSpan ts;
            //if directed to reset log time, OR
            // if this is first event since class was created...
            if ((resetLastLogTime) ||
                (lastLogTime == DateTime.MinValue))
            {
                ts = TimeSpan.FromTicks(0);
            }
            else
            {
                ts = logTime - lastLogTime;
            }
            lastLogTime = logTime;
            //create message
            sb.Append("(");
            sb.Append(ts.TotalMilliseconds.ToString());
            sb.Append(" ms.) ");
            sb.Append(message);
            //write it
            Console.WriteLine(sb.ToString());
        }
    }

    /// <summary>
    /// key and value pair useful for List.
    /// advantage of List of StringPairs over 
    /// DictionaryList is List of StringPairs
    /// preserves case and order of creation
    /// </summary>
    public class StringPair
    {
        /// <summary>
        /// key to search by
        /// </summary>
        public string Key;
        /// <summary>
        /// text to associate with key
        /// </summary>
        public string Value;

        /// <summary>
        /// key and value pair for lists
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public StringPair(string key, string value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// return the first StringPair found exactly matching the key, 
        /// or null if none found.  
        /// </summary>
        /// <param name="list"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static StringPair FindByKey(List<StringPair> list, string key)
        {
            foreach (StringPair pair in list)
            {
                if (pair.Key == key)
                    return pair;
            }
            //if got here didn't find it
            return null;
        }

        /// <summary>
        /// return the first StringPair found exactly matching thekey,
        /// or null if none found.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static StringPair FindByValue(List<StringPair> list, string value)
        {
            foreach (StringPair pair in list)
            {
                if (pair.Value == value)
                    return pair;
            }
            //if got here didn't find it
            return null;
        }
    }
}