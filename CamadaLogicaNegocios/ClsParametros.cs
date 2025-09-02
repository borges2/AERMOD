using CamadaAcessoDados;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Netsof.LIB.Componentes;
using System.Security.Cryptography;
using AERMOD.LIB.Desenvolvimento;

namespace CamadaLogicaNegocios
{
    public class ClsParametros
    {
        #region Declarações

        /// <summary>
        /// Configuração Banco de Dados.
        /// </summary>
        ConfiguracaoRede configuracaoRede = null;

        #endregion

        #region Construtor

        public ClsParametros(ConfiguracaoRede configuracaoRede)
        {
            this.configuracaoRede = configuracaoRede;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Inicializar banco de dados.
        /// </summary>
        public string InicializarDataBase(Form form)
        {
            string caminhoDataBase = CarregarCaminhoDataBase(form);

            INICIO:

            if (string.IsNullOrEmpty(caminhoDataBase))
            {
                caminhoDataBase = ProcurarCaminhoDataBase(form);

                if (string.IsNullOrEmpty(caminhoDataBase) == false)
                {
                    if (SalvarCaminhoDataBase(form, caminhoDataBase) == false)
                    {
                        return string.Empty;
                    }
                }
                else
                {
                    return string.Empty;
                }
            }

            if (VerificarProcessoMySQL() == false)
            {
                if (IniciarServicoMySQL(form, caminhoDataBase) == false)
                {
                    caminhoDataBase = string.Empty;
                    goto INICIO;
                }
            }            

            List<String> lstDatabases = ClsConexao.TestarConexao(configuracaoRede);

            if (lstDatabases == null)
            {                
                form.Invoke(new Action(() => { MsgBoxLIB.Show(form, "Não foi possível estabelecer conexão com o banco de dados.", "Erro", MessageBoxIcon.Error, MessageBoxButtons.OK); }));
                return string.Empty;
            }

            return caminhoDataBase;
        }

        /// <summary>
        /// Verificar processo MySQL.
        /// </summary>
        /// <returns>Retorna true caso exista</returns>
        public bool VerificarProcessoMySQL()
        {
            Process[] pname1 = Process.GetProcessesByName("mysqld");
            Process[] pname2 = Process.GetProcessesByName("mysql");

            if (pname1.Length == 0 && pname2.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Iniciar serviço MySQL.
        /// </summary>
        public bool IniciarServicoMySQL(Form form, string caminhoDataBase)
        {
            string diretorio = $"{caminhoDataBase}\\bin";

            if (Directory.Exists(diretorio) == false)
            {
                form.Invoke(new Action(() => { MsgBoxLIB.Show(form, "Diretório do banco de dados não encontrado.", "Erro", MessageBoxIcon.Error, MessageBoxButtons.OK); }));
                return false;
            }

            Process p = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    WorkingDirectory = diretorio,
                    FileName = Path.Combine(diretorio, "mysqld.exe"),
                    Arguments = "",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                }
            };
            p.Start();

            return true;
        }

        /// <summary>
        /// Encerrar serviço MySQL.
        /// </summary>
        public void EncerrarServicoMySQL(string caminhoDataBase)
        {
            if (VerificarProcessoMySQL() == false)
            {
                return;
            }

            string diretorio = $"{caminhoDataBase}\\bin";

            if (Directory.Exists(diretorio) == false)
            {                
                return;
            }

            Process p = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    WorkingDirectory = diretorio,
                    FileName = Path.Combine(diretorio, "mysqladmin.exe"),
                    Arguments = "--user=root --password=AERMOD1234 shutdown",
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                }
            };
            p.Start();

            //Process mysqlCloseCMD = new Process();
            //mysqlCloseCMD.StartInfo.FileName = "C:\\Bases\\AERMOD\\bin\\mysqladmin.exe"; //Environment.CurrentDirectory + "\\mysql-server\\bin\\mysqladmin.exe";
            //mysqlCloseCMD.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //mysqlCloseCMD.StartInfo.Arguments = "--user=root shutdown";
            //mysqlCloseCMD.Start();
        }

        /// <summary>
        /// Carregar caminho do banco de dados.
        /// </summary>
        /// <returns>Retorna true caso existe</returns>
        public string CarregarCaminhoDataBase(Form form)
        {
            string diretorioTemp = RetornarPastaTemporaria(false);
            string caminhoDataBase = string.Empty;

            try
            {
                string diretorio = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "DATABASE");

                if (File.Exists(Path.Combine(diretorio, "rede.txt")))
                {
                    string readText = File.ReadAllText(Path.Combine(diretorio, "rede.txt"));

                    if (string.IsNullOrEmpty(readText) == false)
                    {
                        caminhoDataBase = readText.TrimEnd();
                    }
                }
            }
            catch { }

            if (string.IsNullOrEmpty(caminhoDataBase))
            {
                try
                {
                    if (File.Exists(Path.Combine(diretorioTemp, "rede.txt")))
                    {
                        string readText = File.ReadAllText(Path.Combine(diretorioTemp, "rede.txt"));

                        if (string.IsNullOrEmpty(readText) == false)
                        {
                            caminhoDataBase = readText.TrimEnd();
                        }
                    }                    
                }
                catch { }

                if (string.IsNullOrEmpty(caminhoDataBase))
                {
                    form.Invoke(new Action(() => { MsgBoxLIB.Show(form, "Informe o caminho do banco de dados.", "Atenção", MessageBoxIcon.Warning, MessageBoxButtons.OK); }));
                }
            }

            DeleteDirectory(diretorioTemp);

            return caminhoDataBase;
        }

        /// <summary>
        /// Procurar caminho do banco de dados.
        /// </summary>
        public string ProcurarCaminhoDataBase(Form form)
        {
            string caminhoArquivo = string.Empty;

            form.Invoke(new Action(() => 
            {
                FolderBrowserDialog fb = new FolderBrowserDialog();
                if (fb.ShowDialog() == DialogResult.OK)
                {
                    caminhoArquivo = fb.SelectedPath;
                    form.Activate();
                }                
            }));

            if (string.IsNullOrEmpty(caminhoArquivo))
            {
                return string.Empty;
            }

            if (!Directory.Exists(caminhoArquivo))
            {
                form.Invoke(new Action(() => { MsgBoxLIB.Show(form, "A pasta selecionada não existe.", "Erro", MessageBoxIcon.Error, MessageBoxButtons.OK); }));
                return string.Empty;
            }

            return caminhoArquivo;
        }

        /// <summary>
        /// Localizar caminho do banco de dados.
        /// </summary>
        public void ProcurarCaminhoDataBaseF2(Form form)
        {
            string caminhoDataBase = ProcurarCaminhoDataBase(form);

            if (string.IsNullOrEmpty(caminhoDataBase) == false)
            {
                form.Invoke(new Action(() => { MsgBoxLIB.Show(form, "Salvando caminho do banco de dados.", "Atenção", MessageBoxIcon.Warning, MessageBoxButtons.OK); }));

                if (SalvarCaminhoDataBase(form, caminhoDataBase))
                {
                    form.Invoke(new Action(() => { MsgBoxLIB.Show(form, "Gravado com sucesso", "Atenção", MessageBoxIcon.Warning, MessageBoxButtons.OK); }));

                    InicializarDataBase(form);
                }
                else
                {
                    form.Close();
                }
            }
        }

        /// <summary>
        /// Salvar caminho do banco de dados.
        /// </summary>        
        public bool SalvarCaminhoDataBase(Form form, string caminhoDataBase)
        {
            if (Directory.Exists(caminhoDataBase) == false)
            {
                form.Invoke(new Action(() => { MsgBoxLIB.Show(form, "Diretório do Banco de Dados não encontrado.", "Erro", MessageBoxIcon.Error, MessageBoxButtons.OK); }));
                return false;
            }

            try
            {                
                Arquivo.EscreverCaminhoDataBase(caminhoDataBase, true);
            }
            catch
            {
                form.Invoke(new Action(() => { MsgBoxLIB.Show(form, "Erro ao criar o arquivo de rede.", "Erro", MessageBoxIcon.Error, MessageBoxButtons.OK); }));
            }

            return true;
        }

        /// <summary>
        /// Retornar pasta temporária.
        /// </summary>
        /// <returns>Retorna caminho da pasta</returns>
        public string GetTemporaryDirectory()
        {
            string tempFolder = Path.GetTempFileName();
            File.Delete(tempFolder);
            Directory.CreateDirectory(tempFolder);

            return tempFolder;
        }

        /// <summary>
        /// Retornar pasta temporaria.
        /// </summary>
        /// <param name="criar">Criar pasta</param>
        /// <returns>Retorna caminho da pasta</returns>
        public string RetornarPastaTemporaria(bool criar)
        {
            string unidade = RetornarUnidadeDisco();
            string caminhoDiretorio = Path.Combine(unidade, "AERMOD_TEMP");            

            if (criar)
            {
                DeleteDirectory(caminhoDiretorio);
                Directory.CreateDirectory(caminhoDiretorio);
            }

            return caminhoDiretorio;
        }

        /// <summary>
        /// Excluir pasta temporária.
        /// </summary>
        /// <param name="caminhoDiretorio">Caminho do diretório</param>
        public void DeleteDirectory(string caminhoDiretorio)
        {
            if (Directory.Exists(caminhoDiretorio))
            {
                string[] arquivosTemporarios = Directory.GetFiles(caminhoDiretorio);

                foreach (var arquivoAtual in arquivosTemporarios)
                {
                    File.Delete(arquivoAtual);
                }

                Directory.Delete(caminhoDiretorio);                
            }            
        }

        /// <summary>
        /// Copia o arquivo para outro diretório.
        /// </summary>
        /// <param name="origemAquivo">Origem do arquivo</param>
        /// <param name="destinoArquivo">Destino do arquivo</param>
        public void CopyFileToFolder(string origemAquivo, string destinoArquivo)
        {
            File.Copy(origemAquivo, destinoArquivo);
        }

        /// <summary>
        /// Retorna unidade de disco.
        /// </summary>
        /// <returns></returns>
        public string RetornarUnidadeDisco()
        {
            string nome = string.Empty;
            DriveInfo[] drives = DriveInfo.GetDrives();
            
            for (int i = 0; i < drives.Count(); i++)
            {
                nome = drives[i].Name;
            }

            return nome;
        }

        #endregion
    }
}
