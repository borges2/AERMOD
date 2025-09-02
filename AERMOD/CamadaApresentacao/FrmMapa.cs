using AERMOD.LIB.Desenvolvimento;
using AERMOD.LIB.Forms;
using CamadaLogicaNegocios;
using CoordinateSharp;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Microsoft.VisualStudio.OLE.Interop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Device.Location;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AERMOD.CamadaApresentacao
{
    public partial class FrmMapa : Form
    {
        #region Instâncias e Propriedades

        /// <summary>
        /// Utilizado para help.
        /// </summary>
        ClsHelp clsHelp = null;

        /// <summary>
        /// Utilizado para help.
        /// </summary>
        ClsHelp classeHelp
        {
            get
            {
                if (clsHelp == null)
                {
                    clsHelp = new ClsHelp(Base.ConfiguracaoRede);
                }

                return clsHelp;
            }
        }

        ClsFonteAERMAP clsFonteAERMAP = null;

        /// <summary>
        /// Classe de negócios ClsFonteAERMAP.
        /// </summary>
        ClsFonteAERMAP classeFonteAERMAP
        {
            get
            {
                if (clsFonteAERMAP == null)
                {
                    clsFonteAERMAP = new ClsFonteAERMAP(Base.ConfiguracaoRede);
                }

                return clsFonteAERMAP;
            }
        }

        /// <summary>
        /// Distância de visualização conforme zoom atual.
        /// </summary>
        Dictionary<double, string> lstDistancia;

        ClsAERMAP_Dominio clsDominioModelagem = null;

        /// <summary>
        /// Get classe de negócios ClsDominioModelagem
        /// </summary>
        ClsAERMAP_Dominio classeDominio
        {
            get
            {
                if (clsDominioModelagem == null)
                {
                    clsDominioModelagem = new ClsAERMAP_Dominio(Base.ConfiguracaoRede);
                }

                return clsDominioModelagem;
            }
        }

        ClsAERMET clsAERMET = null;

        /// <summary>
        /// Get classe de negócios ClsAERMAP
        /// </summary>
        ClsAERMET classeAERMET
        {
            get
            {
                if (clsAERMET == null)
                {
                    clsAERMET = new ClsAERMET(Base.ConfiguracaoRede);
                }

                return clsAERMET;
            }
        }

        ClsAERMOD clsAERMOD = null;

        /// <summary>
        /// Get classe de negócios ClsAERMAP
        /// </summary>
        ClsAERMOD classeAERMOD
        {
            get
            {
                if (clsAERMOD == null)
                {
                    clsAERMOD = new ClsAERMOD(Base.ConfiguracaoRede);
                }

                return clsAERMOD;
            }
        }

        /// <summary>
        /// Tipo do processamento AERMAP/AERMET/AERMOD.
        /// </summary>
        int tipoProcesso;

        GMapOverlay primeiraEscala = new GMapOverlay("primeiraEscala");
        GMapOverlay segundaEscala = new GMapOverlay("segundaEscala");
        GMapOverlay terceiraEscala = new GMapOverlay("terceiraEscala");
        GMapOverlay quartaEscala = new GMapOverlay("quartaEscala");

        GMapOverlay camadaFonte = new GMapOverlay("camadaFonte");
        GMapOverlay camadaBorda = new GMapOverlay("camadaBorda");
        GMapOverlay camadaMenor = new GMapOverlay("camadaMenor");
        GMapOverlay camadaMenorDestaca = new GMapOverlay("camadaMenorDestaca");
        GMapOverlay camadaMaior = new GMapOverlay("camadaMaior");
        GMapOverlay camadaMaiorDestaca = new GMapOverlay("camadaMaiorDestaca");
        List<Tuple<decimal, double, double>> lstConcentracao = null;

        List<decimal> lstEscala1 = null;
        List<decimal> lstEscala2 = null;
        List<decimal> lstEscala3 = null;
        List<decimal> lstEscala4 = null;

        int ZONA_UTM;
        string ZONA_UTM_LETRA;
        DataTable dtFontes = new DataTable();

        #endregion

        #region Construtor

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="tipoProcesso">0 = AERMAP / 1 = AERMOD</param>
        public FrmMapa(int tipoProcesso)
        {
            InitializeComponent();

            this.Icon = Properties.Resources.gmap.ConvertImageToIcon();

            this.tipoProcesso = tipoProcesso;
        }

        #endregion

        #region Eventos FrmMapaAERMAP

        protected override void OnShown(EventArgs e)
        {
            CarregarDistancia();

            switch (tipoProcesso)
            {
                case 0:
                    CarregarMapaAERMAP();
                    break;
                case 1:
                    CarregarMapaAERMOD();
                    break;
            }            

            base.OnShown(e);
        }

        private void FrmMapaAERMAP_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
            }
        }

        #endregion        

        #region Eventos btnZoomIn

        private void btnZoomIn_Click(object sender, EventArgs e)
        {
            gmap.Zoom++;
        }

        #endregion

        #region Eventos btnZoomOut

        private void btnZoomOut_Click(object sender, EventArgs e)
        {
            gmap.Zoom--;
        }

        #endregion

        #region Eventos btnTipoMapa

        private void btnTipoMapa_Click(object sender, EventArgs e)
        {
            if (gmap.MapProvider == GMap.NET.MapProviders.GoogleMapProvider.Instance)
            {
                btnTipoMapa.Image = AERMOD.Properties.Resources.terreno;
                gmap.MapProvider = GMap.NET.MapProviders.GoogleSatelliteMapProvider.Instance;
            }
            else if (gmap.MapProvider == GMap.NET.MapProviders.GoogleSatelliteMapProvider.Instance)
            {
                btnTipoMapa.Image = AERMOD.Properties.Resources.marcador;
                gmap.MapProvider = GMap.NET.MapProviders.GoogleTerrainMapProvider.Instance;
            }
            else if (gmap.MapProvider == GMap.NET.MapProviders.GoogleTerrainMapProvider.Instance)
            {
                btnTipoMapa.Image = AERMOD.Properties.Resources.satellite;
                gmap.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            }            
        }

        #endregion

        #region Eventos gmap

        private void gmap_OnMapZoomChanged()
        {
            btnDistancia.Text = lstDistancia[gmap.Zoom];

            if (gmap.Zoom == gmap.MinZoom)
            {
                btnZoomOut.Enabled = false;
            }
            else
            {
                btnZoomOut.Enabled = true;
            }
            
            if (gmap.Zoom == gmap.MaxZoom)
            {
                btnZoomIn.Enabled = false;
            }
            else
            {
                btnZoomIn.Enabled = true;
            }
        }

        private void gmap_MouseMove(object sender, MouseEventArgs e)
        {
            double lat = gmap.FromLocalToLatLng(e.X, e.Y).Lat;
            double lon = gmap.FromLocalToLatLng(e.X, e.Y).Lng;

            lbCoordenadas.Text = $"Lat {lat} Long {lon}";
        }

        #endregion

        #region Eventos lbEscala1

        private void lbEscala1_Click(object sender, EventArgs e)
        {
            if (gmap.Overlays.Any(I => I.Id == "primeiraEscala"))
            {
                gmap.Overlays.Remove(primeiraEscala);
                gmap.Invalidate();
                gmap.Update();
            }
            else
            {
                CriarPrimeiraEscala();
            }
        }

        #endregion

        #region Eventos lbEscala2

        private void lbEscala2_Click(object sender, EventArgs e)
        {
            if (gmap.Overlays.Any(I => I.Id == "segundaEscala"))
            {
                gmap.Overlays.Remove(segundaEscala);
                gmap.Invalidate();
                gmap.Update();
            }
            else
            {
                CriarSegundaEscala();
            }
        }

        #endregion

        #region Eventos lbEscala3

        private void lbEscala3_Click(object sender, EventArgs e)
        {
            if (gmap.Overlays.Any(I => I.Id == "terceiraEscala"))
            {
                gmap.Overlays.Remove(terceiraEscala);
                gmap.Invalidate();
                gmap.Update();
            }
            else
            {
                CriarTerceiraEscala();
            }
        }

        #endregion

        #region Eventos lbEscala4

        private void lbEscala4_Click(object sender, EventArgs e)
        {
            if (gmap.Overlays.Any(I => I.Id == "quartaEscala"))
            {
                gmap.Overlays.Remove(quartaEscala);
                gmap.Invalidate();
                gmap.Update();
            }
            else
            {
                CriarQuartaEscala();
            }
        }

        #endregion

        #region Eventos btnFonte

        private void btnFonte_Click(object sender, EventArgs e)
        {
            if (gmap.Overlays.Any(I => I.Id == "camadaFonte"))
            {
                gmap.Overlays.Remove(camadaFonte);
                gmap.Invalidate();
                gmap.Update();
            }
            else
            {
                CriarCamadaFonte();
            }
        }

        #endregion

        #region Eventos btnBorda

        private void btnBorda_Click(object sender, EventArgs e)
        {
            if (gmap.Overlays.Any(I => I.Id == "camadaBorda"))
            {
                for (int i = gmap.Overlays.Count - 1; i >= 0; i--)
                {
                    if (gmap.Overlays[i].Id == "camadaBorda")
                    {
                        gmap.Overlays.Remove(gmap.Overlays[i]);
                        gmap.Invalidate();
                        gmap.Update();
                    }
                }                             
            }
            else
            {
                CriarCamadaBorda();
            }
        }

        #endregion

        #region Eventos btnMenor

        private void btnMenor_Click(object sender, EventArgs e)
        {
            if (gmap.Overlays.Any(I => I.Id == "camadaMenor"))
            {
                gmap.Overlays.Remove(camadaMenor);
                gmap.Invalidate();
                gmap.Update();

                CriarCamadaMenor(true);
            }
            else if (gmap.Overlays.Any(I => I.Id == "camadaMenorDestaca"))
            {
                gmap.Overlays.Remove(camadaMenorDestaca);
                gmap.Invalidate();
                gmap.Update();                
            }
            else
            {
                CriarCamadaMenor(false);
            }
        }

        #endregion

        #region Eventos btnMaior

        private void btnMaior_Click(object sender, EventArgs e)
        {
            if (gmap.Overlays.Any(I => I.Id == "camadaMaior"))
            {
                gmap.Overlays.Remove(camadaMaior);
                gmap.Invalidate();
                gmap.Update();

                CriarCamadaMaior(true);
            }
            else if (gmap.Overlays.Any(I => I.Id == "camadaMaiorDestaca"))
            {
                gmap.Overlays.Remove(camadaMaiorDestaca);
                gmap.Invalidate();
                gmap.Update();                
            }
            else
            {
                CriarCamadaMaior(false);
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Carregar distância do zoom atual do gmap.
        /// </summary>
        private void CarregarDistancia()
        {
            lstDistancia = new Dictionary<double, string>();

            lstDistancia.Add(0, "3500 km");
            lstDistancia.Add(1, "3000 km");
            lstDistancia.Add(2, "2500 km");
            lstDistancia.Add(3, "2000 km");
            lstDistancia.Add(4, "1000 km");
            lstDistancia.Add(5, "500 km");
            lstDistancia.Add(6, "200 km");
            lstDistancia.Add(7, "100 km");
            lstDistancia.Add(8, "50 km");
            lstDistancia.Add(9, "20 km");
            lstDistancia.Add(10, "10 km");
            lstDistancia.Add(11, "5 km");
            lstDistancia.Add(12, "2 km");
            lstDistancia.Add(13, "1,5 km");
            lstDistancia.Add(14, "1 km");
            lstDistancia.Add(15, "500 m");
            lstDistancia.Add(16, "200 m");
            lstDistancia.Add(17, "100 m");
            lstDistancia.Add(18, "50 m");
            lstDistancia.Add(19, "20 m");
            lstDistancia.Add(20, "10 m");
            lstDistancia.Add(21, "5 m");
        }

        /// <summary>
        /// Carregar mapa do processo AERMAP.
        /// </summary>
        private void CarregarMapaAERMAP()
        {
            try
            {
                panelLegendaAERMAP.Location = new Point(116, 0);
                panelLegendaAERMAP.Visible = true;

                #region Fonte emissora

                DataTable dtFontes = classeFonteAERMAP.RetornarRegistrosUso();

                if (dtFontes.Rows.Count == 0)
                {
                    MessageBox.Show(this, $"AERMAP - {classeHelp.BuscarMensagem(9)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }

                #endregion

                #region Domínio/Grid modelagem

                DataTable dtDominio = classeDominio.RetornarRegistroUso();

                if (dtDominio.Rows.Count == 0)
                {
                    MessageBox.Show(this, $"AERMAP - {classeHelp.BuscarMensagem(10)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }

                //X = LONGITUDE / Y = LATITUDE

                //movendo p direita => move long - decrescenta
                //movendo p esquerda => move long - acrescenta

                //movendo p baixo => move lat - acrescenta
                //movendo p cima => move lat - decrescenta

                int codigoDominio = dtDominio.Rows[0]["CODIGO"].ValidarValor<int>(0);
                int ZONA_UTM = dtDominio.Rows[0]["ZONA_UTM"].ValidarValor<int>(0);
                string ZONA_UTM_LETRA = dtDominio.Rows[0]["ZONA_UTM_LETRA"].ToString();

                double DOMINIO_X1 = dtDominio.Rows[0]["DOMINIO_X1"].ValidarValor<double>(0);
                double DOMINIO_Y1 = dtDominio.Rows[0]["DOMINIO_Y1"].ValidarValor<double>(0);
                double DOMINIO_X2 = dtDominio.Rows[0]["DOMINIO_X2"].ValidarValor<double>(0);
                double DOMINIO_Y2 = dtDominio.Rows[0]["DOMINIO_Y2"].ValidarValor<double>(0);
                double FONTE_GRADE = dtDominio.Rows[0]["FONTE_GRADE"].ValidarValor<double>(0);                

                decimal maiorCoordenadaFonte_X = classeFonteAERMAP.MaiorCoordenada_X();
                decimal maiorCoordenadaFonte_Y = classeFonteAERMAP.MaiorCoordenada_Y();

                double GRID_X1 = (double)classeDominio.MenorCoordenadaGrade_X(codigoDominio);
                double GRID_Y1 = (double)classeDominio.MenorCoordenadaGrade_Y(codigoDominio);
                double GRID_X2 = (double)maiorCoordenadaFonte_X + FONTE_GRADE;
                double GRID_Y2 = (double)maiorCoordenadaFonte_Y + FONTE_GRADE;

                if (ZONA_UTM < 0)
                {
                    ZONA_UTM = ZONA_UTM * (-1);
                }

                #endregion

                #region Definições AERMET

                var dtDefinicao = classeAERMET.RetornarRegistroUso();

                if (dtDefinicao.Item1.Rows.Count == 0)
                {
                    MessageBox.Show(this, $"AERMET - {classeHelp.BuscarMensagem(24)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }

                double localX = dtDefinicao.Item1.Rows[0]["X"].ValidarValor<double>(0);
                double localY = dtDefinicao.Item1.Rows[0]["Y"].ValidarValor<double>(0);

                UniversalTransverseMercator utmLocal = new UniversalTransverseMercator(ZONA_UTM_LETRA, ZONA_UTM, localX, localY);
                var coordenadasLocal = UniversalTransverseMercator.ConvertUTMtoSignedDegree(utmLocal);

                #endregion

                gmap.ShowCenter = false;
                gmap.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
                GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
                gmap.Position = new GMap.NET.PointLatLng(coordenadasLocal[0], coordenadasLocal[1]);
                //gmap.SetPositionByKeywords("Paris, France");            
                gmap.Dock = DockStyle.Fill;
                gmap.Zoom = 11;
                gmap.DragButton = MouseButtons.Left;

                #region Inserir Rotas Domínio de modelagem

                GMapOverlay routesDominio = new GMapOverlay("routesDominio");
                List<PointLatLng> pointsDominio = new List<PointLatLng>();

                #region Inferior esquerda horizontal

                UniversalTransverseMercator utm = new UniversalTransverseMercator(ZONA_UTM_LETRA, ZONA_UTM, DOMINIO_X1, DOMINIO_Y1);
                var coordenadas1 = UniversalTransverseMercator.ConvertUTMtoSignedDegree(utm);

                pointsDominio.Add(new PointLatLng(coordenadas1[0], coordenadas1[1]));

                #endregion

                #region Inferior direita horizontal

                utm = new UniversalTransverseMercator(ZONA_UTM_LETRA, ZONA_UTM, DOMINIO_X2, DOMINIO_Y2);
                var coordenadas2 = UniversalTransverseMercator.ConvertUTMtoSignedDegree(utm);

                pointsDominio.Add(new PointLatLng(coordenadas1[0], coordenadas2[1]));

                GMapRoute routeDominio = new GMapRoute(pointsDominio, "Domínio de modelagem");
                routeDominio.Stroke = new Pen(Color.Red, 3);
                routesDominio.Routes.Add(routeDominio);
                gmap.Overlays.Add(routesDominio);
                gmap.UpdateRouteLocalPosition(routeDominio);

                //double distanciaDominio = routeDominio.Distance;

                #endregion

                #region Superior direta horizontal

                routesDominio = new GMapOverlay("routesDominio2");
                pointsDominio = new List<PointLatLng>();

                pointsDominio.Add(new PointLatLng(coordenadas2[0], coordenadas2[1]));

                #endregion

                #region Superior esquerda horizontal

                pointsDominio.Add(new PointLatLng(coordenadas2[0], coordenadas1[1]));

                routeDominio = new GMapRoute(pointsDominio, "Domínio de modelagem");
                routeDominio.Stroke = new Pen(Color.Red, 3);
                routesDominio.Routes.Add(routeDominio);
                gmap.Overlays.Add(routesDominio);
                gmap.UpdateRouteLocalPosition(routeDominio);

                #endregion

                #region Esquerda vertical

                routesDominio = new GMapOverlay("routesDominio3");
                pointsDominio = new List<PointLatLng>();

                pointsDominio.Add(new PointLatLng(coordenadas1[0], coordenadas1[1]));
                pointsDominio.Add(new PointLatLng(coordenadas2[0], coordenadas1[1]));

                routeDominio = new GMapRoute(pointsDominio, "Domínio de modelagem");
                routeDominio.Stroke = new Pen(Color.Red, 3);
                routesDominio.Routes.Add(routeDominio);
                gmap.Overlays.Add(routesDominio);
                gmap.UpdateRouteLocalPosition(routeDominio);

                #endregion

                #region Direita vertical

                routesDominio = new GMapOverlay("routesDominio4");
                pointsDominio = new List<PointLatLng>();

                pointsDominio.Add(new PointLatLng(coordenadas2[0], coordenadas2[1]));
                pointsDominio.Add(new PointLatLng(coordenadas1[0], coordenadas2[1]));

                routeDominio = new GMapRoute(pointsDominio, "Domínio de modelagem");
                routeDominio.Stroke = new Pen(Color.Red, 3);
                routesDominio.Routes.Add(routeDominio);
                gmap.Overlays.Add(routesDominio);
                gmap.UpdateRouteLocalPosition(routeDominio);

                #endregion

                #endregion

                #region Inserir Rotas Grid de modelagem            

                GMapOverlay routesGrid = new GMapOverlay("routesGrid");
                List<PointLatLng> pointsGrid = new List<PointLatLng>();

                #region Inferior esquerda horizontal

                utm = new UniversalTransverseMercator(ZONA_UTM_LETRA, ZONA_UTM, GRID_X1, GRID_Y1);
                coordenadas1 = UniversalTransverseMercator.ConvertUTMtoSignedDegree(utm);

                pointsGrid.Add(new PointLatLng(coordenadas1[0], coordenadas1[1]));

                #endregion

                #region Inferior direita horizontal

                utm = new UniversalTransverseMercator(ZONA_UTM_LETRA, ZONA_UTM, GRID_X2, GRID_Y2);
                coordenadas2 = UniversalTransverseMercator.ConvertUTMtoSignedDegree(utm);

                pointsGrid.Add(new PointLatLng(coordenadas1[0], coordenadas2[1]));

                GMapRoute routeGrid = new GMapRoute(pointsGrid, "Grid de modelagem");
                routeGrid.Stroke = new Pen(Color.Green, 3);
                routesGrid.Routes.Add(routeGrid);
                gmap.Overlays.Add(routesGrid);
                gmap.UpdateRouteLocalPosition(routeGrid);

                #endregion

                #region Superior direita horizontal

                routesGrid = new GMapOverlay("routesGrid2");
                pointsGrid = new List<PointLatLng>();

                pointsGrid.Add(new PointLatLng(coordenadas2[0], coordenadas2[1]));

                #endregion

                #region Superior esquerda horizontal

                pointsGrid.Add(new PointLatLng(coordenadas2[0], coordenadas1[1]));

                routeGrid = new GMapRoute(pointsGrid, "Grid de modelagem");
                routeGrid.Stroke = new Pen(Color.Green, 3);
                routesGrid.Routes.Add(routeGrid);
                gmap.Overlays.Add(routesGrid);
                gmap.UpdateRouteLocalPosition(routeGrid);

                #endregion

                #region Esquerda vertical

                routesGrid = new GMapOverlay("routesGrid3");
                pointsGrid = new List<PointLatLng>();

                pointsGrid.Add(new PointLatLng(coordenadas1[0], coordenadas1[1]));
                pointsGrid.Add(new PointLatLng(coordenadas2[0], coordenadas1[1]));

                routeGrid = new GMapRoute(pointsGrid, "Grid de modelagem");
                routeGrid.Stroke = new Pen(Color.Green, 3);
                routesGrid.Routes.Add(routeGrid);
                gmap.Overlays.Add(routesGrid);
                gmap.UpdateRouteLocalPosition(routeGrid);

                #endregion

                #region Direita vertical

                routesGrid = new GMapOverlay("routesGrid4");
                pointsGrid = new List<PointLatLng>();

                pointsGrid.Add(new PointLatLng(coordenadas2[0], coordenadas2[1]));
                pointsGrid.Add(new PointLatLng(coordenadas1[0], coordenadas2[1]));

                routeGrid = new GMapRoute(pointsGrid, "Grid de modelagem");
                routeGrid.Stroke = new Pen(Color.Green, 3);
                routesGrid.Routes.Add(routeGrid);
                gmap.Overlays.Add(routesGrid);
                gmap.UpdateRouteLocalPosition(routeGrid);

                #endregion

                #endregion

                #region Inserir fontes emissoras

                GMapOverlay markers = new GMapOverlay("markers");

                foreach (DataRow item in dtFontes.Rows)
                {
                    string codigo = item["CODIGO"].ValidarValor<int>(0).ToString().PadLeft(2, '0');
                    TipoFonte tipoFonte = (TipoFonte)item["TIPO"];
                    double X = item["X"].ValidarValor<double>(0);
                    double Y = item["Y"].ValidarValor<double>(0);
                    string descricao = item["DESCRICAO"].ToString();

                    utm = new UniversalTransverseMercator(ZONA_UTM_LETRA, ZONA_UTM, X, Y);
                    var coordenadas = UniversalTransverseMercator.ConvertUTMtoSignedDegree(utm);

                    GMapMarker marker = new GMarkerGoogle(
                    new PointLatLng(coordenadas[0], coordenadas[1]),
                    GMarkerGoogleType.red_dot);
                    markers.Markers.Add(marker);
                    gmap.UpdateMarkerLocalPosition(marker);

                    marker.ToolTipText = $"\n{codigo} - {descricao}\nTipo: {tipoFonte.GetEnumDescription()}\nLatitude: {coordenadas[0]}\nLongitude: {coordenadas[1]}";
                    marker.ToolTip.Fill = Brushes.White;
                    marker.ToolTip.Foreground = new SolidBrush(Color.Black);
                    marker.ToolTip.Stroke = Pens.Blue;
                    marker.ToolTip.TextPadding = new Size(20, 10);
                    marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
                }

                gmap.Overlays.Add(markers);

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"{classeHelp.BuscarMensagem(63)}\n\n{ex.Message}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Carregar mapa do processo AERMOD.
        /// </summary>
        private void CarregarMapaAERMOD()
        {
            #region Criar arquivo na pasta AERMOD

            btnFonte.Visible = true;
            btnBorda.Visible = true;
            btnMenor.Visible = true;
            btnMaior.Visible = true;

            int codigoAERMOD = classeAERMOD.RetornarCodigoUso();
            int sequencia = 0;
            string unidadeMedicao = classeAERMOD.RetornarUnidadeMedicao(codigoAERMOD);
            Poluentes poluente = (Poluentes)classeAERMOD.RetornarPoluente(codigoAERMOD);

            if (codigoAERMOD == 0 || string.IsNullOrEmpty(unidadeMedicao))
            {
                MessageBox.Show(this, $"AERMOD - {classeHelp.BuscarMensagem(24)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FrmArquivo frmArquivo = new FrmArquivo(TipoArquivo.PLT, true);
            frmArquivo.ShowDialogFade(this);

            sequencia = frmArquivo.CodigoArquivo;

            if (sequencia == 0)
            {
                this.Close();
                return;
            }

            var arquivo = classeAERMOD.RetornarArquivoPLT(codigoAERMOD, sequencia);
            arquivo = Funcoes.DecompressedGZip(arquivo);
            string descricaoArquivo = classeAERMOD.RetornarDescricaoArquivoPLT(codigoAERMOD, sequencia);
            this.Text += $" - {descricaoArquivo}";
            lbConcentracao.Text = $"Concentração ({poluente}) - {unidadeMedicao}";

            string diretorio = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "AERMOD_BACKEND\\AERMOD");

            string caminho = $"{diretorio}\\{descricaoArquivo}";

            File.Delete(caminho);

            File.WriteAllBytes(caminho, arquivo);

            #endregion

            #region Leitura do arquivo

            lstConcentracao = new List<Tuple<decimal, double, double>>();           

            try
            {
                #region Montar lista com concentrações/coordenadas

                var lines = File.ReadLines(caminho);

                if (descricaoArquivo.Contains("RANK"))
                {
                    #region RANKFILE

                    bool linhaValores = false;
                 
                    foreach (var line in lines)
                    {
                        if (linhaValores)
                        {
                            List<string> lstValor = line.Split(' ').ToList();

                            lstValor.RemoveAll(I => I == "");

                            double coordenada_X = Convert.ToDouble(lstValor[3].Replace(".", ","));
                            double coordenada_Y = Convert.ToDouble(lstValor[4].Replace(".", ","));
                            decimal concentracaoMedia = Convert.ToDecimal(lstValor[1].Replace(".", ","));

                            lstConcentracao.Add(Tuple.Create(Convert.ToDecimal(concentracaoMedia),
                                                             Convert.ToDouble(coordenada_X),
                                                             Convert.ToDouble(coordenada_Y)));                            
                        }

                        if (line.StartsWith("*_"))
                        {
                            linhaValores = true;
                        }
                    }

                    #endregion
                }
                else if (descricaoArquivo.Contains("MAXI"))
                {
                    #region MAXIFILE

                    bool linhaValores = false;

                    foreach (var line in lines)
                    {
                        if (linhaValores)
                        {
                            List<string> lstValor = line.Split(' ').ToList();

                            lstValor.RemoveAll(I => I == "");

                            double coordenada_X = Convert.ToDouble(lstValor[3].Replace(".", ","));
                            double coordenada_Y = Convert.ToDouble(lstValor[4].Replace(".", ","));
                            decimal concentracaoMedia = Convert.ToDecimal(lstValor.Last().Replace(".", ","));

                            lstConcentracao.Add(Tuple.Create(Convert.ToDecimal(concentracaoMedia),
                                                             Convert.ToDouble(coordenada_X),
                                                             Convert.ToDouble(coordenada_Y)));
                        }

                        if (line.StartsWith("*_"))
                        {
                            linhaValores = true;
                        }
                    }

                    #endregion
                }
                else if (descricaoArquivo.Contains("POST"))
                {
                    #region POSTFILE

                    bool linhaValores = false;

                    foreach (var line in lines)
                    {
                        if (linhaValores)
                        {
                            List<string> lstValor = line.Split(' ').ToList();

                            lstValor.RemoveAll(I => I == "");

                            double coordenada_X = Convert.ToDouble(lstValor[0].Replace(".", ","));
                            double coordenada_Y = Convert.ToDouble(lstValor[1].Replace(".", ","));
                            decimal concentracaoMedia = Convert.ToDecimal(lstValor[2].Replace(".", ","));

                            lstConcentracao.Add(Tuple.Create(Convert.ToDecimal(concentracaoMedia),
                                                             Convert.ToDouble(coordenada_X),
                                                             Convert.ToDouble(coordenada_Y)));
                        }

                        if (line.StartsWith("* _"))
                        {
                            linhaValores = true;
                        }
                    }

                    #endregion
                }
                else if (descricaoArquivo.Contains("PLOT"))
                {
                    #region PLOTFILE

                    bool linhaValores = false;

                    foreach (var line in lines)
                    {
                        if (linhaValores)
                        {
                            List<string> lstValor = line.Split(' ').ToList();

                            lstValor.RemoveAll(I => I == "");

                            double coordenada_X = Convert.ToDouble(lstValor[0].Replace(".", ","));
                            double coordenada_Y = Convert.ToDouble(lstValor[1].Replace(".", ","));
                            decimal concentracaoMedia = Convert.ToDecimal(lstValor[2].Replace(".", ","));

                            lstConcentracao.Add(Tuple.Create(Convert.ToDecimal(concentracaoMedia),
                                                             Convert.ToDouble(coordenada_X),
                                                             Convert.ToDouble(coordenada_Y)));
                        }

                        if (line.StartsWith("* _"))
                        {
                            linhaValores = true;
                        }
                    }

                    #endregion
                }

                #endregion

                #region Montar Quartil

                lstConcentracao.Sort();                

                int numeroObservacao = lstConcentracao.Count;

                var lista = lstConcentracao.Select(I => I.Item1).ToArray();
                double[] x = Array.ConvertAll(lista, I => (double)I);
                var q1 = x.Min();
                var q2 = Percentile(x, 25);
                var q3 = Percentile(x, 50);
                var q4 = Percentile(x, 75);
                var q5 = x.Max();

                //decimal primeiroQuartil = numeroObservacao * (1m / 4m);
                //primeiroQuartil = Math.Round(primeiroQuartil);

                //decimal segundoQuartil = numeroObservacao * (2m / 4m);
                //segundoQuartil = Math.Round(segundoQuartil);

                //decimal terceiroQuartil = numeroObservacao * (3m / 4m);
                //terceiroQuartil = Math.Round(terceiroQuartil);

                //decimal quartoQuartil = numeroObservacao * (4m / 4m);
                //quartoQuartil = Math.Round(quartoQuartil);                

                //List<decimal> lstEscala1 = new List<decimal>() { lstConcentracao.Min().Item1, lstConcentracao[Convert.ToInt32(primeiroQuartil)].Item1 };
                //List<decimal> lstEscala2 = new List<decimal>() { lstConcentracao[Convert.ToInt32(primeiroQuartil)].Item1, lstConcentracao[Convert.ToInt32(segundoQuartil)].Item1 };
                //List<decimal> lstEscala3 = new List<decimal>() { lstConcentracao[Convert.ToInt32(segundoQuartil)].Item1, lstConcentracao[Convert.ToInt32(terceiroQuartil)].Item1 };
                //List<decimal> lstEscala4 = new List<decimal>() { lstConcentracao[Convert.ToInt32(terceiroQuartil)].Item1, lstConcentracao.Max().Item1 };

                lstEscala1 = new List<decimal>() { (decimal)q1, (decimal)q2 };
                lstEscala2 = new List<decimal>() { (decimal)q2, (decimal)q3 };
                lstEscala3 = new List<decimal>() { (decimal)q3, (decimal)q4 };
                lstEscala4 = new List<decimal>() { (decimal)q4, (decimal)q5 };

                lbEscala1.Text = $"{lstEscala1[0].Truncar(3, true)} |---  {lstEscala1[1].Truncar(3, true)}";                
                lbEscala2.Text = $"{lstEscala2[0].Truncar(3, true)} |---  {lstEscala2[1].Truncar(3, true)}";
                lbEscala3.Text = $"{lstEscala3[0].Truncar(3, true)} |---  {lstEscala3[1].Truncar(3, true)}";
                lbEscala4.Text = $"{lstEscala4[0].Truncar(3, true)} |---| {lstEscala4[1].Truncar(3, true)}";

                panelLegendaAERMOD.Visible = true;               
                lbConcentracao.Visible = true;

                #endregion

                #region Montar gráfico POST-PLOT

                //Desenhar círculo
                //https://blog.aspose.com/drawing/draw-circle-in-csharp/

                #region Fonte emissora

                dtFontes = classeFonteAERMAP.RetornarRegistros();

                if (dtFontes.Rows.Count == 0)
                {
                    MessageBox.Show(this, $"AERMAP - {classeHelp.BuscarMensagem(9)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }

                #endregion

                #region Domínio/Grid modelagem

                DataTable dtDominio = classeDominio.RetornarRegistroUso();

                if (dtDominio.Rows.Count == 0)
                {
                    MessageBox.Show(this, $"AERMAP - {classeHelp.BuscarMensagem(10)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }                

                ZONA_UTM = dtDominio.Rows[0]["ZONA_UTM"].ValidarValor<int>(0);
                ZONA_UTM_LETRA = dtDominio.Rows[0]["ZONA_UTM_LETRA"].ToString();                

                if (ZONA_UTM < 0)
                {
                    ZONA_UTM = ZONA_UTM * (-1);
                }

                #endregion

                #region Definições AERMET

                var dtDefinicao = classeAERMET.RetornarRegistroUso();

                if (dtDefinicao.Item1.Rows.Count == 0)
                {
                    MessageBox.Show(this, $"AERMET - {classeHelp.BuscarMensagem(24)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }

                double localX = dtDefinicao.Item1.Rows[0]["X"].ValidarValor<double>(0);
                double localY = dtDefinicao.Item1.Rows[0]["Y"].ValidarValor<double>(0);

                UniversalTransverseMercator utmLocal = new UniversalTransverseMercator(ZONA_UTM_LETRA, ZONA_UTM, localX, localY);
                var coordenadasLocal = UniversalTransverseMercator.ConvertUTMtoSignedDegree(utmLocal);

                #endregion

                //X = LONGITUDE / Y = LATITUDE

                //movendo p direita => move long - decrescenta
                //movendo p esquerda => move long - acrescenta

                //movendo p baixo => move lat - acrescenta
                //movendo p cima => move lat - decrescenta

                gmap.ShowCenter = false;
                gmap.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
                GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
                gmap.Position = new GMap.NET.PointLatLng(coordenadasLocal[0], coordenadasLocal[1]);
                //gmap.SetPositionByKeywords("Paris, France");            
                gmap.Dock = DockStyle.Fill;
                gmap.Zoom = 11;
                gmap.DragButton = MouseButtons.Left;

                #region Círculo primeira escala                

                CriarPrimeiraEscala();

                #endregion

                #region Menor concentração

                CriarCamadaMenor(true);

                #endregion

                #region Círculo segunda escala

                CriarSegundaEscala();

                #endregion

                #region Círculo terceira escala                

                CriarTerceiraEscala();

                #endregion

                #region Círculo quarta escala                

                CriarQuartaEscala();

                #endregion

                #region Maior concentração

                CriarCamadaMaior(true);

                #endregion

                #endregion

                #region Domínio/Grade

                #region Domínio/Grid modelagem

                //X = LONGITUDE / Y = LATITUDE

                //movendo p direita => move long - decrescenta
                //movendo p esquerda => move long - acrescenta

                //movendo p baixo => move lat - acrescenta
                //movendo p cima => move lat - decrescenta

                //superior esquerda - (X) - maior / (y) - menor
                //superior direita - (X) - menor  / (y) - menor
                //inferior esquerda - (X) - maior / (y) - maior
                //inferior direita - (x) - menor / (y) - maior
                //esquerda vertical superior - (x) - maior / (y) - menor 
                //esquerda vertical inferior - (x) - maior / (y) - menor 
                //direita vertical superior - (x) - menor / (y) - menor
                //direita vertical inferior - (x) - menor / (y) - maior

                double superiorEsquerdaX = lstConcentracao.Max(I => I.Item2);
                double superiorEsquerdaY = lstConcentracao.Max(I => I.Item3);
                double superiorDireitaX = lstConcentracao.Min(I => I.Item2);
                double superiorDireitaY = lstConcentracao.Max(I => I.Item3);

                double inferiorEsquerdaX = lstConcentracao.Max(I => I.Item2);
                double inferiorEsquerdaY = lstConcentracao.Min(I => I.Item3);
                double inferiorDireitaX = lstConcentracao.Min(I => I.Item2);
                double inferiorDireitaY = lstConcentracao.Min(I => I.Item3);                

                int codigoDominio = dtDominio.Rows[0]["CODIGO"].ValidarValor<int>(0);
                double DOMINIO_X1 = dtDominio.Rows[0]["DOMINIO_X1"].ValidarValor<double>(0);
                double DOMINIO_Y1 = dtDominio.Rows[0]["DOMINIO_Y1"].ValidarValor<double>(0);
                double DOMINIO_X2 = dtDominio.Rows[0]["DOMINIO_X2"].ValidarValor<double>(0);
                double DOMINIO_Y2 = dtDominio.Rows[0]["DOMINIO_Y2"].ValidarValor<double>(0);
                double FONTE_GRADE = dtDominio.Rows[0]["FONTE_GRADE"].ValidarValor<double>(0);

                decimal maiorCoordenadaFonte_X = classeFonteAERMAP.MaiorCoordenada_X();
                decimal maiorCoordenadaFonte_Y = classeFonteAERMAP.MaiorCoordenada_Y();

                double GRID_X1 = (double)classeDominio.MenorCoordenadaGrade_X(codigoDominio);
                double GRID_Y1 = (double)classeDominio.MenorCoordenadaGrade_Y(codigoDominio);
                double GRID_X2 = (double)maiorCoordenadaFonte_X + FONTE_GRADE;
                double GRID_Y2 = (double)maiorCoordenadaFonte_Y + FONTE_GRADE;

                if (ZONA_UTM < 0)
                {
                    ZONA_UTM = ZONA_UTM * (-1);
                }

                #endregion                

                //gmap.ShowCenter = false;
                //gmap.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
                //GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
                //gmap.Position = new GMap.NET.PointLatLng(coordenadasLocal[0], coordenadasLocal[1]);
                ////gmap.SetPositionByKeywords("Paris, France");            
                //gmap.Dock = DockStyle.Fill;
                //gmap.Zoom = 11;
                //gmap.DragButton = MouseButtons.Left;

                #region Inserir Rotas Domínio de modelagem

                CriarCamadaBorda();

                #endregion

                #region Inserir Rotas Grid de modelagem            

                //GMapOverlay routesGrid = new GMapOverlay("routesGrid");
                //List<PointLatLng> pointsGrid = new List<PointLatLng>();

                #region Inferior esquerda horizontal

                //utm = new UniversalTransverseMercator(ZONA_UTM_LETRA, ZONA_UTM, GRID_X1, GRID_Y1);
                //coordenadas1 = UniversalTransverseMercator.ConvertUTMtoSignedDegree(utm);

                //pointsGrid.Add(new PointLatLng(coordenadas1[0], coordenadas1[1]));

                #endregion

                #region Inferior direita horizontal

                //utm = new UniversalTransverseMercator(ZONA_UTM_LETRA, ZONA_UTM, GRID_X2, GRID_Y2);
                //coordenadas2 = UniversalTransverseMercator.ConvertUTMtoSignedDegree(utm);

                //pointsGrid.Add(new PointLatLng(coordenadas1[0], coordenadas2[1]));

                //GMapRoute routeGrid = new GMapRoute(pointsGrid, "Grid de modelagem");
                //routeGrid.Stroke = new Pen(Color.Green, 3);
                //routesGrid.Routes.Add(routeGrid);
                //gmap.Overlays.Add(routesGrid);
                //gmap.UpdateRouteLocalPosition(routeGrid);

                #endregion

                #region Superior direita horizontal

                //routesGrid = new GMapOverlay("routesGrid2");
                //pointsGrid = new List<PointLatLng>();

                //pointsGrid.Add(new PointLatLng(coordenadas2[0], coordenadas2[1]));

                #endregion

                #region Superior esquerda horizontal

                //pointsGrid.Add(new PointLatLng(coordenadas2[0], coordenadas1[1]));

                //routeGrid = new GMapRoute(pointsGrid, "Grid de modelagem");
                //routeGrid.Stroke = new Pen(Color.Green, 3);
                //routesGrid.Routes.Add(routeGrid);
                //gmap.Overlays.Add(routesGrid);
                //gmap.UpdateRouteLocalPosition(routeGrid);

                #endregion

                #region Esquerda vertical

                //routesGrid = new GMapOverlay("routesGrid3");
                //pointsGrid = new List<PointLatLng>();

                //pointsGrid.Add(new PointLatLng(coordenadas1[0], coordenadas1[1]));
                //pointsGrid.Add(new PointLatLng(coordenadas2[0], coordenadas1[1]));

                //routeGrid = new GMapRoute(pointsGrid, "Grid de modelagem");
                //routeGrid.Stroke = new Pen(Color.Green, 3);
                //routesGrid.Routes.Add(routeGrid);
                //gmap.Overlays.Add(routesGrid);
                //gmap.UpdateRouteLocalPosition(routeGrid);

                #endregion

                #region Direita vertical

                //routesGrid = new GMapOverlay("routesGrid4");
                //pointsGrid = new List<PointLatLng>();

                //pointsGrid.Add(new PointLatLng(coordenadas2[0], coordenadas2[1]));
                //pointsGrid.Add(new PointLatLng(coordenadas1[0], coordenadas2[1]));

                //routeGrid = new GMapRoute(pointsGrid, "Grid de modelagem");
                //routeGrid.Stroke = new Pen(Color.Green, 3);
                //routesGrid.Routes.Add(routeGrid);
                //gmap.Overlays.Add(routesGrid);
                //gmap.UpdateRouteLocalPosition(routeGrid);

                #endregion

                #endregion

                #region Inserir fontes emissoras                

                CriarCamadaFonte();

                #endregion

                #endregion

                File.Delete(caminho);
            }
            catch
            {
                File.Delete(caminho);

                MessageBox.Show(this, classeHelp.BuscarMensagem(64), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK);
                return;
            }            

            #endregion
        }

        /// <summary>
        /// Retornar Bitmap.
        /// </summary>
        /// <param name="cor">Cor</param>
        /// <returns></returns>
        private Bitmap RetornarBitmap(Color cor)
        {
            // Create a new Bitmap
            Bitmap bitmap = new Bitmap(10, 10, PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);
          
            // Draw a filled circle
            Brush brush = new SolidBrush(cor);

            // Draw the filled inner circle
            graphics.FillEllipse(brush, 1, 1, 5, 5);

            // Create a Pen class instance
            Pen pen = new Pen(Color.Black, 1);

            // Draw the circle
            graphics.DrawEllipse(pen, 1, 1, 5, 5);

            // Save output drawing image
            //bitmap.Save("C:\\Files\\DrawCircle.jpg");
            return bitmap;
        }

        /// <summary>
        /// Retornar Bitmap menor.
        /// </summary>
        /// <returns></returns>
        private Bitmap RetornarBitmapMenor()
        {
            Bitmap bitmap = new Bitmap(100, 100, PixelFormat.Format32bppPArgb);
            Graphics graphics = Graphics.FromImage(bitmap);

            // Create a Pen class instance
            Pen pen = new Pen(Color.Blue, 1);

            //graphics.DrawLine(pen, 5, 5, 5, 5);
            graphics.DrawLine(new Pen(Color.Blue, 1), 0, 3, 3, 3);


            return bitmap;
        }

        /// <summary>
        /// Calculate percentile of a sorted data set
        /// </summary>
        /// <param name="sortedData"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        internal static double Percentile(double[] sortedData, double p)
        {
            //https://stackoverflow.com/questions/14683467/finding-the-first-and-third-quartiles
            // algo derived from Aczel pg 15 bottom
            if (p >= 100.0d) return sortedData[sortedData.Length - 1];

            double position = (sortedData.Length + 1) * p / 100.0;
            double leftNumber = 0.0d, rightNumber = 0.0d;

            double n = p / 100.0d * (sortedData.Length - 1) + 1.0d;

            if (position >= 1)
            {
                leftNumber = sortedData[(int)Math.Floor(n) - 1];
                rightNumber = sortedData[(int)Math.Floor(n)];
            }
            else
            {
                leftNumber = sortedData[0]; // first data
                rightNumber = sortedData[1]; // first data
            }

            //if (leftNumber == rightNumber)
            if (Equals(leftNumber, rightNumber))
                return leftNumber;
            double part = n - Math.Floor(n);
            return leftNumber + part * (rightNumber - leftNumber);
        } // end of internal function percentile

        /// <summary>
        /// Criar primeira escala.
        /// </summary>
        private void CriarPrimeiraEscala()
        {
            decimal condicao1 = lstEscala1[0];
            decimal condicao2 = lstEscala1[1];

            primeiraEscala = new GMapOverlay("primeiraEscala");
            primeiraEscala.Id = "primeiraEscala";

            var lstCirculo1 = lstConcentracao.Where(I => I.Item1 >= condicao1 && I.Item1 < condicao2);

            //Arquivo.EscreverAERMOD("X-------------Y--------------Z", 0, true);

            decimal concentracaoMenor = lstConcentracao.First().Item1;

            foreach (var item in lstCirculo1)
            {
                if (item.Item1 == concentracaoMenor)
                {
                    continue;
                }

                UniversalTransverseMercator coordenadasPoligonoUTM = new UniversalTransverseMercator(ZONA_UTM_LETRA, ZONA_UTM, item.Item2, item.Item3);
                double[] coordenadasPoligonoGD = UniversalTransverseMercator.ConvertUTMtoSignedDegree(coordenadasPoligonoUTM);

                //GMapMarker marker = new GMarkerGoogle(
                //new PointLatLng(coordenadasPoligonoGD[0], coordenadasPoligonoGD[1]),
                //GMarkerGoogleType.blue);
                //markers.Markers.Add(marker);
                //gmap.UpdateMarkerLocalPosition(marker);

                GMapMarker marker = new GMarkerGoogle(
                new PointLatLng(coordenadasPoligonoGD[0], coordenadasPoligonoGD[1]),
                RetornarBitmap(lbEscala1.BackColor));

                marker.ToolTipMode = MarkerTooltipMode.Never;              

                primeiraEscala.Markers.Add(marker);
                gmap.UpdateMarkerLocalPosition(marker);
            }            
         
            gmap.Overlays.Add(primeiraEscala);
            gmap.Invalidate();
            gmap.Update();
        }

        /// <summary>
        /// Criar segunda escala.
        /// </summary>
        private void CriarSegundaEscala()
        {
            decimal condicao1 = lstEscala2[0];
            decimal condicao2 = lstEscala2[1];

            segundaEscala = new GMapOverlay("segundaEscala");            

            var lstCirculo2 = lstConcentracao.Where(I => I.Item1 >= condicao1 && I.Item1 < condicao2);

            foreach (var item in lstCirculo2)
            {
                UniversalTransverseMercator coordenadasPoligonoUTM = new UniversalTransverseMercator(ZONA_UTM_LETRA, ZONA_UTM, item.Item2, item.Item3);
                double[] coordenadasPoligonoGD = UniversalTransverseMercator.ConvertUTMtoSignedDegree(coordenadasPoligonoUTM);

                GMapMarker marker = new GMarkerGoogle(
                new PointLatLng(coordenadasPoligonoGD[0], coordenadasPoligonoGD[1]),
                RetornarBitmap(lbEscala2.BackColor));

                marker.ToolTipMode = MarkerTooltipMode.Never;

                segundaEscala.Markers.Add(marker);
                gmap.UpdateMarkerLocalPosition(marker);
            }

            gmap.Overlays.Add(segundaEscala);
            gmap.Invalidate();
            gmap.Update();
        }

        /// <summary>
        /// Criar terceira escala.
        /// </summary>
        private void CriarTerceiraEscala()
        {
            decimal condicao1 = lstEscala3[0];
            decimal condicao2 = lstEscala3[1];

            terceiraEscala = new GMapOverlay("terceiraEscala");            

            var lstCirculo3 = lstConcentracao.Where(I => I.Item1 >= condicao1 && I.Item1 < condicao2);

            foreach (var item in lstCirculo3)
            {
                UniversalTransverseMercator coordenadasPoligonoUTM = new UniversalTransverseMercator(ZONA_UTM_LETRA, ZONA_UTM, item.Item2, item.Item3);
                double[] coordenadasPoligonoGD = UniversalTransverseMercator.ConvertUTMtoSignedDegree(coordenadasPoligonoUTM);

                GMapMarker marker = new GMarkerGoogle(
                new PointLatLng(coordenadasPoligonoGD[0], coordenadasPoligonoGD[1]),
                RetornarBitmap(lbEscala3.BackColor));

                marker.ToolTipMode = MarkerTooltipMode.Never;

                terceiraEscala.Markers.Add(marker);
                gmap.UpdateMarkerLocalPosition(marker);
            }

            gmap.Overlays.Add(terceiraEscala);
            gmap.Invalidate();
            gmap.Update();
        }

        /// <summary>
        /// Criar quarta escala.
        /// </summary>
        private void CriarQuartaEscala()
        {
            decimal condicao1 = lstEscala4[0];
            decimal condicao2 = lstEscala4[1];

            quartaEscala = new GMapOverlay("quartaEscala");           

            var lstCirculo4 = lstConcentracao.Where(I => I.Item1 >= condicao1 && I.Item1 <= condicao2);

            decimal concentracaoMaior = lstConcentracao.Last().Item1;

            foreach (var item in lstCirculo4)
            {
                if (item.Item1 == concentracaoMaior)
                {
                    continue;
                }

                UniversalTransverseMercator coordenadasPoligonoUTM = new UniversalTransverseMercator(ZONA_UTM_LETRA, ZONA_UTM, item.Item2, item.Item3);
                double[] coordenadasPoligonoGD = UniversalTransverseMercator.ConvertUTMtoSignedDegree(coordenadasPoligonoUTM);

                GMapMarker marker = new GMarkerGoogle(
                new PointLatLng(coordenadasPoligonoGD[0], coordenadasPoligonoGD[1]),
                RetornarBitmap(lbEscala4.BackColor));

                marker.ToolTipMode = MarkerTooltipMode.Never;

                quartaEscala.Markers.Add(marker);
                gmap.UpdateMarkerLocalPosition(marker);
            }            

            gmap.Overlays.Add(quartaEscala);
            gmap.Invalidate();
            gmap.Update();
        }

        /// <summary>
        /// Criar camada fonte.
        /// </summary>
        private void CriarCamadaFonte()
        {
            camadaFonte = new GMapOverlay("camadaFonte");
            camadaFonte.Id = "camadaFonte";

            foreach (DataRow item in dtFontes.Rows)
            {                
                string codigo = item["CODIGO"].ValidarValor<int>(0).ToString().PadLeft(2, '0');
                TipoFonte tipoFonte = (TipoFonte)item["TIPO"];
                double X = item["X"].ValidarValor<double>(0);
                double Y = item["Y"].ValidarValor<double>(0);
                string descricao = item["DESCRICAO"].ToString();

                UniversalTransverseMercator utm = new UniversalTransverseMercator(ZONA_UTM_LETRA, ZONA_UTM, X, Y);
                var coordenadas = UniversalTransverseMercator.ConvertUTMtoSignedDegree(utm);

                GMapMarker marker = new GMarkerGoogle(
                new PointLatLng(coordenadas[0], coordenadas[1]),              
                GMarkerGoogleType.blue_dot);
                camadaFonte.Markers.Add(marker);
                gmap.UpdateMarkerLocalPosition(marker);

                marker.ToolTipText = $"\n{codigo} - {descricao}\nTipo: {tipoFonte.GetEnumDescription()}\nLatitude: {coordenadas[0]}\nLongitude: {coordenadas[1]}";
                marker.ToolTip.Fill = Brushes.White;
                marker.ToolTip.Foreground = new SolidBrush(Color.Black);
                marker.ToolTip.Stroke = Pens.Blue;
                marker.ToolTip.TextPadding = new Size(20, 10);
                marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            }

            gmap.Overlays.Add(camadaFonte);
            gmap.Invalidate();
            gmap.Update();
        }

        /// <summary>
        /// Criar camada borda.
        /// </summary>
        private void CriarCamadaBorda()
        {
            camadaBorda = new GMapOverlay("camadaBorda");
            camadaBorda.Id = "camadaBorda";

            double superiorEsquerdaX = lstConcentracao.Max(I => I.Item2);
            double superiorEsquerdaY = lstConcentracao.Max(I => I.Item3);
            double superiorDireitaX = lstConcentracao.Min(I => I.Item2);
            double superiorDireitaY = lstConcentracao.Max(I => I.Item3);

            double inferiorEsquerdaX = lstConcentracao.Max(I => I.Item2);
            double inferiorEsquerdaY = lstConcentracao.Min(I => I.Item3);
            double inferiorDireitaX = lstConcentracao.Min(I => I.Item2);
            double inferiorDireitaY = lstConcentracao.Min(I => I.Item3);

            List<PointLatLng> pointsDominio = new List<PointLatLng>();

            #region Inferior esquerda horizontal

            //UniversalTransverseMercator utm = new UniversalTransverseMercator(ZONA_UTM_LETRA, ZONA_UTM, DOMINIO_X1, DOMINIO_Y1);
            UniversalTransverseMercator utm = new UniversalTransverseMercator(ZONA_UTM_LETRA, ZONA_UTM, inferiorEsquerdaX + 400, inferiorEsquerdaY - 100);
            var coordenadas1 = UniversalTransverseMercator.ConvertUTMtoSignedDegree(utm);

            pointsDominio.Add(new PointLatLng(coordenadas1[0], coordenadas1[1]));

            #endregion

            #region Inferior direita horizontal

            //utm = new UniversalTransverseMercator(ZONA_UTM_LETRA, ZONA_UTM, DOMINIO_X2, DOMINIO_Y2);
            utm = new UniversalTransverseMercator(ZONA_UTM_LETRA, ZONA_UTM, inferiorDireitaX - 550, inferiorDireitaY - 100);
            var coordenadas2 = UniversalTransverseMercator.ConvertUTMtoSignedDegree(utm);

            //pointsDominio.Add(new PointLatLng(coordenadas1[0], coordenadas2[1]));
            pointsDominio.Add(new PointLatLng(coordenadas2[0], coordenadas2[1]));

            GMapRoute routeDominio = new GMapRoute(pointsDominio, "Domínio de modelagem");
            routeDominio.Stroke = new Pen(Color.Black, 3);
            camadaBorda.Routes.Add(routeDominio);
            gmap.Overlays.Add(camadaBorda);
            gmap.UpdateRouteLocalPosition(routeDominio);

            #endregion

            #region Superior esquerda horizontal

            //routesDominio = new GMapOverlay("routesDominio2");
            pointsDominio = new List<PointLatLng>();

            //pointsDominio.Add(new PointLatLng(coordenadas2[0], coordenadas2[1]));

            utm = new UniversalTransverseMercator(ZONA_UTM_LETRA, ZONA_UTM, superiorEsquerdaX + 400, superiorEsquerdaY + 900);
            var coordenadas3 = UniversalTransverseMercator.ConvertUTMtoSignedDegree(utm);

            //pointsDominio = new List<PointLatLng>();
            pointsDominio.Add(new PointLatLng(coordenadas3[0], coordenadas3[1]));

            #endregion

            #region Superior direita horizontal

            utm = new UniversalTransverseMercator(ZONA_UTM_LETRA, ZONA_UTM, superiorDireitaX - 550, superiorDireitaY + 900);
            var coordenadas4 = UniversalTransverseMercator.ConvertUTMtoSignedDegree(utm);

            pointsDominio.Add(new PointLatLng(coordenadas4[0], coordenadas4[1]));

            routeDominio = new GMapRoute(pointsDominio, "Domínio de modelagem");
            routeDominio.Stroke = new Pen(Color.Black, 3);
            camadaBorda.Routes.Add(routeDominio);
            gmap.Overlays.Add(camadaBorda);
            gmap.UpdateRouteLocalPosition(routeDominio);

            #endregion

            #region Direita vertical

            pointsDominio = new List<PointLatLng>();

            pointsDominio.Add(new PointLatLng(coordenadas1[0], coordenadas1[1]));
            pointsDominio.Add(new PointLatLng(coordenadas3[0], coordenadas3[1]));

            routeDominio = new GMapRoute(pointsDominio, "Domínio de modelagem");
            routeDominio.Stroke = new Pen(Color.Black, 3);
            camadaBorda.Routes.Add(routeDominio);
            gmap.Overlays.Add(camadaBorda);
            gmap.UpdateRouteLocalPosition(routeDominio);

            #endregion

            #region Esquerda vertical

            pointsDominio = new List<PointLatLng>();

            pointsDominio.Add(new PointLatLng(coordenadas2[0], coordenadas2[1]));
            pointsDominio.Add(new PointLatLng(coordenadas4[0], coordenadas4[1]));

            routeDominio = new GMapRoute(pointsDominio, "Domínio de modelagem");
            routeDominio.Stroke = new Pen(Color.Black, 3);
            camadaBorda.Routes.Add(routeDominio);
            gmap.Overlays.Add(camadaBorda);
            gmap.UpdateRouteLocalPosition(routeDominio);

            #endregion
        }

        /// <summary>
        /// Criar camada menor concentração.
        /// </summary>
        /// <param name="destacar">Destacar com ícone</param>
        private void CriarCamadaMenor(bool destacar)
        {
            if (destacar == false)
            {
                camadaMenor = new GMapOverlay("camadaMenor");
                camadaMenor.Id = "camadaMenor";
            }
            else
            {
                camadaMenorDestaca = new GMapOverlay("camadaMenorDestaca");
                camadaMenorDestaca.Id = "camadaMenorDestaca";
            }

            UniversalTransverseMercator coordenadasPoligonoUTM = new UniversalTransverseMercator(ZONA_UTM_LETRA, ZONA_UTM, lstConcentracao.First().Item2, lstConcentracao.First().Item3);
            double[] coordenadasPoligonoGD = UniversalTransverseMercator.ConvertUTMtoSignedDegree(coordenadasPoligonoUTM);

            GMapMarker markerMenor = new GMarkerGoogle(
            new PointLatLng(coordenadasPoligonoGD[0], coordenadasPoligonoGD[1]),
            destacar == false ? RetornarBitmap(lbEscala1.BackColor) : AERMOD.Properties.Resources.down_azul);

            if (destacar == false)
            {
                camadaMenor.Markers.Add(markerMenor);
            }
            else
            {
                camadaMenorDestaca.Markers.Add(markerMenor);
            }

            gmap.UpdateMarkerLocalPosition(markerMenor);            

            if (destacar)
            {
                UniversalTransverseMercator utm = new UniversalTransverseMercator(ZONA_UTM_LETRA, ZONA_UTM, lstConcentracao.First().Item2, lstConcentracao.First().Item3);
                var coordenadas = UniversalTransverseMercator.ConvertUTMtoSignedDegree(utm);

                decimal concentracaoMenor = lstConcentracao.First().Item1;

                markerMenor.ToolTipText = $"\nMenor concentração: {concentracaoMenor.Truncar(3, true)}\nLatitude: {coordenadas[0]}\nLongitude: {coordenadas[1]}";
                markerMenor.ToolTip.Fill = Brushes.White;
                markerMenor.ToolTip.Foreground = new SolidBrush(Color.Black);
                markerMenor.ToolTip.Stroke = Pens.Blue;
                markerMenor.ToolTip.TextPadding = new Size(20, 10);
                markerMenor.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            }
            else
            {
                markerMenor.ToolTipMode = MarkerTooltipMode.Never;
            }

            if (destacar == false)
            {
                gmap.Overlays.Add(camadaMenor);
            }
            else
            {
                gmap.Overlays.Add(camadaMenorDestaca);
            }

            gmap.Invalidate();
            gmap.Update();            
        }      

        /// <summary>
        /// Criar camada maior concentração.
        /// </summary>
        /// <param name="destacar">Destacar com ícone</param>
        private void CriarCamadaMaior(bool destacar)
        {
            if (destacar == false)
            {
                camadaMaior = new GMapOverlay("camadaMaior");
                camadaMaior.Id = "camadaMaior";
            }
            else
            {
                camadaMaiorDestaca = new GMapOverlay("camadaMaiorDestaca");
                camadaMaiorDestaca.Id = "camadaMaiorDestaca";
            }

            UniversalTransverseMercator coordenadasPoligonoUTM = new UniversalTransverseMercator(ZONA_UTM_LETRA, ZONA_UTM, lstConcentracao.Last().Item2, lstConcentracao.Last().Item3);
            double[] coordenadasPoligonoGD = UniversalTransverseMercator.ConvertUTMtoSignedDegree(coordenadasPoligonoUTM);

            GMapMarker markerMaior = new GMarkerGoogle(
            new PointLatLng(coordenadasPoligonoGD[0], coordenadasPoligonoGD[1]),
            destacar == false ? RetornarBitmap(lbEscala4.BackColor) : AERMOD.Properties.Resources.up_azul);

            if (destacar == false)
            {
                camadaMaior.Markers.Add(markerMaior);
            }
            else
            {
                camadaMaiorDestaca.Markers.Add(markerMaior);
            }

            gmap.UpdateMarkerLocalPosition(markerMaior);

            if (destacar)
            {
                UniversalTransverseMercator utm = new UniversalTransverseMercator(ZONA_UTM_LETRA, ZONA_UTM, lstConcentracao.Last().Item2, lstConcentracao.Last().Item3);
                var coordenadas = UniversalTransverseMercator.ConvertUTMtoSignedDegree(utm);

                decimal concentracaoMaior = lstConcentracao.Last().Item1;

                markerMaior.ToolTipText = $"\nMaior concentração: {concentracaoMaior.Truncar(3, true)}\nLatitude: {coordenadas[0]}\nLongitude: {coordenadas[1]}";
                markerMaior.ToolTip.Fill = Brushes.White;
                markerMaior.ToolTip.Foreground = new SolidBrush(Color.Black);
                markerMaior.ToolTip.Stroke = Pens.Blue;
                markerMaior.ToolTip.TextPadding = new Size(20, 10);
                markerMaior.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            }
            else
            {
                markerMaior.ToolTipMode = MarkerTooltipMode.Never;
            }

            if (destacar == false)
            {
                gmap.Overlays.Add(camadaMaior);
            }
            else
            {
                gmap.Overlays.Add(camadaMaiorDestaca);
            }

            gmap.Invalidate();
            gmap.Update();
        }

        #endregion
    }
}
