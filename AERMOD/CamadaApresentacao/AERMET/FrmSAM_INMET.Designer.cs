
namespace AERMOD
{
    partial class FrmSAM_INMET
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.btnAjuda = new System.Windows.Forms.ToolStripSplitButton();
            this.btnUpload = new System.Windows.Forms.ToolStripSplitButton();
            this.btnDownload = new System.Windows.Forms.ToolStripSplitButton();
            this.btnConverter = new System.Windows.Forms.ToolStripSplitButton();
            this.btnSair = new System.Windows.Forms.ToolStripSplitButton();
            this.cbxUF = new System.Windows.Forms.ComboBox();
            this.lbUF = new System.Windows.Forms.Label();
            this.numericPrimeiraLinha = new System.Windows.Forms.NumericUpDown();
            this.lbPrimeiraLinha = new System.Windows.Forms.Label();
            this.btnDefinirPrimeiraLinha = new System.Windows.Forms.Button();
            this.btnDefinirUltimaLinha = new System.Windows.Forms.Button();
            this.lbUltimaLinha = new System.Windows.Forms.Label();
            this.numericUltimaLinha = new System.Windows.Forms.NumericUpDown();
            this.lbFusoHorario = new System.Windows.Forms.Label();
            this.cbxFusoHorario = new System.Windows.Forms.ComboBox();
            this.btnArquivos = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericPrimeiraLinha)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUltimaLinha)).BeginInit();
            this.SuspendLayout();
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
            this.dataGridView.TabIndex = 7;
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
            this.statusStrip.TabIndex = 7;
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
            // cbxUF
            // 
            this.cbxUF.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxUF.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxUF.FormattingEnabled = true;
            this.cbxUF.Location = new System.Drawing.Point(84, 35);
            this.cbxUF.Name = "cbxUF";
            this.cbxUF.Size = new System.Drawing.Size(58, 24);
            this.cbxUF.TabIndex = 4;
            // 
            // lbUF
            // 
            this.lbUF.AutoSize = true;
            this.lbUF.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUF.Location = new System.Drawing.Point(8, 39);
            this.lbUF.Name = "lbUF";
            this.lbUF.Size = new System.Drawing.Size(63, 16);
            this.lbUF.TabIndex = 9;
            this.lbUF.Text = "Estado:";
            // 
            // numericPrimeiraLinha
            // 
            this.numericPrimeiraLinha.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericPrimeiraLinha.Location = new System.Drawing.Point(259, 5);
            this.numericPrimeiraLinha.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numericPrimeiraLinha.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericPrimeiraLinha.Name = "numericPrimeiraLinha";
            this.numericPrimeiraLinha.Size = new System.Drawing.Size(75, 22);
            this.numericPrimeiraLinha.TabIndex = 0;
            this.numericPrimeiraLinha.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericPrimeiraLinha.Enter += new System.EventHandler(this.numericPrimeiraLinha_Enter);
            this.numericPrimeiraLinha.Leave += new System.EventHandler(this.numericPrimeiraLinha_Leave);
            // 
            // lbPrimeiraLinha
            // 
            this.lbPrimeiraLinha.AutoSize = true;
            this.lbPrimeiraLinha.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPrimeiraLinha.Location = new System.Drawing.Point(2, 8);
            this.lbPrimeiraLinha.Name = "lbPrimeiraLinha";
            this.lbPrimeiraLinha.Size = new System.Drawing.Size(247, 16);
            this.lbPrimeiraLinha.TabIndex = 10;
            this.lbPrimeiraLinha.Text = "Primeira linha para conversão:";
            // 
            // btnDefinirPrimeiraLinha
            // 
            this.btnDefinirPrimeiraLinha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDefinirPrimeiraLinha.Location = new System.Drawing.Point(342, 4);
            this.btnDefinirPrimeiraLinha.Name = "btnDefinirPrimeiraLinha";
            this.btnDefinirPrimeiraLinha.Size = new System.Drawing.Size(52, 24);
            this.btnDefinirPrimeiraLinha.TabIndex = 1;
            this.btnDefinirPrimeiraLinha.Text = "Definir";
            this.btnDefinirPrimeiraLinha.UseVisualStyleBackColor = true;
            this.btnDefinirPrimeiraLinha.Click += new System.EventHandler(this.btnDefinirPrimeiraLinha_Click);
            // 
            // btnDefinirUltimaLinha
            // 
            this.btnDefinirUltimaLinha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDefinirUltimaLinha.Location = new System.Drawing.Point(726, 4);
            this.btnDefinirUltimaLinha.Name = "btnDefinirUltimaLinha";
            this.btnDefinirUltimaLinha.Size = new System.Drawing.Size(52, 24);
            this.btnDefinirUltimaLinha.TabIndex = 3;
            this.btnDefinirUltimaLinha.Text = "Definir";
            this.btnDefinirUltimaLinha.UseVisualStyleBackColor = true;
            this.btnDefinirUltimaLinha.Click += new System.EventHandler(this.btnDefinirUltimaLinha_Click);
            // 
            // lbUltimaLinha
            // 
            this.lbUltimaLinha.AutoSize = true;
            this.lbUltimaLinha.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUltimaLinha.Location = new System.Drawing.Point(401, 8);
            this.lbUltimaLinha.Name = "lbUltimaLinha";
            this.lbUltimaLinha.Size = new System.Drawing.Size(231, 16);
            this.lbUltimaLinha.TabIndex = 13;
            this.lbUltimaLinha.Text = "Última linha para conversão:";
            // 
            // numericUltimaLinha
            // 
            this.numericUltimaLinha.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUltimaLinha.Location = new System.Drawing.Point(643, 5);
            this.numericUltimaLinha.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numericUltimaLinha.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUltimaLinha.Name = "numericUltimaLinha";
            this.numericUltimaLinha.Size = new System.Drawing.Size(75, 22);
            this.numericUltimaLinha.TabIndex = 2;
            this.numericUltimaLinha.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUltimaLinha.Enter += new System.EventHandler(this.numericPrimeiraLinha_Enter);
            this.numericUltimaLinha.Leave += new System.EventHandler(this.numericUltimaLinha_Leave);
            // 
            // lbFusoHorario
            // 
            this.lbFusoHorario.AutoSize = true;
            this.lbFusoHorario.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFusoHorario.Location = new System.Drawing.Point(163, 39);
            this.lbFusoHorario.Name = "lbFusoHorario";
            this.lbFusoHorario.Size = new System.Drawing.Size(111, 16);
            this.lbFusoHorario.TabIndex = 15;
            this.lbFusoHorario.Text = "Fuso horário:";
            // 
            // cbxFusoHorario
            // 
            this.cbxFusoHorario.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFusoHorario.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxFusoHorario.FormattingEnabled = true;
            this.cbxFusoHorario.Location = new System.Drawing.Point(287, 35);
            this.cbxFusoHorario.Name = "cbxFusoHorario";
            this.cbxFusoHorario.Size = new System.Drawing.Size(166, 24);
            this.cbxFusoHorario.TabIndex = 5;
            // 
            // btnArquivos
            // 
            this.btnArquivos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnArquivos.Image = global::AERMOD.Properties.Resources.search_16x16;
            this.btnArquivos.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnArquivos.Location = new System.Drawing.Point(654, 35);
            this.btnArquivos.Name = "btnArquivos";
            this.btnArquivos.Size = new System.Drawing.Size(124, 24);
            this.btnArquivos.TabIndex = 6;
            this.btnArquivos.Text = "    <F2> C&onsulta";
            this.btnArquivos.UseVisualStyleBackColor = true;
            this.btnArquivos.Click += new System.EventHandler(this.btnArquivos_Click);
            // 
            // FrmSAM
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.btnArquivos);
            this.Controls.Add(this.lbFusoHorario);
            this.Controls.Add(this.cbxFusoHorario);
            this.Controls.Add(this.btnDefinirUltimaLinha);
            this.Controls.Add(this.lbUltimaLinha);
            this.Controls.Add(this.numericUltimaLinha);
            this.Controls.Add(this.btnDefinirPrimeiraLinha);
            this.Controls.Add(this.lbPrimeiraLinha);
            this.Controls.Add(this.numericPrimeiraLinha);
            this.Controls.Add(this.lbUF);
            this.Controls.Add(this.cbxUF);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.dataGridView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmSAM";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Editor (.SAM) INMET";
            this.Load += new System.EventHandler(this.FrmSAM_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmSAM_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericPrimeiraLinha)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUltimaLinha)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.ToolStripSplitButton btnAjuda;
        private System.Windows.Forms.ToolStripSplitButton btnUpload;
        private System.Windows.Forms.ToolStripSplitButton btnDownload;
        private System.Windows.Forms.ToolStripSplitButton btnConverter;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripSplitButton btnSair;
        private System.Windows.Forms.ComboBox cbxUF;
        private System.Windows.Forms.Label lbUF;
        private System.Windows.Forms.NumericUpDown numericPrimeiraLinha;
        private System.Windows.Forms.Label lbPrimeiraLinha;
        private System.Windows.Forms.Button btnDefinirPrimeiraLinha;
        private System.Windows.Forms.Button btnDefinirUltimaLinha;
        private System.Windows.Forms.Label lbUltimaLinha;
        private System.Windows.Forms.NumericUpDown numericUltimaLinha;
        private System.Windows.Forms.Label lbFusoHorario;
        private System.Windows.Forms.ComboBox cbxFusoHorario;
        private System.Windows.Forms.Button btnArquivos;
    }
}

