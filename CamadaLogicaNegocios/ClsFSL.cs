using CamadaAcessoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamadaLogicaNegocios
{
    public class ClsFSL
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
        public ClsFSL(ConfiguracaoRede configuracaoRede)
        {
            this.configuracaoRede = configuracaoRede;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Buscar o menor código.
        /// </summary>
        /// <returns>Retorna o menor ID da tabela</returns>
        public int BuscarPrimeiroId()
        {
            string sql = "SELECT " +
                         "CODIGO " +
                         "FROM " +
                         "FSL " +
                         "LIMIT 1";

            return controle.RetornaValor<int>(sql);
        }

        /// <summary>
        /// Incrementar chave primária.
        /// </summary>
        /// <returns></returns>
        public int Incrementar()
        {
            string sql = "SELECT " +
                         "A.CODIGO + 1 AS DISPONIVEL " +
                         "FROM " +
                         "FSL A " +

                         "LEFT JOIN FSL B ON " +
                         "B.CODIGO = A.CODIGO + 1 " +

                         "WHERE " +
                         "B.CODIGO IS NULL AND " +
                         "A.CODIGO < 99999 " +
                         "ORDER BY " +
                         "A.CODIGO DESC " +
                         "LIMIT 1";

            int IdBuscado = controle.RetornaValor<int>(sql);

            if (IdBuscado == 0)
            {
                IdBuscado = BuscarPrimeiroId();

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
        /// Salvar arquivo.
        /// </summary>        
        /// <param name="arquivo">Arquivo</param>
        /// <param name="descricao">Descrição do arquivo</param>
        /// <returns>Retorna false caso exista erro</returns>
        public bool SalvarArquivo(byte[] arquivo, string descricao)
        {
            string sql = "REPLACE INTO " +
                         "FSL " +
                         "(" +
                         "CODIGO, " +
                         "ARQUIVO, " +
                         "DESCRICAO, " +
                         "EM_USO" +
                         ") " +
                         "VALUES " +
                         "(" +
                         "@CODIGO, " +
                         "@ARQUIVO, " +
                         "@DESCRICAO, " +
                         "@EM_USO" +
                         ")";

            int codigo = Incrementar();
            int primeiroCodigo = BuscarPrimeiroId();
            bool emUso = primeiroCodigo == 0 ? true : false;

            List<DbParameter> parametro = new List<DbParameter>();
            parametro.Add(controle.criaParametros("@CODIGO", DbType.Int32, codigo));
            parametro.Add(controle.criaParametros("@ARQUIVO", DbType.Binary, arquivo));
            parametro.Add(controle.criaParametros("@DESCRICAO", DbType.String, descricao));
            parametro.Add(controle.criaParametros("@EM_USO", DbType.Boolean, emUso));

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
        /// Retornar arquivo.
        /// </summary>
        /// <param name="codigoUsuario">Código do usuário</param>
        /// <returns>Retorna byte[]</returns>
        public DataTable RetornarArquivos()
        {
            string sql = "SELECT " +
                         "CODIGO, " +
                         "ARQUIVO, " +
                         "DESCRICAO, " +
                         "EM_USO " +
                         "FROM " +
                         "FSL";

            return controle.RetornarDataTable(sql);
        }

        /// <summary>
        /// Retorna arquivo em uso.
        /// </summary>
        /// <returns>Retorna byte[]</returns>
        public Tuple<byte[], string> RetornarArquivoUso()
        {
            string sql = "SELECT " +
                         "ARQUIVO, " +
                         "DESCRICAO " +
                         "FROM " +
                         "FSL " +
                         "WHERE " +
                         "EM_USO = TRUE";

            DataTable dt = controle.RetornarDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                return new Tuple<byte[], string>((byte[])dt.Rows[0]["ARQUIVO"], dt.Rows[0]["DESCRICAO"].ToString());
            }

            return null;            
        }

        /// <summary>
        /// Retorna arquivo.
        /// </summary>
        /// <param name="codigo">Código do arquivo</param>
        /// <returns>Retorna byte[]</returns>
        public byte[] RetornarArquivo(int codigo)
        {
            string sql = "SELECT " +
                         "ARQUIVO " +
                         "FROM " +
                         "FSL " +
                         "WHERE " +
                         "CODIGO = @CODIGO";

            List<DbParameter> parametro = new List<DbParameter>();
            parametro.Add(controle.criaParametros("@CODIGO", DbType.Int32, codigo));

            return controle.RetornaValor<byte[]>(sql, parametro);
        }

        /// <summary>
        /// Atualizar utilização do arquivo.
        /// </summary>
        /// <param name="codigo">Código do arquivo</param>        
        public void AtualizarUsoArquivo(int codigo)
        {
            List<DbParameter> parametro = new List<DbParameter>();
            parametro.Add(controle.criaParametros("@CODIGO", DbType.Int32, codigo));

            string sql = "UPDATE " +
                         "FSL " +
                         "SET " +
                         "EM_USO = FALSE";

            controle.ExecutarComando(sql);

            sql = "UPDATE " +
                  "FSL " +
                  "SET " +
                  "EM_USO = TRUE " +
                  "WHERE " +
                  "CODIGO = @CODIGO";

            controle.ExecutarComando(sql, parametro);
        }

        /// <summary>
        /// Excluir arquivo.
        /// </summary>
        /// <param name="codigo">Código do arquivo</param>
        public void ExcluirArquivo(int codigo)
        {
            string sql = "DELETE " +
                         "FROM " +
                         "FSL " +
                         "WHERE " +
                         "CODIGO = @CODIGO";

            List<DbParameter> parametro = new List<DbParameter>();
            parametro.Add(controle.criaParametros("@CODIGO", DbType.Int32, codigo));

            controle.ExecutarComando(sql, parametro);
        }

        /// <summary>
        /// Verifica duplicidade da descrição do arquivo.
        /// </summary>       
        /// <param name="descricao">Descrição do arquivo</param>
        /// <returns>Retorna true caso exista</returns>
        public int VerificaDuplicidadeDescricao(string descricao)
        {
            string sql = "SELECT " +
                         "CODIGO " +
                         "FROM " +
                         "FSL " +
                         "WHERE " +
                         "DESCRICAO = @DESCRICAO";

            List<DbParameter> listParametro = new List<DbParameter>();
            listParametro.Add(controle.criaParametros("@DESCRICAO", DbType.String, descricao));

            return controle.RetornaValor<int>(sql, listParametro);
        }

        /// <summary>
        /// Verifica duplicidade do arquivo informado.
        /// </summary>       
        /// <param name="arquivo">Arquivo a ser verificado</param>
        /// <returns>Retorna true caso exista</returns>
        public int VerificaDuplicidadeArquivo(byte[] arquivo)
        {
            string sql = "SELECT " +
                         "CODIGO " +
                         "FROM " +
                         "FSL " +
                         "WHERE " +
                         "ARQUIVO = @ARQUIVO";

            List<DbParameter> listParametro = new List<DbParameter>();
            listParametro.Add(controle.criaParametros("@ARQUIVO", DbType.Binary, arquivo));

            return controle.RetornaValor<int>(sql, listParametro);
        }

        #endregion
    }
}
