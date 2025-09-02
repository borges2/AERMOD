using CamadaAcessoDados;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamadaLogicaNegocios
{
    public class ClsAERMET
    {
        #region Declarações

        ClsDados clsDados = null;

        /// <summary>
        /// Classe de acesso aos dados.
        /// </summary>
        ClsDados controle
        {
            get
            {
                if (clsDados == null)
                {
                    clsDados = new ClsDados(configuracaoRede);
                }

                return clsDados;
            }
        }

        /// <summary>
        /// Configuração Banco de Dados.
        /// </summary>
        ConfiguracaoRede configuracaoRede = null;

        #endregion

        #region Construtor

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="configuracaoRede">Configuração Banco de Dados</param>
        public ClsAERMET(ConfiguracaoRede configuracaoRede)
        {
            this.configuracaoRede = configuracaoRede;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Retornar dados básicos.
        /// </summary>
        /// <returns>Retorna DataTable</returns>
        public DataTable RetornaDados()
        {
            string sql = "SELECT " +
                         "CODIGO, " +
                         "LOCAL, " +
                         "ESTADO, " +
                         "PERIODO_INICIAL, " +
                         "PERIODO_FINAL, " +
                         "X, " +
                         "Y, " +
                         "EM_USO " +
                         "FROM " +
                         "AERMET";

            return controle.RetornarDataTable(sql);
        }

        /// <summary>
        /// Retorna as características de superfície.
        /// </summary>
        /// <param name="codigo">Código</param>
        /// <returns>Retorna DataTable</returns>
        public Tuple<DataTable, DataTable> RetornaDados(int codigo)
        {
            List<DbParameter> parametro = new List<DbParameter>();
            parametro.Add(controle.criaParametros("@CODIGO", DbType.Int32, codigo));

            string sql = "SELECT " +
                         "LOCAL, " +
                         "ESTADO, " +
                         "PERIODO_INICIAL, " +
                         "PERIODO_FINAL, " +
                         "X, " +
                         "Y, " +
                         "EM_USO " +
                         "FROM " +
                         "AERMET " +
                         "WHERE " +
                         "CODIGO = @CODIGO";

            DataTable dtDados = controle.RetornarDataTable(sql, parametro);

            sql = "SELECT " +
                  "SEQUENCIA, " +
                  "FREQUENCIA, " +
                  "ESTACAO, " +
                  "SETOR_INICIAL, " +
                  "SETOR_FINAL, " +
                  "ALBEDO, " +
                  "BOWEN, " +
                  "RUGOSIDADE " +
                  "FROM " +
                  "AERMET_CARACTERISTICAS " +
                  "WHERE " +
                  "CODIGO = @CODIGO";

            DataTable dtCaracteristica = controle.RetornarDataTable(sql, parametro);

            return new Tuple<DataTable, DataTable>(dtDados, dtCaracteristica);
        }

        /// <summary>
        /// Retorna o domínio/grid de modelagem em uso.
        /// </summary>
        /// <returns>Item1 = Dados, Item2 = Características</returns>
        public Tuple<DataTable, DataTable> RetornarRegistroUso()
        {
            string sql = "SELECT " +
                         "CODIGO, " +
                         "LOCAL, " +
                         "ESTADO, " +
                         "PERIODO_INICIAL, " +
                         "PERIODO_FINAL, " +
                         "X, " +
                         "Y, " +
                         "EM_USO " +                         
                         "FROM " +
                         "AERMET " +
                         "WHERE " +
                         "EM_USO = TRUE";

            DataTable dt = controle.RetornarDataTable(sql);
            DataTable dtCaracteristica = new DataTable();
            if (dt.Rows.Count > 0)
            {
                int codigo = Convert.ToInt32(dt.Rows[0]["CODIGO"]);

                sql = "SELECT " +
                      "SEQUENCIA, " +
                      "FREQUENCIA, " +
                      "ESTACAO, " +
                      "SETOR_INICIAL, " +
                      "SETOR_FINAL, " +
                      "ALBEDO, " +
                      "BOWEN, " +
                      "RUGOSIDADE " +
                      "FROM " +
                      "AERMET_CARACTERISTICAS " +
                      "WHERE " +
                     $"CODIGO = {codigo}";

                dtCaracteristica = controle.RetornarDataTable(sql);
            }

            return new Tuple<DataTable, DataTable>(dt, dtCaracteristica);
        }

        /// <summary>
        /// Retorna código em uso.
        /// </summary>
        /// <returns></returns>
        public int RetornarCodigoUso()
        {
            string sql = "SELECT " +
                         "CODIGO " +                         
                         "FROM " +
                         "AERMET " +
                         "WHERE " +
                         "EM_USO = TRUE";

            return controle.RetornaValor<int>(sql);
        }

        /// <summary>
        /// Retorna código do registro caso tenha apenas um registro.
        /// </summary>
        /// <returns></returns>
        public int RetornaCodigoUnico()
        {
            string sql = "SELECT " +
                         "CODIGO " +
                         "FROM " +
                         "AERMET " +
                         "LIMIT 2";

            DataTable dt = controle.RetornarDataTable(sql);

            if (dt.Rows.Count == 1)
            {
                return Convert.ToInt32(dt.Rows[0]["CODIGO"]);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Buscar o menor código.
        /// </summary>
        /// <param name="transacao">Transação utilizada</param>
        /// <returns>Retorna o menor ID da tabela</returns>
        public int BuscarPrimeiroId(DbTransaction transacao)
        {
            string sql = "SELECT " +
                         "CODIGO " +
                         "FROM " +
                         "AERMET " +
                         "LIMIT 1";

            return controle.RetornaValor<int>(sql, null, transacao);
        }

        /// <summary>
        /// Código anterior ao atual.
        /// </summary>
        /// <param name="codigo">Código atual</param>
        /// <returns>Retorna código anterior</returns>
        public int BuscarIdAnterior(int codigo)
        {
            string sql = "SELECT " +
                         "CODIGO " +
                         "FROM " +
                         "AERMET " +
                         "WHERE " +
                        $"CODIGO < {codigo} " +
                         "ORDER BY " +
                         "CODIGO DESC " +
                         "LIMIT 1";

            return controle.RetornaValor<int>(sql);
        }

        /// <summary>
        /// Buscar o menor código.
        /// </summary>
        /// <param name="codigo">Código dos dados básicos</param>
        /// <returns>Retorna o menor ID da tabela</returns>
        public int BuscarPrimeiroIdCaracteristicas(int codigo, DbTransaction transaction)
        {
            List<DbParameter> parametro = new List<DbParameter>();
            parametro.Add(controle.criaParametros("@CODIGO", DbType.Int32, codigo));

            string sql = "SELECT " +
                         "SEQUENCIA " +

                         "FROM " +
                         "AERMET_CARACTERISTICAS " +

                         "WHERE " +
                         "CODIGO = @CODIGO " +

                         "LIMIT 1";

            return controle.RetornaValor<int>(sql, parametro, transaction);
        }

        /// <summary>
        /// Incrementar chave primária.
        /// </summary>
        /// <param name="transacao">Transação utilizada</param>
        /// <returns>Retorna ID da tabela</returns>
        public int Incrementar(DbTransaction transacao)
        {
            string sql = "SELECT " +
                         "A.CODIGO + 1 AS DISPONIVEL " +
                         "FROM " +
                         "AERMET A " +

                         "LEFT JOIN AERMET B ON " +
                         "B.CODIGO = A.CODIGO + 1 " +

                         "WHERE " +
                         "B.CODIGO IS NULL AND " +
                         "A.CODIGO < 99999 " +
                         "ORDER BY " +
                         "A.CODIGO DESC " +
                         "LIMIT 1";

            int IdBuscado = controle.RetornaValor<int>(sql, null, transacao);

            if (IdBuscado == 0)
            {
                IdBuscado = BuscarPrimeiroId(transacao);

                if (IdBuscado != 1)
                {
                    IdBuscado = 1;
                }
                else
                {
                    IdBuscado = 0;
                }
            }

            return IdBuscado;
        }

        /// <summary>
        /// Incrementar chave primária.
        /// </summary>
        /// <param name="codigo">Código dos dados básicos</param>
        /// <param name="transacao">Transação utilizada</param>
        /// <returns>Retorna ID da tabela</returns>
        public int IncrementarCaracteristicas(int codigo, DbTransaction transacao)
        {
            List<DbParameter> parametro = new List<DbParameter>();
            parametro.Add(controle.criaParametros("@CODIGO", DbType.Int32, codigo));

            string sql = "SELECT " +
                         "A.SEQUENCIA + 1 AS DISPONIVEL " +
                         "FROM " +
                         "AERMET_CARACTERISTICAS A " +

                         "LEFT JOIN AERMET_CARACTERISTICAS B ON " +
                         "B.CODIGO = A.CODIGO AND " +
                         "B.SEQUENCIA = A.SEQUENCIA + 1 " +

                         "WHERE " +
                         "A.CODIGO = @CODIGO AND " +
                         "B.SEQUENCIA IS NULL AND " +
                         "A.SEQUENCIA < 99999 " +

                         "ORDER BY " +
                         "A.SEQUENCIA DESC " +

                         "LIMIT 1";

            int IdBuscado = controle.RetornaValor<int>(sql, parametro, transacao);

            if (IdBuscado == 0)
            {
                IdBuscado = BuscarPrimeiroIdCaracteristicas(codigo, transacao);

                if (IdBuscado != 1)
                {
                    IdBuscado = 1;
                }
                else
                {
                    IdBuscado = 0;
                }
            }

            return IdBuscado;
        }

        /// <summary>
        /// Inserir/Atualizar registros.
        /// </summary>
        /// <param name="dadosBasicos">Objeto dados básicos</param>
        /// <param name="lstCaracteristicas">Lista de objetos com características</param>
        /// <returns></returns>
        public int Atualizar(dynamic dadosBasicos, List<dynamic> lstCaracteristicas)
        {
            int codigo = 0;
            DbTransaction transacao = controle.AbrirTransaction();

            try
            {
                string sql = string.Empty;
                codigo = dadosBasicos.CODIGO == 0 ? Incrementar(transacao) : dadosBasicos.CODIGO;

                List<DbParameter> parametro = new List<DbParameter>();
                parametro.Add(controle.criaParametros("@CODIGO", DbType.Int32, codigo));
                parametro.Add(controle.criaParametros("@LOCAL", DbType.String, dadosBasicos.LOCAL));
                parametro.Add(controle.criaParametros("@ESTADO", DbType.Int32, dadosBasicos.ESTADO));
                parametro.Add(controle.criaParametros("@PERIODO_INICIAL", DbType.Date, dadosBasicos.PERIODO_INICIAL));
                parametro.Add(controle.criaParametros("@PERIODO_FINAL", DbType.Date, dadosBasicos.PERIODO_FINAL));
                parametro.Add(controle.criaParametros("@X", DbType.Decimal, dadosBasicos.X));
                parametro.Add(controle.criaParametros("@Y", DbType.Decimal, dadosBasicos.Y));

                if (dadosBasicos.CODIGO == 0)
                {
                    sql = "REPLACE INTO " +
                          "AERMET " +
                          "(" +
                          "CODIGO, " +
                          "LOCAL, " +
                          "ESTADO, " +
                          "PERIODO_INICIAL, " +
                          "PERIODO_FINAL, " +
                          "X, " +
                          "Y" +
                          ") " +
                          "VALUES " +
                          "(" +
                          "@CODIGO, " +
                          "@LOCAL, " +
                          "@ESTADO, " +
                          "@PERIODO_INICIAL, " +
                          "@PERIODO_FINAL, " +
                          "@X, " +
                          "@Y" +
                          ")";

                    controle.ExecutaParametros(sql, transacao, parametro);
                }
                else
                {
                    sql = "UPDATE " +
                          "AERMET " +
                          "SET " +
                          "LOCAL = @LOCAL, " +
                          "ESTADO = @ESTADO, " +
                          "PERIODO_INICIAL = @PERIODO_INICIAL, " +
                          "PERIODO_FINAL = @PERIODO_FINAL, " +
                          "X = @X, " +
                          "Y = @Y " +
                          "WHERE " +
                          "CODIGO = @CODIGO";

                    controle.ExecutaParametros(sql, transacao, parametro);
                }

                sql = "DELETE " +
                      "FROM " +
                      "AERMET_CARACTERISTICAS " +
                      "WHERE " +
                      "CODIGO = @CODIGO";

                controle.ExecutaParametros(sql, transacao, parametro);

                foreach (var item in lstCaracteristicas)
                {
                    int sequencia = IncrementarCaracteristicas(codigo, transacao);

                    List<DbParameter> parametroCaracteristica = new List<DbParameter>();
                    parametroCaracteristica.Add(controle.criaParametros("@CODIGO", DbType.Int32, codigo));
                    parametroCaracteristica.Add(controle.criaParametros("@SEQUENCIA", DbType.Int32, sequencia));
                    parametroCaracteristica.Add(controle.criaParametros("@FREQUENCIA", DbType.Int32, item.FREQUENCIA));
                    parametroCaracteristica.Add(controle.criaParametros("@ESTACAO", DbType.Int32, item.ESTACAO));
                    parametroCaracteristica.Add(controle.criaParametros("@SETOR_INICIAL", DbType.Int32, item.SETOR_INICIAL));
                    parametroCaracteristica.Add(controle.criaParametros("@SETOR_FINAL", DbType.Int32, item.SETOR_FINAL));
                    parametroCaracteristica.Add(controle.criaParametros("@ALBEDO", DbType.Decimal, item.ALBEDO));
                    parametroCaracteristica.Add(controle.criaParametros("@BOWEN", DbType.Decimal, item.BOWEN));
                    parametroCaracteristica.Add(controle.criaParametros("@RUGOSIDADE", DbType.Decimal, item.RUGOSIDADE));

                    sql = "REPLACE INTO " +
                          "AERMET_CARACTERISTICAS " +
                          "(" +
                          "CODIGO, " +
                          "SEQUENCIA, " +
                          "FREQUENCIA, " +
                          "ESTACAO, " +
                          "SETOR_INICIAL, " +
                          "SETOR_FINAL, " +
                          "ALBEDO, " +
                          "BOWEN, " +
                          "RUGOSIDADE" +
                          ") " +
                          "VALUES " +
                          "(" +
                          "@CODIGO, " +
                          "@SEQUENCIA, " +
                          "@FREQUENCIA, " +
                          "@ESTACAO, " +
                          "@SETOR_INICIAL, " +
                          "@SETOR_FINAL, " +
                          "@ALBEDO, " +
                          "@BOWEN, " +
                          "@RUGOSIDADE" +
                          ")";

                    controle.ExecutaParametros(sql, transacao, parametroCaracteristica);
                }

                controle.ExecutarTransaction(true, transacao);               
            }
            catch
            {
                codigo = 0;
                controle.ExecutarTransaction(false, transacao);               
            }

            return codigo;
        }

        /// <summary>
        /// Atualizar utilização do arquivo.
        /// </summary>
        /// <param name="codigo">Código do arquivo</param>        
        public void AtualizarUso(int codigo)
        {
            List<DbParameter> parametro = new List<DbParameter>();
            parametro.Add(controle.criaParametros("@CODIGO", DbType.Int32, codigo));

            string sql = "UPDATE " +
                         "AERMET " +
                         "SET " +
                         "EM_USO = FALSE";

            controle.ExecutarComando(sql, parametro);

            sql = "UPDATE " +
                  "AERMET " +
                  "SET " +
                  "EM_USO = TRUE " +
                  "WHERE " +
                  "CODIGO = @CODIGO";

            controle.ExecutarComando(sql, parametro);
        }

        /// <summary>
        /// Excluir registros.
        /// </summary>
        /// <param name="codigo">Código do registro</param>
        public void Excluir(int codigo)
        {
            List<DbParameter> parametro = new List<DbParameter>();
            parametro.Add(controle.criaParametros("@CODIGO", DbType.Int32, codigo));

            string sql = "DELETE " +
                         "FROM " +
                         "AERMET " +
                         "WHERE " +
                         "CODIGO = @CODIGO";

            controle.ExecutaParametros(sql, parametro);
        }

        /// <summary>
        /// Verificar duplicidade local/período.
        /// </summary>
        /// <param name="codigo">Código</param>
        /// <param name="local">Local</param>
        /// <param name="periodoInicial">Período inicial</param>
        /// <param name="periodoFinal">Período final</param>
        /// <returns>Retorna código existente</returns>
        public int VerificarDuplicidade(int codigo, string local, DateTime periodoInicial, DateTime periodoFinal, decimal X, decimal Y)
        {
            string sql = "SELECT " +
                         "MIN(CODIGO) " +
                         "FROM " +
                         "AERMET " +
                         "WHERE " +
                         "CODIGO <> @CODIGO AND " +
                         "(" +
                         "REPLACE(LOCAL, ' ', '') LIKE @LOCAL AND " +
                         "(" +
                         "PERIODO_INICIAL = @PERIODO_INICIAL AND " +
                         "PERIODO_FINAL = @PERIODO_FINAL" +
                         ") AND " +
                         "(" +
                         "X = @X AND " +
                         "Y = @Y" +
                         ")" +
                         ")";

            List<DbParameter> parametro = new List<DbParameter>();
            parametro.Add(controle.criaParametros("@CODIGO", DbType.Int32, codigo));
            parametro.Add(controle.criaParametros("@LOCAL", DbType.String, local.Replace(" ", "")));
            parametro.Add(controle.criaParametros("@PERIODO_INICIAL", DbType.Date, periodoInicial));
            parametro.Add(controle.criaParametros("@PERIODO_FINAL", DbType.Date, periodoFinal));
            parametro.Add(controle.criaParametros("@X", DbType.Decimal, X));
            parametro.Add(controle.criaParametros("@Y", DbType.Decimal, Y));

            return controle.RetornaValor<int>(sql, parametro);
        }

        /// <summary>
        /// Salvar arquivo.
        /// </summary>        
        /// <param name="codigo">Código</param>
        /// <param name="arquivoINP1">Arquivo (.INP1)</param>
        /// <param name="arquivoINP2">Arquivo (.INP2)</param>
        /// <param name="arquivoINP3">Arquivo (.INP3)</param>
        /// <param name="arquivoPFL">Arquivo (.PFL)</param>
        /// <param name="arquivoSFC">Arquivo (.SFC)</param>
        /// <returns>Retorna false caso exista erro</returns>
        public bool SalvarArquivo(int codigo, byte[] arquivoINP1, byte[] arquivoINP2, byte[] arquivoPFL, byte[] arquivoSFC)
        {
            string sql = "UPDATE " +
                         "AERMET " +
                         "SET " +
                         "ARQUIVO_INP1 = @ARQUIVO_INP1, " +
                         "ARQUIVO_INP2 = @ARQUIVO_INP2, " +                        
                         "ARQUIVO_PFL = @ARQUIVO_PFL, " +
                         "ARQUIVO_SFC = @ARQUIVO_SFC " +
                         "WHERE " +
                         "CODIGO = @CODIGO";

            List<DbParameter> parametro = new List<DbParameter>();
            parametro.Add(controle.criaParametros("@CODIGO", DbType.Int32, codigo));
            parametro.Add(controle.criaParametros("@ARQUIVO_INP1", DbType.Binary, arquivoINP1));
            parametro.Add(controle.criaParametros("@ARQUIVO_INP2", DbType.Binary, arquivoINP2));           
            parametro.Add(controle.criaParametros("@ARQUIVO_PFL", DbType.Binary, arquivoPFL));
            parametro.Add(controle.criaParametros("@ARQUIVO_SFC", DbType.Binary, arquivoSFC));

            try
            {
                controle.ExecutaParametros(sql, parametro);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Retorna arquivo (.INP1).
        /// </summary>  
        /// <param name="codigo">Código</param>
        /// <returns>Retorna byte[]</returns>
        public byte[] RetornarArquivoINP1(int codigo)
        {
            string sql = "SELECT " +
                         "ARQUIVO_INP1 " +
                         "FROM " +
                         "AERMET " +
                         "WHERE " +
                        $"CODIGO = {codigo}";

            return controle.RetornaValor<byte[]>(sql);
        }

        /// <summary>
        /// Retorna arquivo (.INP2).
        /// </summary>  
        /// <param name="codigo">Código</param>
        /// <returns>Retorna byte[]</returns>
        public byte[] RetornarArquivoINP2(int codigo)
        {
            string sql = "SELECT " +
                         "ARQUIVO_INP2 " +
                         "FROM " +
                         "AERMET " +
                         "WHERE " +
                        $"CODIGO = {codigo}";

            return controle.RetornaValor<byte[]>(sql);
        }

        /// <summary>
        /// Retorna arquivo (.PFL).
        /// </summary> 
        /// <param name="codigo">Código</param>
        /// <returns>Retorna byte[]</returns>
        public byte[] RetornarArquivoPFL(int codigo)
        {
            string sql = "SELECT " +
                         "ARQUIVO_PFL " +
                         "FROM " +
                         "AERMET " +
                         "WHERE " +
                        $"CODIGO = {codigo}";

            return controle.RetornaValor<byte[]>(sql);
        }

        /// <summary>
        /// Retorna arquivo (.SFC).
        /// </summary> 
        /// <param name="codigo">Código</param>
        /// <returns>Retorna byte[]</returns>
        public byte[] RetornarArquivoSFC(int codigo)
        {
            string sql = "SELECT " +
                         "ARQUIVO_SFC " +
                         "FROM " +
                         "AERMET " +
                         "WHERE " +
                        $"CODIGO = {codigo}";

            return controle.RetornaValor<byte[]>(sql);
        }

        #endregion
    }
}
