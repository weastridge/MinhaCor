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
    public partial class FormSettings : Form
    {
        private int _rows;
        private int _columns;
        public FormSettings()
        {
            InitializeComponent();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            loadSettings();
        }

        private void loadSettings()
        {
            textBoxRows.Text = MainClass.ConfigSettings.GetValue("Rows");
            textBoxColumns.Text = MainClass.ConfigSettings.GetValue("Columns");
            setCulture(MainClass.ConfigSettings.GetValue("Culture"));
            textBoxPassword.Text = MainClass.ConfigSettings.GetValue("Password");
        }

        private void setCulture(string culture)
        {

        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    applySettings();
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }

        private void applySettings()
        {
            MainClass.ConfigSettings.SetValue("Rows", _rows.ToString());
            MainClass.ConfigSettings.SetValue("Columns", _columns.ToString());
            MainClass.ConfigSettings.SetValue("Culture", comboBoxCulture.SelectedText);
            MainClass.ConfigSettings.SetValue("Password", textBoxPassword.Text);
        }


    }
}
