using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;

namespace Wve
{
    /// <summary>
    /// some tools for working with rtf
    /// </summary>
    public static class WveRtfStuff
    {
        /// <summary>
        /// sets number of leading and trailing paragraphs
        /// (i.e. carriage returns) in the RichTextBox's text
        /// </summary>
        /// <param name="rtb"></param>
        /// <param name="leadingPars"></param>
        /// <param name="trailingPars"></param>
        public static void SetLeadingAndTrailingPars(
            RichTextBox rtb, int leadingPars, int trailingPars)
        {
            int existingLeadingPars = 0; //to begin
            int existingTrailingPars = 0; //to begin
            char[] par = new char[] { (char)0x000D };
            //;object existingClipboardContents;

            //count existing leading pars
            while (rtb.Find(par,
                existingLeadingPars, //start where last iteration left off
                existingLeadingPars + 1 //and look at only one character 
                ) > -1) //-1 indicates not found
            {
                existingLeadingPars++;
            }

            //count existing trailing pars
            while ((rtb.Text.Length > existingLeadingPars + existingTrailingPars) &&
                (rtb.Find(par,
                rtb.Text.Length - 1 - existingTrailingPars,
                rtb.Text.Length - existingTrailingPars)
                > -1))
            {
                existingTrailingPars++;
            }

            //now rebuild the text with desired
            // number of pars


            //select text without leading and trailing pars
            rtb.Select(existingLeadingPars,
                rtb.Text.Length - existingLeadingPars - existingTrailingPars);
            //copy body
            string body = "";
            if (rtb.SelectionLength > 0)
            {
                body = rtb.SelectedRtf;
            }
            //rebuild the rtb
            rtb.Clear();
            //add leading pars
            for (int i = 0; i < leadingPars; i++)
            {
                rtb.AppendText("\r");
            }
            //add middle
            rtb.SelectedRtf = body;
            //and end
            /*
            //(but notice the rich text box shows an extra line
            // if the pasted text has no length, so decrememt if so)
            if ((rtb.Text.Length == leadingPars) &&
                (trailingPars > 0))
            {
                trailingPars--;
            }
             */

            for (int i = 0; i < trailingPars; i++)
            {
                rtb.AppendText("\r");
            }
        }

        /// <summary>
        /// true if material looks like rich text
        /// formatted text
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        public static bool IsRtf(string material)
        {
            return (material.Trim().StartsWith(@"{\rtf"));
        }

        /// <summary>
        /// insert material into current cursor position
        /// of rich text box, as RTF if looks like rtf, or 
        /// as text if not.  Returns true if was inserted as rtf
        /// </summary>
        /// <param name="rtbox"></param>
        /// <param name="material"></param>
        /// <returns></returns>
        public static bool InsertRtf(RichTextBox rtbox, string material)
        {
            //see if looks like rtf
            bool loadedAsRTF = false; //unless changed
            //(like IsRtf() above, but rewritten here for speed)
            if (material.StartsWith(@"{\rtf"))
            {
                loadedAsRTF = true;
            }

            //append material
            if ((material != null) || (material != ""))
            {
                //save as plaintext if doesn't look like rtf
                if (!loadedAsRTF)
                {
                    rtbox.SelectedText = material;
                }
                else
                {
                    rtbox.SelectedRtf = material;
                }
            }
            return loadedAsRTF;
        }

        /// <summary>
        /// appends specified text material into rich text box; as
        /// rtf if it is valid rtf material, or as text if not, returning
        /// bool to indicate if the material was loaded as rtf
        /// </summary>
        /// <param name="rtbox"></param>
        /// <param name="material"></param>
        /// <returns>true if material loaded as rtf</returns>
        public static bool AppendRtf(RichTextBox rtbox, string material)
        {
            //((also consider Clipboard
            // as another way to do this...))

            //move selection to end of existing text
            if (rtbox.Text.Length > 0)
            {
                rtbox.SelectionStart = rtbox.Text.Length;
                rtbox.SelectionLength = 0;
            }

            bool loadedAsRTF = false; //unless changed
            if (material.StartsWith(@"{\rtf"))
            {
                loadedAsRTF = true;
            }

            //append material
            if ((material != null) || (material != ""))
            {
                //save as plaintext if doesn't look like rtf
                if (!loadedAsRTF)
                {
                    rtbox.SelectedText = material;
                }
                else
                {
                    rtbox.SelectedRtf = material;

                    /*  This step may be superfluous...
                    //first try as rtf insertion
                    try
                    {
                        rtbox.SelectedRtf = material;
                    }
                    catch
                    {
                        //use the clipboard to load it so if rtf markup is bad it 
                        //just loads as text instead of throwing exception
                        //first store current clipboard
                        object originalClpContents = Clipboard.GetDataObject();
                        //allow for text boxes that are read-only
                        bool existingSetting = rtbox.ReadOnly;
                        try
                        {
                            Clipboard.SetText(material, TextDataFormat.Rtf);
                            //rtbox.Clear();
                            rtbox.ReadOnly = false;
                            rtbox.Paste(DataFormats.GetFormat(DataFormats.Rtf));
                        }
                        finally
                        {
                            //restore clipboard
                            Clipboard.SetDataObject(originalClpContents);
                            //restore readonlyl
                            rtbox.ReadOnly = existingSetting;
                        }

                    } //from try catch
                     */
                }//from if loaded as rtf or not

                /*
                //finally, remove the extra trailing paragraph symbol the paste 
                // operation puts in automatically
                rtbox.Select(0, rtbox.Text.Length - 1);
                string body = rtbox.SelectedRtf;
                rtbox.Clear();
                rtbox.SelectedRtf = body;
                 */
            }
            return loadedAsRTF;
        }

        /// <summary>
        /// insert given image into rich text box at SelectedRTF insertion point
        /// </summary>
        /// <param name="img"></param>
        /// <param name="rtb"></param>
        public static void AppendImageToRtb(
            Image img,
            RichTextBox rtb)
        {
            //this implementation uses clipboard
            // A more elegant, complicated and slow method is to convert image to wmf
            // and add rtf control words, etc to paste in...

            //save existing clp contennts
            IDataObject preexistingClipbdContents = Clipboard.GetDataObject();
            try
            {
                Clipboard.SetImage(img);
                rtb.Paste();
            }
            finally
            {
                //restore clipboard contents
                Clipboard.SetDataObject(preexistingClipbdContents);
            }
        }
    }
}
