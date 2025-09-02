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
    public class ClsHelp
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
        public ClsHelp(ConfiguracaoRede configuracaoRede)
        {
            this.configuracaoRede = configuracaoRede;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Busca a Mensagem em banco
        /// </summary>
        /// <param name="id">Código da mensagem</param>
        /// <returns>String</returns>
        public string BuscarMensagem(Int64 id)
        {            
            string sql = "SELECT MENSAGEM FROM AJUDA WHERE CODIGO = @CODIGO";

            List<DbParameter> parametro = new List<DbParameter>();
            parametro.Add(controle.criaParametros("@CODIGO", DbType.Int64, id));

            string mensagem = controle.RetornaValor<string>(sql, parametro);

            if (mensagem == string.Empty)
            {
                mensagem = "Mensagem não encontrada :" + id;
            }

            return mensagem;
        }

        #endregion
    }
}
