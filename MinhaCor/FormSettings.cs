using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace MinhaCor
{
    /// <summary>
    /// settings
    /// </summary>
    public partial class FormSettings : Form
    {
        private bool _ignoreChangeEvents = false;
        private bool _cultureWasChanged = false;
        private int _rows;
        private int _columns;
        /// <summary>
        /// settings
        /// </summary>
        public FormSettings()
        {
            InitializeComponent();
        }

        private void FormSettings_Load(object sender, EventArgs e)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    loadSettings();
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }

        private void loadSettings()
        {
            _ignoreChangeEvents = true;
            textBoxRows.Text = MainClass.ConfigSettings.GetValue("Rows");
            textBoxColumns.Text = MainClass.ConfigSettings.GetValue("Columns");
            textBoxPassword.Text = MainClass.ConfigSettings.GetValue("Password");
            comboBoxCulture.Items.Clear();
            comboBoxCulture.Items.Add("default");
            comboBoxCulture.Items.Add("en-US");
            comboBoxCulture.Items.Add("pt-BR");
            comboBoxCulture.SelectedIndex = 0; //unless config is otherwise
            for(int i=0; i<comboBoxCulture.Items.Count; i++)
            {
                if((!string.IsNullOrWhiteSpace(MainClass.DefaultCultureName)) && 
                    (MainClass.DefaultCultureName.Trim().ToLower() == 
                    comboBoxCulture.Items[i].ToString().Trim().ToLower()))
                {
                    comboBoxCulture.SelectedIndex = i;
                    break;
                }
            }
            if(MainClass.EditMode == MainClass.MinhaCorEditMode.fullColor)
            {
                radioButtonFullColor.Checked = true;
            }
            else
            {
                radioButtonEasy.Checked = true;
            }
            _ignoreChangeEvents = false;
        }

        private void setCulture(string culture)
        {
            if(culture == "default")
            {
                //revert to default found at startup
                System.Globalization.CultureInfo.CurrentCulture = MainClass.StartUpCulture;
                System.Globalization.CultureInfo.CurrentUICulture = MainClass.StartUpUICulture;
            }
            else
            {
                try
                {
                    CultureInfo.CurrentCulture = new CultureInfo(culture);
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
            //save for future use
            MainClass.DefaultCultureName = culture;
            //notify
            MessageBox.Show("Culture changes made.  Please restart program now for consistency.");
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    if (applySettings())
                    {
                        this.DialogResult = DialogResult.OK;
                    }
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }
        /// <summary>
        /// apply settings if valid, returns true if validated
        /// </summary>
        /// <returns></returns>
        private bool applySettings()
        {
            bool validated = true;
            if ((!int.TryParse(textBoxRows.Text, out _rows)) ||
                (_rows < 1) ||
                 (_rows > 20))
            {
                MessageBox.Show("please enter rows between 1 and 10");
                return false;
            }
            if ((!int.TryParse(textBoxColumns.Text, out _columns)) ||
                (_columns < 1) ||
                 (_columns > 20))
            {
                MessageBox.Show("please enter columns between 1 and 10");
                return false;
            }
            MainClass.ConfigSettings.SetValue("Rows", _rows.ToString());
            MainClass.ConfigSettings.SetValue("Columns", _columns.ToString());
            MainClass.ConfigSettings.SetValue("Culture", comboBoxCulture.SelectedText);
            MainClass.ConfigSettings.SetValue("Password", textBoxPassword.Text);

            if(radioButtonEasy.Checked)
            {
                MainClass.EditMode = MainClass.MinhaCorEditMode.easy;
            }
            else
            {
                MainClass.EditMode = MainClass.MinhaCorEditMode.fullColor;
            }
            if(_cultureWasChanged)
            {
                setCulture(comboBoxCulture.SelectedItem.ToString());
            }
            return validated;
        }

        private void comboBoxCulture_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!_ignoreChangeEvents)
            {
                _cultureWasChanged = true;
            }
        }
    }
}
