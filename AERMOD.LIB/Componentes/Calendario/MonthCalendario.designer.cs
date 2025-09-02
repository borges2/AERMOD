namespace Netsof.LIB.Componentes.Calendario
{
    partial class MonthCalendario
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonthCalendario));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelCabecalho = new System.Windows.Forms.Panel();
            this.btnMesEsquerdo = new Netsof.LIB.Componentes.Calendario.RepeatButton();
            this.btnAnoDireito = new Netsof.LIB.Componentes.Calendario.RepeatButton();
            this.lbAno = new System.Windows.Forms.Label();
            this.btnAnoEsquerdo = new Netsof.LIB.Componentes.Calendario.RepeatButton();
            this.btnMesDireito = new Netsof.LIB.Componentes.Calendario.RepeatButton();
            this.lbMes = new System.Windows.Forms.Label();
            this.dgvDias = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelRodape = new System.Windows.Forms.Panel();
            this.lbHoje = new System.Windows.Forms.Label();
            this.panelCabecalho.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDias)).BeginInit();
            this.panelRodape.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCabecalho
            // 
            this.panelCabecalho.BackColor = System.Drawing.Color.Gainsboro;
            this.panelCabecalho.Controls.Add(this.btnMesEsquerdo);
            this.panelCabecalho.Controls.Add(this.btnAnoDireito);
            this.panelCabecalho.Controls.Add(this.lbAno);
            this.panelCabecalho.Controls.Add(this.btnAnoEsquerdo);
            this.panelCabecalho.Controls.Add(this.btnMesDireito);
            this.panelCabecalho.Controls.Add(this.lbMes);
            this.panelCabecalho.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCabecalho.Location = new System.Drawing.Point(1, 1);
            this.panelCabecalho.Name = "panelCabecalho";
            this.panelCabecalho.Size = new System.Drawing.Size(225, 29);
            this.panelCabecalho.TabIndex = 1;
            this.panelCabecalho.Click += new System.EventHandler(this.panelCabecalho_Click);
            // 
            // btnMesEsquerdo
            // 
            this.btnMesEsquerdo.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnMesEsquerdo.BackgroundImage")));
            this.btnMesEsquerdo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMesEsquerdo.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnMesEsquerdo.FlatAppearance.BorderSize = 0;
            this.btnMesEsquerdo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMesEsquerdo.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMesEsquerdo.Location = new System.Drawing.Point(7, 9);
            this.btnMesEsquerdo.Name = "btnMesEsquerdo";
            this.btnMesEsquerdo.RepeatInterval = 350;
            this.btnMesEsquerdo.Size = new System.Drawing.Size(10, 10);
            this.btnMesEsquerdo.TabIndex = 1;
            this.btnMesEsquerdo.TabStop = false;
            this.btnMesEsquerdo.UseVisualStyleBackColor = true;
            this.btnMesEsquerdo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnMesEsquerdo_MouseDown);
            // 
            // btnAnoDireito
            // 
            this.btnAnoDireito.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnoDireito.BackgroundImage = global::AERMOD.LIB.Properties.Resources.ladoDireito;
            this.btnAnoDireito.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAnoDireito.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnAnoDireito.FlatAppearance.BorderSize = 0;
            this.btnAnoDireito.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnoDireito.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnoDireito.Location = new System.Drawing.Point(207, 9);
            this.btnAnoDireito.Name = "btnAnoDireito";
            this.btnAnoDireito.RepeatInterval = 350;
            this.btnAnoDireito.Size = new System.Drawing.Size(10, 10);
            this.btnAnoDireito.TabIndex = 4;
            this.btnAnoDireito.TabStop = false;
            this.btnAnoDireito.UseVisualStyleBackColor = true;
            this.btnAnoDireito.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAnoDireito_MouseDown);
            // 
            // lbAno
            // 
            this.lbAno.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbAno.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAno.ForeColor = System.Drawing.Color.Black;
            this.lbAno.Location = new System.Drawing.Point(160, 4);
            this.lbAno.Name = "lbAno";
            this.lbAno.Size = new System.Drawing.Size(43, 21);
            this.lbAno.TabIndex = 4;
            this.lbAno.Text = "2017";
            this.lbAno.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbAno.Click += new System.EventHandler(this.lbAno_Click);
            // 
            // btnAnoEsquerdo
            // 
            this.btnAnoEsquerdo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAnoEsquerdo.BackgroundImage = global::AERMOD.LIB.Properties.Resources.ladoEsquerdo;
            this.btnAnoEsquerdo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAnoEsquerdo.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnAnoEsquerdo.FlatAppearance.BorderSize = 0;
            this.btnAnoEsquerdo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAnoEsquerdo.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAnoEsquerdo.Location = new System.Drawing.Point(143, 9);
            this.btnAnoEsquerdo.Name = "btnAnoEsquerdo";
            this.btnAnoEsquerdo.RepeatInterval = 350;
            this.btnAnoEsquerdo.Size = new System.Drawing.Size(10, 10);
            this.btnAnoEsquerdo.TabIndex = 3;
            this.btnAnoEsquerdo.TabStop = false;
            this.btnAnoEsquerdo.UseVisualStyleBackColor = true;
            this.btnAnoEsquerdo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAnoEsquerdo_MouseDown);
            // 
            // btnMesDireito
            // 
            this.btnMesDireito.BackgroundImage = global::AERMOD.LIB.Properties.Resources.ladoDireito;
            this.btnMesDireito.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnMesDireito.FlatAppearance.BorderColor = System.Drawing.Color.DodgerBlue;
            this.btnMesDireito.FlatAppearance.BorderSize = 0;
            this.btnMesDireito.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMesDireito.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMesDireito.Location = new System.Drawing.Point(99, 9);
            this.btnMesDireito.Name = "btnMesDireito";
            this.btnMesDireito.RepeatInterval = 350;
            this.btnMesDireito.Size = new System.Drawing.Size(10, 10);
            this.btnMesDireito.TabIndex = 2;
            this.btnMesDireito.TabStop = false;
            this.btnMesDireito.UseVisualStyleBackColor = true;
            this.btnMesDireito.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnMesDireito_MouseDown);
            // 
            // lbMes
            // 
            this.lbMes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMes.ForeColor = System.Drawing.Color.Black;
            this.lbMes.Location = new System.Drawing.Point(21, 4);
            this.lbMes.Name = "lbMes";
            this.lbMes.Size = new System.Drawing.Size(71, 21);
            this.lbMes.TabIndex = 1;
            this.lbMes.Text = "Janeiro";
            this.lbMes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbMes.Click += new System.EventHandler(this.lbMes_Click);
            // 
            // dgvDias
            // 
            this.dgvDias.AllowUserToAddRows = false;
            this.dgvDias.AllowUserToDeleteRows = false;
            this.dgvDias.AllowUserToResizeColumns = false;
            this.dgvDias.AllowUserToResizeRows = false;
            this.dgvDias.BackgroundColor = System.Drawing.Color.White;
            this.dgvDias.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDias.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDias.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvDias.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.Column5,
            this.Column6,
            this.Column7});
            this.dgvDias.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDias.Location = new System.Drawing.Point(1, 30);
            this.dgvDias.MultiSelect = false;
            this.dgvDias.Name = "dgvDias";
            this.dgvDias.ReadOnly = true;
            this.dgvDias.RowHeadersVisible = false;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDias.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvDias.RowTemplate.DefaultCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDias.RowTemplate.Height = 18;
            this.dgvDias.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDias.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dgvDias.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgvDias.ShowCellToolTips = false;
            this.dgvDias.Size = new System.Drawing.Size(225, 113);
            this.dgvDias.StandardTab = true;
            this.dgvDias.TabIndex = 0;
            this.dgvDias.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDias_CellDoubleClick);
            this.dgvDias.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDias_CellFormatting);
            this.dgvDias.SelectionChanged += new System.EventHandler(this.dgvDias_SelectionChanged);
            this.dgvDias.SizeChanged += new System.EventHandler(this.dgvDias_SizeChanged);
            this.dgvDias.Click += new System.EventHandler(this.dgvDias_Click);
            this.dgvDias.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgvDias_KeyDown);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column1.HeaderText = "Dom";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column2.HeaderText = "Seg";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column3.HeaderText = "Ter";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column4.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column4.HeaderText = "Qua";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column5.DefaultCellStyle = dataGridViewCellStyle6;
            this.Column5.HeaderText = "Qui";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column6.DefaultCellStyle = dataGridViewCellStyle7;
            this.Column6.HeaderText = "Sex";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column7
            // 
            this.Column7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Column7.DefaultCellStyle = dataGridViewCellStyle8;
            this.Column7.HeaderText = "Sáb";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // panelRodape
            // 
            this.panelRodape.BackColor = System.Drawing.Color.Gainsboro;
            this.panelRodape.Controls.Add(this.lbHoje);
            this.panelRodape.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelRodape.Location = new System.Drawing.Point(1, 143);
            this.panelRodape.Name = "panelRodape";
            this.panelRodape.Size = new System.Drawing.Size(225, 18);
            this.panelRodape.TabIndex = 2;
            this.panelRodape.Click += new System.EventHandler(this.panelRodape_Click);
            // 
            // lbHoje
            // 
            this.lbHoje.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbHoje.ForeColor = System.Drawing.Color.Black;
            this.lbHoje.Location = new System.Drawing.Point(61, 0);
            this.lbHoje.Name = "lbHoje";
            this.lbHoje.Size = new System.Drawing.Size(106, 17);
            this.lbHoje.TabIndex = 2;
            this.lbHoje.Text = "Hoje: 00/00/0000";
            this.lbHoje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbHoje.Click += new System.EventHandler(this.lbHoje_Click);
            this.lbHoje.DoubleClick += new System.EventHandler(this.lbHoje_DoubleClick);
            // 
            // MonthCalendario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvDias);
            this.Controls.Add(this.panelCabecalho);
            this.Controls.Add(this.panelRodape);
            this.Name = "MonthCalendario";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Size = new System.Drawing.Size(227, 162);
            this.panelCabecalho.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDias)).EndInit();
            this.panelRodape.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelCabecalho;
        private System.Windows.Forms.DataGridView dgvDias;
        private System.Windows.Forms.Label lbMes;
        private RepeatButton btnMesDireito;
        private RepeatButton btnAnoDireito;
        private System.Windows.Forms.Label lbAno;
        private RepeatButton btnAnoEsquerdo;
        private RepeatButton btnMesEsquerdo;
        private System.Windows.Forms.Panel panelRodape;
        private System.Windows.Forms.Label lbHoje;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
    }
}
