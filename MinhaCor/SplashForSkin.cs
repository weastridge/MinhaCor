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
    public partial class SplashForSkin : Form
    {
        private int _screenCount = 0;
        private Image[] _images = new Image[5];
        public SplashForSkin()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            DialogResult = DialogResult.OK;
        }

        private void SplashForSkin_Load(object sender, EventArgs e)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    //for now just use the english versions but we have international generics too
                    _images[0] = MinhaCor.Properties.Resources.Help0_en;
                    _images[1] = MinhaCor.Properties.Resources.Help1_en;
                    _images[2] = MinhaCor.Properties.Resources.Help2_en;
                    _images[3] = MinhaCor.Properties.Resources.Help3_en;
                    _images[4] = MinhaCor.Properties.Resources.Help4_en;
                    this.BackgroundImage = _images[0];
                    this.Text = "How to make your skin color";
                    timer1.Interval = 1000; //milliseconds
                    timer1.Start();
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    _screenCount++;
                    if (_screenCount > 4)
                    {
                        timer1.Stop();
                    }
                    else
                    {
                        this.BackgroundImage = _images[_screenCount];
                    }
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }

        private void SplashForSkin_Click(object sender, EventArgs e)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    buttonOK_Click(sender, e);
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }
    }
}
