using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wve.WveDocViewer
{
    /// <summary>
    /// main class
    /// </summary>
    public class WveDocViewerClass
    {
    }

    /// <summary>
    /// types of documents to view
    /// </summary>
    public enum WveDocTypes
    {
        /// <summary>
        /// not specified
        /// </summary>
        unspecified,
        /// <summary>
        /// plain text
        /// </summary>
        plaintext,
        /// <summary>
        /// rich text format
        /// </summary>
        rtf,
        /// <summary>
        /// pdf
        /// </summary>
        pdf,
        /// <summary>
        /// hypertext markup lanuage
        /// </summary>
        html,
        /// <summary>
        /// jpeg
        /// </summary>
        jpeg,
        /// <summary>
        /// xml to show plain
        /// </summary>
        xml,
        /// <summary>
        /// xml to show as ccda transform to html
        /// </summary>
        ccda,
        /// <summary>
        /// zip file
        /// </summary>
        zip
    }
}
