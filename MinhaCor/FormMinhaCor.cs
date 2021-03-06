﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        private DisplaySimpleSkinColorAddEdit _displaySimpleSkinColorEditInstance;
        private DisplaySkinColorAddEdit _displaySkinColorAddEdit;
        internal static FormMinhaCor Instance = null;
        internal int PanelGradientLocationX = 130; //left extent of panelGradient in DisplaySkinColorAddEdit
        private Font _fontForLabel = new Font(FontFamily.GenericSansSerif, 12, FontStyle.Bold);

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
                    _displaySimpleSkinColorEditInstance = new DisplaySimpleSkinColorAddEdit();
                    _displaySkinColorAddEdit = new DisplaySkinColorAddEdit();
                    
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
            if (display is DisplayGrid)
            {
                textBoxTitle.Visible = true;
            }
            else
            {
                textBoxTitle.Visible = false;
            }
            panelDisplayArea.Controls.Add(display);
            display.Dock = DockStyle.Fill;
            if(display is IStartable)
            {
                ((IStartable)display).Start();
            }
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

        /// <summary>
        /// load thes skin color edit diaplay
        /// </summary>
        internal void LoadDisplaySkinColorAddEdit()
        {
            LoadDisplay(_displaySkinColorAddEdit);
            _displaySkinColorAddEdit.Reset();
        }
        /// <summary>
        /// load the simple skin color edit display
        /// (not used)
        /// </summary>
        internal void LoadDisplaySimpleSkinColorAddEdit()
        {
            LoadDisplay(_displaySimpleSkinColorEditInstance);
            _displaySimpleSkinColorEditInstance.Reset();
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
                        //and then load the new file
                        MainClass.LoadColorCreations(openFileDialog1.FileName);
                        // and save it to disk
                        MainClass.SaveColorCreations(MainClass.ColorCreationsPathAndFilename);
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

        private void toolStripMenuItemEdit_Click(object sender, EventArgs e)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    if (MessageBox.Show("This is to edit the data file " +
                        "of entries.  Errors could cause this program to fail!  " +
                        "Do you want to edit the file " + 
                        MainClass.ColorCreationsLocalFileName + 
                        "?",
                        "Confirm edit data?",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Asterisk,
                        MessageBoxDefaultButton.Button2) ==
                        DialogResult.Yes)
                    {
                        if (File.Exists(MainClass.ColorCreationsPathAndFilename))
                        {
                            System.Diagnostics.Process.Start(
                                MainClass.ColorCreationsPathAndFilename);
                        }
                        else
                        {
                            MessageBox.Show("Sorry, can't find file " +
                                MainClass.ColorCreationsPathAndFilename);
                        }
                    }
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }

        private void FormMinhaCor_MouseDown(object sender, MouseEventArgs e)
        {
            string s = "hi";
        }

        private void FormMinhaCor_Click(object sender, EventArgs e)
        {
            string s = "h8";
        }

        //private void menuStrip1_Paint(object sender, PaintEventArgs e)
        //{
        //    try
        //    {
        //        show step 2
        //        e.Graphics.FillRectangle(Brushes.DodgerBlue,
        //            PanelGradientLocationX,
        //            0,
        //               20,
        //               20);
        //        int orgX = PanelGradientLocationX;
        //        int orgY = 0;
        //        e.Graphics.FillPolygon(Brushes.DodgerBlue,
        //            new Point[] {new Point(orgX,orgY),
        //            new Point(orgX + 20 + orgY),
        //            new Point(orgX + 20, orgY + 20),
        //            new Point(orgX + 10, orgY + 25),
        //            new Point(orgX , orgY + 20)});
        //        e.Graphics.DrawString("2", _fontForLabel, Brushes.Yellow,
        //            new Rectangle(
        //            PanelGradientLocationX,
        //            0,
        //            20,
        //            20));
        //    }
        //    catch (Exception er)
        //    {
        //        Wve.MyEr.Show(this, er, true);
        //    }
        //}
    }
}
