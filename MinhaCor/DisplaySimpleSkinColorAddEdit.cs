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
    /// edit and name color
    /// </summary>
    public partial class DisplaySimpleSkinColorAddEdit : UserControl
    {

        private bool _ignoreControlEvents = false;
        private int _trackbarMaxValue = 1000;
        private Point _currentLocation;
        /// <summary>
        /// the color that we change
        /// </summary>
        private Color _currentColor = Color.Tan;
        ///// <summary>
        ///// current angle of selection
        ///// </summary>
        //private double _currentAngle = double.MinValue;
        ///// <summary>
        ///// current distance of selection from center, in pixels
        ///// </summary>
        ////private double _currentDistance = int.MinValue;
        ///// <summary>
        ///// smaller of ht or width of ellipse
        ///// </summary>
        //private int _circleRadius = int.MinValue;
        ///// <summary>
        ///// on axis with origin in middle of ellipse 
        ///// </summary>
        //private int _xPos = int.MinValue;
        //private int _yPos = int.MinValue;
        ////one twelfth of the circle
        //private double _segment = 2 * Math.PI / 12;

        //we define the color in terms of:
        // 1. color tone in terms of red, green and blue as if fully saturated
        // 2. saturation, i.e. inverse of how much gray is addded to the orig rgb tone
        // 3. lightness, i.e. the sum of the combined r,g,b values

        /// <summary>
        /// red component as if fully saturated, 0 to 1
        /// </summary>
        private double _r;
        /// <summary>
        /// green component as if fully saturated, 0 to 1
        /// </summary>
        private double _g;
        /// <summary>
        /// blue component as if fully saturated, 0 to 1
        /// </summary>
        private double _b;
        //these next three are used in Paint()
        private double _rTemp;
        private double _gTemp;
        private double _bTemp;
        /// <summary>
        /// the saturation of the color, 0 to 1
        /// </summary>
        private double _saturation = 1;
        /// <summary>
        /// adjustment to brightness of the color, 0 to 1
        /// </summary>
        private double _lightness = 1;
        /// <summary>
        /// true if mouse down within panelMain, until released
        /// </summary>
        private bool _mouseDown = false;
        /// <summary>
        /// r+g+b of _currentColor
        /// </summary>
        private int _totalIntensityAtMouseDown;

        #region constructor
        /// <summary>
        /// control for creating and naming color
        /// </summary>
        public DisplaySimpleSkinColorAddEdit()
        {
            InitializeComponent();
            panelMain.MouseWheel += PanelMain_MouseWheel;
            
        }
        #endregion constructor

        #region methods

        private void DisplayColorAddEdit_Load(object sender, EventArgs e)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    setDefaults();
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }

        }

        internal void Reset()
        {
            setDefaults();
            textBoxColorName.Clear();
            textBoxPersonName.Clear();
        }

        private void setDisplay(Tuple<double,double,double> t) 
        {
            double saturation = t.Item1;
            double x = t.Item2;
            double y = t.Item3;
            trackBarSaturation.Maximum = _trackbarMaxValue;
            _saturation = saturation;
            trackBarSaturation.Value = (int)Math.Floor(_trackbarMaxValue * (1 - _saturation));
            panelMain.BackColor = Color.Transparent;
            _currentLocation = new Point((int)Math.Floor(panelMain.Width * x),
                (int)Math.Floor(panelMain.Height * (y)));
            //now draw initial setting
            _mouseDown = true;
            panelMain_MouseMove("first call", new MouseEventArgs(MouseButtons,
                int.MinValue,
                _currentLocation.X,
                _currentLocation.Y,
                int.MinValue));
            panelBackground.BackColor = _currentColor;
            panelBackground.Invalidate();
            panelSwatch.Invalidate();
            _mouseDown = false;
        }

        private void setDefaults()
        {
            panelLightLight.BackColor = Color.FromArgb(0xF7, 0xE7, 0xC7);
            panelLight.BackColor = Color.FromArgb(0xEA, 0xCE, 0xA4);
            panelMedium.BackColor = Color.FromArgb(0x6C, 0x53, 0x3D);
            panelDark.BackColor = Color.FromArgb(0x2E, 0x1B, 0x0C);
            setDisplay(new Tuple<double, double, double>(0.30, 0.70, 0.08));
            //trackBarSaturation.Maximum = _trackbarMaxValue;
            //_saturation = 0.30;
            //trackBarSaturation.Value = (int)Math.Floor(_trackbarMaxValue * (1-_saturation));
            //panelMain.BackColor = Color.Transparent;
            //_currentLocation = new Point((int)Math.Floor(panelMain.Width * 0.70),
            //    (int)Math.Floor(panelMain.Height * (0.08)));
            ////now draw initial setting
            //_mouseDown = true;
            //panelMain_MouseMove("first call", new MouseEventArgs(MouseButtons,
            //    int.MinValue,
            //    _currentLocation.X,
            //    _currentLocation.Y,
            //    int.MinValue));
            //panelBackground.BackColor = _currentColor;
            //panelBackground.Invalidate();
            //panelSwatch.Invalidate();
            //_mouseDown = false;
        }

        private Tuple<double,double, double> colorToLocation(Color c)
        {
            //amount of white added to color
            double saturation;
            //amount of red
            double x;
            //amount of dark = (1-lightness)
            double y;
            if (c.R == c.G) 
            {
                if (c.R == 0) //then rgb is black
                {
                    y = 1;
                    x = 0; //can be anything
                    saturation = 1;
                }
                else
                {
                    //blue part all comes from saturation
                    saturation = 1 - ((double)c.B / c.R);
                    //lightness is r divided by what it could be
                    //y inv of lightness
                    y = 1 - ((double)c.R / 256);
                    x = 0.5;
                }
            }
            else if(c.R > c.G) //then r > b too
            {
                //blue part all comes from saturation
                saturation = 1 - ((double)c.B / c.R);
                //lightness is r divided by what it could be
                //y inv of lightness
                y = 1- ((double)c.R / 256 );
                //x is inverse of amount of green decreasing from 1 at half width to 0 by full width
                x = 0.5 + (0.5 * (1 - (((double)c.G - c.B) / ((double)c.R - c.B))));
            }
            else //then (g > r) and (g > b)
            {
                //blue part all comes from saturation
                saturation = 1 - ((double)c.B / c.G);
                //lightness is g divided by what it could be
                //y inv of lightness
                y = 1 - ((double)c.G  / 256);
                //x is measure of amount of red up to 100% by half width of panel
                x = 0.5 * (((double)c.R - c.B) / ((double)c.G - c.B));
            }
            return new Tuple<double, double, double>(saturation, x, y);
        }


        private void PanelMain_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                //labelDelta.Text = e.Delta.ToString();
                //adjust 1% per delta
                if (e.Delta < 0)
                {
                    if (trackBarSaturation.Value > _trackbarMaxValue / 100)
                    {
                        trackBarSaturation.Value -= _trackbarMaxValue / 100;
                    }
                }
                else
                {
                    if (trackBarSaturation.Value < _trackbarMaxValue - _trackbarMaxValue / 100)
                    {
                        trackBarSaturation.Value += _trackbarMaxValue / 100;
                    }
                }
                trackBarSaturation_Scroll(sender, e);
            }
            catch (Exception er)
            {
                Wve.MyEr.Show(this, er, true);
            }
        }


        private void panelMain_Paint(object sender, PaintEventArgs e)
        {
            if (!_ignoreControlEvents)
            {
                try
                {
                    //define color
                    //but first calculate the intensity of the color compared to _totalIntensityAtMousedown
                    // and ajust _lightness accordingly  
                    _rTemp = ((_r * _saturation) + (1 - _saturation)) * 255;
                    _gTemp = ((_g * _saturation) + (1 - _saturation)) * 255;
                    _bTemp = ((_b * _saturation) + (1 - _saturation)) * 255;
                    //check for rounding errors
                    if(_rTemp < 0)
                    {
                        _rTemp = 0;
                    }
                    if(_gTemp < 0)
                    {
                        _gTemp = 0;
                    }
                    if(_bTemp < 0)
                    {
                        _bTemp = 0;
                    }
                    //if (_mouseDown)
                    //{
                    //    //adjust lightness as much as we can to keep the final intensity the 
                    //    //same during mouse drags, but of course can't let it be > 100%
                    //    _lightness = (double)_totalIntensityAtMouseDown / (double)(_rTemp + _gTemp + _bTemp);
                    //    _lightness = _lightness > 1 ? 1 : _lightness;
                    //}
                    // as (pure hue plus whiteness of desaturation ) adjusted for lightness
                    _currentColor = Color.FromArgb(255,
                         (int)Math.Floor((_rTemp * _lightness) > 255 ? 255 : (_rTemp * _lightness)),
                         (int)Math.Floor((_gTemp * _lightness) > 255 ? 255 : (_gTemp * _lightness)),
                         (int)Math.Floor((_bTemp * _lightness) > 255 ? 255 : (_bTemp * _lightness)));

                    //if (_mouseDown)
                    if (true)
                    {
                        panelBackground.BackColor = _currentColor; //to reduce flicker
                    }
                    using (Brush b = new SolidBrush(_currentColor)) //Color.FromArgb(255, 128, 128, 56)))
                    {
                        e.Graphics.FillRectangle(b,
                             new Rectangle(new Point(40, 40), new Size(
                                panelMain.Width - 80, panelMain.Height - 80)));
                    }
                    e.Graphics.FillRectangle(Brushes.Black,
                        _currentLocation.X - 4,
                        _currentLocation.Y - 4,
                        8,
                        8);

                    //mark center
                    e.Graphics.FillRectangle(Brushes.White,
                        panelMain.Width / 2, panelMain.Height / 2, 1, 1);

                    //show rgb
                    labelrgb.Text = Wve.WveTools.BytesToHex(new byte[] {
                    _currentColor.R,
                    _currentColor.G,
                    _currentColor.B},
                    " ");
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }//from if not ignoring events
        }


        private void panelBackground_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelMain_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                _ignoreControlEvents = true;
                _mouseDown = true;
                //labelUpDown.Text = "Down";
                panelBackground.BackgroundImage = null;
                this.Cursor = new Cursor(Cursor.Current.Handle);
                Cursor.Position = panelMain.PointToScreen(_currentLocation);
                _totalIntensityAtMouseDown = _currentColor.R + _currentColor.G + _currentColor.B;
                _ignoreControlEvents = false;
            }
            catch (Exception er)
            {
                Wve.MyEr.Show(this, er, true);
            }
        }

        private void panelMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mouseDown)
            {
                try
                {
                    //don't assign currentLocation if sender is "first call" or if mouse is out of bounds
                    if ((sender is Panel) &&
                        ((e.Y >= 0) && (e.Y <= panelMain.Height)) &&
                        ((e.X >= 0) && (e.X <= panelMain.Width )))
                    {
                        _currentLocation = e.Location;
                    }
                    //lightness
                    if ((e.Y >= 0) && (e.Y <= panelMain.Height))
                    {
                        _lightness = ((double)panelMain.Height - e.Y) / panelMain.Height;
                    }
                    //hue
                    if ((e.X >= 0) && (e.X <= panelMain.Width/2))
                    {
                        _r = (double)e.X / (panelMain.Width / 2);
                        _g = 1; 
                        _b = 0;
                    }
                    else if((e.X >panelMain.Width/2) && (e.X <= panelMain.Width))
                    {
                        _r = 1;
                        _g = 1 - (((double)e.X - panelMain.Width / 2) / (panelMain.Width / 2));
                        _b = 0;
                    }


                    //set if this is before the mouse was pressed...
                    if ((sender is string) && ((string)sender == "first call"))
                    {
                        _totalIntensityAtMouseDown = _currentColor.R + _currentColor.G + _currentColor.B;
                    }
                    panelMain.Invalidate();
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }

        private void panelMain_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                _mouseDown = false;
                //labelUpDown.Text = "Up";
                _ignoreControlEvents = true;
                //panelBackground.BackgroundImage = global::MinhaCor.Properties.Resources.rgb;
                trackBarSaturation.Value = (int)Math.Round((1-_saturation) * _trackbarMaxValue);
                _ignoreControlEvents = false;
                panelMain.Invalidate();
                panelSwatch.Invalidate();
            }
            catch (Exception er)
            {
                Wve.MyEr.Show(this, er, true);
            }
        }

        private void panelMain_Resize(object sender, EventArgs e)
        {
            try
            {
                panelMain.Invalidate();
            }
            catch (Exception er)
            {
                Wve.MyEr.Show(this, er, true);
            }
        }

        private void trackBarSaturation_Scroll(object sender, EventArgs e)
        {
            try
            {
                _saturation = 1-((double)trackBarSaturation.Value / (double)_trackbarMaxValue);
                panelMain.Invalidate();
                panelSwatch.Invalidate();
            }
            catch (Exception er)
            {
                Wve.MyEr.Show(this, er, true);
            }
        }

        //reset to original color etc
        private void buttonReset_Click(object sender, EventArgs e)
        {
            try
            {
                setDefaults();
            }
            catch (Exception er)
            {
                Wve.MyEr.Show(this, er, true);
            }
        }

        #endregion methods

        private void panelSwatch_Paint(object sender, PaintEventArgs e)
        {
            panelSwatch.BackColor = _currentColor;
        }

        private void panelMain_Resize_1(object sender, EventArgs e)
        {
            if (!_ignoreControlEvents)
            {
                try
                {
                    setDefaults();
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }//from if not ignoring events
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    //validate
                    if(String.IsNullOrWhiteSpace(textBoxColorName.Text))
                    {
                        MessageBox.Show("Please name your color, or else click cancel.");
                        return;
                    }
                    //save
                    byte[] colorBytes = new byte[]
                    {
                        _currentColor.R,
                        _currentColor.G,
                        _currentColor.B
                    };
                    string location = MainClass.ConfigSettings.GetValue("Location");
                    ColorCreation cc = new ColorCreation(
                        textBoxColorName.Text,
                        textBoxPersonName.Text,
                        colorBytes,
                        string.Empty, //details
                        DateTime.Now,
                        DateTime.MinValue,
                        location,
                        string.Empty);
                    MainClass.ColorCreations.Insert(0, cc);
                    MainClass.SaveColorCreations();
                    FormMinhaCor.Instance.LoadDisplayGrid();
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    FormMinhaCor.Instance.LoadDisplayGrid();
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }

        private void panelExample_Click(object sender, EventArgs e)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    if(sender is Panel)
                    {
                        setDisplay(colorToLocation(((Panel)sender).BackColor));
                        //setDisplay(new Tuple<double, double, double>(1, 1, 0));
                    }
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }

        private void panelControls_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                e.Graphics.FillRectangle(Brushes.DodgerBlue, 
                    new Rectangle(10, labelAdjustHue.Location.Y, 12, 12));
            }
            catch (Exception er)
            {
                Wve.MyEr.Show(this, er, true);
            }
        }
    }
}
