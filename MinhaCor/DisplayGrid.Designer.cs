namespace MinhaCor
{
    partial class DisplayGrid
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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.buttonPrior = new System.Windows.Forms.Button();
            this.buttonNext = new System.Windows.Forms.Button();
            this.panelGridDisplay = new System.Windows.Forms.Panel();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.buttonNext);
            this.panelHeader.Controls.Add(this.buttonPrior);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1008, 33);
            this.panelHeader.TabIndex = 0;
            // 
            // buttonPrior
            // 
            this.buttonPrior.Location = new System.Drawing.Point(4, 4);
            this.buttonPrior.Name = "buttonPrior";
            this.buttonPrior.Size = new System.Drawing.Size(29, 23);
            this.buttonPrior.TabIndex = 0;
            this.buttonPrior.Text = "<-";
            this.buttonPrior.UseVisualStyleBackColor = true;
            // 
            // buttonNext
            // 
            this.buttonNext.Location = new System.Drawing.Point(976, 4);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(29, 23);
            this.buttonNext.TabIndex = 1;
            this.buttonNext.Text = "->";
            this.buttonNext.UseVisualStyleBackColor = true;
            // 
            // panelGridDisplay
            // 
            this.panelGridDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGridDisplay.Location = new System.Drawing.Point(0, 33);
            this.panelGridDisplay.Name = "panelGridDisplay";
            this.panelGridDisplay.Size = new System.Drawing.Size(1008, 624);
            this.panelGridDisplay.TabIndex = 1;
            this.panelGridDisplay.Click += new System.EventHandler(this.panelGridDisplay_Click);
            this.panelGridDisplay.Paint += new System.Windows.Forms.PaintEventHandler(this.panelGridDisplay_Paint);
            // 
            // DisplayGrid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelGridDisplay);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "DisplayGrid";
            this.Size = new System.Drawing.Size(1008, 657);
            this.Load += new System.EventHandler(this.DisplayGrid_Load);
            this.panelHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Button buttonPrior;
        private System.Windows.Forms.Panel panelGridDisplay;
    }
}
