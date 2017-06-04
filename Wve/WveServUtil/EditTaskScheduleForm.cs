using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Wve.WveServUtil
{
    /// <summary>
    /// edit a task's schedule members
    /// </summary>
    public partial class EditTaskScheduleForm : Form
    {
        /// <summary>
        /// can temporarily set to true to select a property of a control without 
        /// firing its _Change() event delegate
        /// </summary>
        private Wve.BoolBoxed ignoreChangeEvents = 
		new Wve.BoolBoxed(false);


        private Task _taskEdited;
        /// <summary>
        /// new instance of the task to which changes
        /// made on this form are applied
        /// </summary>
        public Task TaskEdited
        {
            get { return _taskEdited; }
        }
        /// <summary>
        /// edit a task's schedule members
        /// </summary>
        /// <param name="taskToEdit"></param>
        public EditTaskScheduleForm(Task taskToEdit)
        {
            _taskEdited = taskToEdit.Clone();
            InitializeComponent();
        }

        /// <summary>
        /// when first loading form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditTaskSchedule_Load(object sender, EventArgs e)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    //avoid firing events when loading controls...
                    using (Wve.BoolCache cache = new Wve.BoolCache(
                        ref ignoreChangeEvents, true))
                    {
                        loadComboBoxDayToDo();
                        loadControls();
                    }//from ignore change events
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }

        //load combobox
        private void loadComboBoxDayToDo()
        {
            comboBoxDayToDo.Items.Clear();
            foreach (string day in Enum.GetNames(typeof(DayOfWeek)))
            {
                comboBoxDayToDo.Items.Add(day);
            }
        }

        /// <summary>
        /// load controls w values from task to edit
        /// </summary>
        private void loadControls()
        {
            radioButtonDoByInterval.Checked = _taskEdited.DoByInterval;
            dateTimePickerIntervalToDo.Value = DateTime.Today.Date + _taskEdited.IntervalToDo;
            radioButtonDoDaily.Checked = _taskEdited.DoDaily;
            dateTimePickerTimeToDo.Value = DateTime.Today + _taskEdited.TimeToDo;
            radioButtonDoWeekly.Checked = _taskEdited.DoWeekly;
            selectDay(_taskEdited.DayToDo.ToString());
            dateTimePickerTimeToDoWeekly.Value = DateTime.Today + _taskEdited.TimeToDo;
            checkBoxTimeToEnable.Checked = 
                (_taskEdited.TimeToEnable != TimeSpan.FromDays(0));
            dateTimePickerTimeToEnable.Value = DateTime.Today + _taskEdited.TimeToEnable;
            checkBoxTimeToDisable.Checked = (_taskEdited.TimeToDisable != TimeSpan.FromDays(1));
            dateTimePickerTimeToDisable.Value = DateTime.Today + _taskEdited.TimeToDisable;
        }

        /// <summary>
        /// select day of week on combobox
        /// </summary>
        /// <param name="dayToMatch"></param>
        private void selectDay(string dayToMatch)
        {
            for (int i = 0; i < comboBoxDayToDo.Items.Count; i++)
            {
                if (comboBoxDayToDo.Items[i].ToString().Trim().ToUpper() ==
                    dayToMatch.Trim().ToUpper())
                {
                    comboBoxDayToDo.SelectedIndex = i;
                    break;
                }
            }
        }

        /// <summary>
        /// cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// apply changes to taskEdited and close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    //apply changes
                    if (checkBoxTimeToEnable.Checked)
                    {
                        _taskEdited.TimeToEnable =
                            dateTimePickerTimeToEnable.Value.TimeOfDay;
                    }
                    else
                    {
                        //indicate ignore TimeToEnable
                        _taskEdited.TimeToEnable = TimeSpan.FromDays(0);
                    }
                    if (checkBoxTimeToDisable.Checked)
                    {
                        _taskEdited.TimeToDisable =
                            dateTimePickerTimeToDisable.Value.TimeOfDay;
                    }
                    else
                    {
                        //indicate ignore TimeToDisable
                        _taskEdited.TimeToDisable = TimeSpan.FromDays(1);
                    }
                    _taskEdited.DoByInterval = radioButtonDoByInterval.Checked;
                    _taskEdited.IntervalToDo = dateTimePickerIntervalToDo.Value.TimeOfDay;
                    _taskEdited.DoDaily = radioButtonDoDaily.Checked;
                    _taskEdited.TimeToDo = dateTimePickerTimeToDo.Value.TimeOfDay;
                    _taskEdited.DoWeekly = radioButtonDoWeekly.Checked;
                    if (_taskEdited.DoWeekly)
                    {
                        _taskEdited.TimeToDo = 
                            dateTimePickerTimeToDoWeekly.Value.TimeOfDay;
                        if (comboBoxDayToDo.SelectedItem != null)
                        {
                            _taskEdited.DayToDo = 
                                (DayOfWeek)Enum.Parse(typeof(DayOfWeek),
                                comboBoxDayToDo.SelectedItem.ToString());
                        }
                    }
                    //return
                    DialogResult = DialogResult.OK;
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }

        //set type of schedule to do when values of that schedule type are altered:
        //....
        private void dateTimePickerIntervalToDo_ValueChanged(object sender, EventArgs e)
        {
            if(!ignoreChangeEvents.Value)
                radioButtonDoByInterval.Checked = true;
        }

        private void dateTimePickerTimeToDo_ValueChanged(object sender, EventArgs e)
        {
            if(!ignoreChangeEvents.Value)
                radioButtonDoDaily.Checked = true;
        }

        private void comboBoxDayToDo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!ignoreChangeEvents.Value)
                radioButtonDoWeekly.Checked = true;
        }

        private void dateTimePickerTimeToDoWeekly_ValueChanged(object sender, EventArgs e)
        {
            if (!ignoreChangeEvents.Value)
                radioButtonDoWeekly.Checked = true;
        }

        private void dateTimePickerTimeToEnable_ValueChanged(object sender, EventArgs e)
        {
            if (!ignoreChangeEvents.Value)
                checkBoxTimeToEnable.Checked = true;
        }

        private void dateTimePickerTimeToDisable_ValueChanged(object sender, EventArgs e)
        {
            if (!ignoreChangeEvents.Value)
                checkBoxTimeToDisable.Checked = true;
        }
    }
}
