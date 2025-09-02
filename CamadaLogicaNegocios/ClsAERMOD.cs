using CamadaAcessoDados;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Reflection.Emit;

namespace CamadaLogicaNegocios
{
    public class ClsAERMOD
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
        public ClsAERMOD(ConfiguracaoRede configuracaoRede)
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
                         "MEDIA_HORARIA, " +
                         "MEDIA_PERIODO, " +
                         "POLUENTE, " +
                         "UNIDADE_MEDICAO, " +
                         "VALOR_MAXIMO, " +
                         "EM_USO " +
                         "FROM " +
                         "AERMOD";

            return controle.RetornarDataTable(sql);
        }

        /// <summary>
        /// Retorna dados AERMOD.
        /// </summary>
        /// <param name="codigo">Código</param>
        /// <returns>Retorna DataTable</returns>
        public Tuple<DataTable, DataTable> RetornaDados(int codigo)
        {
            List<DbParameter> parametro = new List<DbParameter>();
            parametro.Add(controle.criaParametros("@CODIGO", DbType.Int32, codigo));

            string sql = "SELECT " +
                         "MEDIA_HORARIA, " +
                         "MEDIA_PERIODO, " +
                         "POLUENTE, " +
                         "UNIDADE_MEDICAO, " +
                         "VALOR_MAXIMO, " +
                         "EM_USO " +
                         "FROM " +
                         "AERMOD " +
                         "WHERE " +
                         "CODIGO = @CODIGO";

            DataTable dtDados = controle.RetornarDataTable(sql, parametro);

            sql = "SELECT " +
                  "SEQUENCIA, " +
                  "TIPO_SAIDA, " +
                  "MEDIA_HORARIA, " +
                  "MEDIA_PERIODO, " +
                  "PADRAO_QUALIDADE_AR, " +
                  "CRITERIO_RECEPTOR, " +
                  "VALOR_MAXIMO, " +
                  "DESCRICAO " +
                  "FROM " +
                  "AERMOD_SAIDAS " +
                  "WHERE " +
                  "CODIGO = @CODIGO";

            DataTable dtSaidas = controle.RetornarDataTable(sql, parametro);

            return new Tuple<DataTable, DataTable>(dtDados, dtSaidas);
        }

        /// <summary>
        /// Retorna o domínio/grid de modelagem em uso.
        /// </summary>
        /// <returns>Item1 = Dados, Item2 = Características</returns>
        public Tuple<DataTable, DataTable> RetornarRegistroUso()
        {
            string sql = "SELECT " +
                         "CODIGO, " +
                         "MEDIA_HORARIA, " +
                         "MEDIA_PERIODO, " +
                         "POLUENTE, " +
                         "UNIDADE_MEDICAO, " +
                         "VALOR_MAXIMO, " +
                         "EM_USO " +
                         "FROM " +
                         "AERMOD " +
                         "WHERE " +
                         "EM_USO = TRUE";

            DataTable dt = controle.RetornarDataTable(sql);
            DataTable dtSaidas = new DataTable();
            if (dt.Rows.Count > 0)
            {
                int codigo = Convert.ToInt32(dt.Rows[0]["CODIGO"]);

                sql = "SELECT " +
                      "SEQUENCIA, " +
                      "TIPO_SAIDA, " +
                      "MEDIA_HORARIA, " +
                      "MEDIA_PERIODO, " +
                      "PADRAO_QUALIDADE_AR, " +
                      "CRITERIO_RECEPTOR, " +
                      "VALOR_MAXIMO, " +
                      "DESCRICAO " +
                      "FROM " +
                      "AERMOD_SAIDAS " +
                      "WHERE " +
                     $"CODIGO = {codigo}";

                dtSaidas = controle.RetornarDataTable(sql);
            }

            return new Tuple<DataTable, DataTable>(dt, dtSaidas);
        }

        /// <summary>
        /// Retorna os registros de saída em uso.
        /// </summary>
        /// <returns>Retorna DataTable</returns>
        public DataTable RetornarSaidasUso()
        {
            int codigo = RetornarCodigoUso();

            string sql = "SELECT " +
                         "CODIGO, " +
                         "SEQUENCIA, " +
                         "DESCRICAO " +
                         "FROM " +
                         "AERMOD_SAIDAS " +
                         "WHERE " +
                        $"CODIGO = {codigo} AND " +
                         "ARQUIVO IS NOT NULL";

            return controle.RetornarDataTable(sql);
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
                         "AERMOD " +
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
                         "AERMOD " +
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
        /// Retorna unidade de medição em uso.
        /// </summary>
        /// <param name="codigo">Código</param>
        /// <returns>Retorna unidade de medição</returns>
        public string RetornarUnidadeMedicao(int codigo)
        {
            string sql = "SELECT " +
                         "UNIDADE_MEDICAO " +
                         "FROM " +
                         "AERMOD " +
                         "WHERE " +
                        $"CODIGO = {codigo}";

            return controle.RetornaValor<string>(sql);
        }

        /// <summary>
        /// Retorna unidade de medição em uso.
        /// </summary>
        /// <param name="codigo">Código</param>
        /// <returns>Retorna unidade de medição</returns>
        public int RetornarPoluente(int codigo)
        {
            string sql = "SELECT " +
                         "POLUENTE " +
                         "FROM " +
                         "AERMOD " +
                         "WHERE " +
                        $"CODIGO = {codigo}";

            return controle.RetornaValor<int>(sql);
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
                         "AERMOD " +
                         "LIMIT 1";

            return controle.RetornaValor<int>(sql, null, transacao);
        }

        /// <summary>
        /// Buscar o menor código.
        /// </summary>
        /// <param name="codigo">Código dos dados básicos</param>
        /// <returns>Retorna o menor ID da tabela</returns>
        public int BuscarPrimeiroIdSaidas(int codigo, DbTransaction transaction)
        {
            List<DbParameter> parametro = new List<DbParameter>();
            parametro.Add(controle.criaParametros("@CODIGO", DbType.Int32, codigo));

            string sql = "SELECT " +
                         "SEQUENCIA " +

                         "FROM " +
                         "AERMOD_SAIDAS " +

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
                         "AERMOD A " +

                         "LEFT JOIN AERMOD B ON " +
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
        public int IncrementarSaidas(int codigo, DbTransaction transacao)
        {
            List<DbParameter> parametro = new List<DbParameter>();
            parametro.Add(controle.criaParametros("@CODIGO", DbType.Int32, codigo));

            string sql = "SELECT " +
                         "A.SEQUENCIA + 1 AS DISPONIVEL " +
                         "FROM " +
                         "AERMOD_SAIDAS A " +

                         "LEFT JOIN AERMOD_SAIDAS B ON " +
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
                IdBuscado = BuscarPrimeiroIdSaidas(codigo, transacao);

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
        /// <param name="lstSaidas">Lista de objetos com dados de saída</param>
        /// <param name="lstRetangulo">Lista dos valores do retângulo</param>
        /// <returns></returns>
        public int Atualizar(dynamic dadosBasicos, List<dynamic> lstSaidas, List<int> lstRetangulo)
        {
            int codigo = 0;
            DbTransaction transacao = controle.AbrirTransaction();

            try
            {
                string sql = string.Empty;
                codigo = dadosBasicos.CODIGO == 0 ? Incrementar(transacao) : dadosBasicos.CODIGO;

                List<DbParameter> parametro = new List<DbParameter>();
                parametro.Add(controle.criaParametros("@CODIGO", DbType.Int32, codigo));
                parametro.Add(controle.criaParametros("@MEDIA_HORARIA", DbType.Int32, dadosBasicos.MEDIA_HORARIA));
                parametro.Add(controle.criaParametros("@MEDIA_PERIODO", DbType.Int32, dadosBasicos.MEDIA_PERIODO));
                parametro.Add(controle.criaParametros("@POLUENTE", DbType.Int32, dadosBasicos.POLUENTE));
                parametro.Add(controle.criaParametros("@UNIDADE_MEDICAO", DbType.String, dadosBasicos.UNIDADE_MEDICAO));
                parametro.Add(controle.criaParametros("@VALOR_MAXIMO", DbType.Int32, dadosBasicos.VALOR_MAXIMO));

                if (dadosBasicos.CODIGO == 0)
                {
                    sql = "REPLACE INTO " +
                          "AERMOD " +
                          "(" +
                          "CODIGO, " +
                          "MEDIA_HORARIA, " +
                          "MEDIA_PERIODO, " +
                          "POLUENTE, " +
                          "UNIDADE_MEDICAO, " +
                          "VALOR_MAXIMO" +
                          ") " +
                          "VALUES " +
                          "(" +
                          "@CODIGO, " +
                          "@MEDIA_HORARIA, " +
                          "@MEDIA_PERIODO, " +
                          "@POLUENTE, " +
                          "@UNIDADE_MEDICAO, " +
                          "@VALOR_MAXIMO" +
                          ")";

                    controle.ExecutaParametros(sql, transacao, parametro);
                }
                else
                {
                    sql = "UPDATE " +
                          "AERMOD " +
                          "SET " +
                          "MEDIA_HORARIA = @MEDIA_HORARIA, " +
                          "MEDIA_PERIODO = @MEDIA_PERIODO, " +
                          "POLUENTE = @POLUENTE, " +
                          "UNIDADE_MEDICAO = @UNIDADE_MEDICAO, " +
                          "VALOR_MAXIMO = @VALOR_MAXIMO " +
                          "WHERE " +
                          "CODIGO = @CODIGO";

                    controle.ExecutaParametros(sql, transacao, parametro);
                }

                sql = "DELETE " +
                      "FROM " +
                      "AERMOD_SAIDAS " +
                      "WHERE " +
                      "CODIGO = @CODIGO";

                controle.ExecutaParametros(sql, transacao, parametro);

                foreach (var item in lstSaidas)
                {
                    int sequencia = IncrementarSaidas(codigo, transacao);

                    List<DbParameter> parametroCaracteristica = new List<DbParameter>();
                    parametroCaracteristica.Add(controle.criaParametros("@CODIGO", DbType.Int32, codigo));
                    parametroCaracteristica.Add(controle.criaParametros("@SEQUENCIA", DbType.Int32, sequencia));
                    parametroCaracteristica.Add(controle.criaParametros("@TIPO_SAIDA", DbType.Int32, item.TIPO_SAIDA));
                    parametroCaracteristica.Add(controle.criaParametros("@MEDIA_HORARIA", DbType.Int32, item.MEDIA_HORARIA));
                    parametroCaracteristica.Add(controle.criaParametros("@MEDIA_PERIODO", DbType.Int32, item.MEDIA_PERIODO));
                    parametroCaracteristica.Add(controle.criaParametros("@PADRAO_QUALIDADE_AR", DbType.Decimal, item.PADRAO_QUALIDADE_AR));
                    parametroCaracteristica.Add(controle.criaParametros("@CRITERIO_RECEPTOR", DbType.Int32, item.CRITERIO_RECEPTOR));
                    parametroCaracteristica.Add(controle.criaParametros("@VALOR_MAXIMO", DbType.Int32, item.VALOR_MAXIMO));
                    parametroCaracteristica.Add(controle.criaParametros("@DESCRICAO", DbType.String, item.DESCRICAO));

                    sql = "REPLACE INTO " +
                          "AERMOD_SAIDAS " +
                          "(" +
                          "CODIGO, " +
                          "SEQUENCIA, " +
                          "TIPO_SAIDA, " +
                          "MEDIA_HORARIA, " +
                          "MEDIA_PERIODO, " +
                          "PADRAO_QUALIDADE_AR, " +
                          "CRITERIO_RECEPTOR, " +
                          "VALOR_MAXIMO, " +
                          "DESCRICAO" +
                          ") " +
                          "VALUES " +
                          "(" +
                          "@CODIGO, " +
                          "@SEQUENCIA, " +
                          "@TIPO_SAIDA, " +
                          "@MEDIA_HORARIA, " +
                          "@MEDIA_PERIODO, " +
                          "@PADRAO_QUALIDADE_AR, " +
                          "@CRITERIO_RECEPTOR, " +
                          "@VALOR_MAXIMO, " +
                          "@DESCRICAO" +
                          ")";

                    controle.ExecutaParametros(sql, transacao, parametroCaracteristica);
                }

                ExcluirRetangulo(codigo, transacao);
                AtualizarRetangulo(codigo, lstRetangulo, transacao);

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
                         "AERMOD " +
                         "SET " +
                         "EM_USO = FALSE";

            controle.ExecutarComando(sql, parametro);

            sql = "UPDATE " +
                  "AERMOD " +
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
                         "AERMOD " +
                         "WHERE " +
                         "CODIGO = @CODIGO";

            controle.ExecutaParametros(sql, parametro);
        }

        /// <summary>
        /// Verificar duplicidade local/período.
        /// </summary>
        /// <param name="codigo">Código</param>
        /// <param name="poluente">Poluente</param>
        /// <returns>Retorna código existente</returns>
        public int VerificarDuplicidade(int codigo, int poluente)
        {
            string sql = "SELECT " +
                         "MIN(CODIGO) " +

                         "FROM " +
                         "AERMOD " +

                         "WHERE " +
                         "CODIGO <> @CODIGO AND " +
                         "POLUENTE = @POLUENTE";                                                  

            List<DbParameter> parametro = new List<DbParameter>();
            parametro.Add(controle.criaParametros("@CODIGO", DbType.Int32, codigo));
            parametro.Add(controle.criaParametros("@POLUENTE", DbType.Int32, poluente));           

            return controle.RetornaValor<int>(sql, parametro);
        }

        /// <summary>
        /// Salvar arquivo.
        /// </summary>        
        /// <param name="codigo">Código do arquivo</param>
        /// <param name="AERMOD_INP">Arquivo (.INP)</param>
        /// <param name="ERRORS_OUT">ERRORS (.OUT)</param>
        /// <param name="AERMOD_OUT">AERMOD (.OUT)</param>
        /// <param name="AERMOD_PLT">Arquivo (.PLT)</param>        
        /// <returns>Retorna false caso exista erro</returns>
        public bool SalvarArquivo(int codigo, byte[] AERMOD_INP, byte[] ERRORS_OUT, byte[] AERMOD_OUT, List<Tuple<int, byte[]>> AERMOD_PLT)
        {
            string sql = "UPDATE " +
                         "AERMOD " +
                         "SET " +
                         "AERMOD_INP = @AERMOD_INP, " +
                         "ERRORS_OUT = @ERRORS_OUT, " +
                         "AERMOD_OUT = @AERMOD_OUT " +
                         "WHERE " +
                         "CODIGO = @CODIGO";

            List<DbParameter> parametro = new List<DbParameter>();
            parametro.Add(controle.criaParametros("@CODIGO", DbType.Int32, codigo));
            parametro.Add(controle.criaParametros("@AERMOD_INP", DbType.Binary, AERMOD_INP));
            parametro.Add(controle.criaParametros("@ERRORS_OUT", DbType.Binary, ERRORS_OUT));
            parametro.Add(controle.criaParametros("@AERMOD_OUT", DbType.Binary, AERMOD_OUT));

            DbTransaction transacao = controle.AbrirTransaction();

            try
            {
                controle.ExecutaParametros(sql, parametro);

                sql = "UPDATE " +
                      "AERMOD_SAIDAS " +
                      "SET " +                      
                      "ARQUIVO = @ARQUIVO " +
                      "WHERE " +
                      "CODIGO = @CODIGO AND " +
                      "SEQUENCIA = @SEQUENCIA";

                foreach (var item in AERMOD_PLT)
                {
                    parametro = new List<DbParameter>();
                    parametro.Add(controle.criaParametros("@CODIGO", DbType.Int32, codigo));
                    parametro.Add(controle.criaParametros("@SEQUENCIA", DbType.Int32, item.Item1));                    
                    parametro.Add(controle.criaParametros("@ARQUIVO", DbType.Binary, item.Item2));

                    controle.ExecutaParametros(sql, parametro);
                }

                controle.ExecutarTransaction(true, transacao);
            }
            catch
            {
                controle.ExecutarTransaction(false, transacao);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Retorna AERMOD.INP.
        /// </summary>       
        /// <param name="codigo">Código</param>
        /// <returns>Retorna byte[]</returns>
        public byte[] RetornarAERMOD_INP(int codigo)
        {
            string sql = "SELECT " +
                         "AERMOD_INP " +
                         "FROM " +
                         "AERMOD " +
                         "WHERE " +
                        $"CODIGO = {codigo}";

            return controle.RetornaValor<byte[]>(sql);
        }

        /// <summary>
        /// Retorna ERRORS.OUT.
        /// </summary>    
        /// <param name="codigo">Código</param>
        /// <returns>Retorna byte[]</returns>
        public byte[] RetornarERRORS_OUT(int codigo)
        {
            string sql = "SELECT " +
                         "ERRORS_OUT " +
                         "FROM " +
                         "AERMOD " +
                         "WHERE " +
                        $"CODIGO = {codigo}";

            return controle.RetornaValor<byte[]>(sql);
        }

        /// <summary>
        /// Retorna AERMOD.OUT.
        /// </summary>   
        /// <param name="codigo">Código</param>
        /// <returns>Retorna byte[]</returns>
        public byte[] RetornarAERMOD_OUT(int codigo)
        {
            string sql = "SELECT " +
                         "AERMOD_OUT " +
                         "FROM " +
                         "AERMOD " +
                         "WHERE " +
                        $"CODIGO = {codigo}";

            return controle.RetornaValor<byte[]>(sql);
        }

        /// <summary>
        /// Retorna arquivo (.PLT).
        /// </summary>    
        /// <param name="codigo">Código</param>
        /// <param name="sequencia">Sequência</param>
        /// <returns>Retorna byte[]</returns>
        public byte[] RetornarArquivoPLT(int codigo, int sequencia)
        {
            string sql = "SELECT " +
                         "ARQUIVO " +
                         "FROM " +
                         "AERMOD_SAIDAS " +
                         "WHERE " +
                        $"CODIGO = {codigo} AND " +
                        $"SEQUENCIA = {sequencia}";

            return controle.RetornaValor<byte[]>(sql);
        }

        /// <summary>
        /// Retornar descrição do arquivo (.PLT).
        /// </summary>
        /// <param name="codigo">Código do arquivo</param>
        /// <param name="sequencia">Sequência do arquivo</param>
        /// <returns>Retorna descrição do arquivo</returns>
        public string RetornarDescricaoArquivoPLT(int codigo, int sequencia)
        {
            string sql = "SELECT " +
                         "DESCRICAO " +
                         "FROM " +
                         "AERMOD_SAIDAS " +
                         "WHERE " +
                        $"CODIGO = {codigo} AND " +
                        $"SEQUENCIA = {sequencia}";

            return controle.RetornaValor<string>(sql);
        }

        /// <summary>
        /// Excluir arquivo (.PLT).
        /// </summary>    
        /// <param name="codigo">Código</param>
        /// <param name="sequencia">Sequência</param>
        /// <returns>Retorna byte[]</returns>
        public void ExcluirArquivoPLT(int codigo, int sequencia)
        {
            string sql = "UPDATE " +
                         "AERMOD_SAIDAS " +
                         "SET " +
                         "ARQUIVO = @ARQUIVO " +                         
                         "WHERE " +
                        $"CODIGO = @CODIGO AND " +
                        $"SEQUENCIA = @SEQUENCIA";

            List<DbParameter> parametro = new List<DbParameter>();
            parametro.Add(controle.criaParametros("@CODIGO", DbType.Int32, codigo));
            parametro.Add(controle.criaParametros("@SEQUENCIA", DbType.Int32, sequencia));
            parametro.Add(controle.criaParametros("@ARQUIVO", DbType.Binary, null));

            controle.ExecutaParametros(sql, parametro);
        }

        #region Retângulo

        /// <summary>
        /// Buscar o menor código.
        /// </summary>
        /// <param name="codigo">Código dos dados básicos</param>
        /// <returns>Retorna o menor ID da tabela</returns>
        public int BuscarPrimeiroIdRetangulo(int codigo, DbTransaction transaction)
        {
            List<DbParameter> parametro = new List<DbParameter>();
            parametro.Add(controle.criaParametros("@CODIGO", DbType.Int32, codigo));

            string sql = "SELECT " +
                         "SEQUENCIA " +

                         "FROM " +
                         "AERMOD_RETANGULO " +

                         "WHERE " +
                         "CODIGO = @CODIGO " +

                         "LIMIT 1";

            return controle.RetornaValor<int>(sql, parametro, transaction);
        }

        /// <summary>
        /// Incrementar chave primária.
        /// </summary>
        /// <param name="codigo">Código dos dados básicos</param>
        /// <param name="transacao">Transação utilizada</param>
        /// <returns>Retorna ID da tabela</returns>
        public int IncrementarRetangulo(int codigo, DbTransaction transacao)
        {
            List<DbParameter> parametro = new List<DbParameter>();
            parametro.Add(controle.criaParametros("@CODIGO", DbType.Int32, codigo));

            string sql = "SELECT " +
                         "A.SEQUENCIA + 1 AS DISPONIVEL " +
                         "FROM " +
                         "AERMOD_RETANGULO A " +

                         "LEFT JOIN AERMOD_RETANGULO B ON " +
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
                IdBuscado = BuscarPrimeiroIdRetangulo(codigo, transacao);

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
        /// Retorna retângulo.
        /// </summary>
        /// <param name="codigo">Código</param>
        /// <returns>Retorna DataTable</returns>
        public DataTable RetornaRetangulo(int codigo)
        {
            string sql = "SELECT " +
                         "SEQUENCIA, " +
                         "VALOR " +

                         "FROM " +
                         "AERMOD_RETANGULO " +

                         "WHERE " +
                        $"CODIGO = {codigo}";

            return controle.RetornarDataTable(sql);
        }

        /// <summary>
        /// Excluir retângulo.
        /// </summary>
        /// <param name="codigo">Código</param>
        /// <param name="transacao">Transação utilizada</param>
        public void ExcluirRetangulo(int codigo, DbTransaction transacao)
        {
            List<DbParameter> parametro = new List<DbParameter>();
            parametro.Add(controle.criaParametros("@CODIGO", DbType.Int32, codigo));

            string sql = "DELETE " +
                         "FROM " +
                         "AERMOD_RETANGULO " +
                         "WHERE " +
                         "CODIGO = @CODIGO";

            controle.ExecutaParametros(sql, transacao, parametro);
        }

        /// <summary>
        /// Atualizar retângulo.
        /// </summary>
        /// <param name="codigo">Código</param>
        /// <param name="lstValor">Lista de valores</param>
        /// <param name="transacao">Transação utilizada</param>
        public void AtualizarRetangulo(int codigo, List<int> lstValor, DbTransaction transacao)
        {
            string sql = "REPLACE INTO " +
                         "AERMOD_RETANGULO " +
                         "(" +
                         "CODIGO, " +
                         "SEQUENCIA, " +
                         "VALOR" +                         
                         ") " +
                         "VALUES " +
                         "(" +
                         "@CODIGO, " +
                         "@SEQUENCIA, " +
                         "@VALOR" +                         
                         ")";

            foreach (var item in lstValor)
            {
                int sequencia = IncrementarRetangulo(codigo, transacao);

                List<DbParameter> parametro = new List<DbParameter>();
                parametro.Add(controle.criaParametros("@CODIGO", DbType.Int32, codigo));
                parametro.Add(controle.criaParametros("@SEQUENCIA", DbType.Int32, sequencia));
                parametro.Add(controle.criaParametros("@VALOR", DbType.Int32, item));

                controle.ExecutaParametros(sql, transacao, parametro);
            }            
        }

        #endregion

        #region Parâmetros da fonte ponto

        /// <summary>
        /// Retorna parâmetros.
        /// </summary>        
        /// <param name="codigoFonte">Código da fonte</param>
        /// <param name="codigoPeriodo">Código do período</param>
        /// <param name="codigoPoluente">Código do poluente</param>
        /// <returns>Retorna DataTable</returns>
        public DataTable RetornaParametroPonto(int codigoFonte, int codigoPeriodo, int codigoPoluente)
        {
            string sql = "SELECT " +
                         "CODIGO_FONTE, " +
                         "CODIGO_PERIODO, " +
                         "CODIGO_POLUENTE, " +
                         "TAXA_EMISSAO, " +
                         "ALTURA_CHAMINE, " +
                         "TEMPERATURA_SAIDA, " +
                         "VELOCIDADE_SAIDA, " +
                         "DIAMETRO_CHAMINE " +

                         "FROM " +
                         "AERMOD_PONTO " +

                         "WHERE " +                        
                        $"CODIGO_FONTE = {codigoFonte} AND " +
                        $"CODIGO_PERIODO = {codigoPeriodo} AND " +
                        $"CODIGO_POLUENTE = {codigoPoluente}";

            return controle.RetornarDataTable(sql);
        }

        /// <summary>
        /// Atualizar parâmetro.
        /// </summary>
        /// <param name="parametrosPonto">Parâmetros do tipo ponto</param>       
        /// <param name="transacao">Transação utilizada</param>
        public void AtualizarParametroPonto(dynamic parametrosPonto, DbTransaction transacao = null)
        {
            string sql = "REPLACE INTO " +
                         "AERMOD_PONTO " +
                         "(" +                         
                         "CODIGO_FONTE, " +
                         "CODIGO_PERIODO, " +
                         "CODIGO_POLUENTE, " +
                         "TAXA_EMISSAO, " +
                         "ALTURA_CHAMINE, " +
                         "TEMPERATURA_SAIDA, " +
                         "VELOCIDADE_SAIDA, " +
                         "DIAMETRO_CHAMINE" +
                         ") " +
                         "VALUES " +
                         "(" +                         
                         "@CODIGO_FONTE, " +
                         "@CODIGO_PERIODO, " +
                         "@CODIGO_POLUENTE, " +
                         "@TAXA_EMISSAO, " +
                         "@ALTURA_CHAMINE, " +
                         "@TEMPERATURA_SAIDA, " +
                         "@VELOCIDADE_SAIDA, " +
                         "@DIAMETRO_CHAMINE" +
                         ")";

            List<DbParameter> parametro = new List<DbParameter>();
            parametro.Add(controle.criaParametros("@CODIGO_FONTE", DbType.Int32, parametrosPonto.CODIGO_FONTE));
            parametro.Add(controle.criaParametros("@CODIGO_PERIODO", DbType.Int32, parametrosPonto.CODIGO_PERIODO));
            parametro.Add(controle.criaParametros("@CODIGO_POLUENTE", DbType.Int32, parametrosPonto.CODIGO_POLUENTE));
            parametro.Add(controle.criaParametros("@TAXA_EMISSAO", DbType.Decimal, parametrosPonto.TAXA_EMISSAO));
            parametro.Add(controle.criaParametros("@ALTURA_CHAMINE", DbType.Decimal, parametrosPonto.ALTURA_CHAMINE));
            parametro.Add(controle.criaParametros("@TEMPERATURA_SAIDA", DbType.Decimal, parametrosPonto.TEMPERATURA_SAIDA));
            parametro.Add(controle.criaParametros("@VELOCIDADE_SAIDA", DbType.Decimal, parametrosPonto.VELOCIDADE_SAIDA));
            parametro.Add(controle.criaParametros("@DIAMETRO_CHAMINE", DbType.Decimal, parametrosPonto.DIAMETRO_CHAMINE));

            controle.ExecutaParametros(sql, transacao, parametro);
        }

        #endregion

        #region Parâmetros da fonte área

        /// <summary>
        /// Retorna parâmetros.
        /// </summary>        
        /// <param name="codigoFonte">Código da fonte</param>
        /// <param name="codigoPeriodo">Código do período</param>
        /// <param name="codigoPoluente">Código do poluente</param>
        /// <returns>Retorna DataTable</returns>
        public DataTable RetornaParametroArea(int codigoFonte, int codigoPeriodo, int codigoPoluente)
        {
            string sql = "SELECT " +
                         "CODIGO_FONTE, " +
                         "CODIGO_PERIODO, " +
                         "CODIGO_POLUENTE, " +
                         "TAXA_EMISSAO, " +
                         "ALTURA_LANCAMENTO, " +
                         "COMPRIMENTO_X, " +
                         "COMPRIMENTO_Y, " +
                         "ANGULO, " +
                         "DIMENSAO_VERTICAL " +

                         "FROM " +
                         "AERMOD_AREA " +

                         "WHERE " +
                        $"CODIGO_FONTE = {codigoFonte} AND " +
                        $"CODIGO_PERIODO = {codigoPeriodo} AND " +
                        $"CODIGO_POLUENTE = {codigoPoluente}";

            return controle.RetornarDataTable(sql);
        }

        /// <summary>
        /// Atualizar parâmetro.
        /// </summary>
        /// <param name="parametrosArea">Parâmetros do tipo área</param>       
        /// <param name="transacao">Transação utilizada</param>
        public void AtualizarParametroArea(dynamic parametrosArea, DbTransaction transacao = null)
        {
            string sql = "REPLACE INTO " +
                         "AERMOD_AREA " +
                         "(" +                         
                         "CODIGO_FONTE, " +
                         "CODIGO_PERIODO, " +
                         "CODIGO_POLUENTE, " +
                         "TAXA_EMISSAO, " +
                         "ALTURA_LANCAMENTO, " +
                         "COMPRIMENTO_X, " +
                         "COMPRIMENTO_Y, " +
                         "ANGULO, " +
                         "DIMENSAO_VERTICAL" +
                         ") " +
                         "VALUES " +
                         "(" +
                         "@CODIGO_FONTE, " +
                         "@CODIGO_PERIODO, " +
                         "@CODIGO_POLUENTE, " +
                         "@TAXA_EMISSAO, " +
                         "@ALTURA_LANCAMENTO, " +
                         "@COMPRIMENTO_X, " +
                         "@COMPRIMENTO_Y, " +
                         "@ANGULO, " +
                         "@DIMENSAO_VERTICAL" +
                         ")";

            List<DbParameter> parametro = new List<DbParameter>();            
            parametro.Add(controle.criaParametros("@CODIGO_FONTE", DbType.Int32, parametrosArea.CODIGO_FONTE));
            parametro.Add(controle.criaParametros("@CODIGO_PERIODO", DbType.Int32, parametrosArea.CODIGO_PERIODO));
            parametro.Add(controle.criaParametros("@CODIGO_POLUENTE", DbType.Int32, parametrosArea.CODIGO_POLUENTE));
            parametro.Add(controle.criaParametros("@TAXA_EMISSAO", DbType.Decimal, parametrosArea.TAXA_EMISSAO));
            parametro.Add(controle.criaParametros("@ALTURA_LANCAMENTO", DbType.Decimal, parametrosArea.ALTURA_LANCAMENTO));
            parametro.Add(controle.criaParametros("@COMPRIMENTO_X", DbType.Decimal, parametrosArea.COMPRIMENTO_X));
            parametro.Add(controle.criaParametros("@COMPRIMENTO_Y", DbType.Decimal, parametrosArea.COMPRIMENTO_Y));
            parametro.Add(controle.criaParametros("@ANGULO", DbType.Decimal, parametrosArea.ANGULO));
            parametro.Add(controle.criaParametros("@DIMENSAO_VERTICAL", DbType.Decimal, parametrosArea.DIMENSAO_VERTICAL));

            controle.ExecutaParametros(sql, transacao, parametro);
        }

        #endregion

        #region Parâmetros da fonte área polígono

        /// <summary>
        /// Retorna parâmetros.
        /// </summary>        
        /// <param name="codigoFonte">Código da fonte</param>
        /// <param name="codigoPeriodo">Código do período</param>
        /// <param name="codigoPoluente">Código do poluente</param>
        /// <returns>Retorna DataTable</returns>
        public DataTable RetornaParametroAreaPoly(int codigoFonte, int codigoPeriodo, int codigoPoluente)
        {
            string sql = "SELECT " +
                         "CODIGO_FONTE, " +
                         "CODIGO_PERIODO, " +
                         "CODIGO_POLUENTE, " +
                         "TAXA_EMISSAO, " +
                         "ALTURA_LANCAMENTO, " +
                         "NUMERO_VERTICES, " +
                         "DIMENSAO_VERTICAL " +

                         "FROM " +
                         "AERMOD_AREAPOLY " +

                         "WHERE " +
                        $"CODIGO_FONTE = {codigoFonte} AND " +
                        $"CODIGO_PERIODO = {codigoPeriodo} AND " +
                        $"CODIGO_POLUENTE = {codigoPoluente}";

            return controle.RetornarDataTable(sql);
        }

        /// <summary>
        /// Atualizar parâmetro.
        /// </summary>
        /// <param name="parametrosArea">Parâmetros do tipo área</param>       
        /// <param name="transacao">Transação utilizada</param>
        public void AtualizarParametroAreaPoly(dynamic parametrosArea, DbTransaction transacao = null)
        {
            string sql = "REPLACE INTO " +
                         "AERMOD_AREAPOLY " +
                         "(" +
                         "CODIGO_FONTE, " +
                         "CODIGO_PERIODO, " +
                         "CODIGO_POLUENTE, " +
                         "TAXA_EMISSAO, " +
                         "ALTURA_LANCAMENTO, " +
                         "NUMERO_VERTICES, " +
                         "DIMENSAO_VERTICAL " +                         
                         ") " +
                         "VALUES " +
                         "(" +
                         "@CODIGO_FONTE, " +
                         "@CODIGO_PERIODO, " +
                         "@CODIGO_POLUENTE, " +
                         "@TAXA_EMISSAO, " +
                         "@ALTURA_LANCAMENTO, " +
                         "@NUMERO_VERTICES, " +
                         "@DIMENSAO_VERTICAL " +                         
                         ")";

            List<DbParameter> parametro = new List<DbParameter>();
            parametro.Add(controle.criaParametros("@CODIGO_FONTE", DbType.Int32, parametrosArea.CODIGO_FONTE));
            parametro.Add(controle.criaParametros("@CODIGO_PERIODO", DbType.Int32, parametrosArea.CODIGO_PERIODO));
            parametro.Add(controle.criaParametros("@CODIGO_POLUENTE", DbType.Int32, parametrosArea.CODIGO_POLUENTE));
            parametro.Add(controle.criaParametros("@TAXA_EMISSAO", DbType.Decimal, parametrosArea.TAXA_EMISSAO));
            parametro.Add(controle.criaParametros("@ALTURA_LANCAMENTO", DbType.Decimal, parametrosArea.ALTURA_LANCAMENTO));
            parametro.Add(controle.criaParametros("@NUMERO_VERTICES", DbType.Int32, parametrosArea.NUMERO_VERTICES));            
            parametro.Add(controle.criaParametros("@DIMENSAO_VERTICAL", DbType.Decimal, parametrosArea.DIMENSAO_VERTICAL));

            controle.ExecutaParametros(sql, transacao, parametro);
        }

        /// <summary>
        /// Retorna parâmetros.
        /// </summary>        
        /// <param name="codigoFonte">Código da fonte</param>
        /// <param name="codigoPeriodo">Código do período</param>
        /// <param name="codigoPoluente">Código do poluente</param>
        /// <returns>Retorna DataTable</returns>
        public DataTable RetornaParametroAreaPolyCoordenadas(int codigoFonte, int codigoPeriodo, int codigoPoluente)
        {
            string sql = "SELECT " +
                         "CODIGO_FONTE, " +
                         "CODIGO_PERIODO, " +
                         "CODIGO_POLUENTE, " +
                         "SEQUENCIA, " +
                         "X, " +
                         "Y " +

                         "FROM " +
                         "AERMOD_AREAPOLY_COORDENADAS " +

                         "WHERE " +
                        $"CODIGO_FONTE = {codigoFonte} AND " +
                        $"CODIGO_PERIODO = {codigoPeriodo} AND " +
                        $"CODIGO_POLUENTE = {codigoPoluente}";

            return controle.RetornarDataTable(sql);
        }

        /// <summary>
        /// Atualizar parâmetro.
        /// </summary>
        /// <param name="codigoFonte">Código da fonte</param>
        /// <param name="codigoPeriodo">Código do período</param>
        /// <param name="codigoPoluente">Código do poluente</param>
        /// <param name="lstParametrosArea">Parâmetros do tipo área</param>       
        /// <param name="transacao">Transação utilizada</param>
        public bool AtualizarParametroAreaPolyCoordenadas(int codigoFonte, int codigoPeriodo, int codigoPoluente, List<Tuple<decimal, decimal>> lstParametrosArea, DbTransaction transacao = null)
        {
            try
            {
                string sql = "DELETE " +
                             "FROM " +
                             "AERMOD_AREAPOLY_COORDENADAS " +
                             "WHERE " +
                             "CODIGO_FONTE = @CODIGO_FONTE AND " +
                             "CODIGO_PERIODO = @CODIGO_PERIODO AND " +
                             "CODIGO_POLUENTE = @CODIGO_POLUENTE";

                List<DbParameter> parametro = new List<DbParameter>();
                parametro.Add(controle.criaParametros("@CODIGO_FONTE", DbType.Int32, codigoFonte));
                parametro.Add(controle.criaParametros("@CODIGO_PERIODO", DbType.Int32, codigoPeriodo));
                parametro.Add(controle.criaParametros("@CODIGO_POLUENTE", DbType.Int32, codigoPoluente));

                controle.ExecutaParametros(sql, transacao, parametro);

                sql = "REPLACE INTO " +
                      "AERMOD_AREAPOLY_COORDENADAS " +
                      "(" +
                      "CODIGO_FONTE, " +
                      "CODIGO_PERIODO, " +
                      "CODIGO_POLUENTE, " +
                      "SEQUENCIA, " +
                      "X, " +
                      "Y" +
                      ") " +
                      "VALUES " +
                      "(" +
                      "@CODIGO_FONTE, " +
                      "@CODIGO_PERIODO, " +
                      "@CODIGO_POLUENTE, " +
                      "@SEQUENCIA, " +
                      "@X, " +
                      "@Y" +
                      ")";

                int sequencia = 0;
                foreach (var item in lstParametrosArea)
                {
                    sequencia++;

                    parametro = new List<DbParameter>();
                    parametro.Add(controle.criaParametros("@CODIGO_FONTE", DbType.Int32, codigoFonte));
                    parametro.Add(controle.criaParametros("@CODIGO_PERIODO", DbType.Int32, codigoPeriodo));
                    parametro.Add(controle.criaParametros("@CODIGO_POLUENTE", DbType.Int32, codigoPoluente));
                    parametro.Add(controle.criaParametros("@SEQUENCIA", DbType.Int32, sequencia));
                    parametro.Add(controle.criaParametros("@X", DbType.Decimal, item.Item1));
                    parametro.Add(controle.criaParametros("@Y", DbType.Decimal, item.Item2));

                    controle.ExecutaParametros(sql, transacao, parametro);
                }
            }
            catch
            {
                return false;
            }           

            return true;
        }

        /// <summary>
        /// Verifica duplicidade de fonte emissora.
        /// </summary>
        /// <param name="codigoFonte">Código da fonte</param>
        /// <param name="codigoPeriodo">Código do período</param>
        /// <param name="codigoPoluente">Código do poluente</param>
        /// <param name="sequencia">Sequência</param>
        /// <param name="X">Coordenada X</param>
        /// <param name="Y">Coordenada Y</param>
        /// <returns>Retorna true caso exista</returns>
        public int VerificarDuplicidadeAreaPolyCoordenadas(int codigoFonte, int codigoPeriodo, int codigoPoluente, int sequencia, decimal X, decimal Y)
        {
            string sql = "SELECT " +
                         "SEQUENCIA " +
                         "FROM " +
                         "AERMOD_AREAPOLY_COORDENADAS " +
                         "WHERE " +
                         "CODIGO_FONTE = @CODIGO_FONTE AND " +
                         "CODIGO_PERIODO = @CODIGO_PERIODO AND " +
                         "CODIGO_POLUENTE = @CODIGO_POLUENTE AND " +
                         "SEQUENCIA <> @SEQUENCIA AND " +
                         "X = @X AND " +
                         "Y = @Y " +
                         "LIMIT 1";

            List<DbParameter> listParametros = new List<DbParameter>();
            listParametros.Add(controle.criaParametros("@CODIGO_FONTE", DbType.Int32, codigoFonte));
            listParametros.Add(controle.criaParametros("@CODIGO_PERIODO", DbType.Int32, codigoPeriodo));
            listParametros.Add(controle.criaParametros("@CODIGO_POLUENTE", DbType.Int32, codigoPoluente));
            listParametros.Add(controle.criaParametros("@SEQUENCIA", DbType.Int32, sequencia));
            listParametros.Add(controle.criaParametros("@X", DbType.Decimal, X));
            listParametros.Add(controle.criaParametros("@Y", DbType.Decimal, Y));

            return controle.RetornaValor<int>(sql, listParametros);
        }

        #endregion

        #region Parâmetros da fonte área círculo

        /// <summary>
        /// Retorna parâmetros.
        /// </summary>        
        /// <param name="codigoFonte">Código da fonte</param>
        /// <param name="codigoPeriodo">Código do período</param>
        /// <param name="codigoPoluente">Código do poluente</param>
        /// <returns>Retorna DataTable</returns>
        public DataTable RetornaParametroAreaCirc(int codigoFonte, int codigoPeriodo, int codigoPoluente)
        {
            string sql = "SELECT " +
                         "CODIGO_FONTE, " +
                         "CODIGO_PERIODO, " +
                         "CODIGO_POLUENTE, " +
                         "TAXA_EMISSAO, " +
                         "ALTURA_LANCAMENTO, " +
                         "RAIO, " +
                         "NUMERO_VERTICES, " +
                         "DIMENSAO_VERTICAL " +

                         "FROM " +
                         "AERMOD_AREACIRC " +

                         "WHERE " +
                        $"CODIGO_FONTE = {codigoFonte} AND " +
                        $"CODIGO_PERIODO = {codigoPeriodo} AND " +
                        $"CODIGO_POLUENTE = {codigoPoluente}";

            return controle.RetornarDataTable(sql);
        }

        /// <summary>
        /// Atualizar parâmetro.
        /// </summary>
        /// <param name="parametrosArea">Parâmetros do tipo área</param>       
        /// <param name="transacao">Transação utilizada</param>
        public void AtualizarParametroAreaCirc(dynamic parametrosArea, DbTransaction transacao = null)
        {
            string sql = "REPLACE INTO " +
                         "AERMOD_AREACIRC " +
                         "(" +
                         "CODIGO_FONTE, " +
                         "CODIGO_PERIODO, " +
                         "CODIGO_POLUENTE, " +
                         "TAXA_EMISSAO, " +
                         "ALTURA_LANCAMENTO, " +
                         "RAIO, " +
                         "NUMERO_VERTICES, " +                        
                         "DIMENSAO_VERTICAL" +
                         ") " +
                         "VALUES " +
                         "(" +
                         "@CODIGO_FONTE, " +
                         "@CODIGO_PERIODO, " +
                         "@CODIGO_POLUENTE, " +
                         "@TAXA_EMISSAO, " +
                         "@ALTURA_LANCAMENTO, " +
                         "@RAIO, " +
                         "@NUMERO_VERTICES, " +                         
                         "@DIMENSAO_VERTICAL" +
                         ")";

            List<DbParameter> parametro = new List<DbParameter>();
            parametro.Add(controle.criaParametros("@CODIGO_FONTE", DbType.Int32, parametrosArea.CODIGO_FONTE));
            parametro.Add(controle.criaParametros("@CODIGO_PERIODO", DbType.Int32, parametrosArea.CODIGO_PERIODO));
            parametro.Add(controle.criaParametros("@CODIGO_POLUENTE", DbType.Int32, parametrosArea.CODIGO_POLUENTE));
            parametro.Add(controle.criaParametros("@TAXA_EMISSAO", DbType.Decimal, parametrosArea.TAXA_EMISSAO));
            parametro.Add(controle.criaParametros("@ALTURA_LANCAMENTO", DbType.Decimal, parametrosArea.ALTURA_LANCAMENTO));
            parametro.Add(controle.criaParametros("@RAIO", DbType.Decimal, parametrosArea.RAIO));
            parametro.Add(controle.criaParametros("@NUMERO_VERTICES", DbType.Int32, parametrosArea.NUMERO_VERTICES));           
            parametro.Add(controle.criaParametros("@DIMENSAO_VERTICAL", DbType.Decimal, parametrosArea.DIMENSAO_VERTICAL));

            controle.ExecutaParametros(sql, transacao, parametro);
        }

        #endregion

        #region Parâmetros da fonte volume

        /// <summary>
        /// Retorna parâmetros.
        /// </summary>        
        /// <param name="codigoFonte">Código da fonte</param>
        /// <param name="codigoPeriodo">Código do período</param>
        /// <param name="codigoPoluente">Código do poluente</param>
        /// <returns>Retorna DataTable</returns>
        public DataTable RetornaParametroVolume(int codigoFonte, int codigoPeriodo, int codigoPoluente)
        {
            string sql = "SELECT " +
                         "CODIGO_FONTE, " +
                         "CODIGO_PERIODO, " +
                         "CODIGO_POLUENTE, " +
                         "TAXA_EMISSAO, " +
                         "ALTURA_LANCAMENTO, " +                         
                         "DIMENSAO_LATERAL, " +
                         "DIMENSAO_VERTICAL " +

                         "FROM " +
                         "AERMOD_VOLUME " +

                         "WHERE " +
                        $"CODIGO_FONTE = {codigoFonte} AND " +
                        $"CODIGO_PERIODO = {codigoPeriodo} AND " +
                        $"CODIGO_POLUENTE = {codigoPoluente}";

            return controle.RetornarDataTable(sql);
        }

        /// <summary>
        /// Atualizar parâmetro.
        /// </summary>
        /// <param name="parametrosVolume">Parâmetros do tipo volume</param>       
        /// <param name="transacao">Transação utilizada</param>
        public void AtualizarParametroVolume(dynamic parametrosVolume, DbTransaction transacao = null)
        {
            string sql = "REPLACE INTO " +
                         "AERMOD_VOLUME " +
                         "(" +
                         "CODIGO_FONTE, " +
                         "CODIGO_PERIODO, " +
                         "CODIGO_POLUENTE, " +
                         "TAXA_EMISSAO, " +
                         "ALTURA_LANCAMENTO, " +                         
                         "DIMENSAO_LATERAL, " +
                         "DIMENSAO_VERTICAL" +
                         ") " +
                         "VALUES " +
                         "(" +
                         "@CODIGO_FONTE, " +
                         "@CODIGO_PERIODO, " +
                         "@CODIGO_POLUENTE, " +
                         "@TAXA_EMISSAO, " +
                         "@ALTURA_LANCAMENTO, " +                         
                         "@DIMENSAO_LATERAL, " +
                         "@DIMENSAO_VERTICAL" +
                         ")";

            List<DbParameter> parametro = new List<DbParameter>();
            parametro.Add(controle.criaParametros("@CODIGO_FONTE", DbType.Int32, parametrosVolume.CODIGO_FONTE));
            parametro.Add(controle.criaParametros("@CODIGO_PERIODO", DbType.Int32, parametrosVolume.CODIGO_PERIODO));
            parametro.Add(controle.criaParametros("@CODIGO_POLUENTE", DbType.Int32, parametrosVolume.CODIGO_POLUENTE));
            parametro.Add(controle.criaParametros("@TAXA_EMISSAO", DbType.Decimal, parametrosVolume.TAXA_EMISSAO));
            parametro.Add(controle.criaParametros("@ALTURA_LANCAMENTO", DbType.Decimal, parametrosVolume.ALTURA_LANCAMENTO));            
            parametro.Add(controle.criaParametros("@DIMENSAO_LATERAL", DbType.Decimal, parametrosVolume.DIMENSAO_LATERAL));
            parametro.Add(controle.criaParametros("@DIMENSAO_VERTICAL", DbType.Decimal, parametrosVolume.DIMENSAO_VERTICAL));

            controle.ExecutaParametros(sql, transacao, parametro);
        }

        #endregion

        #region Parâmetros da fonte linha

        /// <summary>
        /// Retorna parâmetros.
        /// </summary>        
        /// <param name="codigoFonte">Código da fonte</param>
        /// <param name="codigoPeriodo">Código do período</param>
        /// <param name="codigoPoluente">Código do poluente</param>
        /// <returns>Retorna DataTable</returns>
        public DataTable RetornaParametroLinha(int codigoFonte, int codigoPeriodo, int codigoPoluente)
        {
            string sql = "SELECT " +
                         "CODIGO_FONTE, " +
                         "CODIGO_PERIODO, " +
                         "CODIGO_POLUENTE, " +
                         "TAXA_EMISSAO, " +
                         "ALTURA_LANCAMENTO, " +
                         "LARGURA, " +
                         "DIMENSAO_VERTICAL " +

                         "FROM " +
                         "AERMOD_LINHA " +

                         "WHERE " +
                        $"CODIGO_FONTE = {codigoFonte} AND " +
                        $"CODIGO_PERIODO = {codigoPeriodo} AND " +
                        $"CODIGO_POLUENTE = {codigoPoluente}";

            return controle.RetornarDataTable(sql);
        }

        /// <summary>
        /// Atualizar parâmetro.
        /// </summary>
        /// <param name="parametrosLinha">Parâmetros do tipo linha</param>       
        /// <param name="transacao">Transação utilizada</param>
        public void AtualizarParametroLinha(dynamic parametrosLinha, DbTransaction transacao = null)
        {
            string sql = "REPLACE INTO " +
                         "AERMOD_LINHA " +
                         "(" +
                         "CODIGO_FONTE, " +
                         "CODIGO_PERIODO, " +
                         "CODIGO_POLUENTE, " +
                         "TAXA_EMISSAO, " +
                         "ALTURA_LANCAMENTO, " +
                         "LARGURA, " +
                         "DIMENSAO_VERTICAL" +
                         ") " +
                         "VALUES " +
                         "(" +
                         "@CODIGO_FONTE, " +
                         "@CODIGO_PERIODO, " +
                         "@CODIGO_POLUENTE, " +
                         "@TAXA_EMISSAO, " +
                         "@ALTURA_LANCAMENTO, " +
                         "@LARGURA, " +
                         "@DIMENSAO_VERTICAL" +
                         ")";

            List<DbParameter> parametro = new List<DbParameter>();
            parametro.Add(controle.criaParametros("@CODIGO_FONTE", DbType.Int32, parametrosLinha.CODIGO_FONTE));
            parametro.Add(controle.criaParametros("@CODIGO_PERIODO", DbType.Int32, parametrosLinha.CODIGO_PERIODO));
            parametro.Add(controle.criaParametros("@CODIGO_POLUENTE", DbType.Int32, parametrosLinha.CODIGO_POLUENTE));
            parametro.Add(controle.criaParametros("@TAXA_EMISSAO", DbType.Decimal, parametrosLinha.TAXA_EMISSAO));
            parametro.Add(controle.criaParametros("@ALTURA_LANCAMENTO", DbType.Decimal, parametrosLinha.ALTURA_LANCAMENTO));
            parametro.Add(controle.criaParametros("@LARGURA", DbType.Decimal, parametrosLinha.LARGURA));
            parametro.Add(controle.criaParametros("@DIMENSAO_VERTICAL", DbType.Decimal, parametrosLinha.DIMENSAO_VERTICAL));

            controle.ExecutaParametros(sql, transacao, parametro);
        }

        #endregion

        #region Parâmetros da fonte poço aberto

        /// <summary>
        /// Retorna parâmetros.
        /// </summary>        
        /// <param name="codigoFonte">Código da fonte</param>
        /// <param name="codigoPeriodo">Código do período</param>
        /// <param name="codigoPoluente">Código do poluente</param>
        /// <returns>Retorna DataTable</returns>
        public DataTable RetornaParametroPoco(int codigoFonte, int codigoPeriodo, int codigoPoluente)
        {
            string sql = "SELECT " +
                         "CODIGO_FONTE, " +
                         "CODIGO_PERIODO, " +
                         "CODIGO_POLUENTE, " +
                         "TAXA_EMISSAO, " +
                         "ALTURA_LANCAMENTO, " +
                         "COMPRIMENTO_X, " +
                         "COMPRIMENTO_Y, " +
                         "VOLUME, " +
                         "ANGULO " +

                         "FROM " +
                         "AERMOD_POCO_ABERTO " +

                         "WHERE " +
                        $"CODIGO_FONTE = {codigoFonte} AND " +
                        $"CODIGO_PERIODO = {codigoPeriodo} AND " +
                        $"CODIGO_POLUENTE = {codigoPoluente}";

            return controle.RetornarDataTable(sql);
        }

        /// <summary>
        /// Atualizar parâmetro.
        /// </summary>
        /// <param name="parametrosPoco">Parâmetros do tipo poço aberto</param>       
        /// <param name="transacao">Transação utilizada</param>
        public void AtualizarParametroPoco(dynamic parametrosPoco, DbTransaction transacao = null)
        {
            string sql = "REPLACE INTO " +
                         "AERMOD_POCO_ABERTO " +
                         "(" +
                         "CODIGO_FONTE, " +
                         "CODIGO_PERIODO, " +
                         "CODIGO_POLUENTE, " +
                         "TAXA_EMISSAO, " +
                         "ALTURA_LANCAMENTO, " +
                         "COMPRIMENTO_X, " +
                         "COMPRIMENTO_Y, " +
                         "VOLUME, " +
                         "ANGULO" +
                         ") " +
                         "VALUES " +
                         "(" +
                         "@CODIGO_FONTE, " +
                         "@CODIGO_PERIODO, " +
                         "@CODIGO_POLUENTE, " +
                         "@TAXA_EMISSAO, " +
                         "@ALTURA_LANCAMENTO, " +
                         "@COMPRIMENTO_X, " +
                         "@COMPRIMENTO_Y, " +
                         "@VOLUME, " +
                         "@ANGULO" +
                         ")";

            List<DbParameter> parametro = new List<DbParameter>();
            parametro.Add(controle.criaParametros("@CODIGO_FONTE", DbType.Int32, parametrosPoco.CODIGO_FONTE));
            parametro.Add(controle.criaParametros("@CODIGO_PERIODO", DbType.Int32, parametrosPoco.CODIGO_PERIODO));
            parametro.Add(controle.criaParametros("@CODIGO_POLUENTE", DbType.Int32, parametrosPoco.CODIGO_POLUENTE));
            parametro.Add(controle.criaParametros("@TAXA_EMISSAO", DbType.Decimal, parametrosPoco.TAXA_EMISSAO));
            parametro.Add(controle.criaParametros("@ALTURA_LANCAMENTO", DbType.Decimal, parametrosPoco.ALTURA_LANCAMENTO));
            parametro.Add(controle.criaParametros("@COMPRIMENTO_X", DbType.Decimal, parametrosPoco.COMPRIMENTO_X));
            parametro.Add(controle.criaParametros("@COMPRIMENTO_Y", DbType.Decimal, parametrosPoco.COMPRIMENTO_Y));
            parametro.Add(controle.criaParametros("@VOLUME", DbType.Decimal, parametrosPoco.VOLUME));
            parametro.Add(controle.criaParametros("@ANGULO", DbType.Decimal, parametrosPoco.ANGULO));

            controle.ExecutaParametros(sql, transacao, parametro);
        }

        #endregion

        #endregion
    }
}
