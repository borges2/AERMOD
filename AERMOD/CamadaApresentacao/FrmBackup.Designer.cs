namespace AERMOD.CamadaApresentacao
{
    partial class FrmBackup
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
            this.chboxExportaLinha = new System.Windows.Forms.CheckBox();
            this.chboxExportaEstrutura = new System.Windows.Forms.CheckBox();
            this.btnBackup = new System.Windows.Forms.Button();
            this.btnRestaurarBackup = new System.Windows.Forms.Button();
            this.tbxProgresso = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // chboxExportaLinha
            // 
            this.chboxExportaLinha.AutoSize = true;
            this.chboxExportaLinha.Checked = true;
            this.chboxExportaLinha.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chboxExportaLinha.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chboxExportaLinha.Location = new System.Drawing.Point(611, 12);
            this.chboxExportaLinha.Name = "chboxExportaLinha";
            this.chboxExportaLinha.Size = new System.Drawing.Size(120, 21);
            this.chboxExportaLinha.TabIndex = 3;
            this.chboxExportaLinha.Text = "Exportar linhas";
            this.chboxExportaLinha.UseVisualStyleBackColor = true;
            // 
            // chboxExportaEstrutura
            // 
            this.chboxExportaEstrutura.AutoSize = true;
            this.chboxExportaEstrutura.Checked = true;
            this.chboxExportaEstrutura.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chboxExportaEstrutura.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chboxExportaEstrutura.Location = new System.Drawing.Point(387, 12);
            this.chboxExportaEstrutura.Name = "chboxExportaEstrutura";
            this.chboxExportaEstrutura.Size = new System.Drawing.Size(212, 21);
            this.chboxExportaEstrutura.TabIndex = 2;
            this.chboxExportaEstrutura.Text = "Exportar estrutura das tabelas";
            this.chboxExportaEstrutura.UseVisualStyleBackColor = true;
            // 
            // btnBackup
            // 
            this.btnBackup.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackup.Image = global::AERMOD.Properties.Resources.backup;
            this.btnBackup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBackup.Location = new System.Drawing.Point(5, 5);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(180, 34);
            this.btnBackup.TabIndex = 0;
            this.btnBackup.Text = "    <Ctrl B> Copiar";
            this.btnBackup.UseVisualStyleBackColor = true;
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // btnRestaurarBackup
            // 
            this.btnRestaurarBackup.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestaurarBackup.Image = global::AERMOD.Properties.Resources.restauraBackup;
            this.btnRestaurarBackup.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRestaurarBackup.Location = new System.Drawing.Point(192, 5);
            this.btnRestaurarBackup.Name = "btnRestaurarBackup";
            this.btnRestaurarBackup.Size = new System.Drawing.Size(180, 34);
            this.btnRestaurarBackup.TabIndex = 1;
            this.btnRestaurarBackup.Text = "<Ctrl R> Restaurar";
            this.btnRestaurarBackup.UseVisualStyleBackColor = true;
            this.btnRestaurarBackup.Click += new System.EventHandler(this.btnRestaurarBackup_Click);
            // 
            // tbxProgresso
            // 
            this.tbxProgresso.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.tbxProgresso.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.tbxProgresso.Location = new System.Drawing.Point(5, 45);
            this.tbxProgresso.Multiline = true;
            this.tbxProgresso.Name = "tbxProgresso";
            this.tbxProgresso.ReadOnly = true;
            this.tbxProgresso.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbxProgresso.Size = new System.Drawing.Size(721, 342);
            this.tbxProgresso.TabIndex = 4;
            // 
            // FrmBackup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(731, 392);
            this.Controls.Add(this.tbxProgresso);
            this.Controls.Add(this.btnBackup);
            this.Controls.Add(this.btnRestaurarBackup);
            this.Controls.Add(this.chboxExportaLinha);
            this.Controls.Add(this.chboxExportaEstrutura);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmBackup";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cópia de segurança";
            this.Load += new System.EventHandler(this.FrmBackup_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmBackup_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chboxExportaLinha;
        private System.Windows.Forms.CheckBox chboxExportaEstrutura;
        private System.Windows.Forms.Button btnBackup;
        private System.Windows.Forms.Button btnRestaurarBackup;
        private System.Windows.Forms.TextBox tbxProgresso;
    }
}