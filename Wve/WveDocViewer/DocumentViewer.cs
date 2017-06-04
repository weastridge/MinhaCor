using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Xsl;
using System.Xml;

namespace Wve.WveDocViewer
{
    /// <summary>
    /// user control to view documents
    /// </summary>
    public partial class DocumentViewer : UserControl
    {
        /// <summary>
        /// the data to view
        /// </summary>
        protected byte[] _documentData = null;
        ///// <summary>
        ///// the stream to be viewed
        ///// </summary>
        //protected Stream _documentStream = null;
        ///// <summary>
        ///// stream of document to be viewed
        ///// </summary>
        //public Stream DocumentStream
        //{
        //    get
        //    {
        //        return _documentStream;
        //    }

        //    set
        //    {
        //        _documentStream = value;
        //    }
        //}
        /// <summary>
        /// how to show the document
        /// </summary>
        protected WveDocTypes _documentType = WveDocTypes.unspecified;
        ///// <summary>
        ///// type of document to view
        ///// </summary>
        //public WveDocTypes DocumentType
        //{
        //    get
        //    {
        //        return _documentType;
        //    }

        //    set
        //    {
        //        _documentType = value;
        //    }
        //}

        /// <summary>
        /// caption to show in title bar
        /// </summary>
        protected string _caption = string.Empty;

        /// <summary>
        /// name given or to be given to document to show
        /// </summary>
        public string FileName
        {
            get
            {
                return _fileName;
            }

            set
            {
                _fileName = value;
            }
        }
        private string _fileName = string.Empty;

        #region constructors
        /// <summary>
        /// user control to view documents
        /// </summary>
        public DocumentViewer()
        {
            InitializeComponent();
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    richTextBoxEx1.Dock = DockStyle.Fill;
                    pictureBox1.Dock = DockStyle.Fill;
                    webBrowser1.Dock = DockStyle.Fill;
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }

        #endregion constructors

        /// <summary>
        /// show currently loaded document if any
        /// </summary>
        /// <param name="showAsPlaintext">override document type and show as text</param>
        public void ShowDocument(bool showAsPlaintext)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    resetViewer();
                    if((Parent != null) &&
                        (Parent.Parent != null) &&
                        (Parent.Parent is Form))
                    {
                        ((Form)Parent.Parent).Text = _caption;
                    }
                    if (_documentData != null)
                    {
                        if (showAsPlaintext ||
                            (_documentType == WveDocTypes.plaintext) ||
                            (_documentType == WveDocTypes.unspecified))
                        {
                            //make string from array 
                            richTextBoxEx1.AppendText(
                                Encoding.UTF8.GetString(_documentData));
                            richTextBoxEx1.Visible = true;
                        }
                        else if(_documentType == WveDocTypes.xml)
                        {
                            //for now  will show as plaintext;  later 
                            // maybe show as treeviw...
                            richTextBoxEx1.AppendText(
                                Encoding.UTF8.GetString(_documentData));
                            richTextBoxEx1.Visible = true;
                        }
                        else if(_documentType == WveDocTypes.html)
                        {
                            webBrowser1.Visible = true;
                            //can only set DocumentText once after navigating
                            webBrowser1.Navigate("about:blank");
                            if(webBrowser1.Document != null)
                            {
                                webBrowser1.Document.Write(string.Empty);
                            }
                            webBrowser1.DocumentText = 
                                Encoding.UTF8.GetString(_documentData);
                        }
                        else if((_documentType == WveDocTypes.zip) ||
                            (_documentType ==WveDocTypes.unspecified))
                        {
                            SaveFileDialog dlg = new SaveFileDialog();
                            dlg.FileName = FileName;
                            if(dlg.ShowDialog() == DialogResult.OK)
                            {
                                using (FileStream fs = new FileStream(dlg.FileName, FileMode.Create))
                                {
                                    fs.Write(_documentData, 0, _documentData.Length);
                                    fs.Flush();
                                }
                                System.Diagnostics.ProcessStartInfo psi = 
                                    new System.Diagnostics.ProcessStartInfo();
                                FileInfo fi = new FileInfo(dlg.FileName);
                                DirectoryInfo di = fi.Directory;
                                psi.FileName = di.FullName;
                                psi.UseShellExecute = true;
                                //psi.Verb = "Open";
                                System.Diagnostics.Process.Start(psi);
                            }
                        }
                        else if(_documentType == WveDocTypes.ccda)
                        {
                            //we'll use our own xml transform to show it as html
                            try
                            {
                                StringBuilder sb = new StringBuilder();
                                var myXslTrans = new XslCompiledTransform();
                                XsltSettings transformSettings = new XsltSettings();
                                //this is security opening if the xsl (CDA.xsl) isn't trusted!
                                transformSettings.EnableDocumentFunction = true;

                                using (MemoryStream msTrans = new MemoryStream(Encoding.UTF8.GetBytes(Wve.Properties.Resources.CDA)))
                                //; shows tables but in French! WCDA.Properties.Resources.CDA_stylesheetFromArtDecor)))
                                {
                                    using (XmlReader xslTransReader = XmlReader.Create(msTrans))// , readerSettings))
                                    {
                                        myXslTrans.Load(xslTransReader, transformSettings, new XmlUrlResolver());
                                        using (StringReader sr = new StringReader(Encoding.UTF8.GetString(_documentData)))
                                        {
                                            using (XmlReader xr = XmlReader.Create(sr))
                                            {
                                                using (XmlWriter xw = XmlWriter.Create(sb))
                                                {
                                                    myXslTrans.Transform(xr, xw);
                                                }
                                            }
                                        }
                                    }
                                }

                                webBrowser1.Visible = true;
                                //can only set DocumentText once after navigating
                                webBrowser1.Navigate("about:blank");
                                if (webBrowser1.Document != null)
                                {
                                    webBrowser1.Document.Write(string.Empty);
                                }
                                webBrowser1.DocumentText = sb.ToString();
                            }
                            catch (Exception er)
                            {
                                throw new Exception(
                                    "Error transforming xml to html view for this ccda document.", er);
                            }
                        }
                    }//from if document data not null
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }

        /// <summary>
        /// show string 
        /// </summary>
        /// <param name="document"></param>
        /// <param name="caption"></param>
        /// <param name="doctype">type</param>
        /// <param name="showAsPlaintext">if true show as plaintext regardless of doctype</param>
        public void ShowDocument(string document, 
            string caption,
            WveDocTypes doctype, 
            bool showAsPlaintext)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    _caption = caption;
                    _documentType = doctype;
                    _documentData = null;
                    _documentData = Encoding.UTF8.GetBytes(document);
                    ShowDocument(showAsPlaintext);
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }

        /// <summary>
        /// show byte array 
        /// </summary>
        /// <param name="document"></param>
        /// <param name="caption"></param>
        /// <param name="doctype">type</param>
        /// <param name="showAsPlaintext">if true show as plaintext regardless of doctype</param>
        public void ShowDocument(byte[] document,
            string caption,
            WveDocTypes doctype,
            bool showAsPlaintext)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    _caption = caption;
                    _documentType = doctype;
                    _documentData = null;
                    _documentData = document;
                    ShowDocument(showAsPlaintext);
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }
        /// <summary>
        /// show file
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="caption"></param>
        /// <param name="doctype"></param>
        public void ShowDocument(string filename, string caption, WveDocTypes doctype)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    _caption = caption;
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }

        /// <summary>
        /// show stream
        /// </summary>
        /// <param name="document"></param>
        /// <param name="caption"></param>
        /// <param name="doctype"></param>
        public void ShowDocument(Stream document, string caption, WveDocTypes doctype)
        {
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    _caption = caption;
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }
        /// <summary>
        /// reset controls to invisible and
        /// disposes current documentStream
        /// </summary>
        private void resetViewer()
        {
            richTextBoxEx1.Visible = false;
            richTextBoxEx1.Clear();
            webBrowser1.Visible = false;
            webBrowser1.Navigate("about:blank");

            ////dispose of webBrowser and recreate it each time?
            //if(webBrowser1 != null)
            //{
            //    webBrowser1.Visible = false;
            //    webBrowser1.Dispose();
            //}
            pictureBox1.Visible = false;
            pictureBox1.Image = null;
        }

        /// <summary>
        /// save current document
        /// </summary>
        public void Save()
        {
            //if rtf can use savefile functiion...
            using (Wve.HourglassCursor waitCursor = new Wve.HourglassCursor())
            {
                try
                {
                    SaveFileDialog dlg = new SaveFileDialog();
                    if (!string.IsNullOrEmpty(_fileName))
                    {
                        try
                        {
                            dlg.FileName = _fileName;
                        }
                        catch
                        {
                            //go on
                        }
                    }
                    if(dlg.ShowDialog() == DialogResult.OK)
                    {
                        using (FileStream fs = new FileStream(dlg.FileName, FileMode.Create))
                        {
                            fs.Write(_documentData, 0, _documentData.Length);
                            fs.Flush();
                        }
                    }
                }
                catch (Exception er)
                {
                    Wve.MyEr.Show(this, er, true);
                }
            }
        }

        public void SetViewType()
        {

        }

        /// <summary>
        /// called from dispose() in Designer, this
        /// disposes anything disposable we create
        /// outside of the designer
        /// </summary>
        /// <param name="disposing"></param>
        protected void manualDispose(bool disposing)
        {
            //was going to dispose of stream object
            // but currently not using one
        }

    }
}
