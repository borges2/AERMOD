using AERMOD.LIB.Componentes;
using AERMOD.LIB.Componentes.GridView;
using AERMOD.LIB.Desenvolvimento;
using AERMOD.LIB.Forms;
using CamadaAcessoDados;
using CamadaLogicaNegocios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AERMOD.CamadaApresentacao.PRINCIPAL
{
    public partial class FrmParametrosAERMOD : Form
    {
        #region Instâncias e Propriedades

        /// <summary>
        /// Camada de acesso aos dados.
        /// </summary>
        ClsDados clsDados = null;

        /// <summary>
        /// Camada de acesso aos dados.
        /// </summary>
        ClsDados classeDados
        {
            get
            {
                if (clsDados == null)
                {
                    clsDados = new ClsDados(Base.ConfiguracaoRede);
                }

                return clsDados;
            }
        }

        /// <summary>
        /// Utilizado para help.
        /// </summary>
        ClsHelp clsHelp = null;

        /// <summary>
        /// Utilizado para help.
        /// </summary>
        ClsHelp classeHelp
        {
            get
            {
                if (clsHelp == null)
                {
                    clsHelp = new ClsHelp(Base.ConfiguracaoRede);
                }

                return clsHelp;
            }
        }

        ClsFonteAERMAP clsFonteAERMAP = null;

        /// <summary>
        /// Get a classe de parametro
        /// </summary>
        ClsFonteAERMAP classeFonte
        {
            get
            {
                if (clsFonteAERMAP == null)
                {
                    clsFonteAERMAP = new ClsFonteAERMAP(Base.ConfiguracaoRede);
                }

                return clsFonteAERMAP;
            }
        }

        ClsAERMET clsAERMET = null;

        /// <summary>
        /// Get classe de negócios ClsAERMAP
        /// </summary>
        ClsAERMET classeAERMET
        {
            get
            {
                if (clsAERMET == null)
                {
                    clsAERMET = new ClsAERMET(Base.ConfiguracaoRede);
                }

                return clsAERMET;
            }
        }

        ClsAERMOD clsAERMOD = null;

        /// <summary>
        /// Get a classe AERMOD.
        /// </summary>
        ClsAERMOD classeAERMOD
        {
            get
            {
                if (clsAERMOD == null)
                {
                    clsAERMOD = new ClsAERMOD(Base.ConfiguracaoRede);
                }

                return clsAERMOD;
            }
        }

        /// <summary>
        /// Coordenadas da fonte do tipo área polígono.
        /// </summary>
        List<Tuple<decimal, decimal>> lstCoordenadas { get; set; }

        /// <summary>
        /// Código do período.
        /// </summary>
        int codigoPeriodo;

        /// <summary>
        /// Código do poluente.
        /// </summary>
        int codigoPoluente;

        #endregion

        #region Construtor

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="codigoPeriodo">Código do período</param>
        /// <param name="codigoPoluente">Código do poluente</param>
        public FrmParametrosAERMOD(int codigoPeriodo, int codigoPoluente)
        {
            InitializeComponent();

            this.Icon = Properties.Resources.form2.ConvertImageToIcon();

            this.codigoPeriodo = codigoPeriodo;
            this.codigoPoluente = codigoPoluente;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Carregar fontes emissoras.
        /// </summary>
        private void CarregarFontes()
        {
            FrmLoading frmLoading = new FrmLoading(this);

            Thread thr = new Thread(delegate ()
            {
                DataTable dt = classeFonte.RetornarRegistros();

                frmLoading.Style = ProgressBarStyle.Blocks;
                frmLoading.Maximum = dt.Rows.Count;

                CrossThreadOperation.Invoke(this, delegate
                {
                    int count = 0;
                    foreach (DataRow item in dt.Rows)
                    {
                        count++;
                        frmLoading.AtualizarStatus(count);

                        dgvFonte.Rows.Add(false, 
                                          count.ToString().PadLeft(2, '0'),
                                          item["DESCRICAO"],
                                          RetornaTipoFonte(item["TIPO"]), 
                                          item["X"], 
                                          item["Y"], 
                                          item["CODIGO"]);
                    }
                });
            });
            thr.Start();

            frmLoading.thread = thr;
            frmLoading.PermiteAbortarThread = false;
            frmLoading.Style = ProgressBarStyle.Marquee;
            frmLoading.Maximum = 0;
            frmLoading.Texto = "Carregando registro(s)...";
            frmLoading.ShowDialogFade(this);

            if (dgvFonte.Rows.Count > 0)
            {
                dgvFonte.Rows[0].Cells[0].Selected = true;
                VisualizarParametro();
            }

            dgvFonte.Focus();
        }

        /// <summary>
        /// Retorna tipo da fonte.
        /// </summary>
        /// <param name="row">Tipo da fonte</param>
        /// <returns></returns>
        private string RetornaTipoFonte(object row)
        {
            string descricao = string.Empty;
            if (row != null && row.GetType() != typeof(DBNull))
            {
                descricao = ((TipoFonte)Convert.ToInt32(row)).GetEnumDescription();
            }

            return descricao;
        }

        /// <summary>
        /// Abrir tela de ajuda.
        /// </summary>
        private void AbrirAjuda()
        {
            string caminho = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "Help");
            Help.ShowHelp(this, $"{caminho}\\AERMOD.chm", HelpNavigator.TopicId, "50");
        }

        /// <summary>
        /// Alterar parâmetro selecionado.
        /// </summary>
        private void AlterarParametro()
        {
            if (dgvFonte.CurrentRow != null)
            {
                HabilitarCampos(true);

                if (tabControlParametros.SelectedTab == tabPagePonto)
                {
                    tbxQS.Focus();
                }
                else if (tabControlParametros.SelectedTab == tabPageArea)
                {
                    tbxAremis.Focus();
                }
                else if (tabControlParametros.SelectedTab == tabPageAreaPoligono)
                {
                    tbxAremisPoli.Focus();                    
                }
                else if (tabControlParametros.SelectedTab == tabPageAreaCirculo)
                {
                    tbxAremisCirc.Focus();
                }
                else if (tabControlParametros.SelectedTab == tabPageLinha)
                {
                    tbxAremisLinha.Focus();
                }
                else if (tabControlParametros.SelectedTab == tabPagePoco)
                {
                    tbxOpemisPoco.Focus();
                }
                else if (tabControlParametros.SelectedTab == tabPageVolume)
                {
                    tbxVlemisVolume.Focus();
                }
            }
        }

        /// <summary>
        /// Salvar parâmetros (ponto/area).
        /// </summary>
        private void SalvarParametro()
        {
            if (dgvFonte.CurrentRow != null)
            {
                int codigo = Convert.ToInt32(dgvFonte.CurrentRow.Cells["CODIGO"].Value);

                if (tabControlParametros.SelectedTab == tabPagePonto)
                {
                    #region Ponto

                    //if (Convert.ToDecimal(tbxQS.Text) == 0)
                    //{
                    //    MessageBox.Show(this, $"QS - {classeHelp.BuscarMensagem(35)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    tbxQS.Focus();
                    //    return;
                    //}

                    if (Convert.ToDecimal(tbxHS.Text) == 0)
                    {
                        MessageBox.Show(this, $"HS - {classeHelp.BuscarMensagem(36)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tbxHS.Focus();
                        return;
                    }

                    //Se for inserido um valor de 0, 0 para a temperatura de saída,
                    //o AERMOD ajustará a temperatura de saída para cada hora para
                    //corresponder à temperatura ambiente.

                    //if (Convert.ToDecimal(tbxTS.Text) == 0)
                    //{
                    //    MessageBox.Show(this, $"TS - {classeHelp.BuscarMensagem(37)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    tbxTS.Focus();
                    //    return;
                    //}

                    if (Convert.ToDecimal(tbxVS.Text) == 0)
                    {
                        MessageBox.Show(this, $"VS - {classeHelp.BuscarMensagem(38)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tbxVS.Focus();
                        return;
                    }

                    if (Convert.ToDecimal(tbxDS.Text) == 0)
                    {
                        MessageBox.Show(this, $"DS - {classeHelp.BuscarMensagem(39)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tbxDS.Focus();
                        return;
                    }

                    dynamic dados = new ExpandoObject();
                    dados.CODIGO_FONTE = codigo;
                    dados.CODIGO_PERIODO = codigoPeriodo;
                    dados.CODIGO_POLUENTE = codigoPoluente;
                    dados.TAXA_EMISSAO = Convert.ToDecimal(tbxQS.Text);
                    dados.ALTURA_CHAMINE = Convert.ToDecimal(tbxHS.Text);
                    dados.TEMPERATURA_SAIDA = Convert.ToDecimal(tbxTS.Text);
                    dados.VELOCIDADE_SAIDA = Convert.ToDecimal(tbxVS.Text);
                    dados.DIAMETRO_CHAMINE = Convert.ToDecimal(tbxDS.Text);

                    Thread thr = new Thread(delegate () { classeAERMOD.AtualizarParametroPonto(dados); });
                    thr.Start();

                    #endregion
                }
                else if (tabControlParametros.SelectedTab == tabPageArea)
                {
                    #region Área

                    //if (Convert.ToDecimal(tbxAremis.Text) == 0)
                    //{
                    //    MessageBox.Show(this, $"Aremis - {classeHelp.BuscarMensagem(35)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    tbxAremis.Focus();
                    //    return;
                    //}

                    if (Convert.ToDecimal(tbxRelhgt.Text) == 0)
                    {
                        MessageBox.Show(this, $"Relhgt - {classeHelp.BuscarMensagem(40)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tbxRelhgt.Focus();
                        return;
                    }

                    if (Convert.ToDecimal(tbxXinit.Text) == 0)
                    {
                        MessageBox.Show(this, $"Xinit - {classeHelp.BuscarMensagem(41)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tbxXinit.Focus();
                        return;
                    }

                    decimal comprimentoY = Convert.ToDecimal(tbxYinit.Text);
                    decimal angulo = Convert.ToDecimal(tbxAngulo.Text);
                    decimal dimensaoVertical = Convert.ToDecimal(tbxSzinit.Text);

                    dynamic dados = new ExpandoObject();
                    dados.CODIGO_FONTE = codigo;
                    dados.CODIGO_PERIODO = codigoPeriodo;
                    dados.CODIGO_POLUENTE = codigoPoluente;
                    dados.TAXA_EMISSAO = Convert.ToDecimal(tbxAremis.Text);
                    dados.ALTURA_LANCAMENTO = Convert.ToDecimal(tbxRelhgt.Text);
                    dados.COMPRIMENTO_X = Convert.ToDecimal(tbxXinit.Text);
                    dados.COMPRIMENTO_Y = comprimentoY;
                    dados.ANGULO = angulo;
                    dados.DIMENSAO_VERTICAL = dimensaoVertical;

                    Thread thr = new Thread(delegate () { classeAERMOD.AtualizarParametroArea(dados); });
                    thr.Start();

                    #endregion
                }
                else if (tabControlParametros.SelectedTab == tabPageAreaPoligono)
                {
                    #region Área polígono

                    //if (Convert.ToDecimal(tbxAremisPoli.Text) == 0)
                    //{
                    //    MessageBox.Show(this, $"Aremis - {classeHelp.BuscarMensagem(35)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    tbxAremisPoli.Focus();
                    //    return;
                    //}

                    if (Convert.ToDecimal(tbxRelhgtPoli.Text) == 0)
                    {
                        MessageBox.Show(this, $"Relhgt - {classeHelp.BuscarMensagem(40)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tbxRelhgtPoli.Focus();
                        return;
                    }

                    int numeroVertices = tbxNvertsPoli.Text.ValidarValor<int>(0);

                    if (numeroVertices < 3 || numeroVertices > 20)
                    {
                        MessageBox.Show(this, $"Nverts - {classeHelp.BuscarMensagem(42)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tbxNvertsPoli.Focus();
                        return;
                    }

                    if (lstCoordenadas.Count == 0)
                    {
                        DataTable dtCoordenadas = classeAERMOD.RetornaParametroAreaPolyCoordenadas(codigo, codigoPeriodo, codigoPoluente);
                        foreach (DataRow linha in dtCoordenadas.Rows)
                        {
                            lstCoordenadas.Add(Tuple.Create(Convert.ToDecimal(linha["X"]), Convert.ToDecimal(linha["Y"])));
                        }
                    }

                    if (lstCoordenadas.Count != numeroVertices)
                    {
                        MessageBox.Show(this, $"Coordenadas - {classeHelp.BuscarMensagem(43)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        AbrirCoordenadasAreaPoly();
                        return;
                    }

                    decimal coordenadaX = Convert.ToDecimal(dgvFonte.CurrentRow.Cells["X"].Value);
                    decimal coordenadaY = Convert.ToDecimal(dgvFonte.CurrentRow.Cells["Y"].Value);

                    if (lstCoordenadas.First().Item1 != coordenadaX || lstCoordenadas.First().Item2 != coordenadaY)
                    {
                        MessageBox.Show(this, $"Coordenadas - {classeHelp.BuscarMensagem(45)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        AbrirCoordenadasAreaPoly();
                        return;
                    }

                    decimal dimensaoVertical = Convert.ToDecimal(tbxSzinitPoli.Text);

                    dynamic dados = new ExpandoObject();
                    dados.CODIGO_FONTE = codigo;
                    dados.CODIGO_PERIODO = codigoPeriodo;
                    dados.CODIGO_POLUENTE = codigoPoluente;
                    dados.TAXA_EMISSAO = Convert.ToDecimal(tbxAremisPoli.Text);
                    dados.ALTURA_LANCAMENTO = Convert.ToDecimal(tbxRelhgtPoli.Text);
                    dados.NUMERO_VERTICES = Convert.ToInt32(tbxNvertsPoli.Text);
                    dados.DIMENSAO_VERTICAL = dimensaoVertical;

                    Thread thr = new Thread(delegate ()
                    {
                        DbTransaction transacao = classeDados.AbrirTransaction();

                        classeAERMOD.AtualizarParametroAreaPoly(dados, transacao);
                        if (classeAERMOD.AtualizarParametroAreaPolyCoordenadas(codigo, codigoPeriodo, codigoPoluente, lstCoordenadas, transacao))
                        {
                            classeDados.ExecutarTransaction(true, transacao);
                        }
                        else
                        {
                            classeDados.ExecutarTransaction(false, transacao);
                        }
                    });
                    thr.Start();

                    #endregion
                }
                else if (tabControlParametros.SelectedTab == tabPageAreaCirculo)
                {
                    #region Área círculo

                    //if (Convert.ToDecimal(tbxAremisCirc.Text) == 0)
                    //{
                    //    MessageBox.Show(this, $"Aremis - {classeHelp.BuscarMensagem(35)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //    tbxAremisCirc.Focus();
                    //    return;
                    //}

                    if (Convert.ToDecimal(tbxRelhgtCirc.Text) == 0)
                    {
                        MessageBox.Show(this, $"Relhgt - {classeHelp.BuscarMensagem(40)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tbxRelhgtCirc.Focus();
                        return;
                    }

                    if (Convert.ToDecimal(tbxRadiusCirc.Text) == 0)
                    {
                        MessageBox.Show(this, $"Radius - {classeHelp.BuscarMensagem(44)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tbxRadiusCirc.Focus();
                        return;
                    }

                    int numeroVertices = Convert.ToInt32(tbxNvertsCirc.Text);
                    decimal dimensaoVertical = Convert.ToDecimal(tbxSzinitCirc.Text);

                    dynamic dados = new ExpandoObject();
                    dados.CODIGO_FONTE = codigo;
                    dados.CODIGO_PERIODO = codigoPeriodo;
                    dados.CODIGO_POLUENTE = codigoPoluente;
                    dados.TAXA_EMISSAO = Convert.ToDecimal(tbxAremisCirc.Text);
                    dados.ALTURA_LANCAMENTO = Convert.ToDecimal(tbxRelhgtCirc.Text);
                    dados.RAIO = Convert.ToDecimal(tbxRadiusCirc.Text);
                    dados.NUMERO_VERTICES = numeroVertices;
                    dados.DIMENSAO_VERTICAL = dimensaoVertical;

                    Thread thr = new Thread(delegate () { classeAERMOD.AtualizarParametroAreaCirc(dados); });
                    thr.Start();

                    #endregion
                }
                else if (tabControlParametros.SelectedTab == tabPageLinha)
                {
                    #region Linha

                    if (Convert.ToDecimal(tbxRelhgtLinha.Text) == 0)
                    {
                        MessageBox.Show(this, $"Relhgt - {classeHelp.BuscarMensagem(40)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tbxRelhgtLinha.Focus();
                        return;
                    }

                    if (Convert.ToDecimal(tbxLarguraLinha.Text) == 0)
                    {
                        MessageBox.Show(this, $"Largura - {classeHelp.BuscarMensagem(73)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tbxLarguraLinha.Focus();
                        return;
                    }                    

                    dynamic dados = new ExpandoObject();
                    dados.CODIGO_FONTE = codigo;
                    dados.CODIGO_PERIODO = codigoPeriodo;
                    dados.CODIGO_POLUENTE = codigoPoluente;
                    dados.TAXA_EMISSAO = Convert.ToDecimal(tbxAremisLinha.Text);
                    dados.ALTURA_LANCAMENTO = Convert.ToDecimal(tbxRelhgtLinha.Text);
                    dados.LARGURA = Convert.ToDecimal(tbxLarguraLinha.Text);
                    dados.DIMENSAO_VERTICAL = Convert.ToDecimal(tbxSzinitLinha.Text);

                    Thread thr = new Thread(delegate () { classeAERMOD.AtualizarParametroLinha(dados); });
                    thr.Start();

                    #endregion
                }
                else if (tabControlParametros.SelectedTab == tabPagePoco)
                {
                    #region Poço

                    if (Convert.ToDecimal(tbxRelhgtPoco.Text) == 0)
                    {
                        MessageBox.Show(this, $"Relhgt - {classeHelp.BuscarMensagem(40)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tbxRelhgtPoco.Focus();
                        return;
                    }

                    if (Convert.ToDecimal(tbxXinitPoco.Text) == 0)
                    {
                        MessageBox.Show(this, $"Xinit - {classeHelp.BuscarMensagem(41)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tbxXinitPoco.Focus();
                        return;
                    }

                    if (Convert.ToDecimal(tbxYinitPoco.Text) == 0)
                    {
                        MessageBox.Show(this, $"Yinit - {classeHelp.BuscarMensagem(74)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tbxYinitPoco.Focus();
                        return;
                    }

                    if (Convert.ToDecimal(tbxPitvolPoco.Text) == 0)
                    {
                        MessageBox.Show(this, $"Pitvol - {classeHelp.BuscarMensagem(75)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tbxPitvolPoco.Focus();
                        return;
                    }

                    dynamic dados = new ExpandoObject();
                    dados.CODIGO_FONTE = codigo;
                    dados.CODIGO_PERIODO = codigoPeriodo;
                    dados.CODIGO_POLUENTE = codigoPoluente;
                    dados.TAXA_EMISSAO = Convert.ToDecimal(tbxOpemisPoco.Text);
                    dados.ALTURA_LANCAMENTO = Convert.ToDecimal(tbxRelhgtPoco.Text);
                    dados.COMPRIMENTO_X = Convert.ToDecimal(tbxXinitPoco.Text);
                    dados.COMPRIMENTO_Y = Convert.ToDecimal(tbxYinitPoco.Text);
                    dados.VOLUME = Convert.ToDecimal(tbxPitvolPoco.Text);
                    dados.ANGULO = Convert.ToDecimal(tbxAnguloPoco.Text);

                    Thread thr = new Thread(delegate () { classeAERMOD.AtualizarParametroPoco(dados); });
                    thr.Start();

                    #endregion
                }
                else if (tabControlParametros.SelectedTab == tabPageVolume)
                {
                    #region Volume

                    if (Convert.ToDecimal(tbxRelhgtVolume.Text) == 0)
                    {
                        MessageBox.Show(this, $"Relhgt - {classeHelp.BuscarMensagem(40)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tbxRelhgtVolume.Focus();
                        return;
                    }

                    if (Convert.ToDecimal(tbxSyinitVolume.Text) == 0)
                    {
                        MessageBox.Show(this, $"Syinit - {classeHelp.BuscarMensagem(76)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tbxSyinitVolume.Focus();
                        return;
                    }

                    if (Convert.ToDecimal(tbxSzinitVolume.Text) == 0)
                    {
                        MessageBox.Show(this, $"Szinit - {classeHelp.BuscarMensagem(77)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tbxSzinitVolume.Focus();
                        return;
                    }                    

                    dynamic dados = new ExpandoObject();
                    dados.CODIGO_FONTE = codigo;
                    dados.CODIGO_PERIODO = codigoPeriodo;
                    dados.CODIGO_POLUENTE = codigoPoluente;
                    dados.TAXA_EMISSAO = Convert.ToDecimal(tbxVlemisVolume.Text);
                    dados.ALTURA_LANCAMENTO = Convert.ToDecimal(tbxRelhgtVolume.Text);
                    dados.DIMENSAO_LATERAL = Convert.ToDecimal(tbxSyinitVolume.Text);
                    dados.DIMENSAO_VERTICAL = Convert.ToDecimal(tbxSzinitVolume.Text);                    

                    Thread thr = new Thread(delegate () { classeAERMOD.AtualizarParametroVolume(dados); });
                    thr.Start();

                    #endregion
                }

                dgvFonte.Focus();
            }
        }

        /// <summary>
        /// Sair da edição ou fechar o cadastro. 
        /// </summary>
        private bool SairForm(bool fechando = false)
        {
            if (dgvFonte.Rows.Count > 0 && tbxQS.Enabled)
            {
                dgvFonte.Focus();
                return false;
            }
            else if (fechando == false)
            {
                this.Close();
            }

            return true;
        }

        /// <summary>
        /// Visualizar parâmetros.
        /// </summary>
        private void VisualizarParametro()
        {
            if (dgvFonte.CurrentRow != null)
            {
                TipoFonte tipoFonte = dgvFonte.CurrentRow.Cells["TIPO"].Value.ToString().GetEnumFromDescription<TipoFonte>();
                int codigo = Convert.ToInt32(dgvFonte.CurrentRow.Cells["CODIGO"].Value);

                switch (tipoFonte)
                {
                    case TipoFonte.PONTO:
                        {
                            #region Ponto

                            dgvFonte.Tag = tabPagePonto.Name;
                            tabControlParametros.SelectedIndex = 0;                            
                            dgvFonte.Focus();

                            DataTable dtParametro = classeAERMOD.RetornaParametroPonto(codigo, codigoPeriodo, codigoPoluente);

                            tbxQS.ResetText();
                            tbxHS.ResetText();
                            tbxTS.ResetText();
                            tbxVS.ResetText();
                            tbxDS.ResetText();

                            bool salvar = false;

                            if (dtParametro.Rows.Count == 0)
                            {
                                int codigoPeriodoAnterior = classeAERMET.BuscarIdAnterior(codigoPeriodo);

                                dtParametro = classeAERMOD.RetornaParametroPonto(codigo, codigoPeriodoAnterior, codigoPoluente);

                                if (dtParametro.Rows.Count == 0)
                                {
                                    int primeiroCodigoPeriodo = classeAERMET.BuscarPrimeiroId(null);
                                    int primeiroCodigoPoluente = classeAERMOD.BuscarPrimeiroId(null);

                                    dtParametro = classeAERMOD.RetornaParametroPonto(codigo, primeiroCodigoPeriodo, primeiroCodigoPoluente);
                                }

                                salvar = true;
                            }                            

                            if (dtParametro.Rows.Count > 0)
                            {
                                tbxQS.Text = dtParametro.Rows[0]["TAXA_EMISSAO"].ValidarValor<decimal>(0).ToString();
                                tbxHS.Text = dtParametro.Rows[0]["ALTURA_CHAMINE"].ValidarValor<decimal>(0).ToString();
                                tbxTS.Text = dtParametro.Rows[0]["TEMPERATURA_SAIDA"].ValidarValor<decimal>(0).ToString();
                                tbxVS.Text = dtParametro.Rows[0]["VELOCIDADE_SAIDA"].ValidarValor<decimal>(0).ToString();
                                tbxDS.Text = dtParametro.Rows[0]["DIAMETRO_CHAMINE"].ValidarValor<decimal>(0).ToString();

                                if (salvar)
                                {
                                    SalvarParametro();
                                }
                            }

                            #endregion
                        }
                        break;
                    case TipoFonte.AREA:
                        {
                            #region Área

                            dgvFonte.Tag = tabPageArea.Name;
                            tabControlParametros.SelectedIndex = 1;                            
                            dgvFonte.Focus();

                            DataTable dtParametro = classeAERMOD.RetornaParametroArea(codigo, codigoPeriodo, codigoPoluente);

                            tbxAremis.ResetText();
                            tbxRelhgt.ResetText();
                            tbxXinit.ResetText();
                            tbxYinit.ResetText();
                            tbxAngulo.ResetText();
                            tbxSzinit.ResetText();

                            bool salvar = false;

                            if (dtParametro.Rows.Count == 0)
                            {
                                int codigoPeriodoAnterior = classeAERMET.BuscarIdAnterior(codigoPeriodo);

                                dtParametro = classeAERMOD.RetornaParametroArea(codigo, codigoPeriodoAnterior, codigoPoluente);

                                if (dtParametro.Rows.Count == 0)
                                {
                                    int primeiroCodigoPeriodo = classeAERMET.BuscarPrimeiroId(null);
                                    int primeiroCodigoPoluente = classeAERMOD.BuscarPrimeiroId(null);

                                    dtParametro = classeAERMOD.RetornaParametroArea(codigo, primeiroCodigoPeriodo, primeiroCodigoPoluente);
                                }

                                salvar = true;
                            }

                            if (dtParametro.Rows.Count > 0)
                            {
                                tbxAremis.Text = dtParametro.Rows[0]["TAXA_EMISSAO"].ValidarValor<decimal>(0).ToString();
                                tbxRelhgt.Text = dtParametro.Rows[0]["ALTURA_LANCAMENTO"].ValidarValor<decimal>(0).ToString();
                                tbxXinit.Text = dtParametro.Rows[0]["COMPRIMENTO_X"].ValidarValor<decimal>(0).ToString();
                                tbxYinit.Text = dtParametro.Rows[0]["COMPRIMENTO_Y"].ValidarValor<decimal>(0).ToString();
                                tbxAngulo.Text = dtParametro.Rows[0]["ANGULO"].ValidarValor<decimal>(0).ToString();
                                tbxSzinit.Text = dtParametro.Rows[0]["DIMENSAO_VERTICAL"].ValidarValor<decimal>(0).ToString();

                                if (salvar)
                                {
                                    SalvarParametro();
                                }
                            }                            

                            #endregion
                        }
                        break;
                    case TipoFonte.AREAPOLY:
                        {
                            #region Área polígono

                            dgvFonte.Tag = tabPageAreaPoligono.Name;
                            tabControlParametros.SelectedIndex = 2;                            
                            dgvFonte.Focus();

                            DataTable dtParametro = classeAERMOD.RetornaParametroAreaPoly(codigo, codigoPeriodo, codigoPoluente);

                            tbxAremisPoli.ResetText();
                            tbxRelhgtPoli.ResetText();
                            tbxNvertsPoli.ResetText();
                            tbxSzinitPoli.ResetText();

                            bool salvar = false;

                            if (dtParametro.Rows.Count == 0)
                            {
                                int codigoPeriodoAnterior = classeAERMET.BuscarIdAnterior(codigoPeriodo);

                                dtParametro = classeAERMOD.RetornaParametroAreaPoly(codigo, codigoPeriodoAnterior, codigoPoluente);

                                if (dtParametro.Rows.Count == 0)
                                {
                                    int primeiroCodigoPeriodo = classeAERMET.BuscarPrimeiroId(null);
                                    int primeiroCodigoPoluente = classeAERMOD.BuscarPrimeiroId(null);

                                    dtParametro = classeAERMOD.RetornaParametroAreaPoly(codigo, primeiroCodigoPeriodo, primeiroCodigoPoluente);
                                }

                                salvar = true;
                            }

                            lstCoordenadas = new List<Tuple<decimal, decimal>>();

                            if (dtParametro.Rows.Count > 0)
                            {
                                tbxAremisPoli.Text = dtParametro.Rows[0]["TAXA_EMISSAO"].ValidarValor<decimal>(0).ToString();
                                tbxRelhgtPoli.Text = dtParametro.Rows[0]["ALTURA_LANCAMENTO"].ValidarValor<decimal>(0).ToString();
                                tbxNvertsPoli.Text = dtParametro.Rows[0]["NUMERO_VERTICES"].ValidarValor<int>(0).ToString();
                                tbxSzinitPoli.Text = dtParametro.Rows[0]["DIMENSAO_VERTICAL"].ValidarValor<decimal>(0).ToString();

                                if (salvar)
                                {
                                    SalvarParametro();
                                }
                            }                            

                            #endregion
                        }
                        break;
                    case TipoFonte.AREACIRC:
                        {
                            #region Área círculo

                            dgvFonte.Tag = tabPageAreaCirculo.Name;
                            tabControlParametros.SelectedIndex = 3;                           
                            dgvFonte.Focus();

                            DataTable dtParametro = classeAERMOD.RetornaParametroAreaCirc(codigo, codigoPeriodo, codigoPoluente);

                            tbxAremisCirc.ResetText();
                            tbxRelhgtCirc.ResetText();
                            tbxRadiusCirc.ResetText();
                            tbxNvertsCirc.ResetText();
                            tbxSzinitCirc.ResetText();

                            bool salvar = false;

                            if (dtParametro.Rows.Count == 0)
                            {
                                int codigoPeriodoAnterior = classeAERMET.BuscarIdAnterior(codigoPeriodo);

                                dtParametro = classeAERMOD.RetornaParametroAreaCirc(codigo, codigoPeriodoAnterior, codigoPoluente);

                                if (dtParametro.Rows.Count == 0)
                                {
                                    int primeiroCodigoPeriodo = classeAERMET.BuscarPrimeiroId(null);
                                    int primeiroCodigoPoluente = classeAERMOD.BuscarPrimeiroId(null);

                                    dtParametro = classeAERMOD.RetornaParametroAreaCirc(codigo, primeiroCodigoPeriodo, primeiroCodigoPoluente);
                                }

                                salvar = true;
                            }

                            if (dtParametro.Rows.Count > 0)
                            {
                                tbxAremisCirc.Text = dtParametro.Rows[0]["TAXA_EMISSAO"].ValidarValor<decimal>(0).ToString();
                                tbxRelhgtCirc.Text = dtParametro.Rows[0]["ALTURA_LANCAMENTO"].ValidarValor<decimal>(0).ToString();
                                tbxRadiusCirc.Text = dtParametro.Rows[0]["RAIO"].ValidarValor<decimal>(0).ToString();
                                tbxNvertsCirc.Text = dtParametro.Rows[0]["NUMERO_VERTICES"].ValidarValor<int>(0).ToString();
                                tbxSzinitCirc.Text = dtParametro.Rows[0]["DIMENSAO_VERTICAL"].ValidarValor<decimal>(0).ToString();

                                if (salvar)
                                {
                                    SalvarParametro();
                                }
                            }

                            #endregion
                        }
                        break;
                    case TipoFonte.LINE:
                        {
                            #region Linha

                            dgvFonte.Tag = tabPageLinha.Name;
                            tabControlParametros.SelectedIndex = 4;
                            dgvFonte.Focus();

                            DataTable dtParametro = classeAERMOD.RetornaParametroLinha(codigo, codigoPeriodo, codigoPoluente);

                            tbxAremisLinha.ResetText();
                            tbxRelhgtLinha.ResetText();
                            tbxLarguraLinha.ResetText();
                            tbxSzinitLinha.ResetText();                            

                            bool salvar = false;

                            if (dtParametro.Rows.Count == 0)
                            {
                                int codigoPeriodoAnterior = classeAERMET.BuscarIdAnterior(codigoPeriodo);

                                dtParametro = classeAERMOD.RetornaParametroLinha(codigo, codigoPeriodoAnterior, codigoPoluente);

                                if (dtParametro.Rows.Count == 0)
                                {
                                    int primeiroCodigoPeriodo = classeAERMET.BuscarPrimeiroId(null);
                                    int primeiroCodigoPoluente = classeAERMOD.BuscarPrimeiroId(null);

                                    dtParametro = classeAERMOD.RetornaParametroLinha(codigo, primeiroCodigoPeriodo, primeiroCodigoPoluente);
                                }

                                salvar = true;
                            }

                            if (dtParametro.Rows.Count > 0)
                            {
                                tbxAremisLinha.Text = dtParametro.Rows[0]["TAXA_EMISSAO"].ValidarValor<decimal>(0).ToString();
                                tbxRelhgtLinha.Text = dtParametro.Rows[0]["ALTURA_LANCAMENTO"].ValidarValor<decimal>(0).ToString();
                                tbxLarguraLinha.Text = dtParametro.Rows[0]["LARGURA"].ValidarValor<decimal>(0).ToString();
                                tbxSzinitLinha.Text = dtParametro.Rows[0]["DIMENSAO_VERTICAL"].ValidarValor<decimal>(0).ToString();
                                
                                if (salvar)
                                {
                                    SalvarParametro();
                                }
                            }

                            #endregion
                        }
                        break;
                    case TipoFonte.OPENPIT:
                        {
                            #region Poço aberto

                            dgvFonte.Tag = tabPagePoco.Name;
                            tabControlParametros.SelectedIndex = 5;
                            dgvFonte.Focus();

                            DataTable dtParametro = classeAERMOD.RetornaParametroPoco(codigo, codigoPeriodo, codigoPoluente);

                            tbxOpemisPoco.ResetText();
                            tbxRelhgtPoco.ResetText();
                            tbxXinitPoco.ResetText();
                            tbxYinitPoco.ResetText();
                            tbxPitvolPoco.ResetText();
                            tbxAnguloPoco.ResetText();

                            bool salvar = false;

                            if (dtParametro.Rows.Count == 0)
                            {
                                int codigoPeriodoAnterior = classeAERMET.BuscarIdAnterior(codigoPeriodo);

                                dtParametro = classeAERMOD.RetornaParametroPoco(codigo, codigoPeriodoAnterior, codigoPoluente);

                                if (dtParametro.Rows.Count == 0)
                                {
                                    int primeiroCodigoPeriodo = classeAERMET.BuscarPrimeiroId(null);
                                    int primeiroCodigoPoluente = classeAERMOD.BuscarPrimeiroId(null);

                                    dtParametro = classeAERMOD.RetornaParametroPoco(codigo, primeiroCodigoPeriodo, primeiroCodigoPoluente);
                                }

                                salvar = true;
                            }

                            if (dtParametro.Rows.Count > 0)
                            {
                                tbxOpemisPoco.Text = dtParametro.Rows[0]["TAXA_EMISSAO"].ValidarValor<decimal>(0).ToString();
                                tbxRelhgtPoco.Text = dtParametro.Rows[0]["ALTURA_LANCAMENTO"].ValidarValor<decimal>(0).ToString();
                                tbxXinitPoco.Text = dtParametro.Rows[0]["COMPRIMENTO_X"].ValidarValor<decimal>(0).ToString();
                                tbxYinitPoco.Text = dtParametro.Rows[0]["COMPRIMENTO_Y"].ValidarValor<decimal>(0).ToString();
                                tbxPitvolPoco.Text = dtParametro.Rows[0]["VOLUME"].ValidarValor<decimal>(0).ToString();
                                tbxAnguloPoco.Text = dtParametro.Rows[0]["ANGULO"].ValidarValor<decimal>(0).ToString();

                                if (salvar)
                                {
                                    SalvarParametro();
                                }
                            }

                            #endregion
                        }
                        break;
                    case TipoFonte.VOLUME:
                        {
                            #region Volume

                            dgvFonte.Tag = tabPageVolume.Name;
                            tabControlParametros.SelectedIndex = 6;
                            dgvFonte.Focus();

                            DataTable dtParametro = classeAERMOD.RetornaParametroVolume(codigo, codigoPeriodo, codigoPoluente);

                            tbxVlemisVolume.ResetText();
                            tbxRelhgtVolume.ResetText();
                            tbxSyinitVolume.ResetText();
                            tbxSzinitVolume.ResetText();                            

                            bool salvar = false;

                            if (dtParametro.Rows.Count == 0)
                            {
                                int codigoPeriodoAnterior = classeAERMET.BuscarIdAnterior(codigoPeriodo);

                                dtParametro = classeAERMOD.RetornaParametroVolume(codigo, codigoPeriodoAnterior, codigoPoluente);

                                if (dtParametro.Rows.Count == 0)
                                {
                                    int primeiroCodigoPeriodo = classeAERMET.BuscarPrimeiroId(null);
                                    int primeiroCodigoPoluente = classeAERMOD.BuscarPrimeiroId(null);

                                    dtParametro = classeAERMOD.RetornaParametroVolume(codigo, primeiroCodigoPeriodo, primeiroCodigoPoluente);
                                }

                                salvar = true;
                            }

                            if (dtParametro.Rows.Count > 0)
                            {
                                tbxVlemisVolume.Text = dtParametro.Rows[0]["TAXA_EMISSAO"].ValidarValor<decimal>(0).ToString();
                                tbxRelhgtVolume.Text = dtParametro.Rows[0]["ALTURA_LANCAMENTO"].ValidarValor<decimal>(0).ToString();
                                tbxSyinitVolume.Text = dtParametro.Rows[0]["DIMENSAO_LATERAL"].ValidarValor<decimal>(0).ToString();
                                tbxSzinitVolume.Text = dtParametro.Rows[0]["DIMENSAO_VERTICAL"].ValidarValor<decimal>(0).ToString();
                                
                                if (salvar)
                                {
                                    SalvarParametro();
                                }
                            }

                            #endregion
                        }
                        break;
                }                
            }            
        }

        /// <summary>
        /// Habilita/Desabilita campos
        /// </summary>
        /// <param name="valor">True/False</param>
        private void HabilitarCampos(bool valor)
        {
            tbxQS.Enabled = valor;
            tbxHS.Enabled = valor;
            tbxTS.Enabled = valor;
            tbxVS.Enabled = valor;
            tbxDS.Enabled = valor;

            tbxAremis.Enabled = valor;
            tbxRelhgt.Enabled = valor;
            tbxXinit.Enabled = valor;
            tbxYinit.Enabled = valor;
            tbxAngulo.Enabled = valor;
            tbxSzinit.Enabled = valor;

            tbxAremisPoli.Enabled = valor;
            tbxRelhgtPoli.Enabled = valor;
            tbxNvertsPoli.Enabled = valor;
            tbxSzinitPoli.Enabled = valor;
            btnCoordenadasPoli.Enabled = valor;

            tbxAremisCirc.Enabled = valor;
            tbxRelhgtCirc.Enabled = valor;
            tbxRadiusCirc.Enabled = valor;
            tbxNvertsCirc.Enabled = valor;
            tbxSzinitCirc.Enabled = valor;

            tbxAremisLinha.Enabled = valor;
            tbxRelhgtLinha.Enabled = valor;
            tbxLarguraLinha.Enabled = valor;
            tbxSzinitLinha.Enabled = valor;

            tbxOpemisPoco.Enabled = valor;
            tbxRelhgtPoco.Enabled = valor;
            tbxXinitPoco.Enabled = valor;
            tbxYinitPoco.Enabled = valor;
            tbxPitvolPoco.Enabled = valor;
            tbxAnguloPoco.Enabled = valor;

            tbxVlemisVolume.Enabled = valor;
            tbxRelhgtVolume.Enabled = valor;
            tbxSyinitVolume.Enabled = valor;
            tbxSzinitVolume.Enabled = valor;
        }

        /// <summary>
        /// Abrir cadastro de coordenadas (AREAPOLY).
        /// </summary>
        private void AbrirCoordenadasAreaPoly()
        {
            int codigoFonte = Convert.ToInt32(dgvFonte.CurrentRow.Cells["CODIGO"].Value);

            if (lstCoordenadas.Count == 0)
            {
                decimal coordenadaX = Convert.ToDecimal(dgvFonte.CurrentRow.Cells["X"].Value);
                decimal coordenadaY = Convert.ToDecimal(dgvFonte.CurrentRow.Cells["Y"].Value);

                lstCoordenadas = new List<Tuple<decimal, decimal>>() { Tuple.Create(coordenadaX, coordenadaY) };
            }

            FrmCoordenadasAERMOD frmCoordenadas = new FrmCoordenadasAERMOD(codigoFonte, codigoPeriodo, codigoPoluente, lstCoordenadas);
            frmCoordenadas.ShowDialogFade(this);

            lstCoordenadas = frmCoordenadas.LstCoordenadas;

            tbxAremisPoli.Focus();
        }

        #endregion

        #region Eventos FrmParametrosAERMOD

        private void FrmParametrosAERMOD_Load(object sender, EventArgs e)
        {
            dgvFonte.ScrollBarVisible(true);

            var dadosAERMET = classeAERMET.RetornaDados(codigoPeriodo);
            var dadosAERMOD = classeAERMOD.RetornaDados(codigoPoluente);            

            if (dadosAERMET != null && dadosAERMET.Item1.Rows.Count > 0)
            {
                DateTime dataInicial = Convert.ToDateTime(dadosAERMET.Item1.Rows[0]["PERIODO_INICIAL"]);
                DateTime dataFinal = Convert.ToDateTime(dadosAERMET.Item1.Rows[0]["PERIODO_FINAL"]);

                this.Text = $"{this.Text} ({dataInicial.ToShortDateString()} - {dataFinal.ToShortDateString()})";
            }

            if (dadosAERMOD != null && dadosAERMOD.Item1.Rows.Count > 0)
            {
                Poluentes poluente = (Poluentes)dadosAERMOD.Item1.Rows[0]["POLUENTE"].ValidarValor<int>(0);

                this.Text = $"{this.Text} {poluente.ToString()}";
            }

            CarregarFontes();
        }        

        private void FrmParametrosAERMOD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt)
            {
                switch (e.KeyCode)
                {
                    case Keys.A:
                        e.SuppressKeyPress = true;
                        AlterarParametro();
                        break;
                }
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.F1:
                        e.SuppressKeyPress = true;
                        AbrirAjuda();
                        break;
                    case Keys.F2:
                        e.SuppressKeyPress = true;

                        if (tabControlParametros.SelectedTab == tabPageAreaPoligono)
                        {
                            AbrirCoordenadasAreaPoly();
                        }
                        break;
                    case Keys.Enter:
                        e.SuppressKeyPress = true;

                        if (dgvFonte.Focused == false)
                        {
                            SalvarParametro();
                        }
                        else
                        {
                            this.Close();
                        }
                        break;
                    case Keys.Escape:
                        e.SuppressKeyPress = true;
                        SairForm();
                        break;                    
                }
            }
        }

        private void FrmParametrosAERMOD_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (SairForm(true) == false)
            {
                e.Cancel = true;
            }
        }

        #endregion

        #region Eventos tabControlParametros

        private void tabControlParametros_Selecting(object sender, TabControlCancelEventArgs e)
        {           
            if (dgvFonte.Tag != null && dgvFonte.Tag.ToString() != e.TabPage.Name)
            {
                e.Cancel = true;
            }
        }

        #endregion

        #region Eventos btnCoordenadasPoli

        private void btnCoordenadasPoli_Click(object sender, EventArgs e)
        {
            AbrirCoordenadasAreaPoly();
        }

        #endregion

        #region Eventos dgvFonte

        private void dgvFonte_Enter(object sender, EventArgs e)
        {
            HabilitarCampos(false);
        }

        private void dgvFonte_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvFonte.CurrentCell != null && dgvFonte.CurrentCell.ColumnIndex == 0)
            {
                dgvFonte.ReadOnly = true;
            }
        }

        private void dgvFonte_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AlterarParametro();
        }

        private void dgvFonte_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    VisualizarParametro();
                    break;
                case Keys.Down:
                    VisualizarParametro();
                    break;
            }
        }

        private void dgvFonte_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            VisualizarParametro();
        }

        #endregion

        #region Eventos Ponto

        #region Eventos tbxQS

        private void tbxQS_Leave(object sender, EventArgs e)
        {
            if (this.ActiveControl != null && this.ActiveControl == tabControlParametros)
            {
                ((TextBoxMaskLIB)sender).Focus();
            }
        }

        #endregion

        #endregion

        #region Eventos Área Polígono

        private void btnCoordenadasPoli_Leave(object sender, EventArgs e)
        {
            if (this.ActiveControl != null && this.ActiveControl == tabControlParametros)
            {
                btnCoordenadasPoli.Focus();
            }
        }

        #endregion

        #region Eventos statusStripConsulta

        private void btnAjuda_ButtonClick(object sender, EventArgs e)
        {
            AbrirAjuda();
        }

        private void btnAlterar_ButtonClick(object sender, EventArgs e)
        {
            AlterarParametro();
        }

        private void btnSalvar_ButtonClick(object sender, EventArgs e)
        {
            SalvarParametro();
        }

        private void btnSair_ButtonClick(object sender, EventArgs e)
        {
            SairForm();
        }

        #endregion
    }
}
