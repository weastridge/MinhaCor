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
    }
}
