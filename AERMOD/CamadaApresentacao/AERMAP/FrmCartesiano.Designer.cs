namespace AERMOD.CamadaApresentacao.AERMAP
{
    partial class FrmCartesiano
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
            this.lbEspacamentoX = new System.Windows.Forms.Label();
            this.lbColunasX = new System.Windows.Forms.Label();
            this.lbGrid_X = new System.Windows.Forms.Label();
            this.lbCodigo = new System.Windows.Forms.Label();
            this.tbxDescricao = new System.Windows.Forms.TextBox();
            this.lbDescricao = new System.Windows.Forms.Label();
            this.lbEspacamentoY = new System.Windows.Forms.Label();
            this.lbColunasY = new System.Windows.Forms.Label();
            this.lbGrid_Y = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.btnAjuda = new System.Windows.Forms.ToolStripSplitButton();
            this.btnSair = new System.Windows.Forms.ToolStripSplitButton();
            this.tbxEspacamentoY = new AERMOD.LIB.Componentes.TextBoxMaskLIB();
            this.tbxColunasY = new AERMOD.LIB.Componentes.TextBoxMaskLIB();
            this.tbxCodigo = new AERMOD.LIB.Componentes.TextBoxMaskLIB();
            this.tbxEspacamentoX = new AERMOD.LIB.Componentes.TextBoxMaskLIB();
            this.tbxColunasX = new AERMOD.LIB.Componentes.TextBoxMaskLIB();
            this.tbxGrid_Y = new AERMOD.LIB.Componentes.TextBoxMaskLIB();
            this.tbxGrid_X = new AERMOD.LIB.Componentes.TextBoxMaskLIB();
            this.btnConsultar = new AERMOD.LIB.Componentes.Botao.ButtonLIB();
            this.btnSalvar = new AERMOD.LIB.Componentes.Botao.ButtonLIB();
            this.btnExcluir = new AERMOD.LIB.Componentes.Botao.ButtonLIB();
            this.btnInserir = new AERMOD.LIB.Componentes.Botao.ButtonLIB();
            this.btnUltimo = new AERMOD.LIB.Componentes.Botao.ButtonLIB();
            this.btnProximo = new AERMOD.LIB.Componentes.Botao.ButtonLIB();
            this.btnAnterior = new AERMOD.LIB.Componentes.Botao.ButtonLIB();
            this.btnPrimeiro = new AERMOD.LIB.Componentes.Botao.ButtonLIB();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbEspacamentoX
            // 
            this.lbEspacamentoX.AutoSize = true;
            this.lbEspacamentoX.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEspacamentoX.Location = new System.Drawing.Point(5, 151);
            this.lbEspacamentoX.Name = "lbEspacamentoX";
            this.lbEspacamentoX.Size = new System.Drawing.Size(151, 16);
            this.lbEspacamentoX.TabIndex = 275;
            this.lbEspacamentoX.Text = "Espaçamento eixo X";
            // 
            // lbColunasX
            // 
            this.lbColunasX.AutoSize = true;
            this.lbColunasX.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbColunasX.Location = new System.Drawing.Point(5, 204);
            this.lbColunasX.Name = "lbColunasX";
            this.lbColunasX.Size = new System.Drawing.Size(119, 16);
            this.lbColunasX.TabIndex = 274;
            this.lbColunasX.Text = "Colunas eixo X";
            // 
            // lbGrid_X
            // 
            this.lbGrid_X.AutoSize = true;
            this.lbGrid_X.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGrid_X.Location = new System.Drawing.Point(5, 98);
            this.lbGrid_X.Name = "lbGrid_X";
            this.lbGrid_X.Size = new System.Drawing.Size(199, 16);
            this.lbGrid_X.TabIndex = 272;
            this.lbGrid_X.Text = "Eixo X inferior esquerdo";
            // 
            // lbCodigo
            // 
            this.lbCodigo.AutoSize = true;
            this.lbCodigo.BackColor = System.Drawing.Color.Transparent;
            this.lbCodigo.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCodigo.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lbCodigo.Location = new System.Drawing.Point(5, 9);
            this.lbCodigo.Name = "lbCodigo";
            this.lbCodigo.Size = new System.Drawing.Size(63, 16);
            this.lbCodigo.TabIndex = 280;
            this.lbCodigo.Text = "Código:";
            this.lbCodigo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbxDescricao
            // 
            this.tbxDescricao.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxDescricao.Location = new System.Drawing.Point(8, 64);
            this.tbxDescricao.MaxLength = 8;
            this.tbxDescricao.Name = "tbxDescricao";
            this.tbxDescricao.Size = new System.Drawing.Size(138, 22);
            this.tbxDescricao.TabIndex = 1;
            // 
            // lbDescricao
            // 
            this.lbDescricao.AutoSize = true;
            this.lbDescricao.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDescricao.Location = new System.Drawing.Point(5, 45);
            this.lbDescricao.Name = "lbDescricao";
            this.lbDescricao.Size = new System.Drawing.Size(79, 16);
            this.lbDescricao.TabIndex = 287;
            this.lbDescricao.Text = "Descrição";
            // 
            // lbEspacamentoY
            // 
            this.lbEspacamentoY.AutoSize = true;
            this.lbEspacamentoY.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEspacamentoY.Location = new System.Drawing.Point(244, 151);
            this.lbEspacamentoY.Name = "lbEspacamentoY";
            this.lbEspacamentoY.Size = new System.Drawing.Size(151, 16);
            this.lbEspacamentoY.TabIndex = 291;
            this.lbEspacamentoY.Text = "Espaçamento eixo Y";
            // 
            // lbColunasY
            // 
            this.lbColunasY.AutoSize = true;
            this.lbColunasY.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbColunasY.Location = new System.Drawing.Point(244, 204);
            this.lbColunasY.Name = "lbColunasY";
            this.lbColunasY.Size = new System.Drawing.Size(119, 16);
            this.lbColunasY.TabIndex = 290;
            this.lbColunasY.Text = "Colunas eixo Y";
            // 
            // lbGrid_Y
            // 
            this.lbGrid_Y.AutoSize = true;
            this.lbGrid_Y.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGrid_Y.Location = new System.Drawing.Point(244, 98);
            this.lbGrid_Y.Name = "lbGrid_Y";
            this.lbGrid_Y.Size = new System.Drawing.Size(199, 16);
            this.lbGrid_Y.TabIndex = 292;
            this.lbGrid_Y.Text = "Eixo Y inferior esquerdo";
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.Color.LightBlue;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAjuda,
            this.btnSair});
            this.statusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip.Location = new System.Drawing.Point(0, 256);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(459, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 293;
            this.statusStrip.Text = "statusStrip1";
            // 
            // btnAjuda
            // 
            this.btnAjuda.AutoSize = false;
            this.btnAjuda.DropDownButtonWidth = 0;
            this.btnAjuda.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAjuda.Image = global::AERMOD.Properties.Resources.ajuda;
            this.btnAjuda.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnAjuda.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAjuda.Name = "btnAjuda";
            this.btnAjuda.Size = new System.Drawing.Size(310, 20);
            this.btnAjuda.Text = "<F1> &Ajuda";
            this.btnAjuda.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAjuda.Click += new System.EventHandler(this.btnAjuda_Click);
            // 
            // btnSair
            // 
            this.btnSair.AutoSize = false;
            this.btnSair.DropDownButtonWidth = 0;
            this.btnSair.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.Image = global::AERMOD.Properties.Resources.Sair;
            this.btnSair.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(85, 20);
            this.btnSair.Text = "<Esc> &Sair";
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // tbxEspacamentoY
            // 
            this.tbxEspacamentoY.CampoHabilitado = true;
            this.tbxEspacamentoY.ColumnName = null;
            this.tbxEspacamentoY.Decimais = 3;
            this.tbxEspacamentoY.FocusColor = System.Drawing.Color.Empty;
            this.tbxEspacamentoY.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxEspacamentoY.Inteiro = 10;
            this.tbxEspacamentoY.InteiroDefault = AERMOD.LIB.Componentes.TextBoxMaskLIB.ValorInteiroDefault.Zero;
            this.tbxEspacamentoY.Location = new System.Drawing.Point(247, 168);
            this.tbxEspacamentoY.Mascara = AERMOD.LIB.Componentes.TextBoxMaskLIB.Mask.Decimal;
            this.tbxEspacamentoY.Name = "tbxEspacamentoY";
            this.tbxEspacamentoY.PermiteNegativo = false;
            this.tbxEspacamentoY.Size = new System.Drawing.Size(200, 22);
            this.tbxEspacamentoY.TabIndex = 5;
            this.tbxEspacamentoY.Text = "0,000";
            this.tbxEspacamentoY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbxColunasY
            // 
            this.tbxColunasY.CampoHabilitado = true;
            this.tbxColunasY.ColumnName = null;
            this.tbxColunasY.Decimais = 3;
            this.tbxColunasY.FocusColor = System.Drawing.Color.Empty;
            this.tbxColunasY.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxColunasY.Inteiro = 6;
            this.tbxColunasY.InteiroDefault = AERMOD.LIB.Componentes.TextBoxMaskLIB.ValorInteiroDefault.Zero;
            this.tbxColunasY.Location = new System.Drawing.Point(247, 221);
            this.tbxColunasY.Mascara = AERMOD.LIB.Componentes.TextBoxMaskLIB.Mask.Inteiro;
            this.tbxColunasY.Name = "tbxColunasY";
            this.tbxColunasY.PermiteNegativo = false;
            this.tbxColunasY.Size = new System.Drawing.Size(115, 22);
            this.tbxColunasY.TabIndex = 7;
            this.tbxColunasY.Text = "0";
            this.tbxColunasY.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbxCodigo
            // 
            this.tbxCodigo.CampoHabilitado = true;
            this.tbxCodigo.ColumnName = null;
            this.tbxCodigo.Decimais = 3;
            this.tbxCodigo.Enabled = false;
            this.tbxCodigo.FocusColor = System.Drawing.Color.Empty;
            this.tbxCodigo.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCodigo.Inteiro = 5;
            this.tbxCodigo.InteiroDefault = AERMOD.LIB.Componentes.TextBoxMaskLIB.ValorInteiroDefault.Zero;
            this.tbxCodigo.Location = new System.Drawing.Point(76, 6);
            this.tbxCodigo.Mascara = AERMOD.LIB.Componentes.TextBoxMaskLIB.Mask.Inteiro;
            this.tbxCodigo.MaxLength = 5;
            this.tbxCodigo.Name = "tbxCodigo";
            this.tbxCodigo.PermiteNegativo = false;
            this.tbxCodigo.Size = new System.Drawing.Size(47, 22);
            this.tbxCodigo.TabIndex = 281;
            this.tbxCodigo.Text = "0";
            this.tbxCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbxEspacamentoX
            // 
            this.tbxEspacamentoX.CampoHabilitado = true;
            this.tbxEspacamentoX.ColumnName = null;
            this.tbxEspacamentoX.Decimais = 3;
            this.tbxEspacamentoX.FocusColor = System.Drawing.Color.Empty;
            this.tbxEspacamentoX.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxEspacamentoX.Inteiro = 10;
            this.tbxEspacamentoX.InteiroDefault = AERMOD.LIB.Componentes.TextBoxMaskLIB.ValorInteiroDefault.Zero;
            this.tbxEspacamentoX.Location = new System.Drawing.Point(8, 168);
            this.tbxEspacamentoX.Mascara = AERMOD.LIB.Componentes.TextBoxMaskLIB.Mask.Decimal;
            this.tbxEspacamentoX.Name = "tbxEspacamentoX";
            this.tbxEspacamentoX.PermiteNegativo = false;
            this.tbxEspacamentoX.Size = new System.Drawing.Size(200, 22);
            this.tbxEspacamentoX.TabIndex = 4;
            this.tbxEspacamentoX.Text = "0,000";
            this.tbxEspacamentoX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbxColunasX
            // 
            this.tbxColunasX.CampoHabilitado = true;
            this.tbxColunasX.ColumnName = null;
            this.tbxColunasX.Decimais = 3;
            this.tbxColunasX.FocusColor = System.Drawing.Color.Empty;
            this.tbxColunasX.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxColunasX.Inteiro = 6;
            this.tbxColunasX.InteiroDefault = AERMOD.LIB.Componentes.TextBoxMaskLIB.ValorInteiroDefault.Zero;
            this.tbxColunasX.Location = new System.Drawing.Point(8, 221);
            this.tbxColunasX.Mascara = AERMOD.LIB.Componentes.TextBoxMaskLIB.Mask.Inteiro;
            this.tbxColunasX.Name = "tbxColunasX";
            this.tbxColunasX.PermiteNegativo = false;
            this.tbxColunasX.Size = new System.Drawing.Size(115, 22);
            this.tbxColunasX.TabIndex = 6;
            this.tbxColunasX.Text = "0";
            this.tbxColunasX.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbxGrid_Y
            // 
            this.tbxGrid_Y.CampoHabilitado = true;
            this.tbxGrid_Y.ColumnName = null;
            this.tbxGrid_Y.Decimais = 3;
            this.tbxGrid_Y.FocusColor = System.Drawing.Color.Empty;
            this.tbxGrid_Y.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxGrid_Y.Inteiro = 10;
            this.tbxGrid_Y.Location = new System.Drawing.Point(247, 117);
            this.tbxGrid_Y.Mascara = AERMOD.LIB.Componentes.TextBoxMaskLIB.Mask.Decimal;
            this.tbxGrid_Y.Name = "tbxGrid_Y";
            this.tbxGrid_Y.Size = new System.Drawing.Size(200, 22);
            this.tbxGrid_Y.TabIndex = 3;
            this.tbxGrid_Y.Text = "0,000";
            this.tbxGrid_Y.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbxGrid_X
            // 
            this.tbxGrid_X.CampoHabilitado = true;
            this.tbxGrid_X.ColumnName = null;
            this.tbxGrid_X.Decimais = 3;
            this.tbxGrid_X.FocusColor = System.Drawing.Color.Empty;
            this.tbxGrid_X.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxGrid_X.Inteiro = 10;
            this.tbxGrid_X.Location = new System.Drawing.Point(8, 117);
            this.tbxGrid_X.Mascara = AERMOD.LIB.Componentes.TextBoxMaskLIB.Mask.Decimal;
            this.tbxGrid_X.Name = "tbxGrid_X";
            this.tbxGrid_X.Size = new System.Drawing.Size(200, 22);
            this.tbxGrid_X.TabIndex = 2;
            this.tbxGrid_X.Text = "0,000";
            this.tbxGrid_X.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnConsultar
            // 
            this.btnConsultar.BackColor = System.Drawing.Color.Transparent;
            this.btnConsultar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConsultar.Image = global::AERMOD.Properties.Resources.search_16x16;
            this.btnConsultar.Location = new System.Drawing.Point(414, 5);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.OpacityColor2 = 110;
            this.btnConsultar.Raio = 5F;
            this.btnConsultar.Size = new System.Drawing.Size(40, 23);
            this.btnConsultar.TabIndex = 15;
            this.btnConsultar.TabStop = false;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // btnSalvar
            // 
            this.btnSalvar.BackColor = System.Drawing.Color.Transparent;
            this.btnSalvar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSalvar.Image = global::AERMOD.Properties.Resources.disk;
            this.btnSalvar.Location = new System.Drawing.Point(373, 5);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.OpacityColor2 = 110;
            this.btnSalvar.Raio = 5F;
            this.btnSalvar.Size = new System.Drawing.Size(40, 23);
            this.btnSalvar.TabIndex = 14;
            this.btnSalvar.TabStop = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnExcluir
            // 
            this.btnExcluir.BackColor = System.Drawing.Color.Transparent;
            this.btnExcluir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExcluir.Image = global::AERMOD.Properties.Resources.delete;
            this.btnExcluir.Location = new System.Drawing.Point(332, 5);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.OpacityColor2 = 110;
            this.btnExcluir.Raio = 5F;
            this.btnExcluir.Size = new System.Drawing.Size(40, 23);
            this.btnExcluir.TabIndex = 13;
            this.btnExcluir.TabStop = false;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // btnInserir
            // 
            this.btnInserir.BackColor = System.Drawing.Color.Transparent;
            this.btnInserir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInserir.Image = global::AERMOD.Properties.Resources.add;
            this.btnInserir.Location = new System.Drawing.Point(291, 5);
            this.btnInserir.Name = "btnInserir";
            this.btnInserir.OpacityColor2 = 110;
            this.btnInserir.Raio = 5F;
            this.btnInserir.Size = new System.Drawing.Size(40, 23);
            this.btnInserir.TabIndex = 12;
            this.btnInserir.TabStop = false;
            this.btnInserir.Click += new System.EventHandler(this.btnInserir_Click);
            // 
            // btnUltimo
            // 
            this.btnUltimo.BackColor = System.Drawing.Color.Transparent;
            this.btnUltimo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUltimo.Image = global::AERMOD.Properties.Resources.Ultimo;
            this.btnUltimo.Location = new System.Drawing.Point(250, 5);
            this.btnUltimo.Name = "btnUltimo";
            this.btnUltimo.OpacityColor2 = 110;
            this.btnUltimo.Raio = 5F;
            this.btnUltimo.Size = new System.Drawing.Size(40, 23);
            this.btnUltimo.TabIndex = 11;
            this.btnUltimo.TabStop = false;
            this.btnUltimo.Click += new System.EventHandler(this.btnUltimo_Click);
            // 
            // btnProximo
            // 
            this.btnProximo.BackColor = System.Drawing.Color.Transparent;
            this.btnProximo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProximo.Image = global::AERMOD.Properties.Resources.Proximo;
            this.btnProximo.Location = new System.Drawing.Point(209, 5);
            this.btnProximo.Name = "btnProximo";
            this.btnProximo.OpacityColor2 = 110;
            this.btnProximo.Raio = 5F;
            this.btnProximo.Size = new System.Drawing.Size(40, 23);
            this.btnProximo.TabIndex = 10;
            this.btnProximo.TabStop = false;
            this.btnProximo.Click += new System.EventHandler(this.btnProximo_Click);
            // 
            // btnAnterior
            // 
            this.btnAnterior.BackColor = System.Drawing.Color.Transparent;
            this.btnAnterior.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnterior.Image = global::AERMOD.Properties.Resources.Anterior;
            this.btnAnterior.Location = new System.Drawing.Point(168, 5);
            this.btnAnterior.Name = "btnAnterior";
            this.btnAnterior.OpacityColor2 = 110;
            this.btnAnterior.Raio = 5F;
            this.btnAnterior.Size = new System.Drawing.Size(40, 23);
            this.btnAnterior.TabIndex = 9;
            this.btnAnterior.TabStop = false;
            this.btnAnterior.Click += new System.EventHandler(this.btnAnterior_Click);
            // 
            // btnPrimeiro
            // 
            this.btnPrimeiro.BackColor = System.Drawing.Color.Transparent;
            this.btnPrimeiro.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrimeiro.Image = global::AERMOD.Properties.Resources.Primeiro;
            this.btnPrimeiro.Location = new System.Drawing.Point(127, 5);
            this.btnPrimeiro.Name = "btnPrimeiro";
            this.btnPrimeiro.OpacityColor2 = 110;
            this.btnPrimeiro.Raio = 5F;
            this.btnPrimeiro.Size = new System.Drawing.Size(40, 23);
            this.btnPrimeiro.TabIndex = 8;
            this.btnPrimeiro.TabStop = false;
            this.btnPrimeiro.Click += new System.EventHandler(this.btnPrimeiro_Click);
            // 
            // FrmCartesiano
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(459, 278);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.lbGrid_Y);
            this.Controls.Add(this.tbxEspacamentoY);
            this.Controls.Add(this.tbxColunasY);
            this.Controls.Add(this.lbEspacamentoY);
            this.Controls.Add(this.lbColunasY);
            this.Controls.Add(this.lbDescricao);
            this.Controls.Add(this.tbxDescricao);
            this.Controls.Add(this.btnConsultar);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.btnExcluir);
            this.Controls.Add(this.btnInserir);
            this.Controls.Add(this.tbxCodigo);
            this.Controls.Add(this.lbCodigo);
            this.Controls.Add(this.btnUltimo);
            this.Controls.Add(this.btnProximo);
            this.Controls.Add(this.btnAnterior);
            this.Controls.Add(this.btnPrimeiro);
            this.Controls.Add(this.tbxEspacamentoX);
            this.Controls.Add(this.tbxColunasX);
            this.Controls.Add(this.tbxGrid_Y);
            this.Controls.Add(this.tbxGrid_X);
            this.Controls.Add(this.lbEspacamentoX);
            this.Controls.Add(this.lbColunasX);
            this.Controls.Add(this.lbGrid_X);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCartesiano";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cartesiano";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmCartesiano_KeyDown);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LIB.Componentes.TextBoxMaskLIB tbxEspacamentoX;
        private LIB.Componentes.TextBoxMaskLIB tbxColunasX;
        private LIB.Componentes.TextBoxMaskLIB tbxGrid_Y;
        private LIB.Componentes.TextBoxMaskLIB tbxGrid_X;
        private System.Windows.Forms.Label lbEspacamentoX;
        private System.Windows.Forms.Label lbColunasX;
        private System.Windows.Forms.Label lbGrid_X;
        private LIB.Componentes.Botao.ButtonLIB btnPrimeiro;
        private LIB.Componentes.Botao.ButtonLIB btnAnterior;
        private LIB.Componentes.Botao.ButtonLIB btnProximo;
        private LIB.Componentes.Botao.ButtonLIB btnUltimo;
        private System.Windows.Forms.Label lbCodigo;
        private LIB.Componentes.TextBoxMaskLIB tbxCodigo;
        private LIB.Componentes.Botao.ButtonLIB btnInserir;
        private LIB.Componentes.Botao.ButtonLIB btnExcluir;
        private LIB.Componentes.Botao.ButtonLIB btnSalvar;
        private LIB.Componentes.Botao.ButtonLIB btnConsultar;
        private System.Windows.Forms.TextBox tbxDescricao;
        private System.Windows.Forms.Label lbDescricao;
        private LIB.Componentes.TextBoxMaskLIB tbxEspacamentoY;
        private LIB.Componentes.TextBoxMaskLIB tbxColunasY;
        private System.Windows.Forms.Label lbEspacamentoY;
        private System.Windows.Forms.Label lbColunasY;
        private System.Windows.Forms.Label lbGrid_Y;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripSplitButton btnAjuda;
        private System.Windows.Forms.ToolStripSplitButton btnSair;
    }
}