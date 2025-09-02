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
    public class ClsAERMAP
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
        public ClsAERMAP(ConfiguracaoRede configuracaoRede)
        {
            this.configuracaoRede = configuracaoRede;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Salvar arquivo.
        /// </summary>        
        /// <param name="arquivoINP">Arquivo (.INP)</param>
        /// <param name="arquivoOUT">Arquivo (.OUT)</param>
        /// <param name="arquivoROU">Arquivo (.ROU)</param>
        /// <param name="arquivoSOU">Arquivo (.SOU)</param>
        /// <returns>Retorna false caso exista erro</returns>
        public bool SalvarArquivo(byte[] arquivoINP, byte[] arquivoOUT, byte[] arquivoROU, byte[] arquivoSOU)
        {
            string sql = "REPLACE INTO " +
                         "AERMAP " +
                         "(" +
                         "CODIGO, " +
                         "ARQUIVO_INP, " +
                         "ARQUIVO_OUT, " +
                         "ARQUIVO_ROU, " +
                         "ARQUIVO_SOU" +
                         ") " +
                         "VALUES " +
                         "(" +
                         "@CODIGO, " +
                         "@ARQUIVO_INP, " +
                         "@ARQUIVO_OUT, " +
                         "@ARQUIVO_ROU, " +
                         "@ARQUIVO_SOU" +
                         ")";            

            List<DbParameter> parametro = new List<DbParameter>();
            parametro.Add(controle.criaParametros("@CODIGO", DbType.Int32, 1));
            parametro.Add(controle.criaParametros("@ARQUIVO_INP", DbType.Binary, arquivoINP));
            parametro.Add(controle.criaParametros("@ARQUIVO_OUT", DbType.Binary, arquivoOUT));
            parametro.Add(controle.criaParametros("@ARQUIVO_ROU", DbType.Binary, arquivoROU));
            parametro.Add(controle.criaParametros("@ARQUIVO_SOU", DbType.Binary, arquivoSOU));

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
        /// Retorna arquivo (.INP).
        /// </summary>        
        /// <returns>Retorna byte[]</returns>
        public byte[] RetornarArquivoINP()
        {
            string sql = "SELECT " +
                         "ARQUIVO_INP " +
                         "FROM " +
                         "AERMAP " +
                         "WHERE " +
                         "CODIGO = 1";

            return controle.RetornaValor<byte[]>(sql);
        }

        /// <summary>
        /// Retorna arquivo (.OUT).
        /// </summary>        
        /// <returns>Retorna byte[]</returns>
        public byte[] RetornarArquivoOUT()
        {
            string sql = "SELECT " +
                         "ARQUIVO_OUT " +
                         "FROM " +
                         "AERMAP " +
                         "WHERE " +
                         "CODIGO = 1";            

            return controle.RetornaValor<byte[]>(sql);
        }

        /// <summary>
        /// Retorna arquivo (.ROU).
        /// </summary>        
        /// <returns>Retorna byte[]</returns>
        public byte[] RetornarArquivoROU()
        {
            string sql = "SELECT " +
                         "ARQUIVO_ROU " +
                         "FROM " +
                         "AERMAP " +
                         "WHERE " +
                         "CODIGO = 1";

            return controle.RetornaValor<byte[]>(sql);
        }

        /// <summary>
        /// Retorna arquivo (.SOU).
        /// </summary>        
        /// <returns>Retorna byte[]</returns>
        public byte[] RetornarArquivoSOU()
        {
            string sql = "SELECT " +
                         "ARQUIVO_SOU " +
                         "FROM " +
                         "AERMAP " +
                         "WHERE " +
                         "CODIGO = 1";

            return controle.RetornaValor<byte[]>(sql);
        }

        #endregion
    }
}
