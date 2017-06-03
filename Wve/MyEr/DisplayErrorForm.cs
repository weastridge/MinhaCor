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
    /// expandable dialog form;  intended as a class
    /// to inherit, but don't - better to copy and edit
    /// </summary>
    public  partial class DisplayErrorForm : Form
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
        public DisplayErrorForm()
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
            //start out in expanded state
            if (!isExpanded)
                toggleExpansion();
            //don't have text selected
            if (TextBoxMessage.Text.Length > 0)
            {
                TextBoxMessage.SelectionStart = 0;
            }
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
                //figure new size
                Size clientSize = new Size();
                clientSize.Height = panelMain.Height + panelButtons.Height;
                clientSize.Width = panelMain.Width;
                //shrink it
                panelExpansion.Height = 0;
                this.Size = SizeFromClientSize(clientSize);
                isExpanded = false;
                buttonMore.Text = "&More >>";
            }
            else
            {
                //i.e. not currently expanded
                //save panelButtons ht
                int panelButtonsHeight = panelButtons.Height;
                //expand it
                //hide to prevent flicker
                panelExpansion.Visible = false;
                panelExpansion.Size = sizeExpansionPanel;
                Size clientSize = new Size();
                clientSize.Height = panelMain.Height + sizeExpansionPanel.Height + panelButtonsHeight;
                clientSize.Width = panelMain.Width;
                this.Size = SizeFromClientSize(clientSize);
                isExpanded = true;
                buttonMore.Text = "<< Less";
                panelExpansion.Visible = true;
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

        //copy contents to clipboard
        private void buttonCopy_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(TextBoxMessage.Text);
                if (TextBoxDetails.Text.Trim() != string.Empty)
                {
                    sb.Append(Environment.NewLine);
                    sb.Append("Details:");
                    sb.Append(Environment.NewLine);
                    sb.Append(TextBoxDetails.Text);
                }
                Clipboard.SetData(DataFormats.Text, sb.ToString());
            }
            catch (Exception er)
            {
                MessageBox.Show("Sorry, couldn't copy to clipboard:  " +
                    er.ToString());
                //ignore error- we're already handling an error
            }
        }

        
    }
}