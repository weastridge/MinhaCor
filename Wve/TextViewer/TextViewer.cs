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
    /// form to display text in rich text box
    /// </summary>
    public partial class TextViewer : Form
    {

        /// <summary>
        /// if true, the text is to be shown as rtf
        /// </summary>
        private bool isRTF;

        /// <summary>
        /// if true, scroll to bottom of textbox when showing
        /// </summary>
        public bool ScrollToBottomOnShow = false;

        /// <summary>
        /// handler to be called when ButtonCustomButton is clicked,
        /// which is called with this text viewer as sender
        /// </summary>
        public EventHandler CustomButtonHandler;

        #region constructors
        /// <summary>
        /// Don't create instance directly unless using as dialog box
        /// Best to call instance method ShowText or ShowRTF to show only.
        /// Exposes RichTextBox1, ButtonCustomButton, ScrollToBottomOnShow,
        /// and delegate CustomButtonHandler
        /// </summary>
        public TextViewer(string contents, bool isRTF)
        {
            this.isRTF = isRTF;
            InitializeComponent();
            if (isRTF)
            {
                //show as rtf
                RichTextBox1.Rtf = contents;
            }
            else
            {
                //show as plaintext
                RichTextBox1.Text = contents;
            }
        }

        /// <summary>
        /// instance of TextViewer to use as a dialog box; if isDialog
        /// is true then an OK button shows and you can query TextBoxContents
        /// to see changes.  
        /// Exposes RichTextBox1, ButtonCustomButton, ScrollToBottomOnShow,
        /// and delegate CustomButtonHandler
        /// </summary>
        /// <param name="contents"></param>
        /// <param name="isRTF"></param>
        /// <param name="isDialog">set to true to enable an OK button</param>
        public TextViewer(string contents, bool isRTF, bool isDialog)
        {
            this.isRTF = isRTF;
            InitializeComponent();
            if (isRTF)
            {
                //show as rtf
                RichTextBox1.Rtf = contents;
            }
            else
            {
                //show as plaintext
                RichTextBox1.Text = contents;
            }
            if (isDialog)
            {
                ButtonOK.Visible = true;
                ButtonClose.Text = "Cancel";
                RichTextBox1.ReadOnly = false;
            }
            else
            {
                ;//leave ButtonOK invisible 
                //and Button Close text "Close"
                // and RichTextBox1 read only
            }
        }

        #endregion //constructors

        /// <summary>
        /// scroll to bottom of text when shown, if desired
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextViewer_Load(object sender, EventArgs e)
        {
            if ((ScrollToBottomOnShow) && RichTextBox1.Text.Length > 0)
            {
                RichTextBox1.SelectionStart = RichTextBox1.Text.Length - 1;
                Application.DoEvents();
            }
        }

        //close box
        private void buttonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// show text in viewer with just Close
        /// button and no ok,cancel buttons
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="text"></param>
        public static DialogResult ShowText(
            string caption,
            string text)
        {
            return ShowText(caption, text, false);
        }
        /// <summary>
        /// show text
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="text"></param>
        /// <param name="useOkCancelButtons"></param>
        /// <returns></returns>
        public static DialogResult ShowText(
            string caption,
            string text,
            bool useOkCancelButtons)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    TextViewer viewer = new TextViewer(text, false);
                    viewer.Text = caption;
                    if (useOkCancelButtons)
                    {
                        viewer.ButtonOK.Visible = true;
                        viewer.ButtonClose.Text = "Cancel";
                    }
                    DialogResult result = viewer.ShowDialog();
                    return result;
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show("TextViewer", er, true);
                    return DialogResult.Abort;
                }
            }
        }

        /// <summary>
        /// show rich text format markup in a rich text box
        /// (displays error message if rtf is bad)
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="rtf"></param>
        public static DialogResult ShowRtf(
            string caption,
            string rtf)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    TextViewer viewer = new TextViewer(rtf, true);
                    viewer.Text = caption;
                    DialogResult result = viewer.ShowDialog();
                    return result;
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show("TextViewer", er, true);
                    return DialogResult.Abort;
                }
            }
        }

        /*
        private void TextViewer_Resize(object sender, EventArgs e)
        {

         }*/
         

        /// <summary>
        /// search for text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFind_Click(object sender, EventArgs e)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    //start with beginning if no text is selected or 
                    // with next character if some already selected
                    int startPoint = RichTextBox1.SelectionLength == 0 ?
                        RichTextBox1.SelectionStart : RichTextBox1.SelectionStart + 1;
                    //if matches
                    if (RichTextBox1.Text.ToUpper().IndexOf(textBoxFind.Text.ToUpper(),
                        startPoint) > -1)
                    {
                        RichTextBox1.Select(RichTextBox1.Text.ToUpper().IndexOf(textBoxFind.Text.ToUpper(),
                            startPoint), textBoxFind.Text.Length);
                        RichTextBox1.ScrollToCaret();
                        RichTextBox1.Focus();
                    }
                    else
                    {
                        if (MessageBox.Show("Try again from the beginning?", "Reached end of text.",
                            MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) ==
                            DialogResult.OK)
                        {
                            //restart
                            startPoint = 0;
                            //if matches
                            if (RichTextBox1.Text.ToUpper().IndexOf(textBoxFind.Text.ToUpper(),
                                startPoint) > -1)
                            {
                                RichTextBox1.Select(RichTextBox1.Text.ToUpper().IndexOf(textBoxFind.Text.ToUpper(),
                                    startPoint), textBoxFind.Text.Length);
                                RichTextBox1.ScrollToCaret();
                                RichTextBox1.Focus();
                            }
                            else
                            {
                                MessageBox.Show("Text not found.");
                            }
                        }
                    }
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }

        /// <summary>
        /// key down handler for textboxFind
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxFind_KeyDown(object sender, KeyEventArgs e)
        {
            //call Find if enter is pressed
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    if (e.KeyData == Keys.Enter)
                    {
                        buttonFind_Click(sender, e);
                        e.Handled = true;
                    }
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }

        /// <summary>
        /// optionally, can paste clipboard contents into text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pasteFromClipboard(object sender, EventArgs e)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    string rtf = Clipboard.GetText(TextDataFormat.Rtf);
                    string plainText = Clipboard.GetText(TextDataFormat.Text);
                    if (rtf != null)
                    {
                        RichTextBox1.SelectedRtf = rtf;
                    }
                    else if (plainText != null)
                    {
                        RichTextBox1.SelectedText = plainText;
                    }
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }

        //copy selected text (or all text) to clipboard
        private void buttonCopy_Click(object sender, EventArgs e)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    if (isRTF)
                    {
                        if ((RichTextBox1.SelectedRtf != null) &&
                            (RichTextBox1.SelectedRtf.Length > 0))
                        {
                            Clipboard.SetData(DataFormats.Rtf, RichTextBox1.SelectedRtf);
                        }
                        else
                        {
                            Clipboard.SetData(DataFormats.Rtf, RichTextBox1.Rtf);
                        }
                    }
                    else
                    {
                        if ((RichTextBox1.SelectedText != null) &&
                            (RichTextBox1.SelectedText.Length > 0))
                        {
                            Clipboard.SetData(DataFormats.Text, RichTextBox1.SelectedText);
                        }
                        else
                        {
                            Clipboard.SetData(DataFormats.Text, RichTextBox1.Text);
                        }
                    }

                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }

        /// <summary>
        /// OK is only visible option if this form is in dialog mode
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// call any methods that have been attached to the
        /// EventHandler delegate for ButtonCustomButton
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonCustomButton_Click(object sender, EventArgs e)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    if (CustomButtonHandler != null)
                    {
                        //call the delegate(s)
                        CustomButtonHandler(this, e);
                        //put focus back on text box
                        RichTextBox1.Focus();
                    }

                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, 
                        "Error on delegate handler for button click.",
                        er, 
                        true);
                }
            }
        }
    }
}