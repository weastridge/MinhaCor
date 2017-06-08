using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinhaCor
{
    public partial class FormAbout : Form
    {
        /// <summary>
        /// about
        /// </summary>
        public FormAbout()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    this.Close();
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }

        private void About_Load(object sender, EventArgs e)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    //intro
                    StringBuilder sb = new StringBuilder();
                    
                    sb.Append("MinhaCor (MyColor) is a fun way to show that our own " +
                        "skin color is unique, yet part of a wonderful family of colors.");
                    textBox1.Text = sb.ToString();



                    //culture

                    ResourceManager rm = new ResourceManager("MinhaCor.resources", typeof(Program).Assembly);
                    textBox1.Text += "\r\n" + rm.GetString("StringHello");
                    labelCulture.Text = "Current Culture is " + 
                        System.Globalization.CultureInfo.CurrentCulture.DisplayName;
                    labelUICulture.Text = "Current User Interface Culture is " + 
                        System.Globalization.CultureInfo.CurrentUICulture.DisplayName;
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }
    }
}
