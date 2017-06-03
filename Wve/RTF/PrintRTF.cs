using System;
using System.Collections.Generic;
using System.Text;

namespace Wve
{
    /// <summary>
    /// Object for printing rich text
    /// Some of this code is adapted from MSDN example called 
    /// Getting WYSIWYG Print Results from a .NET RichTextBox
    /// http://msdn2.microsoft.com/en-us/library/ms996492.aspx
    /// author Martin Muller at 4voice_AG www.4voice.de
    /// martin.h.mueller@t-online.de
    /// </summary>
    public class PrintRTF
    {
        /// <summary>
        /// extended rich text box control from msdn example
        /// </summary>
        private RichTextBoxEx richTextBoxEx;
        /// <summary>
        /// PrintDocument to print
        /// </summary>
        private System.Drawing.Printing.PrintDocument printDocument;
        /// <summary>
        /// offset of first character to start printing from
        /// </summary>
        private int m_nFirstCharOnPage;

        /// <summary>
        /// Print the rich text via the given PrintDocument, 
        /// which should not have print event handlers pre-defined!
        /// </summary>
        /// <param name="rtf">text to print.  Prints plaintext if doesn't start like rtf, but
        /// throws exception if looks like rtf but has errors in it.</param>
        /// <param name="printDocument"></param>
        public PrintRTF(string rtf, System.Drawing.Printing.PrintDocument printDocument)
        {
            this.printDocument = printDocument;
            this.richTextBoxEx = new RichTextBoxEx();
            //try to insert the string as rtf if looks like rtf
            if (rtf.Trim().StartsWith(@"{\rtf"))
            {
                //this throws exception if invalid format
                richTextBoxEx.SelectedRtf = rtf;
            }
            else
            {
                //it must be plain text since doesn't look like rtf
                richTextBoxEx.SelectedText = rtf;
            }
            commonConstructor();
        }

        /// <summary>
        /// print the contents of given extended rich text box 
        ///  to the given
        /// PrintDocument
        /// </summary>
        /// <param name="richTextBoxEx">(created my Martin Muller for MSDN, 
        /// defined locally 
        /// in Wve.dll as Wve.RichTextBoxEx)</param>
        /// <param name="printDocument"></param>
        public PrintRTF(RichTextBoxEx richTextBoxEx,
            System.Drawing.Printing.PrintDocument printDocument)
        {
            this.printDocument = printDocument;
            this.richTextBoxEx = richTextBoxEx;
            commonConstructor();
        }

        /// <summary>
        /// called by any constructors once they have established a printDocument
        /// </summary>
        private void commonConstructor()
        {
            printDocument.BeginPrint += 
                new System.Drawing.Printing.PrintEventHandler(printDocument_BeginPrint);
            printDocument.PrintPage += 
                new System.Drawing.Printing.PrintPageEventHandler(printDocument_PrintPage);
            printDocument.EndPrint += 
                new System.Drawing.Printing.PrintEventHandler(printDocument_EndPrint);
        }
            
        /// <summary>
        /// print the existing document
        /// </summary>
        public void Print()
        {
            if (printDocument != null)
                printDocument.Print();
        }

        /// <summary>
        /// print the existing document with preview dialog
        /// </summary>
        /// <param name="dlg"></param>
        public void Print(System.Windows.Forms.PrintDialog dlg)
        {
            if (printDocument != null)
            {
                dlg.Document = printDocument;
                //required for dialog to work in Windows 7:
                dlg.UseEXDialog = true;
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    printDocument.Print();
                }
            }
        }

        private void printDocument_BeginPrint(object sender, 
            System.Drawing.Printing.PrintEventArgs e)
        {
            // Start at the beginning of the text
            m_nFirstCharOnPage = 0;
        }

        private void printDocument_PrintPage(object sender, 
            System.Drawing.Printing.PrintPageEventArgs e)
        {
            // To print the boundaries of the current page margins
            // uncomment the next line:
            //e.Graphics.DrawRectangle(System.Drawing.Pens.Blue, e.MarginBounds);

            // make the RichTextBoxEx calculate and render as much text as will
            // fit on the page and remember the last character printed for the
            // beginning of the next page
            m_nFirstCharOnPage = richTextBoxEx.FormatRange(false,
                e,
                m_nFirstCharOnPage,
                richTextBoxEx.TextLength);

            // check if there are more pages to print
            if (m_nFirstCharOnPage < richTextBoxEx.TextLength)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;
        }

        private void printDocument_EndPrint(object sender, 
            System.Drawing.Printing.PrintEventArgs e)
        {
            // Clean up cached information
            richTextBoxEx.FormatRangeDone();
        }

    }
}
