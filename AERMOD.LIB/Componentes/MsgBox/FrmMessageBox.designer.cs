namespace AERMOD.LIB.Componentes.MsgBox
{
    partial class FrmMessageBox
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
            this.components = new System.ComponentModel.Container();
            this.lbText = new System.Windows.Forms.Label();
            this.flowLayoutPanelTop = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
            this.flowLayoutPanelBotton = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanelFundo = new System.Windows.Forms.FlowLayoutPanel();
            this.timerDecremento = new System.Windows.Forms.Timer(this.components);
            this.flowLayoutPanelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
            this.flowLayoutPanelFundo.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbText
            // 
            this.lbText.AutoSize = true;
            this.lbText.Location = new System.Drawing.Point(64, 20);
            this.lbText.Name = "lbText";
            this.lbText.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.lbText.Size = new System.Drawing.Size(0, 23);
            this.lbText.TabIndex = 0;
            // 
            // flowLayoutPanelTop
            // 
            this.flowLayoutPanelTop.AutoSize = true;
            this.flowLayoutPanelTop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanelTop.Controls.Add(this.pictureBoxIcon);
            this.flowLayoutPanelTop.Controls.Add(this.lbText);
            this.flowLayoutPanelTop.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelTop.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelTop.Name = "flowLayoutPanelTop";
            this.flowLayoutPanelTop.Padding = new System.Windows.Forms.Padding(20, 20, 23, 0);
            this.flowLayoutPanelTop.Size = new System.Drawing.Size(90, 61);
            this.flowLayoutPanelTop.TabIndex = 0;
            // 
            // pictureBoxIcon
            // 
            this.pictureBoxIcon.Location = new System.Drawing.Point(23, 23);
            this.pictureBoxIcon.Name = "pictureBoxIcon";
            this.pictureBoxIcon.Size = new System.Drawing.Size(35, 35);
            this.pictureBoxIcon.TabIndex = 0;
            this.pictureBoxIcon.TabStop = false;
            this.pictureBoxIcon.Visible = false;
            // 
            // flowLayoutPanelBotton
            // 
            this.flowLayoutPanelBotton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanelBotton.AutoSize = true;
            this.flowLayoutPanelBotton.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanelBotton.Location = new System.Drawing.Point(0, 61);
            this.flowLayoutPanelBotton.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelBotton.MinimumSize = new System.Drawing.Size(135, 36);
            this.flowLayoutPanelBotton.Name = "flowLayoutPanelBotton";
            this.flowLayoutPanelBotton.Padding = new System.Windows.Forms.Padding(10, 3, 7, 0);
            this.flowLayoutPanelBotton.Size = new System.Drawing.Size(135, 36);
            this.flowLayoutPanelBotton.TabIndex = 1;
            // 
            // flowLayoutPanelFundo
            // 
            this.flowLayoutPanelFundo.AutoSize = true;
            this.flowLayoutPanelFundo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanelFundo.Controls.Add(this.flowLayoutPanelTop);
            this.flowLayoutPanelFundo.Controls.Add(this.flowLayoutPanelBotton);
            this.flowLayoutPanelFundo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelFundo.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanelFundo.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanelFundo.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanelFundo.Name = "flowLayoutPanelFundo";
            this.flowLayoutPanelFundo.Size = new System.Drawing.Size(136, 100);
            this.flowLayoutPanelFundo.TabIndex = 2;
            // 
            // timerDecremento
            // 
            this.timerDecremento.Interval = 1000;
            this.timerDecremento.Tick += new System.EventHandler(this.timerDecremento_Tick);
            // 
            // FrmMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(136, 100);
            this.Controls.Add(this.flowLayoutPanelFundo);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(142, 128);
            this.Name = "FrmMessageBox";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMessageBox_KeyDown);
            this.flowLayoutPanelTop.ResumeLayout(false);
            this.flowLayoutPanelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
            this.flowLayoutPanelFundo.ResumeLayout(false);
            this.flowLayoutPanelFundo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbText;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelTop;
        private System.Windows.Forms.PictureBox pictureBoxIcon;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelBotton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelFundo;
        private System.Windows.Forms.Timer timerDecremento;
    }
}