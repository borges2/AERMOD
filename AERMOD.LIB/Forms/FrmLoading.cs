using AERMOD.LIB.Desenvolvimento;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AERMOD.LIB.Forms
{
    public partial class FrmLoading : Form
    {
        #region Declarações

        /// <summary>
        /// Titulo do Form.
        /// </summary>
        public string Texto
        {
            get
            {
                return lbTexto.Text;
            }
            set
            {
                CrossThreadOperation.Invoke(this, delegate { lbTexto.Text = value; });
            }
        }

        /// <summary>
        /// Quantidade máxima do ProgressBar
        /// </summary>
        public Int32 Maximum
        {
            get
            {
                return progressBar.Maximum;
            }
            set
            {
                CrossThreadOperation.Invoke(this, delegate { progressBar.Maximum = value; });
            }
        }

        /// <summary>
        /// Estilo do ProgressBar
        /// </summary>
        public ProgressBarStyle Style
        {
            get
            {
                return progressBar.Style;
            }
            set
            {
                CrossThreadOperation.Invoke(this, delegate { progressBar.Style = value; });
            }
        }

        /// <summary>
        /// Visualizar período de execução.
        /// </summary>
        [DefaultValue(true)]
        public bool Periodo
        {
            get
            {
                return lbPeriodo.Visible;
            }
            set
            {
                lbPeriodo.Visible = value;
            }
        }

        /// <summary>
        /// Permite abortar thread através
        /// da tecla ESC.
        /// </summary>
        public bool PermiteAbortarThread = true;

        /// <summary>
        /// Thread da operação atual que está sendo executada.
        /// </summary>
        public Thread thread { get; set; }

        /// <summary>
        /// Tempo de espera quando thread abortada ou finalizada.
        /// </summary>
        System.Timers.Timer timerExecucao = null;

        #endregion

        #region Construtor

        public FrmLoading()
        {
            InitializeComponent();

            timerExecucao = new System.Timers.Timer();
            timerExecucao.Interval = 50;
            timerExecucao.Elapsed += new System.Timers.ElapsedEventHandler(timerExecucao_Tick);
        }

        public FrmLoading(Form form)
        {
            InitializeComponent();

            timerExecucao = new System.Timers.Timer();
            timerExecucao.Interval = 50;
            timerExecucao.Elapsed += new System.Timers.ElapsedEventHandler(timerExecucao_Tick);

            Int32 x = 0;
            Int32 y = 0;

            this.StartPosition = FormStartPosition.Manual;
            if (form.Size.Width >= this.Size.Width)
            {
                x = form.DesktopLocation.X + ((form.Size.Width - this.Size.Width) / 2);
            }
            else
            {
                x = form.DesktopLocation.X - ((this.Size.Width - form.Size.Width) / 2);
            }

            if (form.Height >= this.Size.Height)
            {
                y = form.DesktopLocation.Y + ((form.Size.Height - this.Size.Height) / 2);
            }
            else
            {
                y = form.DesktopLocation.Y - ((this.Size.Height - form.Size.Height) / 2);
            }
            this.Location = new Point(x, y);
        }

        #endregion

        #region Eventos timerExecucao

        private void timerExecucao_Tick(object sender, EventArgs e)
        {
            if (thread != null && (thread.ThreadState == ThreadState.Stopped || thread.ThreadState == ThreadState.Aborted))
            {
                CrossThreadOperation.Invoke(this, delegate { this.Close(); });
            }
        }

        #endregion

        #region Eventos FrmLoading

        protected override void OnShown(EventArgs e)
        {
            if (thread != null)
            {
                timerExecucao.Enabled = true;
            }

            if (progressBar.Style == ProgressBarStyle.Marquee)
            {
                CrossThreadOperation.Invoke(this, delegate
                {
                    progressBar.MarqueeAnimationSpeed = 40;
                    progressBar.Minimum = 0;
                    progressBar.Maximum = 100;
                    progressBar.Step = 10;
                    progressBar.Value = 10;
                });
                
                Periodo = false;
            }

            base.OnShown(e);
        }

        private void FrmLoading_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && PermiteAbortarThread)
            {
                if (thread != null)
                {
                    thread.Abort();

                    while (thread.ThreadState == ThreadState.AbortRequested)
                    {
                        Texto = "Abortando execução...";
                        Application.DoEvents();
                    }
                }

                if (thread == null || (thread.ThreadState == ThreadState.Aborted || thread.ThreadState == ThreadState.Stopped))
                {
                    this.Close();
                }
            }
        }

        private void FrmLoading_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerExecucao.Enabled = false;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Atualiza o status do progressBar.
        /// </summary>
        /// <param name="valor">Valor atual.</param>
        /// <param name="texto">Texto do Form.</param>
        public void AtualizarStatus(int valor, string texto = null)
        {
            CrossThreadOperation.Invoke(this, delegate
            {
                progressBar.Visible = true;

                if (string.IsNullOrEmpty(texto) == false)
                {
                    Texto = texto;
                }

                if (valor > 0)
                {
                    progressBar.Value = valor;
                    lbPeriodo.Text = string.Format("{0} de {1}", valor, progressBar.Maximum);
                }
            });
        }

        /// <summary>
        /// Atualiza o status do progressBar.
        /// </summary>
        /// <param name="valor">Valor atual.</param>
        /// <param name="texto">Texto do Form.</param>
        public void AtualizarProgresso(int valor)
        {
            CrossThreadOperation.Invoke(this, delegate
            {
                if (valor > 0)
                {
                    progressBar.Visible = false;
                    lbPeriodo.Text = string.Format("Total processado {0}", valor);
                }
            });
        }

        #endregion
    }
}
