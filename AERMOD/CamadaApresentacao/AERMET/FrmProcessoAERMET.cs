using AERMOD.LIB.Desenvolvimento;
using CamadaLogicaNegocios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AERMOD.CamadaApresentacao.AERMET
{
    public partial class FrmProcessoAERMET : Form
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
        /// Caminho do arquivo de execução.
        /// </summary>
        string diretorio;

        /// <summary>
        /// Processo atual.
        /// </summary>
        Process p = null;

        #endregion

        #region Construtor

        public FrmProcessoAERMET(string diretorio)
        {
            InitializeComponent();

            this.Icon = Properties.Resources.icon_connect.ConvertImageToIcon();
            this.diretorio = diretorio;
        }

        #endregion

        #region Eventos FrmProcessoAERMET

        protected override void OnShown(EventArgs e)
        {
            try
            {
                p = new Process()
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        WorkingDirectory = diretorio,
                        FileName = Path.Combine(diretorio, "aermet.exe"),
                        Arguments = "",
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true
                    }
                };
                p.Start();

                string cv_error = null;
                Thread et = new Thread(() =>
                {
                    while (p.HasExited == false && (cv_error = p.StandardError.ReadLine()) != null)
                    {
                        CrossThreadOperation.Invoke(this, delegate
                        {
                            listBoxMensagem.Items.Add(cv_error);
                            listBoxMensagem.TopIndex = listBoxMensagem.Items.Count - 1;
                        });
                        //this.Invoke(new Action(() => { LOGMensagem += cv_error + System.Environment.NewLine; }));
                    }
                });
                et.Priority = ThreadPriority.Highest;
                et.Start();

                string cv_out = null;
                Thread ot = new Thread(() =>
                {
                    while (p.HasExited == false && (cv_out = p.StandardOutput.ReadLine()) != null)
                    {
                        CrossThreadOperation.Invoke(this, delegate
                        {
                            listBoxMensagem.Items.Add(cv_out);
                            listBoxMensagem.TopIndex = listBoxMensagem.Items.Count - 1;
                        });

                        //this.Invoke(new Action(() => { LOGMensagem += cv_out + System.Environment.NewLine; }));
                    }
                });
                ot.Priority = ThreadPriority.Highest;
                ot.Start();

                //while (p.WaitForExit(1) == false) { Application.DoEvents(); }
                //while (p.WaitForExit(100) == false) { Thread.Sleep(50); Application.DoEvents(); }

                while (p.HasExited == false)
                {
                    Application.DoEvents();
                }

                ot.Join();
                et.Join();
            }
            catch
            {
                try
                {
                    if (p.HasExited == false)
                    {
                        p.Kill();
                    }
                }
                catch { }
            }

            this.Close();

            base.OnShown(e);
        }

        private void FrmProcessoAERMET_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (p.HasExited == false)
                {
                    p.Kill();
                }
            }
            catch { }
        }

        private void FrmProcessoAERMET_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    e.SuppressKeyPress = true;

                    try
                    {
                        if (p.HasExited == false)
                        {
                            p.Kill();
                        }
                    }
                    catch { }
                    break;
            }
        }

        #endregion
    }
}
