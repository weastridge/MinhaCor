namespace Wve.WveServUtil
{
    partial class EditTaskScheduleForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.radioButtonDoByInterval = new System.Windows.Forms.RadioButton();
            this.radioButtonDoDaily = new System.Windows.Forms.RadioButton();
            this.radioButtonDoWeekly = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxDayToDo = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dateTimePickerTimeToEnable = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerTimeToDisable = new System.Windows.Forms.DateTimePicker();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.checkBoxTimeToDisable = new System.Windows.Forms.CheckBox();
            this.checkBoxTimeToEnable = new System.Windows.Forms.CheckBox();
            this.dateTimePickerIntervalToDo = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerTimeToDo = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerTimeToDoWeekly = new System.Windows.Forms.DateTimePicker();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(284, 276);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(87, 27);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "&OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.CausesValidation = false;
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(379, 276);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(87, 27);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // radioButtonDoByInterval
            // 
            this.radioButtonDoByInterval.AutoSize = true;
            this.radioButtonDoByInterval.Location = new System.Drawing.Point(14, 27);
            this.radioButtonDoByInterval.Name = "radioButtonDoByInterval";
            this.radioButtonDoByInterval.Size = new System.Drawing.Size(130, 19);
            this.radioButtonDoByInterval.TabIndex = 2;
            this.radioButtonDoByInterval.TabStop = true;
            this.radioButtonDoByInterval.Text = "Do every interval of ";
            this.radioButtonDoByInterval.UseVisualStyleBackColor = true;
            // 
            // radioButtonDoDaily
            // 
            this.radioButtonDoDaily.AutoSize = true;
            this.radioButtonDoDaily.Location = new System.Drawing.Point(13, 78);
            this.radioButtonDoDaily.Name = "radioButtonDoDaily";
            this.radioButtonDoDaily.Size = new System.Drawing.Size(85, 19);
            this.radioButtonDoDaily.TabIndex = 3;
            this.radioButtonDoDaily.TabStop = true;
            this.radioButtonDoDaily.Text = "Do daily at ";
            this.radioButtonDoDaily.UseVisualStyleBackColor = true;
            // 
            // radioButtonDoWeekly
            // 
            this.radioButtonDoWeekly.AutoSize = true;
            this.radioButtonDoWeekly.Location = new System.Drawing.Point(13, 132);
            this.radioButtonDoWeekly.Name = "radioButtonDoWeekly";
            this.radioButtonDoWeekly.Size = new System.Drawing.Size(101, 19);
            this.radioButtonDoWeekly.TabIndex = 4;
            this.radioButtonDoWeekly.TabStop = true;
            this.radioButtonDoWeekly.Text = "Do weekly on ";
            this.radioButtonDoWeekly.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(18, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "or";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(18, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "or";
            // 
            // comboBoxDayToDo
            // 
            this.comboBoxDayToDo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxDayToDo.FormattingEnabled = true;
            this.comboBoxDayToDo.Location = new System.Drawing.Point(120, 132);
            this.comboBoxDayToDo.Name = "comboBoxDayToDo";
            this.comboBoxDayToDo.Size = new System.Drawing.Size(121, 23);
            this.comboBoxDayToDo.TabIndex = 10;
            this.comboBoxDayToDo.SelectedIndexChanged += new System.EventHandler(this.comboBoxDayToDo_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(247, 134);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 15);
            this.label3.TabIndex = 11;
            this.label3.Text = "after";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(167, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "O\'clock";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(210, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 15);
            this.label5.TabIndex = 13;
            this.label5.Text = "hours";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(347, 134);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 15);
            this.label6.TabIndex = 14;
            this.label6.Text = "O\'clock";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dateTimePickerTimeToEnable);
            this.groupBox1.Controls.Add(this.dateTimePickerTimeToDisable);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.checkBoxTimeToDisable);
            this.groupBox1.Controls.Add(this.checkBoxTimeToEnable);
            this.groupBox1.Location = new System.Drawing.Point(5, 180);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(470, 90);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "schedule limitation (if checked)";
            // 
            // dateTimePickerTimeToEnable
            // 
            this.dateTimePickerTimeToEnable.CustomFormat = "HH:mm";
            this.dateTimePickerTimeToEnable.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerTimeToEnable.Location = new System.Drawing.Point(223, 19);
            this.dateTimePickerTimeToEnable.Name = "dateTimePickerTimeToEnable";
            this.dateTimePickerTimeToEnable.Size = new System.Drawing.Size(57, 21);
            this.dateTimePickerTimeToEnable.TabIndex = 20;
            this.dateTimePickerTimeToEnable.ValueChanged += new System.EventHandler(this.dateTimePickerTimeToEnable_ValueChanged);
            // 
            // dateTimePickerTimeToDisable
            // 
            this.dateTimePickerTimeToDisable.CustomFormat = "HH:mm";
            this.dateTimePickerTimeToDisable.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerTimeToDisable.Location = new System.Drawing.Point(225, 44);
            this.dateTimePickerTimeToDisable.Name = "dateTimePickerTimeToDisable";
            this.dateTimePickerTimeToDisable.Size = new System.Drawing.Size(57, 21);
            this.dateTimePickerTimeToDisable.TabIndex = 21;
            this.dateTimePickerTimeToDisable.ValueChanged += new System.EventHandler(this.dateTimePickerTimeToDisable_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(289, 49);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 15);
            this.label9.TabIndex = 14;
            this.label9.Text = "O\'clock";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(289, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 15);
            this.label8.TabIndex = 13;
            this.label8.Text = "O\'clock";
            // 
            // checkBoxTimeToDisable
            // 
            this.checkBoxTimeToDisable.AutoSize = true;
            this.checkBoxTimeToDisable.Location = new System.Drawing.Point(47, 48);
            this.checkBoxTimeToDisable.Name = "checkBoxTimeToDisable";
            this.checkBoxTimeToDisable.Size = new System.Drawing.Size(172, 19);
            this.checkBoxTimeToDisable.TabIndex = 1;
            this.checkBoxTimeToDisable.Text = "But only do if time is before";
            this.checkBoxTimeToDisable.UseVisualStyleBackColor = true;
            // 
            // checkBoxTimeToEnable
            // 
            this.checkBoxTimeToEnable.AutoSize = true;
            this.checkBoxTimeToEnable.Location = new System.Drawing.Point(47, 23);
            this.checkBoxTimeToEnable.Name = "checkBoxTimeToEnable";
            this.checkBoxTimeToEnable.Size = new System.Drawing.Size(164, 19);
            this.checkBoxTimeToEnable.TabIndex = 0;
            this.checkBoxTimeToEnable.Text = "But only do if time is after ";
            this.checkBoxTimeToEnable.UseVisualStyleBackColor = true;
            // 
            // dateTimePickerIntervalToDo
            // 
            this.dateTimePickerIntervalToDo.CustomFormat = "HH:mm";
            this.dateTimePickerIntervalToDo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerIntervalToDo.Location = new System.Drawing.Point(150, 27);
            this.dateTimePickerIntervalToDo.Name = "dateTimePickerIntervalToDo";
            this.dateTimePickerIntervalToDo.Size = new System.Drawing.Size(54, 21);
            this.dateTimePickerIntervalToDo.TabIndex = 17;
            this.dateTimePickerIntervalToDo.ValueChanged += new System.EventHandler(this.dateTimePickerIntervalToDo_ValueChanged);
            // 
            // dateTimePickerTimeToDo
            // 
            this.dateTimePickerTimeToDo.CustomFormat = "HH:mm";
            this.dateTimePickerTimeToDo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerTimeToDo.Location = new System.Drawing.Point(104, 80);
            this.dateTimePickerTimeToDo.Name = "dateTimePickerTimeToDo";
            this.dateTimePickerTimeToDo.Size = new System.Drawing.Size(57, 21);
            this.dateTimePickerTimeToDo.TabIndex = 18;
            this.dateTimePickerTimeToDo.ValueChanged += new System.EventHandler(this.dateTimePickerTimeToDo_ValueChanged);
            // 
            // dateTimePickerTimeToDoWeekly
            // 
            this.dateTimePickerTimeToDoWeekly.CustomFormat = "HH:mm";
            this.dateTimePickerTimeToDoWeekly.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerTimeToDoWeekly.Location = new System.Drawing.Point(284, 134);
            this.dateTimePickerTimeToDoWeekly.Name = "dateTimePickerTimeToDoWeekly";
            this.dateTimePickerTimeToDoWeekly.Size = new System.Drawing.Size(57, 21);
            this.dateTimePickerTimeToDoWeekly.TabIndex = 19;
            this.dateTimePickerTimeToDoWeekly.ValueChanged += new System.EventHandler(this.dateTimePickerTimeToDoWeekly_ValueChanged);
            // 
            // EditTaskSchedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(478, 312);
            this.ControlBox = false;
            this.Controls.Add(this.dateTimePickerTimeToDoWeekly);
            this.Controls.Add(this.dateTimePickerTimeToDo);
            this.Controls.Add(this.dateTimePickerIntervalToDo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxDayToDo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radioButtonDoWeekly);
            this.Controls.Add(this.radioButtonDoDaily);
            this.Controls.Add(this.radioButtonDoByInterval);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "EditTaskSchedule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Task Schedule";
            this.Load += new System.EventHandler(this.EditTaskSchedule_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.RadioButton radioButtonDoByInterval;
        private System.Windows.Forms.RadioButton radioButtonDoDaily;
        private System.Windows.Forms.RadioButton radioButtonDoWeekly;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxDayToDo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox checkBoxTimeToDisable;
        private System.Windows.Forms.CheckBox checkBoxTimeToEnable;
        private System.Windows.Forms.DateTimePicker dateTimePickerIntervalToDo;
        private System.Windows.Forms.DateTimePicker dateTimePickerTimeToEnable;
        private System.Windows.Forms.DateTimePicker dateTimePickerTimeToDisable;
        private System.Windows.Forms.DateTimePicker dateTimePickerTimeToDo;
        private System.Windows.Forms.DateTimePicker dateTimePickerTimeToDoWeekly;
    }
}