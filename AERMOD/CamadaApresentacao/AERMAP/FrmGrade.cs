using AERMOD.LIB.Componentes.Splash;
using AERMOD.LIB.Desenvolvimento;
using AERMOD.LIB.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AERMOD.CamadaApresentacao.AERMAP
{
    public partial class FrmGrade : Form
    {
        #region Classes e Propriedades

        /// <summary>
        /// Código do domínio.
        /// </summary>
        int codigoDominio;

        /// <summary>
        /// Distância entre a fonte e a grade de modelagem.
        /// </summary>
        public decimal DistFonteGrade { get; set; }

        #endregion

        #region Construtor

        public FrmGrade(int codigoDominio)
        {
            InitializeComponent();

            this.Icon = Properties.Resources.painelControle.ConvertImageToIcon();
            this.codigoDominio = codigoDominio;            
        }

        #endregion

        #region Eventos FrmGrade

        private void FrmGrade_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    e.SuppressKeyPress = true;
                    AbrirAjuda();
                    break;
                case Keys.F2:
                    e.SuppressKeyPress = true;
                    AbrirCartesiano();
                    break;
                case Keys.F3:
                    e.SuppressKeyPress = true;
                    AbrirCartesianoElevacao();
                    break;
                case Keys.F4:
                    e.SuppressKeyPress = true;
                    AbrirCartesianoDiscreto();
                    break;
                case Keys.F5:
                    e.SuppressKeyPress = true;
                    AbrirEVALFILE();
                    break;
                case Keys.Escape:
                    this.Close();
                    break;
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Abrir tela de ajuda.
        /// </summary>
        private void AbrirAjuda()
        {
            string caminho = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "Help");
            Help.ShowHelp(this, $"{caminho}\\AERMOD.chm", HelpNavigator.TopicId, "50");
            //Help.ShowHelp(this, $"{caminho}\\AERMOD.chm", HelpNavigator.TableOfContents, "10");
            //Help.ShowHelp(this, $"{caminho}\\AERMOD.chm", HelpNavigator.Index, "10");
        }

        /// <summary>
        /// Abrir cadastro de grade cartesiana normal.
        /// </summary>
        private void AbrirCartesiano()
        {
            SplashScreen.FindHandleParent();           
            SplashScreen.StyleProgress = StyleProgress.Marquee;
            SplashScreen.Location = SplashScreen.CalcLocation(this.Location, this.Size);      

            Thread splashthread = new Thread(new ThreadStart(SplashScreen.ShowSplashScreen));
            splashthread.IsBackground = true;
            splashthread.Start();            

            FrmCartesiano frmCartesiano = new FrmCartesiano(codigoDominio);
            frmCartesiano.DistFonteGrade = DistFonteGrade;            
            frmCartesiano.ShowDialogFade(this);
        }

        /// <summary>
        /// Abrir cadastro de grade cartesiana com altitude/elevação.
        /// </summary>
        private void AbrirCartesianoElevacao()
        {
            SplashScreen.FindHandleParent();
            SplashScreen.StyleProgress = StyleProgress.Marquee;
            SplashScreen.Location = SplashScreen.CalcLocation(this.Location, this.Size);

            Thread splashthread = new Thread(new ThreadStart(SplashScreen.ShowSplashScreen));
            splashthread.IsBackground = true;
            splashthread.Start();

            FrmCartesianoRede frmCartesiano = new FrmCartesianoRede(codigoDominio);
            frmCartesiano.DistFonteGrade = DistFonteGrade;            
            frmCartesiano.ShowDialogFade(this);
        }

        /// <summary>
        /// Abrir cadastro de grade cartesiana discreta.
        /// </summary>
        private void AbrirCartesianoDiscreto()
        {
            SplashScreen.FindHandleParent();
            SplashScreen.StyleProgress = StyleProgress.Marquee;
            SplashScreen.Location = SplashScreen.CalcLocation(this.Location, this.Size);

            Thread splashthread = new Thread(new ThreadStart(SplashScreen.ShowSplashScreen));
            splashthread.IsBackground = true;
            splashthread.Start();

            FrmCartesianoDiscreto frmCartesianoDisc = new FrmCartesianoDiscreto(codigoDominio);
            frmCartesianoDisc.DistFonteGrade = DistFonteGrade;            
            frmCartesianoDisc.ShowDialogFade(this);
        }

        /// <summary>
        /// Abrir cadastro de grade cartesiana discreta EVALFILE.
        /// </summary>
        private void AbrirEVALFILE()
        {
            SplashScreen.FindHandleParent();
            SplashScreen.StyleProgress = StyleProgress.Marquee;
            SplashScreen.Location = SplashScreen.CalcLocation(this.Location, this.Size);

            Thread splashthread = new Thread(new ThreadStart(SplashScreen.ShowSplashScreen));
            splashthread.IsBackground = true;
            splashthread.Start();

            FrmCartesianoEVALFILE frmEVALFILE = new FrmCartesianoEVALFILE(codigoDominio);
            frmEVALFILE.DistFonteGrade = DistFonteGrade;            
            frmEVALFILE.ShowDialogFade(this);
        }

        #endregion

        #region Eventos btnCartesiano

        private void btnCartesiano_Click(object sender, EventArgs e)
        {
            AbrirCartesiano();
        }

        #endregion

        #region Eventos btnCartesianoElevacao

        private void btnCartesianoElevacao_Click(object sender, EventArgs e)
        {
            AbrirCartesianoElevacao();
        }

        #endregion

        #region Eventos btnCartesianoDiscreto

        private void btnCartesianoDiscreto_Click(object sender, EventArgs e)
        {
            AbrirCartesianoDiscreto();
        }

        #endregion

        #region Eventos btnEVALFILE

        private void btnEVALFILE_Click(object sender, EventArgs e)
        {
            AbrirEVALFILE();
        }

        #endregion

        #region Eventos statusStripCadastro

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAjuda_Click(object sender, EventArgs e)
        {
            AbrirAjuda();
        }

        #endregion
    }
}
