namespace AERMOD.CamadaApresentacao.AERMAP
{
    partial class FrmGrade
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
            this.lbLineCartesiano = new System.Windows.Forms.Label();
            this.lbCartesiano = new System.Windows.Forms.Label();
            this.lbLineCartesianoDiscreto = new System.Windows.Forms.Label();
            this.lbCartesianoDiscreto = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.btnAjuda = new System.Windows.Forms.ToolStripSplitButton();
            this.btnSair = new System.Windows.Forms.ToolStripSplitButton();
            this.btnEVALFILE = new System.Windows.Forms.Button();
            this.btnCartesianoDiscreto = new System.Windows.Forms.Button();
            this.btnCartesianoElevacao = new System.Windows.Forms.Button();
            this.btnCartesiano = new System.Windows.Forms.Button();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbLineCartesiano
            // 
            this.lbLineCartesiano.AutoSize = true;
            this.lbLineCartesiano.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lbLineCartesiano.Location = new System.Drawing.Point(118, 13);
            this.lbLineCartesiano.Name = "lbLineCartesiano";
            this.lbLineCartesiano.Size = new System.Drawing.Size(253, 13);
            this.lbLineCartesiano.TabIndex = 269;
            this.lbLineCartesiano.Text = "---------------------------------------------------------------------------------" +
    "-";
            // 
            // lbCartesiano
            // 
            this.lbCartesiano.AutoSize = true;
            this.lbCartesiano.BackColor = System.Drawing.Color.Transparent;
            this.lbCartesiano.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCartesiano.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lbCartesiano.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lbCartesiano.Location = new System.Drawing.Point(7, 9);
            this.lbCartesiano.Name = "lbCartesiano";
            this.lbCartesiano.Size = new System.Drawing.Size(110, 17);
            this.lbCartesiano.TabIndex = 268;
            this.lbCartesiano.Text = "Grade cartesiana";
            this.lbCartesiano.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbLineCartesianoDiscreto
            // 
            this.lbLineCartesianoDiscreto.AutoSize = true;
            this.lbLineCartesianoDiscreto.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lbLineCartesianoDiscreto.Location = new System.Drawing.Point(163, 84);
            this.lbLineCartesianoDiscreto.Name = "lbLineCartesianoDiscreto";
            this.lbLineCartesianoDiscreto.Size = new System.Drawing.Size(208, 13);
            this.lbLineCartesianoDiscreto.TabIndex = 271;
            this.lbLineCartesianoDiscreto.Text = "-------------------------------------------------------------------";
            // 
            // lbCartesianoDiscreto
            // 
            this.lbCartesianoDiscreto.AutoSize = true;
            this.lbCartesianoDiscreto.BackColor = System.Drawing.Color.Transparent;
            this.lbCartesianoDiscreto.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCartesianoDiscreto.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lbCartesianoDiscreto.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lbCartesianoDiscreto.Location = new System.Drawing.Point(7, 81);
            this.lbCartesianoDiscreto.Name = "lbCartesianoDiscreto";
            this.lbCartesianoDiscreto.Size = new System.Drawing.Size(162, 17);
            this.lbCartesianoDiscreto.TabIndex = 270;
            this.lbCartesianoDiscreto.Text = "Grade cartesiana discreta";
            this.lbCartesianoDiscreto.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.Color.LightBlue;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAjuda,
            this.btnSair});
            this.statusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip.Location = new System.Drawing.Point(0, 155);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(380, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 276;
            this.statusStrip.Text = "statusStrip1";
            // 
            // btnAjuda
            // 
            this.btnAjuda.AutoSize = false;
            this.btnAjuda.DropDownButtonWidth = 0;
            this.btnAjuda.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAjuda.Image = global::AERMOD.Properties.Resources.ajuda;
            this.btnAjuda.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAjuda.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAjuda.Name = "btnAjuda";
            this.btnAjuda.Size = new System.Drawing.Size(260, 20);
            this.btnAjuda.Text = "<F1> &Ajuda";
            this.btnAjuda.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAjuda.Click += new System.EventHandler(this.btnAjuda_Click);
            // 
            // btnSair
            // 
            this.btnSair.AutoSize = false;
            this.btnSair.DropDownButtonWidth = 0;
            this.btnSair.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.Image = global::AERMOD.Properties.Resources.Sair;
            this.btnSair.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(85, 20);
            this.btnSair.Text = "<Esc> &Sair";
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnEVALFILE
            // 
            this.btnEVALFILE.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEVALFILE.Image = global::AERMOD.Properties.Resources.form;
            this.btnEVALFILE.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEVALFILE.Location = new System.Drawing.Point(189, 107);
            this.btnEVALFILE.Name = "btnEVALFILE";
            this.btnEVALFILE.Size = new System.Drawing.Size(180, 34);
            this.btnEVALFILE.TabIndex = 3;
            this.btnEVALFILE.Text = " <F5> EVALFILE";
            this.btnEVALFILE.UseVisualStyleBackColor = true;
            this.btnEVALFILE.Click += new System.EventHandler(this.btnEVALFILE_Click);
            // 
            // btnCartesianoDiscreto
            // 
            this.btnCartesianoDiscreto.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCartesianoDiscreto.Image = global::AERMOD.Properties.Resources.form;
            this.btnCartesianoDiscreto.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCartesianoDiscreto.Location = new System.Drawing.Point(10, 107);
            this.btnCartesianoDiscreto.Name = "btnCartesianoDiscreto";
            this.btnCartesianoDiscreto.Size = new System.Drawing.Size(180, 34);
            this.btnCartesianoDiscreto.TabIndex = 2;
            this.btnCartesianoDiscreto.Text = " <F4> Normal";
            this.btnCartesianoDiscreto.UseVisualStyleBackColor = true;
            this.btnCartesianoDiscreto.Click += new System.EventHandler(this.btnCartesianoDiscreto_Click);
            // 
            // btnCartesianoElevacao
            // 
            this.btnCartesianoElevacao.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCartesianoElevacao.Image = global::AERMOD.Properties.Resources.form;
            this.btnCartesianoElevacao.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCartesianoElevacao.Location = new System.Drawing.Point(189, 34);
            this.btnCartesianoElevacao.Name = "btnCartesianoElevacao";
            this.btnCartesianoElevacao.Size = new System.Drawing.Size(180, 34);
            this.btnCartesianoElevacao.TabIndex = 1;
            this.btnCartesianoElevacao.Text = " <F3> Elevação";
            this.btnCartesianoElevacao.UseVisualStyleBackColor = true;
            this.btnCartesianoElevacao.Click += new System.EventHandler(this.btnCartesianoElevacao_Click);
            // 
            // btnCartesiano
            // 
            this.btnCartesiano.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCartesiano.Image = global::AERMOD.Properties.Resources.form;
            this.btnCartesiano.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCartesiano.Location = new System.Drawing.Point(10, 34);
            this.btnCartesiano.Name = "btnCartesiano";
            this.btnCartesiano.Size = new System.Drawing.Size(180, 34);
            this.btnCartesiano.TabIndex = 0;
            this.btnCartesiano.Text = " <F2> Uniforme";
            this.btnCartesiano.UseVisualStyleBackColor = true;
            this.btnCartesiano.Click += new System.EventHandler(this.btnCartesiano_Click);
            // 
            // FrmGrade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(380, 177);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.btnEVALFILE);
            this.Controls.Add(this.btnCartesianoDiscreto);
            this.Controls.Add(this.btnCartesianoElevacao);
            this.Controls.Add(this.btnCartesiano);
            this.Controls.Add(this.lbLineCartesianoDiscreto);
            this.Controls.Add(this.lbCartesianoDiscreto);
            this.Controls.Add(this.lbLineCartesiano);
            this.Controls.Add(this.lbCartesiano);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmGrade";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Grade";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmGrade_KeyDown);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbLineCartesiano;
        private System.Windows.Forms.Label lbCartesiano;
        private System.Windows.Forms.Label lbLineCartesianoDiscreto;
        private System.Windows.Forms.Label lbCartesianoDiscreto;
        private System.Windows.Forms.Button btnCartesiano;
        private System.Windows.Forms.Button btnCartesianoElevacao;
        private System.Windows.Forms.Button btnEVALFILE;
        private System.Windows.Forms.Button btnCartesianoDiscreto;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripSplitButton btnAjuda;
        private System.Windows.Forms.ToolStripSplitButton btnSair;
    }
}