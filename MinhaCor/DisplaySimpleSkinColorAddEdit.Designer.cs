namespace MinhaCor
{
    partial class DisplaySimpleSkinColorAddEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisplaySimpleSkinColorAddEdit));
            this.panelControls = new System.Windows.Forms.Panel();
            this.panelMedium = new System.Windows.Forms.Panel();
            this.panelDark = new System.Windows.Forms.Panel();
            this.panelLight = new System.Windows.Forms.Panel();
            this.panelLightLight = new System.Windows.Forms.Panel();
            this.labelrgb = new System.Windows.Forms.Label();
            this.panelGradient = new System.Windows.Forms.Panel();
            this.labelGray = new System.Windows.Forms.Label();
            this.labelColor = new System.Windows.Forms.Label();
            this.trackBarSaturation = new System.Windows.Forms.TrackBar();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPersonName = new System.Windows.Forms.TextBox();
            this.textBoxColorName = new System.Windows.Forms.TextBox();
            this.panelSwatch = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panelBackground = new System.Windows.Forms.Panel();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelRight = new System.Windows.Forms.Panel();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.labelStartingColor = new System.Windows.Forms.Label();
            this.labelAdjustHue = new System.Windows.Forms.Label();
            this.labelAdjustSaturation = new System.Windows.Forms.Label();
            this.panelControls.SuspendLayout();
            this.panelGradient.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSaturation)).BeginInit();
            this.panelBackground.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControls
            // 
            this.panelControls.Controls.Add(this.labelAdjustSaturation);
            this.panelControls.Controls.Add(this.labelAdjustHue);
            this.panelControls.Controls.Add(this.labelStartingColor);
            this.panelControls.Controls.Add(this.panelMedium);
            this.panelControls.Controls.Add(this.panelDark);
            this.panelControls.Controls.Add(this.panelLight);
            this.panelControls.Controls.Add(this.panelLightLight);
            this.panelControls.Controls.Add(this.labelrgb);
            this.panelControls.Controls.Add(this.panelGradient);
            this.panelControls.Controls.Add(this.buttonCancel);
            this.panelControls.Controls.Add(this.buttonOK);
            this.panelControls.Controls.Add(this.buttonReset);
            this.panelControls.Controls.Add(this.label2);
            this.panelControls.Controls.Add(this.label1);
            this.panelControls.Controls.Add(this.textBoxPersonName);
            this.panelControls.Controls.Add(this.textBoxColorName);
            this.panelControls.Controls.Add(this.panelSwatch);
            resources.ApplyResources(this.panelControls, "panelControls");
            this.panelControls.Name = "panelControls";
            this.panelControls.Paint += new System.Windows.Forms.PaintEventHandler(this.panelControls_Paint);
            // 
            // panelMedium
            // 
            resources.ApplyResources(this.panelMedium, "panelMedium");
            this.panelMedium.Name = "panelMedium";
            this.panelMedium.Click += new System.EventHandler(this.panelExample_Click);
            // 
            // panelDark
            // 
            resources.ApplyResources(this.panelDark, "panelDark");
            this.panelDark.Name = "panelDark";
            this.panelDark.Click += new System.EventHandler(this.panelExample_Click);
            // 
            // panelLight
            // 
            resources.ApplyResources(this.panelLight, "panelLight");
            this.panelLight.Name = "panelLight";
            this.panelLight.Click += new System.EventHandler(this.panelExample_Click);
            // 
            // panelLightLight
            // 
            resources.ApplyResources(this.panelLightLight, "panelLightLight");
            this.panelLightLight.Name = "panelLightLight";
            this.panelLightLight.Click += new System.EventHandler(this.panelExample_Click);
            // 
            // labelrgb
            // 
            resources.ApplyResources(this.labelrgb, "labelrgb");
            this.labelrgb.Name = "labelrgb";
            // 
            // panelGradient
            // 
            this.panelGradient.BackgroundImage = global::MinhaCor.Properties.Resources.SaturationGradient;
            resources.ApplyResources(this.panelGradient, "panelGradient");
            this.panelGradient.Controls.Add(this.labelGray);
            this.panelGradient.Controls.Add(this.labelColor);
            this.panelGradient.Controls.Add(this.trackBarSaturation);
            this.panelGradient.Name = "panelGradient";
            // 
            // labelGray
            // 
            resources.ApplyResources(this.labelGray, "labelGray");
            this.labelGray.ForeColor = System.Drawing.Color.LightSteelBlue;
            this.labelGray.Name = "labelGray";
            // 
            // labelColor
            // 
            resources.ApplyResources(this.labelColor, "labelColor");
            this.labelColor.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelColor.Name = "labelColor";
            // 
            // trackBarSaturation
            // 
            this.trackBarSaturation.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.trackBarSaturation, "trackBarSaturation");
            this.trackBarSaturation.Name = "trackBarSaturation";
            this.trackBarSaturation.Scroll += new System.EventHandler(this.trackBarSaturation_Scroll);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonReset
            // 
            resources.ApplyResources(this.buttonReset, "buttonReset");
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBoxPersonName
            // 
            resources.ApplyResources(this.textBoxPersonName, "textBoxPersonName");
            this.textBoxPersonName.Name = "textBoxPersonName";
            // 
            // textBoxColorName
            // 
            resources.ApplyResources(this.textBoxColorName, "textBoxColorName");
            this.textBoxColorName.Name = "textBoxColorName";
            // 
            // panelSwatch
            // 
            resources.ApplyResources(this.panelSwatch, "panelSwatch");
            this.panelSwatch.Name = "panelSwatch";
            this.panelSwatch.Paint += new System.Windows.Forms.PaintEventHandler(this.panelSwatch_Paint);
            // 
            // splitter1
            // 
            resources.ApplyResources(this.splitter1, "splitter1");
            this.splitter1.Name = "splitter1";
            this.splitter1.TabStop = false;
            // 
            // panelBackground
            // 
            this.panelBackground.Controls.Add(this.panelMain);
            resources.ApplyResources(this.panelBackground, "panelBackground");
            this.panelBackground.Name = "panelBackground";
            this.panelBackground.Paint += new System.Windows.Forms.PaintEventHandler(this.panelBackground_Paint);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.panelRight);
            resources.ApplyResources(this.panelMain, "panelMain");
            this.panelMain.Name = "panelMain";
            this.panelMain.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMain_Paint);
            this.panelMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelMain_MouseDown);
            this.panelMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelMain_MouseMove);
            this.panelMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelMain_MouseUp);
            this.panelMain.Resize += new System.EventHandler(this.panelMain_Resize_1);
            // 
            // panelRight
            // 
            this.panelRight.BackgroundImage = global::MinhaCor.Properties.Resources.redBlackGradient;
            resources.ApplyResources(this.panelRight, "panelRight");
            this.panelRight.Name = "panelRight";
            // 
            // panelLeft
            // 
            this.panelLeft.BackgroundImage = global::MinhaCor.Properties.Resources.greenBlackGradient;
            resources.ApplyResources(this.panelLeft, "panelLeft");
            this.panelLeft.Name = "panelLeft";
            // 
            // labelStartingColor
            // 
            resources.ApplyResources(this.labelStartingColor, "labelStartingColor");
            this.labelStartingColor.Name = "labelStartingColor";
            // 
            // labelAdjustHue
            // 
            resources.ApplyResources(this.labelAdjustHue, "labelAdjustHue");
            this.labelAdjustHue.Name = "labelAdjustHue";
            // 
            // labelAdjustSaturation
            // 
            resources.ApplyResources(this.labelAdjustSaturation, "labelAdjustSaturation");
            this.labelAdjustSaturation.Name = "labelAdjustSaturation";
            // 
            // DisplaySimpleSkinColorAddEdit
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelBackground);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panelControls);
            this.Name = "DisplaySimpleSkinColorAddEdit";
            this.Load += new System.EventHandler(this.DisplayColorAddEdit_Load);
            this.panelControls.ResumeLayout(false);
            this.panelControls.PerformLayout();
            this.panelGradient.ResumeLayout(false);
            this.panelGradient.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarSaturation)).EndInit();
            this.panelBackground.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelControls;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Panel panelBackground;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxPersonName;
        private System.Windows.Forms.TextBox textBoxColorName;
        private System.Windows.Forms.Panel panelSwatch;
        private System.Windows.Forms.TrackBar trackBarSaturation;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label labelGray;
        private System.Windows.Forms.Label labelColor;
        private System.Windows.Forms.Panel panelGradient;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Label labelrgb;
        private System.Windows.Forms.Panel panelMedium;
        private System.Windows.Forms.Panel panelDark;
        private System.Windows.Forms.Panel panelLight;
        private System.Windows.Forms.Panel panelLightLight;
        private System.Windows.Forms.Label labelAdjustSaturation;
        private System.Windows.Forms.Label labelAdjustHue;
        private System.Windows.Forms.Label labelStartingColor;
    }
}
