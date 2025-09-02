using System;
using System.Drawing;
using System.Windows.Forms;

namespace AERMOD.LIB.Componentes.Splash
{
    internal partial class SplashScreenForm : Form
    {
        #region Variáveis/Propriedades

        delegate void SplashShowCloseDelegate();

        /// <summary>
        /// To ensure splash screen is closed using the API and not by keyboard or any other things
        /// </summary>
        bool CloseSplashScreenFlag = false;

        Timer timerProgress;

        Timer timerCarregando;

        public int TimerSeconds { get; set; }

        public StyleProgress StyleProgress { get; set; }

        public string Message { get; set; }

        #endregion

        #region Construtor

        /// <summary>
        /// Base constructor
        /// </summary>
        public SplashScreenForm()
        {
            InitializeComponent();

            progressBar.Show();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Displays the splashscreen
        /// </summary>
        public void ShowSplashScreen()
        {
            if (string.IsNullOrEmpty(this.Message) == false)
            {
                if (lbCarregando.InvokeRequired)
                {
                    lbCarregando.BeginInvoke(new Action(() => lbCarregando.Text = (this.Message + "   ")));
                }
                else
                {
                    lbCarregando.Text = (this.Message + "   ");
                }
            }

            timerCarregando = new Timer();
            timerCarregando.Interval = 300;
            timerCarregando.Tick += TimerCarregando_Tick;
            timerCarregando.Enabled = true;

            if (StyleProgress == StyleProgress.Blocks)
            {
                timerProgress = new Timer();
                timerProgress.Interval = 50;
                timerProgress.Tick += TimerProgress_Tick;
                timerProgress.Enabled = true;

                progressBar.Style = ProgressBarStyle.Blocks;
                progressBar.Maximum = TimerSeconds + 1;
            }
            else
            {
                progressBar.Style = ProgressBarStyle.Marquee;
            }

            if (InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new SplashShowCloseDelegate(ShowSplashScreen));
                return;
            }
            //this.ShowDialog();
            Application.Run(this);
        }

        /// <summary>
        /// Closes the SplashScreen
        /// </summary>
        public void CloseSplashScreen()
        {
            if (InvokeRequired)
            {
                // We're not in the UI thread, so we need to call BeginInvoke
                BeginInvoke(new SplashShowCloseDelegate(CloseSplashScreen));
                return;
            }

            CloseSplashScreenFlag = true;
            this.Dispose();
        }

        /// <summary>
        /// Prevents the closing of form other than by calling the CloseSplashScreen function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SplashForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CloseSplashScreenFlag == false)
            {
                e.Cancel = true;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Pen pen = new Pen(new SolidBrush(SystemColors.ControlLight));
            pen.Width = 1;
            e.Graphics.DrawRectangle(pen, 0, 0, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);

            base.OnPaint(e);
        }

        private void TimerProgress_Tick(object sender, EventArgs e)
        {
            timerProgress.Enabled = false;
            timerProgress.Interval = (TimerSeconds * 1000) / TimerSeconds;

            if (StyleProgress == StyleProgress.Blocks)
            {
                if (progressBar.Value < progressBar.Maximum)
                {
                    progressBar.Value++;
                }
            }

            if (CloseSplashScreenFlag == false)
            {
                timerProgress.Enabled = true;
            }
        }

        private void TimerCarregando_Tick(object sender, EventArgs e)
        {
            timerCarregando.Enabled = false;

            if (lbPontos.Text.Trim().Length == 0 || lbPontos.Text.Trim().Length == 3)
            {
                lbPontos.Text = ".  ";
            }
            else if (lbPontos.Text.Trim().Length == 1)
            {
                lbPontos.Text = ".. ";
            }
            else if (lbPontos.Text.Trim().Length == 2)
            {
                lbPontos.Text = "...";
            }

            if (CloseSplashScreenFlag == false)
            {
                timerCarregando.Enabled = true;
            }
        }

        #endregion
    }
}
