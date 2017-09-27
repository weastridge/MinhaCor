using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wve
{
    /// <summary>
    /// customizable trackbar
    /// </summary>
    public partial class WveTrackbar : UserControl
    {
        private double _value = 0.5;
        /// <summary>
        /// value between 0 and 1 inclusive
        /// </summary>
        public double Value
        {
            get
            {
                return _value;
            }

            set
            {
                _value = value;
            }
        }
        /// <summary>
        /// point for the center of the pointer
        /// </summary>
        private int _topMargin = 40;
        private int _bottomMargin = 40;
        private Point _origin;
        /// <summary>
        /// the polygon for pointer when value is zero
        /// </summary>
        private Point[] _pointerShapeAtZero;
        /// <summary>
        /// the polygon for pointer at present value
        /// </summary>
        private Point[] _pointerShapeForValue;
        private Brush _brushForPointer = Brushes.DodgerBlue;
        private Brush _brushForLabel = Brushes.Yellow;
        /// <summary>
        /// keep track of when pointer is being moved
        /// </summary>
        private bool _isScrolling = false;


        #region Events
        /// <summary>
        /// fires when pointer is scrolled
        /// </summary>
        public event EventHandler PointerScrolled;


        #endregion Events

        #region constructors
        /// <summary>
        /// customizable trackbar
        /// </summary>
        public WveTrackbar()
        {
            InitializeComponent();
            initializePointer();
        }
        #endregion constructors
        private void WveTrackbar_Load(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception er)
            {
                throw new Exception("Error initializing wveTrackbar", er);
            }
        }

        private void initializePointer()
        {
            _origin = new Point(Width / 2,
                Height - _bottomMargin);
            //make pointer width 3/4 of total width
            Point p1 = new Point(0 + (Width / 8),_origin.Y);
            Point p2 = new Point(0 + (Width * 7 / 8),
                (int)Math.Floor(_origin.Y-(Math.Sqrt(3) *(Width /4))));
            Point p3 = new Point(0 + (Width * 7/8),
                (int)Math.Floor(_origin.Y + (Math.Sqrt(3) * (Width / 4))));
            _pointerShapeAtZero = new Point[] { p1, p2, p3 };
            _pointerShapeForValue = null; //force recreation when next painted
        }

        private void WveTrackbar_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                //if settings have been initialized:
                if (_pointerShapeAtZero != null)
                {
                    //move pointer
                    if(_pointerShapeForValue == null)
                    {
                        _pointerShapeForValue = (Point[])_pointerShapeAtZero.Clone();
                    }
                    for (int i = 0; i < _pointerShapeAtZero.Length; i++)
                    {
                        _pointerShapeForValue[i].Y = _pointerShapeAtZero[i].Y -
                            (int)(Math.Floor(Value * (Height - _topMargin - _bottomMargin)));
                    }
                    e.Graphics.FillPolygon(_brushForPointer, _pointerShapeForValue);
                    e.Graphics.DrawString("3", new Font("Microsoft Sans", 20,FontStyle.Bold),_brushForLabel,
                        new Point(Width/2 - 5, _pointerShapeForValue[0].Y - 16)); //fudge factor
                }
            }
            catch (Exception er)
            {
                throw new Exception("Error drawing trackbar.", er);
            }
        }

        private void WveTrackbar_Resize(object sender, EventArgs e)
        {
            try
            {
                initializePointer();
                Invalidate();
            }
            catch (Exception er)
            {
                throw new Exception("Error resizing wveTrackbar", er);
            }
        }

        private void WveTrackbar_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                //if clicking close to the pointer; within square around current position
                if((Height -e.Y) > (Value * (Height - (_topMargin + _bottomMargin)) + _bottomMargin - (Width/2)) &&
                    (Height - e.Y) < (Value * (Height - (_topMargin + _bottomMargin)) + _bottomMargin + (Width / 2))
                    )
                {
                    _isScrolling = true;
                }
                
            }
            catch (Exception er)
            {
                throw new Exception("Error in wveTrackbar MouseDown", er);
            }
        }

        private void WveTrackbar_MouseUp(object sender, MouseEventArgs e)
        {
            if(_isScrolling)
            {
                _isScrolling = false;
            }
        }

        private void WveTrackbar_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if(_isScrolling)
                {
                    //reset value
                    if (e.Y <= _topMargin)
                    {
                        Value = 1;
                    }
                    else if (e.Y >= Height - _bottomMargin)
                    {
                        Value = 0;
                    }
                    else
                    {
                        _value = ((double)(Height - e.Y) - _bottomMargin) / ((double)Height - (_topMargin + _bottomMargin));
                    }
                    //and redraw
                    Invalidate();
                    //and call any delegates
                    if(this.PointerScrolled != null)
                    {
                        this.PointerScrolled(this, e);
                    }
                }
            }
            catch (Exception er)
            {
                throw new Exception("Error in wveTrackbar moving", er);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // WveTrackbar
            // 
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Name = "WveTrackbar";
            this.Size = new System.Drawing.Size(53, 487);
            this.Load += new System.EventHandler(this.WveTrackbar_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.WveTrackbar_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WveTrackbar_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WveTrackbar_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.WveTrackbar_MouseUp);
            this.Resize += new System.EventHandler(this.WveTrackbar_Resize);
            this.ResumeLayout(false);

        }
    }
}
