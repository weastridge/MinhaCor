using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
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
                    //StringBuilder sb = new StringBuilder();
                    //sb.Append("MinhaCor (MyColor) is a fun way to show that our own " +
                    //    "skin color is unique, yet part of a wonderful family of colors.");
                    textBoxIntro.Text = MainClass.MinhaCorResourceManager.GetString(
                        "StringAbout");

                    //details
                    StringBuilder sb = new StringBuilder();

                    //show assembly version
                    Assembly asm = Assembly.GetAssembly(this.GetType());
                    StringBuilder sbVersion = new StringBuilder();
                    //sbVersion.Append("Version: ");
                    //sbVersion.Append(System.Reflection.Assembly.GetEntryAssembly()
                    //.GetName().Version.ToString());
                    //sbVersion.Append("  Release:1   (mm3 ");
                    sbVersion.Append(asm.GetName().Version.ToString());
                    //sbVersion.Append(")");
                    sb.Append("MinhaCor version: ");
                    sb.Append(sbVersion.ToString());
                    DateTime buildDate = DateTime.Parse("2000/01/01").AddDays(
                        asm.GetName().Version.Build);
                    DateTime buildTime = DateTime.Today.AddSeconds(
                        asm.GetName().Version.Revision * 2);
                    if ((asm.GetName().Version.Build != 0) &&
                        (buildDate < DateTime.Parse("2100/01/01")))
                    {
                        sb.Append(Environment.NewLine);
                        sb.Append("Built on ");
                        sb.Append(buildDate.ToLongDateString());
                        sb.Append(" at ");
                        sb.Append(buildTime.ToShortTimeString());
                        sb.Append(" Standard Time");
                    }
                    //get application directory
                    sb.Append(Environment.NewLine);
                    sb.Append("Application base directory: ");
                    sb.Append(AppDomain.CurrentDomain.BaseDirectory);

                    //get public key token
                    sb.Append(Environment.NewLine);
                    AssemblyName aName = asm.GetName();
                    byte[] tokenBytes = aName.GetPublicKeyToken();
                    sb.Append("Public Key Token: ");
                    sb.Append(Wve.WveTools.BytesToHex(tokenBytes, string.Empty));


                    sb.Append("\r\nWritten by Wesley Eastridge, 2017 and free for all to use.  " +
                        "Source code is open source under the MIT license.");
                    textBoxDetails.Text = sb.ToString();


                    //culture
                    //;ResourceManager rm = new ResourceManager("MinhaCor.resources", typeof(Program).Assembly);
                    //;textBoxIntro.Text += "\r\n" + MainClass.MinhaCorResourceManager.GetString("StringHello");
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

        private void linkLabelMinhaCor_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    System.Diagnostics.Process.Start("https://eastridges.com/minhacor");
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }

        private void linkLabelHumanae_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    System.Diagnostics.Process.Start("http://www.angelicadass.com/humanae-work-in-progress/");
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }


        private void linkLabelAngelica_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    System.Diagnostics.Process.Start("https://www.ted.com/speakers/angelica_dass");
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }
    }
}
