
namespace AERMOD.CamadaApresentacao
{
    partial class FrmAERMOD
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
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.btnAjuda = new System.Windows.Forms.ToolStripSplitButton();
            this.btnMapa = new System.Windows.Forms.ToolStripSplitButton();
            this.btnBackup = new System.Windows.Forms.ToolStripSplitButton();
            this.btnSair = new System.Windows.Forms.ToolStripSplitButton();
            this.tabControlDados = new System.Windows.Forms.TabControl();
            this.tabPageAERMAP = new System.Windows.Forms.TabPage();
            this.lbVersaoAERMAP = new System.Windows.Forms.Label();
            this.lbLineSaidaAERMAP = new System.Windows.Forms.Label();
            this.lbSaidaAERMAP = new System.Windows.Forms.Label();
            this.lbLineEntradaAERMAP = new System.Windows.Forms.Label();
            this.lbEntradaAERMAP = new System.Windows.Forms.Label();
            this.btnDominio = new System.Windows.Forms.Button();
            this.btnSOU_AERMAP = new System.Windows.Forms.Button();
            this.btnROU_AERMAP = new System.Windows.Forms.Button();
            this.btnOUT_AERMAP = new System.Windows.Forms.Button();
            this.btnINP_AERMAP = new System.Windows.Forms.Button();
            this.btnExecutarAERMAP = new System.Windows.Forms.Button();
            this.btnFontesEmissao = new System.Windows.Forms.Button();
            this.btnTIF = new System.Windows.Forms.Button();
            this.tabPageAERMET = new System.Windows.Forms.TabPage();
            this.lbVersaoAERMET = new System.Windows.Forms.Label();
            this.btnDefinicaoAERMET = new System.Windows.Forms.Button();
            this.lbLineEntradaAERMET = new System.Windows.Forms.Label();
            this.lbEntradaAERMET = new System.Windows.Forms.Label();
            this.lbLineSaidaAERMET = new System.Windows.Forms.Label();
            this.lbSaidaAERMET = new System.Windows.Forms.Label();
            this.btnINP1_AERMET = new System.Windows.Forms.Button();
            this.btnINP2_AERMET = new System.Windows.Forms.Button();
            this.btnSFC_AERMET = new System.Windows.Forms.Button();
            this.btnPFL_AERMET = new System.Windows.Forms.Button();
            this.btnExecutarAERMET = new System.Windows.Forms.Button();
            this.btnEditorSAM = new System.Windows.Forms.Button();
            this.btnFSL = new System.Windows.Forms.Button();
            this.tabPageAERMOD = new System.Windows.Forms.TabPage();
            this.lbVersaoAERMOD = new System.Windows.Forms.Label();
            this.btnParametrosAERMOD = new System.Windows.Forms.Button();
            this.btnDefinicaoAERMOD = new System.Windows.Forms.Button();
            this.btnPLT_AERMOD = new System.Windows.Forms.Button();
            this.btnOUT2_AERMOD = new System.Windows.Forms.Button();
            this.btnOUT1_AERMOD = new System.Windows.Forms.Button();
            this.btnINP_AERMOD = new System.Windows.Forms.Button();
            this.lbLineSaidaAERMOD = new System.Windows.Forms.Label();
            this.lbSaidaAERMOD = new System.Windows.Forms.Label();
            this.lbLineEntradaAERMOD = new System.Windows.Forms.Label();
            this.lbEntradaAERMOD = new System.Windows.Forms.Label();
            this.btnExecutarAERMOD = new System.Windows.Forms.Button();
            this.tabPageParametros = new System.Windows.Forms.TabPage();
            this.lbCaminhoDataBase = new System.Windows.Forms.Label();
            this.tbxCaminhoDataBase = new System.Windows.Forms.TextBox();
            this.btnAtualizacao = new System.Windows.Forms.Button();
            this.lbLineAtualizacao = new System.Windows.Forms.Label();
            this.lbAtualizacao = new System.Windows.Forms.Label();
            this.lbLineDataBase = new System.Windows.Forms.Label();
            this.lbDataBase = new System.Windows.Forms.Label();
            this.btnCaminhoDataBase = new AERMOD.LIB.Componentes.Botao.ButtonLIB();
            this.statusStrip.SuspendLayout();
            this.tabControlDados.SuspendLayout();
            this.tabPageAERMAP.SuspendLayout();
            this.tabPageAERMET.SuspendLayout();
            this.tabPageAERMOD.SuspendLayout();
            this.tabPageParametros.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.BackColor = System.Drawing.Color.LightBlue;
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAjuda,
            this.btnMapa,
            this.btnBackup,
            this.btnSair});
            this.statusStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip.Location = new System.Drawing.Point(0, 280);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(411, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 8;
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
            this.btnAjuda.Size = new System.Drawing.Size(110, 20);
            this.btnAjuda.Text = "<F1> &Ajuda";
            this.btnAjuda.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAjuda.Click += new System.EventHandler(this.btnAjuda_Click);
            // 
            // btnMapa
            // 
            this.btnMapa.AutoSize = false;
            this.btnMapa.DropDownButtonWidth = 0;
            this.btnMapa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMapa.Image = global::AERMOD.Properties.Resources.maps;
            this.btnMapa.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMapa.Name = "btnMapa";
            this.btnMapa.Size = new System.Drawing.Size(96, 20);
            this.btnMapa.Text = "<F11> &Mapa";
            this.btnMapa.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMapa.ButtonClick += new System.EventHandler(this.btnMapa_ButtonClick);
            // 
            // btnBackup
            // 
            this.btnBackup.AutoSize = false;
            this.btnBackup.DropDownButtonWidth = 0;
            this.btnBackup.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackup.Image = global::AERMOD.Properties.Resources.backup;
            this.btnBackup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(107, 20);
            this.btnBackup.Text = "<F12> &Backup";
            this.btnBackup.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnBackup.ButtonClick += new System.EventHandler(this.btnBackup_ButtonClick);
            // 
            // btnSair
            // 
            this.btnSair.AutoSize = false;
            this.btnSair.DropDownButtonWidth = 0;
            this.btnSair.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.Image = global::AERMOD.Properties.Resources.Sair;
            this.btnSair.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(83, 20);
            this.btnSair.Text = "<Esc> &Sair";
            this.btnSair.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSair.ButtonClick += new System.EventHandler(this.btnSair_ButtonClick);
            // 
            // tabControlDados
            // 
            this.tabControlDados.Controls.Add(this.tabPageAERMAP);
            this.tabControlDados.Controls.Add(this.tabPageAERMET);
            this.tabControlDados.Controls.Add(this.tabPageAERMOD);
            this.tabControlDados.Controls.Add(this.tabPageParametros);
            this.tabControlDados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlDados.Location = new System.Drawing.Point(0, 0);
            this.tabControlDados.Name = "tabControlDados";
            this.tabControlDados.SelectedIndex = 0;
            this.tabControlDados.Size = new System.Drawing.Size(411, 280);
            this.tabControlDados.TabIndex = 0;
            // 
            // tabPageAERMAP
            // 
            this.tabPageAERMAP.Controls.Add(this.lbVersaoAERMAP);
            this.tabPageAERMAP.Controls.Add(this.lbLineSaidaAERMAP);
            this.tabPageAERMAP.Controls.Add(this.lbSaidaAERMAP);
            this.tabPageAERMAP.Controls.Add(this.lbLineEntradaAERMAP);
            this.tabPageAERMAP.Controls.Add(this.lbEntradaAERMAP);
            this.tabPageAERMAP.Controls.Add(this.btnDominio);
            this.tabPageAERMAP.Controls.Add(this.btnSOU_AERMAP);
            this.tabPageAERMAP.Controls.Add(this.btnROU_AERMAP);
            this.tabPageAERMAP.Controls.Add(this.btnOUT_AERMAP);
            this.tabPageAERMAP.Controls.Add(this.btnINP_AERMAP);
            this.tabPageAERMAP.Controls.Add(this.btnExecutarAERMAP);
            this.tabPageAERMAP.Controls.Add(this.btnFontesEmissao);
            this.tabPageAERMAP.Controls.Add(this.btnTIF);
            this.tabPageAERMAP.Location = new System.Drawing.Point(4, 22);
            this.tabPageAERMAP.Name = "tabPageAERMAP";
            this.tabPageAERMAP.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAERMAP.Size = new System.Drawing.Size(403, 254);
            this.tabPageAERMAP.TabIndex = 0;
            this.tabPageAERMAP.Text = "AERMAP";
            this.tabPageAERMAP.UseVisualStyleBackColor = true;
            // 
            // lbVersaoAERMAP
            // 
            this.lbVersaoAERMAP.AutoSize = true;
            this.lbVersaoAERMAP.BackColor = System.Drawing.Color.Transparent;
            this.lbVersaoAERMAP.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVersaoAERMAP.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lbVersaoAERMAP.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lbVersaoAERMAP.Location = new System.Drawing.Point(342, 0);
            this.lbVersaoAERMAP.Name = "lbVersaoAERMAP";
            this.lbVersaoAERMAP.Size = new System.Drawing.Size(55, 17);
            this.lbVersaoAERMAP.TabIndex = 270;
            this.lbVersaoAERMAP.Text = "V.18081";
            this.lbVersaoAERMAP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbLineSaidaAERMAP
            // 
            this.lbLineSaidaAERMAP.AutoSize = true;
            this.lbLineSaidaAERMAP.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lbLineSaidaAERMAP.Location = new System.Drawing.Point(56, 132);
            this.lbLineSaidaAERMAP.Name = "lbLineSaidaAERMAP";
            this.lbLineSaidaAERMAP.Size = new System.Drawing.Size(337, 13);
            this.lbLineSaidaAERMAP.TabIndex = 269;
            this.lbLineSaidaAERMAP.Text = "---------------------------------------------------------------------------------" +
    "-----------------------------";
            // 
            // lbSaidaAERMAP
            // 
            this.lbSaidaAERMAP.AutoSize = true;
            this.lbSaidaAERMAP.BackColor = System.Drawing.Color.Transparent;
            this.lbSaidaAERMAP.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSaidaAERMAP.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lbSaidaAERMAP.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lbSaidaAERMAP.Location = new System.Drawing.Point(10, 129);
            this.lbSaidaAERMAP.Name = "lbSaidaAERMAP";
            this.lbSaidaAERMAP.Size = new System.Drawing.Size(47, 17);
            this.lbSaidaAERMAP.TabIndex = 268;
            this.lbSaidaAERMAP.Text = "Saídas";
            this.lbSaidaAERMAP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbLineEntradaAERMAP
            // 
            this.lbLineEntradaAERMAP.AutoSize = true;
            this.lbLineEntradaAERMAP.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lbLineEntradaAERMAP.Location = new System.Drawing.Point(70, 10);
            this.lbLineEntradaAERMAP.Name = "lbLineEntradaAERMAP";
            this.lbLineEntradaAERMAP.Size = new System.Drawing.Size(325, 13);
            this.lbLineEntradaAERMAP.TabIndex = 267;
            this.lbLineEntradaAERMAP.Text = "---------------------------------------------------------------------------------" +
    "-------------------------";
            // 
            // lbEntradaAERMAP
            // 
            this.lbEntradaAERMAP.AutoSize = true;
            this.lbEntradaAERMAP.BackColor = System.Drawing.Color.Transparent;
            this.lbEntradaAERMAP.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEntradaAERMAP.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lbEntradaAERMAP.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lbEntradaAERMAP.Location = new System.Drawing.Point(10, 7);
            this.lbEntradaAERMAP.Name = "lbEntradaAERMAP";
            this.lbEntradaAERMAP.Size = new System.Drawing.Size(61, 17);
            this.lbEntradaAERMAP.TabIndex = 261;
            this.lbEntradaAERMAP.Text = "Entradas";
            this.lbEntradaAERMAP.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnDominio
            // 
            this.btnDominio.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDominio.Image = global::AERMOD.Properties.Resources.search_16x16;
            this.btnDominio.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDominio.Location = new System.Drawing.Point(204, 34);
            this.btnDominio.Name = "btnDominio";
            this.btnDominio.Size = new System.Drawing.Size(186, 34);
            this.btnDominio.TabIndex = 2;
            this.btnDominio.Text = " <Ctrl D> Domínio/Grade";
            this.btnDominio.UseVisualStyleBackColor = true;
            this.btnDominio.Click += new System.EventHandler(this.btnDominio_Click);
            // 
            // btnSOU_AERMAP
            // 
            this.btnSOU_AERMAP.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSOU_AERMAP.Image = global::AERMOD.Properties.Resources.down;
            this.btnSOU_AERMAP.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSOU_AERMAP.Location = new System.Drawing.Point(204, 210);
            this.btnSOU_AERMAP.Name = "btnSOU_AERMAP";
            this.btnSOU_AERMAP.Size = new System.Drawing.Size(186, 34);
            this.btnSOU_AERMAP.TabIndex = 7;
            this.btnSOU_AERMAP.Text = "   <Ctrl S> AERMAP.SOU";
            this.btnSOU_AERMAP.UseVisualStyleBackColor = true;
            this.btnSOU_AERMAP.Click += new System.EventHandler(this.btnSOU_AERMAP_Click);
            // 
            // btnROU_AERMAP
            // 
            this.btnROU_AERMAP.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnROU_AERMAP.Image = global::AERMOD.Properties.Resources.down;
            this.btnROU_AERMAP.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnROU_AERMAP.Location = new System.Drawing.Point(204, 170);
            this.btnROU_AERMAP.Name = "btnROU_AERMAP";
            this.btnROU_AERMAP.Size = new System.Drawing.Size(186, 34);
            this.btnROU_AERMAP.TabIndex = 6;
            this.btnROU_AERMAP.Text = "   <Ctrl R> AERMAP.ROU";
            this.btnROU_AERMAP.UseVisualStyleBackColor = true;
            this.btnROU_AERMAP.Click += new System.EventHandler(this.btnROU_AERMAP_Click);
            // 
            // btnOUT_AERMAP
            // 
            this.btnOUT_AERMAP.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOUT_AERMAP.Image = global::AERMOD.Properties.Resources.down;
            this.btnOUT_AERMAP.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOUT_AERMAP.Location = new System.Drawing.Point(12, 210);
            this.btnOUT_AERMAP.Name = "btnOUT_AERMAP";
            this.btnOUT_AERMAP.Size = new System.Drawing.Size(186, 34);
            this.btnOUT_AERMAP.TabIndex = 5;
            this.btnOUT_AERMAP.Text = "   <Ctrl O> AERMAP.OUT";
            this.btnOUT_AERMAP.UseVisualStyleBackColor = true;
            this.btnOUT_AERMAP.Click += new System.EventHandler(this.btnOUT_AERMAP_Click);
            // 
            // btnINP_AERMAP
            // 
            this.btnINP_AERMAP.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnINP_AERMAP.Image = global::AERMOD.Properties.Resources.down;
            this.btnINP_AERMAP.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnINP_AERMAP.Location = new System.Drawing.Point(12, 170);
            this.btnINP_AERMAP.Name = "btnINP_AERMAP";
            this.btnINP_AERMAP.Size = new System.Drawing.Size(186, 34);
            this.btnINP_AERMAP.TabIndex = 4;
            this.btnINP_AERMAP.Text = " <Ctrl I> AERMAP.INP";
            this.btnINP_AERMAP.UseVisualStyleBackColor = true;
            this.btnINP_AERMAP.Click += new System.EventHandler(this.btnINP_AERMAP_Click);
            // 
            // btnExecutarAERMAP
            // 
            this.btnExecutarAERMAP.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExecutarAERMAP.Image = global::AERMOD.Properties.Resources.executar;
            this.btnExecutarAERMAP.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExecutarAERMAP.Location = new System.Drawing.Point(204, 74);
            this.btnExecutarAERMAP.Name = "btnExecutarAERMAP";
            this.btnExecutarAERMAP.Size = new System.Drawing.Size(186, 34);
            this.btnExecutarAERMAP.TabIndex = 3;
            this.btnExecutarAERMAP.Text = "   <Enter> Executar";
            this.btnExecutarAERMAP.UseVisualStyleBackColor = true;
            this.btnExecutarAERMAP.Click += new System.EventHandler(this.btnExecutarAERMAP_Click);
            // 
            // btnFontesEmissao
            // 
            this.btnFontesEmissao.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFontesEmissao.Image = global::AERMOD.Properties.Resources.search_16x16;
            this.btnFontesEmissao.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFontesEmissao.Location = new System.Drawing.Point(12, 74);
            this.btnFontesEmissao.Name = "btnFontesEmissao";
            this.btnFontesEmissao.Size = new System.Drawing.Size(186, 34);
            this.btnFontesEmissao.TabIndex = 1;
            this.btnFontesEmissao.Text = "  <Ctrl F> Fonte Emissora";
            this.btnFontesEmissao.UseVisualStyleBackColor = true;
            this.btnFontesEmissao.Click += new System.EventHandler(this.btnFontesEmissao_Click);
            // 
            // btnTIF
            // 
            this.btnTIF.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTIF.Image = global::AERMOD.Properties.Resources.search_16x16;
            this.btnTIF.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTIF.Location = new System.Drawing.Point(12, 34);
            this.btnTIF.Name = "btnTIF";
            this.btnTIF.Size = new System.Drawing.Size(186, 34);
            this.btnTIF.TabIndex = 0;
            this.btnTIF.Text = "   <Ctrl T> Arquivo (.TIF)";
            this.btnTIF.UseVisualStyleBackColor = true;
            this.btnTIF.Click += new System.EventHandler(this.btnTIF_Click);
            // 
            // tabPageAERMET
            // 
            this.tabPageAERMET.Controls.Add(this.lbVersaoAERMET);
            this.tabPageAERMET.Controls.Add(this.btnDefinicaoAERMET);
            this.tabPageAERMET.Controls.Add(this.lbLineEntradaAERMET);
            this.tabPageAERMET.Controls.Add(this.lbEntradaAERMET);
            this.tabPageAERMET.Controls.Add(this.lbLineSaidaAERMET);
            this.tabPageAERMET.Controls.Add(this.lbSaidaAERMET);
            this.tabPageAERMET.Controls.Add(this.btnINP1_AERMET);
            this.tabPageAERMET.Controls.Add(this.btnINP2_AERMET);
            this.tabPageAERMET.Controls.Add(this.btnSFC_AERMET);
            this.tabPageAERMET.Controls.Add(this.btnPFL_AERMET);
            this.tabPageAERMET.Controls.Add(this.btnExecutarAERMET);
            this.tabPageAERMET.Controls.Add(this.btnEditorSAM);
            this.tabPageAERMET.Controls.Add(this.btnFSL);
            this.tabPageAERMET.Location = new System.Drawing.Point(4, 22);
            this.tabPageAERMET.Name = "tabPageAERMET";
            this.tabPageAERMET.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAERMET.Size = new System.Drawing.Size(403, 254);
            this.tabPageAERMET.TabIndex = 1;
            this.tabPageAERMET.Text = "AERMET";
            this.tabPageAERMET.UseVisualStyleBackColor = true;
            // 
            // lbVersaoAERMET
            // 
            this.lbVersaoAERMET.AutoSize = true;
            this.lbVersaoAERMET.BackColor = System.Drawing.Color.Transparent;
            this.lbVersaoAERMET.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVersaoAERMET.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lbVersaoAERMET.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lbVersaoAERMET.Location = new System.Drawing.Point(341, 0);
            this.lbVersaoAERMET.Name = "lbVersaoAERMET";
            this.lbVersaoAERMET.Size = new System.Drawing.Size(55, 17);
            this.lbVersaoAERMET.TabIndex = 279;
            this.lbVersaoAERMET.Text = "V.23132";
            this.lbVersaoAERMET.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnDefinicaoAERMET
            // 
            this.btnDefinicaoAERMET.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDefinicaoAERMET.Image = global::AERMOD.Properties.Resources.automacao;
            this.btnDefinicaoAERMET.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDefinicaoAERMET.Location = new System.Drawing.Point(12, 34);
            this.btnDefinicaoAERMET.Name = "btnDefinicaoAERMET";
            this.btnDefinicaoAERMET.Size = new System.Drawing.Size(186, 34);
            this.btnDefinicaoAERMET.TabIndex = 0;
            this.btnDefinicaoAERMET.Text = "    <Ctrl D> Definições";
            this.btnDefinicaoAERMET.UseVisualStyleBackColor = true;
            this.btnDefinicaoAERMET.Click += new System.EventHandler(this.btnDefinicaoAERMET_Click);
            // 
            // lbLineEntradaAERMET
            // 
            this.lbLineEntradaAERMET.AutoSize = true;
            this.lbLineEntradaAERMET.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lbLineEntradaAERMET.Location = new System.Drawing.Point(70, 10);
            this.lbLineEntradaAERMET.Name = "lbLineEntradaAERMET";
            this.lbLineEntradaAERMET.Size = new System.Drawing.Size(325, 13);
            this.lbLineEntradaAERMET.TabIndex = 278;
            this.lbLineEntradaAERMET.Text = "---------------------------------------------------------------------------------" +
    "-------------------------";
            // 
            // lbEntradaAERMET
            // 
            this.lbEntradaAERMET.AutoSize = true;
            this.lbEntradaAERMET.BackColor = System.Drawing.Color.Transparent;
            this.lbEntradaAERMET.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEntradaAERMET.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lbEntradaAERMET.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lbEntradaAERMET.Location = new System.Drawing.Point(10, 7);
            this.lbEntradaAERMET.Name = "lbEntradaAERMET";
            this.lbEntradaAERMET.Size = new System.Drawing.Size(61, 17);
            this.lbEntradaAERMET.TabIndex = 277;
            this.lbEntradaAERMET.Text = "Entradas";
            this.lbEntradaAERMET.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbLineSaidaAERMET
            // 
            this.lbLineSaidaAERMET.AutoSize = true;
            this.lbLineSaidaAERMET.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lbLineSaidaAERMET.Location = new System.Drawing.Point(56, 132);
            this.lbLineSaidaAERMET.Name = "lbLineSaidaAERMET";
            this.lbLineSaidaAERMET.Size = new System.Drawing.Size(337, 13);
            this.lbLineSaidaAERMET.TabIndex = 273;
            this.lbLineSaidaAERMET.Text = "---------------------------------------------------------------------------------" +
    "-----------------------------";
            // 
            // lbSaidaAERMET
            // 
            this.lbSaidaAERMET.AutoSize = true;
            this.lbSaidaAERMET.BackColor = System.Drawing.Color.Transparent;
            this.lbSaidaAERMET.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSaidaAERMET.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lbSaidaAERMET.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lbSaidaAERMET.Location = new System.Drawing.Point(10, 129);
            this.lbSaidaAERMET.Name = "lbSaidaAERMET";
            this.lbSaidaAERMET.Size = new System.Drawing.Size(47, 17);
            this.lbSaidaAERMET.TabIndex = 272;
            this.lbSaidaAERMET.Text = "Saídas";
            this.lbSaidaAERMET.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnINP1_AERMET
            // 
            this.btnINP1_AERMET.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnINP1_AERMET.Image = global::AERMOD.Properties.Resources.down;
            this.btnINP1_AERMET.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnINP1_AERMET.Location = new System.Drawing.Point(12, 170);
            this.btnINP1_AERMET.Name = "btnINP1_AERMET";
            this.btnINP1_AERMET.Size = new System.Drawing.Size(186, 34);
            this.btnINP1_AERMET.TabIndex = 4;
            this.btnINP1_AERMET.Text = "   <Ctrl 1> AERMET_1.INP";
            this.btnINP1_AERMET.UseVisualStyleBackColor = true;
            this.btnINP1_AERMET.Click += new System.EventHandler(this.btnINP1_AERMET_Click);
            // 
            // btnINP2_AERMET
            // 
            this.btnINP2_AERMET.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnINP2_AERMET.Image = global::AERMOD.Properties.Resources.down;
            this.btnINP2_AERMET.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnINP2_AERMET.Location = new System.Drawing.Point(12, 210);
            this.btnINP2_AERMET.Name = "btnINP2_AERMET";
            this.btnINP2_AERMET.Size = new System.Drawing.Size(186, 34);
            this.btnINP2_AERMET.TabIndex = 5;
            this.btnINP2_AERMET.Text = "   <Ctrl 2> AERMET_2.INP";
            this.btnINP2_AERMET.UseVisualStyleBackColor = true;
            this.btnINP2_AERMET.Click += new System.EventHandler(this.btnINP2_AERMET_Click);
            // 
            // btnSFC_AERMET
            // 
            this.btnSFC_AERMET.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSFC_AERMET.Image = global::AERMOD.Properties.Resources.down;
            this.btnSFC_AERMET.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSFC_AERMET.Location = new System.Drawing.Point(204, 210);
            this.btnSFC_AERMET.Name = "btnSFC_AERMET";
            this.btnSFC_AERMET.Size = new System.Drawing.Size(186, 34);
            this.btnSFC_AERMET.TabIndex = 7;
            this.btnSFC_AERMET.Text = "   <Ctrl S> AERMET.SFC";
            this.btnSFC_AERMET.UseVisualStyleBackColor = true;
            this.btnSFC_AERMET.Click += new System.EventHandler(this.btnSFC_AERMET_Click);
            // 
            // btnPFL_AERMET
            // 
            this.btnPFL_AERMET.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPFL_AERMET.Image = global::AERMOD.Properties.Resources.down;
            this.btnPFL_AERMET.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPFL_AERMET.Location = new System.Drawing.Point(204, 170);
            this.btnPFL_AERMET.Name = "btnPFL_AERMET";
            this.btnPFL_AERMET.Size = new System.Drawing.Size(186, 34);
            this.btnPFL_AERMET.TabIndex = 6;
            this.btnPFL_AERMET.Text = "   <Ctrl P> AERMET.PFL";
            this.btnPFL_AERMET.UseVisualStyleBackColor = true;
            this.btnPFL_AERMET.Click += new System.EventHandler(this.btnPFL_AERMET_Click);
            // 
            // btnExecutarAERMET
            // 
            this.btnExecutarAERMET.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExecutarAERMET.Image = global::AERMOD.Properties.Resources.executar;
            this.btnExecutarAERMET.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExecutarAERMET.Location = new System.Drawing.Point(204, 74);
            this.btnExecutarAERMET.Name = "btnExecutarAERMET";
            this.btnExecutarAERMET.Size = new System.Drawing.Size(186, 34);
            this.btnExecutarAERMET.TabIndex = 3;
            this.btnExecutarAERMET.Text = "<Enter> Executar";
            this.btnExecutarAERMET.UseVisualStyleBackColor = true;
            this.btnExecutarAERMET.Click += new System.EventHandler(this.btnExecutarAERMET_Click);
            // 
            // btnEditorSAM
            // 
            this.btnEditorSAM.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditorSAM.Image = global::AERMOD.Properties.Resources.document_edit_16;
            this.btnEditorSAM.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEditorSAM.Location = new System.Drawing.Point(204, 34);
            this.btnEditorSAM.Name = "btnEditorSAM";
            this.btnEditorSAM.Size = new System.Drawing.Size(186, 34);
            this.btnEditorSAM.TabIndex = 2;
            this.btnEditorSAM.Text = "    <Ctrl S> Editor (.SAM)";
            this.btnEditorSAM.UseVisualStyleBackColor = true;
            this.btnEditorSAM.Click += new System.EventHandler(this.btnEditorSAM_Click);
            // 
            // btnFSL
            // 
            this.btnFSL.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFSL.Image = global::AERMOD.Properties.Resources.search_16x16;
            this.btnFSL.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFSL.Location = new System.Drawing.Point(12, 74);
            this.btnFSL.Name = "btnFSL";
            this.btnFSL.Size = new System.Drawing.Size(186, 34);
            this.btnFSL.TabIndex = 1;
            this.btnFSL.Text = "  <Ctrl F> Arquivo (.FSL)";
            this.btnFSL.UseVisualStyleBackColor = true;
            this.btnFSL.Click += new System.EventHandler(this.btnFSL_Click);
            // 
            // tabPageAERMOD
            // 
            this.tabPageAERMOD.Controls.Add(this.lbVersaoAERMOD);
            this.tabPageAERMOD.Controls.Add(this.btnParametrosAERMOD);
            this.tabPageAERMOD.Controls.Add(this.btnDefinicaoAERMOD);
            this.tabPageAERMOD.Controls.Add(this.btnPLT_AERMOD);
            this.tabPageAERMOD.Controls.Add(this.btnOUT2_AERMOD);
            this.tabPageAERMOD.Controls.Add(this.btnOUT1_AERMOD);
            this.tabPageAERMOD.Controls.Add(this.btnINP_AERMOD);
            this.tabPageAERMOD.Controls.Add(this.lbLineSaidaAERMOD);
            this.tabPageAERMOD.Controls.Add(this.lbSaidaAERMOD);
            this.tabPageAERMOD.Controls.Add(this.lbLineEntradaAERMOD);
            this.tabPageAERMOD.Controls.Add(this.lbEntradaAERMOD);
            this.tabPageAERMOD.Controls.Add(this.btnExecutarAERMOD);
            this.tabPageAERMOD.Location = new System.Drawing.Point(4, 22);
            this.tabPageAERMOD.Name = "tabPageAERMOD";
            this.tabPageAERMOD.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageAERMOD.Size = new System.Drawing.Size(403, 254);
            this.tabPageAERMOD.TabIndex = 2;
            this.tabPageAERMOD.Text = "AERMOD";
            this.tabPageAERMOD.UseVisualStyleBackColor = true;
            // 
            // lbVersaoAERMOD
            // 
            this.lbVersaoAERMOD.AutoSize = true;
            this.lbVersaoAERMOD.BackColor = System.Drawing.Color.Transparent;
            this.lbVersaoAERMOD.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVersaoAERMOD.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lbVersaoAERMOD.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lbVersaoAERMOD.Location = new System.Drawing.Point(341, 0);
            this.lbVersaoAERMOD.Name = "lbVersaoAERMOD";
            this.lbVersaoAERMOD.Size = new System.Drawing.Size(55, 17);
            this.lbVersaoAERMOD.TabIndex = 283;
            this.lbVersaoAERMOD.Text = "V.23132";
            this.lbVersaoAERMOD.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnParametrosAERMOD
            // 
            this.btnParametrosAERMOD.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnParametrosAERMOD.Image = global::AERMOD.Properties.Resources.Condicao;
            this.btnParametrosAERMOD.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnParametrosAERMOD.Location = new System.Drawing.Point(12, 74);
            this.btnParametrosAERMOD.Name = "btnParametrosAERMOD";
            this.btnParametrosAERMOD.Size = new System.Drawing.Size(186, 34);
            this.btnParametrosAERMOD.TabIndex = 1;
            this.btnParametrosAERMOD.Text = "    <Ctrl P> Parâmetros";
            this.btnParametrosAERMOD.UseVisualStyleBackColor = true;
            this.btnParametrosAERMOD.Click += new System.EventHandler(this.btnParametrosAERMOD_Click);
            // 
            // btnDefinicaoAERMOD
            // 
            this.btnDefinicaoAERMOD.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDefinicaoAERMOD.Image = global::AERMOD.Properties.Resources.automacao;
            this.btnDefinicaoAERMOD.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDefinicaoAERMOD.Location = new System.Drawing.Point(12, 34);
            this.btnDefinicaoAERMOD.Name = "btnDefinicaoAERMOD";
            this.btnDefinicaoAERMOD.Size = new System.Drawing.Size(186, 34);
            this.btnDefinicaoAERMOD.TabIndex = 0;
            this.btnDefinicaoAERMOD.Text = "    <Ctrl D> Definições";
            this.btnDefinicaoAERMOD.UseVisualStyleBackColor = true;
            this.btnDefinicaoAERMOD.Click += new System.EventHandler(this.btnDefinicaoAERMOD_Click);
            // 
            // btnPLT_AERMOD
            // 
            this.btnPLT_AERMOD.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPLT_AERMOD.Image = global::AERMOD.Properties.Resources.down;
            this.btnPLT_AERMOD.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPLT_AERMOD.Location = new System.Drawing.Point(204, 210);
            this.btnPLT_AERMOD.Name = "btnPLT_AERMOD";
            this.btnPLT_AERMOD.Size = new System.Drawing.Size(186, 34);
            this.btnPLT_AERMOD.TabIndex = 7;
            this.btnPLT_AERMOD.Text = "  <Ctrl A> AERMOD.PLT";
            this.btnPLT_AERMOD.UseVisualStyleBackColor = true;
            this.btnPLT_AERMOD.Click += new System.EventHandler(this.btnPLT_AERMOD_Click);
            // 
            // btnOUT2_AERMOD
            // 
            this.btnOUT2_AERMOD.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOUT2_AERMOD.Image = global::AERMOD.Properties.Resources.down;
            this.btnOUT2_AERMOD.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOUT2_AERMOD.Location = new System.Drawing.Point(204, 170);
            this.btnOUT2_AERMOD.Name = "btnOUT2_AERMOD";
            this.btnOUT2_AERMOD.Size = new System.Drawing.Size(186, 34);
            this.btnOUT2_AERMOD.TabIndex = 6;
            this.btnOUT2_AERMOD.Text = "  <Ctrl E> ERRORS.OUT";
            this.btnOUT2_AERMOD.UseVisualStyleBackColor = true;
            this.btnOUT2_AERMOD.Click += new System.EventHandler(this.btnOUT2_AERMOD_Click);
            // 
            // btnOUT1_AERMOD
            // 
            this.btnOUT1_AERMOD.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOUT1_AERMOD.Image = global::AERMOD.Properties.Resources.down;
            this.btnOUT1_AERMOD.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOUT1_AERMOD.Location = new System.Drawing.Point(12, 210);
            this.btnOUT1_AERMOD.Name = "btnOUT1_AERMOD";
            this.btnOUT1_AERMOD.Size = new System.Drawing.Size(186, 34);
            this.btnOUT1_AERMOD.TabIndex = 5;
            this.btnOUT1_AERMOD.Text = "   <Ctrl O> AERMOD.OUT";
            this.btnOUT1_AERMOD.UseVisualStyleBackColor = true;
            this.btnOUT1_AERMOD.Click += new System.EventHandler(this.btnOUT1_AERMOD_Click);
            // 
            // btnINP_AERMOD
            // 
            this.btnINP_AERMOD.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnINP_AERMOD.Image = global::AERMOD.Properties.Resources.down;
            this.btnINP_AERMOD.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnINP_AERMOD.Location = new System.Drawing.Point(12, 170);
            this.btnINP_AERMOD.Name = "btnINP_AERMOD";
            this.btnINP_AERMOD.Size = new System.Drawing.Size(186, 34);
            this.btnINP_AERMOD.TabIndex = 4;
            this.btnINP_AERMOD.Text = " <Ctrl I> AERMOD.INP";
            this.btnINP_AERMOD.UseVisualStyleBackColor = true;
            this.btnINP_AERMOD.Click += new System.EventHandler(this.btnINP_AERMOD_Click);
            // 
            // lbLineSaidaAERMOD
            // 
            this.lbLineSaidaAERMOD.AutoSize = true;
            this.lbLineSaidaAERMOD.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lbLineSaidaAERMOD.Location = new System.Drawing.Point(56, 132);
            this.lbLineSaidaAERMOD.Name = "lbLineSaidaAERMOD";
            this.lbLineSaidaAERMOD.Size = new System.Drawing.Size(340, 13);
            this.lbLineSaidaAERMOD.TabIndex = 282;
            this.lbLineSaidaAERMOD.Text = "---------------------------------------------------------------------------------" +
    "------------------------------";
            // 
            // lbSaidaAERMOD
            // 
            this.lbSaidaAERMOD.AutoSize = true;
            this.lbSaidaAERMOD.BackColor = System.Drawing.Color.Transparent;
            this.lbSaidaAERMOD.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSaidaAERMOD.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lbSaidaAERMOD.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lbSaidaAERMOD.Location = new System.Drawing.Point(10, 129);
            this.lbSaidaAERMOD.Name = "lbSaidaAERMOD";
            this.lbSaidaAERMOD.Size = new System.Drawing.Size(47, 17);
            this.lbSaidaAERMOD.TabIndex = 281;
            this.lbSaidaAERMOD.Text = "Saídas";
            this.lbSaidaAERMOD.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbLineEntradaAERMOD
            // 
            this.lbLineEntradaAERMOD.AutoSize = true;
            this.lbLineEntradaAERMOD.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lbLineEntradaAERMOD.Location = new System.Drawing.Point(70, 10);
            this.lbLineEntradaAERMOD.Name = "lbLineEntradaAERMOD";
            this.lbLineEntradaAERMOD.Size = new System.Drawing.Size(325, 13);
            this.lbLineEntradaAERMOD.TabIndex = 280;
            this.lbLineEntradaAERMOD.Text = "---------------------------------------------------------------------------------" +
    "-------------------------";
            // 
            // lbEntradaAERMOD
            // 
            this.lbEntradaAERMOD.AutoSize = true;
            this.lbEntradaAERMOD.BackColor = System.Drawing.Color.Transparent;
            this.lbEntradaAERMOD.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEntradaAERMOD.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lbEntradaAERMOD.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lbEntradaAERMOD.Location = new System.Drawing.Point(10, 7);
            this.lbEntradaAERMOD.Name = "lbEntradaAERMOD";
            this.lbEntradaAERMOD.Size = new System.Drawing.Size(61, 17);
            this.lbEntradaAERMOD.TabIndex = 279;
            this.lbEntradaAERMOD.Text = "Entradas";
            this.lbEntradaAERMOD.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnExecutarAERMOD
            // 
            this.btnExecutarAERMOD.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExecutarAERMOD.Image = global::AERMOD.Properties.Resources.executar;
            this.btnExecutarAERMOD.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExecutarAERMOD.Location = new System.Drawing.Point(204, 34);
            this.btnExecutarAERMOD.Name = "btnExecutarAERMOD";
            this.btnExecutarAERMOD.Size = new System.Drawing.Size(186, 34);
            this.btnExecutarAERMOD.TabIndex = 2;
            this.btnExecutarAERMOD.Text = "<Enter> Executar";
            this.btnExecutarAERMOD.UseVisualStyleBackColor = true;
            this.btnExecutarAERMOD.Click += new System.EventHandler(this.btnExecutarAERMOD_Click);
            // 
            // tabPageParametros
            // 
            this.tabPageParametros.Controls.Add(this.btnCaminhoDataBase);
            this.tabPageParametros.Controls.Add(this.lbCaminhoDataBase);
            this.tabPageParametros.Controls.Add(this.tbxCaminhoDataBase);
            this.tabPageParametros.Controls.Add(this.btnAtualizacao);
            this.tabPageParametros.Controls.Add(this.lbLineAtualizacao);
            this.tabPageParametros.Controls.Add(this.lbAtualizacao);
            this.tabPageParametros.Controls.Add(this.lbLineDataBase);
            this.tabPageParametros.Controls.Add(this.lbDataBase);
            this.tabPageParametros.Location = new System.Drawing.Point(4, 22);
            this.tabPageParametros.Name = "tabPageParametros";
            this.tabPageParametros.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageParametros.Size = new System.Drawing.Size(403, 254);
            this.tabPageParametros.TabIndex = 3;
            this.tabPageParametros.Text = "PARÂMETROS";
            this.tabPageParametros.UseVisualStyleBackColor = true;
            // 
            // lbCaminhoDataBase
            // 
            this.lbCaminhoDataBase.AutoSize = true;
            this.lbCaminhoDataBase.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCaminhoDataBase.Location = new System.Drawing.Point(6, 34);
            this.lbCaminhoDataBase.Name = "lbCaminhoDataBase";
            this.lbCaminhoDataBase.Size = new System.Drawing.Size(47, 16);
            this.lbCaminhoDataBase.TabIndex = 275;
            this.lbCaminhoDataBase.Text = "Local";
            // 
            // tbxCaminhoDataBase
            // 
            this.tbxCaminhoDataBase.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbxCaminhoDataBase.Location = new System.Drawing.Point(9, 53);
            this.tbxCaminhoDataBase.MaxLength = 100;
            this.tbxCaminhoDataBase.Multiline = true;
            this.tbxCaminhoDataBase.Name = "tbxCaminhoDataBase";
            this.tbxCaminhoDataBase.Size = new System.Drawing.Size(383, 50);
            this.tbxCaminhoDataBase.TabIndex = 0;
            this.tbxCaminhoDataBase.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbxCaminhoDataBase_KeyDown);
            this.tbxCaminhoDataBase.Leave += new System.EventHandler(this.tbxCaminhoDataBase_Leave);
            // 
            // btnAtualizacao
            // 
            this.btnAtualizacao.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAtualizacao.Image = global::AERMOD.Properties.Resources.Atualizar2;
            this.btnAtualizacao.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAtualizacao.Location = new System.Drawing.Point(91, 178);
            this.btnAtualizacao.Name = "btnAtualizacao";
            this.btnAtualizacao.Size = new System.Drawing.Size(186, 34);
            this.btnAtualizacao.TabIndex = 1;
            this.btnAtualizacao.Text = "   <Ctrl A> Atualização";
            this.btnAtualizacao.UseVisualStyleBackColor = true;
            this.btnAtualizacao.Click += new System.EventHandler(this.btnAtualizacao_Click);
            // 
            // lbLineAtualizacao
            // 
            this.lbLineAtualizacao.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lbLineAtualizacao.Location = new System.Drawing.Point(133, 132);
            this.lbLineAtualizacao.Name = "lbLineAtualizacao";
            this.lbLineAtualizacao.Size = new System.Drawing.Size(264, 13);
            this.lbLineAtualizacao.TabIndex = 273;
            this.lbLineAtualizacao.Text = "---------------------------------------------------------------------------------" +
    "-----------------------------";
            // 
            // lbAtualizacao
            // 
            this.lbAtualizacao.AutoSize = true;
            this.lbAtualizacao.BackColor = System.Drawing.Color.Transparent;
            this.lbAtualizacao.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAtualizacao.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lbAtualizacao.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lbAtualizacao.Location = new System.Drawing.Point(7, 129);
            this.lbAtualizacao.Name = "lbAtualizacao";
            this.lbAtualizacao.Size = new System.Drawing.Size(125, 17);
            this.lbAtualizacao.TabIndex = 272;
            this.lbAtualizacao.Text = "Controle de Versão";
            this.lbAtualizacao.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbLineDataBase
            // 
            this.lbLineDataBase.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lbLineDataBase.Location = new System.Drawing.Point(114, 10);
            this.lbLineDataBase.Name = "lbLineDataBase";
            this.lbLineDataBase.Size = new System.Drawing.Size(283, 13);
            this.lbLineDataBase.TabIndex = 271;
            this.lbLineDataBase.Text = "---------------------------------------------------------------------------------" +
    "--------------------------";
            // 
            // lbDataBase
            // 
            this.lbDataBase.AutoSize = true;
            this.lbDataBase.BackColor = System.Drawing.Color.Transparent;
            this.lbDataBase.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDataBase.ForeColor = System.Drawing.Color.DodgerBlue;
            this.lbDataBase.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.lbDataBase.Location = new System.Drawing.Point(7, 7);
            this.lbDataBase.Name = "lbDataBase";
            this.lbDataBase.Size = new System.Drawing.Size(107, 17);
            this.lbDataBase.TabIndex = 270;
            this.lbDataBase.Text = "Banco de Dados";
            this.lbDataBase.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCaminhoDataBase
            // 
            this.btnCaminhoDataBase.BackColor = System.Drawing.Color.Transparent;
            this.btnCaminhoDataBase.Border = false;
            this.btnCaminhoDataBase.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCaminhoDataBase.Image = global::AERMOD.Properties.Resources.search_16x16;
            this.btnCaminhoDataBase.Location = new System.Drawing.Point(366, 25);
            this.btnCaminhoDataBase.Name = "btnCaminhoDataBase";
            this.btnCaminhoDataBase.OpacityColor2 = 110;
            this.btnCaminhoDataBase.Raio = 7F;
            this.btnCaminhoDataBase.Size = new System.Drawing.Size(26, 22);
            this.btnCaminhoDataBase.TabIndex = 1;
            this.btnCaminhoDataBase.TabStop = false;
            this.btnCaminhoDataBase.Visible = false;
            this.btnCaminhoDataBase.Click += new System.EventHandler(this.btnCaminhoDataBase_Click);
            // 
            // FrmAERMOD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(411, 302);
            this.Controls.Add(this.tabControlDados);
            this.Controls.Add(this.statusStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmAERMOD";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AERMOD Open";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmAERMOD_KeyDown);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tabControlDados.ResumeLayout(false);
            this.tabPageAERMAP.ResumeLayout(false);
            this.tabPageAERMAP.PerformLayout();
            this.tabPageAERMET.ResumeLayout(false);
            this.tabPageAERMET.PerformLayout();
            this.tabPageAERMOD.ResumeLayout(false);
            this.tabPageAERMOD.PerformLayout();
            this.tabPageParametros.ResumeLayout(false);
            this.tabPageParametros.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripSplitButton btnAjuda;
        private System.Windows.Forms.ToolStripSplitButton btnSair;
        private System.Windows.Forms.TabControl tabControlDados;
        private System.Windows.Forms.TabPage tabPageAERMAP;
        private System.Windows.Forms.TabPage tabPageAERMET;
        private System.Windows.Forms.TabPage tabPageAERMOD;
        private System.Windows.Forms.Button btnFontesEmissao;
        private System.Windows.Forms.Button btnTIF;
        private System.Windows.Forms.Button btnSOU_AERMAP;
        private System.Windows.Forms.Button btnROU_AERMAP;
        private System.Windows.Forms.Button btnOUT_AERMAP;
        private System.Windows.Forms.Button btnINP_AERMAP;
        private System.Windows.Forms.Button btnExecutarAERMAP;
        private System.Windows.Forms.Button btnFSL;
        private System.Windows.Forms.Button btnEditorSAM;
        private System.Windows.Forms.Button btnExecutarAERMET;
        private System.Windows.Forms.Button btnExecutarAERMOD;
        private System.Windows.Forms.Button btnDominio;
        private System.Windows.Forms.Label lbEntradaAERMAP;
        private System.Windows.Forms.Label lbLineEntradaAERMAP;
        private System.Windows.Forms.Label lbLineSaidaAERMAP;
        private System.Windows.Forms.Label lbSaidaAERMAP;
        private System.Windows.Forms.Label lbLineSaidaAERMET;
        private System.Windows.Forms.Label lbSaidaAERMET;
        private System.Windows.Forms.Button btnSFC_AERMET;
        private System.Windows.Forms.Button btnPFL_AERMET;
        private System.Windows.Forms.Button btnINP1_AERMET;
        private System.Windows.Forms.Button btnINP2_AERMET;
        private System.Windows.Forms.Label lbLineEntradaAERMET;
        private System.Windows.Forms.Label lbEntradaAERMET;
        private System.Windows.Forms.Button btnDefinicaoAERMET;
        private System.Windows.Forms.Label lbLineEntradaAERMOD;
        private System.Windows.Forms.Label lbEntradaAERMOD;
        private System.Windows.Forms.Label lbLineSaidaAERMOD;
        private System.Windows.Forms.Label lbSaidaAERMOD;
        private System.Windows.Forms.Button btnPLT_AERMOD;
        private System.Windows.Forms.Button btnOUT2_AERMOD;
        private System.Windows.Forms.Button btnOUT1_AERMOD;
        private System.Windows.Forms.Button btnINP_AERMOD;
        private System.Windows.Forms.Button btnDefinicaoAERMOD;
        private System.Windows.Forms.Button btnParametrosAERMOD;
        private System.Windows.Forms.ToolStripSplitButton btnMapa;
        private System.Windows.Forms.ToolStripSplitButton btnBackup;
        private System.Windows.Forms.Label lbVersaoAERMAP;
        private System.Windows.Forms.Label lbVersaoAERMET;
        private System.Windows.Forms.Label lbVersaoAERMOD;
        private System.Windows.Forms.TabPage tabPageParametros;
        private System.Windows.Forms.Label lbLineAtualizacao;
        private System.Windows.Forms.Label lbAtualizacao;
        private System.Windows.Forms.Label lbLineDataBase;
        private System.Windows.Forms.Label lbDataBase;
        private System.Windows.Forms.Button btnAtualizacao;
        private System.Windows.Forms.Label lbCaminhoDataBase;
        private System.Windows.Forms.TextBox tbxCaminhoDataBase;
        private LIB.Componentes.Botao.ButtonLIB btnCaminhoDataBase;
    }
}