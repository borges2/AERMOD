using AERMOD.LIB.Desenvolvimento;
using AERMOD.LIB.Forms;
using CamadaAcessoDados;
using CamadaLogicaNegocios;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Management;
using System.Net;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

namespace AERMOD.CamadaApresentacao
{
    public partial class FrmAtualizacao : Form
    {
        #region Instâncias e Propriedades

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

        private enum TipoErro
        {
            [Description("Normal")]
            Normal,

            [Description("Backup")]
            Backup,

            [Description("Script")]
            Script,

            [Description("Script/Restaurar")]
            Script_Restaurar,

            [Description("Sistema")]
            Sistema,

            [Description("Sistema/Restaurar")]
            Sistema_Restaurar
        }

        /// <summary>
        /// Sistema bloqueado para operações.
        /// </summary>
        bool bloqueado;

        /// <summary>
        /// Caminho do banco de dados.
        /// </summary>
        string caminhoDataBase;

        #endregion

        #region Construtor

        public FrmAtualizacao()
        {
            InitializeComponent();

            this.Icon = Properties.Resources.Atualizar24x24.ConvertImageToIcon();

            Base.ConfiguracaoRede = new ConfiguracaoRede();
            Base.ConfiguracaoRede.Database = "AERMOD";
            Base.ConfiguracaoRede.Hostname = "127.0.0.1";
            Base.ConfiguracaoRede.Porta = 3307;
            Base.ConfiguracaoRede.Usuario = "root";
            Base.ConfiguracaoRede.Senha = "AERMOD1234";
            Base.ConfiguracaoRede.StringConexao = $"Data Source={Base.ConfiguracaoRede.Hostname}; Port={Base.ConfiguracaoRede.Porta}; User Id={Base.ConfiguracaoRede.Usuario}; Password={Base.ConfiguracaoRede.Senha}; Database={Base.ConfiguracaoRede.Database}; Charset=utf8; Pooling=true; Connection Timeout=2; Default Command Timeout=3600";
        }

        #endregion

        #region Eventos FrmAtualizacao

        private void FrmAtualizacao_Load(object sender, EventArgs e)
        {
            caminhoDataBase = classeParametros.InicializarDataBase(this);

            if (string.IsNullOrEmpty(caminhoDataBase))
            {
                bloqueado = true;
                this.Close();
                return;
            }

            VerificarNovaVersao();
        }

        private void FrmAtualizacao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.S:
                        e.SuppressKeyPress = true;
                        Sair();
                        break;
                    case Keys.A:
                        e.SuppressKeyPress = true;
                        AtualizarVersao();
                        break;
                }
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        e.SuppressKeyPress = true;
                        Sair();
                        break;
                }
            }
        }

        private void FrmAtualizacao_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bloqueado == false)
            {
                classeParametros.EncerrarServicoMySQL(caminhoDataBase);
            }
        }

        private void FrmAERMOD_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Base.VerificaVersao == false)
            {
                this.Close();
            }
            else
            {
                Base.VerificaVersao = false;
                VerificarNovaVersao(false);
            }
        }

        #endregion

        #region Eventos btnSair

        private void btnSair_Click(object sender, EventArgs e)
        {
            Sair();
        }

        #endregion

        #region Eventos btnAtualizar

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            AtualizarVersao();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Sobrescreve eventos de click padrões do windows e framework.
        /// </summary>
        /// <param name="m">Tipo da mensagem referenciada.</param>
        protected override void WndProc(ref Message m)
        {
            if (this.Visible && m.Msg == 0x0010) // WM_CLOSE (fechar o form)
            {
                Sair();
            }

            base.WndProc(ref m);
        }

        /// <summary>
        /// Executar aplicativo como Administrador.
        /// </summary>
        /// <returns></returns>
        public bool IsUserAdministrator()
        {
            //https://stackoverflow.com/questions/2818179/how-do-i-force-my-net-application-to-run-as-administrator

            bool isAdmin;
            try
            {
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (UnauthorizedAccessException)
            {
                isAdmin = false;
            }
            catch
            {
                isAdmin = false;
            }

            return isAdmin;
        }

        /// <summary>
        /// Verificar existência de nova versão.
        /// </summary>
        public void VerificarNovaVersao(bool load = true)
        {
            //Formato padrão de versão ('##.##.#####')
            //00.##.##### = primeira casa (muda projeto inteiro)
            //##.00.##### = segunda casa (nova funcionalidade)
            //##.##.00000 = última casa (correção)

            var versaoAtual = Application.ProductVersion.ToString().Split('.');

            tbxVersaoAtual.Text = $"{versaoAtual[0]}.{versaoAtual[1]}.{versaoAtual[2]}";
            tbxNovaVersao.Text = tbxVersaoAtual.Text;

            string versao = tbxNovaVersao.Text;

            FrmLoading frmLoading = new FrmLoading(this);

            Thread thr = new Thread(delegate ()
            {
                try
                {
                    WebClient webClient = new WebClient();
                    versao = webClient.DownloadString("http://aermod.infinityfreeapp.com/update/Update.txt");
                }
                catch { }
            });
            thr.Start();

            frmLoading.thread = thr;
            frmLoading.PermiteAbortarThread = false;
            frmLoading.Style = ProgressBarStyle.Marquee;
            frmLoading.Maximum = 0;
            frmLoading.Texto = "Procurando nova versão...";
            frmLoading.ShowDialog(this);

            if (string.IsNullOrEmpty(versao) || versao == "1.0.1")
            {
                btnAtualizar.Enabled = false;

                if (load)
                {
                    this.Hide();
                    this.ShowInTaskbar = false;

                    FrmAERMOD frmAERMOD = new FrmAERMOD(caminhoDataBase);
                    frmAERMOD.FormClosed += FrmAERMOD_FormClosed;
                    frmAERMOD.ShowDialog();

                    return;
                }
            }
            else
            {
                btnAtualizar.Enabled = true;
            }

            tbxNovaVersao.Text = versao;

            this.Show();
            this.ShowInTaskbar = true;
        }

        /// <summary>
        /// Sair do controle de versão.
        /// </summary>
        private void Sair()
        {
            if (bloqueado)
            {
                return;
            }

            this.Hide();
            this.ShowInTaskbar = false;

            FrmAERMOD frmAERMOD = new FrmAERMOD(caminhoDataBase);
            frmAERMOD.FormClosed += FrmAERMOD_FormClosed;
            frmAERMOD.ShowDialog();
        }

        /// <summary>
        /// Atualizar versão.
        /// </summary>
        private void AtualizarVersao()
        {
            if (btnAtualizar.Enabled == false)
            {
                return;
            }

            bloqueado = true;

            TipoErro tipoErro = TipoErro.Normal;

            FrmLoading frmLoading = new FrmLoading(this);

            Thread thr = new Thread(delegate ()
            {
                string diretorioUpdate = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "UPDATE");
                
                try
                {
                    if (File.Exists(Path.Combine(diretorioUpdate, "script.sql")))
                    {
                        frmLoading.AtualizarStatus(0, "Efetuando backup...");

                        #region Efetuar backup                        

                        if (EfetuarBackup() == false)
                        {
                            tipoErro = TipoErro.Backup;
                            return;
                        }

                        #endregion

                        frmLoading.AtualizarStatus(0, "Atualizando banco de Dados...");

                        #region Atualizar estrutura do banco                        

                        if (ExecutarScriptDataBase() == false)
                        {
                            tipoErro = TipoErro.Script;

                            frmLoading.AtualizarStatus(0, "Restaurando Backup...");

                            if (RestaurarBackup() == false)
                            {
                                tipoErro = TipoErro.Script_Restaurar;
                            }

                            return;
                        }

                        #endregion
                    }

                    frmLoading.AtualizarStatus(0, "Baixando última versão...");

                    #region Excluir instalador

                    if (File.Exists(Path.Combine(diretorioUpdate, "AERMODOpen.zip")))
                    {
                        File.Delete(Path.Combine(diretorioUpdate, "AERMODOpen.zip"));
                    }

                    if (File.Exists(Path.Combine(diretorioUpdate, "AERMODOpen.msi")))
                    {
                        File.Delete(Path.Combine(diretorioUpdate, "AERMODOpen.msi"));
                    }

                    #endregion

                    #region Baixar instalador exemplo 1

                    //https://stackoverflow.com/questions/45500369/how-to-download-file-from-google-drive-using-c-sharp

                    //string url = "https://drive.google.com/uc?export=download&id=1U7NiMqDkqI_ez6HWGezl7uGDlsurKwOg";
                    //WebClient clientTeste = new WebClient();
                    //clientTeste.UseDefaultCredentials = true;
                    //clientTeste.DownloadFile(url, Path.Combine(diretorio, "AERMODOpen.zip"));

                    #endregion

                    #region Baixar instalador exemplo 2

                    //https://thejpanda.com/2023/01/05/automation-download-files-from-google-drive-in-c/

                    //var url = "https://drive.google.com/uc?export=download&id=1U7NiMqDkqI_ez6HWGezl7uGDlsurKwOg";
                    //var path = Path.Combine(diretorio, "AERMODOpen.zip");

                    //using (var clientTeste = new HttpClient())
                    //{
                    //    using (var s = clientTeste.GetStreamAsync(url))
                    //    {
                    //        using (var fs = new FileStream(path, FileMode.OpenOrCreate))
                    //        {
                    //            s.Result.CopyTo(fs);
                    //        }
                    //    }
                    //}

                    #endregion

                    #region Baixar instalador exemplo 3

                    //FileDownloader fileDownloader = new FileDownloader();
                    //fileDownloader.DownloadFile("https://drive.google.com/file/d/1U7NiMqDkqI_ez6HWGezl7uGDlsurKwOg/view?usp=drive_link", Path.Combine(diretorio, "AERMODOpen.zip"));

                    #endregion

                    #region Baixar instalador exemplo 4                    

                    //string URL_DATABASE = "https://drive.google.com/uc?export=download&id=12lVODIf6_c2qwAUu5IC33KQupOoPuQ8l";
                    string URL = "https://drive.google.com/uc?export=download&id=1aazNHbz2H0jlO2yt4ZC2GEdAlTIYEK9E";

                    WebClient client = new WebClient();
                    client.DownloadFile(URL, Path.Combine(diretorioUpdate, "AERMODOpen.zip"));
                    string zipPath = Path.Combine(diretorioUpdate, "AERMODOpen.zip");
                    string extractPath = diretorioUpdate;
                    ZipFile.ExtractToDirectory(zipPath, extractPath);

                    #endregion

                    frmLoading.AtualizarStatus(0, "Desinstalando versão antiga...");

                    #region Desinstalar versão antiga 

                    //UninstallProgram("AERMOD Open");

                    //Quando compilar nova versão sempre mantém o código do produto
                    string productCode = "{E1D2FEBC-475C-4C32-BE68-054A63D9208A}";

                    Process processUninstall = new Process();
                    processUninstall.StartInfo.FileName = "msiexec.exe";
                    processUninstall.StartInfo.Arguments = "/x \"" + productCode + "\"/qf";                    
                    processUninstall.StartInfo.Verb = "runas"; //Run as administrator   
                    processUninstall.StartInfo.UseShellExecute = false;
                    processUninstall.StartInfo.RedirectStandardOutput = true;
                    processUninstall.Start();                    

                    while (processUninstall.HasExited == false)
                    {
                        continue;
                    }

                    #endregion

                    frmLoading.AtualizarStatus(0, "Instalando nova versão...");

                    #region Instalar nova versão                    

                    tipoErro = TipoErro.Sistema;

                    //https://stackoverflow.com/questions/42440163/install-msi-file-using-c-sharp
                    //https://stackoverflow.com/questions/30708372/installing-software-programmatically-in-background
                    //https://jonathancrozier.com/blog/how-to-install-msi-packages-using-msiexec-and-c-sharp
                    //https://www.codeproject.com/Articles/20059/C-Installing-and-uninstalling-software
                    //https://stackoverflow.com/questions/2818179/how-do-i-force-my-net-application-to-run-as-administrator

                    string installerPath = Path.Combine(diretorioUpdate, "AERMODOpen.msi");

                    Process process = new Process();
                    process.StartInfo.FileName = "msiexec";
                    process.StartInfo.Arguments = "/i \"" + installerPath + "\"";
                    process.StartInfo.Verb = "runas"; //Run as administrator

                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;

                    #region Criar diretório temporário

                    string diretorioTemp = classeParametros.RetornarPastaTemporaria(true);
                    string caminhoArquivoRede = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "DATABASE\\rede.txt");
                    classeParametros.CopyFileToFolder(caminhoArquivoRede, Path.Combine(diretorioTemp, "rede.txt"));

                    #endregion

                    this.Invoke(new Action(() => { this.Close(); }));

                    process.Start();

                    string output = process.StandardOutput.ReadToEnd();

                    process.WaitForExit();

                    if (process.ExitCode != 0)
                    {
                        #region Falha ao concluir                        

                        //if (File.Exists(Path.Combine(diretorioUpdate, "script.sql")))
                        //{
                        //    frmLoading.AtualizarStatus(0, "Restaurando backup...");

                        //    if (RestaurarBackup() == false)
                        //    {
                        //        tipoErro = TipoErro.Sistema_Restaurar;
                        //    }
                        //}

                        #endregion                        
                    }                    

                    tipoErro = TipoErro.Normal;

                    #endregion
                }
                catch
                {
                    if (File.Exists(Path.Combine(diretorioUpdate, "script.sql")))
                    {
                        frmLoading.AtualizarStatus(0, "Restaurando backup...");

                        if (RestaurarBackup() == false)
                        {
                            tipoErro = TipoErro.Sistema_Restaurar;
                        }
                    }
                }

                #region Encerrar serviço MySQL

                frmLoading.AtualizarStatus(0, "Encerrando serviço...");

                classeParametros.EncerrarServicoMySQL(caminhoDataBase);

                #endregion

                #region Excluir SQL

                if (File.Exists(Path.Combine(diretorioUpdate, "aermod.sql")))
                {
                    File.Delete(Path.Combine(diretorioUpdate, "aermod.sql"));
                }

                if (File.Exists(Path.Combine(diretorioUpdate, "script.sql")))
                {
                    File.Delete(Path.Combine(diretorioUpdate, "script.sql"));                    
                }

                #endregion

                #region Excluir instalador

                if (File.Exists(Path.Combine(diretorioUpdate, "AERMODOpen.zip")))
                {
                    File.Delete(Path.Combine(diretorioUpdate, "AERMODOpen.zip"));
                }

                if (File.Exists(Path.Combine(diretorioUpdate, "AERMODOpen.msi")))
                {
                    File.Delete(Path.Combine(diretorioUpdate, "AERMODOpen.msi"));
                }

                #endregion

                bloqueado = false;
            });
            thr.Start();

            frmLoading.thread = thr;
            frmLoading.PermiteAbortarThread = false;
            frmLoading.Style = ProgressBarStyle.Marquee;
            frmLoading.Texto = "Processando...";
            frmLoading.ShowDialogFade(this);            

            if (this.IsDisposed)
            {
                return;
            }

            switch (tipoErro)
            {
                case TipoErro.Backup:
                    MsgBoxLIB.Show(this, "Erro ao efetuar backup do banco de dados.", "Erro", MessageBoxIcon.Error, MessageBoxButtons.OK);
                    break;
                case TipoErro.Script:
                    MsgBoxLIB.Show(this, "Erro ao executar script do banco de dados.", "Erro", MessageBoxIcon.Error, MessageBoxButtons.OK);
                    break;
                case TipoErro.Script_Restaurar:
                    MsgBoxLIB.Show(this, "Erro ao executar script/restaurar banco de dados.", "Erro", MessageBoxIcon.Error, MessageBoxButtons.OK);
                    break;
                case TipoErro.Sistema:
                    MsgBoxLIB.Show(this, "Erro ao atualizar o sistema.", "Erro", MessageBoxIcon.Error, MessageBoxButtons.OK);
                    break;
                case TipoErro.Sistema_Restaurar:
                    MsgBoxLIB.Show(this, "Erro ao atualizar o sistema/restaurar banco de dados.", "Erro", MessageBoxIcon.Error, MessageBoxButtons.OK);
                    break;
            }
        }

        private bool UninstallProgram(string ProgramName)
        {
            //https://stackoverflow.com/questions/30067976/programmatically-uninstall-a-software-using-c-sharp

            try
            {
                ManagementObjectSearcher mos = new ManagementObjectSearcher(
                  "SELECT * FROM Win32_Product WHERE Name = '" + ProgramName + "'");
                foreach (ManagementObject mo in mos.Get())
                {
                    try
                    {
                        if (mo["Name"].ToString() == ProgramName)
                        {
                            object hr = mo.InvokeMethod("Uninstall", null);
                            return (bool)hr;
                        }
                    }
                    catch
                    {
                        //this program may not have a name property, so an exception will be thrown
                    }
                }

                //was not found...
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Efetuar backup DataBase.
        /// </summary>
        private bool EfetuarBackup()
        {
            try
            {
                string diretorio = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "UPDATE");
                string stringConexao = Base.ConfiguracaoRede.StringConexao;

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

                            cmd.CommandText = $"use `{db}`";
                            cmd.ExecuteNonQuery();

                            string file = Path.Combine(diretorio, $"{db}.sql");

                            using (MySqlBackup mb = new MySqlBackup(cmd))
                            {
                                mb.ExportInfo.ExportTableStructure = true;
                                mb.ExportInfo.ExportRows = true;
                                mb.ExportToFile(file);
                            }
                        }

                        conn.Close();
                    }
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Restaurar Backup DataBase.
        /// </summary>
        private bool RestaurarBackup()
        {
            try
            {
                string diretorio = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "UPDATE");
                string stringConexao = Base.ConfiguracaoRede.StringConexao;
                string caminhoArquivo = Path.Combine(diretorio, "aermod.sql");

                if (File.Exists(caminhoArquivo) == false)
                {
                    return false;
                }

                #region Importar arquivo

                string extensao = ".sql";

                string fileExt = Path.GetExtension(caminhoArquivo); //get the file extension
                if (fileExt.ToUpper() != extensao.ToUpper())
                {
                    return false;
                }

                #endregion

                if (!Directory.Exists(Path.GetDirectoryName(caminhoArquivo)))
                {
                    CrossThreadOperation.Invoke(this, delegate { MsgBoxLIB.Show(this, "A pasta selecionada não existe.", "Erro", MessageBoxIcon.Error, MessageBoxButtons.OK); });
                    return false;
                }

                string[] files = { caminhoArquivo };

                using (MySqlConnection conn = new MySqlConnection(stringConexao))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        conn.Open();
                        cmd.Connection = conn;

                        foreach (string file in files)
                        {
                            string db = Path.GetFileNameWithoutExtension(file);

                            cmd.CommandText = "create database if not exists `" + db + "`";
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = $"use `{db}`";
                            cmd.ExecuteNonQuery();

                            using (MySqlBackup mb = new MySqlBackup(cmd))
                            {
                                mb.ImportFromFile(file);
                            }
                        }

                        conn.Close();
                    }
                }
            }
            catch
            {
                return false;
            }

            return true;
        }

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
        /// Executar script DataBase.
        /// </summary>
        private bool ExecutarScriptDataBase()
        {
            try
            {
                string diretorio = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "UPDATE");
                string stringConexao = Base.ConfiguracaoRede.StringConexao;

                //using (MySqlConnection conn = new MySqlConnection(stringConexao))
                //{
                //    using (MySqlCommand cmd = new MySqlCommand())
                //    {
                //        conn.Open();    

                //        cmd.Connection = conn;
                //        string script = File.ReadAllText(Path.Combine(diretorio, "script.sql"));
                //        cmd.CommandText = script;
                //        cmd.ExecuteNonQuery();

                //        conn.Close();
                //    }
                //}

                MySqlConnection conn = new MySqlConnection(stringConexao);
                conn.Open();
                string textoSQL = File.ReadAllText(Path.Combine(diretorio, "script.sql"));
                MySqlScript script = new MySqlScript(conn, textoSQL);
                //script.Delimiter = "&&";
                script.Execute();
                conn.Close();
            }
            catch
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
