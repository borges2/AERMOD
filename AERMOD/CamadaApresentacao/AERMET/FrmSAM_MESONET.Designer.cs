namespace AERMOD.CamadaApresentacao.AERMET
{
    partial class FrmSAM_MESONET
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
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.btnAjuda = new System.Windows.Forms.ToolStripSplitButton();
            this.btnUpload = new System.Windows.Forms.ToolStripSplitButton();
            this.btnDownload = new System.Windows.Forms.ToolStripSplitButton();
            this.btnConverter = new System.Windows.Forms.ToolStripSplitButton();
            this.btnSair = new System.Windows.Forms.ToolStripSplitButton();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.lbFusoHorario = new System.Windows.Forms.Label();
            this.cbxFusoHorario = new System.Windows.Forms.ComboBox();
            this.lbUF = new System.Windows.Forms.Label();
            this.cbxUF = new System.Windows.Forms.ComboBox();
            this.btnConsulta = new System.Windows.Forms.Button();
            this.tbxCodigo = new AERMOD.LIB.Componentes.TextBoxMaskLIB();
            this.lbCodigo = new System.Windows.Forms.Label();
            this.lbCidade = new System.Windows.Forms.Label();
            this.tbxCidade = new System.Windows.Forms.TextBox();
            this.tbxLongitude = new AERMOD.LIB.Componentes.TextBoxMaskLIB();
            this.tbxLatitude = new AERMOD.LIB.Componentes.TextBoxMaskLIB();
            this.lbLongitude = new System.Windows.Forms.Label();
            this.lbLatitude = new System.Windows.Forms.Label();
            this.tbxElevacao = new AERMOD.LIB.Componentes.TextBoxMaskLIB();
            this.lbElevacao = new System.Windows.Forms.Label();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.Color.LightBlue;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAjuda,
            this.btnUpload,
            this.btnDownload,
            this.btnConverter,
            this.btnSair});
            this.statusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip.Location = new System.Drawing.Point(0, 539);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(784, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 8;
            this.statusStrip.Text = "statusStrip1";
            // 
            // btnAjuda
            // 
            this.btnAjuda.AutoSize = false;
            this.btnAjuda.DropDownButtonWidth = 0;
            this.btnAjuda.Image = global::AERMOD.Properties.Resources.ajuda;
            this.btnAjuda.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAjuda.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAjuda.Name = "btnAjuda";
            this.btnAjuda.Size = new System.Drawing.Size(147, 20);
            this.btnAjuda.Text = "<F1> &Ajuda";
            this.btnAjuda.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAjuda.Click += new System.EventHandler(this.btnAjuda_Click);
            // 
            // btnUpload
            // 
            this.btnUpload.AutoSize = false;
            this.btnUpload.DropDownButtonWidth = 0;
            this.btnUpload.Image = global::AERMOD.Properties.Resources.add;
            this.btnUpload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(150, 20);
            this.btnUpload.Text = "<Ctrl I> &Importar";
            this.btnUpload.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.AutoSize = false;
            this.btnDownload.DropDownButtonWidth = 0;
            this.btnDownload.Image = global::AERMOD.Properties.Resources.down;
            this.btnDownload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(174, 20);
            this.btnDownload.Text = "<Ctrl E> &Exportar";
            this.btnDownload.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnConverter
            // 
            this.btnConverter.AutoSize = false;
            this.btnConverter.DropDownButtonWidth = 0;
            this.btnConverter.Image = global::AERMOD.Properties.Resources.Atualizar2;
            this.btnConverter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnConverter.Name = "btnConverter";
            this.btnConverter.Size = new System.Drawing.Size(170, 20);
            this.btnConverter.Text = "<Enter> &Converter";
            this.btnConverter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnConverter.Click += new System.EventHandler(this.btnConverter_Click);
            // 
            // btnSair
            // 
            this.btnSair.AutoSize = false;
            this.btnSair.DropDownButtonWidth = 0;
            this.btnSair.Image = global::AERMOD.Properties.Resources.Sair;
            this.btnSair.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(128, 20);
            this.btnSair.Text = "<Esc> &Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(6, 67);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView.Size = new System.Drawing.Size(772, 465);
            this.dataGridView.TabIndex = 8;
            // 
            // lbFusoHorario
            // 
            this.lbFusoHorario.AutoSize = true;
            this.lbFusoHorario.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFusoHorario.Location = new System.Drawing.Point(497, 9);
            this.lbFusoHorario.Name = "lbFusoHorario";
            this.lbFusoHorario.Size = new System.Drawing.Size(111, 16);
            this.lbFusoHorario.TabIndex = 19;
            this.lbFusoHorario.Text = "Fuso horário:";
            // 
            // cbxFusoHorario
            // 
            this.cbxFusoHorario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFusoHorario.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxFusoHorario.FormattingEnabled = true;
            this.cbxFusoHorario.Location = new System.Drawing.Point(611, 5);
            this.cbxFusoHorario.Name = "cbxFusoHorario";
            this.cbxFusoHorario.Size = new System.Drawing.Size(166, 24);
            this.cbxFusoHorario.TabIndex = 3;
            // 
            // lbUF
            // 
            this.lbUF.AutoSize = true;
            this.lbUF.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUF.Location = new System.Drawing.Point(371, 8);
            this.lbUF.Name = "lbUF";
            this.lbUF.Size = new System.Drawing.Size(63, 16);
            this.lbUF.TabIndex = 18;
            this.lbUF.Text = "Estado:";
            // 
            // cbxUF
            // 
            this.cbxUF.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxUF.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxUF.FormattingEnabled = true;
            this.cbxUF.Location = new System.Drawing.Point(437, 4);
            this.cbxUF.Name = "cbxUF";
            this.cbxUF.Size = new System.Drawing.Size(58, 24);
            this.cbxUF.TabIndex = 2;
            // 
            // btnConsulta
            // 
            this.btnConsulta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConsulta.Image = global::AERMOD.Properties.Resources.search_16x16;
            this.btnConsulta.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnConsulta.Location = new System.Drawing.Point(653, 35);
            this.btnConsulta.Name = "btnConsulta";
            this.btnConsulta.Size = new System.Drawing.Size(124, 24);
            this.btnConsulta.TabIndex = 7;
            this.btnConsulta.Text = "    <F2> C&onsulta";
            this.btnConsulta.UseVisualStyleBackColor = true;
            this.btnConsulta.Click += new System.EventHandler(this.btnConsulta_Click);
            // 
            // tbxCodigo
            // 
            this.tbxCodigo.CampoHabilitado = true;
            this.tbxCodigo.ColumnName = null;
            this.tbxCodigo.Decimais = 3;
            this.tbxCodigo.FocusColor = System.Drawing.Color.Empty;
            this.tbxCodigo.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCodigo.Inteiro = 5;
            this.tbxCodigo.InteiroDefault = AERMOD.LIB.Componentes.TextBoxMaskLIB.ValorInteiroDefault.Zero;
            this.tbxCodigo.Location = new System.Drawing.Point(69, 5);
            this.tbxCodigo.Mascara = AERMOD.LIB.Componentes.TextBoxMaskLIB.Mask.Inteiro;
            this.tbxCodigo.Name = "tbxCodigo";
            this.tbxCodigo.Size = new System.Drawing.Size(48, 22);
            this.tbxCodigo.TabIndex = 0;
            this.tbxCodigo.Text = "0";
            this.tbxCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbCodigo
            // 
            this.lbCodigo.AutoSize = true;
            this.lbCodigo.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCodigo.Location = new System.Drawing.Point(3, 8);
            this.lbCodigo.Name = "lbCodigo";
            this.lbCodigo.Size = new System.Drawing.Size(63, 16);
            this.lbCodigo.TabIndex = 269;
            this.lbCodigo.Text = "Código:";
            // 
            // lbCidade
            // 
            this.lbCidade.AutoSize = true;
            this.lbCidade.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCidade.Location = new System.Drawing.Point(120, 8);
            this.lbCidade.Name = "lbCidade";
            this.lbCidade.Size = new System.Drawing.Size(63, 16);
            this.lbCidade.TabIndex = 275;
            this.lbCidade.Text = "Cidade:";
            // 
            // tbxCidade
            // 
            this.tbxCidade.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbxCidade.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCidade.Location = new System.Drawing.Point(186, 5);
            this.tbxCidade.MaxLength = 22;
            this.tbxCidade.Name = "tbxCidade";
            this.tbxCidade.Size = new System.Drawing.Size(183, 22);
            this.tbxCidade.TabIndex = 1;
            // 
            // tbxLongitude
            // 
            this.tbxLongitude.CampoHabilitado = true;
            this.tbxLongitude.ColumnName = null;
            this.tbxLongitude.FocusColor = System.Drawing.Color.Empty;
            this.tbxLongitude.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxLongitude.Location = new System.Drawing.Point(279, 37);
            this.tbxLongitude.Mascara = AERMOD.LIB.Componentes.TextBoxMaskLIB.Mask.Decimal;
            this.tbxLongitude.Name = "tbxLongitude";
            this.tbxLongitude.Size = new System.Drawing.Size(60, 22);
            this.tbxLongitude.TabIndex = 5;
            this.tbxLongitude.Text = "0,00";
            this.tbxLongitude.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbxLatitude
            // 
            this.tbxLatitude.CampoHabilitado = true;
            this.tbxLatitude.ColumnName = null;
            this.tbxLatitude.FocusColor = System.Drawing.Color.Empty;
            this.tbxLatitude.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxLatitude.Location = new System.Drawing.Point(102, 37);
            this.tbxLatitude.Mascara = AERMOD.LIB.Componentes.TextBoxMaskLIB.Mask.Decimal;
            this.tbxLatitude.Name = "tbxLatitude";
            this.tbxLatitude.Size = new System.Drawing.Size(60, 22);
            this.tbxLatitude.TabIndex = 4;
            this.tbxLatitude.Text = "0,00";
            this.tbxLatitude.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbLongitude
            // 
            this.lbLongitude.AutoSize = true;
            this.lbLongitude.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLongitude.Location = new System.Drawing.Point(173, 40);
            this.lbLongitude.Name = "lbLongitude";
            this.lbLongitude.Size = new System.Drawing.Size(103, 16);
            this.lbLongitude.TabIndex = 279;
            this.lbLongitude.Text = "Long.(dd-W):";
            // 
            // lbLatitude
            // 
            this.lbLatitude.AutoSize = true;
            this.lbLatitude.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLatitude.Location = new System.Drawing.Point(3, 40);
            this.lbLatitude.Name = "lbLatitude";
            this.lbLatitude.Size = new System.Drawing.Size(95, 16);
            this.lbLatitude.TabIndex = 278;
            this.lbLatitude.Text = "Lat.(dd-S):";
            // 
            // tbxElevacao
            // 
            this.tbxElevacao.CampoHabilitado = true;
            this.tbxElevacao.ColumnName = null;
            this.tbxElevacao.Decimais = 3;
            this.tbxElevacao.FocusColor = System.Drawing.Color.Empty;
            this.tbxElevacao.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxElevacao.Inteiro = 4;
            this.tbxElevacao.InteiroDefault = AERMOD.LIB.Componentes.TextBoxMaskLIB.ValorInteiroDefault.Zero;
            this.tbxElevacao.Location = new System.Drawing.Point(466, 37);
            this.tbxElevacao.Mascara = AERMOD.LIB.Componentes.TextBoxMaskLIB.Mask.Inteiro;
            this.tbxElevacao.Name = "tbxElevacao";
            this.tbxElevacao.Size = new System.Drawing.Size(48, 22);
            this.tbxElevacao.TabIndex = 6;
            this.tbxElevacao.Text = "0";
            this.tbxElevacao.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbElevacao
            // 
            this.lbElevacao.AutoSize = true;
            this.lbElevacao.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbElevacao.Location = new System.Drawing.Point(352, 40);
            this.lbElevacao.Name = "lbElevacao";
            this.lbElevacao.Size = new System.Drawing.Size(111, 16);
            this.lbElevacao.TabIndex = 281;
            this.lbElevacao.Text = "Elevação (m):";
            // 
            // FrmSAM_MESONET
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.tbxElevacao);
            this.Controls.Add(this.lbElevacao);
            this.Controls.Add(this.tbxLongitude);
            this.Controls.Add(this.tbxLatitude);
            this.Controls.Add(this.lbLongitude);
            this.Controls.Add(this.lbLatitude);
            this.Controls.Add(this.lbCidade);
            this.Controls.Add(this.tbxCidade);
            this.Controls.Add(this.tbxCodigo);
            this.Controls.Add(this.lbCodigo);
            this.Controls.Add(this.btnConsulta);
            this.Controls.Add(this.lbFusoHorario);
            this.Controls.Add(this.cbxFusoHorario);
            this.Controls.Add(this.lbUF);
            this.Controls.Add(this.cbxUF);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.statusStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSAM_MESONET";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Editor (.SAM) MESONET";
            this.Load += new System.EventHandler(this.FrmSAM_MESONET_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmSAM_MESONET_KeyDown);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripSplitButton btnAjuda;
        private System.Windows.Forms.ToolStripSplitButton btnUpload;
        private System.Windows.Forms.ToolStripSplitButton btnDownload;
        private System.Windows.Forms.ToolStripSplitButton btnConverter;
        private System.Windows.Forms.ToolStripSplitButton btnSair;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Label lbFusoHorario;
        private System.Windows.Forms.ComboBox cbxFusoHorario;
        private System.Windows.Forms.Label lbUF;
        private System.Windows.Forms.ComboBox cbxUF;
        private System.Windows.Forms.Button btnConsulta;
        private LIB.Componentes.TextBoxMaskLIB tbxCodigo;
        private System.Windows.Forms.Label lbCodigo;
        private System.Windows.Forms.Label lbCidade;
        private System.Windows.Forms.TextBox tbxCidade;
        private LIB.Componentes.TextBoxMaskLIB tbxLongitude;
        private LIB.Componentes.TextBoxMaskLIB tbxLatitude;
        private System.Windows.Forms.Label lbLongitude;
        private System.Windows.Forms.Label lbLatitude;
        private LIB.Componentes.TextBoxMaskLIB tbxElevacao;
        private System.Windows.Forms.Label lbElevacao;
    }
}