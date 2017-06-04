namespace Wve
{
    partial class ExpandableFormBase
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        protected virtual void InitializeComponent()
        {
            this.panelMain = new System.Windows.Forms.Panel();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonMore = new System.Windows.Forms.Button();
            this.panelExpansion = new System.Windows.Forms.Panel();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.buttonOK);
            this.panelMain.Controls.Add(this.buttonCancel);
            this.panelMain.Controls.Add(this.buttonMore);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(482, 156);
            this.panelMain.TabIndex = 0;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(235, 126);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "&OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(316, 126);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonMore
            // 
            this.buttonMore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMore.Location = new System.Drawing.Point(397, 126);
            this.buttonMore.Name = "buttonMore";
            this.buttonMore.Size = new System.Drawing.Size(75, 23);
            this.buttonMore.TabIndex = 0;
            this.buttonMore.Text = "More >>";
            this.buttonMore.UseVisualStyleBackColor = true;
            this.buttonMore.Click += new System.EventHandler(this.buttonMore_Click);
            // 
            // panelExpansion
            // 
            this.panelExpansion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelExpansion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExpansion.Location = new System.Drawing.Point(0, 156);
            this.panelExpansion.Name = "panelExpansion";
            this.panelExpansion.Size = new System.Drawing.Size(482, 302);
            this.panelExpansion.TabIndex = 1;
            // 
            // ExpandableFormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(482, 458);
            this.ControlBox = false;
            this.Controls.Add(this.panelExpansion);
            this.Controls.Add(this.panelMain);
            this.Name = "ExpandableFormBase";
            this.Text = "ExpandableFormBase";
            this.Load += new System.EventHandler(this.ExpandableFormBase_Load);
            this.panelMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Panel panelMain;
        protected System.Windows.Forms.Panel panelExpansion;
        protected System.Windows.Forms.Button buttonOK;
        protected System.Windows.Forms.Button buttonCancel;
        protected System.Windows.Forms.Button buttonMore;
    }
}