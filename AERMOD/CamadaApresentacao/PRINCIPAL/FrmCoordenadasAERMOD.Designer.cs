namespace AERMOD.CamadaApresentacao.PRINCIPAL
{
    partial class FrmCoordenadasAERMOD
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
            this.tbxY = new AERMOD.LIB.Componentes.TextBoxMaskLIB();
            this.tbxX = new AERMOD.LIB.Componentes.TextBoxMaskLIB();
            this.lbY = new System.Windows.Forms.Label();
            this.lbX = new System.Windows.Forms.Label();
            this.dgvFonte = new System.Windows.Forms.DataGridView();
            this.STATUS = new AERMOD.LIB.Componentes.GridView.DataGridViewCheckBoxLIBColumn();
            this.SEQUENCIA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.X = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Y = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStripConsulta = new System.Windows.Forms.StatusStrip();
            this.btnAjuda = new System.Windows.Forms.ToolStripSplitButton();
            this.btnSalvar = new System.Windows.Forms.ToolStripSplitButton();
            this.btnSair = new System.Windows.Forms.ToolStripSplitButton();
            this.btnExcluirCoordenada = new AERMOD.LIB.Componentes.Botao.ButtonLIB();
            this.btnAlterarCoordenada = new AERMOD.LIB.Componentes.Botao.ButtonLIB();
            this.btnInserirCoordenada = new AERMOD.LIB.Componentes.Botao.ButtonLIB();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFonte)).BeginInit();
            this.statusStripConsulta.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbxY
            // 
            this.tbxY.CampoHabilitado = true;
            this.tbxY.ColumnName = null;
            this.tbxY.Decimais = 3;
            this.tbxY.FocusColor = System.Drawing.Color.Empty;
            this.tbxY.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxY.Inteiro = 10;
            this.tbxY.Location = new System.Drawing.Point(166, 23);
            this.tbxY.Mascara = AERMOD.LIB.Componentes.TextBoxMaskLIB.Mask.Decimal;
            this.tbxY.Name = "tbxY";
            this.tbxY.Size = new System.Drawing.Size(159, 22);
            this.tbxY.TabIndex = 181;
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
            this.tbxX.Location = new System.Drawing.Point(3, 23);
            this.tbxX.Mascara = AERMOD.LIB.Componentes.TextBoxMaskLIB.Mask.Decimal;
            this.tbxX.Name = "tbxX";
            this.tbxX.Size = new System.Drawing.Size(159, 22);
            this.tbxX.TabIndex = 180;
            this.tbxX.Text = "0,000";
            this.tbxX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbY
            // 
            this.lbY.AutoSize = true;
            this.lbY.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbY.Location = new System.Drawing.Point(163, 4);
            this.lbY.Name = "lbY";
            this.lbY.Size = new System.Drawing.Size(15, 16);
            this.lbY.TabIndex = 184;
            this.lbY.Text = "Y";
            // 
            // lbX
            // 
            this.lbX.AutoSize = true;
            this.lbX.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbX.Location = new System.Drawing.Point(0, 4);
            this.lbX.Name = "lbX";
            this.lbX.Size = new System.Drawing.Size(15, 16);
            this.lbX.TabIndex = 183;
            this.lbX.Text = "X";
            // 
            // dgvFonte
            // 
            this.dgvFonte.AllowUserToAddRows = false;
            this.dgvFonte.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgvFonte.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvFonte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFonte.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STATUS,
            this.SEQUENCIA,
            this.X,
            this.Y});
            this.dgvFonte.Location = new System.Drawing.Point(3, 48);
            this.dgvFonte.MultiSelect = false;
            this.dgvFonte.Name = "dgvFonte";
            this.dgvFonte.ReadOnly = true;
            this.dgvFonte.RowHeadersVisible = false;
            this.dgvFonte.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvFonte.Size = new System.Drawing.Size(322, 273);
            this.dgvFonte.TabIndex = 182;
            this.dgvFonte.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFonte_CellDoubleClick);
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
            // SEQUENCIA
            // 
            this.SEQUENCIA.DataPropertyName = "SEQUENCIA";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SEQUENCIA.DefaultCellStyle = dataGridViewCellStyle2;
            this.SEQUENCIA.HeaderText = "ID";
            this.SEQUENCIA.Name = "SEQUENCIA";
            this.SEQUENCIA.ReadOnly = true;
            this.SEQUENCIA.Width = 36;
            // 
            // X
            // 
            this.X.DataPropertyName = "X";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.X.DefaultCellStyle = dataGridViewCellStyle3;
            this.X.HeaderText = "X";
            this.X.Name = "X";
            this.X.ReadOnly = true;
            this.X.Width = 119;
            // 
            // Y
            // 
            this.Y.DataPropertyName = "Y";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.Y.DefaultCellStyle = dataGridViewCellStyle4;
            this.Y.HeaderText = "Y";
            this.Y.Name = "Y";
            this.Y.ReadOnly = true;
            this.Y.Width = 119;
            // 
            // statusStripConsulta
            // 
            this.statusStripConsulta.BackColor = System.Drawing.Color.LightBlue;
            this.statusStripConsulta.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAjuda,
            this.btnSalvar,
            this.btnSair});
            this.statusStripConsulta.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStripConsulta.Location = new System.Drawing.Point(0, 354);
            this.statusStripConsulta.Name = "statusStripConsulta";
            this.statusStripConsulta.Size = new System.Drawing.Size(328, 22);
            this.statusStripConsulta.SizingGrip = false;
            this.statusStripConsulta.TabIndex = 185;
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
            this.btnAjuda.Size = new System.Drawing.Size(110, 20);
            this.btnAjuda.Text = "<F1> &Ajuda";
            this.btnAjuda.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAjuda.ButtonClick += new System.EventHandler(this.btnAjuda_ButtonClick);
            // 
            // btnSalvar
            // 
            this.btnSalvar.AutoSize = false;
            this.btnSalvar.DropDownButtonWidth = 0;
            this.btnSalvar.Image = global::AERMOD.Properties.Resources.disk;
            this.btnSalvar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(113, 20);
            this.btnSalvar.Text = "<Enter> Sa&lvar";
            this.btnSalvar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalvar.ButtonClick += new System.EventHandler(this.btnSalvar_ButtonClick);
            // 
            // btnSair
            // 
            this.btnSair.AutoSize = false;
            this.btnSair.DropDownButtonWidth = 0;
            this.btnSair.Image = global::AERMOD.Properties.Resources.Sair;
            this.btnSair.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(90, 20);
            this.btnSair.Text = "<Esc> &Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.ButtonClick += new System.EventHandler(this.btnSair_ButtonClick);
            // 
            // btnExcluirCoordenada
            // 
            this.btnExcluirCoordenada.BackColor = System.Drawing.Color.Transparent;
            this.btnExcluirCoordenada.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcluirCoordenada.Image = global::AERMOD.Properties.Resources.delete;
            this.btnExcluirCoordenada.ImagePosition = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcluirCoordenada.Location = new System.Drawing.Point(215, 327);
            this.btnExcluirCoordenada.Name = "btnExcluirCoordenada";
            this.btnExcluirCoordenada.OpacityColor2 = 110;
            this.btnExcluirCoordenada.Raio = 1F;
            this.btnExcluirCoordenada.Size = new System.Drawing.Size(110, 23);
            this.btnExcluirCoordenada.TabIndex = 188;
            this.btnExcluirCoordenada.Text = "      <Delete> Excluir";
            this.btnExcluirCoordenada.Click += new System.EventHandler(this.btnExcluirCoordenada_Click);
            // 
            // btnAlterarCoordenada
            // 
            this.btnAlterarCoordenada.BackColor = System.Drawing.Color.Transparent;
            this.btnAlterarCoordenada.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlterarCoordenada.Image = global::AERMOD.Properties.Resources.Editar;
            this.btnAlterarCoordenada.ImagePosition = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAlterarCoordenada.Location = new System.Drawing.Point(109, 327);
            this.btnAlterarCoordenada.Name = "btnAlterarCoordenada";
            this.btnAlterarCoordenada.OpacityColor2 = 110;
            this.btnAlterarCoordenada.Raio = 1F;
            this.btnAlterarCoordenada.Size = new System.Drawing.Size(105, 23);
            this.btnAlterarCoordenada.TabIndex = 187;
            this.btnAlterarCoordenada.Text = "     <Alt A> Alterar";
            this.btnAlterarCoordenada.Click += new System.EventHandler(this.btnAlterarCoordenada_Click);
            // 
            // btnInserirCoordenada
            // 
            this.btnInserirCoordenada.BackColor = System.Drawing.Color.Transparent;
            this.btnInserirCoordenada.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInserirCoordenada.Image = global::AERMOD.Properties.Resources.add;
            this.btnInserirCoordenada.ImagePosition = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInserirCoordenada.Location = new System.Drawing.Point(3, 327);
            this.btnInserirCoordenada.Name = "btnInserirCoordenada";
            this.btnInserirCoordenada.OpacityColor2 = 110;
            this.btnInserirCoordenada.Raio = 1F;
            this.btnInserirCoordenada.Size = new System.Drawing.Size(105, 23);
            this.btnInserirCoordenada.TabIndex = 186;
            this.btnInserirCoordenada.Text = "      <Insert> Inserir";
            this.btnInserirCoordenada.Click += new System.EventHandler(this.btnInserirCoordenada_Click);
            // 
            // FrmCoordenadasAERMOD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(328, 376);
            this.Controls.Add(this.btnExcluirCoordenada);
            this.Controls.Add(this.btnAlterarCoordenada);
            this.Controls.Add(this.btnInserirCoordenada);
            this.Controls.Add(this.statusStripConsulta);
            this.Controls.Add(this.tbxY);
            this.Controls.Add(this.tbxX);
            this.Controls.Add(this.lbY);
            this.Controls.Add(this.lbX);
            this.Controls.Add(this.dgvFonte);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCoordenadasAERMOD";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Coordenadas da área polígono";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmCoordenadasAERMOD_FormClosing);
            this.Load += new System.EventHandler(this.FrmCoordenadasAERMOD_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmCoordenadasAERMOD_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFonte)).EndInit();
            this.statusStripConsulta.ResumeLayout(false);
            this.statusStripConsulta.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LIB.Componentes.TextBoxMaskLIB tbxY;
        private LIB.Componentes.TextBoxMaskLIB tbxX;
        private System.Windows.Forms.Label lbY;
        private System.Windows.Forms.Label lbX;
        private System.Windows.Forms.DataGridView dgvFonte;
        private System.Windows.Forms.StatusStrip statusStripConsulta;
        private System.Windows.Forms.ToolStripSplitButton btnAjuda;
        private System.Windows.Forms.ToolStripSplitButton btnSalvar;
        private System.Windows.Forms.ToolStripSplitButton btnSair;
        private LIB.Componentes.Botao.ButtonLIB btnExcluirCoordenada;
        private LIB.Componentes.Botao.ButtonLIB btnAlterarCoordenada;
        private LIB.Componentes.Botao.ButtonLIB btnInserirCoordenada;
        private LIB.Componentes.GridView.DataGridViewCheckBoxLIBColumn STATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn SEQUENCIA;
        private System.Windows.Forms.DataGridViewTextBoxColumn X;
        private System.Windows.Forms.DataGridViewTextBoxColumn Y;
    }
}