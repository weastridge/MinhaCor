namespace MinhaCor
{
    partial class FormMainTest
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
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelControls = new System.Windows.Forms.Panel();
            this.buttonReset = new System.Windows.Forms.Button();
            this.trackBarLightness = new System.Windows.Forms.TrackBar();
            this.labelB = new System.Windows.Forms.Label();
            this.labelG = new System.Windows.Forms.Label();
            this.labelR = new System.Windows.Forms.Label();
            this.labelRadians = new System.Windows.Forms.Label();
            this.labelDelta = new System.Windows.Forms.Label();
            this.labelUpDown = new System.Windows.Forms.Label();
            this.labelY = new System.Windows.Forms.Label();
            this.labelX = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panelBackground = new System.Windows.Forms.Panel();
            this.panelMain = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.panelControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLightness)).BeginInit();
            this.panelBackground.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1200, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // panelControls
            // 
            this.panelControls.Controls.Add(this.buttonReset);
            this.panelControls.Controls.Add(this.trackBarLightness);
            this.panelControls.Controls.Add(this.labelB);
            this.panelControls.Controls.Add(this.labelG);
            this.panelControls.Controls.Add(this.labelR);
            this.panelControls.Controls.Add(this.labelRadians);
            this.panelControls.Controls.Add(this.labelDelta);
            this.panelControls.Controls.Add(this.labelUpDown);
            this.panelControls.Controls.Add(this.labelY);
            this.panelControls.Controls.Add(this.labelX);
            this.panelControls.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelControls.Location = new System.Drawing.Point(0, 24);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(172, 633);
            this.panelControls.TabIndex = 1;
            // 
            // buttonReset
            // 
            this.buttonReset.Location = new System.Drawing.Point(16, 16);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 9;
            this.buttonReset.Text = "Reset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // trackBarLightness
            // 
            this.trackBarLightness.Dock = System.Windows.Forms.DockStyle.Right;
            this.trackBarLightness.Location = new System.Drawing.Point(127, 0);
            this.trackBarLightness.Maximum = 1000;
            this.trackBarLightness.Name = "trackBarLightness";
            this.trackBarLightness.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBarLightness.Size = new System.Drawing.Size(45, 633);
            this.trackBarLightness.TabIndex = 8;
            this.trackBarLightness.Scroll += new System.EventHandler(this.trackBarLightness_Scroll);
            // 
            // labelB
            // 
            this.labelB.AutoSize = true;
            this.labelB.Location = new System.Drawing.Point(3, 442);
            this.labelB.Name = "labelB";
            this.labelB.Size = new System.Drawing.Size(41, 13);
            this.labelB.TabIndex = 7;
            this.labelB.Text = "radians";
            // 
            // labelG
            // 
            this.labelG.AutoSize = true;
            this.labelG.Location = new System.Drawing.Point(3, 429);
            this.labelG.Name = "labelG";
            this.labelG.Size = new System.Drawing.Size(41, 13);
            this.labelG.TabIndex = 6;
            this.labelG.Text = "radians";
            // 
            // labelR
            // 
            this.labelR.AutoSize = true;
            this.labelR.Location = new System.Drawing.Point(3, 416);
            this.labelR.Name = "labelR";
            this.labelR.Size = new System.Drawing.Size(41, 13);
            this.labelR.TabIndex = 5;
            this.labelR.Text = "radians";
            // 
            // labelRadians
            // 
            this.labelRadians.AutoSize = true;
            this.labelRadians.Location = new System.Drawing.Point(13, 296);
            this.labelRadians.Name = "labelRadians";
            this.labelRadians.Size = new System.Drawing.Size(41, 13);
            this.labelRadians.TabIndex = 4;
            this.labelRadians.Text = "radians";
            // 
            // labelDelta
            // 
            this.labelDelta.AutoSize = true;
            this.labelDelta.Location = new System.Drawing.Point(13, 271);
            this.labelDelta.Name = "labelDelta";
            this.labelDelta.Size = new System.Drawing.Size(30, 13);
            this.labelDelta.TabIndex = 3;
            this.labelDelta.Text = "delta";
            // 
            // labelUpDown
            // 
            this.labelUpDown.AutoSize = true;
            this.labelUpDown.Location = new System.Drawing.Point(13, 240);
            this.labelUpDown.Name = "labelUpDown";
            this.labelUpDown.Size = new System.Drawing.Size(19, 13);
            this.labelUpDown.TabIndex = 2;
            this.labelUpDown.Text = "up";
            // 
            // labelY
            // 
            this.labelY.AutoSize = true;
            this.labelY.Location = new System.Drawing.Point(13, 206);
            this.labelY.Name = "labelY";
            this.labelY.Size = new System.Drawing.Size(12, 13);
            this.labelY.TabIndex = 1;
            this.labelY.Text = "y";
            // 
            // labelX
            // 
            this.labelX.AutoSize = true;
            this.labelX.Location = new System.Drawing.Point(13, 180);
            this.labelX.Name = "labelX";
            this.labelX.Size = new System.Drawing.Size(12, 13);
            this.labelX.TabIndex = 0;
            this.labelX.Text = "x";
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(172, 24);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 633);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // panelBackground
            // 
            this.panelBackground.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.panelBackground.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelBackground.Controls.Add(this.panelMain);
            this.panelBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBackground.Location = new System.Drawing.Point(175, 24);
            this.panelBackground.Name = "panelBackground";
            this.panelBackground.Size = new System.Drawing.Size(1025, 633);
            this.panelBackground.TabIndex = 3;
            this.panelBackground.Paint += new System.Windows.Forms.PaintEventHandler(this.panelBackground_Paint);
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.DarkTurquoise;
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1025, 633);
            this.panelMain.TabIndex = 0;
            this.panelMain.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMain_Paint);
            this.panelMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelMain_MouseDown);
            this.panelMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelMain_MouseMove);
            this.panelMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelMain_MouseUp);
            this.panelMain.Resize += new System.EventHandler(this.panelMain_Resize);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 657);
            this.Controls.Add(this.panelBackground);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panelControls);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "Minha Cor";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panelControls.ResumeLayout(false);
            this.panelControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLightness)).EndInit();
            this.panelBackground.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.Panel panelControls;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panelBackground;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label labelUpDown;
        private System.Windows.Forms.Label labelY;
        private System.Windows.Forms.Label labelX;
        private System.Windows.Forms.Label labelDelta;
        private System.Windows.Forms.Label labelRadians;
        private System.Windows.Forms.Label labelB;
        private System.Windows.Forms.Label labelG;
        private System.Windows.Forms.Label labelR;
        private System.Windows.Forms.TrackBar trackBarLightness;
        private System.Windows.Forms.Button buttonReset;
    }
}

