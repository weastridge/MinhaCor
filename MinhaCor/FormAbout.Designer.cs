namespace MinhaCor
{
    partial class FormAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
            this.buttonOK = new System.Windows.Forms.Button();
            this.textBoxIntro = new System.Windows.Forms.TextBox();
            this.labelCulture = new System.Windows.Forms.Label();
            this.labelUICulture = new System.Windows.Forms.Label();
            this.textBoxDetails = new System.Windows.Forms.TextBox();
            this.linkLabelMinhaCor = new System.Windows.Forms.LinkLabel();
            this.linkLabelHumanae = new System.Windows.Forms.LinkLabel();
            this.linkLabelAngelica = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // textBoxIntro
            // 
            resources.ApplyResources(this.textBoxIntro, "textBoxIntro");
            this.textBoxIntro.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxIntro.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxIntro.Name = "textBoxIntro";
            // 
            // labelCulture
            // 
            resources.ApplyResources(this.labelCulture, "labelCulture");
            this.labelCulture.Name = "labelCulture";
            // 
            // labelUICulture
            // 
            resources.ApplyResources(this.labelUICulture, "labelUICulture");
            this.labelUICulture.Name = "labelUICulture";
            // 
            // textBoxDetails
            // 
            resources.ApplyResources(this.textBoxDetails, "textBoxDetails");
            this.textBoxDetails.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxDetails.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxDetails.Name = "textBoxDetails";
            // 
            // linkLabelMinhaCor
            // 
            resources.ApplyResources(this.linkLabelMinhaCor, "linkLabelMinhaCor");
            this.linkLabelMinhaCor.Name = "linkLabelMinhaCor";
            this.linkLabelMinhaCor.TabStop = true;
            this.linkLabelMinhaCor.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelMinhaCor_LinkClicked);
            // 
            // linkLabelHumanae
            // 
            resources.ApplyResources(this.linkLabelHumanae, "linkLabelHumanae");
            this.linkLabelHumanae.Name = "linkLabelHumanae";
            this.linkLabelHumanae.TabStop = true;
            this.linkLabelHumanae.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelHumanae_LinkClicked);
            // 
            // linkLabelAngelica
            // 
            resources.ApplyResources(this.linkLabelAngelica, "linkLabelAngelica");
            this.linkLabelAngelica.Name = "linkLabelAngelica";
            this.linkLabelAngelica.TabStop = true;
            this.linkLabelAngelica.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelAngelica_LinkClicked);
            // 
            // FormAbout
            // 
            this.AcceptButton = this.buttonOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonOK;
            this.ControlBox = false;
            this.Controls.Add(this.linkLabelAngelica);
            this.Controls.Add(this.linkLabelHumanae);
            this.Controls.Add(this.linkLabelMinhaCor);
            this.Controls.Add(this.textBoxDetails);
            this.Controls.Add(this.labelUICulture);
            this.Controls.Add(this.labelCulture);
            this.Controls.Add(this.textBoxIntro);
            this.Controls.Add(this.buttonOK);
            this.Name = "FormAbout";
            this.Load += new System.EventHandler(this.About_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.TextBox textBoxIntro;
        private System.Windows.Forms.Label labelCulture;
        private System.Windows.Forms.Label labelUICulture;
        private System.Windows.Forms.TextBox textBoxDetails;
        private System.Windows.Forms.LinkLabel linkLabelMinhaCor;
        private System.Windows.Forms.LinkLabel linkLabelHumanae;
        private System.Windows.Forms.LinkLabel linkLabelAngelica;
    }
}