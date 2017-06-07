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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisplayGrid));
            this.panelHeader = new System.Windows.Forms.Panel();
            this.buttonNext = new System.Windows.Forms.Button();
            this.buttonPrior = new System.Windows.Forms.Button();
            this.panelGridDisplay = new System.Windows.Forms.Panel();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.buttonNext);
            this.panelHeader.Controls.Add(this.buttonPrior);
            resources.ApplyResources(this.panelHeader, "panelHeader");
            this.panelHeader.Name = "panelHeader";
            // 
            // buttonNext
            // 
            resources.ApplyResources(this.buttonNext, "buttonNext");
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // buttonPrior
            // 
            resources.ApplyResources(this.buttonPrior, "buttonPrior");
            this.buttonPrior.Name = "buttonPrior";
            this.buttonPrior.UseVisualStyleBackColor = true;
            this.buttonPrior.Click += new System.EventHandler(this.buttonPrior_Click);
            // 
            // panelGridDisplay
            // 
            resources.ApplyResources(this.panelGridDisplay, "panelGridDisplay");
            this.panelGridDisplay.Name = "panelGridDisplay";
            this.panelGridDisplay.Paint += new System.Windows.Forms.PaintEventHandler(this.panelGridDisplay_Paint);
            this.panelGridDisplay.Resize += new System.EventHandler(this.panelGridDisplay_Resize);
            // 
            // DisplayGrid
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelGridDisplay);
            this.Controls.Add(this.panelHeader);
            this.Name = "DisplayGrid";
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
