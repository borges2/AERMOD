namespace AERMOD.CamadaApresentacao
{
    partial class FrmAtualizacao
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
            this.lbAERMOD = new System.Windows.Forms.Label();
            this.lbVersaoAtual = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnAtualizar = new System.Windows.Forms.Button();
            this.tbxVersaoAtual = new System.Windows.Forms.Label();
            this.tbxNovaVersao = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbAERMOD
            // 
            this.lbAERMOD.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAERMOD.Location = new System.Drawing.Point(1, 9);
            this.lbAERMOD.Name = "lbAERMOD";
            this.lbAERMOD.Size = new System.Drawing.Size(329, 37);
            this.lbAERMOD.TabIndex = 272;
            this.lbAERMOD.Text = "AERMOD Open";
            this.lbAERMOD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbVersaoAtual
            // 
            this.lbVersaoAtual.AutoSize = true;
            this.lbVersaoAtual.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVersaoAtual.Location = new System.Drawing.Point(38, 61);
            this.lbVersaoAtual.Name = "lbVersaoAtual";
            this.lbVersaoAtual.Size = new System.Drawing.Size(99, 20);
            this.lbVersaoAtual.TabIndex = 273;
            this.lbVersaoAtual.Text = "Versão atual";
            this.lbVersaoAtual.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(38, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 20);
            this.label1.TabIndex = 274;
            this.label1.Text = "Última versão";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSair
            // 
            this.btnSair.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(72)))), ((int)(((byte)(54)))));
            this.btnSair.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.ForeColor = System.Drawing.Color.White;
            this.btnSair.Location = new System.Drawing.Point(12, 130);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(150, 39);
            this.btnSair.TabIndex = 0;
            this.btnSair.Text = "&Sair";
            this.btnSair.UseVisualStyleBackColor = false;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnAtualizar
            // 
            this.btnAtualizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(57)))), ((int)(((byte)(179)))), ((int)(((byte)(215)))));
            this.btnAtualizar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAtualizar.ForeColor = System.Drawing.Color.White;
            this.btnAtualizar.Location = new System.Drawing.Point(170, 130);
            this.btnAtualizar.Name = "btnAtualizar";
            this.btnAtualizar.Size = new System.Drawing.Size(150, 39);
            this.btnAtualizar.TabIndex = 1;
            this.btnAtualizar.Text = "&Atualizar";
            this.btnAtualizar.UseVisualStyleBackColor = false;
            this.btnAtualizar.Click += new System.EventHandler(this.btnAtualizar_Click);
            // 
            // tbxVersaoAtual
            // 
            this.tbxVersaoAtual.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxVersaoAtual.Location = new System.Drawing.Point(149, 61);
            this.tbxVersaoAtual.Name = "tbxVersaoAtual";
            this.tbxVersaoAtual.Size = new System.Drawing.Size(136, 20);
            this.tbxVersaoAtual.TabIndex = 277;
            this.tbxVersaoAtual.Text = "1.0.0";
            this.tbxVersaoAtual.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbxNovaVersao
            // 
            this.tbxNovaVersao.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxNovaVersao.Location = new System.Drawing.Point(149, 87);
            this.tbxNovaVersao.Name = "tbxNovaVersao";
            this.tbxNovaVersao.Size = new System.Drawing.Size(136, 20);
            this.tbxNovaVersao.TabIndex = 278;
            this.tbxNovaVersao.Text = "1.0.0";
            this.tbxNovaVersao.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FrmAtualizacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(331, 181);
            this.Controls.Add(this.tbxNovaVersao);
            this.Controls.Add(this.tbxVersaoAtual);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnAtualizar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbVersaoAtual);
            this.Controls.Add(this.lbAERMOD);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAtualizacao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AERMOD Open - Atualização";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAtualizacao_FormClosing);
            this.Load += new System.EventHandler(this.FrmAtualizacao_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmAtualizacao_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbAERMOD;
        private System.Windows.Forms.Label lbVersaoAtual;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnAtualizar;
        private System.Windows.Forms.Label tbxVersaoAtual;
        private System.Windows.Forms.Label tbxNovaVersao;
    }
}