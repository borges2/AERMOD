
namespace AERMOD
{
    partial class FrmFonteAERMAP
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbTipoFonte = new System.Windows.Forms.Label();
            this.cbxTipoFonte = new System.Windows.Forms.ComboBox();
            this.statusStripConsulta = new System.Windows.Forms.StatusStrip();
            this.btnAjuda = new System.Windows.Forms.ToolStripSplitButton();
            this.btnInserir = new System.Windows.Forms.ToolStripSplitButton();
            this.btnAlterar = new System.Windows.Forms.ToolStripSplitButton();
            this.btnExcluir = new System.Windows.Forms.ToolStripSplitButton();
            this.btnSair = new System.Windows.Forms.ToolStripSplitButton();
            this.dgvFonte = new System.Windows.Forms.DataGridView();
            this.STATUS = new AERMOD.LIB.Componentes.GridView.DataGridViewCheckBoxLIBColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DESCRICAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIPO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CODIGO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EM_USO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbxY = new AERMOD.LIB.Componentes.TextBoxMaskLIB();
            this.tbxX = new AERMOD.LIB.Componentes.TextBoxMaskLIB();
            this.lbY = new System.Windows.Forms.Label();
            this.lbX = new System.Windows.Forms.Label();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.lbDescricao = new System.Windows.Forms.Label();
            this.tbxDescricao = new System.Windows.Forms.TextBox();
            this.statusStripConsulta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFonte)).BeginInit();
            this.SuspendLayout();
            // 
            // lbTipoFonte
            // 
            this.lbTipoFonte.AutoSize = true;
            this.lbTipoFonte.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTipoFonte.Location = new System.Drawing.Point(1, 44);
            this.lbTipoFonte.Name = "lbTipoFonte";
            this.lbTipoFonte.Size = new System.Drawing.Size(111, 16);
            this.lbTipoFonte.TabIndex = 171;
            this.lbTipoFonte.Text = "Tipo da fonte";
            // 
            // cbxTipoFonte
            // 
            this.cbxTipoFonte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTipoFonte.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxTipoFonte.FormattingEnabled = true;
            this.cbxTipoFonte.Location = new System.Drawing.Point(4, 63);
            this.cbxTipoFonte.Name = "cbxTipoFonte";
            this.cbxTipoFonte.Size = new System.Drawing.Size(113, 24);
            this.cbxTipoFonte.TabIndex = 1;
            // 
            // statusStripConsulta
            // 
            this.statusStripConsulta.BackColor = System.Drawing.Color.LightBlue;
            this.statusStripConsulta.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAjuda,
            this.btnInserir,
            this.btnAlterar,
            this.btnExcluir,
            this.btnSair});
            this.statusStripConsulta.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStripConsulta.Location = new System.Drawing.Point(0, 348);
            this.statusStripConsulta.Name = "statusStripConsulta";
            this.statusStripConsulta.Size = new System.Drawing.Size(514, 22);
            this.statusStripConsulta.SizingGrip = false;
            this.statusStripConsulta.TabIndex = 174;
            this.statusStripConsulta.Text = "statusStrip2";
            // 
            // btnAjuda
            // 
            this.btnAjuda.AutoSize = false;
            this.btnAjuda.DropDownButtonWidth = 0;
            this.btnAjuda.Image = global::AERMOD.Properties.Resources.ajuda;
            this.btnAjuda.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAjuda.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAjuda.Name = "btnAjuda";
            this.btnAjuda.Size = new System.Drawing.Size(90, 20);
            this.btnAjuda.Text = "<F1> &Ajuda";
            this.btnAjuda.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAjuda.Click += new System.EventHandler(this.btnAjuda_Click);
            // 
            // btnInserir
            // 
            this.btnInserir.AutoSize = false;
            this.btnInserir.DropDownButtonWidth = 0;
            this.btnInserir.Image = global::AERMOD.Properties.Resources.add;
            this.btnInserir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnInserir.Name = "btnInserir";
            this.btnInserir.Size = new System.Drawing.Size(107, 20);
            this.btnInserir.Text = "<Insert> &Inserir";
            this.btnInserir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnInserir.Click += new System.EventHandler(this.btnInserir_Click);
            // 
            // btnAlterar
            // 
            this.btnAlterar.AutoSize = false;
            this.btnAlterar.DropDownButtonWidth = 0;
            this.btnAlterar.Image = global::AERMOD.Properties.Resources.Editar;
            this.btnAlterar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAlterar.Name = "btnAlterar";
            this.btnAlterar.Size = new System.Drawing.Size(110, 20);
            this.btnAlterar.Text = "<Alt A> A&lterar";
            this.btnAlterar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAlterar.Click += new System.EventHandler(this.btnAlterar_Click);
            // 
            // btnExcluir
            // 
            this.btnExcluir.AutoSize = false;
            this.btnExcluir.DropDownButtonWidth = 0;
            this.btnExcluir.Image = global::AERMOD.Properties.Resources.delete;
            this.btnExcluir.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(112, 20);
            this.btnExcluir.Text = "<Delete> &Excluir";
            this.btnExcluir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // btnSair
            // 
            this.btnSair.AutoSize = false;
            this.btnSair.DropDownButtonWidth = 0;
            this.btnSair.Image = global::AERMOD.Properties.Resources.Sair;
            this.btnSair.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(80, 20);
            this.btnSair.Text = "<Esc> &Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // dgvFonte
            // 
            this.dgvFonte.AllowUserToAddRows = false;
            this.dgvFonte.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgvFonte.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvFonte.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvFonte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFonte.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STATUS,
            this.ID,
            this.DESCRICAO,
            this.TIPO,
            this.X,
            this.Y,
            this.CODIGO,
            this.EM_USO});
            this.dgvFonte.Location = new System.Drawing.Point(4, 93);
            this.dgvFonte.MultiSelect = false;
            this.dgvFonte.Name = "dgvFonte";
            this.dgvFonte.ReadOnly = true;
            this.dgvFonte.RowHeadersVisible = false;
            this.dgvFonte.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvFonte.Size = new System.Drawing.Size(506, 250);
            this.dgvFonte.TabIndex = 5;
            this.dgvFonte.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFonte_CellDoubleClick);
            this.dgvFonte.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvFonte_CellFormatting);
            this.dgvFonte.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFonte_CellLeave);
            this.dgvFonte.SelectionChanged += new System.EventHandler(this.dgvFonte_SelectionChanged);
            this.dgvFonte.Enter += new System.EventHandler(this.dgvFonte_Enter);
            // 
            // STATUS
            // 
            this.STATUS.DataPropertyName = "STATUS";
            this.STATUS.Frozen = true;
            this.STATUS.HeaderText = "";
            this.STATUS.Name = "STATUS";
            this.STATUS.ReadOnly = true;
            this.STATUS.Width = 28;
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ID.DefaultCellStyle = dataGridViewCellStyle2;
            this.ID.Frozen = true;
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Width = 50;
            // 
            // DESCRICAO
            // 
            this.DESCRICAO.DataPropertyName = "DESCRICAO";
            this.DESCRICAO.HeaderText = "Descrição";
            this.DESCRICAO.Name = "DESCRICAO";
            this.DESCRICAO.ReadOnly = true;
            this.DESCRICAO.Width = 88;
            // 
            // TIPO
            // 
            this.TIPO.DataPropertyName = "TIPO";
            this.TIPO.HeaderText = "TIPO";
            this.TIPO.Name = "TIPO";
            this.TIPO.ReadOnly = true;
            this.TIPO.Width = 70;
            // 
            // X
            // 
            this.X.DataPropertyName = "X";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.X.DefaultCellStyle = dataGridViewCellStyle3;
            this.X.HeaderText = "X";
            this.X.Name = "X";
            this.X.ReadOnly = true;
            this.X.Width = 125;
            // 
            // Y
            // 
            this.Y.DataPropertyName = "Y";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Y.DefaultCellStyle = dataGridViewCellStyle4;
            this.Y.HeaderText = "Y";
            this.Y.Name = "Y";
            this.Y.ReadOnly = true;
            this.Y.Width = 125;
            // 
            // CODIGO
            // 
            this.CODIGO.DataPropertyName = "CODIGO";
            this.CODIGO.HeaderText = "CODIGO";
            this.CODIGO.Name = "CODIGO";
            this.CODIGO.ReadOnly = true;
            this.CODIGO.Visible = false;
            // 
            // EM_USO
            // 
            this.EM_USO.DataPropertyName = "EM_USO";
            this.EM_USO.HeaderText = "EM_USO";
            this.EM_USO.Name = "EM_USO";
            this.EM_USO.ReadOnly = true;
            this.EM_USO.Visible = false;
            // 
            // tbxY
            // 
            this.tbxY.CampoHabilitado = true;
            this.tbxY.ColumnName = null;
            this.tbxY.Decimais = 3;
            this.tbxY.FocusColor = System.Drawing.Color.Empty;
            this.tbxY.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxY.Inteiro = 10;
            this.tbxY.Location = new System.Drawing.Point(279, 64);
            this.tbxY.Mascara = AERMOD.LIB.Componentes.TextBoxMaskLIB.Mask.Decimal;
            this.tbxY.Name = "tbxY";
            this.tbxY.Size = new System.Drawing.Size(148, 22);
            this.tbxY.TabIndex = 3;
            this.tbxY.Text = "0,000";
            this.tbxY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbxX
            // 
            this.tbxX.CampoHabilitado = true;
            this.tbxX.ColumnName = null;
            this.tbxX.Decimais = 3;
            this.tbxX.FocusColor = System.Drawing.Color.Empty;
            this.tbxX.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxX.Inteiro = 10;
            this.tbxX.Location = new System.Drawing.Point(125, 64);
            this.tbxX.Mascara = AERMOD.LIB.Componentes.TextBoxMaskLIB.Mask.Decimal;
            this.tbxX.Name = "tbxX";
            this.tbxX.Size = new System.Drawing.Size(148, 22);
            this.tbxX.TabIndex = 2;
            this.tbxX.Text = "0,000";
            this.tbxX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbY
            // 
            this.lbY.AutoSize = true;
            this.lbY.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbY.Location = new System.Drawing.Point(276, 45);
            this.lbY.Name = "lbY";
            this.lbY.Size = new System.Drawing.Size(63, 16);
            this.lbY.TabIndex = 179;
            this.lbY.Text = "Y (UTM)";
            // 
            // lbX
            // 
            this.lbX.AutoSize = true;
            this.lbX.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbX.Location = new System.Drawing.Point(122, 45);
            this.lbX.Name = "lbX";
            this.lbX.Size = new System.Drawing.Size(63, 16);
            this.lbX.TabIndex = 178;
            this.lbX.Text = "X (UTM)";
            // 
            // btnSalvar
            // 
            this.btnSalvar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvar.Image = global::AERMOD.Properties.Resources.disk;
            this.btnSalvar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalvar.Location = new System.Drawing.Point(432, 47);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(78, 40);
            this.btnSalvar.TabIndex = 4;
            this.btnSalvar.Text = "    <Enter>     Sal&var";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // lbDescricao
            // 
            this.lbDescricao.AutoSize = true;
            this.lbDescricao.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDescricao.Location = new System.Drawing.Point(1, 2);
            this.lbDescricao.Name = "lbDescricao";
            this.lbDescricao.Size = new System.Drawing.Size(79, 16);
            this.lbDescricao.TabIndex = 271;
            this.lbDescricao.Text = "Descrição";
            // 
            // tbxDescricao
            // 
            this.tbxDescricao.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxDescricao.Location = new System.Drawing.Point(4, 21);
            this.tbxDescricao.MaxLength = 100;
            this.tbxDescricao.Name = "tbxDescricao";
            this.tbxDescricao.Size = new System.Drawing.Size(506, 22);
            this.tbxDescricao.TabIndex = 0;
            // 
            // FrmFonteAERMAP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(514, 370);
            this.Controls.Add(this.lbDescricao);
            this.Controls.Add(this.tbxDescricao);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.lbTipoFonte);
            this.Controls.Add(this.tbxY);
            this.Controls.Add(this.cbxTipoFonte);
            this.Controls.Add(this.tbxX);
            this.Controls.Add(this.lbY);
            this.Controls.Add(this.lbX);
            this.Controls.Add(this.dgvFonte);
            this.Controls.Add(this.statusStripConsulta);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmFonteAERMAP";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fontes emissoras";
            this.Load += new System.EventHandler(this.FrmFonteAERMAP_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmFonteAERMAP_KeyDown);
            this.statusStripConsulta.ResumeLayout(false);
            this.statusStripConsulta.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFonte)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbTipoFonte;
        private System.Windows.Forms.ComboBox cbxTipoFonte;
        private System.Windows.Forms.StatusStrip statusStripConsulta;
        private System.Windows.Forms.ToolStripSplitButton btnAjuda;
        private System.Windows.Forms.ToolStripSplitButton btnInserir;
        private System.Windows.Forms.ToolStripSplitButton btnAlterar;
        private System.Windows.Forms.ToolStripSplitButton btnExcluir;
        private System.Windows.Forms.ToolStripSplitButton btnSair;
        private System.Windows.Forms.DataGridView dgvFonte;
        private LIB.Componentes.TextBoxMaskLIB tbxY;
        private LIB.Componentes.TextBoxMaskLIB tbxX;
        private System.Windows.Forms.Label lbY;
        private System.Windows.Forms.Label lbX;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Label lbDescricao;
        private System.Windows.Forms.TextBox tbxDescricao;
        private LIB.Componentes.GridView.DataGridViewCheckBoxLIBColumn STATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DESCRICAO;
        private System.Windows.Forms.DataGridViewTextBoxColumn TIPO;
        private System.Windows.Forms.DataGridViewTextBoxColumn X;
        private System.Windows.Forms.DataGridViewTextBoxColumn Y;
        private System.Windows.Forms.DataGridViewTextBoxColumn CODIGO;
        private System.Windows.Forms.DataGridViewTextBoxColumn EM_USO;
    }
}