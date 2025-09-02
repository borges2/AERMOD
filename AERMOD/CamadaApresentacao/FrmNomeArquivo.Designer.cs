
namespace AERMOD.CamadaApresentacao
{
    partial class FrmNomeArquivo
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
            this.lbUsuario = new System.Windows.Forms.Label();
            this.tbxNomeArquivo = new System.Windows.Forms.TextBox();
            this.panelFundo = new System.Windows.Forms.Panel();
            this.panelFundo.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbUsuario
            // 
            this.lbUsuario.AutoSize = true;
            this.lbUsuario.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUsuario.Location = new System.Drawing.Point(13, 7);
            this.lbUsuario.Name = "lbUsuario";
            this.lbUsuario.Size = new System.Drawing.Size(128, 16);
            this.lbUsuario.TabIndex = 8;
            this.lbUsuario.Text = "Nome do arquivo";
            // 
            // tbxNomeArquivo
            // 
            this.tbxNomeArquivo.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxNomeArquivo.Location = new System.Drawing.Point(16, 26);
            this.tbxNomeArquivo.MaxLength = 30;
            this.tbxNomeArquivo.Name = "tbxNomeArquivo";
            this.tbxNomeArquivo.Size = new System.Drawing.Size(247, 22);
            this.tbxNomeArquivo.TabIndex = 0;
            // 
            // panelFundo
            // 
            this.panelFundo.BackColor = System.Drawing.Color.White;
            this.panelFundo.Controls.Add(this.tbxNomeArquivo);
            this.panelFundo.Controls.Add(this.lbUsuario);
            this.panelFundo.Location = new System.Drawing.Point(1, 1);
            this.panelFundo.Name = "panelFundo";
            this.panelFundo.Size = new System.Drawing.Size(277, 65);
            this.panelFundo.TabIndex = 9;
            // 
            // FrmNomeArquivo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(279, 67);
            this.Controls.Add(this.panelFundo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmNomeArquivo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmNomeArquivo";
            this.Load += new System.EventHandler(this.FrmNomeArquivo_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmNomeArquivo_KeyDown);
            this.panelFundo.ResumeLayout(false);
            this.panelFundo.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbUsuario;
        private System.Windows.Forms.TextBox tbxNomeArquivo;
        private System.Windows.Forms.Panel panelFundo;
    }
}