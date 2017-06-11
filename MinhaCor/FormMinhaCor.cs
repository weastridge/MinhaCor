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
    /// minha cor main form
    /// </summary>
    public partial class FormMinhaCor : Form
    {
        private DisplayGrid _displayGridInstance;
        private DisplayColorAddEdit _displayColorAddEditInstance;
        internal static FormMinhaCor Instance = null;

        /// <summary>
        /// main form
        /// </summary>
        public FormMinhaCor()
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    //initialize main class first because it sets culture info
                    MainClass.Initialize();
                    InitializeComponent();
                    _displayColorAddEditInstance = new DisplayColorAddEdit();
                    _displayGridInstance = new DisplayGrid();
                    Instance = this;
                    //set defaults
                    openFileDialog1.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
                    openFileDialog1.DefaultExt = "csv";
                    openFileDialog1.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
                    saveFileDialog1.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
                    saveFileDialog1.Filter = "csv files (*.csv)|*.csv|All files (*.*)|*.*";
                    saveFileDialog1.DefaultExt = ".csv";
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }

        
        private void FormMinhaCor_Load(object sender, EventArgs e)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    MainClass.LoadColorCreations();
                    LoadDisplayGrid();
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }
        /// <summary>
        /// load given user control in display area
        /// </summary>
        /// <param name="display"></param>
        internal void LoadDisplay(UserControl display)
        {
            panelDisplayArea.Controls.Clear();
            panelDisplayArea.Controls.Add(display);
            display.Dock = DockStyle.Fill;
        }
        /// <summary>
        /// load the display grid
        /// </summary>
        internal void LoadDisplayGrid()
        {
            LoadDisplay(_displayGridInstance);
            _displayGridInstance.DrawGrid();
        }
        /// <summary>
        /// load the color edit display
        /// </summary>
        internal void LoadDisplayColorAddEdit()
        {
            LoadDisplay(_displayColorAddEditInstance);
            _displayColorAddEditInstance.Reset();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    FormSettings dlg = new FormSettings();
                    dlg.ShowDialog();
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    FormAbout dlg = new FormAbout();
                    dlg.ShowDialog();
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }

        private void toolStripMenuItemShowHelp_Click(object sender, EventArgs e)
        {
            try
            {
                FormHelp dlg = new FormHelp();
                dlg.ShowDialog();
            }
            catch (Exception er)
            {
                Wve.MyEr.Show(this, er, true);
            }
        }
        /// <summary>
        /// open named file into list of ColorCreations after saving
        /// the current list to a backup file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    if(openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        //then first save current file backup
                        StringBuilder sb = new StringBuilder();
                        sb.Append(MainClass.ColorCreationsPathAndFilename.Replace(".csv", "_bak_"));
                        sb.Append(Wve.WveTools.DateTimeToCompactString(DateTime.Now));
                        sb.Append(".csv");
                        MainClass.SaveColorCreations(sb.ToString());
                        //and then save the new file
                        MainClass.LoadColorCreations(openFileDialog1.FileName);
                        //refresh grid
                        LoadDisplayGrid();
                    }
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }
        /// <summary>
        /// save current list of ColorCreations to named file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        MainClass.SaveColorCreations(saveFileDialog1.FileName);
                    }
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }
    }
}
