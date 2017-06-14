using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinhaCor
{
    public partial class FormHelp : Form
    {
        /// <summary>
        /// hellp
        /// </summary>
        public FormHelp()
        {
            InitializeComponent();
        }

        private void FormHelp_Load(object sender, EventArgs e)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    if(MainClass.EditMode == MainClass.MinhaCorEditMode.fullColor)
                    {
                        panel1.BackgroundImage = MinhaCor.Properties.Resources.illustration;
                    }
                    else
                    {
                        panel1.BackgroundImage = MinhaCor.Properties.Resources.simpleIllustration;
                    }
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
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
    }
}
