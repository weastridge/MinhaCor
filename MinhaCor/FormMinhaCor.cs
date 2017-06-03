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

        /// <summary>
        /// main form
        /// </summary>
        public FormMinhaCor()
        {
            InitializeComponent();
            MainClass.Initialize();
            _displayColorAddEditInstance = new DisplayColorAddEdit();
            _displayGridInstance = new DisplayGrid();
        }

        
        private void FormMinhaCor_Load(object sender, EventArgs e)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    //start with DisplayGrid
                    LoadDisplay(_displayColorAddEditInstance);
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
    }
}
