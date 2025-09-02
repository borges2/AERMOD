namespace AERMOD.CamadaApresentacao.AERMET
{
    partial class FrmDefinicaoAERMET
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControlDados = new System.Windows.Forms.TabControl();
            this.tabPageCadastro = new System.Windows.Forms.TabPage();
            this.tabControlCadastro = new System.Windows.Forms.TabControl();
            this.tabPageDadosBasicos = new System.Windows.Forms.TabPage();
            this.tbxY = new AERMOD.LIB.Componentes.TextBoxMaskLIB();
            this.tbxX = new AERMOD.LIB.Componentes.TextBoxMaskLIB();
            this.lbY = new System.Windows.Forms.Label();
            this.lbX = new System.Windows.Forms.Label();
            this.lbUF = new System.Windows.Forms.Label();
            this.cbxUF = new System.Windows.Forms.ComboBox();
            this.tbxPeriodoFinal = new AERMOD.LIB.Componentes.MaskedTextBoxLIB();
            this.tbxPeriodoInicial = new AERMOD.LIB.Componentes.MaskedTextBoxLIB();
            this.lbPeriodo = new System.Windows.Forms.Label();
            this.lbLocal = new System.Windows.Forms.Label();
            this.tbxLocal = new System.Windows.Forms.TextBox();
            this.tabPageCaracteristicas = new System.Windows.Forms.TabPage();
            this.btnSalvarCaracteristica = new AERMOD.LIB.Componentes.Botao.ButtonLIB();
            this.btnExcluirCaracteristica = new AERMOD.LIB.Componentes.Botao.ButtonLIB();
            this.tbxSetorFinal = new AERMOD.LIB.Componentes.TextBoxMaskLIB();
            this.btnAlterarCaracteristica = new AERMOD.LIB.Componentes.Botao.ButtonLIB();
            this.tbxSetorInicial = new AERMOD.LIB.Componentes.TextBoxMaskLIB();
            this.btnInserirCaracteristica = new AERMOD.LIB.Componentes.Botao.ButtonLIB();
            this.lbSetor = new System.Windows.Forms.Label();
            this.cbxEstacao = new System.Windows.Forms.ComboBox();
            this.dgvDefinicao = new System.Windows.Forms.DataGridView();
            this.dataGridViewCheckBoxLIBColumn1 = new AERMOD.LIB.Componentes.GridView.DataGridViewCheckBoxLIBColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FREQUENCIA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SETOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ESTACAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ALBEDO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BOWEN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RUGOSIDADE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cbxFrequencia = new System.Windows.Forms.ComboBox();
            this.tbxRugosidade = new AERMOD.LIB.Componentes.TextBoxMaskLIB();
            this.lbRugosidade = new System.Windows.Forms.Label();
            this.lbFrequencia = new System.Windows.Forms.Label();
            this.tbxBowen = new AERMOD.LIB.Componentes.TextBoxMaskLIB();
            this.lbEstacao = new System.Windows.Forms.Label();
            this.lbBowen = new System.Windows.Forms.Label();
            this.lbAlbedo = new System.Windows.Forms.Label();
            this.tbxAlbedo = new AERMOD.LIB.Componentes.TextBoxMaskLIB();
            this.statusStripCadastro = new System.Windows.Forms.StatusStrip();
            this.btnAjudaCadastro = new System.Windows.Forms.ToolStripSplitButton();
            this.btnConsultaCadastro = new System.Windows.Forms.ToolStripSplitButton();
            this.btnSalvarCadastro = new System.Windows.Forms.ToolStripSplitButton();
            this.btnSairCadastro = new System.Windows.Forms.ToolStripSplitButton();
            this.tabPageConsulta = new System.Windows.Forms.TabPage();
            this.dgvDados = new System.Windows.Forms.DataGridView();
            this.STATUS = new AERMOD.LIB.Componentes.GridView.DataGridViewCheckBoxLIBColumn();
            this.CODIGO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOCAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ESTADO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PERIODO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStripConsulta = new System.Windows.Forms.StatusStrip();
            this.btnAjudaConsulta = new System.Windows.Forms.ToolStripSplitButton();
            this.btnInserirConsulta = new System.Windows.Forms.ToolStripSplitButton();
            this.btnAlterarConsulta = new System.Windows.Forms.ToolStripSplitButton();
            this.btnExcluirConsulta = new System.Windows.Forms.ToolStripSplitButton();
            this.btnSairConsulta = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControlDados.SuspendLayout();
            this.tabPageCadastro.SuspendLayout();
            this.tabControlCadastro.SuspendLayout();
            this.tabPageDadosBasicos.SuspendLayout();
            this.tabPageCaracteristicas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDefinicao)).BeginInit();
            this.statusStripCadastro.SuspendLayout();
            this.tabPageConsulta.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDados)).BeginInit();
            this.statusStripConsulta.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControlDados
            // 
            this.tabControlDados.Controls.Add(this.tabPageCadastro);
            this.tabControlDados.Controls.Add(this.tabPageConsulta);
            this.tabControlDados.Location = new System.Drawing.Point(1, 2);
            this.tabControlDados.Name = "tabControlDados";
            this.tabControlDados.SelectedIndex = 0;
            this.tabControlDados.Size = new System.Drawing.Size(585, 411);
            this.tabControlDados.TabIndex = 1;
            this.tabControlDados.TabStop = false;
            this.tabControlDados.SelectedIndexChanged += new System.EventHandler(this.tabControlDados_SelectedIndexChanged);
            this.tabControlDados.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabControlDados_Selecting);
            // 
            // tabPageCadastro
            // 
            this.tabPageCadastro.Controls.Add(this.tabControlCadastro);
            this.tabPageCadastro.Controls.Add(this.statusStripCadastro);
            this.tabPageCadastro.Location = new System.Drawing.Point(4, 22);
            this.tabPageCadastro.Name = "tabPageCadastro";
            this.tabPageCadastro.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCadastro.Size = new System.Drawing.Size(577, 385);
            this.tabPageCadastro.TabIndex = 0;
            this.tabPageCadastro.Text = "Cadastro";
            this.tabPageCadastro.UseVisualStyleBackColor = true;
            // 
            // tabControlCadastro
            // 
            this.tabControlCadastro.Controls.Add(this.tabPageDadosBasicos);
            this.tabControlCadastro.Controls.Add(this.tabPageCaracteristicas);
            this.tabControlCadastro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlCadastro.Location = new System.Drawing.Point(3, 3);
            this.tabControlCadastro.Name = "tabControlCadastro";
            this.tabControlCadastro.SelectedIndex = 0;
            this.tabControlCadastro.Size = new System.Drawing.Size(571, 357);
            this.tabControlCadastro.TabIndex = 2;
            this.tabControlCadastro.TabStop = false;
            this.tabControlCadastro.SelectedIndexChanged += new System.EventHandler(this.tabControlCadastro_SelectedIndexChanged);
            // 
            // tabPageDadosBasicos
            // 
            this.tabPageDadosBasicos.Controls.Add(this.tbxY);
            this.tabPageDadosBasicos.Controls.Add(this.tbxX);
            this.tabPageDadosBasicos.Controls.Add(this.lbY);
            this.tabPageDadosBasicos.Controls.Add(this.lbX);
            this.tabPageDadosBasicos.Controls.Add(this.lbUF);
            this.tabPageDadosBasicos.Controls.Add(this.cbxUF);
            this.tabPageDadosBasicos.Controls.Add(this.tbxPeriodoFinal);
            this.tabPageDadosBasicos.Controls.Add(this.tbxPeriodoInicial);
            this.tabPageDadosBasicos.Controls.Add(this.lbPeriodo);
            this.tabPageDadosBasicos.Controls.Add(this.lbLocal);
            this.tabPageDadosBasicos.Controls.Add(this.tbxLocal);
            this.tabPageDadosBasicos.Location = new System.Drawing.Point(4, 22);
            this.tabPageDadosBasicos.Name = "tabPageDadosBasicos";
            this.tabPageDadosBasicos.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDadosBasicos.Size = new System.Drawing.Size(563, 331);
            this.tabPageDadosBasicos.TabIndex = 0;
            this.tabPageDadosBasicos.Text = "Dados Básicos";
            this.tabPageDadosBasicos.UseVisualStyleBackColor = true;
            // 
            // tbxY
            // 
            this.tbxY.CampoHabilitado = true;
            this.tbxY.ColumnName = null;
            this.tbxY.Decimais = 3;
            this.tbxY.FocusColor = System.Drawing.Color.Empty;
            this.tbxY.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxY.Inteiro = 10;
            this.tbxY.Location = new System.Drawing.Point(160, 177);
            this.tbxY.Mascara = AERMOD.LIB.Componentes.TextBoxMaskLIB.Mask.Decimal;
            this.tbxY.Name = "tbxY";
            this.tbxY.Size = new System.Drawing.Size(148, 22);
            this.tbxY.TabIndex = 5;
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
            this.tbxX.Location = new System.Drawing.Point(6, 177);
            this.tbxX.Mascara = AERMOD.LIB.Componentes.TextBoxMaskLIB.Mask.Decimal;
            this.tbxX.Name = "tbxX";
            this.tbxX.Size = new System.Drawing.Size(148, 22);
            this.tbxX.TabIndex = 4;
            this.tbxX.Text = "0,000";
            this.tbxX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbY
            // 
            this.lbY.AutoSize = true;
            this.lbY.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbY.Location = new System.Drawing.Point(157, 158);
            this.lbY.Name = "lbY";
            this.lbY.Size = new System.Drawing.Size(63, 16);
            this.lbY.TabIndex = 276;
            this.lbY.Text = "Y (UTM)";
            // 
            // lbX
            // 
            this.lbX.AutoSize = true;
            this.lbX.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbX.Location = new System.Drawing.Point(3, 158);
            this.lbX.Name = "lbX";
            this.lbX.Size = new System.Drawing.Size(63, 16);
            this.lbX.TabIndex = 275;
            this.lbX.Text = "X (UTM)";
            // 
            // lbUF
            // 
            this.lbUF.AutoSize = true;
            this.lbUF.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUF.Location = new System.Drawing.Point(3, 57);
            this.lbUF.Name = "lbUF";
            this.lbUF.Size = new System.Drawing.Size(55, 16);
            this.lbUF.TabIndex = 272;
            this.lbUF.Text = "Estado";
            // 
            // cbxUF
            // 
            this.cbxUF.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxUF.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxUF.FormattingEnabled = true;
            this.cbxUF.Location = new System.Drawing.Point(6, 76);
            this.cbxUF.Name = "cbxUF";
            this.cbxUF.Size = new System.Drawing.Size(58, 24);
            this.cbxUF.TabIndex = 1;
            // 
            // tbxPeriodoFinal
            // 
            this.tbxPeriodoFinal.CodigoCampo = null;
            this.tbxPeriodoFinal.FocusColor = System.Drawing.Color.Empty;
            this.tbxPeriodoFinal.FocusColorEnabled = false;
            this.tbxPeriodoFinal.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxPeriodoFinal.Location = new System.Drawing.Point(128, 126);
            this.tbxPeriodoFinal.Mascara = AERMOD.LIB.Componentes.MaskedTextBoxLIB.MascaraEnum.DATA;
            this.tbxPeriodoFinal.Mask = "00/00/0000";
            this.tbxPeriodoFinal.Menos = false;
            this.tbxPeriodoFinal.Name = "tbxPeriodoFinal";
            this.tbxPeriodoFinal.PromptChar = ' ';
            this.tbxPeriodoFinal.Simbolo = AERMOD.LIB.Componentes.MaskedTextBoxLIB.SimboloEnum.NORMAL;
            this.tbxPeriodoFinal.Size = new System.Drawing.Size(110, 22);
            this.tbxPeriodoFinal.TabIndex = 3;
            // 
            // tbxPeriodoInicial
            // 
            this.tbxPeriodoInicial.CodigoCampo = null;
            this.tbxPeriodoInicial.FocusColor = System.Drawing.Color.Empty;
            this.tbxPeriodoInicial.FocusColorEnabled = false;
            this.tbxPeriodoInicial.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxPeriodoInicial.Location = new System.Drawing.Point(6, 126);
            this.tbxPeriodoInicial.Mascara = AERMOD.LIB.Componentes.MaskedTextBoxLIB.MascaraEnum.DATA;
            this.tbxPeriodoInicial.Mask = "00/00/0000";
            this.tbxPeriodoInicial.Menos = false;
            this.tbxPeriodoInicial.Name = "tbxPeriodoInicial";
            this.tbxPeriodoInicial.PromptChar = ' ';
            this.tbxPeriodoInicial.Simbolo = AERMOD.LIB.Componentes.MaskedTextBoxLIB.SimboloEnum.NORMAL;
            this.tbxPeriodoInicial.Size = new System.Drawing.Size(110, 22);
            this.tbxPeriodoInicial.TabIndex = 2;
            // 
            // lbPeriodo
            // 
            this.lbPeriodo.AutoSize = true;
            this.lbPeriodo.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPeriodo.Location = new System.Drawing.Point(3, 107);
            this.lbPeriodo.Name = "lbPeriodo";
            this.lbPeriodo.Size = new System.Drawing.Size(63, 16);
            this.lbPeriodo.TabIndex = 270;
            this.lbPeriodo.Text = "Período";
            // 
            // lbLocal
            // 
            this.lbLocal.AutoSize = true;
            this.lbLocal.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLocal.Location = new System.Drawing.Point(3, 8);
            this.lbLocal.Name = "lbLocal";
            this.lbLocal.Size = new System.Drawing.Size(47, 16);
            this.lbLocal.TabIndex = 269;
            this.lbLocal.Text = "Local";
            // 
            // tbxLocal
            // 
            this.tbxLocal.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxLocal.Location = new System.Drawing.Point(6, 27);
            this.tbxLocal.MaxLength = 37;
            this.tbxLocal.Name = "tbxLocal";
            this.tbxLocal.Size = new System.Drawing.Size(551, 22);
            this.tbxLocal.TabIndex = 0;
            // 
            // tabPageCaracteristicas
            // 
            this.tabPageCaracteristicas.Controls.Add(this.btnSalvarCaracteristica);
            this.tabPageCaracteristicas.Controls.Add(this.btnExcluirCaracteristica);
            this.tabPageCaracteristicas.Controls.Add(this.tbxSetorFinal);
            this.tabPageCaracteristicas.Controls.Add(this.btnAlterarCaracteristica);
            this.tabPageCaracteristicas.Controls.Add(this.tbxSetorInicial);
            this.tabPageCaracteristicas.Controls.Add(this.btnInserirCaracteristica);
            this.tabPageCaracteristicas.Controls.Add(this.lbSetor);
            this.tabPageCaracteristicas.Controls.Add(this.cbxEstacao);
            this.tabPageCaracteristicas.Controls.Add(this.dgvDefinicao);
            this.tabPageCaracteristicas.Controls.Add(this.cbxFrequencia);
            this.tabPageCaracteristicas.Controls.Add(this.tbxRugosidade);
            this.tabPageCaracteristicas.Controls.Add(this.lbRugosidade);
            this.tabPageCaracteristicas.Controls.Add(this.lbFrequencia);
            this.tabPageCaracteristicas.Controls.Add(this.tbxBowen);
            this.tabPageCaracteristicas.Controls.Add(this.lbEstacao);
            this.tabPageCaracteristicas.Controls.Add(this.lbBowen);
            this.tabPageCaracteristicas.Controls.Add(this.lbAlbedo);
            this.tabPageCaracteristicas.Controls.Add(this.tbxAlbedo);
            this.tabPageCaracteristicas.Location = new System.Drawing.Point(4, 22);
            this.tabPageCaracteristicas.Name = "tabPageCaracteristicas";
            this.tabPageCaracteristicas.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCaracteristicas.Size = new System.Drawing.Size(563, 331);
            this.tabPageCaracteristicas.TabIndex = 1;
            this.tabPageCaracteristicas.Text = "Características";
            this.tabPageCaracteristicas.UseVisualStyleBackColor = true;
            this.tabPageCaracteristicas.Visible = false;
            // 
            // btnSalvarCaracteristica
            // 
            this.btnSalvarCaracteristica.BackColor = System.Drawing.Color.Transparent;
            this.btnSalvarCaracteristica.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvarCaracteristica.Image = global::AERMOD.Properties.Resources.disk;
            this.btnSalvarCaracteristica.ImagePosition = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSalvarCaracteristica.Location = new System.Drawing.Point(403, 132);
            this.btnSalvarCaracteristica.Name = "btnSalvarCaracteristica";
            this.btnSalvarCaracteristica.OpacityColor2 = 110;
            this.btnSalvarCaracteristica.Raio = 1F;
            this.btnSalvarCaracteristica.Size = new System.Drawing.Size(114, 23);
            this.btnSalvarCaracteristica.TabIndex = 10;
            this.btnSalvarCaracteristica.Text = "    <Enter> Salvar";
            this.btnSalvarCaracteristica.Click += new System.EventHandler(this.btnSalvarCaracteristica_Click);
            // 
            // btnExcluirCaracteristica
            // 
            this.btnExcluirCaracteristica.BackColor = System.Drawing.Color.Transparent;
            this.btnExcluirCaracteristica.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcluirCaracteristica.Image = global::AERMOD.Properties.Resources.delete;
            this.btnExcluirCaracteristica.ImagePosition = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExcluirCaracteristica.Location = new System.Drawing.Point(278, 132);
            this.btnExcluirCaracteristica.Name = "btnExcluirCaracteristica";
            this.btnExcluirCaracteristica.OpacityColor2 = 110;
            this.btnExcluirCaracteristica.Raio = 1F;
            this.btnExcluirCaracteristica.Size = new System.Drawing.Size(124, 23);
            this.btnExcluirCaracteristica.TabIndex = 9;
            this.btnExcluirCaracteristica.Text = "    <Delete> Excluir";
            this.btnExcluirCaracteristica.Click += new System.EventHandler(this.btnExcluirCaracteristica_Click);
            // 
            // tbxSetorFinal
            // 
            this.tbxSetorFinal.CampoHabilitado = true;
            this.tbxSetorFinal.ColumnName = null;
            this.tbxSetorFinal.Decimais = 3;
            this.tbxSetorFinal.FocusColor = System.Drawing.Color.Empty;
            this.tbxSetorFinal.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxSetorFinal.InteiroDefault = AERMOD.LIB.Componentes.TextBoxMaskLIB.ValorInteiroDefault.Zero;
            this.tbxSetorFinal.Location = new System.Drawing.Point(289, 31);
            this.tbxSetorFinal.Mascara = AERMOD.LIB.Componentes.TextBoxMaskLIB.Mask.Inteiro;
            this.tbxSetorFinal.MaxLength = 3;
            this.tbxSetorFinal.Name = "tbxSetorFinal";
            this.tbxSetorFinal.PermiteNegativo = false;
            this.tbxSetorFinal.Size = new System.Drawing.Size(39, 22);
            this.tbxSetorFinal.TabIndex = 2;
            this.tbxSetorFinal.Text = "360";
            this.tbxSetorFinal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnAlterarCaracteristica
            // 
            this.btnAlterarCaracteristica.BackColor = System.Drawing.Color.Transparent;
            this.btnAlterarCaracteristica.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlterarCaracteristica.Image = global::AERMOD.Properties.Resources.Editar;
            this.btnAlterarCaracteristica.ImagePosition = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAlterarCaracteristica.Location = new System.Drawing.Point(163, 132);
            this.btnAlterarCaracteristica.Name = "btnAlterarCaracteristica";
            this.btnAlterarCaracteristica.OpacityColor2 = 110;
            this.btnAlterarCaracteristica.Raio = 1F;
            this.btnAlterarCaracteristica.Size = new System.Drawing.Size(114, 23);
            this.btnAlterarCaracteristica.TabIndex = 8;
            this.btnAlterarCaracteristica.Text = "    <Alt A> Alterar";
            this.btnAlterarCaracteristica.Click += new System.EventHandler(this.btnAlterarCaracteristica_Click);
            // 
            // tbxSetorInicial
            // 
            this.tbxSetorInicial.CampoHabilitado = true;
            this.tbxSetorInicial.ColumnName = null;
            this.tbxSetorInicial.Decimais = 3;
            this.tbxSetorInicial.FocusColor = System.Drawing.Color.Empty;
            this.tbxSetorInicial.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxSetorInicial.InteiroDefault = AERMOD.LIB.Componentes.TextBoxMaskLIB.ValorInteiroDefault.Zero;
            this.tbxSetorInicial.Location = new System.Drawing.Point(228, 31);
            this.tbxSetorInicial.Mascara = AERMOD.LIB.Componentes.TextBoxMaskLIB.Mask.Inteiro;
            this.tbxSetorInicial.MaxLength = 3;
            this.tbxSetorInicial.Name = "tbxSetorInicial";
            this.tbxSetorInicial.PermiteNegativo = false;
            this.tbxSetorInicial.Size = new System.Drawing.Size(39, 22);
            this.tbxSetorInicial.TabIndex = 1;
            this.tbxSetorInicial.Text = "0";
            this.tbxSetorInicial.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnInserirCaracteristica
            // 
            this.btnInserirCaracteristica.BackColor = System.Drawing.Color.Transparent;
            this.btnInserirCaracteristica.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInserirCaracteristica.Image = global::AERMOD.Properties.Resources.add;
            this.btnInserirCaracteristica.ImagePosition = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInserirCaracteristica.Location = new System.Drawing.Point(48, 132);
            this.btnInserirCaracteristica.Name = "btnInserirCaracteristica";
            this.btnInserirCaracteristica.OpacityColor2 = 110;
            this.btnInserirCaracteristica.Raio = 1F;
            this.btnInserirCaracteristica.Size = new System.Drawing.Size(114, 23);
            this.btnInserirCaracteristica.TabIndex = 7;
            this.btnInserirCaracteristica.Text = "    <Insert> Inserir";
            this.btnInserirCaracteristica.Click += new System.EventHandler(this.btnInserirCaracteristica_Click);
            // 
            // lbSetor
            // 
            this.lbSetor.AutoSize = true;
            this.lbSetor.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSetor.Location = new System.Drawing.Point(225, 12);
            this.lbSetor.Name = "lbSetor";
            this.lbSetor.Size = new System.Drawing.Size(47, 16);
            this.lbSetor.TabIndex = 275;
            this.lbSetor.Text = "Setor";
            // 
            // cbxEstacao
            // 
            this.cbxEstacao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEstacao.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxEstacao.FormattingEnabled = true;
            this.cbxEstacao.Location = new System.Drawing.Point(377, 31);
            this.cbxEstacao.Name = "cbxEstacao";
            this.cbxEstacao.Size = new System.Drawing.Size(155, 24);
            this.cbxEstacao.TabIndex = 3;
            // 
            // dgvDefinicao
            // 
            this.dgvDefinicao.AllowUserToAddRows = false;
            this.dgvDefinicao.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgvDefinicao.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDefinicao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDefinicao.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewCheckBoxLIBColumn1,
            this.ID,
            this.FREQUENCIA,
            this.SETOR,
            this.ESTACAO,
            this.ALBEDO,
            this.BOWEN,
            this.RUGOSIDADE});
            this.dgvDefinicao.Location = new System.Drawing.Point(6, 161);
            this.dgvDefinicao.MultiSelect = false;
            this.dgvDefinicao.Name = "dgvDefinicao";
            this.dgvDefinicao.ReadOnly = true;
            this.dgvDefinicao.RowHeadersVisible = false;
            this.dgvDefinicao.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvDefinicao.Size = new System.Drawing.Size(551, 164);
            this.dgvDefinicao.TabIndex = 11;
            this.dgvDefinicao.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDefinicao_CellDoubleClick);
            this.dgvDefinicao.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDefinicao_CellFormatting);
            this.dgvDefinicao.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDefinicao_CellLeave);
            this.dgvDefinicao.SelectionChanged += new System.EventHandler(this.dgvDefinicao_SelectionChanged);
            this.dgvDefinicao.Enter += new System.EventHandler(this.dgvDefinicao_Enter);
            // 
            // dataGridViewCheckBoxLIBColumn1
            // 
            this.dataGridViewCheckBoxLIBColumn1.DataPropertyName = "STATUS";
            this.dataGridViewCheckBoxLIBColumn1.Frozen = true;
            this.dataGridViewCheckBoxLIBColumn1.HeaderText = "";
            this.dataGridViewCheckBoxLIBColumn1.Name = "dataGridViewCheckBoxLIBColumn1";
            this.dataGridViewCheckBoxLIBColumn1.ReadOnly = true;
            this.dataGridViewCheckBoxLIBColumn1.Width = 28;
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ID.DefaultCellStyle = dataGridViewCellStyle2;
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Width = 50;
            // 
            // FREQUENCIA
            // 
            this.FREQUENCIA.DataPropertyName = "FREQUENCIA";
            this.FREQUENCIA.HeaderText = "Frequência";
            this.FREQUENCIA.Name = "FREQUENCIA";
            this.FREQUENCIA.ReadOnly = true;
            this.FREQUENCIA.Width = 82;
            // 
            // SETOR
            // 
            this.SETOR.DataPropertyName = "SETOR";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.SETOR.DefaultCellStyle = dataGridViewCellStyle3;
            this.SETOR.HeaderText = "Setor";
            this.SETOR.Name = "SETOR";
            this.SETOR.ReadOnly = true;
            this.SETOR.Width = 85;
            // 
            // ESTACAO
            // 
            this.ESTACAO.DataPropertyName = "ESTACAO";
            this.ESTACAO.HeaderText = "Estação";
            this.ESTACAO.Name = "ESTACAO";
            this.ESTACAO.ReadOnly = true;
            this.ESTACAO.Width = 90;
            // 
            // ALBEDO
            // 
            this.ALBEDO.DataPropertyName = "ALBEDO";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.ALBEDO.DefaultCellStyle = dataGridViewCellStyle4;
            this.ALBEDO.HeaderText = "Albedo";
            this.ALBEDO.Name = "ALBEDO";
            this.ALBEDO.ReadOnly = true;
            this.ALBEDO.Width = 65;
            // 
            // BOWEN
            // 
            this.BOWEN.DataPropertyName = "BOWEN";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.BOWEN.DefaultCellStyle = dataGridViewCellStyle5;
            this.BOWEN.HeaderText = "Bowen";
            this.BOWEN.Name = "BOWEN";
            this.BOWEN.ReadOnly = true;
            this.BOWEN.Width = 65;
            // 
            // RUGOSIDADE
            // 
            this.RUGOSIDADE.DataPropertyName = "RUGOSIDADE";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.RUGOSIDADE.DefaultCellStyle = dataGridViewCellStyle6;
            this.RUGOSIDADE.HeaderText = "Rug.";
            this.RUGOSIDADE.Name = "RUGOSIDADE";
            this.RUGOSIDADE.ReadOnly = true;
            this.RUGOSIDADE.Width = 65;
            // 
            // cbxFrequencia
            // 
            this.cbxFrequencia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxFrequencia.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbxFrequencia.FormattingEnabled = true;
            this.cbxFrequencia.Location = new System.Drawing.Point(9, 31);
            this.cbxFrequencia.Name = "cbxFrequencia";
            this.cbxFrequencia.Size = new System.Drawing.Size(171, 24);
            this.cbxFrequencia.TabIndex = 0;
            // 
            // tbxRugosidade
            // 
            this.tbxRugosidade.CampoHabilitado = true;
            this.tbxRugosidade.ColumnName = null;
            this.tbxRugosidade.FocusColor = System.Drawing.Color.Empty;
            this.tbxRugosidade.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxRugosidade.Inteiro = 4;
            this.tbxRugosidade.Location = new System.Drawing.Point(278, 86);
            this.tbxRugosidade.Mascara = AERMOD.LIB.Componentes.TextBoxMaskLIB.Mask.Decimal;
            this.tbxRugosidade.Name = "tbxRugosidade";
            this.tbxRugosidade.Size = new System.Drawing.Size(110, 22);
            this.tbxRugosidade.TabIndex = 6;
            this.tbxRugosidade.Text = "0,00";
            this.tbxRugosidade.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbRugosidade
            // 
            this.lbRugosidade.AutoSize = true;
            this.lbRugosidade.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRugosidade.Location = new System.Drawing.Point(276, 67);
            this.lbRugosidade.Name = "lbRugosidade";
            this.lbRugosidade.Size = new System.Drawing.Size(87, 16);
            this.lbRugosidade.TabIndex = 284;
            this.lbRugosidade.Text = "Rugosidade";
            // 
            // lbFrequencia
            // 
            this.lbFrequencia.AutoSize = true;
            this.lbFrequencia.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFrequencia.Location = new System.Drawing.Point(6, 12);
            this.lbFrequencia.Name = "lbFrequencia";
            this.lbFrequencia.Size = new System.Drawing.Size(87, 16);
            this.lbFrequencia.TabIndex = 273;
            this.lbFrequencia.Text = "Frequência";
            // 
            // tbxBowen
            // 
            this.tbxBowen.CampoHabilitado = true;
            this.tbxBowen.ColumnName = null;
            this.tbxBowen.FocusColor = System.Drawing.Color.Empty;
            this.tbxBowen.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxBowen.Inteiro = 4;
            this.tbxBowen.Location = new System.Drawing.Point(144, 86);
            this.tbxBowen.Mascara = AERMOD.LIB.Componentes.TextBoxMaskLIB.Mask.Decimal;
            this.tbxBowen.Name = "tbxBowen";
            this.tbxBowen.Size = new System.Drawing.Size(110, 22);
            this.tbxBowen.TabIndex = 5;
            this.tbxBowen.Text = "0,00";
            this.tbxBowen.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbEstacao
            // 
            this.lbEstacao.AutoSize = true;
            this.lbEstacao.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEstacao.Location = new System.Drawing.Point(374, 12);
            this.lbEstacao.Name = "lbEstacao";
            this.lbEstacao.Size = new System.Drawing.Size(63, 16);
            this.lbEstacao.TabIndex = 278;
            this.lbEstacao.Text = "Estação";
            // 
            // lbBowen
            // 
            this.lbBowen.AutoSize = true;
            this.lbBowen.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBowen.Location = new System.Drawing.Point(141, 67);
            this.lbBowen.Name = "lbBowen";
            this.lbBowen.Size = new System.Drawing.Size(47, 16);
            this.lbBowen.TabIndex = 282;
            this.lbBowen.Text = "Bowen";
            // 
            // lbAlbedo
            // 
            this.lbAlbedo.AutoSize = true;
            this.lbAlbedo.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAlbedo.Location = new System.Drawing.Point(6, 67);
            this.lbAlbedo.Name = "lbAlbedo";
            this.lbAlbedo.Size = new System.Drawing.Size(55, 16);
            this.lbAlbedo.TabIndex = 280;
            this.lbAlbedo.Text = "Albedo";
            // 
            // tbxAlbedo
            // 
            this.tbxAlbedo.CampoHabilitado = true;
            this.tbxAlbedo.ColumnName = null;
            this.tbxAlbedo.FocusColor = System.Drawing.Color.Empty;
            this.tbxAlbedo.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxAlbedo.Inteiro = 4;
            this.tbxAlbedo.Location = new System.Drawing.Point(9, 86);
            this.tbxAlbedo.Mascara = AERMOD.LIB.Componentes.TextBoxMaskLIB.Mask.Decimal;
            this.tbxAlbedo.Name = "tbxAlbedo";
            this.tbxAlbedo.Size = new System.Drawing.Size(110, 22);
            this.tbxAlbedo.TabIndex = 4;
            this.tbxAlbedo.Text = "0,00";
            this.tbxAlbedo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // statusStripCadastro
            // 
            this.statusStripCadastro.BackColor = System.Drawing.Color.LightBlue;
            this.statusStripCadastro.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAjudaCadastro,
            this.btnConsultaCadastro,
            this.btnSalvarCadastro,
            this.btnSairCadastro});
            this.statusStripCadastro.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStripCadastro.Location = new System.Drawing.Point(3, 360);
            this.statusStripCadastro.Name = "statusStripCadastro";
            this.statusStripCadastro.Size = new System.Drawing.Size(571, 22);
            this.statusStripCadastro.SizingGrip = false;
            this.statusStripCadastro.TabIndex = 243;
            this.statusStripCadastro.Text = "statusStrip1";
            // 
            // btnAjudaCadastro
            // 
            this.btnAjudaCadastro.AutoSize = false;
            this.btnAjudaCadastro.DropDownButtonWidth = 0;
            this.btnAjudaCadastro.Image = global::AERMOD.Properties.Resources.ajuda;
            this.btnAjudaCadastro.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAjudaCadastro.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAjudaCadastro.Name = "btnAjudaCadastro";
            this.btnAjudaCadastro.Size = new System.Drawing.Size(140, 20);
            this.btnAjudaCadastro.Text = "<F1> &Ajuda";
            this.btnAjudaCadastro.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAjudaCadastro.ButtonClick += new System.EventHandler(this.btnAjudaCadastro_ButtonClick);
            // 
            // btnConsultaCadastro
            // 
            this.btnConsultaCadastro.AutoSize = false;
            this.btnConsultaCadastro.DropDownButtonWidth = 0;
            this.btnConsultaCadastro.Image = global::AERMOD.Properties.Resources.search_16x16;
            this.btnConsultaCadastro.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnConsultaCadastro.Name = "btnConsultaCadastro";
            this.btnConsultaCadastro.Size = new System.Drawing.Size(130, 20);
            this.btnConsultaCadastro.Text = "<F2> &Consultar";
            this.btnConsultaCadastro.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnConsultaCadastro.ButtonClick += new System.EventHandler(this.btnConsultaCadastro_ButtonClick);
            // 
            // btnSalvarCadastro
            // 
            this.btnSalvarCadastro.AutoSize = false;
            this.btnSalvarCadastro.DropDownButtonWidth = 0;
            this.btnSalvarCadastro.Image = global::AERMOD.Properties.Resources.disk;
            this.btnSalvarCadastro.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSalvarCadastro.Name = "btnSalvarCadastro";
            this.btnSalvarCadastro.Size = new System.Drawing.Size(170, 20);
            this.btnSalvarCadastro.Text = "<Enter> &Salvar";
            this.btnSalvarCadastro.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSalvarCadastro.ButtonClick += new System.EventHandler(this.btnSalvarCadastro_ButtonClick);
            // 
            // btnSairCadastro
            // 
            this.btnSairCadastro.AutoSize = false;
            this.btnSairCadastro.DropDownButtonWidth = 0;
            this.btnSairCadastro.Image = global::AERMOD.Properties.Resources.Sair;
            this.btnSairCadastro.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSairCadastro.Name = "btnSairCadastro";
            this.btnSairCadastro.Size = new System.Drawing.Size(110, 20);
            this.btnSairCadastro.Text = "<Esc> &Sair";
            this.btnSairCadastro.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSairCadastro.ButtonClick += new System.EventHandler(this.btnSairCadastro_ButtonClick);
            // 
            // tabPageConsulta
            // 
            this.tabPageConsulta.Controls.Add(this.dgvDados);
            this.tabPageConsulta.Controls.Add(this.statusStripConsulta);
            this.tabPageConsulta.Location = new System.Drawing.Point(4, 22);
            this.tabPageConsulta.Name = "tabPageConsulta";
            this.tabPageConsulta.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageConsulta.Size = new System.Drawing.Size(577, 385);
            this.tabPageConsulta.TabIndex = 1;
            this.tabPageConsulta.Text = "Consulta";
            this.tabPageConsulta.UseVisualStyleBackColor = true;
            // 
            // dgvDados
            // 
            this.dgvDados.AllowUserToAddRows = false;
            this.dgvDados.AllowUserToDeleteRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgvDados.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvDados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDados.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STATUS,
            this.CODIGO,
            this.LOCAL,
            this.ESTADO,
            this.PERIODO});
            this.dgvDados.Location = new System.Drawing.Point(6, 5);
            this.dgvDados.MultiSelect = false;
            this.dgvDados.Name = "dgvDados";
            this.dgvDados.ReadOnly = true;
            this.dgvDados.RowHeadersVisible = false;
            this.dgvDados.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvDados.Size = new System.Drawing.Size(565, 350);
            this.dgvDados.TabIndex = 0;
            this.dgvDados.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDados_CellDoubleClick);
            this.dgvDados.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDados_CellLeave);
            this.dgvDados.Enter += new System.EventHandler(this.dgvDados_Enter);
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
            // CODIGO
            // 
            this.CODIGO.DataPropertyName = "CODIGO";
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.CODIGO.DefaultCellStyle = dataGridViewCellStyle8;
            this.CODIGO.Frozen = true;
            this.CODIGO.HeaderText = "ID";
            this.CODIGO.Name = "CODIGO";
            this.CODIGO.ReadOnly = true;
            this.CODIGO.Width = 50;
            // 
            // LOCAL
            // 
            this.LOCAL.DataPropertyName = "LOCAL";
            this.LOCAL.HeaderText = "Local";
            this.LOCAL.Name = "LOCAL";
            this.LOCAL.ReadOnly = true;
            this.LOCAL.Width = 207;
            // 
            // ESTADO
            // 
            this.ESTADO.DataPropertyName = "ESTADO";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.ESTADO.DefaultCellStyle = dataGridViewCellStyle9;
            this.ESTADO.HeaderText = "Estado";
            this.ESTADO.Name = "ESTADO";
            this.ESTADO.ReadOnly = true;
            this.ESTADO.Width = 65;
            // 
            // PERIODO
            // 
            this.PERIODO.DataPropertyName = "PERIODO";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.PERIODO.DefaultCellStyle = dataGridViewCellStyle10;
            this.PERIODO.HeaderText = "Período";
            this.PERIODO.Name = "PERIODO";
            this.PERIODO.ReadOnly = true;
            this.PERIODO.Width = 195;
            // 
            // statusStripConsulta
            // 
            this.statusStripConsulta.BackColor = System.Drawing.Color.LightBlue;
            this.statusStripConsulta.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAjudaConsulta,
            this.btnInserirConsulta,
            this.btnAlterarConsulta,
            this.btnExcluirConsulta,
            this.btnSairConsulta,
            this.toolStripStatusLabel1});
            this.statusStripConsulta.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStripConsulta.Location = new System.Drawing.Point(3, 360);
            this.statusStripConsulta.Name = "statusStripConsulta";
            this.statusStripConsulta.Size = new System.Drawing.Size(571, 22);
            this.statusStripConsulta.SizingGrip = false;
            this.statusStripConsulta.TabIndex = 175;
            this.statusStripConsulta.Text = "statusStrip2";
            // 
            // btnAjudaConsulta
            // 
            this.btnAjudaConsulta.AutoSize = false;
            this.btnAjudaConsulta.DropDownButtonWidth = 0;
            this.btnAjudaConsulta.Image = global::AERMOD.Properties.Resources.ajuda;
            this.btnAjudaConsulta.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAjudaConsulta.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAjudaConsulta.Name = "btnAjudaConsulta";
            this.btnAjudaConsulta.Size = new System.Drawing.Size(105, 20);
            this.btnAjudaConsulta.Text = "<F1> &Ajuda";
            this.btnAjudaConsulta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAjudaConsulta.ButtonClick += new System.EventHandler(this.btnAjudaConsulta_ButtonClick);
            // 
            // btnInserirConsulta
            // 
            this.btnInserirConsulta.AutoSize = false;
            this.btnInserirConsulta.DropDownButtonWidth = 0;
            this.btnInserirConsulta.Image = global::AERMOD.Properties.Resources.add;
            this.btnInserirConsulta.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnInserirConsulta.Name = "btnInserirConsulta";
            this.btnInserirConsulta.Size = new System.Drawing.Size(120, 20);
            this.btnInserirConsulta.Text = "<Insert> &Inserir";
            this.btnInserirConsulta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnInserirConsulta.ButtonClick += new System.EventHandler(this.btnInserirConsulta_ButtonClick);
            // 
            // btnAlterarConsulta
            // 
            this.btnAlterarConsulta.AutoSize = false;
            this.btnAlterarConsulta.DropDownButtonWidth = 0;
            this.btnAlterarConsulta.Image = global::AERMOD.Properties.Resources.Editar;
            this.btnAlterarConsulta.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAlterarConsulta.Name = "btnAlterarConsulta";
            this.btnAlterarConsulta.Size = new System.Drawing.Size(111, 20);
            this.btnAlterarConsulta.Text = "<Alt A> A&lterar";
            this.btnAlterarConsulta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAlterarConsulta.ButtonClick += new System.EventHandler(this.btnAlterarConsulta_ButtonClick);
            // 
            // btnExcluirConsulta
            // 
            this.btnExcluirConsulta.AutoSize = false;
            this.btnExcluirConsulta.DropDownButtonWidth = 0;
            this.btnExcluirConsulta.Image = global::AERMOD.Properties.Resources.delete;
            this.btnExcluirConsulta.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExcluirConsulta.Name = "btnExcluirConsulta";
            this.btnExcluirConsulta.Size = new System.Drawing.Size(120, 20);
            this.btnExcluirConsulta.Text = "<Delete> &Excluir";
            this.btnExcluirConsulta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExcluirConsulta.ButtonClick += new System.EventHandler(this.btnExcluirConsulta_ButtonClick);
            // 
            // btnSairConsulta
            // 
            this.btnSairConsulta.AutoSize = false;
            this.btnSairConsulta.DropDownButtonWidth = 0;
            this.btnSairConsulta.Image = global::AERMOD.Properties.Resources.Sair;
            this.btnSairConsulta.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSairConsulta.Name = "btnSairConsulta";
            this.btnSairConsulta.Size = new System.Drawing.Size(100, 20);
            this.btnSairConsulta.Text = "<Esc> &Sair";
            this.btnSairConsulta.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSairConsulta.ButtonClick += new System.EventHandler(this.btnSairConsulta_ButtonClick);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // FrmDefinicaoAERMET
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(588, 414);
            this.Controls.Add(this.tabControlDados);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmDefinicaoAERMET";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Definições das características de superfície";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmDefinicaoAERMET_FormClosing);
            this.Load += new System.EventHandler(this.FrmDefinicaoAERMET_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmDefinicaoAERMET_KeyDown);
            this.tabControlDados.ResumeLayout(false);
            this.tabPageCadastro.ResumeLayout(false);
            this.tabPageCadastro.PerformLayout();
            this.tabControlCadastro.ResumeLayout(false);
            this.tabPageDadosBasicos.ResumeLayout(false);
            this.tabPageDadosBasicos.PerformLayout();
            this.tabPageCaracteristicas.ResumeLayout(false);
            this.tabPageCaracteristicas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDefinicao)).EndInit();
            this.statusStripCadastro.ResumeLayout(false);
            this.statusStripCadastro.PerformLayout();
            this.tabPageConsulta.ResumeLayout(false);
            this.tabPageConsulta.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDados)).EndInit();
            this.statusStripConsulta.ResumeLayout(false);
            this.statusStripConsulta.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControlDados;
        private System.Windows.Forms.TabPage tabPageCadastro;
        private LIB.Componentes.MaskedTextBoxLIB tbxPeriodoFinal;
        private LIB.Componentes.MaskedTextBoxLIB tbxPeriodoInicial;
        private System.Windows.Forms.Label lbPeriodo;
        private System.Windows.Forms.Label lbLocal;
        private System.Windows.Forms.TextBox tbxLocal;
        private System.Windows.Forms.StatusStrip statusStripCadastro;
        private System.Windows.Forms.ToolStripSplitButton btnAjudaCadastro;
        private System.Windows.Forms.ToolStripSplitButton btnConsultaCadastro;
        private System.Windows.Forms.ToolStripSplitButton btnSalvarCadastro;
        private System.Windows.Forms.ToolStripSplitButton btnSairCadastro;
        private System.Windows.Forms.TabPage tabPageConsulta;
        private System.Windows.Forms.DataGridView dgvDados;
        private System.Windows.Forms.StatusStrip statusStripConsulta;
        private System.Windows.Forms.ToolStripSplitButton btnAjudaConsulta;
        private System.Windows.Forms.ToolStripSplitButton btnInserirConsulta;
        private System.Windows.Forms.ToolStripSplitButton btnAlterarConsulta;
        private System.Windows.Forms.ToolStripSplitButton btnExcluirConsulta;
        private System.Windows.Forms.ToolStripSplitButton btnSairConsulta;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Label lbFrequencia;
        private System.Windows.Forms.Label lbSetor;
        private System.Windows.Forms.ComboBox cbxFrequencia;
        private LIB.Componentes.TextBoxMaskLIB tbxSetorFinal;
        private LIB.Componentes.TextBoxMaskLIB tbxSetorInicial;
        private LIB.Componentes.TextBoxMaskLIB tbxRugosidade;
        private System.Windows.Forms.Label lbRugosidade;
        private LIB.Componentes.TextBoxMaskLIB tbxBowen;
        private System.Windows.Forms.Label lbBowen;
        private LIB.Componentes.TextBoxMaskLIB tbxAlbedo;
        private System.Windows.Forms.Label lbAlbedo;
        private System.Windows.Forms.ComboBox cbxEstacao;
        private System.Windows.Forms.Label lbEstacao;
        private System.Windows.Forms.DataGridView dgvDefinicao;
        private LIB.Componentes.Botao.ButtonLIB btnExcluirCaracteristica;
        private LIB.Componentes.Botao.ButtonLIB btnAlterarCaracteristica;
        private LIB.Componentes.Botao.ButtonLIB btnInserirCaracteristica;
        private System.Windows.Forms.TabControl tabControlCadastro;
        private System.Windows.Forms.TabPage tabPageDadosBasicos;
        private System.Windows.Forms.TabPage tabPageCaracteristicas;
        private LIB.Componentes.Botao.ButtonLIB btnSalvarCaracteristica;
        private LIB.Componentes.GridView.DataGridViewCheckBoxLIBColumn dataGridViewCheckBoxLIBColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn FREQUENCIA;
        private System.Windows.Forms.DataGridViewTextBoxColumn SETOR;
        private System.Windows.Forms.DataGridViewTextBoxColumn ESTACAO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ALBEDO;
        private System.Windows.Forms.DataGridViewTextBoxColumn BOWEN;
        private System.Windows.Forms.DataGridViewTextBoxColumn RUGOSIDADE;
        private System.Windows.Forms.Label lbUF;
        private System.Windows.Forms.ComboBox cbxUF;
        private LIB.Componentes.GridView.DataGridViewCheckBoxLIBColumn STATUS;
        private System.Windows.Forms.DataGridViewTextBoxColumn CODIGO;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOCAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn ESTADO;
        private System.Windows.Forms.DataGridViewTextBoxColumn PERIODO;
        private LIB.Componentes.TextBoxMaskLIB tbxY;
        private LIB.Componentes.TextBoxMaskLIB tbxX;
        private System.Windows.Forms.Label lbY;
        private System.Windows.Forms.Label lbX;
    }
}