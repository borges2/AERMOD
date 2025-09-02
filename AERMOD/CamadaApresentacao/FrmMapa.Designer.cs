namespace AERMOD.CamadaApresentacao
{
    partial class FrmMapa
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
            this.gmap = new GMap.NET.WindowsForms.GMapControl();
            this.lbCoordenadas = new System.Windows.Forms.Label();
            this.lbDominio = new System.Windows.Forms.Label();
            this.lbGrid = new System.Windows.Forms.Label();
            this.lbConcentracao = new System.Windows.Forms.Label();
            this.lbEscala1 = new System.Windows.Forms.Label();
            this.lbEscala2 = new System.Windows.Forms.Label();
            this.lbEscala3 = new System.Windows.Forms.Label();
            this.lbEscala4 = new System.Windows.Forms.Label();
            this.panelLegendaAERMOD = new System.Windows.Forms.Panel();
            this.panelLegendaAERMAP = new System.Windows.Forms.Panel();
            this.btnBorda = new System.Windows.Forms.Button();
            this.btnFonte = new System.Windows.Forms.Button();
            this.btnDistancia = new System.Windows.Forms.Button();
            this.btnTipoMapa = new System.Windows.Forms.Button();
            this.btnZoomOut = new System.Windows.Forms.Button();
            this.btnZoomIn = new System.Windows.Forms.Button();
            this.btnMenor = new System.Windows.Forms.Button();
            this.btnMaior = new System.Windows.Forms.Button();
            this.panelLegendaAERMOD.SuspendLayout();
            this.panelLegendaAERMAP.SuspendLayout();
            this.SuspendLayout();
            // 
            // gmap
            // 
            this.gmap.Bearing = 0F;
            this.gmap.CanDragMap = true;
            this.gmap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gmap.EmptyTileColor = System.Drawing.Color.Navy;
            this.gmap.GrayScaleMode = false;
            this.gmap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gmap.LevelsKeepInMemory = 5;
            this.gmap.Location = new System.Drawing.Point(0, 0);
            this.gmap.MarkersEnabled = true;
            this.gmap.MaxZoom = 21;
            this.gmap.MinZoom = 0;
            this.gmap.MouseWheelZoomEnabled = true;
            this.gmap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gmap.Name = "gmap";
            this.gmap.NegativeMode = false;
            this.gmap.PolygonsEnabled = true;
            this.gmap.RetryLoadTile = 0;
            this.gmap.RoutesEnabled = true;
            this.gmap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gmap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gmap.ShowTileGridLines = false;
            this.gmap.Size = new System.Drawing.Size(724, 461);
            this.gmap.TabIndex = 0;
            this.gmap.Zoom = 0D;
            this.gmap.OnMapZoomChanged += new GMap.NET.MapZoomChanged(this.gmap_OnMapZoomChanged);
            this.gmap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gmap_MouseMove);
            // 
            // lbCoordenadas
            // 
            this.lbCoordenadas.AutoSize = true;
            this.lbCoordenadas.BackColor = System.Drawing.Color.Transparent;
            this.lbCoordenadas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbCoordenadas.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lbCoordenadas.Location = new System.Drawing.Point(0, 446);
            this.lbCoordenadas.Name = "lbCoordenadas";
            this.lbCoordenadas.Size = new System.Drawing.Size(147, 15);
            this.lbCoordenadas.TabIndex = 6;
            this.lbCoordenadas.Text = "Lat -23,5555 Long -25,23555";
            // 
            // lbDominio
            // 
            this.lbDominio.BackColor = System.Drawing.Color.Red;
            this.lbDominio.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbDominio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDominio.ForeColor = System.Drawing.Color.White;
            this.lbDominio.Location = new System.Drawing.Point(2, 2);
            this.lbDominio.Margin = new System.Windows.Forms.Padding(3);
            this.lbDominio.Name = "lbDominio";
            this.lbDominio.Size = new System.Drawing.Size(141, 16);
            this.lbDominio.TabIndex = 8;
            this.lbDominio.Text = "Domínio de modelagem";
            this.lbDominio.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbGrid
            // 
            this.lbGrid.BackColor = System.Drawing.Color.Green;
            this.lbGrid.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbGrid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGrid.ForeColor = System.Drawing.Color.White;
            this.lbGrid.Location = new System.Drawing.Point(144, 2);
            this.lbGrid.Margin = new System.Windows.Forms.Padding(1, 0, 3, 0);
            this.lbGrid.Name = "lbGrid";
            this.lbGrid.Size = new System.Drawing.Size(141, 16);
            this.lbGrid.TabIndex = 8;
            this.lbGrid.Text = "Grade de modelagem";
            this.lbGrid.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lbConcentracao
            // 
            this.lbConcentracao.AutoSize = true;
            this.lbConcentracao.BackColor = System.Drawing.Color.Transparent;
            this.lbConcentracao.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbConcentracao.Location = new System.Drawing.Point(117, 20);
            this.lbConcentracao.Name = "lbConcentracao";
            this.lbConcentracao.Size = new System.Drawing.Size(74, 13);
            this.lbConcentracao.TabIndex = 9;
            this.lbConcentracao.Text = "Concentração";
            this.lbConcentracao.Visible = false;
            // 
            // lbEscala1
            // 
            this.lbEscala1.BackColor = System.Drawing.Color.Green;
            this.lbEscala1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbEscala1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbEscala1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEscala1.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.lbEscala1.Location = new System.Drawing.Point(2, 2);
            this.lbEscala1.Name = "lbEscala1";
            this.lbEscala1.Size = new System.Drawing.Size(130, 16);
            this.lbEscala1.TabIndex = 11;
            this.lbEscala1.Text = "11,260 |--- 11,890";
            this.lbEscala1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbEscala1.Click += new System.EventHandler(this.lbEscala1_Click);
            // 
            // lbEscala2
            // 
            this.lbEscala2.BackColor = System.Drawing.Color.Yellow;
            this.lbEscala2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbEscala2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbEscala2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEscala2.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.lbEscala2.Location = new System.Drawing.Point(133, 2);
            this.lbEscala2.Name = "lbEscala2";
            this.lbEscala2.Size = new System.Drawing.Size(130, 16);
            this.lbEscala2.TabIndex = 12;
            this.lbEscala2.Text = "11,890 |--- 11,890";
            this.lbEscala2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbEscala2.Click += new System.EventHandler(this.lbEscala2_Click);
            // 
            // lbEscala3
            // 
            this.lbEscala3.BackColor = System.Drawing.Color.Red;
            this.lbEscala3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbEscala3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbEscala3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEscala3.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.lbEscala3.Location = new System.Drawing.Point(264, 2);
            this.lbEscala3.Name = "lbEscala3";
            this.lbEscala3.Size = new System.Drawing.Size(130, 16);
            this.lbEscala3.TabIndex = 13;
            this.lbEscala3.Text = "11,890 |--- 22,270";
            this.lbEscala3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbEscala3.Click += new System.EventHandler(this.lbEscala3_Click);
            // 
            // lbEscala4
            // 
            this.lbEscala4.BackColor = System.Drawing.Color.Black;
            this.lbEscala4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbEscala4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbEscala4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEscala4.ForeColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.lbEscala4.Location = new System.Drawing.Point(395, 2);
            this.lbEscala4.Name = "lbEscala4";
            this.lbEscala4.Size = new System.Drawing.Size(130, 16);
            this.lbEscala4.TabIndex = 14;
            this.lbEscala4.Text = "22,270 |---| 23,990";
            this.lbEscala4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbEscala4.Click += new System.EventHandler(this.lbEscala4_Click);
            // 
            // panelLegendaAERMOD
            // 
            this.panelLegendaAERMOD.Controls.Add(this.lbEscala1);
            this.panelLegendaAERMOD.Controls.Add(this.lbEscala2);
            this.panelLegendaAERMOD.Controls.Add(this.lbEscala3);
            this.panelLegendaAERMOD.Controls.Add(this.lbEscala4);
            this.panelLegendaAERMOD.Location = new System.Drawing.Point(116, 0);
            this.panelLegendaAERMOD.Name = "panelLegendaAERMOD";
            this.panelLegendaAERMOD.Size = new System.Drawing.Size(527, 20);
            this.panelLegendaAERMOD.TabIndex = 20;
            this.panelLegendaAERMOD.Visible = false;
            // 
            // panelLegendaAERMAP
            // 
            this.panelLegendaAERMAP.Controls.Add(this.lbDominio);
            this.panelLegendaAERMAP.Controls.Add(this.lbGrid);
            this.panelLegendaAERMAP.Location = new System.Drawing.Point(116, 37);
            this.panelLegendaAERMAP.Name = "panelLegendaAERMAP";
            this.panelLegendaAERMAP.Size = new System.Drawing.Size(287, 20);
            this.panelLegendaAERMAP.TabIndex = 21;
            this.panelLegendaAERMAP.Visible = false;
            // 
            // btnBorda
            // 
            this.btnBorda.Image = global::AERMOD.Properties.Resources.menu;
            this.btnBorda.Location = new System.Drawing.Point(0, 136);
            this.btnBorda.Name = "btnBorda";
            this.btnBorda.Size = new System.Drawing.Size(40, 35);
            this.btnBorda.TabIndex = 23;
            this.btnBorda.UseVisualStyleBackColor = true;
            this.btnBorda.Visible = false;
            this.btnBorda.Click += new System.EventHandler(this.btnBorda_Click);
            // 
            // btnFonte
            // 
            this.btnFonte.Image = global::AERMOD.Properties.Resources.AERMOD16;
            this.btnFonte.Location = new System.Drawing.Point(0, 102);
            this.btnFonte.Name = "btnFonte";
            this.btnFonte.Size = new System.Drawing.Size(40, 35);
            this.btnFonte.TabIndex = 22;
            this.btnFonte.UseVisualStyleBackColor = true;
            this.btnFonte.Visible = false;
            this.btnFonte.Click += new System.EventHandler(this.btnFonte_Click);
            // 
            // btnDistancia
            // 
            this.btnDistancia.Image = global::AERMOD.Properties.Resources.distancia;
            this.btnDistancia.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnDistancia.Location = new System.Drawing.Point(39, 0);
            this.btnDistancia.Name = "btnDistancia";
            this.btnDistancia.Size = new System.Drawing.Size(78, 35);
            this.btnDistancia.TabIndex = 5;
            this.btnDistancia.Text = "2000 km";
            this.btnDistancia.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnDistancia.UseVisualStyleBackColor = true;
            // 
            // btnTipoMapa
            // 
            this.btnTipoMapa.Image = global::AERMOD.Properties.Resources.satellite;
            this.btnTipoMapa.Location = new System.Drawing.Point(0, 68);
            this.btnTipoMapa.Name = "btnTipoMapa";
            this.btnTipoMapa.Size = new System.Drawing.Size(40, 35);
            this.btnTipoMapa.TabIndex = 3;
            this.btnTipoMapa.UseVisualStyleBackColor = true;
            this.btnTipoMapa.Click += new System.EventHandler(this.btnTipoMapa_Click);
            // 
            // btnZoomOut
            // 
            this.btnZoomOut.Image = global::AERMOD.Properties.Resources.ZoomOutButton;
            this.btnZoomOut.Location = new System.Drawing.Point(0, 34);
            this.btnZoomOut.Name = "btnZoomOut";
            this.btnZoomOut.Size = new System.Drawing.Size(40, 35);
            this.btnZoomOut.TabIndex = 2;
            this.btnZoomOut.UseVisualStyleBackColor = true;
            this.btnZoomOut.Click += new System.EventHandler(this.btnZoomOut_Click);
            // 
            // btnZoomIn
            // 
            this.btnZoomIn.Image = global::AERMOD.Properties.Resources.ZoomInButton;
            this.btnZoomIn.Location = new System.Drawing.Point(0, 0);
            this.btnZoomIn.Name = "btnZoomIn";
            this.btnZoomIn.Size = new System.Drawing.Size(40, 35);
            this.btnZoomIn.TabIndex = 1;
            this.btnZoomIn.UseVisualStyleBackColor = true;
            this.btnZoomIn.Click += new System.EventHandler(this.btnZoomIn_Click);
            // 
            // btnMenor
            // 
            this.btnMenor.Image = global::AERMOD.Properties.Resources.down_azul;
            this.btnMenor.Location = new System.Drawing.Point(0, 170);
            this.btnMenor.Name = "btnMenor";
            this.btnMenor.Size = new System.Drawing.Size(40, 35);
            this.btnMenor.TabIndex = 24;
            this.btnMenor.UseVisualStyleBackColor = true;
            this.btnMenor.Visible = false;
            this.btnMenor.Click += new System.EventHandler(this.btnMenor_Click);
            // 
            // btnMaior
            // 
            this.btnMaior.Image = global::AERMOD.Properties.Resources.up_azul;
            this.btnMaior.Location = new System.Drawing.Point(0, 204);
            this.btnMaior.Name = "btnMaior";
            this.btnMaior.Size = new System.Drawing.Size(40, 35);
            this.btnMaior.TabIndex = 25;
            this.btnMaior.UseVisualStyleBackColor = true;
            this.btnMaior.Visible = false;
            this.btnMaior.Click += new System.EventHandler(this.btnMaior_Click);
            // 
            // FrmMapa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(724, 461);
            this.Controls.Add(this.btnMaior);
            this.Controls.Add(this.btnMenor);
            this.Controls.Add(this.btnBorda);
            this.Controls.Add(this.btnFonte);
            this.Controls.Add(this.panelLegendaAERMAP);
            this.Controls.Add(this.lbConcentracao);
            this.Controls.Add(this.panelLegendaAERMOD);
            this.Controls.Add(this.lbCoordenadas);
            this.Controls.Add(this.btnDistancia);
            this.Controls.Add(this.btnTipoMapa);
            this.Controls.Add(this.btnZoomOut);
            this.Controls.Add(this.btnZoomIn);
            this.Controls.Add(this.gmap);
            this.KeyPreview = true;
            this.MinimizeBox = false;
            this.Name = "FrmMapa";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Visualizar mapa";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmMapaAERMAP_KeyDown);
            this.panelLegendaAERMOD.ResumeLayout(false);
            this.panelLegendaAERMAP.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl gmap;
        private System.Windows.Forms.Button btnZoomIn;
        private System.Windows.Forms.Button btnZoomOut;
        private System.Windows.Forms.Button btnTipoMapa;
        private System.Windows.Forms.Button btnDistancia;
        private System.Windows.Forms.Label lbCoordenadas;
        private System.Windows.Forms.Label lbDominio;
        private System.Windows.Forms.Label lbGrid;
        private System.Windows.Forms.Label lbConcentracao;
        private System.Windows.Forms.Label lbEscala1;
        private System.Windows.Forms.Label lbEscala2;
        private System.Windows.Forms.Label lbEscala3;
        private System.Windows.Forms.Label lbEscala4;
        private System.Windows.Forms.Panel panelLegendaAERMOD;
        private System.Windows.Forms.Panel panelLegendaAERMAP;
        private System.Windows.Forms.Button btnFonte;
        private System.Windows.Forms.Button btnBorda;
        private System.Windows.Forms.Button btnMenor;
        private System.Windows.Forms.Button btnMaior;
    }
}