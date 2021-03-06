﻿using System;
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
    public partial class DisplaySkinColorAddEdit : UserControl, IStartable
    {

        private bool _ignoreControlEvents = false;
        //private int _trackbarMaxValue = 999; //1000 possible values
        private Point _currentLocation = new Point(0,0);
        private Point _startingLocation = new Point(0, 0);
        /// <summary>
        /// the color that we change
        /// </summary>
        private Color _currentColor = Color.Tan;
        //should match _fontForLabel in FormMinhaCor
        private Font _fontForLabel = new Font(FontFamily.GenericSansSerif, 20,FontStyle.Bold);
        private int _numberFlashesOfBoxAtStartup = 2;
        private int _timerCount = 1;
        private bool _showStartupBox = false; //to draw yellow box around step 1 buttons
        


        //we define the color in terms of:
        // 1. color tone in terms of red, green and blue as if fully saturated
        // 2. saturation, i.e. inverse of how much white is addded to the orig rgb tone
        // color and saturation define the hue
        // 3. lightness, i.e. number between 0 and 1 to multiply hue by, zero being black

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
        /// the desaturation of the color, 0 saturated to 1 white
        /// </summary>
        private double _desaturation = 0;
        /// <summary>
        /// adjustment to brightness of the color, 0 to 1
        /// </summary>
        private double _lightness = 1;
        /// <summary>
        /// true if mouse down within panelMain, until released
        /// </summary>
        private bool _mouseDown = false;
        ///// <summary>
        ///// r+g+b of _currentColor
        ///// </summary>
        //private int _totalIntensityAtMouseDown;

        #region constructor
        /// <summary>
        /// control for creating and naming color
        /// </summary>
        public DisplaySkinColorAddEdit()
        {
            InitializeComponent();
            panelMain.MouseWheel += PanelMain_MouseWheel;
            groupBoxPickStarting.BackColor = Color.DodgerBlue;
            wveTrackbar1.PointerDirection = Wve.WveTrackbar.PointerDirections.DiamondHorizontal;
            timer1.Interval = 500; //1/2 second, used for flashing box at startup
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
        /// <summary>
        /// has to be called explicitly
        /// </summary>
        public void Start()
        {
            SplashForSkin2 dlg = new SplashForSkin2();
            //dlg.Deactivate
            dlg.ShowDialog();
            _timerCount = 0; //restart timer 
            timer1.Start(); //start flashing of box
            

            //StringBuilder sbInstruct = new StringBuilder();
            //sbInstruct.Append(MainClass.MinhaCorResourceManager.GetString("StringInstruct1"));
            //sbInstruct.Append(Environment.NewLine);
            //sbInstruct.Append(MainClass.MinhaCorResourceManager.GetString("StringInstruct2"));
            //sbInstruct.Append(Environment.NewLine);
            //sbInstruct.Append(MainClass.MinhaCorResourceManager.GetString("StringInstruct3"));
            //sbInstruct.Append(Environment.NewLine);
            //sbInstruct.Append(MainClass.MinhaCorResourceManager.GetString("StringInstruct4"));
            //sbInstruct.Append(Environment.NewLine);
            //MessageBox.Show(sbInstruct.ToString(),
            //    "1 - 2 - 3 ",
            //    MessageBoxButtons.OK,
            //    MessageBoxIcon.None);
        }

        internal void Reset()
        {
            setDefaults();
            textBoxColorName.Clear();
            textBoxPersonName.Clear();
        }

        /// <summary>
        /// lightness, color, desaturation
        /// </summary>
        /// <param name="t"></param>
        private void setDisplay(Tuple<double,double,double> t) 
        {
            double lightness = t.Item1;
            double color = t.Item2;
            double desaturation = t.Item3;
            //;trackBarLightness.Maximum = _trackbarMaxValue;
            _lightness = lightness;
            if (lightness == 1)
            {
                //;trackBarLightness.Value = _trackbarMaxValue;
                wveTrackbar1.Value = 1;
            }
            else
            {
                //;trackBarLightness.Value = (int)Math.Floor((_trackbarMaxValue + 1) * (_lightness));
                wveTrackbar1.Value = _lightness;
            }
            panelMain.BackColor = Color.Transparent;
            if(color == 1)
            {
                _startingLocation.X = panelMain.Width - 1;
                _currentLocation.X = _startingLocation.X;
            }
            else
            {
                _startingLocation.X = (int)Math.Floor(panelMain.Width * color);
                _currentLocation.X = _startingLocation.X;
            }
            
            if(desaturation==1)
            {
                _startingLocation.Y = panelMain.Height - 1;
                _currentLocation.Y = _startingLocation.Y;
            }
            else
            {
                _startingLocation.Y = (int)Math.Floor(panelMain.Height * desaturation);
                _currentLocation.Y = _startingLocation.Y;
            }
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
            radioButtonLightLight.BackColor = Color.FromArgb(0xF7, 0xE7, 0xC7);
            radioButtonLight.BackColor = Color.FromArgb(0xEA, 0xCE, 0xA4);
            radioButtonDark.BackColor = Color.FromArgb(0x6C, 0x53, 0x3D);
            radioButtonDarkDark.BackColor = Color.FromArgb(0x2E, 0x1B, 0x0C);
            //setDisplay(new Tuple<double, double, double>(0.30, 0.70, 0.90));
            setDisplay(colorToLocation(Color.FromArgb(0, 0, 0)));// was (0xFF, 0xFF, 0xE0)));  //; wasradioButtonLight.BackColor));
            textBoxColorName.Focus();
        }

        /// <summary>
        /// lightness, color, desaturation
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private Tuple<double,double, double> colorToLocation(Color c)
        {
            //1- amount of white added to color
            double desaturation;  //corresponds with y
            //0 green, 1 red
            double color;
            //0 to 1, 0 is black
            double lightness;
            //if yellow
            if (c.R == c.G) 
            {
                if (c.R == 0) //then rgb is black
                {
                    desaturation = 0.5; //can be anything
                    color = 0.5; //yellow, but could be anything
                    lightness = 0;
                }
                else
                {
                    //blue part all comes from desaturation
                    desaturation =  ((double)c.B / c.R);
                    //lightness is r divided by what it could be
                    //y inv of lightness
                    lightness = ((double)c.R / 256);
                    color = 0.5;
                }
            }
            else if(c.R > c.G) //then r > b too
            {
                //blue part all comes from desaturation
                desaturation = ((double)c.B / c.R);
                //lightness is r divided by what it could be
                lightness =  ((double)c.R / 255 );
                //x is inverse of amount of green decreasing from 1 at half width to 0 by full width
                color = 0.5 + (0.5 * (1 - (((double)c.G - c.B) / ((double)c.R - c.B))));
            }
            else //then (g > r) and (g > b)
            {
                //blue part all comes from desaturation
                desaturation =  ((double)c.B / c.G);
                //lightness is g divided by what it could be
                lightness = ((double)c.G  / 255);
                //x is measure of amount of red up to 100% by half width of panel
                color = 0.5 * (((double)c.R - c.B) / ((double)c.G - c.B));
            }
            return new Tuple<double, double, double>(lightness, color, desaturation);
        }


        private void PanelMain_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                //labelDelta.Text = e.Delta.ToString();
                //adjust 1% per delta
                if (e.Delta < 0)
                {
                    //if (trackBarLightness.Value >= (int)Math.Floor((double) _trackbarMaxValue / 100))
                    //{
                    //    trackBarLightness.Value -= (int)Math.Floor((double)_trackbarMaxValue / 100);
                    //}
                    //else
                    //{
                    //    trackBarLightness.Value = 0;
                    //}
                    if(wveTrackbar1.Value > 0.01)
                    {
                        wveTrackbar1.Value -= 0.01;
                    }
                    else
                    {
                        wveTrackbar1.Value = 0;
                    }
                }
                else
                {
                    //if (trackBarLightness.Value < (int)Math.Floor((double)_trackbarMaxValue - _trackbarMaxValue / 100))
                    //{
                    //    trackBarLightness.Value += (int)Math.Floor((double)_trackbarMaxValue / 100);
                    //}
                    //else
                    //{
                    //    trackBarLightness.Value = _trackbarMaxValue;
                    //}
                    if(wveTrackbar1.Value < 0.99)
                    {
                        wveTrackbar1.Value += 0.01;
                    }
                    else
                    {
                        wveTrackbar1.Value = 1;
                    }
                }
                //trackBarLightness_Scroll(sender, e);
                wveTrackbar1_PointerScrolled(sender, e);
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

                    //first figure r,g,b  as floating numbers between zero and one
                    _rTemp = ((_r * (1 - _desaturation)) + _desaturation) * _lightness;
                    _gTemp = ((_g * (1 - _desaturation)) + _desaturation) * _lightness;
                    _bTemp = ((_b * (1 - _desaturation)) + _desaturation) * _lightness;
                    //watch for rounding errors
                    if ((double.IsNaN(_rTemp)) || (_rTemp < 0))
                        _rTemp = 0;
                    if ((double.IsNaN(_gTemp)) || (_gTemp < 0))
                        _gTemp = 0;
                    if ((double.IsNaN(_bTemp)) || (_bTemp < 0))
                        _bTemp = 0;
                    //then  convert to int 0-255
                    _currentColor = Color.FromArgb(255,
                        _rTemp >= 1 ? 255 : (int)Math.Floor(_rTemp * 256),
                        _gTemp >= 1 ? 255 : (int)Math.Floor(_gTemp * 256),
                        _bTemp >= 1 ? 255 : (int)Math.Floor(_bTemp * 256));


                    //if (_mouseDown)
                    if (true)
                    {
                        panelBackground.BackColor = _currentColor; //to reduce flicker
                    }
                    using (Brush b = new SolidBrush(_currentColor)) 
                    {
                        e.Graphics.FillRectangle(b,
                             new Rectangle(new Point(40, 40), new Size(
                                panelMain.Width - 80, panelMain.Height - 80)));
                    }
                    e.Graphics.FillEllipse(Brushes.DodgerBlue,
                        _currentLocation.X - 20,
                        _currentLocation.Y - 20,
                        40,
                        40);
                    e.Graphics.DrawString("3", _fontForLabel, Brushes.Yellow, 
                        new Rectangle(_currentLocation.X - 12,
                        _currentLocation.Y - 17,
                        40,
                        40));

                    //mark center
                    e.Graphics.FillRectangle(Brushes.White,
                        panelMain.Width / 2, panelMain.Height / 2, 1, 1);

                    //mark starting point
                    e.Graphics.DrawEllipse(Pens.DodgerBlue, new Rectangle(
                        _startingLocation.X - 200,
                        _startingLocation.Y - 200,
                        400,
                        400));

                    //show rgb
                    labelrgb.Text = Wve.WveTools.BytesToHex(new byte[] {
                    _currentColor.R,
                    _currentColor.G,
                    _currentColor.B},
                    " ");
                    panelRgb.Invalidate();

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
                //this.Cursor = new Cursor(Cursor.Current.Handle);
                //Cursor.Position = panelMain.PointToScreen(_currentLocation);
                //_totalIntensityAtMouseDown = _currentColor.R + _currentColor.G + _currentColor.B;
                _ignoreControlEvents = false;
                panelMain_MouseMove(sender, e);
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
                    if ((sender == panelMain) &&
                        ((e.Y >= 0) && (e.Y <= panelMain.Height -1)) &&
                        ((e.X >= 0) && (e.X <= panelMain.Width - 1 )))
                    {
                        _currentLocation = e.Location;
                    }
                    //lightness
                    //if ((e.Y >= 0) && (e.Y <= panelMain.Height))
                    //{
                    //    _lightness = ((double)panelMain.Height - e.Y) / panelMain.Height;
                    //}
                    //color
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
                    //saturation
                    _desaturation = (double)e.Y / (panelMain.Height - 1);

                    ////set if this is before the mouse was pressed...
                    //if ((sender is string) && ((string)sender == "first call"))
                    //{
                    //    _totalIntensityAtMouseDown = _currentColor.R + _currentColor.G + _currentColor.B;
                    //}
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
                //trackBarLightness.Value = (int)Math.Round((1-_saturation) * _trackbarMaxValue);
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

        //private void trackBarLightness_Scroll(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //got here
        //        _lightness = ((double)trackBarLightness.Value / (double)_trackbarMaxValue);
        //        panelMain.Invalidate();
        //        panelSwatch.Invalidate();
        //    }
        //    catch (Exception er)
        //    {
        //        Wve.MyEr.Show(this, er, true);
        //    }
        //}

        private void wveTrackbar1_PointerScrolled(object sender, EventArgs e)
        {
            _lightness = wveTrackbar1.Value;
            panelMain.Invalidate();
            panelSwatch.Invalidate();
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

        /// <summary>
        /// set color and cursor square position to the page default 
        /// so cursor doesn't get lost
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        MessageBox.Show("Please name your color, or else click cancel.",
                            "Color not named yet");
                        //if (MessageBox.Show("Please name your color, or else click cancel.",
                        //    "Color not named yet",
                        //    MessageBoxButtons.OKCancel,
                        //    MessageBoxIcon.None,
                        //    MessageBoxDefaultButton.Button1) == DialogResult.Cancel)
                        //{
                        //    buttonCancel_Click(sender, e);
                        //}
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
            
        }

        //private void panelControls_Paint(object sender, PaintEventArgs e)
        //{

        //}

        private void panelRgb_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                //e.Graphics.DrawString(labelrgb.Width.ToString(),this.Font, Brushes.Black, new PointF(0,0));
                e.Graphics.FillRectangle(Brushes.Red, new Rectangle(0,
                    (int)Math.Floor((255-_currentColor.R)*26/255D), 14, 26)); //D for double
                e.Graphics.FillRectangle(Brushes.Lime, new Rectangle(20,
                    (int)Math.Floor((255 - _currentColor.G) * 26 / 255D), 14, 26)); //D for double
                e.Graphics.FillRectangle(Brushes.Blue, new Rectangle(40,
                    (int)Math.Floor((255 - _currentColor.B) * 26 / 255D), 14, 26)); //D for double
            }
            catch
            {
                e.Graphics.Clear(Color.Red);
            }
        }

        private void radio_Click(object sender, EventArgs e)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    if (sender is RadioButton)
                    {
                        setDisplay(colorToLocation(((RadioButton)sender).BackColor));
                    }
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }

        private void groupBoxPickStarting_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawString("1", _fontForLabel, Brushes.Yellow,
                        new Rectangle(groupBoxPickStarting.Width / 2 - 15,
                        17,
                        40,
                        40));
                        //groupBoxPickStarting.Location.X + 50,
                        //groupBoxPickStarting.Location.Y - 30,
                        //20,
                        //20));
        }

        /// <summary>
        /// make flashing box for about 4 iterations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    if(_timerCount < _numberFlashesOfBoxAtStartup * 2)
                    {
                        //for even counts draw box
                        _showStartupBox = (_timerCount %2 == 0);
                        if(_showStartupBox)
                        {
                            groupBoxPickStarting.BackColor = Color.DodgerBlue;
                        }
                        else
                        {
                            groupBoxPickStarting.BackColor = Color.Yellow;
                        }
                        _timerCount++;
                        panelControls.Invalidate();
                    }
                    else
                    {
                        timer1.Stop();
                        _timerCount = 1;
                        _showStartupBox = false;
                        groupBoxPickStarting.BackColor = Color.DodgerBlue;
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
            if (_showStartupBox)
            {
                Pen pen = new Pen(Color.Yellow, 15);
                e.Graphics.DrawRectangle(pen,
                    0,
                    50,
                    groupBoxPickStarting.Width + 5,
                    groupBoxPickStarting.Height + 5);
            }
        }
    }
}
