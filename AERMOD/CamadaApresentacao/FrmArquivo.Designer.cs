
namespace AERMOD.CamadaApresentacao
{
    partial class FrmArquivo
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvArquivo = new System.Windows.Forms.DataGridView();
            this.STATUS = new AERMOD.LIB.Componentes.GridView.DataGridViewCheckBoxLIBColumn();
            this.DESCRICAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CODIGO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SEQUENCIA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnImportarArquivo = new System.Windows.Forms.Button();
            this.btnExportarArquivo = new System.Windows.Forms.Button();
            this.dataGridViewCheckBoxLIBColumn1 = new AERMOD.LIB.Componentes.GridView.DataGridViewCheckBoxLIBColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvArquivo)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvArquivo
            // 
            this.dgvArquivo.AllowUserToAddRows = false;
            this.dgvArquivo.AllowUserToDeleteRows = false;
            this.dgvArquivo.AllowUserToResizeColumns = false;
            this.dgvArquivo.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgvArquivo.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvArquivo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvArquivo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STATUS,
            this.DESCRICAO,
            this.CODIGO,
            this.SEQUENCIA});
            this.dgvArquivo.Location = new System.Drawing.Point(5, 5);
            this.dgvArquivo.MultiSelect = false;
            this.dgvArquivo.Name = "dgvArquivo";
            this.dgvArquivo.RowHeadersVisible = false;
            this.dgvArquivo.RowHeadersWidth = 20;
            this.dgvArquivo.ShowCellToolTips = false;
            this.dgvArquivo.Size = new System.Drawing.Size(227, 258);
            this.dgvArquivo.TabIndex = 0;
            this.dgvArquivo.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvArquivo_CellDoubleClick);
            this.dgvArquivo.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvArquivo_CellLeave);
            this.dgvArquivo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvArquivo_KeyDown);
            // 
            // STATUS
            // 
            this.STATUS.DataPropertyName = "STATUS";
            this.STATUS.HeaderText = "";
            this.STATUS.Name = "STATUS";
            this.STATUS.Width = 28;
            // 
            // DESCRICAO
            // 
            this.DESCRICAO.DataPropertyName = "DESCRICAO";
            this.DESCRICAO.HeaderText = "Descrição";
            this.DESCRICAO.Name = "DESCRICAO";
            this.DESCRICAO.Width = 196;
            // 
            // CODIGO
            // 
            this.CODIGO.DataPropertyName = "CODIGO";
            this.CODIGO.HeaderText = "Código";
            this.CODIGO.Name = "CODIGO";
            this.CODIGO.Visible = false;
            // 
            // SEQUENCIA
            // 
            this.SEQUENCIA.DataPropertyName = "SEQUENCIA";
            this.SEQUENCIA.HeaderText = "Sequência";
            this.SEQUENCIA.Name = "SEQUENCIA";
            this.SEQUENCIA.Visible = false;
            // 
            // btnImportarArquivo
            // 
            this.btnImportarArquivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportarArquivo.Image = global::AERMOD.Properties.Resources.add;
            this.btnImportarArquivo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnImportarArquivo.Location = new System.Drawing.Point(4, 266);
            this.btnImportarArquivo.Name = "btnImportarArquivo";
            this.btnImportarArquivo.Size = new System.Drawing.Size(112, 24);
            this.btnImportarArquivo.TabIndex = 1;
            this.btnImportarArquivo.Text = "    <F5> &Importar";
            this.btnImportarArquivo.UseVisualStyleBackColor = true;
            this.btnImportarArquivo.Click += new System.EventHandler(this.btnImportarArquivo_Click);
            // 
            // btnExportarArquivo
            // 
            this.btnExportarArquivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportarArquivo.Image = global::AERMOD.Properties.Resources.down;
            this.btnExportarArquivo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExportarArquivo.Location = new System.Drawing.Point(121, 266);
            this.btnExportarArquivo.Name = "btnExportarArquivo";
            this.btnExportarArquivo.Size = new System.Drawing.Size(112, 24);
            this.btnExportarArquivo.TabIndex = 2;
            this.btnExportarArquivo.Text = "    <F6> &Exportar";
            this.btnExportarArquivo.UseVisualStyleBackColor = true;
            this.btnExportarArquivo.Click += new System.EventHandler(this.btnExportarArquivo_Click);
            // 
            // dataGridViewCheckBoxLIBColumn1
            // 
            this.dataGridViewCheckBoxLIBColumn1.DataPropertyName = "STATUS";
            this.dataGridViewCheckBoxLIBColumn1.HeaderText = "";
            this.dataGridViewCheckBoxLIBColumn1.Name = "dataGridViewCheckBoxLIBColumn1";
            this.dataGridViewCheckBoxLIBColumn1.Width = 28;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "DESCRICAO";
            this.dataGridViewTextBoxColumn1.HeaderText = "Descrição";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 196;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "CODIGO";
            this.dataGridViewTextBoxColumn2.HeaderText = "Código";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Visible = false;
            // 
            // FrmArquivo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(237, 293);
            this.Controls.Add(this.btnExportarArquivo);
            this.Controls.Add(this.btnImportarArquivo);
            this.Controls.Add(this.dgvArquivo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmArquivo";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Importar/Exportar arquivos";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmArquivo_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvArquivo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvArquivo;
        private System.Windows.Forms.Button btnImportarArquivo;
        private System.Windows.Forms.Button btnExportarArquivo;
        private LIB.Componentes.GridView.DataGridViewCheckBoxLIBColumn dataGridViewCheckBoxLIBColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private LIB.Componentes.GridView.DataGridViewCheckBoxLIBColumn STATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn DESCRICAO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CODIGO;
        private System.Windows.Forms.DataGridViewTextBoxColumn SEQUENCIA;
    }
}