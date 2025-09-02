
namespace CamadaAcessoDados
{
    partial class FrmReconectando
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
            this.lbTitulo = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.pbxImagem = new System.Windows.Forms.PictureBox();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lbTexto1 = new System.Windows.Forms.Label();
            this.lbTexto2 = new System.Windows.Forms.Label();
            this.lbTexto3 = new System.Windows.Forms.Label();
            this.lbTexto4 = new System.Windows.Forms.Label();
            this.lbIp = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagem)).BeginInit();
            this.SuspendLayout();
            // 
            // lbTitulo
            // 
            this.lbTitulo.BackColor = System.Drawing.Color.Transparent;
            this.lbTitulo.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTitulo.ForeColor = System.Drawing.Color.Blue;
            this.lbTitulo.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lbTitulo.Location = new System.Drawing.Point(0, 4);
            this.lbTitulo.Name = "lbTitulo";
            this.lbTitulo.Size = new System.Drawing.Size(167, 20);
            this.lbTitulo.TabIndex = 109;
            this.lbTitulo.Text = "Reconectando-se";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(0, 24);
            this.progressBar1.MarqueeAnimationSpeed = 30;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(430, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 117;
            this.progressBar1.Value = 10;
            // 
            // pbxImagem
            // 
            this.pbxImagem.Image = global::CamadaAcessoDados.Properties.Resources.networkError;
            this.pbxImagem.Location = new System.Drawing.Point(2, 55);
            this.pbxImagem.Name = "pbxImagem";
            this.pbxImagem.Size = new System.Drawing.Size(54, 53);
            this.pbxImagem.TabIndex = 118;
            this.pbxImagem.TabStop = false;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Image = global::CamadaAcessoDados.Properties.Resources.turnOff;
            this.btnCancelar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCancelar.Location = new System.Drawing.Point(3, 144);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(424, 34);
            this.btnCancelar.TabIndex = 119;
            this.btnCancelar.Text = "&Cancelar e sair do sistema";
            this.btnCancelar.UseVisualStyleBackColor = true;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // lbTexto1
            // 
            this.lbTexto1.BackColor = System.Drawing.Color.Transparent;
            this.lbTexto1.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTexto1.ForeColor = System.Drawing.Color.Black;
            this.lbTexto1.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lbTexto1.Location = new System.Drawing.Point(54, 52);
            this.lbTexto1.Name = "lbTexto1";
            this.lbTexto1.Size = new System.Drawing.Size(378, 20);
            this.lbTexto1.TabIndex = 120;
            this.lbTexto1.Text = "A conexão foi perdida com o servidor.";
            // 
            // lbTexto2
            // 
            this.lbTexto2.BackColor = System.Drawing.Color.Transparent;
            this.lbTexto2.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTexto2.ForeColor = System.Drawing.Color.Black;
            this.lbTexto2.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lbTexto2.Location = new System.Drawing.Point(54, 72);
            this.lbTexto2.Name = "lbTexto2";
            this.lbTexto2.Size = new System.Drawing.Size(378, 20);
            this.lbTexto2.TabIndex = 121;
            this.lbTexto2.Text = "A rede não está disponível. A conexão";
            // 
            // lbTexto3
            // 
            this.lbTexto3.BackColor = System.Drawing.Color.Transparent;
            this.lbTexto3.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTexto3.ForeColor = System.Drawing.Color.Black;
            this.lbTexto3.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lbTexto3.Location = new System.Drawing.Point(54, 92);
            this.lbTexto3.Name = "lbTexto3";
            this.lbTexto3.Size = new System.Drawing.Size(378, 20);
            this.lbTexto3.TabIndex = 122;
            this.lbTexto3.Text = "será restaurada quando a rede estiver";
            // 
            // lbTexto4
            // 
            this.lbTexto4.BackColor = System.Drawing.Color.Transparent;
            this.lbTexto4.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTexto4.ForeColor = System.Drawing.Color.Black;
            this.lbTexto4.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lbTexto4.Location = new System.Drawing.Point(-1, 114);
            this.lbTexto4.Name = "lbTexto4";
            this.lbTexto4.Size = new System.Drawing.Size(220, 20);
            this.lbTexto4.TabIndex = 123;
            this.lbTexto4.Text = "disponível. Servidor:";
            // 
            // lbIp
            // 
            this.lbIp.BackColor = System.Drawing.Color.Transparent;
            this.lbIp.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbIp.ForeColor = System.Drawing.Color.Black;
            this.lbIp.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lbIp.Location = new System.Drawing.Point(216, 115);
            this.lbIp.Name = "lbIp";
            this.lbIp.Size = new System.Drawing.Size(143, 20);
            this.lbIp.TabIndex = 124;
            this.lbIp.Text = "192.168.0.101";
            // 
            // FrmReconectando
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(436, 188);
            this.Controls.Add(this.lbIp);
            this.Controls.Add(this.lbTexto4);
            this.Controls.Add(this.lbTexto3);
            this.Controls.Add(this.lbTexto2);
            this.Controls.Add(this.lbTexto1);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.pbxImagem);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.lbTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrmReconectando";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmReconectando";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmReconectando_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pbxImagem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbTitulo;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.PictureBox pbxImagem;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lbTexto1;
        private System.Windows.Forms.Label lbTexto2;
        private System.Windows.Forms.Label lbTexto3;
        private System.Windows.Forms.Label lbTexto4;
        private System.Windows.Forms.Label lbIp;
    }
}