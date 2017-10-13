using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinhaCor
{
    public partial class SplashForSkin2 : Form
    {
        private bool instruct1Visible = true;
        private bool instruct2Visible = false;
        private bool instruct3Visible = false;
        private int timerCount = 0;
        private Pen penForIllustrations = new Pen(Color.Yellow, 5);
        public SplashForSkin2()
        {
            InitializeComponent();
        }

        private void SplashForSkin2_Load(object sender, EventArgs e)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    this.Text = "Instructions";
                    panel1.BackgroundImage = SetImageOpacity(
                        MinhaCor.Properties.Resources.Screenshot, 
                        0.50F);
                    label1.Text = MainClass.MinhaCorResourceManager.GetString(
                        "StringInstruct1");
                    label2.Text = MainClass.MinhaCorResourceManager.GetString(
                        "StringInstruct2");
                    label3.Text = MainClass.MinhaCorResourceManager.GetString(
                        "StringInstruct3");
                    label4.Text = MainClass.MinhaCorResourceManager.GetString(
                        "StringInstruct4");
                    timer1.Interval = 2000;
                    timer1.Start();
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }


        // https//stackoverflow.com/questions/23114282/are-we-able-to-set-opacity-of-the-background-image-of-a-panel

        public Image SetImageOpacity(Image image, float opacity)
        {
            Bitmap bmp = new Bitmap(image.Width, image.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                ColorMatrix matrix = new ColorMatrix();
                matrix.Matrix33 = opacity;
                ImageAttributes attributes = new ImageAttributes();
                attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default,
                                                  ColorAdjustType.Bitmap);
                g.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height),
                                   0, 0, image.Width, image.Height,
                                   GraphicsUnit.Pixel, attributes);
            }
            return bmp;
        }

        
        private void buttonGo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if(instruct1Visible)
            {
                e.Graphics.DrawRectangle(penForIllustrations,
                    new Rectangle(2, 35, 55, 80));
            }
            if(instruct2Visible)
            {
                e.Graphics.DrawLine(penForIllustrations,
                    88, 50, 88, 335);
            }
            if(instruct3Visible)
            {
                e.Graphics.DrawEllipse(penForIllustrations,
                    469, 204, 30, 30);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(timerCount < 2)
                {
                timerCount++;
                //if (timerCount == 0)
                //{
                //    label1.Visible = true;
                //    instruct1Visible = true;
                //}
                if (timerCount == 1)
                {
                    label2.Visible = true;
                    instruct2Visible = true;
                }
                else if(timerCount == 2)
                {
                    label3.Visible = true;
                    instruct3Visible = true;
                }
                panel1.Invalidate();
            }
            else
            {
                timer1.Stop();
            }
        }
    }
}
