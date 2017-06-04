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
            //e.Graphics.FillRectangle(Brushes.Red,
            //    new Rectangle(0, 0, _swatchWidth, _swatchHeight));

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
