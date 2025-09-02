namespace AERMOD.LIB.Componentes.Splash
{
    partial class SplashScreenForm
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
            this.lbCarregando = new System.Windows.Forms.Label();
            this.lbPontos = new System.Windows.Forms.Label();
            this.progressBar = new AERMOD.LIB.Componentes.StyleProgressBar.ProgressBarWin();
            this.SuspendLayout();
            // 
            // lbCarregando
            // 
            this.lbCarregando.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbCarregando.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCarregando.Location = new System.Drawing.Point(5, 5);
            this.lbCarregando.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.lbCarregando.Name = "lbCarregando";
            this.lbCarregando.Size = new System.Drawing.Size(282, 89);
            this.lbCarregando.TabIndex = 1;
            this.lbCarregando.Text = "Carregando   ";
            this.lbCarregando.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbPontos
            // 
            this.lbPontos.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbPontos.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPontos.Location = new System.Drawing.Point(255, 5);
            this.lbPontos.Name = "lbPontos";
            this.lbPontos.Size = new System.Drawing.Size(105, 89);
            this.lbPontos.TabIndex = 3;
            this.lbPontos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressBar
            // 
            this.progressBar.BackColor = System.Drawing.SystemColors.ControlLight;
            this.progressBar.Depth = 0;
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.Location = new System.Drawing.Point(5, 94);
            this.progressBar.MarqueeAnimationSpeed = 5;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(355, 28);
            this.progressBar.TabIndex = 3;
            // 
            // SplashScreenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(365, 127);
            this.Controls.Add(this.lbPontos);
            this.Controls.Add(this.lbCarregando);
            this.Controls.Add(this.progressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SplashScreenForm";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SplashScreenForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbCarregando;
        private System.Windows.Forms.Label lbPontos;
        private StyleProgressBar.ProgressBarWin progressBar;
    }
}