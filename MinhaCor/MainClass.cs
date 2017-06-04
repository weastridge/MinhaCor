using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhaCor
{
    /// <summary>
    /// the main program class
    /// </summary>
    public static class MainClass
    {
        /// <summary>
        /// configuration settings
        /// </summary>
        public static Wve.MyConfigSettings ConfigSettings;
        private static int _rows = int.MinValue;
        /// <summary>
        /// number of rows
        /// </summary>
        public static int Rows
        {
            get
            {
                if (_rows == int.MinValue) //if first time called
                {
                    if (!int.TryParse(ConfigSettings.GetValue("Rows"), out _rows))
                    {
                        _rows = 4; //default
                    }
                }
                return _rows;
            }
            set
            {
                _rows = value;
                ConfigSettings.SetValue("Rows", _rows.ToString());
            }
        }

        private static int _columns = int.MinValue;
        /// <summary>
        /// number of columns
        /// </summary>
        public static int Columns
        {
            get
            {
                if (_columns == int.MinValue) //if first time called
                {
                    if (!int.TryParse(ConfigSettings.GetValue("Columns"), out _columns))
                    {
                        _columns = 6; //default
                    }
                }
                return _columns;
            }
            set
            {
                _columns = value;
                ConfigSettings.SetValue("Columns", _columns.ToString());
            }
        }
        public static void Initialize()
        {
            MainClass.ConfigSettings = new Wve.MyConfigSettings(true); //to locate in this folder
            //make defaults if none exist
            if (MainClass.ConfigSettings.GetMyConfigList().Count == 0)
            {
                MainClass.ConfigSettings.SetValue("Rows", "4");
                MainClass.ConfigSettings.SetValue("Columns", "6");
                MainClass.ConfigSettings.SetValue("Culture", "en-US");
                MainClass.ConfigSettings.SetValue("Password", string.Empty);
            }
        }
    }
}
