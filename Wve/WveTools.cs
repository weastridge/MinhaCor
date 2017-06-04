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
using System.Globalization;

namespace Wve
{
    /// <summary>
    /// some utilities
    /// </summary>
    public class WveTools
    {
        private static Random randomInstance = null;
        /// <summary>
        /// returns a random double number between 0 and 1
        /// </summary>
        /// <returns></returns>
        public static double GetRandomNumber()
        {
            //instantiate if necessary
            if (randomInstance == null)
            {
                int seed = DateTime.Now.Millisecond;
                randomInstance = new Random(seed);
            }
            return randomInstance.NextDouble();
        }

        /// <summary>
        /// forces regeneration of the Random class
        /// with given seed, then returns the first
        /// double between 0 and 1
        /// </summary>
        /// <param name="seed"></param>
        /// <returns></returns>
        public static double GetRandomNumber(int seed)
        {
            randomInstance = new Random(seed);
            return randomInstance.NextDouble();
        }

        /// <summary>
        /// true if string is one or more digits
        /// allows minus sign and trimming
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool IsInteger(string input)
        {
            return Regex.IsMatch(input.Trim(), @"^-*\d\d*$");
        }

        /// <summary>
        /// formats a 7 and 10 digit number into phone number
        /// format, or else returns unchanged string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FormatPhoneNumber(string input)
        {
            StringBuilder sb;
            string inputTrimmed = input.Trim();
            if(Regex.IsMatch(inputTrimmed.Trim(), @"^\d\d\d\d\d\d\d$"))
            {
                inputTrimmed = inputTrimmed.Trim();
                sb = new StringBuilder();
                sb.Append(inputTrimmed.Substring(0,3));
                sb.Append("-");
                sb.Append(inputTrimmed.Substring(3,4));
                return sb.ToString();
            }
            else if (Regex.IsMatch(inputTrimmed.Trim(), @"^\d\d\d\d\d\d\d\d\d\d$"))
            {
                inputTrimmed = inputTrimmed.Trim();
                sb = new StringBuilder();
                sb.Append("(");
                sb.Append(inputTrimmed.Substring(0, 3));
                sb.Append(") ");
                sb.Append(inputTrimmed.Substring(3, 3));
                sb.Append("-");
                sb.Append(inputTrimmed.Substring(6, 4));
                return sb.ToString();
            }
            else
            {
                return inputTrimmed;
            }
        }

        /// <summary>
        /// returns true if byte arrays are of same length
        /// and each byte is equal
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool ByteArraysMatch(byte[] first, byte[] second)
        {
            bool result = false;
            if (first.Length == second.Length)
            {
                int i;
                for (i = 0; i < first.Length; i++)
                {
                    if (first[i] != second[i])
                        break;
                }
                //if all matched i should = length
                result = (i == first.Length);
            }
            return result;
        }

        /// <summary>
        /// returns the age in years of someone with 
        /// given date of birth.  Use TargetDate = DateTime.Today
        /// to find age today.
        /// </summary>
        /// <param name="DOB"></param>
        /// <param name="TargetDate"></param>
        /// <returns></returns>
        public static int AgeYears(DateTime DOB, DateTime TargetDate)
        {
            //start with raw subtraction
            int result = TargetDate.Year - DOB.Year;
            //subtract one if hasn't had birthday yet...
            if (TargetDate.Month < DOB.Month)
                result--;
            else if ((TargetDate.Month == DOB.Month) &&
                (TargetDate.Day < DOB.Day))
                result--;
            return result;
        }

        /// <summary>
        /// years, including fraction of years, from DOB til TargetDate,
        /// or float.MinValue if TargetDate is before date of birth
        /// </summary>
        /// <param name="DOB"></param>
        /// <param name="TargetDate"></param>
        /// <returns></returns>
        public static float AgeYearsFloat(DateTime DOB, DateTime TargetDate)
        {
            float result = float.MinValue;
            TimeSpan ts = TargetDate - DOB;
            if (TargetDate > DOB)
            {
                result = (float)(ts.TotalDays / 365.2425D);
            }
            return result;
        }

        /// <summary>
        /// get age from dob to targetDate, shown either as 
        /// 'err' if target before dob or  'decsd' if dead
        /// or else an integer followed
        /// by y,m or d
        /// </summary>
        /// <param name="dob"></param>
        /// <param name="targetDate"></param>
        /// <param name="isDeceased"></param>
        /// <returns></returns>
        public static string AgeString(DateTime dob, DateTime targetDate, bool isDeceased)
        {
            if (isDeceased)
                return "decsd";
            else
                return AgeString(dob, targetDate);
        }

        /// <summary>
        /// get age from dob to targetDate, shown either as 
        /// 'err' if target before dob or else an integer followed
        /// by y,m or d 
        /// </summary>
        /// <param name="dob"></param>
        /// <param name="targetDate"></param>
        /// <returns></returns>
        public static string AgeString(DateTime dob, DateTime targetDate)
        {
            //------------
            //written by WVE
            //Nov 14, 2006
            //____________

            //full years
            int years = int.MinValue;
            //months in addition to full years
            int monthsRemainder = int.MinValue;
            //days in addition to full years and months
            int daysRemainder = int.MinValue;
            StringBuilder sb = new StringBuilder();

            //return err if negative value
            if (targetDate < dob)
            {
                sb.Append("err");
            }
            else
            {
                //find years
                years = targetDate.Year - dob.Year; //to begin with
                //decrement if haven't reached month yet
                if (targetDate.Month < dob.Month)
                {
                    years--;
                }
                //or if months same but haven't reached day yet
                else if ((targetDate.Month == dob.Month) &&
                    (targetDate.Day < dob.Day))
                {
                    years--;
                }

                //find months
                if (targetDate.Month == dob.Month)
                {
                    //then remainder is 0 or 11 mo, depending on day
                    if (targetDate.Day < dob.Day)
                    {
                        //haven't reached the day yet so not a full year yet
                        monthsRemainder = 11;
                    }
                    else
                    {
                        //reached full year
                        monthsRemainder = 0;
                    }
                }
                else
                {
                    //different months...
                    if (targetDate.Month > dob.Month)
                    {
                        monthsRemainder = targetDate.Month - dob.Month;
                    }
                    else
                    {
                        monthsRemainder = targetDate.Month + 12 - dob.Month;
                    }
                    //from which we decrement if haven't reached day yet
                    if (targetDate.Day < dob.Day)
                    {
                        monthsRemainder--;
                    }
                }//from if months different

                //find days only if age is <= 31 days
                if (targetDate - dob < TimeSpan.FromDays(31))
                {
                    TimeSpan ts = targetDate - dob;
                    daysRemainder = ts.Days;
                }

                //generate return value
                //show days if they are defined (would be int.MinValue if not)
                if (daysRemainder > -1)
                {
                    sb.Append(daysRemainder.ToString().Trim());
                    sb.Append("d");
                }
                //or months if less than 2 years old
                else if (years < 2)
                {
                    //adjust for age between 1-2 years
                    if (years == 1)
                    {
                        monthsRemainder = monthsRemainder + 12;
                    }
                    sb.Append(monthsRemainder.ToString().Trim());
                    sb.Append("m");
                }
                //or years
                else
                {
                    sb.Append(years.ToString().Trim());
                    sb.Append("y");
                }

            }//from if not negative value

            //return
            return sb.ToString();
        }

        /// <summary>
        /// date in HL7 TS (timestamp) format which conforms to
        /// ISO 8824-1987(E) sort of... 
        /// YYYY[MM[DD[HHMM[SS]]]][+ZZZZ]
        /// </summary>
        /// <param name="date"></param>
        /// <param name="precision">YMDhms</param>
        /// <param name="timezone">int.MinValue for not specified, or
        /// -6 for Eastern Standard, 0 for GMT.  Ignores integers
        /// outside of range from -23 to 23</param>
        /// <returns></returns>
        public static string DateToHL7(DateTime date, 
            char precision,
            int timezone)
        {
            //note the full ISO includes thousandths of seconds and precision:
            //YYYY[MM[DD[HHMM[SS[.S[S[S[S]]]]]]]][+ZZZZ]^<degree of precision>
            // but this doesn't.
            StringBuilder result = new StringBuilder();
            result.Append(string.Format("{0:0000}", date.Year));
            if (precision != 'Y')
            {
                result.Append(string.Format("{0:00}", date.Month));
                if (precision != 'M')
                {
                    result.Append(string.Format("{0:00}", date.Day));
                    if (precision != 'D')
                    {
                        result.Append(string.Format("{0:00}", date.Hour));
                        if (precision != 'h')
                        {
                            result.Append(string.Format("{0:00}", date.Minute));
                            if (precision != 'm')
                            {
                                result.Append(string.Format("{0:00}", date.Second));
                            }
                        }
                    }
                }
            }

            //now add time zone if given
            if ((timezone < 0) && timezone > -24)
            {
                //result.Append("-");
                result.Append(string.Format("{0:00}", timezone));
                result.Append("00");
            }
            else if ((timezone > -1) && (timezone < 24))
            {
                result.Append("+");
                result.Append(string.Format("{0:00}", timezone));
                result.Append("00");
            }
            return result.ToString();
        }

        /// <summary>
        /// SureScripts XML DateTime format YYYY-MM-DDT12:00:00-05:00 for example, 
        /// for surescripts UtcDate specify convertToUTC
        /// </summary>
        /// <param name="date"></param>
        /// <param name="precision"></param>
        /// <param name="timezone">significant ONLY if convertToUTC is false!</param>
        /// <param name="convertToUTC">if true, converts to zulu time - OVERRIDES and IGNORES 
        /// timezone value.  If you send a date that is already in UTC, just give it a 
        /// timezone value of zero and leave convertToUTC false.</param>
        /// <returns></returns>
        public static string DateToSureScripts(DateTime value,
            int timezone, bool convertToUTC)
        {
            DateTime date;
            if (convertToUTC)
                date = value.ToUniversalTime();
            else
                date = value;

            StringBuilder result = new StringBuilder();
            result.Append(string.Format("{0:0000}", date.Year));
            result.Append("-");
            result.Append(string.Format("{0:00}", date.Month));
            result.Append("-");
            result.Append(string.Format("{0:00}", date.Day));
            result.Append("T");
            result.Append(string.Format("{0:00}", date.Hour));
            result.Append(":");
            result.Append(string.Format("{0:00}", date.Minute));
            result.Append(":");
            result.Append(string.Format("{0:00}", date.Second));


            //now add time zone
            if ((convertToUTC) ||( timezone == 0))
            {
                result.Append("Z");
            }
            else if ((timezone < 0) && timezone > -24)
            {
                result.Append("-");
                result.Append(string.Format("{0:00}", timezone));
                result.Append(":00");
            }
            else if ((timezone > -1) && (timezone < 24))
            {
                result.Append("+");
                result.Append(string.Format("{0:00}", timezone));
                result.Append(":00");
            }
            return result.ToString();
        }

        /// <summary>
        /// W3C XML Schema DateTime format YYYY-MM-DDT12:00:00.000-05:00 for example, 
        /// for SureScripts UtcDate as well as SAML DateTime specify convertToUTC
        /// </summary>
        /// <param name="date"></param>
        /// <param name="precision"></param>
        /// <param name="timezone">significant ONLY if convertToUTC is false!</param>
        /// <param name="convertToUTC">if true, converts value to zulu time - OVERRIDES and IGNORES 
        /// timezone value.  If you send a date that is already in UTC, you can give it a 
        /// timezone value of zero and leave convertToUTC false.</param>
        /// <returns></returns>
        public static string DateToXMLSchema(DateTimeOffset value,
            int timezone, bool convertToUTC, bool includeMilliseconds)
        {
            DateTimeOffset date;
            if (convertToUTC)
                date = value.ToUniversalTime();
            else
                date = value;

            StringBuilder result = new StringBuilder();
            result.Append(string.Format("{0:0000}", date.Year));
            result.Append("-");
            result.Append(string.Format("{0:00}", date.Month));
            result.Append("-");
            result.Append(string.Format("{0:00}", date.Day));
            result.Append("T");
            result.Append(string.Format("{0:00}", date.Hour));
            result.Append(":");
            result.Append(string.Format("{0:00}", date.Minute));
            result.Append(":");
            result.Append(string.Format("{0:00}", date.Second));
            if(includeMilliseconds)
            {
                result.Append(":");
                result.Append(string.Format("{0:000}", date.Millisecond));
            }


            //now add time zone
            if ((convertToUTC) || (timezone == 0))
            {
                result.Append("Z");
            }
            else if ((timezone < 0) && timezone > -24)
            {
                result.Append("-");
                result.Append(string.Format("{0:00}", timezone));
                result.Append(":00");
            }
            else if ((timezone > -1) && (timezone < 24))
            {
                result.Append("+");
                result.Append(string.Format("{0:00}", timezone));
                result.Append(":00");
            }
            return result.ToString();
        }

        /// <summary>
        /// e.g. 20091109_115959
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DateTimeToCompactString(DateTime dt)
        {
            StringBuilder result = new StringBuilder();
            result.Append(string.Format("{0:0000}", dt.Year));
            result.Append(string.Format("{0:00}", dt.Month));
            result.Append(string.Format("{0:00}", dt.Day));
            result.Append("_");
            result.Append(string.Format("{0:00}", dt.Hour));
            result.Append(string.Format("{0:00}", dt.Minute));
            result.Append(string.Format("{0:00}", dt.Second));
            return result.ToString();
        }

        /// <summary>
        /// from string generated by DateTimeToCompactString(),
        /// e.g. 20091109_115959
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static DateTime DateTimeFromCompactString(string s)
        {
            return new DateTime(
                int.Parse(s.Substring(0, 4)),
                int.Parse(s.Substring(4, 2)),
                int.Parse(s.Substring(6, 2)),
                int.Parse(s.Substring(9, 2)),
                int.Parse(s.Substring(11, 2)),
                int.Parse(s.Substring(13, 2)));
        }
        

        /// <summary>
        /// creates string representation of social security number
        /// or empty string if int.MinValue or negative
        /// </summary>
        /// <param name="ssnInt"></param>
        /// <returns></returns>
        public static string SSN(int ssnInt)
        {
            if (ssnInt < 0)
                return "";
            else
            {
                StringBuilder sb;
                string result = ssnInt.ToString();
                //prepend zeros if needed
                if (result.Length < 9)
                {
                    sb = new StringBuilder();
                    for (int i = 0; i < (9 - result.Length); i++)
                    {
                        sb.Append("0");
                    }
                    sb.Append(result);
                    result = sb.ToString();
                }
                //put in dashes
                sb = new StringBuilder();
                sb.Append(result.Substring(0, 3));
                sb.Append("-");
                sb.Append(result.Substring(3, 2));
                sb.Append("-");
                sb.Append(result.Substring(5, 4));
                return sb.ToString();
            }
        }

        /// <summary>
        /// returns the last four digits of integer, padded with
        /// zeros if needed, to be used as last 4 digits of SSN.
        /// Returns 'unkn' if ssnInt is negative
        /// </summary>
        /// <param name="ssnInt">integer representing last 4 digits.  If 
        /// longer than 4 digits only the last 4 are included</param>
        /// <returns></returns>
        public static string SSNLast4(int ssnInt)
        {
            if (ssnInt < 0)
                return "unkn";
            //ignore more than 4 digits
            if (ssnInt > 9999)
                ssnInt = ssnInt % 10000;
            return string.Format("{0:0000}", ssnInt);
        }

        /// <summary>
        /// deeply clone an object that is serializable and cloneable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T DeepCloneObject<T>(T obj)
        {
            //this comes from Programming Microsoft Visual C# 2005:
            //The Base Class Library,
            // by Francesco Balena, p 520

            //create a memory stream and a formatter.
            using (MemoryStream ms = new MemoryStream(1000))
            {
                BinaryFormatter bf = new BinaryFormatter(
                    null,  //ISurrogateSelector
                    new StreamingContext(StreamingContextStates.Clone));
                //serialize the object
                bf.Serialize(ms, obj);
                //position stream pointer back to first byte
                ms.Seek(0, SeekOrigin.Begin);
                //deserialize
                return (T)bf.Deserialize(ms);
            }
        }

        /// <summary>
        /// serialize a serializable object to a byte array
        /// or null if error
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] SerializeToBytes<T>(T obj)
        {
            //this comes from Programming Microsoft Visual C# 2005:
            //The Base Class Library,
            // by Francesco Balena, p 523
            byte[] result = null; //unless assigned below
            //open stream
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter bf = new BinaryFormatter(
                    null, new StreamingContext(StreamingContextStates.Persistence));
                bf.Serialize(ms, obj);
                //save to byte array
                result = new byte[ms.Length];
                //point to beginning of memory stream
                ms.Seek(0, SeekOrigin.Begin);
                ms.Read(result, 0, (int)ms.Length); //error if length longer than int.MaxValue
            }
            return result;
        }

        /// <summary>
        /// return the object that was serialized in given byte array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serialBytes"></param>
        /// <returns></returns>
        public static T DeserializeFromBytes<T>(byte[] serialBytes)
        {
            //this comes from Programming Microsoft Visual C# 2005:
            //The Base Class Library,
            // by Francesco Balena, p 523
            using (MemoryStream ms = new MemoryStream(serialBytes))
            {
                BinaryFormatter bf = new BinaryFormatter(
                    null, new StreamingContext(StreamingContextStates.Persistence));
                //Deserialize
                return (T)bf.Deserialize(ms);
            }
        }

        /// <summary>
        /// remove item at given index and return array that is one
        /// item shorter if index was found, or throws exception
        /// if index out of range.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original"></param>
        /// <param name="removeAt">zero based index of item to remove</param>
        /// <returns></returns>
        public static T[] RemoveArrayItem<T>(T[] original, int removeAt)
        {
            if (removeAt < 0)
            {
                throw new IndexOutOfRangeException("Tried to remove item " +
                    "from array but requested index is negative.");
            }
            else if (removeAt < original.Length)
            {
                T[] tempArray = new T[original.Length - 1];
                Array.Copy(original, 0, tempArray, 0, removeAt);
                if (removeAt < original.Length - 1)
                {
                    Array.Copy(original, removeAt + 1,
                        tempArray, removeAt,
                        original.Length - 1 - removeAt);
                }
                return tempArray;
            }
            else
            {
                throw new IndexOutOfRangeException("Tried to remove array "
                        + "item at greater index value than array has.");
            }
        }

        /// <summary>
        /// insert new item at requested index position or raise
        /// error if out of range
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="original">original array</param>
        /// <param name="newItem">new item to insert</param>
        /// <param name="insertAt">zero based index of position to insert item</param>
        /// <returns></returns>
        public static T[] InsertArrayItem<T>(T[] original, T newItem, int insertAt)
        {
            if (insertAt < 0)
            {
                throw new IndexOutOfRangeException(
                    "Tried to insert item into array but "+
                    "requested index of position is negative.");
            }
            else if (insertAt < original.Length + 1)
            {
                T[] tempArray = new T[original.Length + 1];
                Array.Copy(original, 0, tempArray, 0, insertAt);
                tempArray[insertAt] = newItem;
                if (original.Length > insertAt)
                {
                    Array.Copy(original, insertAt,
                        tempArray, insertAt + 1,
                        original.Length - insertAt);
                }
                return tempArray;
            }
            else
            {
                throw new IndexOutOfRangeException(
                    "Tried to insert array item but " +
                    "requested index is too high for this array's size");
            }
        }

        /// <summary>
        /// returns a color with the given name,
        /// or black if can't tell
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static System.Drawing.Color GetColorFromName(string name)
        {
            //get common colors first, quickly
            if (name == null)
                return Color.Black;
            switch (name)
            {
                case "":
                    return Color.Black;

                case "Black":
                    return Color.Black;

                case "Red":
                    return Color.Red;

                case "Green":
                    return Color.Green;

                case "Blue":
                    return Color.Blue;

                case "Orange":
                    return Color.Orange;

                case "Purple":
                    return Color.Purple;

                case "Yellow":
                    return Color.Yellow;

                case "Brown":
                    return Color.Brown;

                default:
                    //don't assign it
                    break;

            }
            //if we got here we haven't assigned color yet so try all system colors with
            // (a time-costly) error trapping
            try
            {
                return ColorTranslator.FromHtml(name);
            }
            catch
            {
                //default to black
                return Color.Black;
            }
        }

        #region WriteIntAsText
        /// <summary>
        /// text representation of integer under 20, 
        /// or null if > 20 or negative
        /// </summary>
        /// <param name="integer"></param>
        /// <returns></returns>
        public static string IntToTextUnder20(int integer)
        {
            switch (integer)
            {
                case 0:
                    return "zero";
                case 1:
                    return "one";
                case 2:
                    return "two";
                case 3:
                    return "three";
                case 4:
                    return "four";
                case 5:
                    return "five";
                case 6:
                    return "six";
                case 7:
                    return "seven";
                case 8:
                    return "eight";
                case 9:
                    return "nine";
                case 10:
                    return "ten";
                case 11:
                    return "eleven";
                case 12:
                    return "twelve";
                case 13:
                    return "thirteen";
                case 14:
                    return "fourteen";
                case 15:
                    return "fifteen";
                case 16:
                    return "sixteen";
                case 17:
                    return "seventeen";
                case 18:
                    return "eighteen";
                case 19:
                    return "nineteen";
                default:
                    return null;
            }
        }

        /// <summary>
        /// text representation of positive integers between 
        /// 0 and 99, inclusive, or null if outside range
        /// </summary>
        /// <param name="integer"></param>
        /// <returns></returns>
        public static string IntToTextUnder100(int integer)
        {
            StringBuilder sb = new StringBuilder();
            //if negative return null
            if (integer < 0)
                return null;
            //if under 20 just return that
            else if (integer < 20)
                return IntToTextUnder20(integer);
            //otherwise...

            //write the tens digit: (whats left after subtract mod 10)
            switch (integer - (integer % 10))
            {
                case 20:
                    sb.Append("twenty");
                    break;
                case 30:
                    sb.Append("thirty");
                    break;
                case 40:
                    sb.Append("forty");
                    break;
                case 50:
                    sb.Append("fifty");
                    break;
                case 60:
                    sb.Append("sixty");
                    break;
                case 70:
                    sb.Append("seventy");
                    break;
                case 80:
                    sb.Append("eighty");
                    break;
                case 90:
                    sb.Append("ninety");
                    break;
                default:
                    return null;
            }
            //add hyphen and ones digit if ones digit > 0
            if ((integer % 10) > 0)
            {
                sb.Append("-");
                sb.Append(IntToTextUnder20(integer % 10));
            }
            return sb.ToString();
        }

        /// <summary>
        /// text representation of positive integer under 1000,
        /// or null if negative or > 999.
        /// </summary>
        /// <param name="integer"></param>
        /// <returns></returns>
        public static string IntToTextUnder1000(int integer)
        {
            StringBuilder sb = new StringBuilder();
            //if negative return null
            if (integer < 0)
                return null;
            //or if < 100, call IntTextUnder100
            else if (integer < 100)
            {
                return IntToTextUnder100(integer);
            }
            //or if 100 or more (returns null if > 999)
            else
            {
                //write hundreds digit (what's left after subract integer mod 100
                switch (integer - (integer % 100))
                {
                    case 100:
                        sb.Append("one hundred");
                        break;
                    case 200:
                        sb.Append("two hundred");
                        break;
                    case 300:
                        sb.Append("three hundred");
                        break;
                    case 400:
                        sb.Append("four hundred");
                        break;
                    case 500:
                        sb.Append("five hundred");
                        break;
                    case 600:
                        sb.Append("six hundred");
                        break;
                    case 700:
                        sb.Append("seven hundred");
                        break;
                    case 800:
                        sb.Append("eight hundred");
                        break;
                    case 900:
                        sb.Append("nine hundred");
                        break;
                    default:
                        return null;
                }
                //add space and modulus if any
                if ((integer % 100) > 0)
                {
                    sb.Append(" ");
                    sb.Append(IntToTextUnder100(integer % 100));
                }
            }
            return sb.ToString();
        }
        #endregion //WriteIntAsText

        /// <summary>
        /// trim or extend string to given size including a 
        /// one-character buffer at the end (which is either a space
        /// if string fits or '>' if truncated)
        /// </summary>
        /// <param name="size">full size including the single buffer char
        /// at the end</param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string SizeStringWithBuffer(int size, string input)
        {
            StringBuilder sb = new StringBuilder();

            if (input.Length < size - 1)
            {
                sb.Append(input);
                sb.Append(new string(' ', size - input.Length));
            }
            else if (input.Length == size - 1)
            {
                sb.Append(input);
                sb.Append(" ");
            }
            else if (input.Length > size - 1)
            {
                sb.Append(input.Substring(0, size -1));
                sb.Append(">");
            }
            return sb.ToString();
        }

        /// <summary>
        /// maps available single character tokens (0-9,A-Z, a-z)
        /// to integers (0-61), 
        /// but returns int.MinValue (representing not-defined) if token
        /// is char '@' or other non-alphanumeric character.
        /// (char overload)
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static int AlphaNumToInt(char token)
        {
            //beginning of numerals
            if (token > 47) // '/'
            {
                if (token < 58) // ':'
                {
                    //numerals 0-9
                    return (int)(token - 48);
                }

                //beginning of capitals
                else if (token > 64) // '@'
                {
                    //capitals
                    if (token < 91) //'['
                    {
                        return (int)(token - 55);
                    }
                    //skip up to lower case
                    else if (token > 96) //'`'
                    {
                        if (token < 123) //'{'
                        {
                            return (int)(token - 61);
                        }
                        else //above lower case 
                        {
                            return int.MinValue;
                        }
                    }
                    else // between caps and lower case
                    {
                        return int.MinValue;
                    }
                }
                else //below caps and above numerals
                {
                    return int.MinValue;
                }
            }
            else //below numerals
            {
                return int.MinValue;
            }
        }

        /// <summary>
        /// maps available single character tokens (0-9,A-Z, a-z)
        /// to integers (0-61), 
        /// but returns int.MinValue (representing not-defined) if token
        /// is char '@' or other non-alphanumeric character.
        /// (string overload interprets only first char of string)
        /// </summary>
        /// <param name="tokenString"></param>
        /// <returns></returns>
        public static int AlphaNumToInt(string tokenString)
        {
            //convert first char of string to char token
            // or return int.MinValue signifying null if string empty
            char token;
            if ((tokenString != null) &&
                (tokenString.Length > 0))
            {
                token = char.Parse(tokenString.Substring(0, 1));
            }
            else
                return int.MinValue;

            //beginning of numerals
            if (token > 47) // '/'
            {
                if (token < 58) // ':'
                {
                    //numerals 0-9
                    return (int)(token - 48);
                }

                //beginning of capitals
                else if (token > 64) // '@'
                {
                    //capitals
                    if (token < 91) //'['
                    {
                        return (int)(token - 55);
                    }
                    //skip up to lower case
                    else if (token > 96) //'`'
                    {
                        if (token < 123) //'{'
                        {
                            return (int)(token - 61);
                        }
                        else //above lower case 
                        {
                            return int.MinValue;
                        }
                    }
                    else // between caps and lower case
                    {
                        return int.MinValue;
                    }
                }
                else //below caps and above numerals
                {
                    return int.MinValue;
                }
            }
            else //below numerals
            {
                return int.MinValue;
            }
        }


        /// <summary>
        /// maps available single character tokens (A-Z, a-z)
        /// to integers(0-51), 
        /// but returns '@' representing not-defined or null if ordinal outside the 
        /// allowed range.  
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static char IntToAlphaNum(int i)
        {
            if ((i < 0) || (i > 61))
            {
                //outside range, so return '@' (representing not-defined)
                return '@';
            }
            //numerals
            else if (i < 10)
            {
                //0-9
                return (char)(i + 48);
            }
            //upper case
            else if (i < 36)
            {
                //A-Z
                return (char)(i + 55);
            }
            //lower case
            else
            {
                //a-z
                return (char)(i + 61);
            }
        }

        /// <summary>
        /// returns the date and time an assembly was built, if
        /// the build and revision parts of the version values
        /// look like they were generated by the default method
        /// that calculates them from days and duple seconds 
        /// or DateTime.MinValue if not
        /// </summary>
        /// <param name="ver">The assembly's version</param>
        /// <returns></returns>
        public static DateTime GetBuildDateTime(Version ver)
        {
            DateTime result = DateTime.MinValue;
            DateTime buildDate = DateTime.Parse("2000/01/01").AddDays(
                        ver.Build);
            DateTime buildTime = DateTime.Today.AddSeconds(
                ver.Revision * 2);
            //return datetime if it looks like a reasonable date
            if (( ver.Build >  1095 )&& //date after Jan 2003
                (ver.Build < 10950)) //date before 2030
            {
                result = buildDate + buildTime.TimeOfDay;
            }
            return result;
        }

        /// <summary>
        /// useful utility to check to see if a column exists to avoid error
        /// when trying to read from it with reader["MyColumnName"]
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static bool ColumnExists(System.Data.SqlClient.SqlDataReader reader, 
            string columnName)
        {
            // not this but something similar might work:
            //; return reader.GetSchemaTable().Columns.Contains(columnName);
            
            reader.GetSchemaTable().DefaultView.RowFilter = "ColumnName= '" +
            columnName + "'";
            return (reader.GetSchemaTable().DefaultView.Count > 0);
            
        }
        /// <summary>
        /// return substring of only teh characters and numbers of a string 
        /// or periods or hyphens or underlines
        /// </summary>
        /// <param name="rawName"></param>
        /// <returns></returns>
        public static string GroomStringForFileName(string rawName)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < rawName.Length; i++)
            {
                if((rawName.Substring(i,1) == "." ) ||
                    (rawName.Substring(i,1) == "_") ||
                    (rawName.Substring(i,1) == "-") ||
                    (Regex.IsMatch(rawName.Substring(i,1),@"\w|\d")))
                {
                    sb.Append(rawName.Substring(i,1));
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// converts string to hexadecimal representation of its ascii chars
        /// </summary>
        /// <param name="ascii"></param>
        /// <param name="delimiter">delimiter between hex two char bytes, 
        /// or string.Empty to run them all together</param>
        /// <returns></returns>
        public static string AsciiToHex(string ascii, string delimiter)
        {
            byte[] byteArray = System.Text.ASCIIEncoding.ASCII.GetBytes(ascii);
            return BytesToHex(byteArray, delimiter);
        }

        /*
        /// <summary>
        /// convert hex string to string its bytes could represent??
        /// ?? This hasn't been tested yet!!!
        /// </summary>
        /// <param name="hex"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public static string HexToString(string hex, string delimiter)
        {
            StringBuilder sb = new StringBuilder();
            //remove delimiter
            hex = hex.Replace(delimiter, string.Empty);
            for (int i = 0; i < hex.Length/2; i++)
            {
                sb.Append(Convert.ToString(
                Convert.ToChar(int.Parse(hex.Substring((i * 2), 2),
                    System.Globalization.NumberStyles.HexNumber))));
            }
            return sb.ToString();
        }
         */

        /// <summary>
        /// return string from bytes, assuming bytes are in ASCII
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string AsciiBytesToString(byte[] bytes)
        {
            ASCIIEncoding ae = new ASCIIEncoding();
            return ae.GetString(bytes);
        }
        /// <summary>
        /// return hexadecimal representation of byte array, optionally with
        /// delimiter between each two character byte 
        /// </summary>
        /// <param name="byteArray">array of bytes to conver to hex string</param>
        /// <param name="delimiter">delimiter to insert between bytes, or
        /// string.Empty for none</param>
        /// <returns></returns>
        public static string BytesToHex(byte[] byteArray, string delimiter)
        {
            if(delimiter == null)
                delimiter = string.Empty;
            StringBuilder sb;
            string[] hexs = { "00", "01", "02", "03", "04", "05", "06", "07", 
                                 "08", "09", "0A", "0B", "0C", "0D", "0E", "0F", 
                                 "10", "11", "12", "13", "14", "15", "16", "17", 
                                 "18", "19", "1A", "1B", "1C", "1D", "1E", "1F", 
                                 "20", "21", "22", "23", "24", "25", "26", "27", 
                                 "28", "29", "2A", "2B", "2C", "2D", "2E", "2F", 
                                 "30", "31", "32", "33", "34", "35", "36", "37", 
                                 "38", "39", "3A", "3B", "3C", "3D", "3E", "3F", 
                                 "40", "41", "42", "43", "44", "45", "46", "47", 
                                 "48", "49", "4A", "4B", "4C", "4D", "4E", "4F", 
                                 "50", "51", "52", "53", "54", "55", "56", "57", 
                                 "58", "59", "5A", "5B", "5C", "5D", "5E", "5F", 
                                 "60", "61", "62", "63", "64", "65", "66", "67", 
                                 "68", "69", "6A", "6B", "6C", "6D", "6E", "6F", 
                                 "70", "71", "72", "73", "74", "75", "76", "77", 
                                 "78", "79", "7A", "7B", "7C", "7D", "7E", "7F", 
                                 "80", "81", "82", "83", "84", "85", "86", "87", 
                                 "88", "89", "8A", "8B", "8C", "8D", "8E", "8F", 
                                 "90", "91", "92", "93", "94", "95", "96", "97", 
                                 "98", "99", "9A", "9B", "9C", "9D", "9E", "9F", 
                                 "A0", "A1", "A2", "A3", "A4", "A5", "A6", "A7", 
                                 "A8", "A9", "AA", "AB", "AC", "AD", "AE", "AF", 
                                 "B0", "B1", "B2", "B3", "B4", "B5", "B6", "B7", 
                                 "B8", "B9", "BA", "BB", "BC", "BD", "BE", "BF", 
                                 "C0", "C1", "C2", "C3", "C4", "C5", "C6", "C7", 
                                 "C8", "C9", "CA", "CB", "CC", "CD", "CE", "CF", 
                                 "D0", "D1", "D2", "D3", "D4", "D5", "D6", "D7", 
                                 "D8", "D9", "DA", "DB", "DC", "DD", "DE", "DF", 
                                 "E0", "E1", "E2", "E3", "E4", "E5", "E6", "E7", 
                                 "E8", "E9", "EA", "EB", "EC", "ED", "EE", "EF", 
                                 "F0", "F1", "F2", "F3", "F4", "F5", "F6", "F7", 
                                 "F8", "F9", "FA", "FB", "FC", "FD", "FE", "FF" };
            sb = new StringBuilder();
            for(int i=0; i<byteArray.Length; i++)
            {
                sb.Append(hexs[byteArray[i]]);
                //append delimiter until last byte
                if (i < byteArray.Length - 1) 
                {
                    sb.Append(delimiter);
                }
            }
            return sb.ToString();
        }



        /// <summary>
        /// adapted from stackoverflow, by Aswath Krishnan
        /// requires 2 chars per byte input
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] HexStringToByteArray(string hexString)
        {
            if (hexString.Length % 2 != 0)
            {
                throw new ArgumentException(String.Format(
                    CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", hexString));
            }

            byte[] hexAsBytes = new byte[hexString.Length / 2];
            for (int index = 0; index < hexAsBytes.Length; index++)
            {
                string byteValue = hexString.Substring(index * 2, 2);
                hexAsBytes[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }

            return hexAsBytes;
        }
 
        /// <summary>
        /// parse string for numbers or fractions and
        /// return the float value or float.MinValue if can't
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static float ParseFraction(string s)
        {
            float result = float.MinValue; //unless found
            string numeratorString = null;
            string denominatorString = null;
            float numerator;
            float denominator;
            int divideLocation = s.IndexOf("/");
            if (divideLocation > -1)
            {
                numeratorString = s.Substring(0, divideLocation);
                if (s.Length > divideLocation + 1)
                {
                    denominatorString = s.Substring(divideLocation + 1, s.Length - divideLocation - 1);
                }
            }
            else
            {
                numeratorString = s;
            }
            //now divide
            if (float.TryParse(numeratorString, out numerator))
            {
                result = numerator;
                //divide if anything to divide by
                if (float.TryParse(denominatorString, out denominator))
                {
                    result = numerator / denominator;
                }
            }
            return result;
        }

        /// <summary>
        /// writes fields to a string separated by commas and, if
        /// any of the fields contains quotes (") or commas or if 
        /// forceQuotes is selected it surrounds
        /// that field with quotes and doubles the quotes inside ""
        /// appropriately.  Optionally appends one last comma at the end
        /// (in case further fields are expected to be appended) but does
        /// not append NewLine char at the end
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="appendLastComma">append a comma afer the last item, as when expecting more to be added</param>
        /// <param name="forceQuotes"></param>
        /// <param name="appendNewLine">append Environment.NewLine at end of last item, as at end of line</param>
        /// <returns></returns>
        public static string WriteCsv(string[] fields, bool appendLastComma, bool forceQuotes, bool appendNewLine)
        {
            StringBuilder sb = new StringBuilder();
            string field;
            for (int i = 0; i < fields.Length; i++)
            {
                //treat nulls as empty strings
                field = fields[i] == null ? string.Empty : fields[i];
                if ((forceQuotes) || 
                    (field.Contains("\"")) ||
                    (field.Contains(",")))
                {
                    sb.Append("\"");
                    sb.Append(field.Replace("\"", "\"\""));
                    sb.Append("\"");
                }
                else
                {
                    sb.Append(field);
                }
                if ((i < fields.Length - 1) || appendLastComma)
                {
                    sb.Append(",");
                }
                if((i==fields.Length -1 ) && (appendNewLine))
                {
                    sb.Append(Environment.NewLine);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// writes fields to a string separated by commas and, if
        /// any of the fields contains quotes (") or commas or if 
        /// forceQuotes is selected it surrounds
        /// that field with quotes and doubles the quotes inside ""
        /// appropriately.  Optionally appends one last comma at the end
        /// (in case further fields are expected to be appended) but does
        /// not append NewLine char at the end
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="appendLastComma"></param>
        /// <param name="forceQuotes"></param>
        /// <returns></returns>
        public static string WriteCsv(string[] fields, bool appendLastComma, bool forceQuotes)
        {
            return WriteCsv(fields, appendLastComma, forceQuotes, false); //false to append newline
        }

        /// <summary>
        /// writes fields to a string separated by commas and, if
        /// any of the fields contains quotes (") or commas it surrounds
        /// that field with quotes and doubles the quotes inside ""
        /// appropriately.  Optionally appends one last comma at the end
        /// (in case further fields are expected to be appended) but does
        /// not append NewLine char at the end
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="appendLastComma">if true will append a comma at the end</param>
        /// <returns></returns>
        public static string WriteCsv(string[] fields, bool appendLastComma)
        {
            return WriteCsv(fields, appendLastComma, false); //false force quotes.
        }

        /// <summary>
        /// using convention:  delimiter is comma,
        /// optional quotes are "
        /// imbedded quotes inside quotes are ""
        /// expects whole field to be in quotes if it contains
        /// any commas or quotes
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static string[] ReadCsvLine(string line)
        {
            char delimiter = ',';
            char quote = '"';
            int startsize = 20;
            string[] result = new string[startsize];
            string[] tempArray;
            string item = string.Empty;
            bool isInsideQuotes = false;
            StringBuilder sbCurrentField = new StringBuilder();
            int currentFieldIndex = 0;
            for (int i = 0; i < line.Length; i++)
            {
                //1. check for new field delimiter
                if ((!isInsideQuotes) && (line[i] == delimiter))
                {
                    //start of new field, so save current field after checking array length
                    if (currentFieldIndex >= result.Length)
                    {
                        //resize array
                        tempArray = new string[result.Length + startsize];
                        Array.Copy(result, tempArray, result.Length);
                        result = tempArray;
                    }
                    result[currentFieldIndex] = sbCurrentField.ToString();
                    //and increment counter and restart string builder
                    currentFieldIndex++;
                    sbCurrentField = new StringBuilder();
                }
                //2. check for quote outside of quotes
                else if ((!isInsideQuotes) && (line[i] == quote))
                {
                    //now we're inside quotes
                    isInsideQuotes = true;
                }
                //3. chesck for quote inside quotes
                else if ((isInsideQuotes) && (line[i] == quote))
                {
                    //check for double quote which would indicate double quote
                    if ((line.Length > i+1) && (line[i + 1] == quote))
                    {
                        sbCurrentField.Append(quote);
                        //and increment i to skip the second quote
                        i++;
                    }
                    //otherwise this ends the quotation
                    else
                    {
                        isInsideQuotes = false;
                    }
                }
                //4. else append the character to current field
                else
                {
                    sbCurrentField.Append(line[i]);
                }
            }//from for each char
            //now we got to end of string, so save the last field after checking array length
            if (currentFieldIndex >= result.Length)
            {
                //resize array
                tempArray = new string[result.Length + startsize];
                Array.Copy(result, tempArray, result.Length);
                result = tempArray;
            }
            result[currentFieldIndex] = sbCurrentField.ToString();
            currentFieldIndex++;
            sbCurrentField = new StringBuilder();
            //resize array
            tempArray = new string[currentFieldIndex];  //incremented, now represents array size
            Array.Copy(result, tempArray, currentFieldIndex);
            result = tempArray;
            //and return
            return result;
        }

        /// <summary>
        /// replaces special chars in base64 string with chars allowed in url's
        /// </summary>
        /// <param name="base64String"></param>
        /// <returns></returns>
        public static string WUrlEncodeBase64(string base64String)
        {
            //note this is standard base64url substitutions.
            // I previously used $ for = but droids cut off the $s
            //other programs still cut off the .s so may need to repad when decoding
            return base64String.Replace('+', '-').Replace('/', '_').Replace('=', '.');
        }

        /// <summary>
        /// reverses operation of WUrlEncodeBase64 back to the
        /// original base64 string
        /// </summary>
        /// <param name="wUrlEncodedBase64"></param>
        /// <returns></returns>
        public static string WUrlDecodeBase64(string wUrlEncodedBase64)
        {
            //note this is standard base64url substitutions.
            // I previously used $ for = but droids cut off the $s
            //other programs still cut off the .s so may need to repad when decoding
            return wUrlEncodedBase64.Replace('-', '+').Replace('_', '/').Replace('.', '=');
        }

        /// <summary>
        /// reverses operation of WUrlEncodeBase64 back to the
        /// original base64 string, optionally adding == pad chars
        /// if the length is not a multiple of 4
        /// </summary>
        /// <param name="wUrlEncodedBase64"></param>
        /// <param name="addPadsIfNeeded"></param>
        /// <returns></returns>
        public static string WUrlDecodeBase64(string wUrlEncodedBase64, bool addPadsIfNeeded)
        {
            string result = WUrlDecodeBase64(wUrlEncodedBase64);
            if(addPadsIfNeeded)
            {
                //should be 0,1 or 2
                int padsNeeded = (result.Length % 4 == 0)?0
                    : 4- (result.Length % 4);
                for (int i = 0; i < padsNeeded; i++)
                {
                    result += "=";
                }
            }
            return result;
        }
    }
}
