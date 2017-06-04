using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaCor
{
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
        public int[] RgbValue = new int[] { 0, 0, 0 };
        /// <summary>
        /// optional details
        /// </summary>
        public string Details = string.Empty;
        /// <summary>
        /// when color creation created
        /// </summary>
        public DateTime WhenCreated = DateTime.MinValue;
        /// <summary>
        /// when last edited
        /// </summary>
        public DateTime WhenLastEdited = DateTime.MinValue;

        #region constructors
        public ColorCreation(string colorName, 
            string personName, 
            int[] rgbValue, 
            string details, 
            DateTime whenCreated, 
            DateTime whenLastEdited)
        {
            ColorName = colorName;
            PersonName = personName;
            RgbValue = rgbValue;
            Details = details;
            WhenCreated = whenCreated;
            WhenLastEdited = whenLastEdited;
        }
        #endregion constructors
    }
}
