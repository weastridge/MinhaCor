namespace MinhaCor
{
    partial class DisplaySkinColorAddEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DisplaySkinColorAddEdit));
            this.panelControls = new System.Windows.Forms.Panel();
            this.groupBoxPickStarting = new System.Windows.Forms.GroupBox();
            this.radioButtonDarkDark = new System.Windows.Forms.RadioButton();
            this.radioButtonDark = new System.Windows.Forms.RadioButton();
            this.radioButtonLight = new System.Windows.Forms.RadioButton();
            this.radioButtonLightLight = new System.Windows.Forms.RadioButton();
            this.panelRgb = new System.Windows.Forms.Panel();
            this.labelrgb = new System.Windows.Forms.Label();
            this.panelGradient = new System.Windows.Forms.Panel();
            this.labelLight = new System.Windows.Forms.Label();
            this.labelDark = new System.Windows.Forms.Label();
            this.trackBarLightness = new System.Windows.Forms.TrackBar();
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
            this.panelControls.SuspendLayout();
            this.groupBoxPickStarting.SuspendLayout();
            this.panelGradient.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLightness)).BeginInit();
            this.panelBackground.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControls
            // 
            resources.ApplyResources(this.panelControls, "panelControls");
            this.panelControls.Controls.Add(this.groupBoxPickStarting);
            this.panelControls.Controls.Add(this.panelRgb);
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
            this.panelControls.Name = "panelControls";
            this.panelControls.Paint += new System.Windows.Forms.PaintEventHandler(this.panelControls_Paint);
            // 
            // groupBoxPickStarting
            // 
            resources.ApplyResources(this.groupBoxPickStarting, "groupBoxPickStarting");
            this.groupBoxPickStarting.Controls.Add(this.radioButtonDarkDark);
            this.groupBoxPickStarting.Controls.Add(this.radioButtonDark);
            this.groupBoxPickStarting.Controls.Add(this.radioButtonLight);
            this.groupBoxPickStarting.Controls.Add(this.radioButtonLightLight);
            this.groupBoxPickStarting.Name = "groupBoxPickStarting";
            this.groupBoxPickStarting.TabStop = false;
            // 
            // radioButtonDarkDark
            // 
            resources.ApplyResources(this.radioButtonDarkDark, "radioButtonDarkDark");
            this.radioButtonDarkDark.Name = "radioButtonDarkDark";
            this.radioButtonDarkDark.TabStop = true;
            this.radioButtonDarkDark.UseVisualStyleBackColor = true;
            this.radioButtonDarkDark.Click += new System.EventHandler(this.radio_Click);
            // 
            // radioButtonDark
            // 
            resources.ApplyResources(this.radioButtonDark, "radioButtonDark");
            this.radioButtonDark.Name = "radioButtonDark";
            this.radioButtonDark.TabStop = true;
            this.radioButtonDark.UseVisualStyleBackColor = true;
            this.radioButtonDark.Click += new System.EventHandler(this.radio_Click);
            // 
            // radioButtonLight
            // 
            resources.ApplyResources(this.radioButtonLight, "radioButtonLight");
            this.radioButtonLight.Name = "radioButtonLight";
            this.radioButtonLight.TabStop = true;
            this.radioButtonLight.UseVisualStyleBackColor = true;
            this.radioButtonLight.Click += new System.EventHandler(this.radio_Click);
            // 
            // radioButtonLightLight
            // 
            resources.ApplyResources(this.radioButtonLightLight, "radioButtonLightLight");
            this.radioButtonLightLight.Name = "radioButtonLightLight";
            this.radioButtonLightLight.TabStop = true;
            this.radioButtonLightLight.UseVisualStyleBackColor = true;
            this.radioButtonLightLight.Click += new System.EventHandler(this.radio_Click);
            // 
            // panelRgb
            // 
            resources.ApplyResources(this.panelRgb, "panelRgb");
            this.panelRgb.Name = "panelRgb";
            this.panelRgb.Paint += new System.Windows.Forms.PaintEventHandler(this.panelRgb_Paint);
            // 
            // labelrgb
            // 
            resources.ApplyResources(this.labelrgb, "labelrgb");
            this.labelrgb.Name = "labelrgb";
            // 
            // panelGradient
            // 
            resources.ApplyResources(this.panelGradient, "panelGradient");
            this.panelGradient.BackgroundImage = global::MinhaCor.Properties.Resources.blackWhiteGradient;
            this.panelGradient.Controls.Add(this.labelLight);
            this.panelGradient.Controls.Add(this.labelDark);
            this.panelGradient.Controls.Add(this.trackBarLightness);
            this.panelGradient.Name = "panelGradient";
            // 
            // labelLight
            // 
            resources.ApplyResources(this.labelLight, "labelLight");
            this.labelLight.ForeColor = System.Drawing.Color.LightSteelBlue;
            this.labelLight.Name = "labelLight";
            // 
            // labelDark
            // 
            resources.ApplyResources(this.labelDark, "labelDark");
            this.labelDark.ForeColor = System.Drawing.Color.DarkBlue;
            this.labelDark.Name = "labelDark";
            // 
            // trackBarLightness
            // 
            resources.ApplyResources(this.trackBarLightness, "trackBarLightness");
            this.trackBarLightness.BackColor = System.Drawing.SystemColors.Control;
            this.trackBarLightness.Name = "trackBarLightness";
            this.trackBarLightness.Scroll += new System.EventHandler(this.trackBarLightness_Scroll);
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
            resources.ApplyResources(this.panelBackground, "panelBackground");
            this.panelBackground.Controls.Add(this.panelMain);
            this.panelBackground.Name = "panelBackground";
            this.panelBackground.Paint += new System.Windows.Forms.PaintEventHandler(this.panelBackground_Paint);
            // 
            // panelMain
            // 
            resources.ApplyResources(this.panelMain, "panelMain");
            this.panelMain.Controls.Add(this.panelRight);
            this.panelMain.Name = "panelMain";
            this.panelMain.Paint += new System.Windows.Forms.PaintEventHandler(this.panelMain_Paint);
            this.panelMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelMain_MouseDown);
            this.panelMain.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelMain_MouseMove);
            this.panelMain.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelMain_MouseUp);
            this.panelMain.Resize += new System.EventHandler(this.panelMain_Resize);
            // 
            // panelRight
            // 
            resources.ApplyResources(this.panelRight, "panelRight");
            this.panelRight.BackgroundImage = global::MinhaCor.Properties.Resources.RedToGrayGradient;
            this.panelRight.Name = "panelRight";
            // 
            // panelLeft
            // 
            resources.ApplyResources(this.panelLeft, "panelLeft");
            this.panelLeft.BackgroundImage = global::MinhaCor.Properties.Resources.GreenToGrayGradient;
            this.panelLeft.Name = "panelLeft";
            // 
            // DisplaySkinColorAddEdit
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelBackground);
            this.Controls.Add(this.panelLeft);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panelControls);
            this.Name = "DisplaySkinColorAddEdit";
            this.Load += new System.EventHandler(this.DisplayColorAddEdit_Load);
            this.panelControls.ResumeLayout(false);
            this.panelControls.PerformLayout();
            this.groupBoxPickStarting.ResumeLayout(false);
            this.groupBoxPickStarting.PerformLayout();
            this.panelGradient.ResumeLayout(false);
            this.panelGradient.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLightness)).EndInit();
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
        private System.Windows.Forms.TrackBar trackBarLightness;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label labelLight;
        private System.Windows.Forms.Label labelDark;
        private System.Windows.Forms.Panel panelGradient;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Label labelrgb;
        private System.Windows.Forms.Panel panelRgb;
        private System.Windows.Forms.GroupBox groupBoxPickStarting;
        private System.Windows.Forms.RadioButton radioButtonDarkDark;
        private System.Windows.Forms.RadioButton radioButtonDark;
        private System.Windows.Forms.RadioButton radioButtonLight;
        private System.Windows.Forms.RadioButton radioButtonLightLight;
    }
}
