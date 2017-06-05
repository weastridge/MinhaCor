using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinhaCor
{
    /// <summary>
    /// display group of color creations
    /// </summary>
    public partial class DisplayGrid : UserControl
    {
        private int _swatchHeight;
        private int _swatchWidth;
        private int _colorCreationsStartingIndex = 0;
        /// <summary>
        /// display group of ColorCreations
        /// </summary>
        public DisplayGrid()
        {
            InitializeComponent();
        }

        internal void DrawGrid()
        {
            panelGridDisplay.Controls.Clear();
            Panel panelColors = new Panel();
            panelColors.Size = new Size(_swatchWidth, _swatchHeight);
            panelColors.BackgroundImage = MinhaCor.Properties.Resources.rgb;
            panelColors.BackgroundImageLayout = ImageLayout.Stretch;
            Panel panelGo = new Panel();
            panelGo.Size = new Size(_swatchWidth, _swatchHeight);
            panelGo.BackColor = Color.Transparent;
            panelGo.Dock = DockStyle.Fill;
            panelGo.BackgroundImage = MinhaCor.Properties.Resources.start;
            panelGo.BackgroundImageLayout = ImageLayout.Zoom;
            panelColors.Controls.Add(panelGo);
            panelGridDisplay.Controls.Add(panelColors);
            panelGo.Click += new System.EventHandler(this.panelGridDisplay_Click);



        }

        private void DisplayGrid_Load(object sender, EventArgs e)
        {
            setMeasurements();
        }

        private void setMeasurements()
        {
            _swatchWidth = (int)Math.Floor(
                (double)panelGridDisplay.Width / (double)MainClass.Columns);
            _swatchHeight = (int)Math.Floor(
                (double)panelGridDisplay.Height / (double)MainClass.Rows);
        }

        private void panelGridDisplay_Paint(object sender, PaintEventArgs e)
        {
            Point location;
            Brush brush;
            ColorCreation cc;
            int colorCreationIndex;
            //load swatches
            for (int r = 0; r < MainClass.Rows; r++)
            {
                for (int c = 0; c < MainClass.Columns; c++)
                {
                    //skip first one which is the start buttton 
                    if ((r != 0) || (c != 0))
                    {
                        colorCreationIndex = _colorCreationsStartingIndex +
                            (r * MainClass.Columns) + c - 1; //because first spot is for start button
                        if (colorCreationIndex < MainClass.ColorCreations.Count)
                        {
                            cc = MainClass.ColorCreations[colorCreationIndex];
                            location = new Point(
                                (panelGridDisplay.Width/MainClass.Columns) * c,
                                (panelGridDisplay.Height/MainClass.Rows) * r);
                            byte[] argb = new byte[4];
                            //reverse endian!
                            argb[3] = 255;
                            argb[2] = cc.RgbValue[0];
                            argb[1] = cc.RgbValue[1];
                            argb[0] = cc.RgbValue[2];
                            brush = new SolidBrush(Color.FromArgb(BitConverter.ToInt32(argb, 0)));
                            e.Graphics.FillRectangle(brush,
                                new Rectangle(location, new Size(_swatchWidth, _swatchHeight)));
                        }//from if that many color creations exist
                    }//from skip first spot
                }//from for each column
            }//from for each row

        }


        private void panelGridDisplay_Click(object sender, EventArgs e)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    FormMinhaCor.Instance.LoadDisplayColorAddEdit();
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }

        private void panelGridDisplay_Resize(object sender, EventArgs e)
        {
            setMeasurements();
            DrawGrid();
        }
    }
}
