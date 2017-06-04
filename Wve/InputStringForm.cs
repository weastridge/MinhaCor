using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Wve
{
    /// <summary>
    /// Dialog for inputting short string
    /// exposes LabelPrompt and TextBoxInput
    /// </summary>
    public partial class InputStringForm : Form
    {
        /// <summary>
        /// exposes LabelPrompt and TextBoxInput
        /// </summary>
        public InputStringForm()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}