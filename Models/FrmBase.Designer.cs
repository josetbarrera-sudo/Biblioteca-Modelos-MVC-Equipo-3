namespace Biblioteca
{
    partial class FrmBase
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
            pnlFormularioBase = new Panel();
            SuspendLayout();
            // 
            // pnlFormularioBase
            // 
            pnlFormularioBase.Dock = DockStyle.Left;
            pnlFormularioBase.Location = new Point(0, 0);
            pnlFormularioBase.Name = "pnlFormularioBase";
            pnlFormularioBase.Size = new Size(240, 450);
            pnlFormularioBase.TabIndex = 0;
            // 
            // FrmBase
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(pnlFormularioBase);
            Name = "FrmBase";
            Text = "FrmBase";
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlFormularioBase;
    }
}