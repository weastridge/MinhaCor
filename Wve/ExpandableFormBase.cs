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
    /// expandable dialog form
    /// </summary>
    public  partial class ExpandableFormBase : Form
    {
        /// <summary>
        /// keep track of expansion state
        /// </summary>
        protected bool isExpanded = true;
        /// <summary>
        /// keep track of expansion panel size
        /// before it is shunk to nothing
        /// </summary>
        protected Size sizeExpansionPanel;

        /// <summary>
        /// expandable dialog form
        /// </summary>
        public ExpandableFormBase()
        {
            InitializeComponent();
        }

        /// <summary>
        /// on loading of form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void ExpandableFormBase_Load(object sender, EventArgs e)
        {
            //start out in contracted state
            if (isExpanded)
                toggleExpansion();
        }

        /// <summary>
        /// user pressed the More (or less) button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void buttonMore_Click(object sender, EventArgs e)
        {
            toggleExpansion();
        }

        /// <summary>
        /// toggle from expanded to not, changing
        /// caption of buttonMore
        /// </summary>
        protected virtual void toggleExpansion()
        {
            if (isExpanded)
            {
                //save size for reexpansion later
                sizeExpansionPanel = panelExpansion.Size;
                //shrink it
                this.Size = SizeFromClientSize(panelMain.Size);
                isExpanded = false;
                buttonMore.Text = "More >>";
            }
            else
            {
                //expand it
                Size clientSize = new Size();
                clientSize.Height = panelMain.Height + sizeExpansionPanel.Height;
                clientSize.Width = panelMain.Width;
                this.Size = SizeFromClientSize(clientSize);
                isExpanded = true;
                buttonMore.Text = "<< Less";
            }
        }

        /// <summary>
        /// ok
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOK_Click(object sender, EventArgs e)
        {
            onButtonOkClick();
        }

        /// <summary>
        /// when ok clicked
        /// </summary>
        protected virtual void onButtonOkClick()
        {
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            onButtonCancelClick();
        }

        protected virtual void onButtonCancelClick()
        {
            DialogResult = DialogResult.Cancel;
        }

        
    }
}