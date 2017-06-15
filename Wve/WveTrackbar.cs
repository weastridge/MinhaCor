using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wve
{
    /// <summary>
    /// custom trackbar, not useful at this point
    /// </summary>
    public class WveTrackbar:System.Windows.Forms.TrackBar
    {
        /// <summary>
        /// paint
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.FillRectangle(System.Drawing.Brushes.Black,
                ClientRectangle);
        }
    }
}
