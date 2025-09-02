namespace AERMOD.LIB.Componentes
{
    partial class DataLIB
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flPanel = new AERMOD.LIB.Componentes.Taskbar.FlowLayoutPanelLIB();
            this.mtbx = new AERMOD.LIB.Componentes.MaskedTextBoxSeguro();
            this.btnCalendario = new AERMOD.LIB.Componentes.Botao.ButtonLIB();
            this.flPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // flPanel
            // 
            this.flPanel.AutoSize = true;
            this.flPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flPanel.Controls.Add(this.mtbx);
            this.flPanel.Controls.Add(this.btnCalendario);
            this.flPanel.Location = new System.Drawing.Point(0, 0);
            this.flPanel.Margin = new System.Windows.Forms.Padding(0);
            this.flPanel.Name = "flPanel";
            this.flPanel.Size = new System.Drawing.Size(117, 22);
            this.flPanel.TabIndex = 32;
            // 
            // mtbx
            // 
            this.mtbx.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mtbx.Location = new System.Drawing.Point(0, 0);
            this.mtbx.Margin = new System.Windows.Forms.Padding(0);
            this.mtbx.Mask = "00/00/0000";
            this.mtbx.Name = "mtbx";
            this.mtbx.PromptChar = ' ';
            this.mtbx.Size = new System.Drawing.Size(90, 22);
            this.mtbx.TabIndex = 0;
            this.mtbx.ValidatingType = typeof(System.DateTime);
            this.mtbx.Click += new System.EventHandler(this.mtbx_Click);
            this.mtbx.TextChanged += new System.EventHandler(this.mtbx_TextChanged);
            this.mtbx.Enter += new System.EventHandler(this.mtbx_Enter);
            this.mtbx.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mtbx_KeyDown);
            this.mtbx.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.mtbx_KeyPress);
            this.mtbx.Leave += new System.EventHandler(this.mtbx_Leave);
            this.mtbx.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mtbx_MouseDown);
            this.mtbx.MouseEnter += new System.EventHandler(this.mtbx_MouseEnter);
            this.mtbx.MouseLeave += new System.EventHandler(this.mtbx_MouseLeave);
            this.mtbx.MouseHover += new System.EventHandler(this.mtbx_MouseHover);
            this.mtbx.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mtbx_MouseMove);
            this.mtbx.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mtbx_MouseUp);
            // 
            // btnCalendario
            // 
            this.btnCalendario.AngleColor = 90F;
            this.btnCalendario.BackColor = System.Drawing.Color.Transparent;
            this.btnCalendario.BackgroundImage = global::AERMOD.LIB.Properties.Resources.calendar;
            this.btnCalendario.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCalendario.Border = false;
            this.btnCalendario.Color1 = System.Drawing.Color.White;
            this.btnCalendario.Color2 = System.Drawing.Color.Black;
            this.btnCalendario.ColorClick1 = System.Drawing.Color.Orange;
            this.btnCalendario.ColorClick2 = System.Drawing.Color.Orange;
            this.btnCalendario.ColorFocused = System.Drawing.Color.Black;
            this.btnCalendario.ColorMouseEnter1 = System.Drawing.Color.White;
            this.btnCalendario.ColorMouseEnter2 = System.Drawing.Color.SkyBlue;
            this.btnCalendario.DelayInitial = 900;
            this.btnCalendario.DelayRepeat = 100;
            this.btnCalendario.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalendario.GradientMode = AERMOD.LIB.Componentes.Botao.GradienteMode.Linear;
            this.btnCalendario.Image = null;
            this.btnCalendario.ImagePosition = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCalendario.Location = new System.Drawing.Point(91, 0);
            this.btnCalendario.Margin = new System.Windows.Forms.Padding(1, 0, 0, 0);
            this.btnCalendario.Name = "btnCalendario";
            this.btnCalendario.OpacityColor1 = 100;
            this.btnCalendario.OpacityColor2 = 110;
            this.btnCalendario.Raio = 5F;
            this.btnCalendario.RepeatClick = false;
            this.btnCalendario.Size = new System.Drawing.Size(26, 22);
            this.btnCalendario.TabIndex = 31;
            this.btnCalendario.TabStop = false;
            this.btnCalendario.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnCalendario.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnCalendario_MouseDown);
            // 
            // DataLIB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.flPanel);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "DataLIB";
            this.Size = new System.Drawing.Size(117, 22);
            this.Resize += new System.EventHandler(this.DataLIB_Resize);
            this.flPanel.ResumeLayout(false);
            this.flPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public MaskedTextBoxSeguro mtbx;
        public AERMOD.LIB.Componentes.Botao.ButtonLIB btnCalendario;
        private AERMOD.LIB.Componentes.Taskbar.FlowLayoutPanelLIB flPanel;
    }
}
