using AERMOD.LIB.Desenvolvimento;
using AERMOD.LIB.Forms;
using CamadaAcessoDados;
using CamadaLogicaNegocios;
using GMap.NET.MapProviders;
using Microsoft.VisualStudio.OLE.Interop;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AERMOD.CamadaApresentacao
{
    public partial class FrmBackup : Form
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

        BackgroundWorker bw = new BackgroundWorker();
        bool isBackup = true;
        int count = 0;
        DateTime timeProcessStart = DateTime.Now;
        bool existeErro = false;
        string msgErro = string.Empty;

        /// <summary>
        /// String de conexão.
        /// </summary>
        string stringConexao;

        /// <summary>
        /// Caminho do arquivo para salvar/restaurar.
        /// </summary>
        string caminhoArquivo;

        #endregion

        #region Construtor

        public FrmBackup()
        {
            InitializeComponent();

            this.Icon = Properties.Resources.backupMySql24.ConvertImageToIcon();

            bw.WorkerReportsProgress = true;
            bw.DoWork += Bw_DoWork;
            bw.RunWorkerCompleted += Bw_RunWorkerCompleted;
            bw.ProgressChanged += Bw_ProgressChanged;
        }

        #endregion        

        #region Métodos

        /// <summary>
        /// Retorna base de dados.
        /// </summary>
        /// <param name="cmd">Comando</param>
        /// <param name="sql">SQL</param>
        /// <returns>Retorna DataTable</returns>
        DataTable RetornaBaseDados(MySqlCommand cmd, string sql)
        {
            cmd.CommandText = sql;
            DataTable dt = new DataTable();
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

        /// <summary>
        /// Faz validação antes de criar backup.
        /// </summary>
        private void ValidarCriarBackup()
        {
            caminhoArquivo = string.Empty;

            FolderBrowserDialog fb = new FolderBrowserDialog();
            if (fb.ShowDialog() == DialogResult.OK)
            {
                caminhoArquivo = fb.SelectedPath;
            }
            else
            {
                return;
            }

            if (!Directory.Exists(caminhoArquivo))
            {
                MsgBoxLIB.Show(this, classeHelp.BuscarMensagem(67), classeHelp.BuscarMensagem(2), MessageBoxIcon.Warning, MessageBoxButtons.OK);
                return;
            }

            isBackup = true;

            if (!CarregarDados())
            {
                return;
            }

            bw.RunWorkerAsync();
        }

        /// <summary>
        /// Criar backup.
        /// </summary>
        private void CriarBackup()
        {
            using (MySqlConnection conn = new MySqlConnection(stringConexao))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    conn.Open();
                    cmd.Connection = conn;

                    DataTable dt = RetornaBaseDados(cmd, "show databases;");

                    foreach (DataRow dr in dt.Rows)
                    {
                        string db = dr[0] + "";

                        //switch (db)
                        //{
                        //    case "sys":
                        //    case "performance_schema":
                        //    case "mysql":
                        //    case "information_schema":
                        //        continue;
                        //}

                        if (db != "aermod")
                        {
                            continue;
                        }

                        DateTime dateStart = DateTime.Now;

                        string appendText = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "  Copiando " + db + "....";
                        bw.ReportProgress(0, appendText);

                        cmd.CommandText = $"use `{db}`";
                        cmd.ExecuteNonQuery();

                        string file = Path.Combine(caminhoArquivo, $"{db}.sql");

                        using (MySqlBackup mb = new MySqlBackup(cmd))
                        {
                            mb.ExportInfo.ExportTableStructure = chboxExportaEstrutura.Checked;
                            mb.ExportInfo.ExportRows = chboxExportaLinha.Checked;                            
                            mb.ExportToFile(file);
                        }

                        DateTime dateEnd = DateTime.Now;

                        var timeElapsed = dateEnd - dateStart;

                        appendText = $" completado ({timeElapsed.Hours} h {timeElapsed.Minutes} m {timeElapsed.Seconds} s {timeElapsed.Milliseconds} ms)" + "\r\n";
                        bw.ReportProgress(0, appendText);

                        count++;
                    }

                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Faz validação antes de restaurar backup.
        /// </summary>
        private void ValidarRestaurarBackup()
        {
            caminhoArquivo = string.Empty;

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Selecionar arquivo";
            ofd.Multiselect = false;
            DialogResult dr = ofd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                if (ofd.FileName == "" || !File.Exists(ofd.FileName))
                {
                    //
                    // If the requested file is not ok...
                    //
                    return;
                }

                #region Importar arquivo

                string extensao = ".sql";

                string fileExt = Path.GetExtension(ofd.FileName); //get the file extension
                if (fileExt.ToUpper() != extensao.ToUpper())
                {
                    MessageBox.Show(this, classeHelp.BuscarMensagem(68), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    caminhoArquivo = ofd.FileName;
                }

                #endregion
            }
            else
            {
                return;
            }
            
            if (!Directory.Exists(Path.GetDirectoryName(caminhoArquivo)))
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(67), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            isBackup = false;

            if (!CarregarDados())
            {
                return;
            }

            bw.RunWorkerAsync();
        }

        /// <summary>
        /// Restaurar backup.
        /// </summary>
        private void RestaurarBackup()
        {
            string[] files = { caminhoArquivo }; //Directory.GetFiles(Path.GetDirectoryName(caminhoArquivo));

            using (MySqlConnection conn = new MySqlConnection(stringConexao))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    conn.Open();
                    cmd.Connection = conn;

                    foreach (string file in files)
                    {
                        string db = Path.GetFileNameWithoutExtension(file);

                        DateTime dateStart = DateTime.Now;

                        string appendText = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + "  Restaurando " + db + "....";
                        bw.ReportProgress(0, appendText);

                        cmd.CommandText = "create database if not exists `" + db + "`";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = $"use `{db}`";
                        cmd.ExecuteNonQuery();

                        using (MySqlBackup mb = new MySqlBackup(cmd))
                        {
                            mb.ImportFromFile(file);
                        }

                        DateTime dateEnd = DateTime.Now;

                        var timeElapsed = dateEnd - dateStart;

                        appendText = $" concluído ({timeElapsed.Hours} h {timeElapsed.Minutes} m {timeElapsed.Seconds} s {timeElapsed.Milliseconds} ms)\r\n";
                        bw.ReportProgress(0, appendText);

                        count++;
                    }

                    conn.Close();
                }
            }
        }

        /// <summary>
        /// Carregar dados.
        /// </summary>
        private bool CarregarDados()
        {
            GC.Collect();
            existeErro = false;
            msgErro = "";
            count = 0;
            timeProcessStart = DateTime.Now;
            tbxProgresso.Text = "Iniciado em " + timeProcessStart.ToString("dd/MM/yyyy HH:mm:ss ffff") + "\r\n\r\n";
            this.Refresh();

            if (stringConexao.Length == 0)
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(65), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK);
                return false;
            }

            return true;
        }

        #endregion

        #region Eventos BackgroundWorker

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (isBackup)
                {
                    CriarBackup();
                }
                else
                {
                    RestaurarBackup();
                }
            }
            catch (Exception ex)
            {
                existeErro = true;
                msgErro = ex.ToString();
            }
        }

        private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DateTime timeProcessEnd = DateTime.Now;

            var timeTotal = timeProcessEnd - timeProcessStart;

            this.SuspendLayout();

            tbxProgresso.Text = tbxProgresso.Text + "\r\nProcesso finalizado em " + timeProcessEnd.ToString("dd/MM/yyyy HH:mm:ss ffff") + "\r\n\r\nTempo total decorrido: " + string.Format("{0} h {1} m {2} s {3} ms", timeTotal.Hours, timeTotal.Minutes, timeTotal.Seconds, timeTotal.Milliseconds);

            if (existeErro)
            {
                tbxProgresso.AppendText("\r\nErro:\r\n\r\n");
                tbxProgresso.AppendText(msgErro);
            }

            tbxProgresso.Select(tbxProgresso.Text.Length - 1, 0);
            tbxProgresso.ScrollToCaret();

            this.ResumeLayout();
            this.PerformLayout();

            if (isBackup)
            {
                MessageBox.Show(this, $"{count} base(s) de dados exportado para\r\n\r\n{caminhoArquivo}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show(this, $"{count} base(s) de dados importado de\r\n\r\n{caminhoArquivo}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK);
            }
        }

        private void Bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            tbxProgresso.AppendText(e.UserState + "");
            tbxProgresso.Select(tbxProgresso.Text.Length - 1, 0);
            tbxProgresso.ScrollToCaret();
        }

        #endregion

        #region Eventos FrmBackup

        private void FrmBackup_Load(object sender, EventArgs e)
        {
            stringConexao = $"Data Source={Base.ConfiguracaoRede.Hostname}; Port={Base.ConfiguracaoRede.Porta}; User Id={Base.ConfiguracaoRede.Usuario}; Password={Base.ConfiguracaoRede.Senha}; Database={Base.ConfiguracaoRede.Database}; Charset=utf8; Pooling=true; Connection Timeout=2; Default Command Timeout=3600";
        }

        private void FrmBackup_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.B:
                        e.SuppressKeyPress = true;
                        ValidarCriarBackup();
                        break;
                    case Keys.R:
                        e.SuppressKeyPress = true;
                        ValidarRestaurarBackup();
                        break;
                }
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        this.Close();
                        break;
                }
            }
        }

        #endregion

        #region Eventos btnBackup

        private void btnBackup_Click(object sender, EventArgs e)
        {
            ValidarCriarBackup();
        }

        #endregion

        #region Eventos btnRestaurarBackup

        private void btnRestaurarBackup_Click(object sender, EventArgs e)
        {
            ValidarRestaurarBackup();
        }

        #endregion
    }
}
