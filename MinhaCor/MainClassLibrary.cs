using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaCor
{
    /// <summary>
    /// just exposes a method called start that can be called
    /// when a display is  shown each time
    /// </summary>
    public interface IStartable
    {
        void Start();
    }
    /// <summary>
    /// a color and name someone has created
    /// </summary>
    public class ColorCreation
    {
        /// <summary>
        /// the name someone gave the color
        /// </summary>
        public string ColorName = string.Empty;
        /// <summary>
        /// who named it
        /// </summary>
        public string PersonName = string.Empty;
        /// <summary>
        /// red, green and blue , 0 to 255
        /// </summary>
        public byte[] RgbValue = new byte[] { 0, 0, 0 };
        /// <summary>
        /// optional details
        /// </summary>
        public string Details = string.Empty;
        /// <summary>
        /// when color creation created
        /// </summary>
        public DateTime WhenCreated = DateTime.MinValue;
        /// <summary>
        /// when x-d out 
        /// </summary>
        public DateTime WhenXdOut = DateTime.MinValue;
        /// <summary>
        /// location of this instance of MinhaCor, specified in settings
        /// </summary>
        public string Location = "unspecified";
        /// <summary>
        /// optional tag
        /// </summary>
        public string Tag = string.Empty;

        #region constructors
        public ColorCreation(string colorName, 
            string personName, 
            byte[] rgbValue, 
            string details, 
            DateTime whenCreated, 
            DateTime whenLastEdited,
            string location,
            string tag)
        {
            ColorName = colorName;
            PersonName = personName;
            RgbValue = rgbValue;
            Details = details;
            WhenCreated = whenCreated;
            WhenXdOut = whenLastEdited;
            if (!string.IsNullOrEmpty(location))
            {
                Location = location;
            }
            if (tag != null)
            {
                Tag = tag;
            }
        }
        #endregion constructors

        /// <summary>
        /// this ColorCreation as a comma separated value
        /// </summary>
        /// <returns></returns>
        public string ToCsv()
        {
            return Wve.WveTools.WriteCsv(new string[]
            {
                ColorName,
                PersonName,
                Wve.WveTools.BytesToHex(RgbValue,string.Empty),
                Details,
                WhenCreated.ToString("o"), //iso 8601 format
                WhenXdOut.ToString("o"),
                Location,
                Tag
            },
            false); //false to append last comma
        }
        
        /// <summary>
        /// make ColorCreation from comma separated value serialization
        /// </summary>
        /// <param name="csv"></param>
        /// <returns></returns>
        public static ColorCreation FromCsv(string csv)
        {
            string[] parts = Wve.WveTools.ReadCsvLine(csv);
            DateTime saved;
            DateTime edited;
            DateTime.TryParse(parts[4], out saved);
            DateTime.TryParse(parts[5], out edited);
            string location = parts.Length < 7 ? null : parts[6];
            string tag = parts.Length < 8 ? null : parts[7];
            return new ColorCreation(
                parts[0],
                parts[1],
                Wve.WveTools.HexStringToByteArray(parts[2]),
                parts[3],
                saved,
                edited,
                location,
                tag);
        }
    }
}
