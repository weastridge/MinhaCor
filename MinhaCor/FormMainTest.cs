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
    /// <summary>
    /// Main form
    /// </summary>
    public partial class FormMainTest : Form
    {
        private bool _ignoreControlEvents = false;
        private int _trackbarMaxValue = 1000;
        private Point _currentLocation;
        /// <summary>
        /// the color that we change
        /// </summary>
        private Color _currentColor = Color.Tan;
        /// <summary>
        /// current angle of selection
        /// </summary>
        private double _currentAngle = double.MinValue;
        /// <summary>
        /// current distance of selection from center, in pixels
        /// </summary>
        private double _currentDistance = int.MinValue;
        /// <summary>
        /// smaller of ht or width of ellipse
        /// </summary>
        private int _circleRadius = int.MinValue;
        /// <summary>
        /// on axis with origin in middle of ellipse 
        /// </summary>
        private int _xPos = int.MinValue;
        private int _yPos = int.MinValue;
        //one twelfth of the circle
        private double _segment = 2 * Math.PI / 12;

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


        /// <summary>
        /// main form
        /// </summary>
        public FormMainTest()
        {
            InitializeComponent();
            panelMain.MouseWheel += PanelMain_MouseWheel;
        }

        private void PanelMain_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                labelDelta.Text = e.Delta.ToString();
                if(e.Delta < 0)
                {
                    if (trackBarLightness.Value > _trackbarMaxValue / 100)
                    {
                        trackBarLightness.Value -= _trackbarMaxValue / 100;
                    }
                }
                else
                {
                    if(trackBarLightness.Value < _trackbarMaxValue - _trackbarMaxValue/100)
                    {
                        trackBarLightness.Value += _trackbarMaxValue/ 100;
                    }
                }
                trackBarLightness_Scroll(sender, e);
            }
            catch (Exception er)
            {
                Wve.MyEr.Show(this, er, true);
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    trackBarLightness.Maximum = _trackbarMaxValue;
                    _lightness = 0.8;
                    trackBarLightness.Value = (int)Math.Floor(_trackbarMaxValue * _lightness);
                    panelMain.BackColor = Color.Transparent;
                    _currentLocation = new Point((int)Math.Floor(panelMain.Width * 0.57), 
                        (int)Math.Floor(panelMain.Height * (0.3)));
                    //now draw initial setting
                    _mouseDown = true;
                    panelMain_MouseMove("first call", new MouseEventArgs(MouseButtons,
                        int.MinValue,
                        _currentLocation.X,
                        _currentLocation.Y,
                        int.MinValue));
                    panelBackground.BackColor = _currentColor;
                    panelBackground.Invalidate();
                    _mouseDown = false;
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
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
                    if (_mouseDown)
                    {
                        //adjust lightness as much as we can to keep the final intensity the 
                        //same during mouse drags, but of course can't let it be > 100%
                        _lightness = (double)_totalIntensityAtMouseDown / (double)(_rTemp + _gTemp + _bTemp);
                        _lightness = _lightness > 1 ? 1 : _lightness;
                    }
                    // as (pure hue plus whiteness of desaturation ) adjusted for lightness
                    _currentColor = Color.FromArgb(255,
                         (int)Math.Floor((_rTemp * _lightness) > 255?255: (_rTemp * _lightness)),
                         (int)Math.Floor((_gTemp * _lightness) > 255?255: (_gTemp * _lightness)),
                         (int)Math.Floor((_bTemp * _lightness) > 255?255: (_bTemp * _lightness)));

                    //if (_mouseDown)
                    if(true)
                    {
                        panelBackground.BackColor = _currentColor; //to reduce flicker
                    }
                    //else
                    //{
                    //    panelBackground.BackColor = Color.Gray;
                    //}
                    using (Brush b = new SolidBrush(_currentColor)) //Color.FromArgb(255, 128, 128, 56)))
                    {
                        e.Graphics.FillRectangle(b,
                             new Rectangle(new Point(40, 40), new Size(
                                panelMain.Width - 80, panelMain.Height - 80)));
                        //e.Graphics.FillEllipse(b,
                        //    new Rectangle(new Point(1, 1), new Size(
                        //        panelMain.Width - 1, panelMain.Height - 1)));
                    }
                    e.Graphics.FillRectangle(Brushes.Black,
                        _currentLocation.X - 2,
                        _currentLocation.Y - 2,
                        4,
                        4);


                    //debug:
                    labelRadians.Text = _currentAngle.ToString();
                    labelR.Text = _rTemp + "->" + _currentColor.R.ToString();
                    labelG.Text = _gTemp + "->" + _currentColor.G.ToString();
                    labelB.Text = _bTemp + "->" + _currentColor.B.ToString();
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }//from if not ignoring events
        }

        private void panelMain_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                _ignoreControlEvents = true;
                _mouseDown = true;
                labelUpDown.Text = "Down";
                //panelBackground.BackgroundImage = null;
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
                    if (!(sender is Form))
                    {
                        _currentLocation = e.Location;
                    }
                    _circleRadius = panelMain.Width > panelMain.Height ? 
                        panelMain.Height/2 : 
                        panelMain.Width/2;
                    _xPos = (e.X - (panelMain.Width / 2));
                    _yPos = (-e.Y + (panelMain.Height / 2));
                    //debugging:
                    labelX.Text = _xPos.ToString();
                    labelY.Text = _yPos.ToString();


                    _currentDistance = Math.Sqrt(_xPos * _xPos + _yPos * _yPos);
                    _saturation = _currentDistance / _circleRadius > 1 ?
                        1 :
                        _currentDistance / _circleRadius;
                    //if in origon (center)
                    if (_currentDistance == 0)
                    {
                        _r = 1 / 3;
                        _g = 1 / 3;
                        _b = 1 / 3;
                    }
                    //note that a _segment is one twelfth of the circle
                    else if (_xPos >= 0 && _yPos >= 0)
                    {
                        //quadrant 1 = rt upper:  red
                        _r = 1; //the whole quadrant
                        _currentAngle = Math.Asin(
                            _yPos / _currentDistance);
                        if (_currentAngle >= _segment)
                        {
                            //red and green
                            _g = (_currentAngle - _segment) / (2 * _segment);
                            _b = 0;
                        }
                        else
                        {
                            //red and blue
                            _b = (_segment - _currentAngle) / (2 * _segment);
                            _g = 0;
                        }
                    }
                    else if (_xPos < 0 && _yPos >= 0)
                    {
                        //quadrant 2 = left upper:  green
                        _currentAngle = Math.PI - Math.Asin(
                            _yPos / _currentDistance);
                        _g = 1;  // the whole qudrant
                        if(_currentAngle < 5*_segment)
                        {
                            //red and green
                            _r = (5 * _segment - _currentAngle) /( 2 * _segment);
                            _b = 0;
                        }
                        else
                        {
                            //blue and green
                            _b = (_currentAngle - 5 * _segment) / (2 * _segment);
                            _r = 0;
                        }
                    }
                    else if (_xPos < 0 && _yPos < 0)
                    {
                        //quadrant 3 = left lower:  blue and green
                        _currentAngle = Math.PI + Math.Asin(
                            -1 * _yPos / _currentDistance);
                        if (_currentAngle < 7*_segment)
                        {
                            _g = 1;
                            _r = 0;
                            _b = 1- (7 * _segment - _currentAngle) / (2 * _segment);
                        }
                        else
                        {
                            _b = 1;
                            _r = 0;
                            _g = 1- (_currentAngle - 7 * _segment) / (2 * _segment);
                        }
                    }
                    else //(_xPos >= 0 && _yPos < 0)
                    {
                        //quadreant 4 = right lower:  Blue and red
                        _currentAngle = 2 * Math.PI - Math.Asin(
                            -1 * _yPos / _currentDistance);
                        if (_currentAngle < 11*_segment)
                        {
                            _g = 0;
                            _b = 1;
                            _r = 1 - (11 * _segment - _currentAngle) / (2 * _segment);
                        }
                        else
                        {
                            _g = 0;
                            _r = 1;
                            _b = 1 - (_currentAngle - 11 * _segment) / (2 * _segment);

                        }
                    }

                    //set if this is before the mouse was pressed...
                    if((sender is string) &&  ((string) sender == "first call"))
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
                labelUpDown.Text = "Up";
                _ignoreControlEvents = true;
                //panelBackground.BackgroundImage = global::MinhaCor.Properties.Resources.rgb;
                trackBarLightness.Value = (int)Math.Round(_lightness * _trackbarMaxValue);
                _ignoreControlEvents = false;
                panelMain.Invalidate();
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

        private void trackBarLightness_Scroll(object sender, EventArgs e)
        {
            try
            {
                _lightness = ((double)trackBarLightness.Value / (double)_trackbarMaxValue);
                panelMain.Invalidate();
            }
            catch (Exception er)
            {
                Wve.MyEr.Show(this, er, true);
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            FormMain_Load(sender, e);
        }

        private void panelBackground_Paint(object sender, PaintEventArgs e)
        {
            if (!_ignoreControlEvents)
            {
                try
                {
                    e.Graphics.FillEllipse(Brushes.Lime, new Rectangle(20, 20, 20, 20));
                    e.Graphics.FillEllipse(Brushes.Red, new Rectangle((panelBackground.Width - 40),
                        20, 20,20));
                    e.Graphics.FillEllipse(Brushes.Blue,
                        new Rectangle((int)(Math.Floor((double)panelBackground.Width / 2)),
                        panelBackground.Height - 40, 20, 20));
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }//from if not ignoring events
        }
    }
}
