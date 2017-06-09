using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace MinhaCor
{
    /// <summary>
    /// the main program class
    /// </summary>
    public static class MainClass
    {
        private static string _colorCreationsLocalFileName = "ColorCreations.csv";
        private static string _colorCreationsPathAndFilename;
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
        /// <summary>
        /// Name of culture to set the thread to for this program per config settings
        /// </summary>
        public static string DefaultCultureName
        {
            get
            {
                _defaultCultureName = ConfigSettings.GetValue("DefaultCulture");
                return _defaultCultureName;
            }

            set
            {
                _defaultCultureName = value;
                ConfigSettings.SetValue("DefaultCulture", _defaultCultureName);
            }
        }

        /// <summary>
        /// ResourceManager for the project, such as for culture-specific resources
        /// </summary>
        public static ResourceManager MinhaCorResourceManager
        {
            get
            {
                return _minhaCorResourceManager;
            }

            set
            {
                _minhaCorResourceManager = value;
            }
        }

        /// <summary>
        /// the working list of ColorCreations
        /// </summary>
        public static List<ColorCreation> ColorCreations;

        private static string _defaultCultureName = null;

        private static System.Resources.ResourceManager _minhaCorResourceManager;


        /// <summary>
        /// initialize the MainClass
        /// </summary>
        public static void Initialize()
        {
            MainClass.ConfigSettings = new Wve.MyConfigSettings(true); //to locate in this folder
            //make defaults if none exist
            if (MainClass.ConfigSettings.GetMyConfigList().Count == 0)
            {
                MainClass.ConfigSettings.SetValue("Rows", "4");
                MainClass.ConfigSettings.SetValue("Columns", "6");
                MainClass.ConfigSettings.SetValue("Culture", "default");
                MainClass.ConfigSettings.SetValue("Password", string.Empty);
            }

            //set current culture
            if((!string.IsNullOrWhiteSpace(MainClass.DefaultCultureName)) &&
                (DefaultCultureName != "default"))
            {
                System.Globalization.CultureInfo.CurrentCulture = 
                    new System.Globalization.CultureInfo(MainClass.DefaultCultureName);
                System.Globalization.CultureInfo.CurrentUICulture = 
                    new System.Globalization.CultureInfo(MainClass.DefaultCultureName);
            }
            MinhaCorResourceManager = new ResourceManager(
                "MinhaCor.resources", 
                typeof(Program).Assembly);

            StringBuilder sb = new StringBuilder();
            sb.Append(System.IO.Directory.GetCurrentDirectory());
            sb.Append(@"\");
            //doesnt work:  sb.Append(System.IO.Path.PathSeparator);
            sb.Append(_colorCreationsLocalFileName);
            _colorCreationsPathAndFilename = sb.ToString();
        }


        public static void SaveColorCreations()
        {
            SaveColorCreations(_colorCreationsPathAndFilename);
        }

        public static void SaveColorCreations(string path)
        {
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(path))
            {
                for (int i = 0; i < ColorCreations.Count; i++)
                {
                    sw.WriteLine(ColorCreations[i].ToCsv());
                }
                sw.Flush();
            }//from using sw
        }

        public static void LoadColorCreations()
        {
            LoadColorCreations(_colorCreationsPathAndFilename);
        }

        /// <summary>
        /// load given file into list of color creations
        /// </summary>
        /// <param name="path"></param>
        public static void LoadColorCreations(string path)
        {
            ColorCreation cc;
            ColorCreations = new List<ColorCreation>();
            if (System.IO.File.Exists(path))
            {
                using (System.IO.StreamReader sr =
                    new System.IO.StreamReader(
                        System.IO.File.OpenRead(path)))
                {
                    string line;
                    while (!string.IsNullOrWhiteSpace(line = sr.ReadLine()))
                    {
                        cc = ColorCreation.FromCsv(line);
                        if (cc.RgbValue.Length > 2) //can't accept if no color
                        {
                            ColorCreations.Add(ColorCreation.FromCsv(line));
                        }
                    }
                }//from using sw
            }
        }

        /// <summary>
        /// this is the default culture info determined at program statup in the Program startup
        /// </summary>
        public static System.Globalization.CultureInfo StartUpCulture;
        /// <summary>
        /// default ui culture info determined at program startup in the program setup
        /// </summary>
        public static System.Globalization.CultureInfo StartUpUICulture;
    }
}
