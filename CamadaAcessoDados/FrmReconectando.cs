using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CamadaAcessoDados
{
    public partial class FrmReconectando : Form
    {
        #region Declarações

        /// <summary>
        /// Timer utilizado qdo está aguardando resposta da queda de conexão com servidor
        /// para tentar reconectar automaticamente qdo está esperando resposta do usuário.
        /// </summary>
        System.Timers.Timer timerAguardandoResposta = new System.Timers.Timer(1000);

        //Provedor de acesso aos dados
        /// <summary>
        /// Provedor de acesso aos dados.
        /// Recebe o providerName.
        /// </summary>
        public DbProviderFactory factory;

        /// <summary>
        /// Adapta a string de acordo com sua conexão.
        /// Recebe a connectionString.
        /// </summary>
        public string conexao;

        #endregion

        #region Construtor

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="configuracaoRede">Configuração da rede</param>
        public FrmReconectando(ConfiguracaoRede configuracaoRede)
        {
            InitializeComponent();

            conexao = ClsConexao.ConnnectionString(configuracaoRede);
            factory = DbProviderFactories.GetFactory("MySql.Data.MySqlClient");

            lbIp.Text = configuracaoRede.Hostname;
            timerAguardandoResposta.Elapsed += new System.Timers.ElapsedEventHandler(timerAguardandoResposta_Elapsed);
            timerAguardandoResposta.Enabled = true;
        }

        #endregion

        #region Eventos FrmReconectando

        private void FrmReconectando_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Environment.Exit(Environment.ExitCode);
            }
        }

        #endregion

        #region Eventos timerAguardandoResposta

        void timerAguardandoResposta_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            timerAguardandoResposta.Enabled = false;

            try
            {
                DbConnection conexaoUtilizada = factory.CreateConnection();
                conexaoUtilizada.ConnectionString = conexao;

                if (conexaoUtilizada.State == ConnectionState.Closed)
                {
                    conexaoUtilizada.Open();
                    Estado.AguardandoResposta = false;
                }
                conexaoUtilizada.Close();
            }
            catch { }

            timerAguardandoResposta.Enabled = true;
        }

        #endregion

        #region Eventos btnCancelar

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        #endregion
    }
}
