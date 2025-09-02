namespace AERMOD.CamadaApresentacao.AERMAP
{
    partial class FrmCartesianoEVALFILE
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
            this.btnConsultar = new AERMOD.LIB.Componentes.Botao.ButtonLIB();
            this.btnSalvar = new AERMOD.LIB.Componentes.Botao.ButtonLIB();
            this.btnExcluir = new AERMOD.LIB.Componentes.Botao.ButtonLIB();
            this.btnInserir = new AERMOD.LIB.Componentes.Botao.ButtonLIB();
            this.tbxCodigo = new AERMOD.LIB.Componentes.TextBoxMaskLIB();
            this.lbCodigo = new System.Windows.Forms.Label();
            this.btnUltimo = new AERMOD.LIB.Componentes.Botao.ButtonLIB();
            this.btnProximo = new AERMOD.LIB.Componentes.Botao.ButtonLIB();
            this.btnAnterior = new AERMOD.LIB.Componentes.Botao.ButtonLIB();
            this.btnPrimeiro = new AERMOD.LIB.Componentes.Botao.ButtonLIB();
            this.lbGrid_Y = new System.Windows.Forms.Label();
            this.tbxAltura = new AERMOD.LIB.Componentes.TextBoxMaskLIB();
            this.lbAltura = new System.Windows.Forms.Label();
            this.lbDescricao = new System.Windows.Forms.Label();
            this.tbxDescricao = new System.Windows.Forms.TextBox();
            this.tbxElevacao = new AERMOD.LIB.Componentes.TextBoxMaskLIB();
            this.tbxCoordenada_Y = new AERMOD.LIB.Componentes.TextBoxMaskLIB();
            this.tbxCoordenada_X = new AERMOD.LIB.Componentes.TextBoxMaskLIB();
            this.lbElevacao = new System.Windows.Forms.Label();
            this.lbGrid_X = new System.Windows.Forms.Label();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.btnAjuda = new System.Windows.Forms.ToolStripSplitButton();
            this.btnSair = new System.Windows.Forms.ToolStripSplitButton();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
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
            this.btnConsultar.TabIndex = 13;
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
            this.btnSalvar.TabIndex = 12;
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
            this.btnExcluir.TabIndex = 11;
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
            this.btnInserir.TabIndex = 10;
            this.btnInserir.TabStop = false;
            this.btnInserir.Click += new System.EventHandler(this.btnInserir_Click);
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
            this.tbxCodigo.TabIndex = 0;
            this.tbxCodigo.Text = "0";
            this.tbxCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
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
            this.lbCodigo.TabIndex = 290;
            this.lbCodigo.Text = "Código:";
            this.lbCodigo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.btnUltimo.TabIndex = 9;
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
            this.btnProximo.TabIndex = 8;
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
            this.btnAnterior.TabIndex = 7;
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
            this.btnPrimeiro.TabIndex = 6;
            this.btnPrimeiro.TabStop = false;
            this.btnPrimeiro.Click += new System.EventHandler(this.btnPrimeiro_Click);
            // 
            // lbGrid_Y
            // 
            this.lbGrid_Y.AutoSize = true;
            this.lbGrid_Y.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGrid_Y.Location = new System.Drawing.Point(247, 93);
            this.lbGrid_Y.Name = "lbGrid_Y";
            this.lbGrid_Y.Size = new System.Drawing.Size(55, 16);
            this.lbGrid_Y.TabIndex = 306;
            this.lbGrid_Y.Text = "Eixo Y";
            // 
            // tbxAltura
            // 
            this.tbxAltura.CampoHabilitado = true;
            this.tbxAltura.ColumnName = null;
            this.tbxAltura.Decimais = 3;
            this.tbxAltura.FocusColor = System.Drawing.Color.Empty;
            this.tbxAltura.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxAltura.Inteiro = 10;
            this.tbxAltura.InteiroDefault = AERMOD.LIB.Componentes.TextBoxMaskLIB.ValorInteiroDefault.Zero;
            this.tbxAltura.Location = new System.Drawing.Point(250, 163);
            this.tbxAltura.Mascara = AERMOD.LIB.Componentes.TextBoxMaskLIB.Mask.Decimal;
            this.tbxAltura.Name = "tbxAltura";
            this.tbxAltura.PermiteNegativo = false;
            this.tbxAltura.Size = new System.Drawing.Size(200, 22);
            this.tbxAltura.TabIndex = 5;
            this.tbxAltura.Text = "0,000";
            this.tbxAltura.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbAltura
            // 
            this.lbAltura.AutoSize = true;
            this.lbAltura.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAltura.Location = new System.Drawing.Point(247, 146);
            this.lbAltura.Name = "lbAltura";
            this.lbAltura.Size = new System.Drawing.Size(55, 16);
            this.lbAltura.TabIndex = 305;
            this.lbAltura.Text = "Altura";
            // 
            // lbDescricao
            // 
            this.lbDescricao.AutoSize = true;
            this.lbDescricao.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDescricao.Location = new System.Drawing.Point(7, 40);
            this.lbDescricao.Name = "lbDescricao";
            this.lbDescricao.Size = new System.Drawing.Size(79, 16);
            this.lbDescricao.TabIndex = 303;
            this.lbDescricao.Text = "Descrição";
            // 
            // tbxDescricao
            // 
            this.tbxDescricao.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxDescricao.Location = new System.Drawing.Point(10, 59);
            this.tbxDescricao.MaxLength = 8;
            this.tbxDescricao.Name = "tbxDescricao";
            this.tbxDescricao.Size = new System.Drawing.Size(138, 22);
            this.tbxDescricao.TabIndex = 1;
            // 
            // tbxElevacao
            // 
            this.tbxElevacao.CampoHabilitado = true;
            this.tbxElevacao.ColumnName = null;
            this.tbxElevacao.Decimais = 3;
            this.tbxElevacao.FocusColor = System.Drawing.Color.Empty;
            this.tbxElevacao.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxElevacao.Inteiro = 10;
            this.tbxElevacao.InteiroDefault = AERMOD.LIB.Componentes.TextBoxMaskLIB.ValorInteiroDefault.Zero;
            this.tbxElevacao.Location = new System.Drawing.Point(10, 163);
            this.tbxElevacao.Mascara = AERMOD.LIB.Componentes.TextBoxMaskLIB.Mask.Decimal;
            this.tbxElevacao.Name = "tbxElevacao";
            this.tbxElevacao.PermiteNegativo = false;
            this.tbxElevacao.Size = new System.Drawing.Size(200, 22);
            this.tbxElevacao.TabIndex = 4;
            this.tbxElevacao.Text = "0,000";
            this.tbxElevacao.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbxCoordenada_Y
            // 
            this.tbxCoordenada_Y.CampoHabilitado = true;
            this.tbxCoordenada_Y.ColumnName = null;
            this.tbxCoordenada_Y.Decimais = 3;
            this.tbxCoordenada_Y.FocusColor = System.Drawing.Color.Empty;
            this.tbxCoordenada_Y.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCoordenada_Y.Inteiro = 10;
            this.tbxCoordenada_Y.Location = new System.Drawing.Point(250, 112);
            this.tbxCoordenada_Y.Mascara = AERMOD.LIB.Componentes.TextBoxMaskLIB.Mask.Decimal;
            this.tbxCoordenada_Y.Name = "tbxCoordenada_Y";
            this.tbxCoordenada_Y.Size = new System.Drawing.Size(200, 22);
            this.tbxCoordenada_Y.TabIndex = 3;
            this.tbxCoordenada_Y.Text = "0,000";
            this.tbxCoordenada_Y.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // tbxCoordenada_X
            // 
            this.tbxCoordenada_X.CampoHabilitado = true;
            this.tbxCoordenada_X.ColumnName = null;
            this.tbxCoordenada_X.Decimais = 3;
            this.tbxCoordenada_X.FocusColor = System.Drawing.Color.Empty;
            this.tbxCoordenada_X.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCoordenada_X.Inteiro = 10;
            this.tbxCoordenada_X.Location = new System.Drawing.Point(10, 112);
            this.tbxCoordenada_X.Mascara = AERMOD.LIB.Componentes.TextBoxMaskLIB.Mask.Decimal;
            this.tbxCoordenada_X.Name = "tbxCoordenada_X";
            this.tbxCoordenada_X.Size = new System.Drawing.Size(200, 22);
            this.tbxCoordenada_X.TabIndex = 2;
            this.tbxCoordenada_X.Text = "0,000";
            this.tbxCoordenada_X.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbElevacao
            // 
            this.lbElevacao.AutoSize = true;
            this.lbElevacao.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbElevacao.Location = new System.Drawing.Point(7, 146);
            this.lbElevacao.Name = "lbElevacao";
            this.lbElevacao.Size = new System.Drawing.Size(71, 16);
            this.lbElevacao.TabIndex = 302;
            this.lbElevacao.Text = "Elevação";
            // 
            // lbGrid_X
            // 
            this.lbGrid_X.AutoSize = true;
            this.lbGrid_X.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGrid_X.Location = new System.Drawing.Point(7, 93);
            this.lbGrid_X.Name = "lbGrid_X";
            this.lbGrid_X.Size = new System.Drawing.Size(55, 16);
            this.lbGrid_X.TabIndex = 300;
            this.lbGrid_X.Text = "Eixo X";
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
            this.statusStrip.TabIndex = 307;
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
            // FrmCartesianoEVALFILE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(459, 278);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.lbGrid_Y);
            this.Controls.Add(this.tbxAltura);
            this.Controls.Add(this.lbAltura);
            this.Controls.Add(this.lbDescricao);
            this.Controls.Add(this.tbxDescricao);
            this.Controls.Add(this.tbxElevacao);
            this.Controls.Add(this.tbxCoordenada_Y);
            this.Controls.Add(this.tbxCoordenada_X);
            this.Controls.Add(this.lbElevacao);
            this.Controls.Add(this.lbGrid_X);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCartesianoEVALFILE";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cartesiano discreto EVALFILE";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmCartesianoEVALFILE_KeyDown);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private LIB.Componentes.Botao.ButtonLIB btnConsultar;
        private LIB.Componentes.Botao.ButtonLIB btnSalvar;
        private LIB.Componentes.Botao.ButtonLIB btnExcluir;
        private LIB.Componentes.Botao.ButtonLIB btnInserir;
        private LIB.Componentes.TextBoxMaskLIB tbxCodigo;
        private System.Windows.Forms.Label lbCodigo;
        private LIB.Componentes.Botao.ButtonLIB btnUltimo;
        private LIB.Componentes.Botao.ButtonLIB btnProximo;
        private LIB.Componentes.Botao.ButtonLIB btnAnterior;
        private LIB.Componentes.Botao.ButtonLIB btnPrimeiro;
        private System.Windows.Forms.Label lbGrid_Y;
        private LIB.Componentes.TextBoxMaskLIB tbxAltura;
        private System.Windows.Forms.Label lbAltura;
        private System.Windows.Forms.Label lbDescricao;
        private System.Windows.Forms.TextBox tbxDescricao;
        private LIB.Componentes.TextBoxMaskLIB tbxElevacao;
        private LIB.Componentes.TextBoxMaskLIB tbxCoordenada_Y;
        private LIB.Componentes.TextBoxMaskLIB tbxCoordenada_X;
        private System.Windows.Forms.Label lbElevacao;
        private System.Windows.Forms.Label lbGrid_X;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripSplitButton btnAjuda;
        private System.Windows.Forms.ToolStripSplitButton btnSair;
    }
}