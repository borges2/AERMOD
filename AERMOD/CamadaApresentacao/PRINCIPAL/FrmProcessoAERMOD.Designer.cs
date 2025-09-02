namespace AERMOD.CamadaApresentacao.PRINCIPAL
{
    partial class FrmProcessoAERMOD
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
            this.listBoxMensagem = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // listBoxMensagem
            // 
            this.listBoxMensagem.BackColor = System.Drawing.Color.White;
            this.listBoxMensagem.FormattingEnabled = true;
            this.listBoxMensagem.Location = new System.Drawing.Point(4, 5);
            this.listBoxMensagem.Name = "listBoxMensagem";
            this.listBoxMensagem.Size = new System.Drawing.Size(527, 394);
            this.listBoxMensagem.TabIndex = 1;
            // 
            // FrmProcessoAERMOD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(535, 403);
            this.Controls.Add(this.listBoxMensagem);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmProcessoAERMOD";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Processamento AERMOD";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmProcessoAERMOD_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmProcessoAERMOD_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxMensagem;
    }
}