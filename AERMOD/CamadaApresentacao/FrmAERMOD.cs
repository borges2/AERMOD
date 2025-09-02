using AERMOD.CamadaApresentacao.AERMAP;
using AERMOD.CamadaApresentacao.AERMET;
using AERMOD.CamadaApresentacao.PRINCIPAL;
using AERMOD.LIB;
using AERMOD.LIB.Componentes.Splash;
using AERMOD.LIB.Desenvolvimento;
using AERMOD.LIB.Forms;
using CamadaAcessoDados;
using CamadaLogicaNegocios;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell.Interop;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AERMOD.CamadaApresentacao
{
    public partial class FrmAERMOD : Form
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

        /// <summary>
        /// Classe de negócios clsTIF.
        /// </summary>
        ClsTIF clsTIF = null;

        /// <summary>
        /// Get a classe de parametro
        /// </summary>
        ClsTIF classeTIF
        {
            get
            {
                if (clsTIF == null)
                {
                    clsTIF = new ClsTIF(Base.ConfiguracaoRede);
                }

                return clsTIF;
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

        ClsAERMAP clsAERMAP = null;

        /// <summary>
        /// Get classe de negócios ClsAERMAP
        /// </summary>
        ClsAERMAP classeAERMAP
        {
            get
            {
                if (clsAERMAP == null)
                {
                    clsAERMAP = new ClsAERMAP(Base.ConfiguracaoRede);
                }

                return clsAERMAP;
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
        /// Classe de negócios SAMSON.
        /// </summary>
        ClsSamson clsSamson = null;

        /// <summary>
        /// Get a classe de parametro
        /// </summary>
        ClsSamson classeSAM
        {
            get
            {
                if (clsSamson == null)
                {
                    clsSamson = new ClsSamson(Base.ConfiguracaoRede);
                }

                return clsSamson;
            }
        }

        /// <summary>
        /// Classe de negócios FSL.
        /// </summary>
        ClsFSL clsFSL = null;

        /// <summary>
        /// Get a classe de parametro
        /// </summary>
        ClsFSL classeFSL
        {
            get
            {
                if (clsFSL == null)
                {
                    clsFSL = new ClsFSL(Base.ConfiguracaoRede);
                }

                return clsFSL;
            }
        }

        /// <summary>
        /// Classe de negócios clsParametros.
        /// </summary>
        ClsParametros clsParametros = null;

        /// <summary>
        /// Get a classe de parametro
        /// </summary>
        ClsParametros classeParametros
        {
            get
            {
                if (clsParametros == null)
                {
                    clsParametros = new ClsParametros(Base.ConfiguracaoRede);
                }

                return clsParametros;
            }
        }

        #endregion

        #region Construtor

        public FrmAERMOD(string caminhoDataBase)
        {
            InitializeComponent();

            this.Icon = Properties.Resources.AERMOD24.ConvertImageToIcon();

            var versaoAtual = Application.ProductVersion.ToString().Split('.');
            this.Text = $"{this.Text} V.{versaoAtual[0]}.{versaoAtual[1]}.{versaoAtual[2]}";

            if (string.IsNullOrEmpty(caminhoDataBase) == false)
            {
                tbxCaminhoDataBase.Text = caminhoDataBase;
                tbxCaminhoDataBase.Enabled = false;
                btnCaminhoDataBase.Enabled = false;                
            }
        }

        #endregion

        #region Eventos FrmAERMOD
   
        private void FrmAERMOD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.NumPad1:
                    case Keys.Oem1:
                        e.SuppressKeyPress = true;

                        if (tabControlDados.SelectedTab == tabPageAERMET)
                        {
                            ExportarINP1_AERMET();
                        }
                        break;
                    case Keys.NumPad2:
                    case Keys.Oem2:
                        e.SuppressKeyPress = true;

                        if (tabControlDados.SelectedTab == tabPageAERMET)
                        {
                            ExportarINP2_AERMET();
                        }
                        break;                    
                    case Keys.A:
                        e.SuppressKeyPress = true;

                        if (tabControlDados.SelectedTab == tabPageAERMOD)
                        {
                            ExportarPLT_AERMOD();
                        }
                        else if (tabControlDados.SelectedTab == tabPageParametros)
                        {
                            AbrirAtualizacao();
                        }
                        break;                    
                    case Keys.D:
                        e.SuppressKeyPress = true;

                        if (tabControlDados.SelectedTab == tabPageAERMAP)
                        {
                            AbrirDominioModelagem();
                        }
                        else if (tabControlDados.SelectedTab == tabPageAERMET)
                        {
                            AbrirDefinioes_AEMET();
                        }
                        else if (tabControlDados.SelectedTab == tabPageAERMOD)
                        {
                            AbrirDefinioes_AEMOD();
                        }
                        break;
                    case Keys.F:
                        e.SuppressKeyPress = true;

                        if (tabControlDados.SelectedTab == tabPageAERMAP)
                        {
                            AbrirFontesAERMAP();
                        }
                        else if (tabControlDados.SelectedTab == tabPageAERMET)
                        {
                            AbrirEditorFSL();
                        }
                        break;
                    case Keys.E:
                        e.SuppressKeyPress = true;

                        if (tabControlDados.SelectedTab == tabPageAERMOD)
                        {
                            ExportarOUT2_AERMOD();
                        }
                        break;
                    case Keys.I:
                        e.SuppressKeyPress = true;

                        if (tabControlDados.SelectedTab == tabPageAERMAP)
                        {
                            ExportarINP_AERMAP();
                        }
                        if (tabControlDados.SelectedTab == tabPageAERMOD)
                        {
                            ExportarINP_AERMOD();
                        }
                        break;
                    case Keys.M:
                        e.SuppressKeyPress = true;
                        AbrirMapa();
                        break;
                    case Keys.O:
                        e.SuppressKeyPress = true;

                        if (tabControlDados.SelectedTab == tabPageAERMAP)
                        {
                            ExportarOUT_AERMAP();
                        }
                        else if (tabControlDados.SelectedTab == tabPageAERMOD)
                        {
                            ExportarOUT1_AERMOD();
                        }
                        break;
                    case Keys.P:
                        e.SuppressKeyPress = true;

                        if (tabControlDados.SelectedTab == tabPageAERMOD)
                        {
                            AbrirParametros_AERMOD();
                        }
                        break;
                    case Keys.R:
                        e.SuppressKeyPress = true;

                        if (tabControlDados.SelectedTab == tabPageAERMAP)
                        {
                            ExportarROU_AERMAP();
                        }                        
                        break;
                    case Keys.S:
                        e.SuppressKeyPress = true;

                        if (tabControlDados.SelectedTab == tabPageAERMAP)
                        {
                            ExportarSOU_AERMAP();
                        }
                        else if (tabControlDados.SelectedTab == tabPageAERMET)
                        {
                            AbrirEditorSAM();
                        }
                        break;
                    case Keys.T:
                        e.SuppressKeyPress = true;

                        if (tabControlDados.SelectedTab == tabPageAERMAP)
                        {
                            AbrirTIF();
                        }
                        break;                    
                }
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.F1:
                        AbrirAjuda();
                        break;
                    case Keys.F11:
                        AbrirMapa();
                        break;
                    case Keys.F12:
                        AbrirBackup();
                        break;
                    case Keys.Escape:
                        this.Close();
                        break;
                    case Keys.Enter:
                        e.SuppressKeyPress = true;

                        if (tabControlDados.SelectedTab == tabPageAERMAP)
                        {
                            ExecutarAERMAP();
                        }
                        else if (tabControlDados.SelectedTab == tabPageAERMET)
                        {
                            ExecutarAERMET();
                        }
                        else if (tabControlDados.SelectedTab == tabPageAERMET)
                        {
                            ExecutarAERMOD();
                        }
                        break;
                }
            }
        }

        #endregion

        #region Eventos Parâmetros

        #region Eventos tbxCaminhoDataBase

        private void tbxCaminhoDataBase_Leave(object sender, EventArgs e)
        {
            if (this.ActiveControl == btnCaminhoDataBase)
            {
                return;
            }

            if (classeParametros.SalvarCaminhoDataBase(this, tbxCaminhoDataBase.Text))
            {
                if (string.IsNullOrEmpty(classeParametros.InicializarDataBase(this)))
                {
                    MsgBoxLIB.Show(this, "Não foi possível estabelecer conexão com o banco de dados.", "Erro", MessageBoxIcon.Error, MessageBoxButtons.OK);
                }
            }
            else
            {
                this.Close();
            }
        }

        private void tbxCaminhoDataBase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                classeParametros.ProcurarCaminhoDataBaseF2(this);
            }
        }

        private void btnCaminhoDataBase_Click(object sender, EventArgs e)
        {
            classeParametros.ProcurarCaminhoDataBaseF2(this);
        }

        #endregion

        #region Eventos btnAtualizacao

        private void btnAtualizacao_Click(object sender, EventArgs e)
        {
            AbrirAtualizacao();
        }

        #endregion

        #endregion

        #region Eventos AERMAP

        #region Eventos btnTIF

        private void btnTIF_Click(object sender, EventArgs e)
        {
            AbrirTIF();
        }

        #endregion

        #region Eventos btnFontesEmissao

        private void btnFontesEmissao_Click(object sender, EventArgs e)
        {
            AbrirFontesAERMAP();
        }

        #endregion

        #region Eventos btnDominio

        private void btnDominio_Click(object sender, EventArgs e)
        {
            AbrirDominioModelagem();
        }

        #endregion

        #region Eventos btnExecutarAERMAP

        private void btnExecutarAERMAP_Click(object sender, EventArgs e)
        {
            ExecutarAERMAP();
        }

        #endregion

        #region Eventos btnINP_AERMAP

        private void btnINP_AERMAP_Click(object sender, EventArgs e)
        {
            ExportarINP_AERMAP();
        }

        #endregion

        #region Eventos btnOUT_AERMAP

        private void btnOUT_AERMAP_Click(object sender, EventArgs e)
        {
            ExportarOUT_AERMAP();
        }

        #endregion

        #region Eventos btnROU_AERMAP

        private void btnROU_AERMAP_Click(object sender, EventArgs e)
        {
            ExportarROU_AERMAP();
        }

        #endregion

        #region Eventos btnSOU_AERMAP

        private void btnSOU_AERMAP_Click(object sender, EventArgs e)
        {
            ExportarSOU_AERMAP();
        }

        #endregion

        #endregion

        #region Eventos AERMET

        #region Eventos btnEditorSAM

        private void btnEditorSAM_Click(object sender, EventArgs e)
        {
            AbrirEditorSAM();
        }

        #endregion

        #region Eventos btnFSL

        private void btnFSL_Click(object sender, EventArgs e)
        {
            AbrirEditorFSL();
        }

        #endregion

        #region Eventos btnDefinicaoAERMET

        private void btnDefinicaoAERMET_Click(object sender, EventArgs e)
        {
            AbrirDefinioes_AEMET();
        }

        #endregion

        #region Eventos btnExecutarAERMET

        private void btnExecutarAERMET_Click(object sender, EventArgs e)
        {
            ExecutarAERMET();
        }

        #endregion

        #region Eventos btnINP1_AERMET

        private void btnINP1_AERMET_Click(object sender, EventArgs e)
        {
            ExportarINP1_AERMET();
        }

        #endregion

        #region Eventos btnINP2_AERMET

        private void btnINP2_AERMET_Click(object sender, EventArgs e)
        {
            ExportarINP2_AERMET();
        }

        #endregion

        #region Eventos btnPFL_AERMET

        private void btnPFL_AERMET_Click(object sender, EventArgs e)
        {
            ExportarPFL_AERMET();
        }

        #endregion

        #region Eventos btnSFC_AERMET

        private void btnSFC_AERMET_Click(object sender, EventArgs e)
        {
            ExportarSFC_AERMET();
        }

        #endregion

        #endregion

        #region Eventos AERMOD

        #region Eventos btnDefinicaoAERMOD

        private void btnDefinicaoAERMOD_Click(object sender, EventArgs e)
        {
            AbrirDefinioes_AEMOD();
        }

        #endregion

        #region Eventos btnParametrosAERMOD

        private void btnParametrosAERMOD_Click(object sender, EventArgs e)
        {
            AbrirParametros_AERMOD();
        }

        #endregion

        #region Eventos btnExecutarAERMOD

        private void btnExecutarAERMOD_Click(object sender, EventArgs e)
        {
            ExecutarAERMOD();
        }

        #endregion

        #region Eventos btnINP_AERMOD

        private void btnINP_AERMOD_Click(object sender, EventArgs e)
        {
            ExportarINP_AERMOD();
        }

        #endregion

        #region Eventos btnOUT1_AERMOD

        private void btnOUT1_AERMOD_Click(object sender, EventArgs e)
        {
            ExportarOUT1_AERMOD();
        }

        #endregion

        #region Eventos btnOUT2_AERMOD

        private void btnOUT2_AERMOD_Click(object sender, EventArgs e)
        {
            ExportarOUT2_AERMOD();
        }

        #endregion

        #region Eventos btnPLT_AERMOD

        private void btnPLT_AERMOD_Click(object sender, EventArgs e)
        {
            ExportarPLT_AERMOD();
        }

        #endregion

        #endregion

        #region Eventos statusStrip

        private void btnAjuda_Click(object sender, EventArgs e)
        {
            AbrirAjuda();
        }

        private void btnMapa_ButtonClick(object sender, EventArgs e)
        {
            AbrirMapa();
        }

        private void btnBackup_ButtonClick(object sender, EventArgs e)
        {
            AbrirBackup();
        }

        private void btnSair_ButtonClick(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Métodos

        #region Parâmetros

        /// <summary>
        /// Abrir tela de atualização.
        /// </summary>
        private void AbrirAtualizacao()
        {
            Base.VerificaVersao = true;
            this.Close();
        }

        #endregion

        #region AERMAP

        /// <summary>
        /// Abrir tela de ajuda.
        /// </summary>
        private void AbrirAjuda()
        {
            if (tabControlDados.SelectedTab == tabPageAERMAP)
            {
                AbrirAjudaAERMAP();
            }
            else if (tabControlDados.SelectedTab == tabPageAERMET)
            {
                AbrirAjudaAERMET();
            }
            else
            {
                AbrirAjudaAERMOD();
            }
        }

        /// <summary>
        /// Abrir tela de ajuda.
        /// </summary>
        private void AbrirAjudaAERMAP()
        {
            string caminho = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "Help");
            Help.ShowHelp(this, $"{caminho}\\AERMOD.chm", HelpNavigator.TopicId, "50");
            //Help.ShowHelp(this, $"{caminho}\\AERMOD.chm", HelpNavigator.TableOfContents, "10");
            //Help.ShowHelp(this, $"{caminho}\\AERMOD.chm", HelpNavigator.Index, "10");
        }

        /// <summary>
        /// Abrir tela de visualização de mapas.
        /// </summary>
        private void AbrirMapa()
        {
            //Interpolação utilizando o Microsoft R Open
            //R.exe CMD BATCH meuscript.r
            //https://datacornering.com/how-to-run-r-scripts-from-the-windows-command-line-cmd/
            //https://stackoverflow.com/questions/26274553/install-r-packages-from-windows-cmd

            if (tabControlDados.SelectedTab == tabPageAERMAP || tabControlDados.SelectedTab == tabPageAERMET)
            {
                AbrirMapaAERMAP();
            }            
            else if (tabControlDados.SelectedTab == tabPageAERMOD)
            {
                AbrirMapaAERMOD();
            }
        }

        /// <summary>
        /// Abrir tela de visualização de mapas.
        /// </summary>
        private void AbrirMapaAERMAP()
        {
            FrmMapa frmMapa = new FrmMapa(0);                      
            frmMapa.ShowDialogFade(this);

            frmMapa.Dispose();
        }

        /// <summary>
        /// Abrir consulta de arquivos TIF.
        /// </summary>
        private void AbrirTIF()
        {
            FrmArquivo frmArquivo = new FrmArquivo(TipoArquivo.TIF);
            frmArquivo.ShowDialogFade(this);
        }

        /// <summary>
        /// Abrir fontes AERMAP.
        /// </summary>
        private void AbrirFontesAERMAP()
        {
            FrmFonteAERMAP frmFonte = new FrmFonteAERMAP();
            frmFonte.ShowDialogFade(this);
        }

        /// <summary>
        /// Abrir o cadastro de domínio de modelagem.
        /// </summary>
        private void AbrirDominioModelagem()
        {
            SplashScreen.FindHandleParent();           
            SplashScreen.StyleProgress = StyleProgress.Marquee;
            SplashScreen.Location = SplashScreen.CalcLocation(this.Location, this.Size);

            Thread splashthread = new Thread(new ThreadStart(SplashScreen.ShowSplashScreen));
            splashthread.IsBackground = true;
            splashthread.Start();           

            FrmDominio frmDominio = new FrmDominio();            
            frmDominio.ShowDialogFade(this);
        }

        /// <summary>
        /// Executar processador AERMAP.
        /// </summary>
        private void ExecutarAERMAP()
        {
            string diretorio = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "AERMOD_BACKEND\\AERMAP");
            string[] arquivosTemporarios = Directory.GetFiles(diretorio);
            bool confirmado = false;

            FrmLoading frmLoading = new FrmLoading(this);

            Thread thr = new Thread(delegate ()
            {
                frmLoading.AtualizarStatus(1);

                #region Excluir últimos arquivos                

                foreach (var arquivoAtual in arquivosTemporarios)
                {
                    if (arquivoAtual.EndsWith(".exe") == false)
                    {
                        File.Delete(arquivoAtual);
                    }
                }

                #endregion

                frmLoading.AtualizarStatus(2);

                #region Arquivo TIF

                List<Tuple<byte[], string>> listaTIF = classeTIF.RetornarArquivoUso();

                if (listaTIF.Count == 0)
                {
                    this.Invoke(new Action(() => { MessageBox.Show(this, classeHelp.BuscarMensagem(8), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning); }));
                    return;
                }

                #endregion

                frmLoading.AtualizarStatus(3);

                #region Fonte emissora

                DataTable dtFontes = classeFonteAERMAP.RetornarRegistros();

                if (dtFontes.Rows.Count == 0)
                {
                    this.Invoke(new Action(() => { MessageBox.Show(this, classeHelp.BuscarMensagem(9), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning); }));
                    return;
                }

                #endregion

                frmLoading.AtualizarStatus(4);

                #region Domínio/Grade de modelagem

                DataTable dtDominio = classeDominio.RetornarRegistroUso();

                if (dtDominio.Rows.Count == 0)
                {
                    this.Invoke(new Action(() => { MessageBox.Show(this, classeHelp.BuscarMensagem(10), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning); }));
                    return;
                }

                int codigoDominio = dtDominio.Rows[0]["CODIGO"].ValidarValor<int>(0);

                int qtdGrade = classeDominio.RetornaQtdGrade(codigoDominio);

                if (qtdGrade == 0)
                {
                    this.Invoke(new Action(() => { MessageBox.Show(this, classeHelp.BuscarMensagem(90), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning); }));
                    return;
                }

                if (qtdGrade > 10)
                {
                    this.Invoke(new Action(() => { MessageBox.Show(this, classeHelp.BuscarMensagem(89), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning); }));
                    return;
                }

                ClsAERMAP_Cartesiano classeCartesiana = new ClsAERMAP_Cartesiano(Base.ConfiguracaoRede, codigoDominio);
                ClsAERMAP_CartesianoRede classeCartesianaElev = new ClsAERMAP_CartesianoRede(Base.ConfiguracaoRede, codigoDominio);
                ClsAERMAP_CartesianoDiscreto classeCartesianaDisc = new ClsAERMAP_CartesianoDiscreto(Base.ConfiguracaoRede, codigoDominio);
                ClsAERMAP_EVALFILE classeEVALFILE = new ClsAERMAP_EVALFILE(Base.ConfiguracaoRede, codigoDominio);

                DataTable dtGradeCartesiana = classeCartesiana.RetornaDadosUso();
                DataTable dtGradeCartesianaElev = classeCartesianaElev.RetornaDadosUso();
                DataTable dtGradeCartesianaDisc = classeCartesianaDisc.RetornaDadosUso();
                DataTable dtGradeEVALFILE = classeEVALFILE.RetornaDadosUso();

                #endregion

                frmLoading.AtualizarStatus(5);

                #region Montar arquivo (.INP)            

                if (Directory.Exists(diretorio) == false)
                {
                    this.Invoke(new Action(() => { MessageBox.Show(this, classeHelp.BuscarMensagem(11), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning); }));
                    return;
                }

                Arquivo.EscreverAERMAP("****************************************", 0, true);
                Arquivo.EscreverAERMAP("** AERMAP Control Pathway");
                Arquivo.EscreverAERMAP("****************************************");
                Arquivo.EscreverAERMAP("**");
                Arquivo.EscreverAERMAP("CO STARTING");
                Arquivo.EscreverAERMAP("   TITLEONE  Trabalho AERMOD");
                Arquivo.EscreverAERMAP("   TITLETWO  Inputs arquivo TIF");
                Arquivo.EscreverAERMAP("   TERRHGTS  EXTRACT");
                Arquivo.EscreverAERMAP("CO DATATYPE  NED     FILLGAPS");

                foreach (var arquivoTIF in listaTIF)
                {
                    Arquivo.EscreverAERMAP($"CO DATAFILE  {arquivoTIF.Item2}");
                }

                Arquivo.EscreverAERMAP($"   DOMAINXY  {dtDominio.Rows[0]["DOMINIO_X1"].ToString().Replace(",", ".")} {dtDominio.Rows[0]["DOMINIO_Y1"].ToString().Replace(",", ".")} {dtDominio.Rows[0]["ZONA_UTM"]} {dtDominio.Rows[0]["DOMINIO_X2"].ToString().Replace(",", ".")} {dtDominio.Rows[0]["DOMINIO_Y2"].ToString().Replace(",", ".")} {dtDominio.Rows[0]["ZONA_UTM"]}");
                Arquivo.EscreverAERMAP($"   ANCHORXY  0.0 0.0 0.0 0.0 {dtDominio.Rows[0]["ZONA_UTM"]} {dtDominio.Rows[0]["ZONA_GMT"]}");
                Arquivo.EscreverAERMAP("   RUNORNOT  RUN");
                Arquivo.EscreverAERMAP("CO FINISHED");
                Arquivo.EscreverAERMAP("            ");
                Arquivo.EscreverAERMAP("**");
                Arquivo.EscreverAERMAP("****************************************");
                Arquivo.EscreverAERMAP("** AERMAP Receptor Pathway");
                Arquivo.EscreverAERMAP("****************************************");
                Arquivo.EscreverAERMAP("**");
                Arquivo.EscreverAERMAP("**");
                Arquivo.EscreverAERMAP("SO STARTING");

                int count = 0;

                foreach (DataRow item in dtFontes.Rows)
                {
                    count++;

                    string tipoFonte = ((TipoFonte)Convert.ToInt32(item["TIPO"])).GetEnumDescription().PadRight(8, ' ');

                    Arquivo.EscreverAERMAP($"   LOCATION {count.ToString().PadRight(2, ' ')} {tipoFonte} {item["X"].ToString().Replace(",", ".")} {item["Y"].ToString().Replace(",", ".")}");
                }

                Arquivo.EscreverAERMAP(" ");
                Arquivo.EscreverAERMAP("SO FINISHED");
                Arquivo.EscreverAERMAP(" ");
                Arquivo.EscreverAERMAP("**");
                Arquivo.EscreverAERMAP("****************************************");
                Arquivo.EscreverAERMAP("** AERMAP Receptor Pathway");
                Arquivo.EscreverAERMAP("****************************************");
                Arquivo.EscreverAERMAP("**");
                Arquivo.EscreverAERMAP("**");
                Arquivo.EscreverAERMAP("RE STARTING");

                #region Grade cartesiana

                #region Grade cartesiana uniforme
                
                foreach (DataRow item in dtGradeCartesiana.Rows)
                {
                    int codigo = item["CODIGO"].ValidarValor<int>(0);
                    string descricao = item["DESCRICAO"].ToString();
                    decimal XINIT = item["XINIT"].ValidarValor<decimal>(0);
                    int colunasX = item["XNUM"].ValidarValor<int>(0);
                    decimal espacamentoX = item["XDELTA"].ValidarValor<decimal>(0);
                    decimal YINIT = item["YINIT"].ValidarValor<decimal>(0);
                    int colunasY = item["YNUM"].ValidarValor<int>(0);
                    decimal espacamentoY = item["YDELTA"].ValidarValor<decimal>(0);

                    string espacamentoX_st = espacamentoX.ToString().Replace(",", ".");
                    espacamentoX_st = espacamentoX_st.TrimEnd('0').TrimEnd('.');

                    string espacamentoY_st = espacamentoY.ToString().Replace(",", ".");
                    espacamentoY_st = espacamentoY_st.TrimEnd('0').TrimEnd('.');

                    Arquivo.EscreverAERMAP($"   GRIDCART {descricao} STA");
                    Arquivo.EscreverAERMAP($"                    XYINC {XINIT.ToString().Replace(",", ".")} {colunasX} {espacamentoX_st} {YINIT.ToString().Replace(",", ".")} {colunasY} {espacamentoY_st}");
                    Arquivo.EscreverAERMAP($"            {descricao} END");
                }

                #endregion

                if (dtGradeCartesiana.Rows.Count > 0 && dtGradeCartesianaElev.Rows.Count > 0)
                {
                    Arquivo.EscreverAERMAP("**");
                }

                #region Grade cartesiana elevação

                {                   
                    string valorX = string.Empty;
                    string valorY = string.Empty;
                    int qtdX = 0;

                    for (int i = 0; i < dtGradeCartesianaElev.Rows.Count; i++)
                    {
                        DataRow item = dtGradeCartesianaElev.Rows[i];

                        int codigo = item["CODIGO"].ValidarValor<int>(0);
                        string descricao = item["DESCRICAO"].ToString();
                        decimal XPNTS = item["XPNTS"].ValidarValor<decimal>(0);
                        decimal YPNTS = item["YPNTS"].ValidarValor<decimal>(0);

                        valorX += $" {XPNTS.ToString().Replace(",", ".")}";
                        qtdX++;

                        if (YPNTS > 0)
                        {
                            valorY += $" {YPNTS.ToString().Replace(",", ".")}";
                        }

                        int proximaLinha = i + 1;

                        if (proximaLinha >= dtGradeCartesianaElev.Rows.Count)
                        {
                            proximaLinha = -1;
                        }

                        int proximoCodigo = proximaLinha >= 0 ? dtGradeCartesianaElev.Rows[proximaLinha]["CODIGO"].ValidarValor<int>(0) : 0;

                        if (codigo != proximoCodigo)
                        {
                            #region Escreve no bloco de notas

                            Arquivo.EscreverAERMAP($"   GRIDCART {descricao} STA");
                            Arquivo.EscreverAERMAP($"                      XPNTS{valorX}");
                            Arquivo.EscreverAERMAP($"                      YPNTS{valorY}");

                            #region Elevação/Altura

                            DataTable dtElevacao = classeCartesianaElev.RetornaDadosElevacao(codigo);
                            foreach (DataRow itemElev in dtElevacao.Rows)
                            {
                                int SEQUENCIA = itemElev["SEQUENCIA"].ValidarValor<int>(0);
                                decimal ELEV = itemElev["ELEV"].ValidarValor<decimal>(0);
                                decimal FLAG = itemElev["FLAG"].ValidarValor<decimal>(0);

                                string elevacao = ELEV > 0 ? ELEV.ToString().Replace(",", ".") : "0.0";
                                string altura = FLAG > 0 ? FLAG.ToString().Replace(",", ".") : "0.0";

                                Arquivo.EscreverAERMAP($"                      ELEV {SEQUENCIA} {qtdX}*{elevacao}");
                                Arquivo.EscreverAERMAP($"                      FLAG {SEQUENCIA} {qtdX}*{altura}");
                            }

                            #endregion

                            Arquivo.EscreverAERMAP($"            {descricao} END");

                            #endregion

                            valorX = string.Empty;
                            valorY = string.Empty;
                            qtdX = 0;
                        }
                    }                    
                }

                #endregion

                if (dtGradeCartesianaElev.Rows.Count > 0 && dtGradeCartesianaDisc.Rows.Count > 0)
                {
                    Arquivo.EscreverAERMAP("**");
                }

                #region Grade cartesiana discreta

                foreach (DataRow item in dtGradeCartesianaDisc.Rows)
                {                    
                    decimal XCOORD = item["XCOORD"].ValidarValor<decimal>(0);                    
                    decimal YCOORD = item["YCOORD"].ValidarValor<decimal>(0);
                    decimal ZELEV = item["ZELEV"].ValidarValor<decimal>(0);
                    decimal ZFLAG = item["ZFLAG"].ValidarValor<decimal>(0);

                    string elevacao = ZELEV > 0 ? ZELEV.ToString().Replace(",", ".") : "0.0";
                    string altura = ZFLAG > 0 ? ZFLAG.ToString().Replace(",", ".") : "0.0";

                    Arquivo.EscreverAERMAP($"RE DISCCART {XCOORD.ToString().Replace(",", ".")} {YCOORD.ToString().Replace(",", ".")} {elevacao} {altura}");                    
                }

                #endregion

                if (dtGradeCartesianaDisc.Rows.Count > 0 && dtGradeEVALFILE.Rows.Count > 0)
                {
                    Arquivo.EscreverAERMAP("**");
                }

                #region Grade EVALFILE

                foreach (DataRow item in dtGradeEVALFILE.Rows)
                {
                    decimal XCOORD = item["XCOORD"].ValidarValor<decimal>(0);
                    decimal YCOORD = item["YCOORD"].ValidarValor<decimal>(0);
                    decimal ZELEV = item["ZELEV"].ValidarValor<decimal>(0);
                    decimal ZFLAG = item["ZFLAG"].ValidarValor<decimal>(0);
                    string ARCID = item["ARCID"].ToString();                    

                    Arquivo.EscreverAERMAP($"RE EVALCART {XCOORD.ToString().Replace(",", ".")} {YCOORD.ToString().Replace(",", ".")} {ZELEV.ToString().Replace(",", ".")} {ZFLAG.ToString().Replace(",", ".")} {ARCID}");
                }

                #endregion

                #endregion

                Arquivo.EscreverAERMAP("RE FINISHED");
                Arquivo.EscreverAERMAP(" ");
                Arquivo.EscreverAERMAP("**");
                Arquivo.EscreverAERMAP("****************************************");
                Arquivo.EscreverAERMAP("** AERMAP Output Pathway");
                Arquivo.EscreverAERMAP("****************************************");
                Arquivo.EscreverAERMAP("**");
                Arquivo.EscreverAERMAP("OU STARTING");
                Arquivo.EscreverAERMAP("   RECEPTOR  AERMAP.ROU");
                Arquivo.EscreverAERMAP("   SOURCLOC  AERMAP.SOU");
                Arquivo.EscreverAERMAP("OU FINISHED");

                #endregion

                frmLoading.AtualizarStatus(6);

                #region colocar arquivo TIF na pasta AERMAP

                foreach (var item in listaTIF)
                {
                    byte[] arquivoTIF = Funcoes.DecompressedGZip(item.Item1);
                    string caminhoTIF = $"{diretorio}\\{item.Item2}";

                    File.WriteAllBytes(caminhoTIF, arquivoTIF);
                }

                #endregion

                confirmado = true;
            });
            thr.Start();

            frmLoading.thread = thr;
            frmLoading.PermiteAbortarThread = false;
            frmLoading.Texto = "Preparando arquivo(s)...";
            frmLoading.Style = ProgressBarStyle.Blocks;
            frmLoading.Maximum = 6;
            frmLoading.ShowDialogFade(this);

            if (confirmado == false)
            {
                return;
            }

            #region Executar o AERMAP

            //string caminhoEXE = diretorio + "\\" + "aermap.exe";
            //string caminhoDIR = diretorio + "\\" + "AERMAP.INP";

            #region Teste 1

            FrmProcessoAERMAP frmProcesssoAERMAP = new FrmProcessoAERMAP(diretorio);
            frmProcesssoAERMAP.ShowDialogFade(this);

            //Process p = new Process()
            //{
            //    StartInfo = new ProcessStartInfo()
            //    {
            //        CreateNoWindow = false,
            //        WorkingDirectory = diretorio,
            //        WindowStyle = ProcessWindowStyle.Maximized,
            //        FileName = Path.Combine(diretorio, "aermap.exe"),
            //        Arguments = "",
            //        UseShellExecute = false,
            //        RedirectStandardOutput = true,
            //        RedirectStandardError = true

            //        //CreateNoWindow = false,
            //        //WorkingDirectory = diretorio,
            //        //WindowStyle = ProcessWindowStyle.Hidden,
            //        //FileName = "cmd.exe",
            //        //Arguments = "/c start aermap.exe",
            //        //UseShellExecute = false,
            //        //RedirectStandardOutput = true,
            //        //RedirectStandardError = true
            //    }
            //};
            //p.Start();

            //string cv_error = null;
            //Thread et = new Thread(() => { cv_error = p.StandardError.ReadLine(); });
            //et.Start();

            //string cv_out = null;
            //Thread ot = new Thread(() => { cv_out = p.StandardOutput.ReadLine(); });
            //ot.Start();

            ////string cv_error = null;
            ////Thread et = new Thread(() => { cv_error = p.StandardError.ReadToEnd(); });
            ////et.Start();

            ////string cv_out = null;
            ////Thread ot = new Thread(() => { cv_out = p.StandardOutput.ReadToEnd(); });
            ////ot.Start();

            //p.WaitForExit();
            //ot.Join();
            //et.Join();

            #endregion

            #region Teste 2

            //using (Process process = new Process())
            //{
            //    process.StartInfo.WorkingDirectory = diretorio;
            //    process.StartInfo.FileName = "cmd.exe";
            //    process.StartInfo.Arguments = "/c start aermap.exe";
            //    process.StartInfo.UseShellExecute = false;
            //    process.StartInfo.RedirectStandardOutput = true;
            //    process.StartInfo.RedirectStandardError = true;

            //    StringBuilder output = new StringBuilder();
            //    StringBuilder error = new StringBuilder();

            //    using (AutoResetEvent outputWaitHandle = new AutoResetEvent(false))
            //    using (AutoResetEvent errorWaitHandle = new AutoResetEvent(false))
            //    {
            //        process.OutputDataReceived += (sender, e) =>
            //        {
            //            if (e.Data == null)
            //            {
            //                outputWaitHandle.Set();
            //            }
            //            else
            //            {
            //                output.AppendLine(e.Data);
            //            }
            //        };
            //        process.ErrorDataReceived += (sender, e) =>
            //        {
            //            if (e.Data == null)
            //            {
            //                errorWaitHandle.Set();
            //            }
            //            else
            //            {
            //                error.AppendLine(e.Data);
            //            }
            //        };

            //        process.Start();

            //        process.BeginOutputReadLine();
            //        process.BeginErrorReadLine();

            //        if (process.WaitForExit(timeout) &&
            //            outputWaitHandle.WaitOne(timeout) &&
            //            errorWaitHandle.WaitOne(timeout))
            //        {
            //            // Process completed. Check process.ExitCode here.
            //        }
            //        else
            //        {
            //            // 
            //        }
            //    }

            #endregion

            #region Teste 3

            //// Start the child process.
            //Process p = new Process();
            //// Redirect the output stream of the child process.
            //p.StartInfo.WorkingDirectory = diretorio;
            //p.StartInfo.FileName = "cmd.exe";
            //p.StartInfo.Arguments = "/c start aermap.exe";
            //p.StartInfo.UseShellExecute = false;
            //p.StartInfo.RedirectStandardOutput = true;         
            //p.Start();
            //// Do not wait for the child process to exit before
            //// reading to the end of its redirected stream.
            //// p.WaitForExit();
            //// Read the output stream first and then wait.
            //string output = p.StandardOutput.ReadToEnd();
            //p.WaitForExit();

            #endregion

            #region Teste 4

            //this.Enabled = false;

            //ProcessStartInfo processStartInfo = new ProcessStartInfo();
            //processStartInfo.UseShellExecute = true;
            //processStartInfo.WorkingDirectory = diretorio;
            //processStartInfo.CreateNoWindow = false;
            //processStartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            //processStartInfo.RedirectStandardOutput = false;
            //processStartInfo.RedirectStandardError = false;

            ////running cmd.exe then .bat as argument
            //processStartInfo.FileName = Path.Combine(diretorio, "aermap.exe");
            //processStartInfo.Arguments = "";

            //Process process = Process.Start(processStartInfo);

            ////string output = process.StandardOutput.ReadToEnd();
            //process.WaitForExit();

            //this.Enabled = true;

            #endregion

            //System.Diagnostics.Process process = new System.Diagnostics.Process();
            //System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            //startInfo.UseShellExecute = false;
            //startInfo.CreateNoWindow = true;
            //startInfo.RedirectStandardOutput = true;
            //startInfo.RedirectStandardError = false;
            //startInfo.WorkingDirectory = diretorio;
            //startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //startInfo.FileName = "cmd.exe";
            //startInfo.Arguments = "/c start aermap.exe";
            //process.StartInfo = startInfo;

            //process.Start();
            //process.StandardOutput.ReadToEnd();
            //process.WaitForExit();          


            //try
            //{
            //    process.Kill();
            //}
            //catch { }
            //process.Dispose();





            ////

            //winRarProcess.Dispose();

            //Process.Start("G:\\AERMOD\\AERMOD\\bin\\Debug\\AERMOD_BACKEND\\AERMAP\\aermap.exe");

            //LaunchCommandLineApp(diretorio, "aermap.exe");

            //string caminho = $"{diretorio}\\aermap.exe";
            //Process process = Process.Start(caminho);
            //int id = process.Id;
            //Process tempProc = Process.GetProcessById(id);
            //this.Visible = false;
            //tempProc.WaitForExit();
            //this.Visible = true;

            //Process.Start("C:\\");

            //Process process = Process.Start(@"Data\myApp.exe");
            //int id = process.Id;
            //Process tempProc = Process.GetProcessById(id);
            //this.Visible = false;
            //tempProc.WaitForExit();
            //this.Visible = true;

            //https://zetcode.com/csharp/file/
            //https://stackoverflow.com/questions/139593/processstartinfo-hanging-on-waitforexit-why

            //Console.WriteLine("Press any key to run CMD...");
            //Console.ReadKey();

            //ProcessStartInfo processStartInfo = new ProcessStartInfo();
            //processStartInfo.FileName = caminho;
            ////processStartInfo.Arguments = "/c date /t";

            //processStartInfo.CreateNoWindow = false;
            //processStartInfo.UseShellExecute = false;
            //processStartInfo.RedirectStandardOutput = true;
            //processStartInfo.WindowStyle = ProcessWindowStyle.Normal;

            //Process process = new Process();
            //process.StartInfo = processStartInfo;
            //process.Start();

            //string output = process.StandardOutput.ReadToEnd();
            //process.WaitForExit();

            //Console.WriteLine("Current date (received from CMD):");
            //Console.Write(output);           

            arquivosTemporarios = Directory.GetFiles(diretorio);

            foreach (var arquivoAtual in arquivosTemporarios)
            {
                if (arquivoAtual.EndsWith(".dir"))
                {
                    MessageBox.Show(this, classeHelp.BuscarMensagem(16), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            #endregion

            #region Verificar resultado da execução (Erro/Sucesso)            

            string readText = File.ReadAllText($"{diretorio}\\aermap.out");

            if (readText.Contains("AERMAP Finishes Successfully") == false)
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(12), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);

                if (readText.Length > 0)
                {
                    ProcessStartInfo processStartInfo = new ProcessStartInfo();
                    processStartInfo.UseShellExecute = true;
                    processStartInfo.WorkingDirectory = diretorio;
                    processStartInfo.CreateNoWindow = false;
                    processStartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                    processStartInfo.RedirectStandardOutput = false;
                    processStartInfo.RedirectStandardError = false;

                    //running cmd.exe then .bat as argument
                    processStartInfo.FileName = Path.Combine(diretorio, "aermap.out");
                    processStartInfo.Arguments = "";

                    Process process = Process.Start(processStartInfo);
                }

                return;
            }

            #endregion

            frmLoading = new FrmLoading(this);

            thr = new Thread(delegate ()
            {
                #region Guardar os arquivos no banco

                frmLoading.AtualizarStatus(1);

                #region .INP

                FileStream arquivo_INP = File.Open(Path.Combine(diretorio, "AERMAP.INP"), FileMode.Open, FileAccess.Read, FileShare.None);

                var buffer = new byte[arquivo_INP.Length];
                using (arquivo_INP)
                {
                    arquivo_INP.Read(buffer, 0, Convert.ToInt32(arquivo_INP.Length));
                    arquivo_INP.Close();
                }

                byte[] arquivoCompactado_INP = Funcoes.CompressedGZip(buffer);

                #endregion

                frmLoading.AtualizarStatus(2);

                #region .OUT

                FileStream arquivo_OUT = File.Open(Path.Combine(diretorio, "aermap.out"), FileMode.Open, FileAccess.Read, FileShare.None);

                buffer = new byte[arquivo_OUT.Length];
                using (arquivo_OUT)
                {
                    arquivo_OUT.Read(buffer, 0, Convert.ToInt32(arquivo_OUT.Length));
                    arquivo_OUT.Close();
                }

                byte[] arquivoCompactado_OUT = Funcoes.CompressedGZip(buffer);

                #endregion

                frmLoading.AtualizarStatus(3);

                #region .ROU

                FileStream arquivo_ROU = File.Open(Path.Combine(diretorio, "AERMAP.ROU"), FileMode.Open, FileAccess.Read, FileShare.None);

                buffer = new byte[arquivo_ROU.Length];
                using (arquivo_ROU)
                {
                    arquivo_ROU.Read(buffer, 0, Convert.ToInt32(arquivo_ROU.Length));
                    arquivo_ROU.Close();
                }

                byte[] arquivoCompactado_ROU = Funcoes.CompressedGZip(buffer);

                #endregion

                frmLoading.AtualizarStatus(4);

                #region .SOU

                FileStream arquivo_SOU = File.Open(Path.Combine(diretorio, "AERMAP.SOU"), FileMode.Open, FileAccess.Read, FileShare.None);

                buffer = new byte[arquivo_SOU.Length];
                using (arquivo_SOU)
                {
                    arquivo_SOU.Read(buffer, 0, Convert.ToInt32(arquivo_SOU.Length));
                    arquivo_SOU.Close();
                }

                byte[] arquivoCompactado_SOU = Funcoes.CompressedGZip(buffer);

                #endregion

                classeAERMAP.SalvarArquivo(arquivoCompactado_INP, arquivoCompactado_OUT, arquivoCompactado_ROU, arquivoCompactado_SOU);

                #endregion

                #region Excluir últimos arquivos            

                frmLoading.AtualizarStatus(5);

                arquivosTemporarios = Directory.GetFiles(diretorio);

                foreach (var arquivoAtual in arquivosTemporarios)
                {
                    if (arquivoAtual.EndsWith(".exe") == false)
                    {
                        File.Delete(arquivoAtual);
                    }
                }

                #endregion
            });
            thr.Start();

            frmLoading.thread = thr;
            frmLoading.PermiteAbortarThread = false;
            frmLoading.Texto = "Salvando arquivo(s)...";
            frmLoading.Style = ProgressBarStyle.Blocks;
            frmLoading.Maximum = 5;
            frmLoading.ShowDialogFade(this);
        }

        /// <summary>
        /// Launch the application with some options set.
        /// </summary>
        /// <param name="caminho">Caminho do arquivo</param>
        /// <param name="executavel">Nome do arquivo executável</param>
        static void LaunchCommandLineApp(string caminho, string executavel)
        {
            // For the example
            //const string ex1 = "C:\\";
            //const string ex2 = "C:\\Dir";

            // Use ProcessStartInfo class
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.FileName = $"{caminho}\\{executavel}";
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //startInfo.Arguments = caminho; //"-f j -o \"" + ex1 + "\" -z 1.0 -s y " + caminho;
            //startInfo.Arguments = "-f j -o \"" + ex1 + "\" -z 1.0 -s y " + ex2;

            try
            {
                // Start the process with the info we specified.
                // Call WaitForExit and then the using statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    exeProcess.WaitForExit();
                }
            }
            catch
            {
                // Log error.
            }
        }

        /// <summary>
        /// Exportar arquivo (.INP).
        /// </summary>
        private void ExportarINP_AERMAP()
        {
            byte[] arquivo = classeAERMAP.RetornarArquivoINP();

            if (arquivo == null)
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(13), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string descricao = "AERMAP";
            string extencao = ".INP";

            byte[] result = Funcoes.DecompressedGZip(arquivo);

            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = descricao;
                sfd.DefaultExt = extencao;
                sfd.Filter = "INP File|.INP";

                DialogResult dr = sfd.ShowDialog(this);

                if (dr == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(sfd.FileName))
                    {
                        return;
                    }
                    else if (sfd.FileName.Contains(extencao) == false)
                    {
                        sfd.FileName = sfd.FileName + extencao;
                    }

                    using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create))
                    {
                        fs.Write(result, 0, result.Length);
                    }

                    MessageBox.Show(this, classeHelp.BuscarMensagem(14), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(15), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Exportar arquivo no formato (.SAM).
        /// </summary>
        private void ExportarOUT_AERMAP()
        {
            byte[] arquivo = classeAERMAP.RetornarArquivoOUT();

            if (arquivo == null)
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(13), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string descricao = "AERMAP";
            string extencao = ".OUT";

            byte[] result = Funcoes.DecompressedGZip(arquivo);

            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = descricao;
                sfd.DefaultExt = extencao;
                sfd.Filter = "OUT File|.OUT";

                DialogResult dr = sfd.ShowDialog(this);

                if (dr == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(sfd.FileName))
                    {
                        return;
                    }
                    else if (sfd.FileName.Contains(extencao) == false)
                    {
                        sfd.FileName = sfd.FileName + extencao;
                    }

                    using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create))
                    {
                        fs.Write(result, 0, result.Length);
                    }

                    MessageBox.Show(this, classeHelp.BuscarMensagem(14), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(15), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Exportar arquivo no formato (.SAM).
        /// </summary>
        private void ExportarROU_AERMAP()
        {
            byte[] arquivo = classeAERMAP.RetornarArquivoROU();

            if (arquivo == null)
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(13), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string descricao = "AERMAP";
            string extencao = ".ROU";

            byte[] result = Funcoes.DecompressedGZip(arquivo);

            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = descricao;
                sfd.DefaultExt = extencao;
                sfd.Filter = "ROU File|.ROU";

                DialogResult dr = sfd.ShowDialog(this);

                if (dr == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(sfd.FileName))
                    {
                        return;
                    }
                    else if (sfd.FileName.Contains(extencao) == false)
                    {
                        sfd.FileName = sfd.FileName + extencao;
                    }

                    using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create))
                    {
                        fs.Write(result, 0, result.Length);
                    }

                    MessageBox.Show(this, classeHelp.BuscarMensagem(14), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(15), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Exportar arquivo no formato (.SAM).
        /// </summary>
        private void ExportarSOU_AERMAP()
        {
            byte[] arquivo = classeAERMAP.RetornarArquivoSOU();

            if (arquivo == null)
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(13), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string descricao = "AERMAP";
            string extencao = ".SOU";

            byte[] result = Funcoes.DecompressedGZip(arquivo);

            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = descricao;
                sfd.DefaultExt = extencao;
                sfd.Filter = "SOU File|.SOU";

                DialogResult dr = sfd.ShowDialog(this);

                if (dr == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(sfd.FileName))
                    {
                        return;
                    }
                    else if (sfd.FileName.Contains(extencao) == false)
                    {
                        sfd.FileName = sfd.FileName + extencao;
                    }

                    using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create))
                    {
                        fs.Write(result, 0, result.Length);
                    }

                    MessageBox.Show(this, classeHelp.BuscarMensagem(14), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(15), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region AERMET

        /// <summary>
        /// Abrir tela de ajuda.
        /// </summary>
        private void AbrirAjudaAERMET()
        {
            string caminho = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "Help");
            Help.ShowHelp(this, $"{caminho}\\AERMOD.chm", HelpNavigator.TopicId, "50");
            //Help.ShowHelp(this, $"{caminho}\\AERMOD.chm", HelpNavigator.TableOfContents, "10");
            //Help.ShowHelp(this, $"{caminho}\\AERMOD.chm", HelpNavigator.Index, "10");
        }

        /// <summary>
        /// Abrir editor (.SAM).
        /// </summary>
        private void AbrirEditorSAM()
        {
            MessageBoxButton dialogResult = MsgBoxLIB.Show(this, classeHelp.BuscarMensagem(91), classeHelp.BuscarMensagem(2), MessageBoxIcon.Question, new MessageBoxButton[] { new MessageBoxButton() { Id = 1, Texto = "1 - &INMET", AtalhoNumerico = true }, new MessageBoxButton() { Id = 2, Texto = "2 - &MESONET", AtalhoNumerico = true } });
            if (dialogResult.Id == 1)
            {
                FrmSAM_INMET frmSAM = new FrmSAM_INMET();
                frmSAM.ShowDialogFade(this);
            }
            else if (dialogResult.Id == 2)
            {
                FrmSAM_MESONET frmSAM = new FrmSAM_MESONET();
                frmSAM.ShowDialogFade(this);
            }
        }

        /// <summary>
        /// Abrir editor FSL.
        /// </summary>
        private void AbrirEditorFSL()
        {
            FrmArquivo frmArquivo = new FrmArquivo(TipoArquivo.FSL);
            frmArquivo.ShowDialogFade(this);
        }

        /// <summary>
        /// Abrir definições AERMET.
        /// </summary>
        private void AbrirDefinioes_AEMET()
        {
            FrmDefinicaoAERMET frmDefinicao = new FrmDefinicaoAERMET();
            frmDefinicao.ShowDialogFade(this);
        }

        /// <summary>
        /// Executar processador AERMET.
        /// </summary>
        private void ExecutarAERMET()
        {
            string diretorio = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "AERMOD_BACKEND\\AERMET");
            string[] arquivosTemporarios = Directory.GetFiles(diretorio);
            bool confirmado = false;
            int codigo = 0;

            FrmLoading frmLoading = new FrmLoading(this);

            Thread thr = new Thread(delegate ()
            {
                frmLoading.AtualizarStatus(1);

                #region Excluir últimos arquivos                

                foreach (var arquivoAtual in arquivosTemporarios)
                {
                    if (arquivoAtual.EndsWith(".exe") == false)
                    {
                        File.Delete(arquivoAtual);
                    }
                }

                #endregion

                frmLoading.AtualizarStatus(2);

                #region Arquivo SAM                

                var arquivoSAM = classeSAM.RetornarArquivoUso();

                if (arquivoSAM == null)
                {
                    this.Invoke(new Action(() => { MessageBox.Show(this, classeHelp.BuscarMensagem(22), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning); }));
                    return;
                }

                #endregion

                frmLoading.AtualizarStatus(3);

                #region Arquivo FSL

                var arquivoFSL = classeFSL.RetornarArquivoUso();

                if (arquivoFSL == null)
                {
                    this.Invoke(new Action(() => { MessageBox.Show(this, classeHelp.BuscarMensagem(23), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning); }));
                    return;
                }

                #endregion

                frmLoading.AtualizarStatus(4);

                #region Definições

                var dtDefinicao = classeAERMET.RetornarRegistroUso();
                string local = string.Empty;
                string uf = string.Empty;
                DateTime periodoInicial = DateTime.MinValue;
                DateTime periodoFinal = DateTime.MinValue;

                FrequenciaSetor frequencia = FrequenciaSetor.ANUAL;
                Estacao estacao = Estacao.INVERNO;
                int setorInicial = 0;
                int setorFinal = 0;
                decimal albedo = 0;
                decimal bowen = 0;
                decimal rugosidade = 0;

                if (dtDefinicao.Item1.Rows.Count == 0)
                {
                    this.Invoke(new Action(() => { MessageBox.Show(this, classeHelp.BuscarMensagem(24), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning); }));
                    return;
                }
                else
                {
                    codigo = dtDefinicao.Item1.Rows[0]["CODIGO"].ValidarValor<int>(0);
                    local = dtDefinicao.Item1.Rows[0]["LOCAL"].ToString();
                    uf = ((UF)Convert.ToInt32(dtDefinicao.Item1.Rows[0]["ESTADO"])).GetEnumDescription();
                    periodoInicial = Convert.ToDateTime(dtDefinicao.Item1.Rows[0]["PERIODO_INICIAL"]);
                    periodoFinal = Convert.ToDateTime(dtDefinicao.Item1.Rows[0]["PERIODO_FINAL"]);
                }

                #endregion

                frmLoading.AtualizarStatus(5);

                #region Montar arquivo (AERMET_1.INP)            

                if (Directory.Exists(diretorio) == false)
                {
                    this.Invoke(new Action(() => { MessageBox.Show(this, classeHelp.BuscarMensagem(11), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning); }));
                    return;
                }

                Arquivo.EscreverAERMET_1("***************************************************************************", 0, true);
                Arquivo.EscreverAERMET_1("** AERMET Input File - ETAPA 1");
                Arquivo.EscreverAERMET_1($"** Local: {local} - {uf}");
                Arquivo.EscreverAERMET_1("***************************************************************************");
                Arquivo.EscreverAERMET_1(" ");
                Arquivo.EscreverAERMET_1("***************************************************************************");
                Arquivo.EscreverAERMET_1("** JOB - Definição dos arquivos de relatórios da primeira etapa");
                Arquivo.EscreverAERMET_1("***************************************************************************");
                Arquivo.EscreverAERMET_1("JOB");
                Arquivo.EscreverAERMET_1("** Resumo da compilação - Nome do arquivo de saída **");
                Arquivo.EscreverAERMET_1("   REPORT      REPORT.RP1");
                Arquivo.EscreverAERMET_1("** Relatório de erros e avisos da compilação - Nome do arquivo de saída **");
                Arquivo.EscreverAERMET_1("   MESSAGES    MESSAGE.MG1");
                Arquivo.EscreverAERMET_1(" ");
                Arquivo.EscreverAERMET_1("*************************************************************");
                Arquivo.EscreverAERMET_1("** UPPERAIR - Extração dos dados meteorológicos de altitude");
                Arquivo.EscreverAERMET_1("*************************************************************");
                Arquivo.EscreverAERMET_1("**");
                Arquivo.EscreverAERMET_1("UPPERAIR");
                Arquivo.EscreverAERMET_1("**  Indicação  do  arquivo  de  entrada  bem  como  sua  extensão  -  Dados  meteorológicos  de altitude **");
                Arquivo.EscreverAERMET_1($"   DATA        {arquivoFSL.Item2} FSL");
                Arquivo.EscreverAERMET_1("** Extração dos dados **");
                Arquivo.EscreverAERMET_1("   EXTRACT     EXTRACT_UA.IQA");
                Arquivo.EscreverAERMET_1("** Avaliação da qualidade dos dados - Preparando arquivo de entrada do Estágio 2 **");
                Arquivo.EscreverAERMET_1("   QAOUT       QAOUT_UA.OQA");
                Arquivo.EscreverAERMET_1("** Definição do intervalo de tempo para extração **");
                Arquivo.EscreverAERMET_1($"   XDATES      {periodoInicial.ToString("yyyy/MM/dd")} TO {periodoFinal.ToString("yyyy/MM/dd")}");
                Arquivo.EscreverAERMET_1("** Informações da estação meteorológica: ID - Latitude - Longitude - Fator de conversão para horário local - Altitude**");

                #region colocar arquivo FSL na pasta AERMET

                var FSL = Funcoes.DecompressedGZip(arquivoFSL.Item1);
                string caminhoFSL = $"{diretorio}\\{arquivoFSL.Item2}";
                File.WriteAllBytes(caminhoFSL, FSL);

                string localizacao = string.Empty;
                bool segundaLinha = false;
                foreach (string line in File.ReadLines(caminhoFSL))
                {
                    if (segundaLinha)
                    {
                        //LOCATION    83827 25.52S    54.58W 0 
                        localizacao = $"{line.Substring(16, 5)} {line.Substring(23, 6)}    {line.Substring(30, 6)} 0";

                        break;
                    }

                    segundaLinha = true;
                }

                #endregion

                Arquivo.EscreverAERMET_1($"   LOCATION    {localizacao}");
                Arquivo.EscreverAERMET_1(" ");
                Arquivo.EscreverAERMET_1("*************************************************************");
                Arquivo.EscreverAERMET_1("** SURFACE - Extração dos dados meteorológicos de superfície");
                Arquivo.EscreverAERMET_1("*************************************************************");
                Arquivo.EscreverAERMET_1("SURFACE");
                Arquivo.EscreverAERMET_1("**  Indicação  do  arquivo  de  entrada  bem  como  sua  extensão  -  Dados  meteorológicos  de superfície **");
                Arquivo.EscreverAERMET_1($"   DATA        {arquivoSAM.Item2} SAMSON");
                Arquivo.EscreverAERMET_1("** Extração dos dados **");
                Arquivo.EscreverAERMET_1("   EXTRACT     EXTRACT_SF.IQA");
                Arquivo.EscreverAERMET_1("** Avaliação da qualidade dos dados - Preparando arquivo de entrada do Estágio 2 **");
                Arquivo.EscreverAERMET_1("   QAOUT       QAOUT_SF.OQA");
                Arquivo.EscreverAERMET_1("** Definição do intervalo de tempo para extração **");
                Arquivo.EscreverAERMET_1($"   XDATES      {periodoInicial.ToString("yyyy/MM/dd")} TO {periodoFinal.ToString("yyyy/MM/dd")}");
                Arquivo.EscreverAERMET_1("** Informações da estação meteorológica: ID - Latitude - Longitude - Fator de conversão para horário local - Altitude **");

                #region colocar arquivo SAM na pasta AERMET

                var SAM = Funcoes.DecompressedGZip(arquivoSAM.Item1);
                string caminhoSAM = $"{diretorio}\\{arquivoSAM.Item2}";
                File.WriteAllBytes(caminhoSAM, SAM);

                localizacao = string.Empty;

                try
                {
                    foreach (string line in File.ReadLines(caminhoSAM))
                    {
                        //LOCATION    00820 24.53S    54.01W 0 392
                        string latitude = line.Substring(38, 6);
                        latitude = latitude.Replace(" ", ".").Replace("S", "");
                        latitude = latitude.TrimStart('0').TrimStart('.').TrimEnd('.');

                        string longitude = line.Substring(46, 7);
                        longitude = longitude.Replace(" ", ".").Replace("W", "");                       
                        longitude = longitude.TrimStart('0').TrimStart('.').TrimEnd('.');

                        string elevacao = line.Substring(55, 4);
                        elevacao = elevacao.Trim(' ');

                        localizacao = $"{line.Substring(1, 5)} {latitude + "S"}    {longitude + "W"} 0 {elevacao}";

                        break;
                    }
                }
                catch
                {
                    this.Invoke(new Action(() => { MessageBox.Show(this, classeHelp.BuscarMensagem(81), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning); }));
                    return;
                }

                #endregion

                Arquivo.EscreverAERMET_1($"   LOCATION    {localizacao}");

                #endregion

                frmLoading.AtualizarStatus(6);

                #region Montar arquivo (AERMET_2.INP)

                Arquivo.EscreverAERMET_2("***************************************************************************");
                Arquivo.EscreverAERMET_2("** AERMET Input File - ETAPA 2 ** AUTOR : UTFPR");
                Arquivo.EscreverAERMET_2($"** Data: {periodoInicial}");
                Arquivo.EscreverAERMET_2("** Projeto: Modelagem das fontes emissoras");
                Arquivo.EscreverAERMET_2($"** Local: {local} - {uf}");
                Arquivo.EscreverAERMET_2("***************************************************************************");
                Arquivo.EscreverAERMET_2(" ");
                Arquivo.EscreverAERMET_2("***********************************************************");
                Arquivo.EscreverAERMET_2("** JOB - Definição dos arquivos de relatórios do Estágio 2");
                Arquivo.EscreverAERMET_2("***********************************************************");
                Arquivo.EscreverAERMET_2("JOB");
                Arquivo.EscreverAERMET_2("** Resumo da compilação - Nome do arquivo de saída **");
                Arquivo.EscreverAERMET_2("   REPORT      REPORT.RP2");
                Arquivo.EscreverAERMET_2("** Relatório de erros e avisos da compilação - Nome do arquivo de saída **");
                Arquivo.EscreverAERMET_2("   MESSAGES    MESSAGE.MG2");
                Arquivo.EscreverAERMET_2(" ");
                Arquivo.EscreverAERMET_2("***********************************************************");
                Arquivo.EscreverAERMET_2("** UPPERAIR - Entrada de dados meteorológicos de altitude");
                Arquivo.EscreverAERMET_2("***********************************************************");
                Arquivo.EscreverAERMET_2("UPPERAIR");
                Arquivo.EscreverAERMET_2("** Indicação do dado de entrada (arquivo de saída do Estágio 1) **");
                Arquivo.EscreverAERMET_2("   QAOUT       QAOUT_UA.OQA");
                Arquivo.EscreverAERMET_2(" ");
                Arquivo.EscreverAERMET_2("***********************************************************");
                Arquivo.EscreverAERMET_2("** SURFACE - Entrada de dados meteorológicos de superfície");
                Arquivo.EscreverAERMET_2("***********************************************************");
                Arquivo.EscreverAERMET_2("SURFACE");
                Arquivo.EscreverAERMET_2("** Indicação do dado de entrada (arquivo de saída do Estágio 1) **");
                Arquivo.EscreverAERMET_2("   QAOUT       QAOUT_SF.OQA");
                Arquivo.EscreverAERMET_2(" ");
                //Arquivo.EscreverAERMET_2("******************************************************************************");
                //Arquivo.EscreverAERMET_2("** MERGE - Combinação dos dados de superfície e altitude em blocos de 24 horas");
                //Arquivo.EscreverAERMET_2("******************************************************************************");
                //Arquivo.EscreverAERMET_2("MERGE");
                //Arquivo.EscreverAERMET_2("** Definição do arquivo de saída do Estágio 2 **");
                //Arquivo.EscreverAERMET_2("   OUTPUT      OUTPUT_MERGE.MRG");
                //Arquivo.EscreverAERMET_2("** Definição do intervalo de tempo para a combinação dos dados **");
                //Arquivo.EscreverAERMET_2($"   XDATES      {periodoInicial.ToString("yyyy/MM/dd")} TO {periodoFinal.ToString("yyyy/MM/dd")}");
                Arquivo.EscreverAERMET_2("*******************************************");
                Arquivo.EscreverAERMET_2("** METPREP - Cálculo dos parâmetros da CLP");
                Arquivo.EscreverAERMET_2("*******************************************");
                Arquivo.EscreverAERMET_2("METPREP");
                //Arquivo.EscreverAERMET_3("** Indicação do dado de entrada - Arquivo de saída do Estágio 2 **");
                //Arquivo.EscreverAERMET_3("   DATA        OUTPUT_MERGE.MRG");
                //Arquivo.EscreverAERMET_3("   MODEL       AERMOD");
                Arquivo.EscreverAERMET_2(" ");
                Arquivo.EscreverAERMET_2("** Definição dos arquivos de saída - Surface File (SFC) e Profile File (PFL) **");
                Arquivo.EscreverAERMET_2("   OUTPUT      AERMET.SFC");
                Arquivo.EscreverAERMET_2("   PROFILE     AERMET.PFL");
                Arquivo.EscreverAERMET_2(" ");
                Arquivo.EscreverAERMET_2("** Definição do intervalo de tempo para cálculos **");
                Arquivo.EscreverAERMET_2($"   XDATES      {periodoInicial.ToString("yyyy/MM/dd")} TO {periodoFinal.ToString("yyyy/MM/dd")}");
                Arquivo.EscreverAERMET_2(" ");
                Arquivo.EscreverAERMET_2("** METHOD - Opções avançadas do modelo **");
                Arquivo.EscreverAERMET_2("   METHOD      REFLEVEL  SUBNWS");
                Arquivo.EscreverAERMET_2("   METHOD      WIND_DIR  NORAND");
                Arquivo.EscreverAERMET_2(" ");
                Arquivo.EscreverAERMET_2("** Definição da altura do instrumento de medição **");
                Arquivo.EscreverAERMET_2("   NWS_HGT     WIND 10.0");
                Arquivo.EscreverAERMET_2(" ");
                Arquivo.EscreverAERMET_2("** DEFINIÇÃO DAS CARACTERÍSTICAS DE SUPERFÍCIE **");
                Arquivo.EscreverAERMET_2("**FREQ_SECT: Frequência da variação das características(Período) - Número de setores classificados**");

                #region Frequência

                //ANNUAL, SEASONAL or MONTHLY

                int countFrequencia = 0;
                foreach (DataRow linha in dtDefinicao.Item2.Rows)
                {
                    countFrequencia++;

                    frequencia = (FrequenciaSetor)linha["FREQUENCIA"];
                    string descricaoFrequencia = string.Empty;
                    switch (frequencia)
                    {
                        case FrequenciaSetor.ANUAL:
                            descricaoFrequencia = "ANNUAL";
                            break;
                        case FrequenciaSetor.MENSAL:
                            descricaoFrequencia = "MONTHLY";
                            break;
                        case FrequenciaSetor.SAZONAL:
                            descricaoFrequencia = "SEASONAL";
                            break;
                    }

                    Arquivo.EscreverAERMET_2($"   FREQ_SECT   {descricaoFrequencia} {countFrequencia}");
                }

                #endregion

                Arquivo.EscreverAERMET_2(" ");
                Arquivo.EscreverAERMET_2("** Divisão dos setores: ID - Início (graus) - Fim do setor (graus) **");

                #region Setores

                int countSetor = 0;
                foreach (DataRow linha in dtDefinicao.Item2.Rows)
                {
                    countSetor++;

                    setorInicial = Convert.ToInt32(linha["SETOR_INICIAL"]);
                    setorFinal = Convert.ToInt32(linha["SETOR_FINAL"]);

                    Arquivo.EscreverAERMET_2($"   SECTOR      {countSetor}   {setorInicial} {setorFinal}");
                }

                #endregion

                Arquivo.EscreverAERMET_2(" ");
                Arquivo.EscreverAERMET_2("** SITE_CHAR: Período - ID - Albedo - Razão de Bowen - Rugosidade **");
                Arquivo.EscreverAERMET_2("**             Season Sect  Alb    Bo    Zo");

                #region Característica (Albedo/Bowen/Rugosidade

                int countCaracteristica = 0;
                foreach (DataRow linha in dtDefinicao.Item2.Rows)
                {
                    countCaracteristica++;

                    estacao = (Estacao)linha["ESTACAO"];
                    albedo = Convert.ToDecimal(linha["ALBEDO"]);
                    bowen = Convert.ToDecimal(linha["BOWEN"]);
                    rugosidade = Convert.ToDecimal(linha["RUGOSIDADE"]);

                    Arquivo.EscreverAERMET_2($"   SITE_CHAR        {(int)estacao}    {countCaracteristica}  {albedo.ToString().Replace(",", ".")}  {bowen.ToString().Replace(",", ".")}  {rugosidade.ToString().Replace(",", ".")}");
                }

                #endregion

                #endregion

                frmLoading.AtualizarStatus(7);

                #region Montar arquivo (AERMET_3.INP)

                //Arquivo.EscreverAERMET_3("***************************************************************************");
                //Arquivo.EscreverAERMET_3("** AERMET Input File - ETAPA 3 ** AUTOR : UTFPR");
                //Arquivo.EscreverAERMET_3($"** Local: {local} - {uf}");
                //Arquivo.EscreverAERMET_3("***************************************************************************");
                //Arquivo.EscreverAERMET_3(" ");
                //Arquivo.EscreverAERMET_3("**********************************************************");
                //Arquivo.EscreverAERMET_3("** JOB - Definição dos arquivos de relatórios do Estágio 3");
                //Arquivo.EscreverAERMET_3("**********************************************************");
                //Arquivo.EscreverAERMET_3("JOB");
                //Arquivo.EscreverAERMET_3("** Resumo da compilação - Nome do arquivo de saída **");
                //Arquivo.EscreverAERMET_3("   REPORT      REPORT.RP3");
                //Arquivo.EscreverAERMET_3("** Relatório de erros e avisos da compilação - Nome do arquivo de saída **");
                //Arquivo.EscreverAERMET_3("   MESSAGES    MESSAGE.MG3");
                //Arquivo.EscreverAERMET_3(" ");
                //Arquivo.EscreverAERMET_3("*******************************************");
                //Arquivo.EscreverAERMET_3("** METPREP - Cálculo dos parâmetros da CLP");
                //Arquivo.EscreverAERMET_3("*******************************************");
                //Arquivo.EscreverAERMET_3("METPREP");
                //Arquivo.EscreverAERMET_3("** Indicação do dado de entrada - Arquivo de saída do Estágio 2 **");
                //Arquivo.EscreverAERMET_3("   DATA        OUTPUT_MERGE.MRG");
                //Arquivo.EscreverAERMET_3("   MODEL       AERMOD");
                //Arquivo.EscreverAERMET_3(" ");
                //Arquivo.EscreverAERMET_3("** Definição dos arquivos de saída - Surface File (SFC) e Profile File (PFL) **");
                //Arquivo.EscreverAERMET_3("   OUTPUT      AERMET.SFC");
                //Arquivo.EscreverAERMET_3("   PROFILE     AERMET.PFL");
                //Arquivo.EscreverAERMET_3(" ");
                //Arquivo.EscreverAERMET_3("** Definição do intervalo de tempo para cálculos **");
                //Arquivo.EscreverAERMET_3($"   XDATES      {periodoInicial.ToString("yyyy/MM/dd")} TO {periodoFinal.ToString("yyyy/MM/dd")}");
                //Arquivo.EscreverAERMET_3(" ");
                //Arquivo.EscreverAERMET_3("** METHOD - Opções avançadas do modelo **");
                //Arquivo.EscreverAERMET_3("   METHOD      REFLEVEL  SUBNWS");
                //Arquivo.EscreverAERMET_3("   METHOD      WIND_DIR  NORAND");
                //Arquivo.EscreverAERMET_3(" ");
                //Arquivo.EscreverAERMET_3("** Definição da altura do instrumento de medição **");
                //Arquivo.EscreverAERMET_3("   NWS_HGT     WIND 10.0");
                //Arquivo.EscreverAERMET_3(" ");
                //Arquivo.EscreverAERMET_3("** DEFINIÇÃO DAS CARACTERÍSTICAS DE SUPERFÍCIE **");
                //Arquivo.EscreverAERMET_3("**FREQ_SECT: Frequência da variação das características(Período) - Número de setores classificados**");

                //#region Frequência

                ////ANNUAL, SEASONAL or MONTHLY

                //int countFrequencia = 0;
                //foreach (DataRow linha in dtDefinicao.Item2.Rows)
                //{
                //    countFrequencia++;

                //    frequencia = (FrequenciaSetor)linha["FREQUENCIA"];
                //    string descricaoFrequencia = string.Empty;
                //    switch (frequencia)
                //    {
                //        case FrequenciaSetor.ANUAL:
                //            descricaoFrequencia = "ANNUAL";
                //            break;
                //        case FrequenciaSetor.MENSAL:
                //            descricaoFrequencia = "MONTHLY";
                //            break;
                //        case FrequenciaSetor.SAZONAL:
                //            descricaoFrequencia = "SEASONAL";
                //            break;
                //    }

                //    Arquivo.EscreverAERMET_3($"   FREQ_SECT   {descricaoFrequencia} {countFrequencia}");
                //}

                //#endregion

                //Arquivo.EscreverAERMET_3(" ");
                //Arquivo.EscreverAERMET_3("** Divisão dos setores: ID - Início (graus) - Fim do setor (graus) **");

                //#region Setores

                //int countSetor = 0;
                //foreach (DataRow linha in dtDefinicao.Item2.Rows)
                //{
                //    countSetor++;

                //    setorInicial = Convert.ToInt32(linha["SETOR_INICIAL"]);
                //    setorFinal = Convert.ToInt32(linha["SETOR_FINAL"]);

                //    Arquivo.EscreverAERMET_3($"   SECTOR      {countSetor}   {setorInicial} {setorFinal}");
                //}

                //#endregion

                //Arquivo.EscreverAERMET_3(" ");
                //Arquivo.EscreverAERMET_3("** SITE_CHAR: Período - ID - Albedo - Razão de Bowen - Rugosidade **");
                //Arquivo.EscreverAERMET_3("**             Season Sect  Alb    Bo    Zo");

                //#region Característica (Albedo/Bowen/Rugosidade

                //int countCaracteristica = 0;
                //foreach (DataRow linha in dtDefinicao.Item2.Rows)
                //{
                //    countCaracteristica++;

                //    estacao = (Estacao)linha["ESTACAO"];
                //    albedo = Convert.ToDecimal(linha["ALBEDO"]);
                //    bowen = Convert.ToDecimal(linha["BOWEN"]);
                //    rugosidade = Convert.ToDecimal(linha["RUGOSIDADE"]);

                //    Arquivo.EscreverAERMET_3($"   SITE_CHAR        {(int)estacao}    {countCaracteristica}  {albedo.ToString().Replace(",", ".")}  {bowen.ToString().Replace(",", ".")}  {rugosidade.ToString().Replace(",", ".")}");
                //}

                //#endregion

                #endregion                                

                confirmado = true;
            });
            thr.Start();

            frmLoading.thread = thr;
            frmLoading.PermiteAbortarThread = false;
            frmLoading.Texto = "Preparando arquivo(s)...";
            frmLoading.Style = ProgressBarStyle.Blocks;
            frmLoading.Maximum = 7;
            frmLoading.ShowDialogFade(this);

            if (confirmado == false)
            {
                return;
            }

            #region Executar o AERMET

            List<string> lstSucesso = new List<string>()
            {
                "AERMET Data Processing Finished Successfully",
                "AERMET FINISHED SUCCESSFULLY"
            };

            #region Primeira etapa

            File.Copy($"{diretorio}\\AERMET_1.INP", $"{diretorio}\\AERMET.INP", true);
            File.Delete($"{diretorio}\\AERMET_1.INP");

            FrmProcessoAERMET frmProcesssoAERMET = new FrmProcessoAERMET(diretorio);
            frmProcesssoAERMET.ShowDialogFade(this);

            File.Copy($"{diretorio}\\AERMET.INP", $"{diretorio}\\AERMET_1.INP", true);
            File.Delete($"{diretorio}\\AERMET.INP");

            #region Verificar resultado da execução (Erro/Sucesso)            

            string readText = string.Empty;
            try
            {
                readText = File.ReadAllText($"{diretorio}\\REPORT.RP1");
            }
            catch
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(16), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK);
                return;
            }

            if (lstSucesso.Any(I => readText.Contains(I)) == false)
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(27), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK);

                if (readText.Length > 0)
                {
                    ProcessStartInfo processStartInfo = new ProcessStartInfo();
                    processStartInfo.UseShellExecute = true;
                    processStartInfo.WorkingDirectory = diretorio;
                    processStartInfo.CreateNoWindow = false;
                    processStartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                    processStartInfo.RedirectStandardOutput = false;
                    processStartInfo.RedirectStandardError = false;

                    //running cmd.exe then .bat as argument
                    processStartInfo.FileName = Path.Combine(diretorio, "REPORT.RP1");
                    processStartInfo.Arguments = "";

                    Process process = Process.Start(processStartInfo);
                }

                return;
            }

            #endregion

            #endregion

            #region Segunda etapa

            File.Copy($"{diretorio}\\AERMET_2.INP", $"{diretorio}\\AERMET.INP", true);
            File.Delete($"{diretorio}\\AERMET_2.INP");

            frmProcesssoAERMET = new FrmProcessoAERMET(diretorio);
            frmProcesssoAERMET.ShowDialogFade(this);

            File.Copy($"{diretorio}\\AERMET.INP", $"{diretorio}\\AERMET_2.INP", true);
            File.Delete($"{diretorio}\\AERMET.INP");

            #region Verificar resultado da execução (Erro/Sucesso)

            try
            {
                readText = File.ReadAllText($"{diretorio}\\REPORT.RP2");
            }
            catch
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(16), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK);
                return;
            }

            if (lstSucesso.Any(I => readText.Contains(I)) == false)
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(27), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK);

                if (readText.Length > 0)
                {
                    ProcessStartInfo processStartInfo = new ProcessStartInfo();
                    processStartInfo.UseShellExecute = true;
                    processStartInfo.WorkingDirectory = diretorio;
                    processStartInfo.CreateNoWindow = false;
                    processStartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                    processStartInfo.RedirectStandardOutput = false;
                    processStartInfo.RedirectStandardError = false;

                    //running cmd.exe then .bat as argument
                    processStartInfo.FileName = Path.Combine(diretorio, "REPORT.RP2");
                    processStartInfo.Arguments = "";

                    Process process = Process.Start(processStartInfo);
                }

                return;
            }

            #endregion

            #endregion

            #region Terceira etapa

            //File.Copy($"{diretorio}\\AERMET_3.INP", $"{diretorio}\\AERMET.INP", true);
            //File.Delete($"{diretorio}\\AERMET_3.INP");

            //frmProcesssoAERMET = new FrmProcessoAERMET(diretorio);
            //frmProcesssoAERMET.ShowDialogFade(this);

            //File.Copy($"{diretorio}\\AERMET.INP", $"{diretorio}\\AERMET_3.INP", true);
            //File.Delete($"{diretorio}\\AERMET.INP");

            #region Verificar resultado da execução (Erro/Sucesso)

            //try
            //{
            //    readText = File.ReadAllText($"{diretorio}\\REPORT.RP3");
            //}
            //catch
            //{
            //    MessageBox.Show(this, classeHelp.BuscarMensagem(16), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK);
            //    return;
            //}

            //if (lstSucesso.Any(I => readText.Contains(I)) == false)
            //{
            //    MessageBox.Show(this, classeHelp.BuscarMensagem(27), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK);

            //    if (readText.Length > 0)
            //    {
            //        ProcessStartInfo processStartInfo = new ProcessStartInfo();
            //        processStartInfo.UseShellExecute = true;
            //        processStartInfo.WorkingDirectory = diretorio;
            //        processStartInfo.CreateNoWindow = false;
            //        processStartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            //        processStartInfo.RedirectStandardOutput = false;
            //        processStartInfo.RedirectStandardError = false;

            //        //running cmd.exe then .bat as argument
            //        processStartInfo.FileName = Path.Combine(diretorio, "REPORT.RP3");
            //        processStartInfo.Arguments = "";

            //        Process process = Process.Start(processStartInfo);
            //    }

            //    return;
            //}

            #endregion

            #endregion

            #endregion

            frmLoading = new FrmLoading(this);

            thr = new Thread(delegate ()
            {
                #region Guardar os arquivos no banco

                frmLoading.AtualizarStatus(1);

                #region 1.INP

                FileStream arquivo1_INP = File.Open(Path.Combine(diretorio, "AERMET_1.INP"), FileMode.Open, FileAccess.Read, FileShare.None);

                var buffer = new byte[arquivo1_INP.Length];
                using (arquivo1_INP)
                {
                    arquivo1_INP.Read(buffer, 0, Convert.ToInt32(arquivo1_INP.Length));
                    arquivo1_INP.Close();
                }

                byte[] arquivoCompactado1_INP = Funcoes.CompressedGZip(buffer);

                #endregion

                frmLoading.AtualizarStatus(2);

                #region 2.INP

                FileStream arquivo2_INP = File.Open(Path.Combine(diretorio, "AERMET_2.INP"), FileMode.Open, FileAccess.Read, FileShare.None);

                buffer = new byte[arquivo2_INP.Length];
                using (arquivo2_INP)
                {
                    arquivo2_INP.Read(buffer, 0, Convert.ToInt32(arquivo2_INP.Length));
                    arquivo2_INP.Close();
                }

                byte[] arquivoCompactado2_INP = Funcoes.CompressedGZip(buffer);

                #endregion

                frmLoading.AtualizarStatus(3);

                #region .PFL

                FileStream arquivo_PFL = File.Open(Path.Combine(diretorio, "AERMET.PFL"), FileMode.Open, FileAccess.Read, FileShare.None);

                buffer = new byte[arquivo_PFL.Length];
                using (arquivo_PFL)
                {
                    arquivo_PFL.Read(buffer, 0, Convert.ToInt32(arquivo_PFL.Length));
                    arquivo_PFL.Close();
                }

                byte[] arquivoCompactado_PFL = Funcoes.CompressedGZip(buffer);

                #endregion

                frmLoading.AtualizarStatus(4);

                #region .SFC

                FileStream arquivo_SFC = File.Open(Path.Combine(diretorio, "AERMET.SFC"), FileMode.Open, FileAccess.Read, FileShare.None);

                buffer = new byte[arquivo_SFC.Length];
                using (arquivo_SFC)
                {
                    arquivo_SFC.Read(buffer, 0, Convert.ToInt32(arquivo_SFC.Length));
                    arquivo_SFC.Close();
                }

                byte[] arquivoCompactado_SFC = Funcoes.CompressedGZip(buffer);

                #endregion                

                classeAERMET.SalvarArquivo(codigo, arquivoCompactado1_INP, arquivoCompactado2_INP, arquivoCompactado_PFL, arquivoCompactado_SFC);

                #endregion

                frmLoading.AtualizarStatus(5);

                #region Excluir últimos arquivos            

                arquivosTemporarios = Directory.GetFiles(diretorio);

                foreach (var arquivoAtual in arquivosTemporarios)
                {
                    if (arquivoAtual.EndsWith(".exe") == false)
                    {
                        File.Delete(arquivoAtual);
                    }
                }

                #endregion
            });
            thr.Start();

            frmLoading.thread = thr;
            frmLoading.PermiteAbortarThread = false;
            frmLoading.Texto = "Salvando arquivo(s)...";
            frmLoading.Style = ProgressBarStyle.Blocks;
            frmLoading.Maximum = 5;
            frmLoading.ShowDialogFade(this);
        }

        /// <summary>
        /// Exportar INP1 AERMET.
        /// </summary>
        private void ExportarINP1_AERMET()
        {
            int codigo = classeAERMET.RetornarCodigoUso();

            byte[] arquivo = classeAERMET.RetornarArquivoINP1(codigo);

            if (arquivo == null)
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(13), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string descricao = "AERMET_1";
            string extencao = ".INP";

            byte[] result = Funcoes.DecompressedGZip(arquivo);

            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = descricao;
                sfd.DefaultExt = extencao;
                sfd.Filter = "INP File|.INP";

                DialogResult dr = sfd.ShowDialog(this);

                if (dr == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(sfd.FileName))
                    {
                        return;
                    }
                    else if (sfd.FileName.Contains(extencao) == false)
                    {
                        sfd.FileName = sfd.FileName + extencao;
                    }

                    using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create))
                    {
                        fs.Write(result, 0, result.Length);
                    }

                    MessageBox.Show(this, classeHelp.BuscarMensagem(14), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(15), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Exportar INP2 AERMET.
        /// </summary>
        private void ExportarINP2_AERMET()
        {
            int codigo = classeAERMET.RetornarCodigoUso();

            byte[] arquivo = classeAERMET.RetornarArquivoINP2(codigo);

            if (arquivo == null)
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(13), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string descricao = "AERMET_2";
            string extencao = ".INP";

            byte[] result = Funcoes.DecompressedGZip(arquivo);

            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = descricao;
                sfd.DefaultExt = extencao;
                sfd.Filter = "INP File|.INP";

                DialogResult dr = sfd.ShowDialog(this);

                if (dr == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(sfd.FileName))
                    {
                        return;
                    }
                    else if (sfd.FileName.Contains(extencao) == false)
                    {
                        sfd.FileName = sfd.FileName + extencao;
                    }

                    using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create))
                    {
                        fs.Write(result, 0, result.Length);
                    }

                    MessageBox.Show(this, classeHelp.BuscarMensagem(14), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(15), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Exportar PFL AERMET.
        /// </summary>
        private void ExportarPFL_AERMET()
        {
            int codigo = classeAERMET.RetornarCodigoUso();

            byte[] arquivo = classeAERMET.RetornarArquivoPFL(codigo);

            if (arquivo == null)
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(13), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string descricao = "AERMET";
            string extencao = ".PFL";

            byte[] result = Funcoes.DecompressedGZip(arquivo);

            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = descricao;
                sfd.DefaultExt = extencao;
                sfd.Filter = "PFL File|.PFL";

                DialogResult dr = sfd.ShowDialog(this);

                if (dr == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(sfd.FileName))
                    {
                        return;
                    }
                    else if (sfd.FileName.Contains(extencao) == false)
                    {
                        sfd.FileName = sfd.FileName + extencao;
                    }

                    using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create))
                    {
                        fs.Write(result, 0, result.Length);
                    }

                    MessageBox.Show(this, classeHelp.BuscarMensagem(14), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(15), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Exportar SFC AERMET.
        /// </summary>
        private void ExportarSFC_AERMET()
        {
            int codigo = classeAERMET.RetornarCodigoUso();

            byte[] arquivo = classeAERMET.RetornarArquivoSFC(codigo);

            if (arquivo == null)
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(13), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string descricao = "AERMET";
            string extencao = ".SFC";

            byte[] result = Funcoes.DecompressedGZip(arquivo);

            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = descricao;
                sfd.DefaultExt = extencao;
                sfd.Filter = "SFC File|.SFC";

                DialogResult dr = sfd.ShowDialog(this);

                if (dr == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(sfd.FileName))
                    {
                        return;
                    }
                    else if (sfd.FileName.Contains(extencao) == false)
                    {
                        sfd.FileName = sfd.FileName + extencao;
                    }

                    using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create))
                    {
                        fs.Write(result, 0, result.Length);
                    }

                    MessageBox.Show(this, classeHelp.BuscarMensagem(14), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(15), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region AERMOD

        /// <summary>
        /// Abrir backup.
        /// </summary>
        private void AbrirBackup()
        {
            //Backup MySql
            //https://www.codeproject.com/Tips/1260485/Backup-All-MySQL-Databases-in-Csharp-WinForm-with

            FrmBackup frmBackup = new FrmBackup();
            frmBackup.ShowDialogFade(this);
        }

        /// <summary>
        /// Abrir tela de ajuda.
        /// </summary>
        private void AbrirAjudaAERMOD()
        {
            string caminho = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "Help");
            Help.ShowHelp(this, $"{caminho}\\AERMOD.chm", HelpNavigator.TopicId, "50");
            //Help.ShowHelp(this, $"{caminho}\\AERMOD.chm", HelpNavigator.TableOfContents, "10");
            //Help.ShowHelp(this, $"{caminho}\\AERMOD.chm", HelpNavigator.Index, "10");
        }

        /// <summary>
        /// Abrir tela de visualização de mapas.
        /// </summary>
        private void AbrirMapaAERMOD()
        {
            FrmMapa frmMapa = new FrmMapa(1);
            frmMapa.ShowDialogFade(this);
        }

        /// <summary>
        /// Executar processador AERMOD.
        /// </summary>
        private void ExecutarAERMOD()
        {
            string diretorio = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "AERMOD_BACKEND\\AERMOD");
            string[] arquivosTemporarios = Directory.GetFiles(diretorio);
            bool confirmado = false;
            int codigoAERMOD = 0;

            FrmLoading frmLoading = new FrmLoading(this);

            Thread thr = new Thread(delegate ()
            {
                frmLoading.AtualizarStatus(1);

                #region Excluir últimos arquivos                

                foreach (var arquivoAtual in arquivosTemporarios)
                {
                    if (arquivoAtual.EndsWith(".exe") == false)
                    {
                        File.Delete(arquivoAtual);
                    }
                }

                #endregion

                frmLoading.AtualizarStatus(2);

                #region AERMAP Arquivos ROU/SOU               

                var AERMAP_ROU = classeAERMAP.RetornarArquivoROU();
                var AERMAP_SOU = classeAERMAP.RetornarArquivoSOU();

                if (AERMAP_ROU == null || AERMAP_SOU == null)
                {
                    this.Invoke(new Action(() => { MessageBox.Show(this, classeHelp.BuscarMensagem(46), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning); }));
                    return;
                }

                AERMAP_ROU = Funcoes.DecompressedGZip(AERMAP_ROU);
                AERMAP_SOU = Funcoes.DecompressedGZip(AERMAP_SOU);

                string caminhoROU = $"{diretorio}\\AERMAP.ROU";
                string caminhoSOU = $"{diretorio}\\AERMAP.SOU";

                File.WriteAllBytes(caminhoROU, AERMAP_ROU);
                File.WriteAllBytes(caminhoSOU, AERMAP_SOU);

                #endregion

                frmLoading.AtualizarStatus(3);

                #region AERMET Arquivos PFL/SFC

                int codigoAERMET = classeAERMET.RetornarCodigoUso();
                var AERMET_PFL = classeAERMET.RetornarArquivoPFL(codigoAERMET);
                var AERMET_SFC = classeAERMET.RetornarArquivoSFC(codigoAERMET);

                if (AERMET_PFL == null || AERMET_SFC == null)
                {
                    this.Invoke(new Action(() => { MessageBox.Show(this, classeHelp.BuscarMensagem(47), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning); }));
                    return;
                }

                AERMET_PFL = Funcoes.DecompressedGZip(AERMET_PFL);
                AERMET_SFC = Funcoes.DecompressedGZip(AERMET_SFC);

                string caminhoPFL = $"{diretorio}\\AERMET.PFL";
                string caminhoSFC = $"{diretorio}\\AERMET.SFC";

                File.WriteAllBytes(caminhoPFL, AERMET_PFL);
                File.WriteAllBytes(caminhoSFC, AERMET_SFC);

                #endregion

                frmLoading.AtualizarStatus(4);

                #region Definições AERMET

                var dtDefinicao = classeAERMET.RetornarRegistroUso();                
                string local = string.Empty;
                string uf = string.Empty;
                DateTime periodoInicial = DateTime.MinValue;
                DateTime periodoFinal = DateTime.MinValue;

                if (dtDefinicao.Item1.Rows.Count == 0)
                {
                    this.Invoke(new Action(() => { MessageBox.Show(this, $"AERMET - {classeHelp.BuscarMensagem(24)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning); }));
                    return;
                }
                else
                {                    
                    local = dtDefinicao.Item1.Rows[0]["LOCAL"].ToString();
                    uf = ((UF)Convert.ToInt32(dtDefinicao.Item1.Rows[0]["ESTADO"])).GetEnumDescription();
                    periodoInicial = Convert.ToDateTime(dtDefinicao.Item1.Rows[0]["PERIODO_INICIAL"]);
                    periodoFinal = Convert.ToDateTime(dtDefinicao.Item1.Rows[0]["PERIODO_FINAL"]);
                }

                #region colocar arquivo FSL na pasta AERMOD

                var arquivoFSL = classeFSL.RetornarArquivoUso();

                if (arquivoFSL == null)
                {
                    this.Invoke(new Action(() => { MessageBox.Show(this, $"AERMET - {classeHelp.BuscarMensagem(23)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning); }));
                    return;
                }

                var FSL = Funcoes.DecompressedGZip(arquivoFSL.Item1);
                string caminhoFSL = $"{diretorio}\\{arquivoFSL.Item2}";
                File.WriteAllBytes(caminhoFSL, FSL);

                string estacaoFSL = string.Empty;
                bool segundaLinha = false;
                foreach (string line in File.ReadLines(caminhoFSL))
                {
                    if (segundaLinha)
                    {
                        estacaoFSL = line.Substring(16, 5);

                        break;
                    }

                    segundaLinha = true;
                }

                #endregion

                #region colocar arquivo SAM na pasta AERMOD

                var arquivoSAM = classeSAM.RetornarArquivoUso();

                if (arquivoSAM == null)
                {
                    this.Invoke(new Action(() => { MessageBox.Show(this, $"AERMET - {classeHelp.BuscarMensagem(22)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning); }));
                    return;
                }

                var SAM = Funcoes.DecompressedGZip(arquivoSAM.Item1);
                string caminhoSAM = $"{diretorio}\\{arquivoSAM.Item2}";
                File.WriteAllBytes(caminhoSAM, SAM);

                string estacaoSAM = string.Empty;
                foreach (string line in File.ReadLines(caminhoSAM))
                {
                    estacaoSAM = line.Substring(1, 5);

                    break;
                }

                #endregion

                #endregion                

                frmLoading.AtualizarStatus(5);

                #region Montar arquivo (AERMOD.INP)            

                if (Directory.Exists(diretorio) == false)
                {
                    this.Invoke(new Action(() => { MessageBox.Show(this, classeHelp.BuscarMensagem(48), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning); }));
                    return;
                }

                var definicoesAERMOD = classeAERMOD.RetornarRegistroUso();

                if (definicoesAERMOD.Item1.Rows.Count == 0)
                {
                    this.Invoke(new Action(() => { MessageBox.Show(this, classeHelp.BuscarMensagem(24), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning); }));
                    return;
                }

                codigoAERMOD = definicoesAERMOD.Item1.Rows[0]["CODIGO"].ValidarValor<int>(0);
                int codigoMediaHoraria = definicoesAERMOD.Item1.Rows[0]["MEDIA_HORARIA"].ValidarValor<int>(0);
                int codigoMediaPeriodo = definicoesAERMOD.Item1.Rows[0]["MEDIA_PERIODO"].ValidarValor<int>(0);
                int codigoPoluente = definicoesAERMOD.Item1.Rows[0]["POLUENTE"].ValidarValor<int>(0);
                int valorMaximo = definicoesAERMOD.Item1.Rows[0]["VALOR_MAXIMO"].ValidarValor<int>(0);
                string valoresRetangulo = string.Empty;

                DataTable dtRetangulo = classeAERMOD.RetornaRetangulo(codigoAERMOD);
                foreach (DataRow item in dtRetangulo.Rows)
                {
                    if (string.IsNullOrEmpty(valoresRetangulo))
                    {
                        valoresRetangulo += item["VALOR"].ToString();
                    }
                    else
                    {
                        valoresRetangulo += $"  {item["VALOR"]}";
                    }
                }

                Poluentes poluente = (Poluentes)codigoPoluente;
                MediaHoraria mediaHoraria = (MediaHoraria)codigoMediaHoraria;
                MediaPeriodo mediaPeriodo = (MediaPeriodo)codigoMediaPeriodo;

                Arquivo.EscreverAERMOD("***************************************************************************", 0, true);
                Arquivo.EscreverAERMOD("** AERMOD Input File");
                Arquivo.EscreverAERMOD($"** Local: {local} - {uf}");
                Arquivo.EscreverAERMOD("***************************************************************************");
                Arquivo.EscreverAERMOD(" ");
                Arquivo.EscreverAERMOD("***************************************************************************");
                Arquivo.EscreverAERMOD("** AERMOD Control Pathway (Bloco de Controle)");
                Arquivo.EscreverAERMOD("***************************************************************************");
                Arquivo.EscreverAERMOD("CO STARTING");
                Arquivo.EscreverAERMOD("** Título do projeto **");
                Arquivo.EscreverAERMOD($"   TITLEONE Modelagem {poluente.ToString()}");
                Arquivo.EscreverAERMOD(" ");
                Arquivo.EscreverAERMOD("** Opções gerais do modelo **");
                Arquivo.EscreverAERMOD("   MODELOPT DFAULT CONC ");
                Arquivo.EscreverAERMOD("** Médias temporais **");
                Arquivo.EscreverAERMOD("** Média horária e de todo período **");
                Arquivo.EscreverAERMOD($"   AVERTIME {mediaHoraria.GetEnumDescription()} {mediaPeriodo.ToString()}");
                Arquivo.EscreverAERMOD("** Determinação do poluente **");
                Arquivo.EscreverAERMOD($"   POLLUTID {poluente.ToString()}");
                Arquivo.EscreverAERMOD("   RUNORNOT RUN");
                Arquivo.EscreverAERMOD("   ERRORFIL ERRORS.OUT");
                Arquivo.EscreverAERMOD("CO FINISHED");
                Arquivo.EscreverAERMOD(" ");
                Arquivo.EscreverAERMOD("*********************************************");
                Arquivo.EscreverAERMOD("** AERMOD Source Pathway (Bloco das Fontes)");
                Arquivo.EscreverAERMOD("*********************************************");
                Arquivo.EscreverAERMOD(" ");
                Arquivo.EscreverAERMOD("SO STARTING");

                #region Fontes fixas de emissão

                #region Altitude

                List<string> lstAltitude = new List<string>();

                try
                {
                    var lines = File.ReadLines($"{diretorio}\\AERMAP.SOU");

                    foreach (var line in lines)
                    {
                        if (line.StartsWith("SO LOCATION"))
                        {
                            string[] valor = line.Split(' ');
                            lstAltitude.Add(valor.Last());
                        }
                    }
                }
                catch
                {
                    MessageBox.Show(this, classeHelp.BuscarMensagem(49), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK);
                    return;
                }

                #endregion

                DataTable dtFontes = classeFonteAERMAP.RetornarRegistros();
                if (dtFontes.Rows.Count > 0)
                {
                    int count = 0;

                    foreach (DataRow linha in dtFontes.Rows)
                    {
                        count++;

                        TipoFonte tipoFonte = (TipoFonte)Convert.ToInt32(linha["TIPO"]);
                        string coordenadaX = linha["X"].ToString().Replace(",", ".");
                        string coordenadaY = linha["Y"].ToString().Replace(",", ".");

                        Arquivo.EscreverAERMOD($"   LOCATION  SOURCE{count.ToString().PadRight(2, ' ')} {tipoFonte.GetEnumDescription().PadRight(8, ' ')}  {coordenadaX}   {coordenadaY}   {lstAltitude[count - 1]}");
                    }
                }
                else
                {
                    this.Invoke(new Action(() => { MessageBox.Show(this, $"AERMAP - {classeHelp.BuscarMensagem(9)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning); }));
                    return;
                }

                #endregion

                Arquivo.EscreverAERMOD(" ");
                Arquivo.EscreverAERMOD("** Point Source    QS     HS     TS     VS     DS");
                Arquivo.EscreverAERMOD("** Area Source     Aremis (Relhgt) (Xinit) Yinit Angle Szinit");
                Arquivo.EscreverAERMOD("** AreaPoly Source Aremis (Relhgt) (Nverts) Szinit");
                Arquivo.EscreverAERMOD("** AreaCirc Source Aremis (Relhgt) (Radius) Nverts Szinit");
                Arquivo.EscreverAERMOD("** Line Source Lnemis (Relhgt) (Width) Szinit");
                Arquivo.EscreverAERMOD("** OpenPit Source Opemis (Relhgt) (Xinit) (Yinit) (Pitvol) Angle");
                Arquivo.EscreverAERMOD("** Volume Source Vlemis (Relhgt) (Syinit) (Szinit)");
                Arquivo.EscreverAERMOD("** Parameters:     ------ ------ ------ ------ ------");

                #region Parâmetros das fontes fixas de emissão

                if (dtFontes.Rows.Count > 0)
                {
                    int count = 0;

                    foreach (DataRow linha in dtFontes.Rows)
                    {
                        count++;

                        int codigoFonte = linha["CODIGO"].ValidarValor<int>(0);
                        TipoFonte tipoFonte = (TipoFonte)Convert.ToInt32(linha["TIPO"]);

                        switch (tipoFonte)
                        {
                            case TipoFonte.PONTO:
                                #region Ponto
                                {
                                    DataTable dtPonto = classeAERMOD.RetornaParametroPonto(codigoFonte, codigoAERMET, codigoAERMOD);
                                    if (dtPonto.Rows.Count > 0)
                                    {
                                        string QS = dtPonto.Rows[0]["TAXA_EMISSAO"].ToString().Replace(",", ".");
                                        string HS = dtPonto.Rows[0]["ALTURA_CHAMINE"].ToString().Replace(",", ".");
                                        string TS = dtPonto.Rows[0]["TEMPERATURA_SAIDA"].ToString().Replace(",", ".");
                                        string VS = dtPonto.Rows[0]["VELOCIDADE_SAIDA"].ToString().Replace(",", ".");
                                        string DS = dtPonto.Rows[0]["DIAMETRO_CHAMINE"].ToString().Replace(",", ".");

                                        QS = QS.TrimEnd('0').TrimEnd('.');
                                        HS = HS.TrimEnd('0').TrimEnd('.');
                                        TS = TS.TrimEnd('0').TrimEnd('.');
                                        VS = VS.TrimEnd('0').TrimEnd('.');
                                        DS = DS.TrimEnd('0').TrimEnd('.');

                                        Arquivo.EscreverAERMOD($"   SRCPARAM  SOURCE{count.ToString().PadRight(2, ' ')} {QS.ToString().PadLeft(11, ' ')} {HS.ToString().PadLeft(11, ' ')} {TS.PadLeft(11, ' ')} {VS.PadLeft(11, ' ')} {DS.PadLeft(11, ' ')}");
                                    }
                                    else
                                    {
                                        this.Invoke(new Action(() => { MessageBox.Show(this, classeHelp.BuscarMensagem(50), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning); }));
                                        return;
                                    }
                                }
                                #endregion
                                break;
                            case TipoFonte.AREA:
                                #region AREA
                                {
                                    DataTable dtArea = classeAERMOD.RetornaParametroArea(codigoFonte, codigoAERMET, codigoAERMOD);
                                    if (dtArea.Rows.Count > 0)
                                    {
                                        string Aremis = dtArea.Rows[0]["TAXA_EMISSAO"].ToString().Replace(",", ".");
                                        string Relhgt = dtArea.Rows[0]["ALTURA_LANCAMENTO"].ToString().Replace(",", ".");
                                        string Xinit = dtArea.Rows[0]["COMPRIMENTO_X"].ToString().Replace(",", ".");
                                        string Yinit = (dtArea.Rows[0]["COMPRIMENTO_Y"] ?? "").ToString().Replace(",", ".");
                                        string Angle = (dtArea.Rows[0]["ANGULO"] ?? "").ToString().Replace(",", ".");
                                        string Szinit = (dtArea.Rows[0]["DIMENSAO_VERTICAL"] ?? "").ToString().Replace(",", ".");

                                        Aremis = Aremis.TrimEnd('0').TrimEnd('.');
                                        Relhgt = Relhgt.TrimEnd('0').TrimEnd('.');
                                        Xinit = Xinit.TrimEnd('0').TrimEnd('.');
                                        Yinit = Yinit.TrimEnd('0').TrimEnd('.');
                                        Angle = Angle.TrimEnd('0').TrimEnd('.');
                                        Szinit = Szinit.TrimEnd('0').TrimEnd('.');

                                        Arquivo.EscreverAERMOD($"   SRCPARAM  SOURCE{count.ToString().PadRight(2, ' ')} {Aremis.PadLeft(11, ' ')} {Relhgt.ToString().PadLeft(11, ' ')} {Xinit.ToString().PadLeft(11, ' ')} {Yinit.ToString().PadLeft(11, ' ')} {Angle.ToString().PadLeft(11, ' ')} {Szinit.ToString().PadLeft(11, ' ')}");
                                    }
                                    else
                                    {
                                        this.Invoke(new Action(() => { MessageBox.Show(this, classeHelp.BuscarMensagem(51), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning); }));
                                        return;
                                    }
                                }
                                #endregion
                                break;
                            case TipoFonte.AREAPOLY:
                                #region AreaPoly
                                {
                                    DataTable dtAreaPoly = classeAERMOD.RetornaParametroAreaPoly(codigoFonte, codigoAERMET, codigoAERMOD);
                                    if (dtAreaPoly.Rows.Count > 0)
                                    {
                                        string Aremis = dtAreaPoly.Rows[0]["TAXA_EMISSAO"].ToString().Replace(",", ".");
                                        string Relhgt = dtAreaPoly.Rows[0]["ALTURA_LANCAMENTO"].ToString().Replace(",", ".");
                                        string Nverts = dtAreaPoly.Rows[0]["NUMERO_VERTICES"].ToString();
                                        string Szinit = (dtAreaPoly.Rows[0]["DIMENSAO_VERTICAL"] ?? "").ToString().Replace(",", ".");

                                        Aremis = Aremis.TrimEnd('0').TrimEnd('.');
                                        Relhgt = Relhgt.TrimEnd('0').TrimEnd('.');
                                        Nverts = Nverts.TrimEnd('0').TrimEnd('.');
                                        Szinit = Szinit.TrimEnd('0').TrimEnd('.');

                                        Arquivo.EscreverAERMOD($"   SRCPARAM  SOURCE{count.ToString().PadRight(2, ' ')} {Aremis.PadLeft(11, ' ')} {Relhgt.ToString().PadLeft(11, ' ')} {Nverts.ToString().PadLeft(2, ' ')} {Szinit.ToString().PadLeft(11, ' ')}");

                                        DataTable dtAreaPolyCoordenadas = classeAERMOD.RetornaParametroAreaPolyCoordenadas(codigoFonte, codigoAERMET, codigoAERMOD);
                                        if (dtAreaPolyCoordenadas.Rows.Count > 0)
                                        {
                                            string linhaCoordenadas = string.Empty;

                                            foreach (DataRow linhaCoordenada in dtAreaPolyCoordenadas.Rows)
                                            {
                                                string coordenadaX = linhaCoordenada["X"].ToString().Replace(",", ".");
                                                string coordenadaY = linhaCoordenada["Y"].ToString().Replace(",", ".");

                                                coordenadaX = coordenadaX.TrimEnd('0').TrimEnd('.');
                                                coordenadaY = coordenadaY.TrimEnd('0').TrimEnd('.');

                                                linhaCoordenadas += $" {coordenadaX}   {coordenadaY}";
                                            }

                                            Arquivo.EscreverAERMOD($"SO AREAVERT  SOURCE{count.ToString().PadRight(2, ' ')}{linhaCoordenadas}");
                                        }
                                        else
                                        {
                                            this.Invoke(new Action(() => { MessageBox.Show(this, classeHelp.BuscarMensagem(52), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning); }));
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        this.Invoke(new Action(() => { MessageBox.Show(this, classeHelp.BuscarMensagem(51), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning); }));
                                        return;
                                    }
                                }
                                #endregion
                                break;
                            case TipoFonte.AREACIRC:
                                #region AreaCirc
                                {
                                    DataTable dtAreaCirc = classeAERMOD.RetornaParametroAreaCirc(codigoFonte, codigoAERMET, codigoAERMOD);
                                    if (dtAreaCirc.Rows.Count > 0)
                                    {
                                        string Aremis = dtAreaCirc.Rows[0]["TAXA_EMISSAO"].ToString().Replace(",", ".");
                                        string Relhgt = dtAreaCirc.Rows[0]["ALTURA_LANCAMENTO"].ToString().Replace(",", ".");
                                        string Radius = dtAreaCirc.Rows[0]["RAIO"].ToString().Replace(",", ".");
                                        string Nverts = (dtAreaCirc.Rows[0]["NUMERO_VERTICES"] ?? "").ToString();
                                        string Szinit = (dtAreaCirc.Rows[0]["DIMENSAO_VERTICAL"] ?? "").ToString().Replace(",", ".");

                                        Aremis = Aremis.TrimEnd('0').TrimEnd('.');
                                        Relhgt = Relhgt.TrimEnd('0').TrimEnd('.');
                                        Radius = Radius.TrimEnd('0').TrimEnd('.');
                                        Nverts = Nverts.TrimEnd('0').TrimEnd('.');
                                        Szinit = Szinit.TrimEnd('0').TrimEnd('.');

                                        Arquivo.EscreverAERMOD($"   SRCPARAM  SOURCE{count.ToString().PadRight(2, ' ')} {Aremis.ToString().PadLeft(11, ' ')} {Relhgt.ToString().PadLeft(11, ' ')} {Radius.ToString().PadLeft(11, ' ')} {Nverts.PadLeft(2, ' ')} {Szinit.PadLeft(11, ' ')}");
                                    }
                                    else
                                    {
                                        this.Invoke(new Action(() => { MessageBox.Show(this, classeHelp.BuscarMensagem(53), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning); }));
                                        return;
                                    }
                                }
                                #endregion
                                break;
                            case TipoFonte.LINE:
                                #region Line

                                DataTable dtLine = classeAERMOD.RetornaParametroLinha(codigoFonte, codigoAERMET, codigoAERMOD);
                                if (dtLine.Rows.Count > 0)
                                {
                                    string Lnemis = dtLine.Rows[0]["TAXA_EMISSAO"].ToString().Replace(",", ".");
                                    string Relhgt = dtLine.Rows[0]["ALTURA_LANCAMENTO"].ToString().Replace(",", ".");
                                    string Width = dtLine.Rows[0]["LARGURA"].ToString().Replace(",", ".");                                    
                                    string Szinit = dtLine.Rows[0]["DIMENSAO_VERTICAL"].ToString().Replace(",", ".");

                                    Lnemis = Lnemis.TrimEnd('0').TrimEnd('.');
                                    Relhgt = Relhgt.TrimEnd('0').TrimEnd('.');
                                    Width = Width.TrimEnd('0').TrimEnd('.');                                   
                                    Szinit = Szinit.TrimEnd('0').TrimEnd('.');

                                    Arquivo.EscreverAERMOD($"   SRCPARAM  SOURCE{count.ToString().PadRight(2, ' ')} {Lnemis.ToString().PadLeft(11, ' ')} {Relhgt.ToString().PadLeft(11, ' ')} {Width.ToString().PadLeft(11, ' ')} {Szinit.PadLeft(11, ' ')}");
                                }
                                else
                                {
                                    this.Invoke(new Action(() => { MessageBox.Show(this, classeHelp.BuscarMensagem(78), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning); }));
                                    return;
                                }

                                #endregion
                                break;
                            case TipoFonte.OPENPIT:
                                #region OpenPit

                                DataTable dtOpenPit = classeAERMOD.RetornaParametroPoco(codigoFonte, codigoAERMET, codigoAERMOD);
                                if (dtOpenPit.Rows.Count > 0)
                                {
                                    string Opemis = dtOpenPit.Rows[0]["TAXA_EMISSAO"].ToString().Replace(",", ".");
                                    string Relhgt = dtOpenPit.Rows[0]["ALTURA_LANCAMENTO"].ToString().Replace(",", ".");
                                    string Xinit = dtOpenPit.Rows[0]["COMPRIMENTO_X"].ToString().Replace(",", ".");
                                    string Yinit = dtOpenPit.Rows[0]["COMPRIMENTO_Y"].ToString().Replace(",", ".");
                                    string Pitvol = dtOpenPit.Rows[0]["VOLUME"].ToString().Replace(",", ".");
                                    string Angle = dtOpenPit.Rows[0]["ANGULO"].ToString().Replace(",", ".");

                                    Opemis = Opemis.TrimEnd('0').TrimEnd('.');
                                    Relhgt = Relhgt.TrimEnd('0').TrimEnd('.');
                                    Xinit = Xinit.TrimEnd('0').TrimEnd('.');
                                    Yinit = Yinit.TrimEnd('0').TrimEnd('.');
                                    Pitvol = Pitvol.TrimEnd('0').TrimEnd('.');
                                    Angle = Angle.TrimEnd('0').TrimEnd('.');

                                    Arquivo.EscreverAERMOD($"   SRCPARAM  SOURCE{count.ToString().PadRight(2, ' ')} {Opemis.ToString().PadLeft(11, ' ')} {Relhgt.ToString().PadLeft(11, ' ')} {Xinit.ToString().PadLeft(11, ' ')} {Yinit.PadLeft(11, ' ')} {Pitvol.PadLeft(11, ' ')} {Angle.PadLeft(11, ' ')}");
                                }
                                else
                                {
                                    this.Invoke(new Action(() => { MessageBox.Show(this, classeHelp.BuscarMensagem(79), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning); }));
                                    return;
                                }

                                #endregion
                                break;
                            case TipoFonte.VOLUME:
                                #region Volume

                                DataTable dtVolume = classeAERMOD.RetornaParametroVolume(codigoFonte, codigoAERMET, codigoAERMOD);
                                if (dtVolume.Rows.Count > 0)
                                {
                                    string Vlemis = dtVolume.Rows[0]["TAXA_EMISSAO"].ToString().Replace(",", ".");
                                    string Relhgt = dtVolume.Rows[0]["ALTURA_LANCAMENTO"].ToString().Replace(",", ".");
                                    string Syinit = dtVolume.Rows[0]["DIMENSAO_LATERAL"].ToString().Replace(",", ".");
                                    string Szinit = dtVolume.Rows[0]["DIMENSAO_VERTICAL"].ToString().Replace(",", ".");

                                    Vlemis = Vlemis.TrimEnd('0').TrimEnd('.');
                                    Relhgt = Relhgt.TrimEnd('0').TrimEnd('.');
                                    Syinit = Syinit.TrimEnd('0').TrimEnd('.');
                                    Szinit = Szinit.TrimEnd('0').TrimEnd('.');                                    

                                    Arquivo.EscreverAERMOD($"   SRCPARAM  SOURCE{count.ToString().PadRight(2, ' ')} {Vlemis.ToString().PadLeft(11, ' ')} {Relhgt.ToString().PadLeft(11, ' ')} {Syinit.ToString().PadLeft(11, ' ')} {Szinit.PadLeft(11, ' ')}");
                                }
                                else
                                {
                                    this.Invoke(new Action(() => { MessageBox.Show(this, classeHelp.BuscarMensagem(79), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning); }));
                                    return;
                                }

                                #endregion
                                break;
                        }
                    }
                }

                #endregion

                Arquivo.EscreverAERMOD(" ");
                Arquivo.EscreverAERMOD("   SRCGROUP  ALL");
                Arquivo.EscreverAERMOD("SO FINISHED");
                Arquivo.EscreverAERMOD(" ");
                Arquivo.EscreverAERMOD("**************************************************");
                Arquivo.EscreverAERMOD("** AERMOD Receptor Pathway (Bloco dos Receptores)");
                Arquivo.EscreverAERMOD("**************************************************");
                Arquivo.EscreverAERMOD("RE STARTING");
                Arquivo.EscreverAERMOD("RE INCLUDED AERMAP.ROU");
                Arquivo.EscreverAERMOD("RE FINISHED");
                Arquivo.EscreverAERMOD(" ");
                Arquivo.EscreverAERMOD("*******************************************************");
                Arquivo.EscreverAERMOD("** AERMOD Meteorological Pathway (Bloco de Meteorologia)");
                Arquivo.EscreverAERMOD("*******************************************************");
                Arquivo.EscreverAERMOD(" ");
                Arquivo.EscreverAERMOD("ME STARTING");
                Arquivo.EscreverAERMOD("   SURFFILE  AERMET.SFC");
                Arquivo.EscreverAERMOD("   PROFFILE  AERMET.PFL");
                Arquivo.EscreverAERMOD($"   SURFDATA  {estacaoSAM}  {periodoInicial.Year}  {uf}");
                Arquivo.EscreverAERMOD($"   UAIRDATA  {estacaoFSL}  {periodoInicial.Year}  {uf}");
                Arquivo.EscreverAERMOD("   PROFBASE  0.0  METERS");
                Arquivo.EscreverAERMOD("ME FINISHED");
                Arquivo.EscreverAERMOD(" ");
                Arquivo.EscreverAERMOD("******************************************");
                Arquivo.EscreverAERMOD("** AERMOD Output Pathway (Bloco de Saída)");
                Arquivo.EscreverAERMOD("******************************************");
                Arquivo.EscreverAERMOD(" ");
                Arquivo.EscreverAERMOD("OU STARTING");
                Arquivo.EscreverAERMOD($"OU RECTABLE  ALLAVE  {valoresRetangulo}");
                Arquivo.EscreverAERMOD($"OU MAXTABLE  ALLAVE  {valorMaximo}");

                #region Saídas

                DataTable dtSaidas = definicoesAERMOD.Item2;
                foreach (DataRow item in dtSaidas.Rows)
                {
                    TipoSaida tipoSaida = (TipoSaida)(int)item["TIPO_SAIDA"];
                    MediaHoraria? mediaHorariaSaida = item["MEDIA_HORARIA"] != null && item["MEDIA_HORARIA"] != DBNull.Value ? ((MediaHoraria)(int)item["MEDIA_HORARIA"]) : (MediaHoraria?)null;
                    MediaPeriodo? mediaPeriodoSaida = item["MEDIA_PERIODO"] != null && item["MEDIA_PERIODO"] != DBNull.Value ? ((MediaPeriodo)(int)item["MEDIA_PERIODO"]) : (MediaPeriodo?)null;
                    decimal padraoQualidadeAr = item["PADRAO_QUALIDADE_AR"].ValidarValor<decimal>(0);
                    CriterioReceptor? criterioReceptor = item["CRITERIO_RECEPTOR"] != null && item["CRITERIO_RECEPTOR"] != DBNull.Value ? ((CriterioReceptor)(int)item["CRITERIO_RECEPTOR"]) : (CriterioReceptor?)null;
                    int valorMaximoSaida = item["VALOR_MAXIMO"].ValidarValor<int>(0);
                    string descricaoArquivo = item["DESCRICAO"].ToString();
                    string mediaTemporal = mediaHorariaSaida.HasValue ? mediaHorariaSaida.GetEnumDescription() : mediaPeriodoSaida.ToString();
                    switch (tipoSaida)
                    {
                        case TipoSaida.MAXIFILE:
                            string qualidadeAr = padraoQualidadeAr.ToString().Replace(',', '.');
                            qualidadeAr = qualidadeAr.TrimEnd('0').TrimEnd('.');

                            Arquivo.EscreverAERMOD($"OU MAXIFILE  {mediaTemporal.ToString().PadRight(6, ' ')}  ALL  {qualidadeAr.PadRight(6, ' ')} {descricaoArquivo}");
                            break;
                        case TipoSaida.POSTFILE:
                            Arquivo.EscreverAERMOD($"OU POSTFILE  {mediaTemporal.ToString().PadRight(6, ' ')}  ALL  PLOT{"  "} {descricaoArquivo}");
                            break;
                        case TipoSaida.PLOTFILE:
                            if (criterioReceptor.HasValue)
                            {
                                Arquivo.EscreverAERMOD($"OU PLOTFILE  {mediaTemporal.ToString().PadRight(6, ' ')}  ALL  {criterioReceptor.ToString().PadRight(6, ' ')} {descricaoArquivo}");
                            }
                            else
                            {
                                Arquivo.EscreverAERMOD($"OU PLOTFILE  {mediaTemporal.ToString().PadRight(6, ' ')}  ALL  {"      "} {descricaoArquivo}");
                            }
                            break;
                        case TipoSaida.RANKFILE:
                            Arquivo.EscreverAERMOD($"OU RANKFILE  {mediaTemporal.ToString().PadRight(6, ' ')}  {"   "}  {valorMaximoSaida.ToString().PadRight(6, ' ')} {descricaoArquivo}");
                            break;
                    }
                }

                #endregion

                Arquivo.EscreverAERMOD("OU FINISHED");

                #endregion

                confirmado = true;
            });
            thr.Start();

            frmLoading.thread = thr;
            frmLoading.PermiteAbortarThread = false;
            frmLoading.Texto = "Preparando arquivo(s)...";
            frmLoading.Style = ProgressBarStyle.Blocks;
            frmLoading.Maximum = 5;
            frmLoading.ShowDialogFade(this);

            if (confirmado == false)
            {
                return;
            }

            #region Executar o AERMOD

            FrmProcessoAERMOD frmProcesssoAERMOD = new FrmProcessoAERMOD(diretorio);
            frmProcesssoAERMOD.ShowDialogFade(this);

            #region Verificar resultado da execução (Erro/Sucesso)            

            string readText = string.Empty;
            try
            {
                readText = File.ReadAllText($"{diretorio}\\aermod.out");
            }
            catch
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(16), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK);
                return;
            }

            if (readText.Contains("AERMOD Finishes Successfully") == false)
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(55), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK);

                if (readText.Length > 0)
                {
                    ProcessStartInfo processStartInfo = new ProcessStartInfo();
                    processStartInfo.UseShellExecute = true;
                    processStartInfo.WorkingDirectory = diretorio;
                    processStartInfo.CreateNoWindow = false;
                    processStartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                    processStartInfo.RedirectStandardOutput = false;
                    processStartInfo.RedirectStandardError = false;

                    //running cmd.exe then .bat as argument
                    processStartInfo.FileName = Path.Combine(diretorio, "aermod.out");
                    processStartInfo.Arguments = "";

                    Process process = Process.Start(processStartInfo);
                }

                return;
            }

            #endregion

            #endregion

            frmLoading = new FrmLoading(this);

            thr = new Thread(delegate ()
            {
                #region Guardar os arquivos no banco

                frmLoading.AtualizarStatus(1);

                #region INP

                FileStream arquivo_INP = File.Open(Path.Combine(diretorio, "AERMOD.INP"), FileMode.Open, FileAccess.Read, FileShare.None);

                var buffer = new byte[arquivo_INP.Length];
                using (arquivo_INP)
                {
                    arquivo_INP.Read(buffer, 0, Convert.ToInt32(arquivo_INP.Length));
                    arquivo_INP.Close();
                }

                byte[] arquivoCompactado_INP = Funcoes.CompressedGZip(buffer);

                #endregion

                frmLoading.AtualizarStatus(2);

                #region AERMOD.OUT

                FileStream arquivoAERMOD_OUT = File.Open(Path.Combine(diretorio, "aermod.out"), FileMode.Open, FileAccess.Read, FileShare.None);

                buffer = new byte[arquivoAERMOD_OUT.Length];
                using (arquivoAERMOD_OUT)
                {
                    arquivoAERMOD_OUT.Read(buffer, 0, Convert.ToInt32(arquivoAERMOD_OUT.Length));
                    arquivoAERMOD_OUT.Close();
                }

                byte[] arquivoCompactadoAERMOD_OUT = Funcoes.CompressedGZip(buffer);

                #endregion

                frmLoading.AtualizarStatus(3);

                #region ERRORS.OUT

                FileStream arquivoERRORS_OUT = File.Open(Path.Combine(diretorio, "ERRORS.OUT"), FileMode.Open, FileAccess.Read, FileShare.None);

                buffer = new byte[arquivoERRORS_OUT.Length];
                using (arquivoERRORS_OUT)
                {
                    arquivoERRORS_OUT.Read(buffer, 0, Convert.ToInt32(arquivoERRORS_OUT.Length));
                    arquivoERRORS_OUT.Close();
                }

                byte[] arquivoCompactadoERRORS_OUT = Funcoes.CompressedGZip(buffer);

                #endregion

                frmLoading.AtualizarStatus(4);

                #region .PLT

                List<Tuple<int, byte[]>> lstSaidas = new List<Tuple<int, byte[]>>();

                var definicoesAERMOD = classeAERMOD.RetornarRegistroUso();                
                foreach (DataRow item in definicoesAERMOD.Item2.Rows)
                {
                    int sequencia = item["SEQUENCIA"].ValidarValor<int>(0);
                    string descricaoArquivo = item["DESCRICAO"].ToString();

                    FileStream arquivo_PLT = File.Open(Path.Combine(diretorio, descricaoArquivo), FileMode.Open, FileAccess.Read, FileShare.None);

                    buffer = new byte[arquivo_PLT.Length];
                    using (arquivo_PLT)
                    {
                        arquivo_PLT.Read(buffer, 0, Convert.ToInt32(arquivo_PLT.Length));
                        arquivo_PLT.Close();
                    }

                    byte[] arquivoCompactado_PLT = Funcoes.CompressedGZip(buffer);
                    lstSaidas.Add(Tuple.Create(sequencia, arquivoCompactado_PLT));
                }

                #endregion                

                classeAERMOD.SalvarArquivo(codigoAERMOD, arquivoCompactado_INP, arquivoCompactadoERRORS_OUT, arquivoCompactadoAERMOD_OUT, lstSaidas);

                #endregion

                frmLoading.AtualizarStatus(5);

                #region Excluir últimos arquivos

                arquivosTemporarios = Directory.GetFiles(diretorio);

                foreach (var arquivoAtual in arquivosTemporarios)
                {
                    if (arquivoAtual.EndsWith(".exe") == false)
                    {
                        File.Delete(arquivoAtual);
                    }
                }

                #endregion
            });
            thr.Start();

            frmLoading.thread = thr;
            frmLoading.PermiteAbortarThread = false;
            frmLoading.Texto = "Salvando arquivo(s)...";
            frmLoading.Style = ProgressBarStyle.Blocks;
            frmLoading.Maximum = 5;
            frmLoading.ShowDialogFade(this);
        }

        /// <summary>
        /// Abrir definições AERMOD.
        /// </summary>
        private void AbrirDefinioes_AEMOD()
        {
            FrmDefinicaoAERMOD frmDefinicao = new FrmDefinicaoAERMOD();
            frmDefinicao.ShowDialogFade(this);
        }

        /// <summary>
        /// Abrir parâmetros das fontes emissoras (ponto/area).
        /// </summary>
        private void AbrirParametros_AERMOD()
        {
            int codigoPeriodo = classeAERMET.RetornaCodigoUnico();

            if (codigoPeriodo == 0)
            {
                FrmDefinicaoAERMET frmAERMET = new FrmDefinicaoAERMET(true);
                frmAERMET.ShowDialogFade(this);

                if (frmAERMET.Codigo > 0)
                {
                    codigoPeriodo = frmAERMET.Codigo;
                }
                else
                {
                    return;
                }
            }

            int codigoPoluente = classeAERMOD.RetornaCodigoUnico();

            if (codigoPoluente == 0)
            {
                FrmDefinicaoAERMOD frmAERMOD = new FrmDefinicaoAERMOD(true);
                frmAERMOD.ShowDialogFade(this);

                if (frmAERMOD.Codigo > 0)
                {
                    codigoPoluente = frmAERMOD.Codigo;
                }
                else
                {
                    return;
                }
            }

            FrmParametrosAERMOD frmParametros = new FrmParametrosAERMOD(codigoPeriodo, codigoPoluente);
            frmParametros.ShowDialogFade(this);
        }

        /// <summary>
        /// Exportar AERMOD.INP
        /// </summary>
        private void ExportarINP_AERMOD()
        {
            int codigo = classeAERMOD.RetornarCodigoUso();
            byte[] arquivo = classeAERMOD.RetornarAERMOD_INP(codigo);

            if (arquivo == null)
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(13), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string descricao = "AERMOD";
            string extencao = ".INP";

            byte[] result = Funcoes.DecompressedGZip(arquivo);

            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = descricao;
                sfd.DefaultExt = extencao;
                sfd.Filter = "INP File|.INP";

                DialogResult dr = sfd.ShowDialog(this);

                if (dr == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(sfd.FileName))
                    {
                        return;
                    }
                    else if (sfd.FileName.Contains(extencao) == false)
                    {
                        sfd.FileName = sfd.FileName + extencao;
                    }

                    using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create))
                    {
                        fs.Write(result, 0, result.Length);
                    }

                    MessageBox.Show(this, classeHelp.BuscarMensagem(14), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(15), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Exportar AERMOD.OUT
        /// </summary>
        private void ExportarOUT1_AERMOD()
        {
            int codigo = classeAERMOD.RetornarCodigoUso();
            byte[] arquivo = classeAERMOD.RetornarAERMOD_OUT(codigo);

            if (arquivo == null)
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(13), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string descricao = "AERMOD";
            string extencao = ".OUT";

            byte[] result = Funcoes.DecompressedGZip(arquivo);

            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = descricao;
                sfd.DefaultExt = extencao;
                sfd.Filter = "OUT File|.OUT";

                DialogResult dr = sfd.ShowDialog(this);

                if (dr == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(sfd.FileName))
                    {
                        return;
                    }
                    else if (sfd.FileName.Contains(extencao) == false)
                    {
                        sfd.FileName = sfd.FileName + extencao;
                    }

                    using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create))
                    {
                        fs.Write(result, 0, result.Length);
                    }

                    MessageBox.Show(this, classeHelp.BuscarMensagem(14), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(15), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Exportar ERRORS.OUT
        /// </summary>
        private void ExportarOUT2_AERMOD()
        {
            int codigo = classeAERMOD.RetornarCodigoUso();
            byte[] arquivo = classeAERMOD.RetornarERRORS_OUT(codigo);

            if (arquivo == null)
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(13), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string descricao = "ERRORS";
            string extencao = ".OUT";

            byte[] result = Funcoes.DecompressedGZip(arquivo);

            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = descricao;
                sfd.DefaultExt = extencao;
                sfd.Filter = "OUT File|.OUT";

                DialogResult dr = sfd.ShowDialog(this);

                if (dr == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(sfd.FileName))
                    {
                        return;
                    }
                    else if (sfd.FileName.Contains(extencao) == false)
                    {
                        sfd.FileName = sfd.FileName + extencao;
                    }

                    using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create))
                    {
                        fs.Write(result, 0, result.Length);
                    }

                    MessageBox.Show(this, classeHelp.BuscarMensagem(14), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(15), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Exportar arquivos (PLT).
        /// </summary>
        private void ExportarPLT_AERMOD()
        {
            FrmArquivo frmArquivo = new FrmArquivo(TipoArquivo.PLT);
            frmArquivo.ShowDialogFade(this);
        }

        #endregion

        #endregion
    }
}
