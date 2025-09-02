namespace AERMOD.CamadaApresentacao.AERMAP
{
    partial class FrmConsultaGrade
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvGrade = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxLIBColumn1 = new AERMOD.LIB.Componentes.GridView.DataGridViewCheckBoxLIBColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STATUS = new AERMOD.LIB.Componentes.GridView.DataGridViewCheckBoxLIBColumn();
            this.CODIGO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DESCRICAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CODIGO_DOMINIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EM_USO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrade)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvGrade
            // 
            this.dgvGrade.AllowUserToAddRows = false;
            this.dgvGrade.AllowUserToDeleteRows = false;
            this.dgvGrade.AllowUserToResizeColumns = false;
            this.dgvGrade.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgvGrade.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvGrade.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGrade.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STATUS,
            this.CODIGO,
            this.DESCRICAO,
            this.CODIGO_DOMINIO,
            this.EM_USO});
            this.dgvGrade.Location = new System.Drawing.Point(5, 5);
            this.dgvGrade.MultiSelect = false;
            this.dgvGrade.Name = "dgvGrade";
            this.dgvGrade.RowHeadersVisible = false;
            this.dgvGrade.RowHeadersWidth = 20;
            this.dgvGrade.ShowCellToolTips = false;
            this.dgvGrade.Size = new System.Drawing.Size(227, 368);
            this.dgvGrade.TabIndex = 0;
            this.dgvGrade.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGrade_CellDoubleClick);            
            this.dgvGrade.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGrade_CellLeave);
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
            this.dataGridViewTextBoxColumn1.DataPropertyName = "CODIGO";
            this.dataGridViewTextBoxColumn1.HeaderText = "ID";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 40;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "DESCRICAO";
            this.dataGridViewTextBoxColumn2.HeaderText = "Descrição";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 156;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "CODIGO_DOMINIO";
            this.dataGridViewTextBoxColumn3.HeaderText = "Domínio";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Visible = false;
            // 
            // STATUS
            // 
            this.STATUS.DataPropertyName = "STATUS";
            this.STATUS.HeaderText = "";
            this.STATUS.Name = "STATUS";
            this.STATUS.Width = 28;
            // 
            // CODIGO
            // 
            this.CODIGO.DataPropertyName = "CODIGO";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.CODIGO.DefaultCellStyle = dataGridViewCellStyle2;
            this.CODIGO.HeaderText = "ID";
            this.CODIGO.Name = "CODIGO";
            this.CODIGO.Width = 40;
            // 
            // DESCRICAO
            // 
            this.DESCRICAO.DataPropertyName = "DESCRICAO";
            this.DESCRICAO.HeaderText = "Descrição";
            this.DESCRICAO.Name = "DESCRICAO";
            this.DESCRICAO.Width = 156;
            // 
            // CODIGO_DOMINIO
            // 
            this.CODIGO_DOMINIO.DataPropertyName = "CODIGO_DOMINIO";
            this.CODIGO_DOMINIO.HeaderText = "Domínio";
            this.CODIGO_DOMINIO.Name = "CODIGO_DOMINIO";
            this.CODIGO_DOMINIO.Visible = false;
            // 
            // EM_USO
            // 
            this.EM_USO.DataPropertyName = "EM_USO";
            this.EM_USO.HeaderText = "Em uso";
            this.EM_USO.Name = "EM_USO";
            this.EM_USO.Visible = false;
            // 
            // FrmConsultaGrade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(237, 378);
            this.Controls.Add(this.dgvGrade);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmConsultaGrade";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consulta de grades";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmConsultaGrade_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGrade)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvGrade;
        private LIB.Componentes.GridView.DataGridViewCheckBoxLIBColumn dataGridViewCheckBoxLIBColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private LIB.Componentes.GridView.DataGridViewCheckBoxLIBColumn STATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn CODIGO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DESCRICAO;
        private System.Windows.Forms.DataGridViewTextBoxColumn CODIGO_DOMINIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn EM_USO;
    }
}