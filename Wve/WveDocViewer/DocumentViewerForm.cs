using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wve.WveDocViewer
{
    /// <summary>
    /// form for viewing documents, exposes DocumentViewer1 object
    /// </summary>
    public partial class DocumentViewerForm : Form
    {
        /// <summary>
        /// form for viewing documents, please call DocumentViewer1 methods to use
        /// </summary>
        public DocumentViewerForm()
        {
            InitializeComponent();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    DocumentViewer1.Save();
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }
    }
}
