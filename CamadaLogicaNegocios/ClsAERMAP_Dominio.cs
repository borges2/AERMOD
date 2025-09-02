using CamadaAcessoDados;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace CamadaLogicaNegocios
{
    public class ClsAERMAP_Dominio
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
        public ClsAERMAP_Dominio(ConfiguracaoRede configuracaoRede)
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
                         "AERMAP_DOMINIO_MODELAGEM " +
                         "LIMIT 1";

            return controle.RetornaValor<int>(sql);
        }

        /// <summary>
        /// Retorna todos os registros.
        /// </summary>
        /// <returns>Retorna DataTable</returns>
        public DataTable RetornarRegistros()
        {
            string sql = "SELECT " +
                         "CODIGO, " +      
                         "DESCRICAO, " +
                         "DOMINIO_X1, " +
                         "DOMINIO_Y1, " +
                         "DOMINIO_X2, " +
                         "DOMINIO_Y2, " +
                         "ZONA_UTM, " +
                         "ZONA_UTM_LETRA, " +
                         "ZONA_GMT, " +
                         "FONTE_GRADE, " +
                         "GRADE_DOMINIO, " +
                         "EM_USO " +
                         "FROM " +
                         "AERMAP_DOMINIO_MODELAGEM";

            return controle.RetornarDataTable(sql);
        }

        /// <summary>
        /// Retorna o domínio/grid de modelagem em uso.
        /// </summary>
        /// <returns>Retorna DataTable</returns>
        public DataTable RetornarRegistroUso()
        {
            string sql = "SELECT " +
                         "CODIGO, " +     
                         "DESCRICAO, " +
                         "DOMINIO_X1, " +
                         "DOMINIO_Y1, " +
                         "DOMINIO_X2, " +
                         "DOMINIO_Y2, " +
                         "ZONA_UTM, " +
                         "ZONA_UTM_LETRA, " +
                         "ZONA_GMT, " +
                         "FONTE_GRADE, " +
                         "GRADE_DOMINIO, " +
                         "EM_USO " +
                         "FROM " +
                         "AERMAP_DOMINIO_MODELAGEM " +
                         "WHERE " +
                         "EM_USO = TRUE";

            return controle.RetornarDataTable(sql);
        }

        /// <summary>
        /// Incrementar chave primária.
        /// </summary>
        /// <returns>Retorna chave primária</returns>
        public int Incrementar()
        {
            string sql = "SELECT " +
                         "A.CODIGO + 1 AS DISPONIVEL " +
                         "FROM " +
                         "AERMAP_DOMINIO_MODELAGEM A " +

                         "LEFT JOIN AERMAP_DOMINIO_MODELAGEM B ON " +
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
        /// Insere uma nova grade no banco(Somento o código)
        /// </summary>
        /// <param name="codigo">Código da grade(Incremento)</param> 
        public void Inserir(int codigo)
        {
            string sql = "INSERT INTO " +
                         "AERMAP_DOMINIO_MODELAGEM " +
                         "(" +                         
                         "CODIGO" +
                         ") " +
                         "VALUES " +
                         "(" +                        
                        $"{codigo}" +
                        ")";

            controle.ExecutarComando(sql);
        }

        /// <summary>
        /// Inserir/Alterar registro.
        /// </summary>
        /// <param name="dominio">Domínio de modelagem</param>
        /// <returns>Retorna false caso exista erro</returns>
        public int Atualizar(dynamic dominio)
        {
            string sql = string.Empty;
            if (dominio.CODIGO == 0)
            {
                sql = "REPLACE INTO " +
                      "AERMAP_DOMINIO_MODELAGEM " +
                      "(" +
                      "CODIGO, " +      
                      "DESCRICAO, " +
                      "DOMINIO_X1, " +
                      "DOMINIO_Y1, " +
                      "DOMINIO_X2, " +
                      "DOMINIO_Y2, " +
                      "ZONA_UTM, " +
                      "ZONA_UTM_LETRA, " +
                      "ZONA_GMT, " +
                      "FONTE_GRADE, " +
                      "GRADE_DOMINIO" +
                      ") " +
                      "VALUES " +
                      "(" +
                      "@CODIGO, " +        
                      "@DESCRICAO, " +
                      "@DOMINIO_X1, " +
                      "@DOMINIO_Y1, " +
                      "@DOMINIO_X2, " +
                      "@DOMINIO_Y2, " +
                      "@ZONA_UTM, " +
                      "@ZONA_UTM_LETRA, " +
                      "@ZONA_GMT, " +
                      "@FONTE_GRADE, " +
                      "@GRADE_DOMINIO" +
                      ")";

                dominio.CODIGO = Incrementar();
            }
            else
            {
                sql = "UPDATE " +
                      "AERMAP_DOMINIO_MODELAGEM " +
                      "SET " +      
                      "DESCRICAO = @DESCRICAO, " +
                      "DOMINIO_X1 = @DOMINIO_X1, " +
                      "DOMINIO_Y1 = @DOMINIO_Y1, " +
                      "DOMINIO_X2 = @DOMINIO_X2, " +
                      "DOMINIO_Y2 = @DOMINIO_Y2, " +
                      "ZONA_UTM = @ZONA_UTM, " +
                      "ZONA_UTM_LETRA = @ZONA_UTM_LETRA, " +
                      "ZONA_GMT = @ZONA_GMT, " +
                      "FONTE_GRADE = @FONTE_GRADE, " +
                      "GRADE_DOMINIO = @GRADE_DOMINIO " +
                      "WHERE " +
                      "CODIGO = @CODIGO";                      
            }

            List<DbParameter> listParametros = new List<DbParameter>();
            listParametros.Add(controle.criaParametros("@CODIGO", DbType.Int32, dominio.CODIGO));
            listParametros.Add(controle.criaParametros("@DESCRICAO", DbType.String, dominio.DESCRICAO));
            listParametros.Add(controle.criaParametros("@DOMINIO_X1", DbType.Decimal, dominio.DOMINIO_X1));
            listParametros.Add(controle.criaParametros("@DOMINIO_Y1", DbType.Decimal, dominio.DOMINIO_Y1));
            listParametros.Add(controle.criaParametros("@DOMINIO_X2", DbType.Decimal, dominio.DOMINIO_X2));
            listParametros.Add(controle.criaParametros("@DOMINIO_Y2", DbType.Decimal, dominio.DOMINIO_Y2));
            listParametros.Add(controle.criaParametros("@ZONA_UTM", DbType.Int32, dominio.ZONA_UTM));
            listParametros.Add(controle.criaParametros("@ZONA_UTM_LETRA", DbType.String, dominio.ZONA_UTM_LETRA));
            listParametros.Add(controle.criaParametros("@ZONA_GMT", DbType.Int32, dominio.ZONA_GMT));
            listParametros.Add(controle.criaParametros("@FONTE_GRADE", DbType.Decimal, dominio.FONTE_GRADE));
            listParametros.Add(controle.criaParametros("@GRADE_DOMINIO", DbType.Decimal, dominio.GRADE_DOMINIO));

            try
            {
                controle.ExecutaParametros(sql, listParametros);

                return dominio.CODIGO;
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
                         "AERMAP_DOMINIO_MODELAGEM " +
                         "WHERE " +
                        $"CODIGO = {codigo}";

            controle.ExecutarComando(sql);
        }

        /// <summary>
        /// Verifica duplicidade da descrição.
        /// </summary>
        /// <param name="codigo">Código da fonte emissora</param>
        /// <param name="descricao">Descrição do domínio</param>
        /// <returns>Retorna true caso exista</returns>
        public int VerificarDuplicidade(int codigo, string descricao)
        {
            string sql = "SELECT " +
                         "CODIGO " +
                         "FROM " +
                         "AERMAP_DOMINIO_MODELAGEM " +
                         "WHERE " +
                         "CODIGO <> @CODIGO AND " +
                         "DESCRICAO = @DESCRICAO " +                         
                         "LIMIT 1";

            List<DbParameter> listParametros = new List<DbParameter>();
            listParametros.Add(controle.criaParametros("@CODIGO", DbType.Int32, codigo));
            listParametros.Add(controle.criaParametros("@DESCRICAO", DbType.String, descricao));           

            return controle.RetornaValor<int>(sql, listParametros);
        }

        /// <summary>
        /// Atualizar utilização do arquivo.
        /// </summary>
        /// <param name="codigo">Código do arquivo</param>
        /// <param name="emUso">Arquivo em uso</param>
        public void AtualizarUso(int codigo)
        {
            List<DbParameter> parametro = new List<DbParameter>();
            parametro.Add(controle.criaParametros("@CODIGO", DbType.Int32, codigo));

            string sql = "UPDATE " +
                         "AERMAP_DOMINIO_MODELAGEM " +
                         "SET " +
                         "EM_USO = FALSE";                         

            controle.ExecutarComando(sql, parametro);

            sql = "UPDATE " +
                  "AERMAP_DOMINIO_MODELAGEM " +
                  "SET " +
                  "EM_USO = TRUE " +
                  "WHERE " +
                  "CODIGO = @CODIGO";

            controle.ExecutarComando(sql, parametro);
        }

        #region Cartesiano uniforme/Elevação/Discreto/EVALFILE

        /// <summary>
        /// Retorna quantidade de grades.
        /// </summary>
        /// <param name="codigoDominio">Código do domínio</param>
        /// <returns>Retorna quantidade de grades</returns>
        public int RetornaQtdGrade(int codigoDominio)
        {
            ClsAERMAP_Cartesiano classeCartesiano = new ClsAERMAP_Cartesiano(configuracaoRede, codigoDominio);
            ClsAERMAP_CartesianoRede classeElevacao = new ClsAERMAP_CartesianoRede(configuracaoRede, codigoDominio);
            ClsAERMAP_CartesianoDiscreto classeCartesianoDisc = new ClsAERMAP_CartesianoDiscreto(configuracaoRede, codigoDominio);
            ClsAERMAP_EVALFILE classeEVALFILE = new ClsAERMAP_EVALFILE(configuracaoRede, codigoDominio);

            int qtd = classeCartesiano.RetornaQtdGrade();
            qtd += classeElevacao.RetornaQtdGrade();
            qtd += classeCartesianoDisc.RetornaQtdGrade();
            qtd += classeEVALFILE.RetornaQtdGrade();

            return qtd;
        }

        /// <summary>
        /// Retorna menor coordenada do eixo X.
        /// </summary>
        /// <param name="codigoDominio">Código do domínio</param>
        /// <returns>Retorna decimal</returns>
        public decimal MenorCoordenadaGrade_X(int codigoDominio)
        {
            ClsAERMAP_Cartesiano classeCartesiano = new ClsAERMAP_Cartesiano(configuracaoRede, codigoDominio);
            ClsAERMAP_CartesianoRede classeElevacao = new ClsAERMAP_CartesianoRede(configuracaoRede, codigoDominio);
            ClsAERMAP_CartesianoDiscreto classeCartesianoDisc = new ClsAERMAP_CartesianoDiscreto(configuracaoRede, codigoDominio);
            ClsAERMAP_EVALFILE classeEVALFILE = new ClsAERMAP_EVALFILE(configuracaoRede, codigoDominio);

            decimal coordenada1 = classeCartesiano.MenorCoordenada_X();
            decimal coordenada2 = classeElevacao.MenorCoordenada_X();
            decimal coordenada3 = classeCartesianoDisc.MenorCoordenada_X();
            decimal coordenada4 = classeEVALFILE.MenorCoordenada_X();

            List<decimal> lista = new List<decimal>()
            {
                coordenada1,
                coordenada2,
                coordenada3,
                coordenada4
            };

            return lista.Where(I => I > 0).Min();
        }

        /// <summary>
        /// Retorna menor coordenada do eixo Y.
        /// </summary>
        /// <param name="codigoDominio">Código do domínio</param>
        /// <returns>Retorna decimal</returns>
        public decimal MenorCoordenadaGrade_Y(int codigoDominio)
        {
            ClsAERMAP_Cartesiano classeCartesiano = new ClsAERMAP_Cartesiano(configuracaoRede, codigoDominio);
            ClsAERMAP_CartesianoRede classeElevacao = new ClsAERMAP_CartesianoRede(configuracaoRede, codigoDominio);
            ClsAERMAP_CartesianoDiscreto classeCartesianoDisc = new ClsAERMAP_CartesianoDiscreto(configuracaoRede, codigoDominio);
            ClsAERMAP_EVALFILE classeEVALFILE = new ClsAERMAP_EVALFILE(configuracaoRede, codigoDominio);

            decimal coordenada1 = classeCartesiano.MenorCoordenada_Y();
            decimal coordenada2 = classeElevacao.MenorCoordenada_Y();
            decimal coordenada3 = classeCartesianoDisc.MenorCoordenada_Y();
            decimal coordenada4 = classeEVALFILE.MenorCoordenada_Y();

            List<decimal> lista = new List<decimal>()
            {
                coordenada1,
                coordenada2,
                coordenada3,
                coordenada4
            };

            return lista.Where(I => I > 0).Min();
        }

        /// <summary>
        /// Retorna maior coordenada do eixo X.
        /// </summary>
        /// <param name="codigoDominio">Código do domínio</param>
        /// <returns>Retorna decimal</returns>
        public decimal MaiorCoordenadaGrade_X(int codigoDominio)
        {
            ClsAERMAP_Cartesiano classeCartesiano = new ClsAERMAP_Cartesiano(configuracaoRede, codigoDominio);
            ClsAERMAP_CartesianoRede classeElevacao = new ClsAERMAP_CartesianoRede(configuracaoRede, codigoDominio);
            ClsAERMAP_CartesianoDiscreto classeCartesianoDisc = new ClsAERMAP_CartesianoDiscreto(configuracaoRede, codigoDominio);
            ClsAERMAP_EVALFILE classeEVALFILE = new ClsAERMAP_EVALFILE(configuracaoRede, codigoDominio);

            decimal coordenada1 = classeCartesiano.MaiorCoordenada_X();
            decimal coordenada2 = classeElevacao.MaiorCoordenada_X();
            decimal coordenada3 = classeCartesianoDisc.MaiorCoordenada_X();
            decimal coordenada4 = classeEVALFILE.MaiorCoordenada_X();

            List<decimal> lista = new List<decimal>()
            {
                coordenada1,
                coordenada2,
                coordenada3,
                coordenada4
            };

            return lista.Where(I => I > 0).Max();
        }

        /// <summary>
        /// Retorna maior coordenada do eixo Y.
        /// </summary>
        /// <param name="codigoDominio">Código do domínio</param>
        /// <returns>Retorna decimal</returns>
        public decimal MaiorCoordenadaGrade_Y(int codigoDominio)
        {
            ClsAERMAP_Cartesiano classeCartesiano = new ClsAERMAP_Cartesiano(configuracaoRede, codigoDominio);
            ClsAERMAP_CartesianoRede classeElevacao = new ClsAERMAP_CartesianoRede(configuracaoRede, codigoDominio);
            ClsAERMAP_CartesianoDiscreto classeCartesianoDisc = new ClsAERMAP_CartesianoDiscreto(configuracaoRede, codigoDominio);
            ClsAERMAP_EVALFILE classeEVALFILE = new ClsAERMAP_EVALFILE(configuracaoRede, codigoDominio);

            decimal coordenada1 = classeCartesiano.MaiorCoordenada_Y();
            decimal coordenada2 = classeElevacao.MaiorCoordenada_Y();
            decimal coordenada3 = classeCartesianoDisc.MaiorCoordenada_Y();
            decimal coordenada4 = classeEVALFILE.MaiorCoordenada_Y();

            List<decimal> lista = new List<decimal>()
            {
                coordenada1,
                coordenada2,
                coordenada3,
                coordenada4
            };

            return lista.Where(I => I > 0).Max();
        }

        #endregion

        #endregion
    }
}
