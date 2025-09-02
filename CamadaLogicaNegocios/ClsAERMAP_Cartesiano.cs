using CamadaAcessoDados;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AERMOD.LIB.Formatacao;

namespace CamadaLogicaNegocios
{
    public class ClsAERMAP_Cartesiano
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

        /// <summary>
        /// Código do domínio.
        /// </summary>
        int codigoDominio;

        #endregion

        #region Construtor

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="configuracaoRede">Configuração Banco de Dados</param>
        public ClsAERMAP_Cartesiano(ConfiguracaoRede configuracaoRede, int codigoDominio)
        {
            this.configuracaoRede = configuracaoRede;
            this.codigoDominio = codigoDominio;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Busca o ultimo registro
        /// </summary>
        /// <returns></returns>
        public int BuscarUltimoId()
        {
            string sql = "SELECT " +
                         "CODIGO " +
                         "FROM " +
                         "AERMAP_CARTESIANO " +
                         "WHERE " +
                        $"CODIGO_DOMINIO = {codigoDominio} " +
                         "ORDER BY " +
                         "CODIGO DESC " +
                         "LIMIT 1";

            return controle.RetornaValor<int>(sql);
        }

        /// <summary>
        /// Busca o primeiro registro
        /// </summary>
        /// <returns></returns>
        public int BuscarPrimeiroId()
        {
            string sql = "SELECT " +
                         "CODIGO " +
                         "FROM " +
                         "AERMAP_CARTESIANO " +
                         "WHERE " +
                        $"CODIGO_DOMINIO = {codigoDominio} " +
                         "ORDER BY " +
                         "CODIGO " +
                         "LIMIT 1";

            return controle.RetornaValor<int>(sql);
        }

        /// <summary>
        /// Busca o proximo código
        /// </summary>
        /// <param name="codigoAtual">Codigo atual</param>
        /// <returns></returns>
        public int BuscarIdProximo(int codigoAtual)
        {
            string sql = "SELECT " +
                         "CODIGO " +
                         "FROM " +
                         "AERMAP_CARTESIANO " +
                         "WHERE " +
                        $"CODIGO_DOMINIO = {codigoDominio} AND " +
                        $"CODIGO > {codigoAtual} " +
                         "ORDER BY " +
                         "CODIGO " +
                         "LIMIT 1";
            
            return controle.RetornaValor<int>(sql);
        }

        /// <summary>
        /// Busca o código anterior
        /// </summary>
        /// <param name="codigoAtual">Código atual</param>
        /// <returns></returns>
        public int BuscarIdAnterior(int codigoAtual)
        {
            string sql = "SELECT " +
                         "CODIGO " +
                         "FROM " +
                         "AERMAP_CARTESIANO " +
                         "WHERE " +
                        $"CODIGO_DOMINIO = {codigoDominio} AND " +
                        $"CODIGO < {codigoAtual} " +
                         "ORDER BY " +
                         "CODIGO DESC " +
                         "LIMIT 1";

            return controle.RetornaValor<int>(sql);
        }

        /// <summary>
        /// Método para atualizar (alterar um registro)
        /// </summary>
        /// <param name="grade">Objeto grade</param>
        /// <returns>Retorna false caso exista erro</returns>
        public bool Atualizar(dynamic grade)
        {            
            string sql = "REPLACE INTO " +
                         "AERMAP_CARTESIANO " +
                         "(" +
                         "CODIGO_DOMINIO, " +
                         "CODIGO, " +
                         "XINIT, " +
                         "XNUM, " +
                         "XDELTA, " +
                         "YINIT, " +
                         "YNUM, " +
                         "YDELTA, " +
                         "DESCRICAO, " +
                         "EM_USO" +
                         ") " +
                         "VALUES " +
                         "(" +
                         "@CODIGO_DOMINIO, " +
                         "@CODIGO, " +
                         "@XINIT, " +
                         "@XNUM, " +
                         "@XDELTA, " +
                         "@YINIT, " +
                         "@YNUM, " +
                         "@YDELTA, " +
                         "@DESCRICAO, " +
                         "@EM_USO" +
                         ")";

            List<DbParameter> parametro = new List<DbParameter>();
            parametro.Add(controle.criaParametros("@CODIGO_DOMINIO", DbType.Int32, codigoDominio));
            parametro.Add(controle.criaParametros("@CODIGO", DbType.Int32, grade.CODIGO));
            parametro.Add(controle.criaParametros("@XINIT", DbType.Decimal, grade.XINIT));
            parametro.Add(controle.criaParametros("@XNUM", DbType.Int32, grade.XNUM));
            parametro.Add(controle.criaParametros("@XDELTA", DbType.Decimal, grade.XDELTA));
            parametro.Add(controle.criaParametros("@YINIT", DbType.Decimal, grade.YINIT));
            parametro.Add(controle.criaParametros("@YNUM", DbType.Int32, grade.YNUM));
            parametro.Add(controle.criaParametros("@YDELTA", DbType.Decimal, grade.YDELTA));
            parametro.Add(controle.criaParametros("@DESCRICAO", DbType.String, grade.DESCRICAO));
            parametro.Add(controle.criaParametros("@EM_USO", DbType.Boolean, grade.EM_USO));

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
        /// Verifica se a descrição da COR já existe
        /// </summary>
        /// <param name="codigo">Código da grade</param>
        /// <param name="descricao">Descrição</param>
        /// <returns></returns>
        public int VerificaDuplicidadeDescricao(int codigo, string descricao)
        {
            string sql = "SELECT " +
                         "CODIGO " +
                         "FROM " +
                         "AERMAP_CARTESIANO " +
                         "WHERE " +
                         "CODIGO_DOMINIO = @CODIGO_DOMINIO AND " +
                         "CODIGO != @CODIGO AND " +
                         "DESCRICAO = @DESCRICAO";

            List<DbParameter> parametro = new List<DbParameter>();

            parametro.Add(controle.criaParametros("@CODIGO_DOMINIO", DbType.Int32, codigoDominio));
            parametro.Add(controle.criaParametros("@CODIGO", DbType.Int32, codigo));
            parametro.Add(controle.criaParametros("@DESCRICAO", DbType.String, descricao));            

            return controle.RetornaValor<int>(sql, parametro);
        }

        /// <summary>
        /// Verifica se o código ja esta sendo utilizado(True = existe)
        /// </summary>
        /// <param name="codigo">Código da grade</param>
        /// <returns></returns>
        public bool VerificaExistencia(int codigo)
        {
            string sql = "SELECT " +
                         "CODIGO " +
                         "FROM " +
                         "AERMAP_CARTESIANO " +
                         "WHERE " +
                        $"CODIGO_DOMINIO = {codigoDominio} AND " +
                        $"CODIGO = {codigo} " +
                         "LIMIT 1";

            int countExiste = controle.RetornaValor<int>(sql);
            if (countExiste == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Verifica se o código ja esta sendo utilizado(True = existe)
        /// </summary>
        /// <returns></returns>
        public bool VerificaExistencia()
        {
            string sql = "SELECT " +
                         "CODIGO " +
                         "FROM " +
                         "AERMAP_CARTESIANO " +
                         "WHERE " +
                        $"CODIGO_DOMINIO = {codigoDominio} " +                       
                         "LIMIT 1";

            int countExiste = controle.RetornaValor<int>(sql);
            if (countExiste == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Retorna os dados das cores
        /// </summary>
        /// <param name="codigo">Código da grade</param>
        /// <returns></returns>
        public DataTable RetornaDados(long codigo)
        {
            string sql = "SELECT " +
                         "* " +
                         "FROM " +
                         "AERMAP_CARTESIANO " +
                         "WHERE " +
                        $"CODIGO_DOMINIO = {codigoDominio} AND " +
                        $"CODIGO = {codigo}";           

            return controle.RetornarDataTable(sql);
        }

        /// <summary>
        /// Retorna os dados das cores
        /// </summary>        
        /// <returns></returns>
        public DataTable RetornaDados()
        {
            string sql = "SELECT " +
                         "* " +
                         "FROM " +
                         "AERMAP_CARTESIANO " +
                         "WHERE " +
                        $"CODIGO_DOMINIO = {codigoDominio}";

            return controle.RetornarDataTable(sql);
        }

        /// <summary>
        /// Retorna os registros em uso.
        /// </summary>        
        /// <returns></returns>
        public DataTable RetornaDadosUso()
        {
            string sql = "SELECT " +
                         "* " +
                         "FROM " +
                         "AERMAP_CARTESIANO " +
                         "WHERE " +
                        $"CODIGO_DOMINIO = {codigoDominio} AND " +
                         "EM_USO = TRUE";

            return controle.RetornarDataTable(sql);
        }

        /// <summary>
        /// Excluir grade
        /// </summary>
        /// <param name="codigo">Código da grade</param>  
        public void Excluir(int codigo)
        {
            string sql = "DELETE " +
                         "FROM " +
                         "AERMAP_CARTESIANO " +
                         "WHERE " +
                        $"CODIGO_DOMINIO = {codigoDominio} AND " +
                        $"CODIGO = {codigo}";

            controle.ExecutarComando(sql);
        }

        /// <summary>
        /// Incrementa código da grade
        /// </summary>   
        /// <returns></returns>
        public int Incrementa()
        {
            string sql = "SELECT " +
                         "A.CODIGO + 1 AS DISPONIVEL " +
                         "FROM AERMAP_CARTESIANO A " +

                         "LEFT JOIN AERMAP_CARTESIANO B ON " +
                         "B.CODIGO_DOMINIO = A.CODIGO_DOMINIO AND " +
                         "B.CODIGO = A.CODIGO + 1 " +                                      

                         "WHERE " +
                        $"A.CODIGO_DOMINIO = {codigoDominio} AND " +
                         "B.CODIGO IS NULL AND " +
                         "A.CODIGO < 99999 " +                                   
                         "ORDER BY " +
                         "A.CODIGO DESC " +
                         "LIMIT 1";

            int codigo = controle.RetornaValor<int>(sql);

            if (codigo == 0)
            {
                codigo = BuscarPrimeiroId();

                if (codigo != 1)
                {
                    codigo = 1;
                }
                else
                {
                    codigo = 0;
                }
            }

            return codigo;
        }

        /// <summary>
        /// Insere uma nova grade no banco(Somento o código)
        /// </summary>
        /// <param name="codigo">Código da grade(Incremento)</param> 
        public void Inserir(int codigo)
        {
            string sql = "INSERT INTO " +
                         "AERMAP_CARTESIANO " +
                         "(" +
                         "CODIGO_DOMINIO, " +
                         "CODIGO" +
                         ") " +
                         "VALUES " +
                         "(" +
                        $"{codigoDominio}, " +
                        $"{codigo}" +
                        ")";

            controle.ExecutarComando(sql);
        }

        /// <summary>
        /// Método que retorna a descrição da grade.
        /// </summary>
        /// <param name="codigo">Código da grade</param>      
        /// <returns>Retorna string.</returns>
        public string RetornaDescricao(int codigo)
        {
            string sql = "SELECT " +
                         "DESCRICAO " +
                         "FROM " +
                         "AERMAP_CARTESIANO " +
                         "WHERE " +
                        $"CODIGO_DOMINIO = {codigoDominio} AND " +
                        $"CODIGO = {codigo}";

            return controle.RetornaValor<string>(sql);
        }

        /// <summary>
        /// Verifica existência de registro único.
        /// </summary>
        /// <returns>Retorna true caso tenha apenas um registro</returns>
        public bool VerificaRegistroUnico()
        {
            bool retorno = false;

            string sql = "SELECT " +
                         "CODIGO " +
                         "FROM " +
                         "AERMAP_CARTESIANO " +
                         "WHERE " +
                        $"CODIGO_DOMINIO = {codigoDominio} " +
                         "LIMIT 2";

            DataTable dt = controle.RetornarDataTable(sql);

            if (dt.Rows.Count == 1)
            {
                retorno = true;
            }
            else if (dt.Rows.Count > 1)
            {
                retorno = false;
            }

            return retorno;
        }

        /// <summary>
        /// Verifica se o registro está em uso.
        /// </summary>
        /// <param name="codigo">Código do registro</param>
        /// <returns>retorna true caso esteja em uso</returns>
        public bool VerificaRegistroEmUso(int codigo)
        {
            string sql = "SELECT " +
                         "EM_USO " +
                         "FROM " +
                         "AERMAP_CARTESIANO " +
                         "WHERE " +
                        $"CODIGO_DOMINIO = {codigoDominio} AND " +
                        $"CODIGO = {codigo}";

            return controle.RetornaValor<bool>(sql);
        }

        /// <summary>
        /// Atualizar utilização da fonte emissora.
        /// </summary>       
        /// <param name="lstCodigo">Lista com código das fontes emissoras</param>
        public void AtualizarUso(List<int> lstCodigo)
        {
            string codigo = lstCodigo.Select(I => I.ToString()).Concatenar(",");

            string sql = "UPDATE " +
                         "AERMAP_CARTESIANO " +
                         "SET " +
                         "EM_USO = FALSE " +
                         "WHERE " +
                        $"CODIGO_DOMINIO = {codigoDominio}";

            controle.ExecutarComando(sql);

            sql = "UPDATE " +
                  "AERMAP_CARTESIANO " +
                  "SET " +
                  "EM_USO = TRUE " +
                  "WHERE " +
                 $"CODIGO_DOMINIO = {codigoDominio} AND " +
                 $"CODIGO IN ({codigo})";

            controle.ExecutarComando(sql);
        }

        /// <summary>
        /// Retorna quantidade de grades.
        /// </summary>
        /// <returns></returns>
        public int RetornaQtdGrade()
        {
            string sql = "SELECT " +
                         "COUNT(CODIGO) AS QTD " +
                         "FROM " +
                         "AERMAP_CARTESIANO " +
                         "WHERE " +
                        $"CODIGO_DOMINIO = {codigoDominio}";

            return controle.RetornaValor<int>(sql);
        }

        /// <summary>
        /// Retorna menor coordenada X.
        /// </summary>
        /// <returns>Retorna coordenada</returns>
        public decimal MenorCoordenada_X()
        {
            string sql = "SELECT " +
                         "XINIT " +
                         "FROM " +
                         "AERMAP_CARTESIANO " +
                         "WHERE " +
                         "EM_USO = TRUE " +
                         "ORDER BY " +
                         "XINIT ASC " +
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
                         "YINIT " +
                         "FROM " +
                         "AERMAP_CARTESIANO " +
                         "WHERE " +
                         "EM_USO = TRUE " +
                         "ORDER BY " +
                         "YINIT ASC " +
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
                         "XINIT " +
                         "FROM " +
                         "AERMAP_CARTESIANO " +
                         "WHERE " +
                         "EM_USO = TRUE " +
                         "ORDER BY " +
                         "XINIT DESC " +
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
                         "YINIT " +
                         "FROM " +
                         "AERMAP_CARTESIANO " +
                         "WHERE " +
                         "EM_USO = TRUE " +
                         "ORDER BY " +
                         "YINIT DESC " +
                         "LIMIT 1";

            return controle.RetornaValor<decimal>(sql);
        }

        #endregion
    }
}
