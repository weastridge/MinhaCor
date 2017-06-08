using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinhaCor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //set default culture before any changes are made
            try
            {
                MainClass.StartUpCulture = System.Globalization.CultureInfo.CurrentCulture;
                MainClass.StartUpUICulture = System.Globalization.CultureInfo.CurrentUICulture;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.ToString());
            }
            Application.Run(new FormMinhaCor());
        }
    }
}
