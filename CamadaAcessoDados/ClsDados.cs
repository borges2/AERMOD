using AERMOD.LIB;
using AERMOD.LIB.Desenvolvimento;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CamadaAcessoDados
{
    public class ClsDados
    {
        #region Variáveis

        /// <summary>
        /// Colocar janela em primeiro plano.
        /// Utilizado para ativar o Form que estava ativo antes da queda da conexão.
        /// </summary>
        /// <param name="handle">Manipulador do Form que estava ativo antes da queda da conexão.</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr handle);

        public Boolean DesativarErroConexao = false;

        /// <summary>
        /// Adapta a string de acordo com sua conexão.
        /// Recebe a connectionString.
        /// </summary>
        public string conexao;

        /// <summary>
        /// Provedor de acesso aos dados.
        /// Recebe o providerName.
        /// </summary>
        public DbProviderFactory factory;

        /// <summary>
        /// Retorna o status da conexão com o banco de dados
        /// </summary>
        private StatusConnection status;

        /// <summary>
        /// Retorna o status da conexão com o banco de dados
        /// </summary>
        public StatusConnection Status
        {
            get { return status; }
        }

        /// <summary>
        /// Configuração de rede.
        /// </summary>
        ConfiguracaoRede configuracaoRede;

        delegate T ObjectActivator<T>();

        #endregion

        #region Construtor

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="configuracaoRede">Configuração de rede</param>
        public ClsDados(ConfiguracaoRede configuracaoRede)
        {
            this.configuracaoRede = configuracaoRede;

            var sb = new StringBuilder();

            sb.AppendFormat("Data Source={0}; ", configuracaoRede.Hostname);
            sb.AppendFormat("Port={0}; ", configuracaoRede.Porta);
            sb.AppendFormat("User Id={0}; ", configuracaoRede.Usuario);
            sb.AppendFormat("Password={0}; ", configuracaoRede.Senha);
            sb.AppendFormat("Database={0}; ", configuracaoRede.Database);

            sb.Append("Charset=utf8; ");
            sb.Append("Pooling=true; ");
            sb.Append("Respect Binary Flags=false; ");

            sb.Append("Connection Timeout=2; ");
            sb.Append("Default Command Timeout=3600; ");

            conexao = sb.ToString();
            factory = DbProviderFactories.GetFactory("MySql.Data.MySqlClient");
        }

        #endregion

        #region AbrirBanco

        /// <summary>
        /// Método para Abrir o Banco de Dados
        /// </summary>
        /// <param name="aguardaResposta">Se deve ou não aguardar resposta de retorno da conexão.</param>
        /// <returns>Conexão aberta</returns>       
        public DbConnection AbrirBanco(bool aguardaResposta = true)
        {
            DbConnection conexaoUtilizada = factory.CreateConnection();
            conexaoUtilizada.ConnectionString = conexao;
            Form formPrincipal = Application.OpenForms.OfType<Form>().Where(I => I.Name == "FrmLogin" || I.Name == "FrmAERMOD").FirstOrDefault();

            while (aguardaResposta && Estado.AguardandoResposta)
            {
                if (formPrincipal != null)
                {
                    formPrincipal.Invoke((MethodInvoker)Application.DoEvents);
                }
            }

            try
            {
                if (conexaoUtilizada.State == ConnectionState.Closed)
                {
                    conexaoUtilizada.Open();

                    FrmReconectando frmAguardando = (FrmReconectando)Application.OpenForms.OfType<Form>().Where(I => I.Name == "FrmReconectando").FirstOrDefault();
                    if (frmAguardando != null)
                    {
                        frmAguardando.Invoke((MethodInvoker)frmAguardando.Close);
                    }

                    Estado.AguardandoResposta = false;

                    if (Estado.formUltimo != null)
                    {
                        SetForegroundWindow(Estado.formUltimo.Handle);
                        Estado.formUltimo = null;
                    }
                }

                status = StatusConnection.ON;
            }
            catch (Exception e)
            {
                if (DesativarErroConexao)
                    throw e;

                status = StatusConnection.OFF;

                if (e.InnerException != null && e.InnerException.GetType() == typeof(ThreadAbortException))
                    return null;

                if (e.GetType().Name == "MySqlException" || e.InnerException != null && e.InnerException.GetType() == typeof(SocketException) && (((SocketException)e.InnerException).SocketErrorCode == SocketError.NetworkUnreachable || ((SocketException)e.InnerException).SocketErrorCode == SocketError.HostUnreachable))
                {
                    Form form = Application.OpenForms.OfType<Form>().FirstOrDefault();
                    if (form != null && (form.ActiveControl != null || Application.OpenForms.OfType<Form>().Any(I => I.Name == "FrmDesktop")))
                    {
                        Estado.formUltimo = Form.ActiveForm;
                        Estado.AguardandoResposta = true;

                        CrossThreadOperation.Invoke(formPrincipal, delegate
                        {
                            FrmReconectando frm = new FrmReconectando(configuracaoRede);
                            frm.Show(formPrincipal);
                        });

                        while (Estado.AguardandoResposta)
                        {
                            formPrincipal.Invoke((MethodInvoker)Application.DoEvents);
                        }

                        return AbrirBanco(false);
                    }
                }
            }

            return conexaoUtilizada;
        }

        #endregion

        #region FecharBanco

        /// <summary>
        /// Método para fechar o banco
        /// </summary>
        /// <param name="cn"></param>
        public void FecharBanco(DbConnection cn)
        {
            if (cn.State == ConnectionState.Open)
            {
                cn.Close();
            }
        }

        #endregion

        #region TestarConexao

        /// <summary>
        /// Testa conexão e atualiza status da conexão.
        /// </summary>
        public bool TestarConexao(Boolean utilizarDatabase = false)
        {
            try
            {
                DbConnection conexaoUtilizada = factory.CreateConnection();

                if (utilizarDatabase)
                {
                    conexaoUtilizada.ConnectionString = ClsConexao.ConnnectionString(configuracaoRede);
                }
                else
                {
                    conexaoUtilizada.ConnectionString = conexao;
                }

                if (conexaoUtilizada.State == ConnectionState.Closed)
                {
                    conexaoUtilizada.Open();
                    status = StatusConnection.ON;
                }
                conexaoUtilizada.Close();

                return true;
            }
            catch
            {
                status = StatusConnection.OFF;
                return false;
            }
        }

        /// <summary>
        /// Testa conexão e atualiza status da conexão.
        /// </summary>
        public bool TestarConexao(string connectionString)
        {
            try
            {
                DbConnection conexaoUtilizada = factory.CreateConnection();

                conexaoUtilizada.ConnectionString = connectionString;

                conexaoUtilizada.Open();
                status = StatusConnection.ON;
                conexaoUtilizada.Close();

                return true;
            }
            catch
            {
                status = StatusConnection.OFF;
                return false;
            }
        }

        #endregion

        #region ExecutarComando

        /// <summary>
        /// Método para execução de comando
        /// </summary>
        /// <param name="strQuery">sql</param>
        public void ExecutarComando(string strQuery, List<DbParameter> listParametros = null, Int32 timeout = 0, DbConnection conexao = null)
        {
            using (DbCommand cmd = factory.CreateCommand())
            {
                if (timeout > 0)
                {
                    cmd.CommandTimeout = timeout;
                }
                cmd.CommandText = strQuery;
                cmd.CommandType = CommandType.Text;

                if (conexao != null)
                {
                    cmd.Connection = conexao;

                    if (listParametros != null)
                    {
                        if (cmd.Parameters.GetType() == typeof(MySql.Data.MySqlClient.MySqlParameterCollection))
                        {
                            IDictionary<String, Int32> indexHashCS = new Dictionary<String, Int32>();
                            IDictionary<String, Int32> indexHashCI = new Dictionary<String, Int32>();

                            for (int i = 0; i < listParametros.Count; i++)
                            {
                                String parameterName = listParametros[i].ParameterName;

                                indexHashCS.Add(parameterName, i);
                                indexHashCI.Add(parameterName, i);
                            }

                            cmd.Parameters.GetType().GetField("items", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(cmd.Parameters, listParametros.Cast<MySql.Data.MySqlClient.MySqlParameter>().ToList());
                            cmd.Parameters.GetType().GetField("indexHashCS", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(cmd.Parameters, indexHashCS);
                            cmd.Parameters.GetType().GetField("indexHashCI", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(cmd.Parameters, indexHashCI);


                            //ss1.AddRange(((MySql.Data.MySqlClient.MySqlParameterCollection)(cmd.Parameters)).GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance));

                            //cmd.Parameters.SetPropertyValue("items", listParametros);

                        }
                        else
                        {
                            cmd.Parameters.AddRange(listParametros.ToArray());
                        }
                    }

                    cmd.ExecuteNonQuery();

                    if (conexao == null)
                    {
                        FecharBanco(cmd.Connection);
                    }
                }
                else
                {
                    using (cmd.Connection = AbrirBanco())
                    {
                        if (listParametros != null)
                        {
                            cmd.Parameters.AddRange(listParametros.ToArray());
                        }

                        cmd.ExecuteNonQuery();

                        if (conexao == null)
                        {
                            FecharBanco(cmd.Connection);
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Método para execução de comando utilizando
        /// transação a qual pertence o command.
        /// </summary>
        /// <param name="strQuery">sql</param>
        /// <param name="Transacao">Transação a qual pertence o command.</param>
        /// <param name="listParametros">Lista de parâmetros.</param>
        public void ExecutarComando(string strQuery, DbTransaction Transacao, List<DbParameter> listParametros = null, Int32 timeout = 0)
        {
            if (Transacao == null)
            {
                ExecutarComando(strQuery, listParametros);
                return;
            }

            using (DbCommand cmd = factory.CreateCommand())
            {
                if (timeout > 0)
                {
                    cmd.CommandTimeout = timeout;
                }

                cmd.CommandText = strQuery;
                cmd.CommandType = CommandType.Text;

                if (listParametros != null)
                {
                    cmd.Parameters.AddRange(listParametros.ToArray());
                }

                cmd.Connection = Transacao.Connection;
                cmd.Transaction = Transacao;
                cmd.ExecuteNonQuery();
            }
        }

        #endregion

        #region CriarParâmetros

        /// <summary>
        /// Método que retorna parâmetros
        /// </summary>
        /// <param name="nomeParametro">nome do parametro</param>
        /// <param name="tipoParametro">Tipo primitivo</param>
        /// <param name="valorParametro">valor</param>
        /// <returns>DbParameter</returns>
        public DbParameter criaParametros(String nomeParametro, DbType tipoParametro, Object valorParametro)
        {
            //Cria o Parâmetro e add seus valores
            DbParameter parametro = factory.CreateParameter();
            parametro.ParameterName = nomeParametro;
            parametro.DbType = tipoParametro;
            parametro.Value = valorParametro;

            //Retorna o Parâmetro criado
            return parametro;
        }

        /// <summary>
        /// Método que retorna parâmetros
        /// </summary>
        /// <param name="nomeParametro">nome do parametro</param>
        /// <param name="tipoParametro">Tipo primitivo</param>
        /// <param name="valorParametro">valor</param>
        /// <returns>DbParameter</returns>
        public DbParameter criaParametroEmBranco(String nomeParametro)
        {
            //Cria o Parâmetro e add seus valores
            DbParameter parametro = factory.CreateParameter();
            parametro.ParameterName = nomeParametro;

            //Retorna o Parâmetro criado
            return parametro;
        }

        public List<DbParameter> ToDbParameter(List<Parametro> listParametro)
        {
            List<DbParameter> listDbParameter = new List<DbParameter>();

            foreach (Parametro item in listParametro)
            {
                DbParameter parametro = factory.CreateParameter();
                parametro.ParameterName = item.NomeParametro;
                parametro.DbType = item.Tipo;
                parametro.Value = item.Value;

                listDbParameter.Add(parametro);
            }

            return listDbParameter;
        }

        #endregion

        #region Transação

        /// <summary>
        /// Retorna uma transação iniciada para
        /// um DbCommand.
        /// </summary>
        /// <param name="level">Nivel de isolamento da transação.</param>
        /// <returns>Retorna DbTransaction.</returns>
        public DbTransaction AbrirTransaction(IsolationLevel? level = null)
        {
            DbTransaction Transacao = null;

            if (level.HasValue == false)
            {
                Transacao = AbrirBanco().BeginTransaction();
            }
            else
            {
                Transacao = AbrirBanco().BeginTransaction(level.Value);
            }

            return Transacao;
        }

        /// <summary>
        /// Tipo de execução da transação.
        /// </summary>
        /// <param name="commit">
        /// True executa Commit.
        /// False executa Rollback.
        /// </param>
        /// <param name="Transacao">Recebe um DbTransaction como parâmetro.</param>
        public void ExecutarTransaction(bool commit, DbTransaction Transacao)
        {
            if (Transacao.Connection.State == ConnectionState.Open)
            {
                if (commit)
                {
                    Transacao.Commit();
                }
                else
                {
                    Transacao.Rollback();
                }

                FecharBanco(Transacao.Connection);
            }
        }

        #endregion

        #region ExecutarParâmetros

        /// <summary>
        /// Executa sql com parametros
        /// </summary>
        /// <param name="cmmdText">sql</param>
        /// <param name="listaParametros">Lista de parametros</param>
        public void ExecutaParametros(String cmmdText, List<DbParameter> listaParametros)
        {
            using (DbCommand cmd = factory.CreateCommand())
            {
                cmd.CommandText = cmmdText;
                cmd.CommandType = CommandType.Text;

                if (listaParametros != null)
                {
                    foreach (DbParameter parametro in listaParametros)
                    {
                        //Adicionando o parâmetro
                        cmd.Parameters.Add(parametro);
                    }
                }

                using (cmd.Connection = AbrirBanco())
                {
                    cmd.ExecuteNonQuery();

                    FecharBanco(cmd.Connection);
                }
            }
        }

        /// <summary>
        /// Executa sql com parametros
        /// </summary>
        /// <param name="cmmdText">sql</param>
        /// <param name="Transacao">Transação a qual pertence o command.</param>
        /// <param name="listaParametros">Lista de parametros</param>
        public void ExecutaParametros(String cmmdText, DbTransaction Transacao, List<DbParameter> listaParametros)
        {
            if (Transacao == null)
            {
                ExecutaParametros(cmmdText, listaParametros);
                return;
            }

            using (DbCommand cmd = factory.CreateCommand())
            {
                cmd.CommandText = cmmdText;
                cmd.CommandType = CommandType.Text;

                if (listaParametros != null)
                {
                    foreach (DbParameter parametro in listaParametros)
                    {
                        //Adicionando o parâmetro
                        cmd.Parameters.Add(parametro);
                    }
                }

                cmd.Connection = Transacao.Connection;
                cmd.Transaction = Transacao;
                cmd.ExecuteNonQuery();

            }
        }

        #endregion        

        #region RetornarRegistros

        /// <summary>
        /// Método para retornar DataTable
        /// </summary>
        /// <param name="strQuery">sql</param>
        /// <param name="listaParametros">Lista de parametros</param>
        /// <returns>DataTable</returns>
        public DataTable RetornarDataTable(string strQuery, List<DbParameter> listaParametros = null)
        {
            DataTable dt = new DataTable();

            using (DbCommand cmd = factory.CreateCommand())
            {
                cmd.CommandText = strQuery;
                cmd.CommandType = CommandType.Text;

                if (listaParametros != null)
                {
                    foreach (DbParameter parametro in listaParametros)
                    {
                        //Adicionando o parâmetro
                        cmd.Parameters.Add(parametro);
                    }
                }

                using (cmd.Connection = AbrirBanco())
                {
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            for (int i = 0;
                                i < reader.FieldCount; i++)
                            {
                                dt.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
                            }

                            while (reader.Read())
                            {
                                object[] valores = new object[reader.FieldCount];

                                reader.GetValues(valores);

                                dt.Rows.Add(valores);
                            }
                        }
                    }

                    FecharBanco(cmd.Connection);
                }
            }

            return dt;
        }

        public DataTable RetornarDataTable(string strQuery, List<DbParameter> listaParametros, params Tuple<string, Type>[] listColumnsType)
        {
            DataTable dt = new DataTable();

            using (DbCommand cmd = factory.CreateCommand())
            {
                cmd.CommandText = strQuery;
                cmd.CommandType = CommandType.Text;

                if (listaParametros != null)
                {
                    foreach (DbParameter parametro in listaParametros)
                    {
                        //Adicionando o parâmetro
                        cmd.Parameters.Add(parametro);
                    }
                }

                using (cmd.Connection = AbrirBanco())
                {
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        Tuple<string, Type> tupleColumn = null;
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            tupleColumn = listColumnsType.FirstOrDefault(f => f.Item1.ToUpper() == reader.GetName(i).ToUpper());

                            if (tupleColumn != null)
                            {
                                dt.Columns.Add(reader.GetName(i), tupleColumn.Item2);
                            }
                            else
                            {
                                dt.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
                            }
                        }

                        while (reader.Read())
                        {
                            object[] valores = new object[reader.FieldCount];

                            reader.GetValues(valores);

                            dt.Rows.Add(valores);
                        }
                    }

                    FecharBanco(cmd.Connection);
                }
            }

            return dt;
        }

        public DataTable RetornarDataTableTime(string strQuery, Int32 timeout, List<DbParameter> listaParametros = null)
        {
            DataTable dt = new DataTable();

            using (DbCommand cmd = factory.CreateCommand())
            {
                cmd.CommandText = strQuery;
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = timeout;

                if (listaParametros != null)
                {
                    foreach (DbParameter parametro in listaParametros)
                    {
                        //Adicionando o parâmetro
                        cmd.Parameters.Add(parametro);
                    }
                }

                using (cmd.Connection = AbrirBanco())
                {
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            dt.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
                        }

                        while (reader.Read())
                        {
                            object[] valores = new object[reader.FieldCount];

                            reader.GetValues(valores);

                            dt.Rows.Add(valores);
                        }
                    }

                    FecharBanco(cmd.Connection);
                }
            }

            return dt;
        }

        public DataTable RetornarDataTableTime(string strQuery, Int32 timeout, List<DbParameter> listaParametros, params Tuple<string, Type>[] listColumnsType)
        {
            DataTable dt = new DataTable();

            using (DbCommand cmd = factory.CreateCommand())
            {
                cmd.CommandText = strQuery;
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = timeout;

                if (listaParametros != null)
                {
                    foreach (DbParameter parametro in listaParametros)
                    {
                        //Adicionando o parâmetro
                        cmd.Parameters.Add(parametro);
                    }
                }

                using (cmd.Connection = AbrirBanco())
                {
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        Tuple<string, Type> tupleColumn = null;
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            tupleColumn = listColumnsType.FirstOrDefault(f => f.Item1.ToUpper() == reader.GetName(i).ToUpper());

                            if (tupleColumn != null)
                            {
                                dt.Columns.Add(reader.GetName(i), tupleColumn.Item2);
                            }
                            else
                            {
                                dt.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
                            }
                        }

                        while (reader.Read())
                        {
                            object[] valores = new object[reader.FieldCount];

                            reader.GetValues(valores);

                            dt.Rows.Add(valores);
                        }
                    }

                    FecharBanco(cmd.Connection);
                }
            }

            return dt;
        }

        /// <summary>
        /// Método para retornar DataTable utilizando transação
        /// </summary>
        /// <param name="strQuery">sql</param>
        /// <param name="listaParametros">Lista de parametros</param>
        /// <param name="Transacao">Transação a qual pertence o command.</param>
        /// <returns>DataTable</returns>
        public DataTable RetornarDataTable(string strQuery, List<DbParameter> listaParametros, DbTransaction Transacao)
        {
            if (Transacao == null)
                return RetornarDataTable(strQuery, listaParametros);

            DataTable dt = new DataTable();

            using (DbCommand cmd = factory.CreateCommand())
            {
                cmd.CommandText = strQuery;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = Transacao.Connection;
                cmd.Transaction = Transacao;

                if (listaParametros != null)
                {
                    foreach (DbParameter parametro in listaParametros)
                    {
                        //Adicionando o parâmetro
                        cmd.Parameters.Add(parametro);
                    }
                }

                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        dt.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
                    }

                    while (reader.Read())
                    {
                        object[] valores = new object[reader.FieldCount];

                        reader.GetValues(valores);

                        dt.Rows.Add(valores);
                    }


                }
            }

            return dt;
        }

        public DataTable PreencherDataTable(DataTable dt, string strQuery, List<DbParameter> listaParametros, DbTransaction Transacao)
        {
            if (dt == null || dt.Columns.Count == 0)
            {
                return RetornarDataTable(strQuery, listaParametros, Transacao);
            }

            if (Transacao == null)
                return RetornarDataTable(strQuery, listaParametros);

            using (DbCommand cmd = factory.CreateCommand())
            {
                cmd.CommandText = strQuery;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = Transacao.Connection;
                cmd.Transaction = Transacao;

                if (listaParametros != null)
                {
                    foreach (DbParameter parametro in listaParametros)
                    {
                        //Adicionando o parâmetro
                        cmd.Parameters.Add(parametro);
                    }
                }

                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        object[] valores = new object[reader.FieldCount];

                        reader.GetValues(valores);

                        dt.Rows.Add(valores);
                    }
                }
            }

            return dt;
        }

        /// <summary>
        /// Retorna um DataTable com paginação
        /// </summary>
        /// <param name="strQuery">Sql</param>
        /// <param name="listaParametros">Lista de parametro</param>
        /// <param name="inicio">Incio da paginação</param>
        /// <param name="size">Quantidade de registro selecionados</param>
        /// <returns></returns>
        public DataTable RetornarDataTablePaginado(string strQuery, List<DbParameter> listaParametros, int inicio, int size)
        {
            DataTable dt = new DataTable();

            using (DbCommand cmd = factory.CreateCommand())
            {
                cmd.CommandText = strQuery;
                cmd.CommandType = CommandType.Text;

                if (listaParametros != null)
                {
                    foreach (DbParameter parametro in listaParametros)
                    {
                        //Adicionando o parâmetro
                        cmd.Parameters.Add(parametro);
                    }
                }

                using (cmd.Connection = AbrirBanco())
                {
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            dt.Columns.Add(reader.GetName(i), reader.GetFieldType(i));
                        }

                        Int32 contador = 0;
                        while (true)
                        {
                            reader.Read();

                            if (++contador == inicio)
                                break;
                        }

                        contador = 0;
                        while (reader.Read())
                        {
                            if (contador == size)
                                break;

                            object[] valores = new object[reader.FieldCount];

                            reader.GetValues(valores);

                            dt.Rows.Add(valores);
                        }
                    }

                    FecharBanco(cmd.Connection);
                }
            }

            return dt;
        }

        #endregion

        #region RetornarDataAdapter

        /// <summary>
        /// Método que retorna DbDataAdapter
        /// </summary>
        /// <param name="strQuery">sql</param>
        /// <returns>DbDataAdapter</returns>
        public DbDataAdapter RetornarDbDataAdapter(string strQuery)
        {
            DbDataAdapter da = factory.CreateDataAdapter();

            using (DbCommand cmd = factory.CreateCommand())
            {
                cmd.CommandText = strQuery;
                cmd.CommandType = CommandType.Text;

                using (cmd.Connection = AbrirBanco())
                {
                    da.SelectCommand = cmd;
                }
            }

            return da;
        }

        #endregion

        #region RetornarDataReader

        //Método para retornar um DataReader()
        /// <summary>
        /// Método para retornar um objeto DataReader.
        /// </summary>
        /// <param name="strQuery">Comando sql.</param>
        /// <returns>Retorna DataReader com registros de acordo com sql informado.</returns>
        public DbDataReader RetornarDataReader(string strQuery, List<DbParameter> listaParametros = null)
        {
            DbCommand cmd = factory.CreateCommand();
            DbDataReader DbDados = null;

            try
            {
                cmd.CommandText = strQuery.ToString();
                cmd.CommandType = CommandType.Text;

                if (listaParametros != null)
                {
                    foreach (DbParameter parametro in listaParametros)
                    {
                        //Adicionando o parâmetro
                        cmd.Parameters.Add(parametro);
                    }
                }

                cmd.Connection = AbrirBanco();
                DbDados = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch
            {

            }

            return DbDados;
        }

        //Método para retornar um DataReader()
        /// <summary>
        /// Método para retornar um objeto DataReader.
        /// </summary>
        /// <param name="strQuery">Comando sql.</param>
        /// <param name="Transacao">Transação utilizada.</param>
        /// <param name="listaParametros">Lista com parâmetros.</param>
        /// <returns>Retorna DataReader com registros de acordo com sql informado.</returns>
        public DbDataReader RetornarDataReader(string strQuery, DbTransaction Transacao, List<DbParameter> listaParametros = null)
        {
            if (Transacao == null)
                return RetornarDataReader(strQuery, listaParametros);

            DbCommand cmd = factory.CreateCommand();
            DbDataReader DbDados = null;

            try
            {
                cmd.CommandText = strQuery.ToString();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = Transacao.Connection;
                cmd.Transaction = Transacao;

                if (listaParametros != null)
                {
                    foreach (DbParameter parametro in listaParametros)
                    {
                        //Adicionando o parâmetro
                        cmd.Parameters.Add(parametro);
                    }
                }

                DbDados = cmd.ExecuteReader();
            }
            catch { }

            return DbDados;
        }

        #endregion

        #region RetornarDataCommand

        /// <summary>
        /// Método para retornar um DbCommand
        /// </summary>
        /// <param name="strQuery">sql</param>
        /// <returns>DbCommand</returns>
        public DbCommand RetornarDataCommand(string strQuery)
        {
            DbCommand cmd = factory.CreateCommand();

            using (cmd.Connection = AbrirBanco())
            {
                cmd.CommandText = strQuery;
            }

            return cmd;
        }

        #endregion

        #region RetornarValorÚnico

        /// <summary>
        /// Retorna valor do tipo especificado
        /// </summary>
        /// <typeparam name="T">Tipo de retorno</typeparam>
        /// <param name="strQuery">sql</param>
        /// <param name="listaParametros">Lista de parametro</param>
        /// <returns></returns>
        public T RetornaValor<T>(string strQuery, List<DbParameter> listaParametros = null)
        {
            T retorno = default(T);

            if (typeof(T) == typeof(String))
            {
                retorno = (T)Convert.ChangeType(String.Empty, typeof(T));
            }

            using (DbCommand cmd = factory.CreateCommand())
            {
                cmd.CommandText = strQuery;
                cmd.CommandType = CommandType.Text;

                if (listaParametros != null)
                {
                    foreach (DbParameter parametro in listaParametros)
                    {
                        //Adicionando o parâmetro
                        cmd.Parameters.Add(parametro);
                    }
                }

                using (cmd.Connection = AbrirBanco())
                {
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        object valor = null;

                        if (reader == null)
                            return retorno;

                        if (reader.Read())
                        {
                            valor = reader.GetValue(0);
                            if (valor != null && valor.GetType() != typeof(DBNull))
                            {
                                if (valor.GetType() != typeof(T))
                                {
                                    if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(Nullable<>))
                                    {
                                        retorno = (T)Convert.ChangeType(valor, Nullable.GetUnderlyingType(typeof(T)));
                                    }
                                    else
                                    {
                                        retorno = (T)Convert.ChangeType(valor, typeof(T));
                                    }
                                }
                                else
                                {
                                    retorno = (T)valor;
                                }
                            }
                        }
                    }

                    FecharBanco(cmd.Connection);
                }
            }

            return retorno;
        }

        /// <summary>
        /// Retorna valor do tipo especificado
        /// </summary>
        /// <typeparam name="T">Tipo de retorno</typeparam>
        /// <param name="strQuery">sql</param>
        /// <param name="listaParametros">Lista de parametro</param>
        /// <returns></returns>
        public List<T> RetornaValores<T>(string strQuery, List<DbParameter> listaParametros = null)
        {
            List<T> listRetorno = new List<T>();

            using (DbCommand cmd = factory.CreateCommand())
            {
                cmd.CommandText = strQuery;
                cmd.CommandType = CommandType.Text;

                if (listaParametros != null)
                {
                    foreach (DbParameter parametro in listaParametros)
                    {
                        //Adicionando o parâmetro
                        cmd.Parameters.Add(parametro);
                    }
                }

                using (cmd.Connection = AbrirBanco())
                {
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        object valor = null;

                        if (reader == null)
                            return listRetorno;

                        if (reader.Read())
                        {
                            valor = reader.GetValue(0);
                            if (valor != null && valor.GetType() != typeof(DBNull))
                            {
                                if (valor.GetType() != typeof(T))
                                {
                                    if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(Nullable<>))
                                    {
                                        listRetorno.Add((T)Convert.ChangeType(valor, Nullable.GetUnderlyingType(typeof(T))));
                                    }
                                    else
                                    {
                                        listRetorno.Add((T)Convert.ChangeType(valor, typeof(T)));
                                    }
                                }
                                else
                                {
                                    listRetorno.Add((T)valor);
                                }
                            }
                        }
                    }

                    FecharBanco(cmd.Connection);
                }
            }

            return listRetorno;
        }

        /// <summary>
        /// Retorna valor do tipo especificado - Usando Transaction
        /// </summary>
        /// <typeparam name="T">Tipo de retorno</typeparam>
        /// <param name="strQuery">Sql</param>
        /// <param name="listaParametros">Lista de parametro</param>
        /// <param name="Transacao">Transação</param>
        /// <returns></returns>
        public T RetornaValor<T>(string strQuery, List<DbParameter> listaParametros, DbTransaction Transacao)
        {
            if (Transacao == null)
            {
                return RetornaValor<T>(strQuery, listaParametros);
            }

            T retorno = default(T);

            if (typeof(T) == typeof(String))
            {
                retorno = (T)Convert.ChangeType(String.Empty, typeof(T));
            }

            using (DbCommand cmd = factory.CreateCommand())
            {
                cmd.Connection = Transacao.Connection;
                cmd.Transaction = Transacao;
                cmd.CommandText = strQuery;
                cmd.CommandType = CommandType.Text;

                if (listaParametros != null)
                {
                    foreach (DbParameter parametro in listaParametros)
                    {
                        //Adicionando o parâmetro
                        cmd.Parameters.Add(parametro);
                    }
                }

                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    object valor = null;

                    if (reader == null)
                        return retorno;

                    if (reader.Read())
                    {
                        valor = reader.GetValue(0);

                        if (valor != null && valor.GetType() != typeof(DBNull))
                        {
                            if (valor.GetType() != typeof(T))
                            {
                                if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(Nullable<>))
                                {
                                    retorno = (T)Convert.ChangeType(valor, Nullable.GetUnderlyingType(typeof(T)));
                                }
                                else
                                {
                                    retorno = (T)Convert.ChangeType(valor, typeof(T));
                                }
                            }
                            else
                            {
                                retorno = (T)valor;
                            }
                        }
                    }
                }

            }

            return retorno;
        }

        private T ChangeType<T>(object value)
        {
            var t = typeof(T);

            if (t.IsGenericType && t.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                if (value == null)
                {
                    return default(T);
                }

                t = Nullable.GetUnderlyingType(t);
            }

            return (T)Convert.ChangeType(value, t);
        }

        /// <summary>
        /// Método para retornar Inteiro
        /// </summary>
        /// <param name="strQuery">sql</param>
        /// <returns>Int64</returns>
        public Int64 RetornarIdNumerico(string strQuery)
        {
            return RetornaValor<Int64>(strQuery);
        }

        /// <summary>
        /// Método para retorna Inteiro
        /// </summary>
        /// <param name="strQuery">sql</param>
        /// <param name="listaParametros">Lista de parametros</param>
        /// <returns>Int64</returns>
        public Int64 RetornarIdNumerico(string strQuery, List<DbParameter> listaParametros)
        {
            return RetornaValor<Int64>(strQuery, listaParametros);
        }

        /// <summary>
        /// Método para retorna Inteiro
        /// </summary>
        /// <param name="strQuery">sql</param>
        /// <param name="Transacao">Transação a qual pertence o command.</param>
        /// <returns>Int64</returns>
        public Int64 RetornarIdNumerico(string strQuery, List<DbParameter> listaParametros, DbTransaction Transacao)
        {
            return RetornaValor<Int64>(strQuery, listaParametros, Transacao);
        }

        /// <summary>
        /// Método para retorna codigo em formato string
        /// </summary>
        /// <param name="strQuery">sql</param>
        /// <returns>String</returns>
        public string RetornarIdString(string strQuery)
        {
            return RetornaValor<Int64>(strQuery).ToString();
        }

        /// <summary>
        /// Método para retorna codigo em formato string
        /// </summary>
        /// <param name="strQuery">sql</param>
        /// <param name="listaParametros">Lista de parametros</param>
        /// <returns>String</returns>
        public string RetornarString(string strQuery, List<DbParameter> listaParametros)
        {
            return RetornaValor<String>(strQuery, listaParametros);
        }

        /// <summary>
        /// Método para retorna codigo em formato string
        /// </summary>
        /// <param name="strQuery">sql</param>
        /// <param name="listaParametros">Lista de parametros</param>
        /// <param name="Transacao">Transação a qual pertence o command.</param>
        /// <returns>String</returns>
        public string RetornarString(string strQuery, List<DbParameter> listaParametros, DbTransaction Transacao)
        {
            return RetornaValor<String>(strQuery, listaParametros, Transacao);
        }

        /// <summary>
        /// Método para retornar uma Image
        /// </summary>
        /// <param name="strQuery">sql</param>
        /// <returns>Image</returns>
        public Image RetornarImage(string strQuery)
        {
            Image image = null;
            Byte[] bytes;

            byte[] dbImagem = RetornaValor<byte[]>(strQuery);

            if (dbImagem != null)
            {
                bytes = (byte[])dbImagem;
                MemoryStream ms = new MemoryStream(bytes);
                image = Image.FromStream(ms);
            }

            return image;
        }

        /// <summary>
        /// Método para retornar uma Stream
        /// </summary>
        /// <param name="strQuery">sql</param>
        /// <returns>Stream</returns>
        public Stream RetornarStream(string strQuery)
        {
            Stream stream = null;

            Byte[] bytes;
            object streamObject = RetornaValor<object>(strQuery);

            if (streamObject.ToString() != string.Empty)
            {
                bytes = (byte[])streamObject;
                MemoryStream ms = new MemoryStream(bytes);
                stream = ms;
            }

            return stream;
        }

        /// <summary>
        /// Retorna valor Boolean
        /// </summary>
        /// <param name="strQuery">sql</param>
        /// <returns>Boolean</returns>
        public Boolean RetornarBoolean(string strQuery)
        {
            return RetornaValor<Boolean>(strQuery);
        }

        #endregion

        #region MontarObjetos

        public List<T> RetornaObjetos<T>(String sql, List<DbParameter> lstParams = null)
        {
            List<T> ret = (List<T>)Activator.CreateInstance(typeof(List<T>));
            PropertyInfo[] propriedadesObjeto = typeof(T).GetProperties().Where(I => I.CanWrite).ToArray();

            DbCommand cmd = factory.CreateCommand();
            DbDataReader dataReader = null;

            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;

            if (lstParams != null)
            {
                foreach (DbParameter parametro in lstParams)
                {
                    //Adicionando o parâmetro
                    cmd.Parameters.Add(parametro);
                }
            }

            using (cmd.Connection = AbrirBanco())
            using (dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
            {
                while (dataReader.Read())
                {
                    T objAtual = (T)Activator.CreateInstance(typeof(T));

                    foreach (PropertyInfo propriedadeAtual in propriedadesObjeto)
                    {
                        Int32 index = -1;
                        try
                        {
                            index = dataReader.GetOrdinal(propriedadeAtual.Name);
                        }
                        catch (IndexOutOfRangeException) { continue; }

                        if (dataReader.IsDBNull(index) == false)
                        {
                            Type TipoCampo = propriedadeAtual.PropertyType.IsNullable() ? Nullable.GetUnderlyingType(propriedadeAtual.PropertyType) : propriedadeAtual.PropertyType;
                            var valorDB = dataReader[propriedadeAtual.Name];

                            object valor = null;

                            if (TipoCampo.IsEnum)
                            {
                                valor = Enum.Parse(TipoCampo, valorDB.ToString());
                            }
                            else
                            {
                                valor = Convert.ChangeType(valorDB, TipoCampo);
                            }

                            propriedadeAtual.SetValue(objAtual, valor, null);
                        }
                    }

                    ret.Add(objAtual);
                }

                dataReader.Close();
                FecharBanco(cmd.Connection);
            }

            return ret;
        }

        public List<T> RetornaObjetosT<T>(String sql, List<DbParameter> lstParams = null, DbTransaction transacao = null)
        {
            List<T> ret = CreateInstance<List<T>>();
            PropertyInfo[] propriedadesObjeto = typeof(T).GetProperties().Where(I => I.CanWrite).ToArray();

            DbCommand cmd = factory.CreateCommand();
            DbDataReader dataReader = null;

            cmd.CommandText = sql;
            cmd.CommandType = CommandType.Text;
            if (transacao != null)
            {
                cmd.Connection = transacao.Connection;
                cmd.Transaction = transacao;
            }

            if (lstParams != null)
            {
                foreach (DbParameter parametro in lstParams)
                {
                    //Adicionando o parâmetro
                    cmd.Parameters.Add(parametro);
                }
            }

            if (transacao == null)
            {
                cmd.Connection = AbrirBanco();
            }

            using (dataReader = cmd.ExecuteReader(transacao == null ? CommandBehavior.CloseConnection : CommandBehavior.Default))
            {
                Dictionary<Int32, PropertyInfo> dictProp = new Dictionary<Int32, PropertyInfo>();
                bool flag = false;

                while (dataReader.Read())
                {
                    //T objAtual = (T)Activator.CreateInstance(typeof(T));

                    T objAtual = CreateInstance<T>();

                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                        PropertyInfo propriedadeAtual = null;

                        if (flag == true)
                        {
                            if (dictProp.TryGetValue(i, out propriedadeAtual) == false)
                            {
                                continue;
                            }
                        }
                        else
                        {
                            propriedadeAtual = propriedadesObjeto.FirstOrDefault(f => f.Name.ToUpper() == dataReader.GetName(i).ToUpper());
                            if (propriedadeAtual != null)
                            {
                                dictProp.Add(i, propriedadeAtual);
                            }
                            else
                            {
                                continue;
                            }
                        }

                        if (dataReader.IsDBNull(i) == false)
                        {
                            Type TipoCampo = propriedadeAtual.PropertyType.IsNullable() ? Nullable.GetUnderlyingType(propriedadeAtual.PropertyType) : propriedadeAtual.PropertyType;
                            var valorDB = dataReader[i];

                            object valor = null;

                            if (TipoCampo.IsEnum)
                            {
                                valor = Enum.Parse(TipoCampo, valorDB.ToString());
                            }
                            else
                            {
                                if (TipoCampo == typeof(DateTime) && dataReader[i].GetType() == typeof(TimeSpan))
                                {
                                    valor = Convert.ChangeType(valorDB.ToString(), TipoCampo);
                                }
                                else
                                {
                                    valor = Convert.ChangeType(valorDB, TipoCampo);
                                }
                            }

                            propriedadeAtual.SetValue(objAtual, valor, null);
                        }

                    }

                    flag = true;

                    ret.Add(objAtual);
                }

                if (transacao == null)
                {
                    dataReader.Close();
                    FecharBanco(cmd.Connection);
                }
            }

            return ret;
        }

        private T CreateInstance<T>()
        {
            Type[] types = Type.EmptyTypes;

            ConstructorInfo ctor = typeof(T).GetConstructor(types);

            //make a NewExpression that calls the
            //ctor with the args we just created
            NewExpression newExp = Expression.New(ctor);

            //create a lambda with the New
            //Expression as body and our param object[] as arg
            LambdaExpression lambda = Expression.Lambda(typeof(ObjectActivator<T>), newExp);

            //compile it
            ObjectActivator<T> compiled = (ObjectActivator<T>)lambda.Compile();

            //create an instance:
            return compiled();
        }

        public void SalvarObjeto<T>(String sql, T obj, List<DbParameter> _lstParams = null, DbTransaction transacao = null)
        {
            List<DbParameter> lstParams = _lstParams ?? new List<DbParameter>();

            PropertyInfo[] propriedadesObjeto = typeof(T).GetProperties();

            foreach (PropertyInfo propriedadeAtual in propriedadesObjeto)
            {
                Type TipoCampo = propriedadeAtual.PropertyType.IsNullable() ? Nullable.GetUnderlyingType(propriedadeAtual.PropertyType) : propriedadeAtual.PropertyType;

                object valor = propriedadeAtual.GetValue(obj, null);

                if (TipoCampo.IsEnum)
                {
                    lstParams.Add(criaParametros(String.Format("@{0}", propriedadeAtual.Name), DbType.Int64, valor != null ? (object)Convert.ToInt64(valor) : DBNull.Value));
                }
                else
                {
                    lstParams.Add(criaParametros(String.Format("@{0}", propriedadeAtual.Name), TipoCampo.ToDbType(), valor ?? DBNull.Value));
                }
            }

            ExecutarComando(sql, transacao, lstParams);
        }

        public T PreencherObjeto<T>(T obj, DataRow dados)
        {
            PropertyInfo[] propriedadesObjeto = typeof(T).GetProperties().Where(I => I.CanWrite).ToArray();

            IEnumerable<DataColumn> lstColunas = dados.Table.Columns.Cast<DataColumn>();

            foreach (PropertyInfo propriedadeAtual in propriedadesObjeto)
            {
                DataColumn coluna = lstColunas.FirstOrDefault(I => String.Equals(I.ColumnName, propriedadeAtual.Name, StringComparison.CurrentCultureIgnoreCase));

                if (coluna != null)
                {
                    Type TipoCampo = propriedadeAtual.PropertyType.IsNullable() ? Nullable.GetUnderlyingType(propriedadeAtual.PropertyType) : propriedadeAtual.PropertyType;
                    var valorDB = dados[coluna];

                    if (valorDB != DBNull.Value)
                    {
                        object valor = null;

                        if (TipoCampo.IsEnum)
                        {
                            valor = Enum.Parse(TipoCampo, valorDB.ToString());
                        }
                        else
                        {
                            valor = Convert.ChangeType(valorDB, TipoCampo);
                        }

                        propriedadeAtual.SetValue(obj, valor, null);
                    }
                }
            }

            return obj;
        }

        #endregion
    }

    public static class Estado
    {
        public static volatile Boolean AguardandoResposta = false;
        public static Form formUltimo;
    }

    public class ConfiguracaoRede
    {
        public string Hostname { get; set; }
        public int Porta { get; set; }
        public string Usuario { get; set; }
        public string Senha { get; set; }
        public string Database { get; set; }
        public string StringConexao { get; set; }
    }

    public class Parametro
    {
        public string NomeParametro { get; set; }
        public DbType Tipo { get; set; }
        public object Value { get; set; }
        public object Value2 { get; set; }
        /// <summary>
        /// Lista com valores do tipo (BETWEEN) do filtro (IntegerIn).
        /// </summary>
        public List<Tuple<object, object>> ListBetween { get; set; }
    }

    public enum StatusConnection
    {
        ON,
        OFF
    }
}
