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
        public static Wve.MyConfigSettings ConfigSettings = 
            new Wve.MyConfigSettings(true);

        public static void Initialize()
        {
            //make defaults if none exist
            if (MainClass.ConfigSettings.GetMyConfigList().Count == 0)
            {
                MainClass.ConfigSettings = new Wve.MyConfigSettings();
                MainClass.ConfigSettings.SetValue("Rows", "4");
                MainClass.ConfigSettings.SetValue("Columns", "6");
                MainClass.ConfigSettings.SetValue("Culture", "en-US");
                MainClass.ConfigSettings.SetValue("Password", string.Empty);
            }
        }
    }
}
