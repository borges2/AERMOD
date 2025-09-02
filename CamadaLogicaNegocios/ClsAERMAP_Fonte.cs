using AERMOD.LIB.Formatacao;
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
    public class ClsFonteAERMAP
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
        public ClsFonteAERMAP(ConfiguracaoRede configuracaoRede)
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
                         "AERMAP_FONTES " +
                         "LIMIT 1";

            return controle.RetornaValor<int>(sql);
        }

        /// <summary>
        /// Retorna todas as fontes.
        /// </summary>
        /// <returns></returns>
        public DataTable RetornarRegistros()
        {
            string sql = "SELECT " +
                         "CODIGO, " +
                         "TIPO, " +
                         "X, " +
                         "Y, " +
                         "DESCRICAO, " +
                         "EM_USO " +
                         "FROM " +
                         "AERMAP_FONTES";

            return controle.RetornarDataTable(sql);
        }

        /// <summary>
        /// Retorna todas as fontes.
        /// </summary>
        /// <returns></returns>
        public DataTable RetornarRegistrosUso()
        {
            string sql = "SELECT " +
                         "CODIGO, " +
                         "TIPO, " +
                         "X, " +
                         "Y, " +
                         "DESCRICAO, " +
                         "EM_USO " +
                         "FROM " +
                         "AERMAP_FONTES " +
                         "WHERE " +
                         "EM_USO = TRUE";

            return controle.RetornarDataTable(sql);
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
                         "AERMAP_FONTES A " +

                         "LEFT JOIN AERMAP_FONTES B ON " +
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
        /// Inserir/Alterar fonte.
        /// </summary>
        /// <param name="fonte">Objeto fonte</param>
        /// <returns>Retorna false caso exista erro</returns>
        public int Atualizar(dynamic fonte)
        {
            string sql = "REPLACE INTO " +
                         "AERMAP_FONTES " +
                         "(" +
                         "CODIGO, " +
                         "TIPO, " +
                         "X, " +
                         "Y, " +
                         "DESCRICAO, " +
                         "EM_USO" +
                         ") " +
                         "VALUES " +
                         "(" +
                         "@CODIGO, " +
                         "@TIPO, " +
                         "@X, " +
                         "@Y, " +
                         "@DESCRICAO, " +
                         "@EM_USO" +
                         ")";

            if (fonte.CODIGO == 0)
            {
                fonte.CODIGO = Incrementar();
            }

            List<DbParameter> listParametros = new List<DbParameter>();
            listParametros.Add(controle.criaParametros("@CODIGO", DbType.Int32, fonte.CODIGO));
            listParametros.Add(controle.criaParametros("@TIPO", DbType.Int32, fonte.TIPO));
            listParametros.Add(controle.criaParametros("@X", DbType.Decimal, fonte.X));
            listParametros.Add(controle.criaParametros("@Y", DbType.Decimal, fonte.Y));
            listParametros.Add(controle.criaParametros("@DESCRICAO", DbType.String, fonte.DESCRICAO));
            listParametros.Add(controle.criaParametros("@EM_USO", DbType.Boolean, fonte.EM_USO));

            try
            {
                controle.ExecutaParametros(sql, listParametros);

                return fonte.CODIGO;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Excluir fonte.
        /// </summary>
        /// <param name="codigo">Código da fonte</param>
        public void Excluir(int codigo)
        {
            string sql = "DELETE " +
                         "FROM " +
                         "AERMAP_FONTES " +
                         "WHERE " +
                        $"CODIGO = {codigo}";

            controle.ExecutarComando(sql);
        }

        /// <summary>
        /// Verifica duplicidade de fonte emissora.
        /// </summary>
        /// <param name="codigo">Código da fonte emissora</param>
        /// <param name="X">Coordenada X</param>
        /// <param name="Y">Coordenada Y</param>
        /// <param name="descricao">Descrição da fonte</param>
        /// <returns>Retorna true caso exista</returns>
        public int VerificarDuplicidade(int codigo, decimal X, decimal Y, string descricao)
        {
            string sql = "SELECT " +
                         "CODIGO " +
                         "FROM " +
                         "AERMAP_FONTES " +
                         "WHERE " +
                         "CODIGO <> @CODIGO AND " +
                         "X = @X AND " +
                         "Y = @Y AND " +
                         "DESCRICAO = @DESCRICAO " +
                         "LIMIT 1";

            List<DbParameter> listParametros = new List<DbParameter>();
            listParametros.Add(controle.criaParametros("@CODIGO", DbType.Int32, codigo));            
            listParametros.Add(controle.criaParametros("@X", DbType.Decimal, X));
            listParametros.Add(controle.criaParametros("@Y", DbType.Decimal, Y));
            listParametros.Add(controle.criaParametros("@DESCRICAO", DbType.String, descricao));

            return controle.RetornaValor<int>(sql, listParametros);            
        }

        /// <summary>
        /// Atualizar utilização da fonte emissora.
        /// </summary>       
        /// <param name="lstCodigo">Lista com código das fontes emissoras</param>
        public void AtualizarUso(List<int> lstCodigo)
        {

            string codigo = lstCodigo.Select(I => I.ToString()).Concatenar(",");            

            string sql = "UPDATE " +
                         "AERMAP_FONTES " +
                         "SET " +
                         "EM_USO = FALSE";

            controle.ExecutarComando(sql);

            sql = "UPDATE " +
                  "AERMAP_FONTES " +
                  "SET " +
                  "EM_USO = TRUE " +
                  "WHERE " +
                 $"CODIGO IN ({codigo})";

            controle.ExecutarComando(sql);
        }

        /// <summary>
        /// Retorna menor coordenada X.
        /// </summary>
        /// <returns>Retorna coordenada</returns>
        public decimal MenorCoordenada_X()
        {
            string sql = "SELECT " +
                         "X " +
                         "FROM " +
                         "AERMAP_FONTES " +
                         "WHERE " +
                         "EM_USO = TRUE " +
                         "ORDER BY " +
                         "X ASC " +
                         "LIMIT 1";

            return controle.RetornaValor<decimal>(sql);
        }

        /// <summary>
        /// Retorna menor coordenada Y.
        /// </summary>
        /// <returns>Retorna coordenada</returns>
        public decimal MenorCoordenada_Y()
        {
            string sql = "SELECT " +
                         "Y " +
                         "FROM " +
                         "AERMAP_FONTES " +
                         "WHERE " +
                         "EM_USO = TRUE " +
                         "ORDER BY " +
                         "Y ASC " +
                         "LIMIT 1";

            return controle.RetornaValor<decimal>(sql);
        }

        /// <summary>
        /// Retorna maior coordenada X.
        /// </summary>
        /// <returns>Retorna coordenada</returns>
        public decimal MaiorCoordenada_X()
        {
            string sql = "SELECT " +
                         "X " +
                         "FROM " +
                         "AERMAP_FONTES " +
                         "WHERE " +
                         "EM_USO = TRUE " +
                         "ORDER BY " +
                         "X DESC " +
                         "LIMIT 1";

            return controle.RetornaValor<decimal>(sql);
        }

        /// <summary>
        /// Retorna maior coordenada Y.
        /// </summary>
        /// <returns>Retorna coordenada</returns>
        public decimal MaiorCoordenada_Y()
        {
            string sql = "SELECT " +
                         "Y " +
                         "FROM " +
                         "AERMAP_FONTES " +
                         "WHERE " +
                         "EM_USO = TRUE " +
                         "ORDER BY " +
                         "Y DESC " +
                         "LIMIT 1";

            return controle.RetornaValor<decimal>(sql);
        }

        #endregion
    }
}
