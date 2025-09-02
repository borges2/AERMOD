using AERMOD.LIB.Desenvolvimento;
using CamadaLogicaNegocios;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace AERMOD.CamadaApresentacao.AERMAP
{
    public partial class FrmProcessoAERMAP : Form
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

        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOWMINIMIZED = 2;
        private const int SW_SHOWMAXIMIZED = 3;
        private const int SW_SHOWNOACTIVATE = 4;
        private const int SW_RESTORE = 9;
        private const int SW_SHOWDEFAULT = 10;

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        static extern bool LockWindowUpdate(IntPtr hWndLock);

        string diretorio;

        /// <summary>
        /// Processo atual.
        /// </summary>
        Process p = null;

        String _LOGMensagem = "";
        String LOGMensagem
        {
            get
            {
                return _LOGMensagem;
            }
            set
            {
                //if (tbxMensagem.IsDisposed == false)
                //{
                //    LockWindowUpdate(tbxMensagem.Handle);
                //    tbxMensagem.Text = value;
                //    tbxMensagem.SelectionStart = LOGMensagem.Length;
                //    tbxMensagem.ScrollToCaret();
                //}

                LockWindowUpdate(IntPtr.Zero);

                Application.DoEvents();
                _LOGMensagem = value;
            }
        }

        #endregion

        #region Construtor

        public FrmProcessoAERMAP(string diretorio)
        {
            InitializeComponent();

            this.Icon = Properties.Resources.icon_connect.ConvertImageToIcon();
            this.diretorio = diretorio;
        }        

        #endregion

        #region Eventos FrmProcessoAERMAP

        protected override void OnShown(EventArgs e)
        {            
            #region Teste 1

            try
            {
                p = new Process()
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        WorkingDirectory = diretorio,
                        FileName = Path.Combine(diretorio, "aermap.exe"),
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

            #endregion

            #region Teste 2

            //using (Process process = new Process())
            //{
            //    process.StartInfo.WorkingDirectory = diretorio;
            //    process.StartInfo.FileName = Path.Combine(diretorio, "aermap.exe");
            //    process.StartInfo.Arguments = "";
            //    process.StartInfo.UseShellExecute = false;
            //    process.StartInfo.CreateNoWindow = true;
            //    process.StartInfo.RedirectStandardOutput = true;
            //    process.StartInfo.RedirectStandardError = true;

            //    StringBuilder output = new StringBuilder();
            //    StringBuilder error = new StringBuilder();

            //    using (AutoResetEvent outputWaitHandle = new AutoResetEvent(false))
            //    using (AutoResetEvent errorWaitHandle = new AutoResetEvent(false))
            //    {
            //        process.OutputDataReceived += (sender, ev) => {
            //            if (ev.Data == null)
            //            {
            //                outputWaitHandle.Set();
            //            }
            //            else
            //            {
            //                this.Invoke(new Action(() => { LOGMensagem += ev.Data + System.Environment.NewLine; }));                            
            //                //output.AppendLine(e.Data);
            //            }
            //        };
            //        process.ErrorDataReceived += (sender, ev) =>
            //        {
            //            if (ev.Data == null)
            //            {
            //                errorWaitHandle.Set();
            //            }
            //            else
            //            {
            //                this.Invoke(new Action(() => { LOGMensagem += ev.Data + System.Environment.NewLine; }));                            
            //                //error.AppendLine(e.Data);
            //            }
            //        };

            //        process.Start();

            //        process.BeginOutputReadLine();
            //        process.BeginErrorReadLine();

            //        while (process.HasExited == false)
            //        {
            //            Application.DoEvents();
            //        }

            //        //if (process.WaitForExit(timeout) &&
            //        //    outputWaitHandle.WaitOne(timeout) &&
            //        //    errorWaitHandle.WaitOne(timeout))
            //        //{
            //        //    // Process completed. Check process.ExitCode here.
            //        //}
            //        //else
            //        //{
            //        //    // Timed out.
            //        //}
            //    }
            //}

            #endregion

            this.Close();

            base.OnShown(e);
        }

        private void FrmProcessoAERMAP_FormClosing(object sender, FormClosingEventArgs e)
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

        private void FrmProcessoAERMAP_KeyDown(object sender, KeyEventArgs e)
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
