namespace Wve
{
    partial class DisplayErrorForm
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
            this.TextBoxMessage = new System.Windows.Forms.TextBox();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonMore = new System.Windows.Forms.Button();
            this.panelExpansion = new System.Windows.Forms.Panel();
            this.TextBoxDetails = new System.Windows.Forms.TextBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.panelMain.SuspendLayout();
            this.panelExpansion.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.TextBoxMessage);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(392, 150);
            this.panelMain.TabIndex = 1;
            // 
            // TextBoxMessage
            // 
            this.TextBoxMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextBoxMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextBoxMessage.Location = new System.Drawing.Point(0, 0);
            this.TextBoxMessage.Multiline = true;
            this.TextBoxMessage.Name = "TextBoxMessage";
            this.TextBoxMessage.ReadOnly = true;
            this.TextBoxMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBoxMessage.Size = new System.Drawing.Size(392, 150);
            this.TextBoxMessage.TabIndex = 1;
            this.TextBoxMessage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOK.Location = new System.Drawing.Point(314, 3);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "&OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonMore
            // 
            this.buttonMore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonMore.Location = new System.Drawing.Point(233, 3);
            this.buttonMore.Name = "buttonMore";
            this.buttonMore.Size = new System.Drawing.Size(75, 23);
            this.buttonMore.TabIndex = 4;
            this.buttonMore.Text = "<< Less";
            this.buttonMore.UseVisualStyleBackColor = true;
            this.buttonMore.Click += new System.EventHandler(this.buttonMore_Click);
            // 
            // panelExpansion
            // 
            this.panelExpansion.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelExpansion.Controls.Add(this.TextBoxDetails);
            this.panelExpansion.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelExpansion.Location = new System.Drawing.Point(0, 150);
            this.panelExpansion.Name = "panelExpansion";
            this.panelExpansion.Size = new System.Drawing.Size(392, 153);
            this.panelExpansion.TabIndex = 2;
            // 
            // TextBoxDetails
            // 
            this.TextBoxDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TextBoxDetails.Location = new System.Drawing.Point(0, 0);
            this.TextBoxDetails.Multiline = true;
            this.TextBoxDetails.Name = "TextBoxDetails";
            this.TextBoxDetails.ReadOnly = true;
            this.TextBoxDetails.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBoxDetails.Size = new System.Drawing.Size(388, 149);
            this.TextBoxDetails.TabIndex = 2;
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.buttonCopy);
            this.panelButtons.Controls.Add(this.buttonOK);
            this.panelButtons.Controls.Add(this.buttonMore);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelButtons.Location = new System.Drawing.Point(0, 303);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(392, 32);
            this.panelButtons.TabIndex = 2;
            // 
            // buttonCopy
            // 
            this.buttonCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonCopy.Location = new System.Drawing.Point(3, 3);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(75, 23);
            this.buttonCopy.TabIndex = 3;
            this.buttonCopy.Text = "&Copy";
            this.buttonCopy.UseVisualStyleBackColor = true;
            this.buttonCopy.Click += new System.EventHandler(this.buttonCopy_Click);
            // 
            // DisplayErrorForm
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonOK;
            this.ClientSize = new System.Drawing.Size(392, 335);
            this.ControlBox = false;
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panelExpansion);
            this.Controls.Add(this.panelMain);
            this.Name = "DisplayErrorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ExpandableFormBase";
            this.Load += new System.EventHandler(this.ExpandableFormBase_Load);
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelExpansion.ResumeLayout(false);
            this.panelExpansion.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelExpansion;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonMore;
        private System.Windows.Forms.Panel panelButtons;
        public System.Windows.Forms.TextBox TextBoxMessage;
        public System.Windows.Forms.TextBox TextBoxDetails;
        private System.Windows.Forms.Button buttonCopy;
    }
}