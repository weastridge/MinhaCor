using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Wve
{
    /// <summary>
    /// Custom DateTime picker that allows quick entry and editing of dates
    /// </summary>
    public partial class WveDatePicker : UserControl
    {
        /// <summary>
        /// text to show when user rests pointer over control's textbox part
        /// </summary>
        private string toolTipTextForTextbox = 
            "T= today\r\nD= +day\r\nW= +week\r\nM= +month\r\nY= +year \r\nX= minim";

        /// <summary>
        /// constructor for custom date time picker
        /// </summary>
        public WveDatePicker()
        {
            InitializeComponent();
            textBoxDate.Text = "";
            dateTimePicker1.Value = DateTimePicker.MinimumDateTime;
            toolTip1.SetToolTip(dateTimePicker1, toolTipTextForTextbox);
        }

        /// <summary>
        /// WveDatePicker's value, or DateTime.MinValue to represent 'null'
        /// </summary>
        public DateTime Value
        {
            get
            {
                //return DateTime.MinValue to indicate 'null'
                // which may be different from the picker's MinValue
                if (dateTimePicker1.Value == DateTimePicker.MinimumDateTime)
                {
                    return DateTime.MinValue;
                }
                else
                {
                    return dateTimePicker1.Value;
                }
            }
            set
            {
                //set to picker to MinimumDateTime to represent 'null'
                // if value is DateTime.MinValue, which may be different
                // from DateTimePicker.MinimumDateTime
                if ((value == DateTime.MinValue) ||
                    (value <= DateTimePicker.MinimumDateTime))
                {
                    dateTimePicker1.Value = DateTimePicker.MinimumDateTime;
                }
                else
                {
                    dateTimePicker1.Value = value;
                }
                //this will fire event to show the value in the textbox
            }
        }
        /// <summary>
        /// occurs when user changes focus from the text box, which will usually
        ///  be to go to the datetime picker
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxDate_Validating(object sender, CancelEventArgs e)
        {

        }
        /// <summary>
        /// occurs when value set by program or user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            //show date in text box, or blank if null
            if (dateTimePicker1.Value == DateTimePicker.MinimumDateTime)
            {
                textBoxDate.Clear();
            }
            else
            {
                textBoxDate.Text = dateTimePicker1.Value.ToShortDateString();
            }
            //tell the world the value has changed
            if (this.ValueChanged != null)
            {
                ValueChanged(this, e);
            }
        }

        //make event raiser to show the world when value changes
        /// <summary>
        /// value of MM3DatePicker has changed
        /// </summary>
        public event EventHandler ValueChanged;

        //process keystrokes
        private void control_KeyDown(object sender, KeyEventArgs e)
        {

        }

        /* 
         * not used because sometimes locks out subsequent keypresses;  use keypress instead
        /// <summary>
        /// trap letters to alter date value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateTimePicker1_KeyDown(object sender, KeyEventArgs e)
        {
            //allow numbers, dashes, etc, but 
            // if key is alphabet, then process it and supress it so it won't print
            // note A=65, Z=90
            if (e.KeyValue > 64 && e.KeyValue < 91)
            {
                //don't pass key to textbox
                e.SuppressKeyPress = true;
                //now process letters:

                //reset if x
                if (e.KeyCode == Keys.X)
                    dateTimePicker1.Value = DateTimePicker.MinimumDateTime;
                //set to today if t
                else if (e.KeyCode == Keys.T)
                    dateTimePicker1.Value = DateTime.Today;
                //subtract if tab key is down
                else if ((e.Modifiers & Keys.Shift) == Keys.Shift)
                {
                    if (e.KeyCode == Keys.D)
                        dateTimePicker1.Value = dateTimePicker1.Value.AddDays(-1);
                    else if (e.KeyCode == Keys.W)
                        dateTimePicker1.Value = dateTimePicker1.Value.AddDays(-7);
                    else if (e.KeyCode == Keys.M)
                        dateTimePicker1.Value = dateTimePicker1.Value.AddMonths(-1);
                    else if (e.KeyCode == Keys.Y)
                        dateTimePicker1.Value = dateTimePicker1.Value.AddYears(-1);
                }
                else //no tab
                {
                    if (e.KeyCode == Keys.D)
                        dateTimePicker1.Value = dateTimePicker1.Value.AddDays(1);
                    else if (e.KeyCode == Keys.W)
                        dateTimePicker1.Value = dateTimePicker1.Value.AddDays(7);
                    else if (e.KeyCode == Keys.M)
                        dateTimePicker1.Value = dateTimePicker1.Value.AddMonths(1);
                    else if (e.KeyCode == Keys.Y)
                        dateTimePicker1.Value = dateTimePicker1.Value.AddYears(1);
                }
            }//from if alphabet
        }*/

        /// <summary>
        /// process alpha chars that have special meaning
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateTimePicker1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //allow numbers, dashes, etc, but 
            // if key is alphabet, then process it and supress it so it won't print
            // note A=65, Z=90
            if (char.IsLetter(e.KeyChar))
            {
                //don't pass key to other controls
                e.Handled = true;
                //now process letters:

                //reset if x
                if (e.KeyChar == 'x')
                    dateTimePicker1.Value = DateTimePicker.MinimumDateTime;
                //set to today if t
                else if (e.KeyChar == 't')
                    dateTimePicker1.Value = DateTime.Today;
                //subtract if tab key is down
                else if (e.KeyChar == 'D')
                    dateTimePicker1.Value = dateTimePicker1.Value.AddDays(-1);
                else if (e.KeyChar == 'W')
                    dateTimePicker1.Value = dateTimePicker1.Value.AddDays(-7);
                else if (e.KeyChar == 'M')
                    dateTimePicker1.Value = dateTimePicker1.Value.AddMonths(-1);
                else if (e.KeyChar == 'Y')
                    dateTimePicker1.Value = dateTimePicker1.Value.AddYears(-1);

                else if (e.KeyChar == 'd')
                    dateTimePicker1.Value = dateTimePicker1.Value.AddDays(1);
                else if (e.KeyChar == 'w')
                    dateTimePicker1.Value = dateTimePicker1.Value.AddDays(7);
                else if (e.KeyChar == 'm')
                    dateTimePicker1.Value = dateTimePicker1.Value.AddMonths(1);
                else if (e.KeyChar == 'y')
                    dateTimePicker1.Value = dateTimePicker1.Value.AddYears(1);

            }//from if alphabet
        }
    }
}
