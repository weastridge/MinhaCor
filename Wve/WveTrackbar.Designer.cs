namespace Wve
{
    partial class WveTrackbar
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // WveTrackbar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Name = "WveTrackbar";
            this.Size = new System.Drawing.Size(55, 442);
            this.Load += new System.EventHandler(this.WveTrackbar_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.WveTrackbar_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WveTrackbar_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WveTrackbar_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.WveTrackbar_MouseUp);
            this.Resize += new System.EventHandler(this.WveTrackbar_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
