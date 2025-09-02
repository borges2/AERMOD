using AERMOD.LIB.Componentes.GridView;
using AERMOD.LIB.Desenvolvimento;
using AERMOD.LIB.Formatacao;
using AERMOD.LIB.Forms;
using CamadaLogicaNegocios;
using Microsoft.VisualStudio.OLE.Interop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace AERMOD.CamadaApresentacao.PRINCIPAL
{
    public partial class FrmDefinicaoAERMOD : Form
    {
        #region Instâncias e Propriedades

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

        ClsAERMET clsAERMET = null;

        /// <summary>
        /// Get classe de negócios ClsAERMET
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
        /// Get classe de negócios ClsAERMET
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
        /// Lista com os dados de saída.
        /// </summary>
        List<dynamic> lstSaidas = new List<dynamic>();

        /// <summary>
        /// Abrir o cadastro no modo consulta.
        /// </summary>
        bool consulta;

        /// <summary>
        /// Código do registro.
        /// </summary>
        public int Codigo { get; set; }

        #endregion

        #region Construtor

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="consulta">Modo consulta</param>
        public FrmDefinicaoAERMOD(bool consulta = false)
        {
            InitializeComponent();

            this.Icon = Properties.Resources.form2.ConvertImageToIcon();
            this.consulta = consulta;
        }

        #endregion

        #region Métodos

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            #region Tecla Escape

            if (dgvRetangulo.Focused)
            {
                if (keyData == Keys.Delete)
                {
                    ExcluirRetangulo();
                    return false;
                }

                if (keyData == Keys.Insert)
                {
                    InserirRetangulo();
                    return false;
                }

                if (keyData == (Keys.Alt | Keys.A))
                {
                    AlterarRetangulo();
                    return false;
                }
            }

            #endregion

            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Abrir tela de ajuda.
        /// </summary>
        private void AbrirAjuda()
        {
            string caminho = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "Help");
            Help.ShowHelp(this, $"{caminho}\\AERMOD.chm", HelpNavigator.TopicId, "50");
            //Help.ShowHelp(this, $"{caminho}\\AERMOD.chm", HelpNavigator.TableOfContents, "10");
            //Help.ShowHelp(this, $"{caminho}\\AERMOD.chm", HelpNavigator.Index, "10");
        }

        /// <summary>
        /// Carregar registros.
        /// </summary>
        private void CarregarRegistros()
        {
            FrmLoading frmLoading = new FrmLoading(this);

            Thread thr = new Thread(delegate ()
            {
                DataTable dt = classeAERMOD.RetornaDados();

                frmLoading.Style = ProgressBarStyle.Blocks;
                frmLoading.Maximum = dt.Rows.Count;

                CrossThreadOperation.Invoke(this, delegate
                {
                    int count = 0;
                    foreach (DataRow item in dt.Rows)
                    {
                        count++;
                        frmLoading.AtualizarStatus(count);

                        dgvDados.Rows.Add(false,
                                          item["CODIGO"],
                                          ((Poluentes)Convert.ToInt32(item["POLUENTE"])).GetEnumDescription(),
                                          ((Poluentes)Convert.ToInt32(item["POLUENTE"])).ToString()                                          
                                          );

                        if (item["EM_USO"].ValidarValor<bool>(false))
                        {
                            dgvDados.Rows[dgvDados.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Blue;
                        }
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

            if (dgvDados.Rows.Count > 0)
            {
                dgvDados.Rows[0].Cells[0].Selected = true;
                dgvDados.Focus();

                tabControlDados.SelectedTab = tabPageConsulta;
            }
            else
            {
                InserirRegistro(true);
            }
        }

        /// <summary>
        /// Inserir registro.
        /// </summary>
        private void InserirRegistro(bool dadosDefault)
        {
            if (dadosDefault)
            {
                cbxMediaHoraria.SelectedValue = (int)MediaHoraria.VINTE_E_QUATRO;
                cbxMediaPeriodo.SelectedValue = (int)MediaPeriodo.PERIOD;
                cbxPoluente.SelectedValue = (int)Poluentes.PM10;
                tbxUnidadeMedicao.Text = "Microgramas/metro cúbico ( µg/m3)";

                dgvRetangulo.Rows.Clear();
                dgvRetangulo.Rows.Add(false, 1);
                dgvRetangulo.Rows.Add(false, 2);

                tbxValorMaximo.Text = "50";                
            }
            else
            {
                LimparCampos();
            }

            dgvDados.Tag = "INSERIR";
            tabControlDados.SelectedTab = tabPageCadastro;
            tabControlCadastro.SelectedTab = tabPageDadosBasicos;

            this.ActiveControl = cbxMediaHoraria;
            cbxMediaHoraria.Focus();

            lstSaidas.Clear();
            dgvSaida.Rows.Clear();
        }

        /// <summary>
        /// Limpar campos.
        /// </summary>
        private void LimparCampos()
        {
            cbxMediaHoraria.SelectedValue = 0;
            cbxMediaPeriodo.SelectedValue = 0;
            cbxPoluente.SelectedValue = 1;
            tbxUnidadeMedicao.ResetText();
            dgvRetangulo.Rows.Clear();
            tbxValorMaximo.ResetText();
        }

        /// <summary>
        /// Limpar campos.
        /// </summary>
        private void LimparCamposSaidas()
        {
            cbxTipoSaida.SelectedValue = 0;
            cbxMediaHorariaPoluente.SelectedValue = DBNull.Value;
            cbxMediaPeriodoPoluente.SelectedValue = DBNull.Value;
            tbxQualidadeAr.ResetText();
            cbxCriterioReceptor.SelectedValue = DBNull.Value;
            tbxValorMaximoPoluente.ResetText();
        }

        /// <summary>
        /// Preenchimento de campos automático.
        /// </summary>
        private void PreencherCamposAutomatico()
        {
            cbxTipoSaida.SelectedValue = (int)TipoSaida.PLOTFILE;
            cbxMediaHorariaPoluente.SelectedValue = DBNull.Value;
            cbxMediaPeriodoPoluente.SelectedValue = (int)MediaPeriodo.PERIOD;
            tbxQualidadeAr.ResetText();
            cbxCriterioReceptor.SelectedValue = DBNull.Value;
            tbxValorMaximoPoluente.ResetText();            
        }

        /// <summary>
        /// Alterar registro.
        /// </summary>
        private void AlterarRegistro()
        {
            if (dgvDados.Focused && dgvDados.CurrentRow != null)
            {
                lstSaidas.Clear();
                dgvRetangulo.Rows.Clear();

                int codigo = Convert.ToInt32(dgvDados.CurrentRow.Cells["CODIGO"].Value);

                var registro = classeAERMOD.RetornaDados(codigo);

                if (registro != null && registro.Item1.Rows.Count > 0)
                {
                    cbxMediaHoraria.SelectedValue = registro.Item1.Rows[0]["MEDIA_HORARIA"].ValidarValor<int>(0);
                    cbxMediaPeriodo.SelectedValue = registro.Item1.Rows[0]["MEDIA_PERIODO"].ValidarValor<int>(0);
                    cbxPoluente.SelectedValue = registro.Item1.Rows[0]["POLUENTE"].ValidarValor<int>(0);
                    tbxUnidadeMedicao.Text = registro.Item1.Rows[0]["UNIDADE_MEDICAO"].ToString();

                    DataTable dtRetangulo = classeAERMOD.RetornaRetangulo(codigo);
                    foreach (DataRow item in dtRetangulo.Rows)
                    {
                        dgvRetangulo.Rows.Add(false, item["VALOR"]);
                    }                    

                    tbxValorMaximo.Text = registro.Item1.Rows[0]["VALOR_MAXIMO"].ToString();

                    dgvSaida.Rows.Clear();
                    
                    DataTable dtSaida = registro.Item2;

                    foreach (DataRow item in dtSaida.Rows)
                    {
                        dynamic dados = new ExpandoObject();
                        dados.SEQUENCIA = item["SEQUENCIA"];
                        dados.TIPO_SAIDA = item["TIPO_SAIDA"];
                        dados.MEDIA_HORARIA = item["MEDIA_HORARIA"].ValidarValor<int?>(null);
                        dados.MEDIA_PERIODO = item["MEDIA_PERIODO"].ValidarValor<int?>(null);
                        dados.PADRAO_QUALIDADE_AR = item["PADRAO_QUALIDADE_AR"];
                        dados.CRITERIO_RECEPTOR = item["CRITERIO_RECEPTOR"].ValidarValor<int?>(null);
                        dados.VALOR_MAXIMO = item["VALOR_MAXIMO"];
                        dados.DESCRICAO = item["DESCRICAO"];
                        dados.POLUENTE = Convert.ToInt32(cbxPoluente.SelectedValue);

                        lstSaidas.Add(dados);

                        var configuracaoSaida = ConfigurarSaidas(dados);

                        dgvSaida.Rows.Add(false,
                                          item["SEQUENCIA"],
                                          configuracaoSaida.Item1,
                                          configuracaoSaida.Item2,
                                          configuracaoSaida.Item3,
                                          configuracaoSaida.Item4);                        
                    }
                }

                dgvDados.Tag = "ALTERAR";
                tabControlDados.SelectedTab = tabPageCadastro;
                tabControlCadastro.SelectedTab = tabPageDadosBasicos;

                cbxMediaHoraria.Focus();
            }
        }

        /// <summary>
        /// Retorna parametros de saída.
        /// </summary>
        /// <param name="dados">Objeto dados</param>
        /// <param name="poluente">Poluente atual</param>
        /// <returns>Tuple</returns>
        private Tuple<string, string, string, string> ConfigurarSaidas(dynamic dados)
        {
            TipoSaida tipoSaida = (TipoSaida)dados.TIPO_SAIDA;
            string criterio = string.Empty;
            string descricao = string.Empty;

            string mediaTemporal = string.IsNullOrEmpty(Convert.ToString(dados.MEDIA_HORARIA)) == false ? ((MediaHoraria)dados.MEDIA_HORARIA).GetEnumDescription() : string.Empty;

            if (string.IsNullOrEmpty(mediaTemporal))
            {
                mediaTemporal = ((MediaPeriodo)dados.MEDIA_PERIODO).ToString();
            }

            switch (tipoSaida)
            {
                case TipoSaida.MAXIFILE:
                    criterio = dados.PADRAO_QUALIDADE_AR.ToString();
                    descricao = $"{((Poluentes)dados.POLUENTE).ToString()}_{mediaTemporal}_MAXI.PLT";
                    break;
                case TipoSaida.POSTFILE:
                    criterio = "PLOT";
                    descricao = $"{((Poluentes)dados.POLUENTE).ToString()}_{mediaTemporal}_POST.PLT";
                    break;
                case TipoSaida.PLOTFILE:
                    string criterioReceptor = Convert.ToString(dados.CRITERIO_RECEPTOR);

                    if (string.IsNullOrEmpty(criterioReceptor) == false)
                    {
                        criterio = ((CriterioReceptor)Convert.ToInt32(criterioReceptor)).ToString();
                    }

                    descricao = $"{((Poluentes)dados.POLUENTE).ToString()}_{mediaTemporal}_PLOT.PLT";
                    break;
                case TipoSaida.RANKFILE:
                    criterio = dados.VALOR_MAXIMO.ToString();
                    descricao = $"{((Poluentes)dados.POLUENTE).ToString()}_{mediaTemporal}_{criterio}_RANK.PLT";
                    break;
            }

            return new Tuple<string, string, string, string>(tipoSaida.GetEnumDescription(), mediaTemporal, criterio, descricao);
        }

        /// <summary>
        /// Excluir registro.
        /// </summary>
        private void ExcluirRegistro()
        {
            if (dgvDados.Focused && dgvDados.CurrentRow != null)
            {
                Boolean marcado = dgvDados.Rows.OfType<DataGridViewRow>().Any(f => Convert.ToBoolean(f.Cells[0].Value) == true);

                if (marcado.Equals(false))
                {
                    dgvDados.CurrentRow.Cells[0].Value = true;
                    dgvDados.RefreshEdit();
                }

                DialogResult dialogResult = MessageBox.Show(this, classeHelp.BuscarMensagem(1), classeHelp.BuscarMensagem(2), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    FrmLoading frmLoading = new FrmLoading(this);

                    Thread thrExcluir = new Thread(delegate ()
                    {
                        try
                        {
                            int count = 0;

                            for (Int32 i = dgvDados.Rows.Count - 1; i >= 0; i--)
                            {
                                if (Convert.ToBoolean(dgvDados.Rows[i].Cells[0].Value) == true)
                                {
                                    count++;
                                    frmLoading.AtualizarStatus(count);

                                    classeAERMOD.Excluir(Convert.ToInt32(dgvDados.Rows[i].Cells["CODIGO"].Value));

                                    CrossThreadOperation.Invoke(this, delegate
                                    {
                                        dgvDados.Rows.Remove(dgvDados.Rows[i]);
                                    });
                                }
                            }
                        }
                        catch { }
                    });
                    thrExcluir.Start();

                    frmLoading.thread = thrExcluir;
                    frmLoading.Texto = "Excluindo registro(s)...";
                    frmLoading.Style = ProgressBarStyle.Blocks;
                    frmLoading.Maximum = dgvDados.Rows.OfType<DataGridViewRow>().Count(f => Convert.ToBoolean(f.Cells[0].Value));
                    frmLoading.ShowDialogFade(this);

                    if (dgvDados.RowCount == 0)
                    {
                        InserirRegistro(true);
                    }
                }
                else if (marcado == false)
                {
                    dgvDados.CurrentRow.Cells[0].Value = false;
                    dgvDados.RefreshEdit();
                }
            }
        }

        /// <summary>
        /// Salvar registro em edição.
        /// </summary>
        private void SalvarRegistro()
        {
            if (dgvDados.Tag == null)
            {
                return;
            }

            #region Validação            

            if (dgvRetangulo.Rows.Count == 0 || dgvRetangulo.Rows.Count > 999)
            {
                MessageBox.Show(this, $"Retângulo - {classeHelp.BuscarMensagem(28)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dgvRetangulo.Focus();
                return;
            }            

            if (Convert.ToInt32(tbxValorMaximo.Text) == 0)
            {
                MessageBox.Show(this, $"Valor máximo - {classeHelp.BuscarMensagem(3)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxValorMaximo.Focus();
                return;
            }
            else if (Convert.ToInt32(tbxValorMaximo.Text) > 400)
            {
                MessageBox.Show(this, $"Valor máximo - {classeHelp.BuscarMensagem(54)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxValorMaximo.Focus();
                return;
            }

            if (tbxUnidadeMedicao.Text.Trim().Length == 0)
            {
                MessageBox.Show(this, $"Unidade de medição - {classeHelp.BuscarMensagem(3)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxUnidadeMedicao.Focus();
                return;
            }

            int codigo = dgvDados.Tag.ToString() == "ALTERAR" ? Convert.ToInt32(dgvDados.CurrentRow.Cells["CODIGO"].Value) : 0;

            int retorno = classeAERMOD.VerificarDuplicidade(codigo, Convert.ToInt32(cbxPoluente.SelectedValue));
            if (retorno > 0)
            {
                string ID = retorno.ToString();
                string msg = $"{classeHelp.BuscarMensagem(20)}\nID: {ID}";

                MessageBox.Show(this, msg, classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbxMediaHoraria.Focus();
                return;
            }

            if (lstSaidas.Count == 0)
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(29), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);

                tabControlDados.SelectedTab = tabPageCadastro;
                tabControlCadastro.SelectedTab = tabPageSaidas;

                InserirSaida();
                return;
            }

            #region Comparação (Médias temporais/Médias temporais saídas)

            MediaHoraria mediaHoraria = (MediaHoraria)Convert.ToInt32(cbxMediaHoraria.SelectedValue);
            MediaPeriodo mediaPeriodo = (MediaPeriodo)Convert.ToInt32(cbxMediaPeriodo.SelectedValue);

            foreach (var item in lstSaidas)
            {
                MediaHoraria? mediaHorariaSaida = item.MEDIA_HORARIA != null ? (MediaHoraria)item.MEDIA_HORARIA : (MediaHoraria?)null;
                MediaPeriodo? mediaPeriodoSaida = item.MEDIA_PERIODO != null ? (MediaPeriodo)item.MEDIA_PERIODO : (MediaPeriodo?)null;

                if (mediaHorariaSaida.HasValue && mediaHorariaSaida.Value != mediaHoraria)
                {
                    MessageBox.Show(this, $"{classeHelp.BuscarMensagem(60)} {item.SEQUENCIA}.", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    tabControlDados.SelectedTab = tabPageCadastro;
                    tabControlCadastro.SelectedTab = tabPageSaidas;

                    dgvSaida.Focus();
                    DataGridViewRow linha = dgvSaida.Rows.OfType<DataGridViewRow>().FirstOrDefault(I => Convert.ToInt32(I.Cells["ID"].Value) == item.SEQUENCIA);
                    linha.Cells[0].Selected = true;
                    return;
                }

                if (mediaPeriodoSaida.HasValue && mediaPeriodoSaida.Value != mediaPeriodo)
                {
                    MessageBox.Show(this, $"{classeHelp.BuscarMensagem(61)} {item.SEQUENCIA}.", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    tabControlDados.SelectedTab = tabPageCadastro;
                    tabControlCadastro.SelectedTab = tabPageSaidas;

                    dgvSaida.Focus();
                    DataGridViewRow linha = dgvSaida.Rows.OfType<DataGridViewRow>().FirstOrDefault(I => Convert.ToInt32(I.Cells["ID"].Value) == item.SEQUENCIA);
                    linha.Cells[0].Selected = true;
                    return;
                }
            }

            #endregion

            #endregion

            dynamic dados = new ExpandoObject();
            dados.CODIGO = codigo;
            dados.MEDIA_HORARIA = Convert.ToInt32(cbxMediaHoraria.SelectedValue);
            dados.MEDIA_PERIODO = Convert.ToInt32(cbxMediaPeriodo.SelectedValue);
            dados.POLUENTE = Convert.ToInt32(cbxPoluente.SelectedValue);
            dados.UNIDADE_MEDICAO = tbxUnidadeMedicao.Text;
            dados.VALOR_MAXIMO = Convert.ToInt32(tbxValorMaximo.Text);            

            List<int> lstRetangulo = new List<int>();

            foreach (DataGridViewRow item in dgvRetangulo.Rows)
            {
                lstRetangulo.Add(Convert.ToInt32(item.Cells["VALORES"].Value));
            }

            codigo = classeAERMOD.Atualizar(dados, lstSaidas, lstRetangulo);
            if (codigo > 0)
            {
                if (dgvDados.Tag.ToString() == "INSERIR")
                {
                    dgvDados.Rows.Add(false,
                                      codigo,
                                      ((Poluentes)Convert.ToInt32(cbxPoluente.SelectedValue)).GetEnumDescription(),
                                      ((Poluentes)Convert.ToInt32(cbxPoluente.SelectedValue)).ToString()
                                      );

                    MessageBox.Show(this, classeHelp.BuscarMensagem(7), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    InserirRegistro(false);

                    if (dgvDados.Rows.Count == 1)
                    {
                        classeAERMOD.AtualizarUso(codigo);
                    }
                }
                else
                {
                    dgvDados.CurrentRow.Cells["POLUENTE"].Value = ((Poluentes)Convert.ToInt32(cbxPoluente.SelectedValue)).GetEnumDescription();
                    dgvDados.CurrentRow.Cells["FORMULA"].Value = ((Poluentes)Convert.ToInt32(cbxPoluente.SelectedValue)).ToString();

                    dgvDados.Focus();
                    tabControlDados.SelectedTab = tabPageConsulta;
                }                
            }
            else
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(5), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbxMediaHoraria.Focus();
            }
        }

        /// <summary>
        /// Atualizar uso.
        /// </summary>
        private void AtualizarUso()
        {
            if (dgvDados.CurrentRow != null)
            {
                int codigo = Convert.ToInt32(dgvDados.CurrentRow.Cells["CODIGO"].Value);

                classeAERMOD.AtualizarUso(codigo);
                this.Close();
            }
        }

        /// <summary>
        /// Consultar registros.
        /// </summary>
        private void ConsultarRegistros()
        {
            tabControlDados.SelectedTab = tabPageConsulta;
            dgvDados.Focus();
        }

        /// <summary>
        /// Sair do form.
        /// </summary>
        private void SairForm()
        {
            if (tabControlDados.SelectedTab == tabPageCadastro && tabControlCadastro.SelectedTab == tabPageSaidas)
            {
                if (dgvSaida.Rows.Count > 0)
                {
                    if (dgvSaida.Focused == false)
                    {
                        dgvSaida.Focus();
                    }
                    else
                    {
                        if (dgvDados.Rows.Count > 0)
                        {
                            dgvDados.Focus();
                            tabControlDados.SelectedTab = tabPageConsulta;
                        }
                        else
                        {
                            this.Close();
                        }
                    }
                }
                else if (dgvDados.Rows.Count > 0)
                {
                    dgvDados.Focus();
                    tabControlDados.SelectedTab = tabPageConsulta;
                }
                else
                {
                    this.Close();
                }

                return;
            }

            if (tabControlDados.SelectedTab == tabPageConsulta || dgvDados.Rows.Count == 0)
            {
                this.Close();
            }
            else
            {
                dgvDados.Focus();
                tabControlDados.SelectedTab = tabPageConsulta;
            }
        }

        /// <summary>
        /// Carregar combobox com enumeradores.
        /// </summary>
        private void CarregarComboBox()
        {
            #region Média horária

            DataTable dtMediaHoraria = Enumeradores.RetornarMediaHoraria();

            cbxMediaHoraria.DisplayMember = "DESCRICAO";
            cbxMediaHoraria.ValueMember = "CODIGO";
            cbxMediaHoraria.DataSource = dtMediaHoraria;

            DataTable dtMediaHorariaPoluente = Enumeradores.RetornarMediaHoraria();
            dtMediaHorariaPoluente.Rows.InsertAt(dtMediaHorariaPoluente.NewRow(), 0);

            cbxMediaHorariaPoluente.DisplayMember = "DESCRICAO";
            cbxMediaHorariaPoluente.ValueMember = "CODIGO";
            cbxMediaHorariaPoluente.DataSource = dtMediaHorariaPoluente;            

            #endregion

            #region Média período

            DataTable dtMediaPeriodo = Enumeradores.RetornarMediaPeriodo();

            cbxMediaPeriodo.DisplayMember = "DESCRICAO";
            cbxMediaPeriodo.ValueMember = "CODIGO";
            cbxMediaPeriodo.DataSource = dtMediaPeriodo;

            DataTable dtMediaPeriodoPoluente = Enumeradores.RetornarMediaPeriodo();
            dtMediaPeriodoPoluente.Rows.InsertAt(dtMediaPeriodoPoluente.NewRow(), 0);

            cbxMediaPeriodoPoluente.DisplayMember = "DESCRICAO";
            cbxMediaPeriodoPoluente.ValueMember = "CODIGO";
            cbxMediaPeriodoPoluente.DataSource = dtMediaPeriodoPoluente;

            #endregion

            #region Poluente

            DataTable dtPoluente = Enumeradores.RetornarPoluentes();

            cbxPoluente.DisplayMember = "DESCRICAO";
            cbxPoluente.ValueMember = "CODIGO";
            cbxPoluente.DataSource = dtPoluente;

            #endregion

            #region Tipo saída

            DataTable dtTipoSaida = Enumeradores.RetornarTipoSaida();

            cbxTipoSaida.DisplayMember = "DESCRICAO";
            cbxTipoSaida.ValueMember = "CODIGO";
            cbxTipoSaida.DataSource = dtTipoSaida;

            #endregion

            #region Critério receptor

            DataTable dtCriterioReceptor = Enumeradores.RetornarCriterioReceptor();
            dtCriterioReceptor.Rows.InsertAt(dtCriterioReceptor.NewRow(), 0);

            cbxCriterioReceptor.DisplayMember = "DESCRICAO";
            cbxCriterioReceptor.ValueMember = "CODIGO";
            cbxCriterioReceptor.DataSource = dtCriterioReceptor;

            #endregion
        }

        /// <summary>
        /// Capturar registro no modo consulta.
        /// </summary>
        private void CapturarRegistro()
        {
            if (dgvDados.CurrentRow != null)
            {
                Codigo = Convert.ToInt32(dgvDados.CurrentRow.Cells["CODIGO"].Value);
                this.Close();
            }
        }

        #region Retângulo

        /// <summary>
        /// Inserir retângulo.
        /// </summary>
        private void InserirRetangulo()
        {
            if ((dgvRetangulo.CurrentCell == null || dgvRetangulo.CurrentCell.ColumnIndex == 0) || (dgvRetangulo.CurrentCell.ColumnIndex != 0 && dgvRetangulo.CurrentCell.IsInEditMode == false))
            {
                int ultimoValor = dgvRetangulo.Rows.Count > 0 ? Convert.ToInt32(dgvRetangulo.Rows[dgvRetangulo.Rows.Count - 1].Cells["VALORES"].Value) : 0;
                ultimoValor++;

                if (ultimoValor < 999)
                {
                    dgvRetangulo.Rows.Add();
                    dgvRetangulo.Rows[dgvRetangulo.Rows.Count - 1].Cells[1].Selected = true;

                    dgvRetangulo.Rows[dgvRetangulo.Rows.Count - 1].Cells[1].Value = ultimoValor;
                    dgvRetangulo.ReadOnly = false;
                    dgvRetangulo.BeginEdit(true);
                }
                else
                {
                    MessageBox.Show(this, classeHelp.BuscarMensagem(33), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// Alterar retângulo.
        /// </summary>
        private void AlterarRetangulo()
        {
            if (dgvRetangulo.CurrentCell != null && dgvRetangulo.CurrentCell.ColumnIndex == 1 && dgvRetangulo.CurrentCell.IsInEditMode == false)
            {
                dgvRetangulo.ReadOnly = false;
                dgvRetangulo.BeginEdit(false);
            }
        }

        /// <summary>
        /// Excluir retângulo.
        /// </summary>
        private void ExcluirRetangulo()
        {
            if (dgvRetangulo.Focused && dgvRetangulo.CurrentRow != null)
            {
                Boolean marcado = dgvRetangulo.Rows.OfType<DataGridViewRow>().Any(f => Convert.ToBoolean(f.Cells[0].Value) == true);

                if (marcado.Equals(false))
                {
                    dgvRetangulo.CurrentRow.Cells[0].Value = true;
                    dgvRetangulo.RefreshEdit();
                }

                DialogResult dialogResult = MessageBox.Show(this, classeHelp.BuscarMensagem(1), classeHelp.BuscarMensagem(2), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    FrmLoading frmLoading = new FrmLoading(this);

                    Thread thrExcluir = new Thread(delegate ()
                    {
                        try
                        {
                            int count = 0;

                            for (Int32 i = dgvRetangulo.Rows.Count - 1; i >= 0; i--)
                            {
                                if (Convert.ToBoolean(dgvRetangulo.Rows[i].Cells[0].Value) == true)
                                {
                                    count++;
                                    frmLoading.AtualizarStatus(count);

                                    CrossThreadOperation.Invoke(this, delegate
                                    {
                                        dgvRetangulo.Rows.Remove(dgvRetangulo.Rows[i]);
                                    });
                                }
                            }
                        }
                        catch { }
                    });
                    thrExcluir.Start();

                    frmLoading.thread = thrExcluir;
                    frmLoading.Texto = "Excluindo registro(s)...";
                    frmLoading.Style = ProgressBarStyle.Blocks;
                    frmLoading.Maximum = dgvRetangulo.Rows.OfType<DataGridViewRow>().Count(f => Convert.ToBoolean(f.Cells[0].Value));
                    frmLoading.ShowDialogFade(this);
                }
                else if (marcado == false)
                {
                    dgvRetangulo.CurrentRow.Cells[0].Value = false;
                    dgvRetangulo.RefreshEdit();
                }
            }
        }

        #endregion

        #region Saídas

        /// <summary>
        /// Habilitar/Desabilitar campos.
        /// </summary>
        /// <param name="habilitar">Habilitar/Desabilitar</param>
        private void HabilitarCamposSaidas(bool habilitar)
        {
            cbxTipoSaida.Enabled = habilitar;
            cbxMediaHorariaPoluente.Enabled = habilitar;
            cbxMediaPeriodoPoluente.Enabled = habilitar;
            tbxQualidadeAr.Enabled = habilitar;
            cbxCriterioReceptor.Enabled = habilitar;
            tbxValorMaximoPoluente.Enabled = habilitar;            

            cbxTipoSaida.Focus();

            if (habilitar)
            {
                if (cbxTipoSaida.Focused)
                {
                    btnInserirSaida.Enabled = false;
                    btnAlterarSaida.Enabled = false;
                    btnExcluirSaida.Enabled = false;
                    btnSalvarSaida.Enabled = true;
                }
            }
            else
            {
                btnInserirSaida.Enabled = true;
                btnAlterarSaida.Enabled = true;
                btnExcluirSaida.Enabled = true;
                btnSalvarSaida.Enabled = false;
            }
        }

        /// <summary>
        /// Habilitar/Desabilitar campos conforme o tipo de saída do arquivo.
        /// </summary>
        private void HabilitarCamposTipoSaidas()
        {
            if (cbxTipoSaida.SelectedValue != null && cbxTipoSaida.SelectedValue != DBNull.Value)
            {
                TipoSaida tipoSaida = (TipoSaida)cbxTipoSaida.SelectedValue;

                if (cbxMediaHorariaPoluente.SelectedValue != null && cbxMediaHorariaPoluente.SelectedValue != DBNull.Value)
                {
                    cbxMediaHorariaPoluente.Enabled = true;
                    cbxMediaPeriodoPoluente.Enabled = false;
                }
                else
                {
                    cbxMediaHorariaPoluente.Enabled = false;
                    cbxMediaPeriodoPoluente.Enabled = true;
                }

                switch (tipoSaida)
                {
                    case TipoSaida.MAXIFILE:
                        tbxQualidadeAr.Enabled = true;
                        cbxCriterioReceptor.SelectedValue = DBNull.Value;
                        cbxCriterioReceptor.Enabled = false;
                        tbxValorMaximoPoluente.ResetText();
                        tbxValorMaximoPoluente.Enabled = false;
                        break;
                    case TipoSaida.POSTFILE:
                        tbxQualidadeAr.ResetText();
                        tbxQualidadeAr.Enabled = false;
                        cbxCriterioReceptor.SelectedValue = DBNull.Value;
                        cbxCriterioReceptor.Enabled = false;
                        tbxValorMaximoPoluente.ResetText();
                        tbxValorMaximoPoluente.Enabled = false;
                        break;
                    case TipoSaida.PLOTFILE:
                        tbxQualidadeAr.ResetText();
                        tbxQualidadeAr.Enabled = false;
                        cbxCriterioReceptor.SelectedValue = DBNull.Value;
                        cbxCriterioReceptor.Enabled = false;
                        tbxValorMaximoPoluente.ResetText();
                        tbxValorMaximoPoluente.Enabled = false;
                        break;
                    case TipoSaida.RANKFILE:
                        tbxQualidadeAr.ResetText();
                        tbxQualidadeAr.Enabled = false;
                        cbxCriterioReceptor.SelectedValue = DBNull.Value;
                        cbxCriterioReceptor.Enabled = false;
                        tbxValorMaximoPoluente.Enabled = true;
                        break;
                }
            }
        }

        /// <summary>
        /// Inserir saida.
        /// </summary>
        private void InserirSaida()
        {
            dgvSaida.Tag = "INSERIR";

            HabilitarCamposSaidas(true);

            if (dgvSaida.Rows.Count == 0)
            {
                PreencherCamposAutomatico();
            }
            else
            {
                LimparCamposSaidas();
            }

            this.ActiveControl = cbxTipoSaida;
            cbxTipoSaida.Focus();
        }

        /// <summary>
        /// Alterar saída.
        /// </summary>
        private void AlterarSaida()
        {
            if (dgvSaida.CurrentRow != null)
            {
                HabilitarCamposSaidas(true);
                HabilitarCamposTipoSaidas();

                dgvSaida.Tag = "ALTERAR";

                var itemSaida = lstSaidas.FirstOrDefault(I => I.SEQUENCIA == Convert.ToInt32(dgvSaida.CurrentRow.Cells["ID"].Value));
                if (itemSaida != null)
                {
                    cbxTipoSaida.SelectedValue = itemSaida.TIPO_SAIDA;
                    cbxMediaHorariaPoluente.SelectedValue = itemSaida.MEDIA_HORARIA != null ? itemSaida.MEDIA_HORARIA : DBNull.Value;
                    cbxMediaPeriodoPoluente.SelectedValue = itemSaida.MEDIA_PERIODO != null ? itemSaida.MEDIA_PERIODO : DBNull.Value;
                    tbxQualidadeAr.Text = Convert.ToString(itemSaida.PADRAO_QUALIDADE_AR);
                    cbxCriterioReceptor.SelectedValue = itemSaida.CRITERIO_RECEPTOR != null ? itemSaida.CRITERIO_RECEPTOR : DBNull.Value;
                    tbxValorMaximoPoluente.Text = Convert.ToString(itemSaida.VALOR_MAXIMO);
                }                
            }
        }

        /// <summary>
        /// Salvar saída.
        /// </summary>
        private void SalvarSaida()
        {
            if (dgvSaida.Tag == null)
            {
                return;
            }

            #region Validação

            TipoSaida tipoSaida = (TipoSaida)cbxTipoSaida.SelectedValue;
            MediaHoraria? mediaHoraria = cbxMediaHorariaPoluente.Enabled && cbxMediaHorariaPoluente.SelectedValue != DBNull.Value ? (MediaHoraria)cbxMediaHorariaPoluente.SelectedValue : (MediaHoraria?)null;
            MediaPeriodo? mediaPeriodo = cbxMediaPeriodoPoluente.Enabled && cbxMediaPeriodoPoluente.SelectedValue != DBNull.Value ? (MediaPeriodo)cbxMediaPeriodoPoluente.SelectedValue : (MediaPeriodo?)null;
            CriterioReceptor? criterioReceptor = cbxCriterioReceptor.Enabled && cbxCriterioReceptor.SelectedValue != DBNull.Value ? (CriterioReceptor)cbxCriterioReceptor.SelectedValue : (CriterioReceptor?)null;

            if (mediaHoraria.HasValue == false && mediaPeriodo.HasValue == false)
            {
                MessageBox.Show(this, $"Média temporal - {classeHelp.BuscarMensagem(3)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbxMediaHorariaPoluente.Focus();
                return;
            }

            if (tipoSaida == TipoSaida.MAXIFILE)
            {
                if (Convert.ToDecimal(tbxQualidadeAr.Text) == 0)
                {
                    MessageBox.Show(this, $"Qualidade do ar - {classeHelp.BuscarMensagem(3)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tbxQualidadeAr.Focus();
                    return;
                }

                if (mediaPeriodo.HasValue && mediaPeriodo != MediaPeriodo.MONTH)
                {
                    MessageBox.Show(this, $"Média do período - {classeHelp.BuscarMensagem(31)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbxMediaPeriodoPoluente.Focus();
                    return;
                }
            }

            if (tipoSaida == TipoSaida.PLOTFILE)
            {
                if (mediaPeriodo.HasValue && mediaPeriodo.Value == MediaPeriodo.MONTH && criterioReceptor.HasValue == false)
                {
                    MessageBox.Show(this, $"Média do período - {classeHelp.BuscarMensagem(71)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbxMediaPeriodoPoluente.Focus();
                    return;
                }

                if (mediaHoraria.HasValue && criterioReceptor.HasValue == false)
                {
                    MessageBox.Show(this, $"Média horária - {classeHelp.BuscarMensagem(72)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbxMediaHorariaPoluente.Focus();
                    return;
                }
            }

            if (tipoSaida == TipoSaida.RANKFILE)
            {
                if (mediaPeriodo.HasValue && mediaPeriodo != MediaPeriodo.MONTH)
                {
                    MessageBox.Show(this, classeHelp.BuscarMensagem(31), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbxMediaPeriodoPoluente.Focus();
                    return;
                }

                if (Convert.ToInt32(tbxValorMaximoPoluente.Text) == 0)
                {
                    MessageBox.Show(this, $"Valor máximo - {classeHelp.BuscarMensagem(3)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tbxValorMaximoPoluente.Focus();
                    return;
                }
                else if (Convert.ToInt32(tbxValorMaximoPoluente.Text) > 400)
                {
                    MessageBox.Show(this, $"Valor máximo - {classeHelp.BuscarMensagem(54)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tbxValorMaximoPoluente.Focus();
                    return;
                }
                else if (Convert.ToInt32(tbxValorMaximoPoluente.Text) > Convert.ToInt32(tbxValorMaximo.Text))
                {
                    MessageBox.Show(this, $"Valor máximo - {classeHelp.BuscarMensagem(32)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tbxValorMaximoPoluente.Focus();
                    return;
                }
            }

            #region Definições AERMET

            var dtDefinicao = classeAERMET.RetornarRegistroUso();
            if (dtDefinicao.Item1.Rows.Count == 0)
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(56), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {                
                DateTime periodoInicial = Convert.ToDateTime(dtDefinicao.Item1.Rows[0]["PERIODO_INICIAL"]);
                DateTime periodoFinal = Convert.ToDateTime(dtDefinicao.Item1.Rows[0]["PERIODO_FINAL"]);

                TimeSpan ts = periodoFinal.Subtract(periodoInicial);
                DateTime periodo = new DateTime(ts.Ticks);

                int qtdAnos = periodo.Year - 1;

                if (qtdAnos < 1 && periodo.Month <= 1 && mediaPeriodo.HasValue && mediaPeriodo != MediaPeriodo.PERIOD)
                {
                    MessageBox.Show(this, classeHelp.BuscarMensagem(57), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbxMediaPeriodoPoluente.Focus();
                    return;
                }

                if ((periodo.Year - 1) < 1 && mediaPeriodo.HasValue && mediaPeriodo == MediaPeriodo.ANNUAL)
                {
                    MessageBox.Show(this, classeHelp.BuscarMensagem(59), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cbxMediaPeriodoPoluente.Focus();
                    return;
                }
            }

            #endregion            

            int sequencia = dgvSaida.Tag.ToString() == "ALTERAR" ? Convert.ToInt32(dgvSaida.CurrentRow.Cells["ID"].Value) : 0;

            #endregion

            if (sequencia == 0)
            {
                sequencia = dgvSaida.Rows.Count;

                dynamic dados = new ExpandoObject();
                dados.SEQUENCIA = sequencia + 1;
                dados.TIPO_SAIDA = Convert.ToInt32(tipoSaida);
                dados.MEDIA_HORARIA = mediaHoraria.HasValue ? (int)mediaHoraria.Value : (int?)null;
                dados.MEDIA_PERIODO = mediaPeriodo.HasValue ? (int)mediaPeriodo.Value : (int?)null;
                dados.PADRAO_QUALIDADE_AR = Convert.ToDecimal(tbxQualidadeAr.Text);
                dados.CRITERIO_RECEPTOR = criterioReceptor.HasValue ? (int)criterioReceptor.Value : (int?)null;
                dados.VALOR_MAXIMO = Convert.ToInt32(tbxValorMaximoPoluente.Text);
                dados.POLUENTE = Convert.ToInt32(cbxPoluente.SelectedValue);                

                Tuple<string, string, string, string> configuracaoSaida = ConfigurarSaidas(dados);

                dados.DESCRICAO = configuracaoSaida.Item4;

                DataGridViewRow linhaExistente = dgvSaida.Rows.OfType<DataGridViewRow>().FirstOrDefault(I => I.Cells["TIPO_SAIDA"].Value.ToString() == configuracaoSaida.Item1 &&
                                                                                                             I.Cells["MEDIA_TEMPORAL"].Value.ToString() == configuracaoSaida.Item2 &&
                                                                                                             I.Cells["CRITERIO"].Value.ToString() == configuracaoSaida.Item3 &&
                                                                                                             I.Cells["DESCRICAO"].Value.ToString() == configuracaoSaida.Item4);

                if (linhaExistente != null)
                {
                    MessageBox.Show(this, string.Format(classeHelp.BuscarMensagem(34), linhaExistente.Cells["ID"].Value), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }                

                lstSaidas.Add(dados);

                dgvSaida.Rows.Add(false,
                                  dados.SEQUENCIA,
                                  configuracaoSaida.Item1,
                                  configuracaoSaida.Item2,
                                  configuracaoSaida.Item3,
                                  configuracaoSaida.Item4
                                  );

                InserirSaida();
            }
            else
            {
                dynamic dados = lstSaidas.FirstOrDefault(I => I.SEQUENCIA == sequencia);
                if (dados != null)
                {
                    dados.TIPO_SAIDA = Convert.ToInt32(tipoSaida);
                    dados.MEDIA_HORARIA = mediaHoraria.HasValue ? (int)mediaHoraria.Value : (int?)null;
                    dados.MEDIA_PERIODO = mediaPeriodo.HasValue ? (int)mediaPeriodo.Value : (int?)null;
                    dados.PADRAO_QUALIDADE_AR = Convert.ToDecimal(tbxQualidadeAr.Text);
                    dados.CRITERIO_RECEPTOR = criterioReceptor.HasValue ? (int)criterioReceptor.Value : (int?)null;
                    dados.VALOR_MAXIMO = Convert.ToInt32(tbxValorMaximoPoluente.Text);
                    dados.POLUENTE = Convert.ToInt32(cbxPoluente.SelectedValue);

                    Tuple<string, string, string, string> configuracaoSaida = ConfigurarSaidas(dados);

                    dados.DESCRICAO = configuracaoSaida.Item4;

                    DataGridViewRow linhaExistente = dgvSaida.Rows.OfType<DataGridViewRow>().FirstOrDefault(I => Convert.ToInt32(I.Cells["ID"].Value) != sequencia &&
                                                                                                                 I.Cells["TIPO_SAIDA"].Value.ToString() == configuracaoSaida.Item1 &&
                                                                                                                 I.Cells["MEDIA_TEMPORAL"].Value.ToString() == configuracaoSaida.Item2 &&
                                                                                                                 I.Cells["CRITERIO"].Value.ToString() == configuracaoSaida.Item3 &&
                                                                                                                 I.Cells["DESCRICAO"].Value.ToString() == configuracaoSaida.Item4);

                    if (linhaExistente != null)
                    {
                        MessageBox.Show(this, string.Format(classeHelp.BuscarMensagem(34), linhaExistente.Cells["ID"].Value), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    dgvSaida.CurrentRow.Cells["TIPO_SAIDA"].Value = configuracaoSaida.Item1;
                    dgvSaida.CurrentRow.Cells["MEDIA_TEMPORAL"].Value = configuracaoSaida.Item2;
                    dgvSaida.CurrentRow.Cells["CRITERIO"].Value = configuracaoSaida.Item3;
                    dgvSaida.CurrentRow.Cells["DESCRICAO"].Value = configuracaoSaida.Item4;                    

                    dgvSaida.Focus();
                }
            }
        }

        /// <summary>
        /// Excluir saída.
        /// </summary>
        private void ExcluirSaida()
        {
            if (dgvSaida.CurrentRow != null)
            {
                Boolean marcado = dgvSaida.Rows.OfType<DataGridViewRow>().Any(f => Convert.ToBoolean(f.Cells[0].Value) == true);

                if (marcado.Equals(false))
                {
                    dgvSaida.CurrentRow.Cells[0].Value = true;
                    dgvSaida.RefreshEdit();
                }

                DialogResult dialogResult = MessageBox.Show(this, classeHelp.BuscarMensagem(1), classeHelp.BuscarMensagem(2), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    FrmLoading frmLoading = new FrmLoading(this);

                    Thread thrExcluir = new Thread(delegate ()
                    {
                        try
                        {
                            int count = 0;

                            for (Int32 i = dgvSaida.Rows.Count - 1; i >= 0; i--)
                            {
                                if (Convert.ToBoolean(dgvSaida.Rows[i].Cells[0].Value) == true)
                                {
                                    count++;
                                    frmLoading.AtualizarStatus(count);

                                    dynamic registro = lstSaidas.FirstOrDefault(I => I.SEQUENCIA == Convert.ToInt32(dgvSaida.Rows[i].Cells["ID"].Value));
                                    if (registro != null)
                                    {
                                        lstSaidas.Remove(registro);
                                    }

                                    CrossThreadOperation.Invoke(this, delegate
                                    {
                                        dgvSaida.Rows.Remove(dgvSaida.Rows[i]);
                                    });
                                }
                            }

                            AtualizarSequencia();
                        }
                        catch { }
                    });
                    thrExcluir.Start();

                    frmLoading.thread = thrExcluir;
                    frmLoading.Texto = "Excluindo registro(s)...";
                    frmLoading.Style = ProgressBarStyle.Blocks;
                    frmLoading.Maximum = dgvSaida.Rows.OfType<DataGridViewRow>().Count(f => Convert.ToBoolean(f.Cells[0].Value));
                    frmLoading.ShowDialogFade(this);

                    if (dgvSaida.RowCount == 0)
                    {
                        InserirSaida();
                    }
                }
                else if (marcado == false)
                {
                    dgvSaida.CurrentRow.Cells[0].Value = false;
                    dgvSaida.RefreshEdit();
                }
            }
        }

        /// <summary>
        /// Atualizar código da sequência das saídas.
        /// </summary>
        private void AtualizarSequencia()
        {
            int count = 0;
            foreach (DataGridViewRow item in dgvSaida.Rows)
            {
                count++;

                var dados = lstSaidas.FirstOrDefault(I => I.SEQUENCIA == Convert.ToInt32(item.Cells["ID"].Value));
                if (dados != null)
                {
                    dados.SEQUENCIA = count;
                }                

                item.Cells["ID"].Value = count;                
            }
        }

        /// <summary>
        /// Carregar saídas selecionada no gridView.
        /// </summary>
        private void CarregarSaidaSelecionada()
        {
            if (dgvSaida.CurrentRow != null)
            {
                var dados = lstSaidas.FirstOrDefault(I => I.SEQUENCIA == Convert.ToInt32(dgvSaida.CurrentRow.Cells["ID"].Value));
                if (dados != null)
                {
                    cbxTipoSaida.SelectedValue = dados.TIPO_SAIDA;
                    cbxMediaHorariaPoluente.SelectedValue = dados.MEDIA_HORARIA != null ? dados.MEDIA_HORARIA : DBNull.Value;
                    cbxMediaPeriodoPoluente.SelectedValue = dados.MEDIA_PERIODO != null ? dados.MEDIA_PERIODO : DBNull.Value;
                    tbxQualidadeAr.Text = Convert.ToString(dados.PADRAO_QUALIDADE_AR);
                    cbxCriterioReceptor.SelectedValue = dados.CRITERIO_RECEPTOR != null ? dados.CRITERIO_RECEPTOR : DBNull.Value;
                    tbxValorMaximoPoluente.Text = Convert.ToString(dados.VALOR_MAXIMO);
                }                
            }
        }

        #endregion

        #endregion

        #region Eventos FrmDefinicaoAERMOD

        private void FrmDefinicaoAERMOD_Load(object sender, EventArgs e)
        {
            dgvSaida.ScrollBarVisible(true);
            dgvDados.ScrollBarVisible(true);
            dgvRetangulo.ScrollBarVisible(true);

            CarregarComboBox();
            CarregarRegistros();

            if (consulta)
            {
                tabControlDados.SelectedTab = tabPageConsulta;
            }
        }

        private void FrmDefinicaoAERMOD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt)
            {
                switch (e.KeyCode)
                {
                    case Keys.A:
                        e.SuppressKeyPress = true;

                        if (tabControlDados.SelectedTab == tabPageConsulta)
                        {
                            AlterarRegistro();
                        }
                        else if (tabControlCadastro.SelectedTab == tabPageSaidas && dgvSaida.Focused)
                        {
                            AlterarSaida();
                        }
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

                        if (tabControlDados.SelectedTab == tabPageCadastro)
                        {
                            ConsultarRegistros();
                        }
                        break;
                    case Keys.Insert:
                        e.SuppressKeyPress = true;

                        if (tabControlDados.SelectedTab == tabPageConsulta)
                        {
                            InserirRegistro(false);
                        }
                        else if (tabControlCadastro.SelectedTab == tabPageSaidas && dgvSaida.Focused)
                        {
                            InserirSaida();
                        }
                        break;
                    case Keys.Delete:
                        e.SuppressKeyPress = true;

                        if (tabControlDados.SelectedTab == tabPageConsulta)
                        {
                            ExcluirRegistro();
                        }
                        else if (tabControlCadastro.SelectedTab == tabPageSaidas && dgvSaida.Focused)
                        {
                            ExcluirSaida();
                        }
                        break;
                    case Keys.Escape:
                        e.SuppressKeyPress = true;

                        SairForm();
                        break;
                    case Keys.Enter:
                        e.SuppressKeyPress = true;

                        if (tabControlDados.SelectedTab == tabPageConsulta)
                        {                           
                            if (consulta == false)
                            {
                                AtualizarUso();
                            }
                            else
                            {
                                CapturarRegistro();
                            }
                        }
                        else if (tabControlCadastro.SelectedTab == tabPageSaidas && dgvSaida.Focused == false)
                        {
                            SalvarSaida();
                        }
                        else
                        {
                            SalvarRegistro();
                        }
                        break;
                }
            }
        }

        private void FrmDefinicaoAERMOD_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tabControlDados.SelectedTab == tabPageCadastro && dgvDados.Rows.Count > 0)
            {
                dgvDados.Focus();
                tabControlDados.SelectedTab = tabPageConsulta;
                e.Cancel = true;
            }
        }

        #endregion

        #region Eventos tabControlDados

        private void tabControlDados_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == tabPageCadastro && consulta)
            {
                dgvDados.Focus();
                e.Cancel = true;
            }
        }

        private void tabControlDados_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlDados.SelectedTab == tabPageCadastro)
            {
                if (dgvDados.Tag == null)
                {
                    InserirRegistro(false);
                }
            }
            else
            {
                this.ActiveControl = dgvDados;
                dgvDados.Focus();
            }
        }

        #endregion

        #region Eventos tabControlCadastro

        private void tabControlCadastro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlCadastro.SelectedTab == tabPageDadosBasicos)
            {
                cbxMediaHoraria.Focus();
            }
            else
            {
                if (dgvSaida.Rows.Count > 0)
                {
                    dgvSaida.Focus();
                }
                else
                {
                    InserirSaida();
                }
            }
        }

        #endregion

        #region Eventos dgvDados

        private void dgvDados_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDados.CurrentCell != null && dgvDados.CurrentCell.ColumnIndex == 0)
            {
                dgvDados.ReadOnly = true;
            }
        }

        private void dgvDados_Enter(object sender, EventArgs e)
        {
            dgvDados.Tag = null;
        }

        private void dgvDados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (consulta == false)
            {
                AlterarRegistro();
            }
            else
            {
                CapturarRegistro();
            }
        }

        #endregion

        #region Eventos statusStripConsulta

        private void btnAjudaConsulta_ButtonClick(object sender, EventArgs e)
        {
            AbrirAjuda();
        }

        private void btnInserirConsulta_ButtonClick(object sender, EventArgs e)
        {
            InserirRegistro(false);
        }

        private void btnAlterarConsulta_ButtonClick(object sender, EventArgs e)
        {
            AlterarRegistro();
        }

        private void btnExcluirConsulta_ButtonClick(object sender, EventArgs e)
        {
            ExcluirRegistro();
        }

        private void btnSairConsulta_ButtonClick(object sender, EventArgs e)
        {
            SairForm();
        }

        #endregion

        #region Eventos statusStripCadastro

        private void btnAjudaCadastro_ButtonClick(object sender, EventArgs e)
        {
            AbrirAjuda();
        }

        private void btnConsultaCadastro_ButtonClick(object sender, EventArgs e)
        {
            ConsultarRegistros();
        }

        private void btnSalvarCadastro_ButtonClick(object sender, EventArgs e)
        {
            SalvarRegistro();
        }

        private void btnSairCadastro_ButtonClick(object sender, EventArgs e)
        {
            SairForm();
        }

        #endregion

        #region Eventos CRUD saídas

        private void btnInserirSaida_Click(object sender, EventArgs e)
        {
            InserirSaida();
        }

        private void btnAlterarSaida_Click(object sender, EventArgs e)
        {
            AlterarSaida();
        }

        private void btnExcluirSaida_Click(object sender, EventArgs e)
        {
            ExcluirSaida();
        }

        private void btnSalvarSaida_Click(object sender, EventArgs e)
        {
            SalvarSaida();
        }

        #endregion

        #region Eventos cbxTipoSaida

        private void cbxTipoSaida_SelectedValueChanged(object sender, EventArgs e)
        {
            HabilitarCamposTipoSaidas();            
        }

        #endregion

        #region Eventos cbxMediaHorariaPoluente

        private void cbxMediaHorariaPoluente_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbxMediaHorariaPoluente.SelectedValue != null && cbxMediaHorariaPoluente.SelectedValue != DBNull.Value)
            {
                cbxMediaPeriodoPoluente.SelectedValue = DBNull.Value;
                cbxMediaPeriodoPoluente.Enabled = false;

                if (((TipoSaida)Convert.ToInt32(cbxTipoSaida.SelectedValue)) == TipoSaida.PLOTFILE)
                {
                    cbxCriterioReceptor.Enabled = true;
                }
                else
                {
                    cbxCriterioReceptor.Enabled = false;
                }
            }
            else
            {
                cbxMediaPeriodoPoluente.Enabled = true;
            }
        }

        #endregion

        #region Eventos cbxMediaPeriodoPoluente

        private void cbxMediaPeriodoPoluente_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbxMediaPeriodoPoluente.SelectedValue != null && cbxMediaPeriodoPoluente.SelectedValue != DBNull.Value)
            {
                cbxMediaHorariaPoluente.SelectedValue = DBNull.Value;
                cbxMediaHorariaPoluente.Enabled = false;

                if (((TipoSaida)Convert.ToInt32(cbxTipoSaida.SelectedValue)) == TipoSaida.PLOTFILE)
                {
                    if ((MediaPeriodo)Convert.ToInt32(cbxMediaPeriodoPoluente.SelectedValue) != MediaPeriodo.MONTH)
                    {
                        cbxCriterioReceptor.SelectedValue = DBNull.Value;
                        cbxCriterioReceptor.Enabled = false;
                    }
                    else
                    {
                        cbxCriterioReceptor.Enabled = true;
                    }
                }
            }
            else
            {
                cbxMediaHorariaPoluente.Enabled = true;               
            }            
        }

        #endregion

        #region Eventos dgvSaida

        private void dgvSaida_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvSaida.CurrentCell != null && dgvSaida.CurrentCell.ColumnIndex == 0)
            {
                dgvSaida.ReadOnly = true;
            }
        }

        private void dgvSaida_Enter(object sender, EventArgs e)
        {
            dgvSaida.Tag = null;
            HabilitarCamposSaidas(false);
            CarregarSaidaSelecionada();
        }

        private void dgvSaida_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AlterarSaida();
        }        

        private void dgvSaida_SelectionChanged(object sender, EventArgs e)
        {
            CarregarSaidaSelecionada();
        }

        #endregion

        #region Eventos dgvRetangulo

        private void dgvRetangulo_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            switch (dgvRetangulo.CurrentCell.ColumnIndex)
            {
                case 1:
                    if (e.Control is TextBox)
                    {                        
                        ((TextBox)e.Control).MaxLength = 3;                        
                        ((TextBox)e.Control).KeyPress += tbx_KeyPress;
                    }
                    break;                
            }
        }

        private void dgvRetangulo_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (((DataGridView)sender).CurrentCell.IsInEditMode == true && e.ColumnIndex == 1)
            {
                if (e.FormattedValue.ToString() == string.Empty || Convert.ToInt32(e.FormattedValue) <= 0)
                {
                    MessageBox.Show(this, classeHelp.BuscarMensagem(3), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                }                
            }
        }

        private void dgvRetangulo_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRetangulo.CurrentCell != null && dgvRetangulo.CurrentCell.ColumnIndex == 0)
            {
                dgvRetangulo.ReadOnly = true;
            }
        }

        private void dgvRetangulo_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            ((DataGridView)sender).ReadOnly = true;
            ((DataGridView)sender).RefreshEdit();
        }

        private void tbx_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Form.ModifierKeys != Keys.Control && !Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8) //permite apenas inserção de números  (char)8) = tecla backapace
            {
                e.Handled = true;
            }
        }

        #endregion
    }
}
