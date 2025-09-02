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
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AERMOD.CamadaApresentacao.AERMET
{
    public partial class FrmDefinicaoAERMET : Form
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

        /// <summary>
        /// lista com as características de superfície.
        /// </summary>
        List<dynamic> lstCaracteristicas = new List<dynamic>();

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
        public FrmDefinicaoAERMET(bool consulta = false)
        {
            InitializeComponent();

            this.Icon = Properties.Resources.form2.ConvertImageToIcon();
            this.consulta = consulta;
        }

        #endregion

        #region Métodos

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
                DataTable dt = classeAERMET.RetornaDados();

                frmLoading.Style = ProgressBarStyle.Blocks;
                frmLoading.Maximum = dt.Rows.Count;

                CrossThreadOperation.Invoke(this, delegate
                {
                    int count = 0;
                    foreach (DataRow item in dt.Rows)
                    {
                        count++;
                        frmLoading.AtualizarStatus(count);

                        int codigoEstado = item["ESTADO"].ValidarValor<int>(0);

                        dgvDados.Rows.Add(false,
                                          item["CODIGO"],
                                          item["LOCAL"],
                                          codigoEstado > 0 ? ((UF)codigoEstado).GetEnumDescription() : string.Empty,
                                          $"{Convert.ToDateTime(item["PERIODO_INICIAL"]).ToString("dd/MM/yyyy")} - {Convert.ToDateTime(item["PERIODO_FINAL"]).ToString("dd/MM/yyyy")}"
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
                tbxPeriodoInicial.Text = Convert.ToDateTime($"01/{DateTime.Now.Month}/{DateTime.Now.Year}").ToString("dd/MM/yyyy");
                int ultimoDia = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                tbxPeriodoFinal.Text = Convert.ToDateTime($"{ultimoDia}/{DateTime.Now.Month}/{DateTime.Now.Year}").ToString("dd/MM/yyyy");
            }
            else
            {
                LimparCampos();
            }

            dgvDados.Tag = "INSERIR";
            tabControlDados.SelectedTab = tabPageCadastro;
            tabControlCadastro.SelectedTab = tabPageDadosBasicos;

            this.ActiveControl = tbxLocal;
            tbxLocal.Focus();                      

            lstCaracteristicas.Clear();
            dgvDefinicao.Rows.Clear();
        }

        /// <summary>
        /// Limpar campos.
        /// </summary>
        private void LimparCampos()
        {
            tbxLocal.ResetText();
            tbxPeriodoInicial.ResetText();
            tbxPeriodoFinal.ResetText();
            cbxEstacao.SelectedValue = 41;
            tbxX.ResetText();
            tbxY.ResetText();
        }

        /// <summary>
        /// Limpar campos.
        /// </summary>
        private void LimparCamposCaracteristicas()
        {
            cbxFrequencia.SelectedValue = 0;
            tbxSetorInicial.ResetText();
            tbxSetorFinal.ResetText();
            cbxEstacao.SelectedValue = 1;
            tbxAlbedo.ResetText();
            tbxBowen.ResetText();
            tbxRugosidade.ResetText();
        }

        /// <summary>
        /// Preenchimento de campos automático.
        /// </summary>
        private void PreencherCamposAutomatico()
        {
            cbxFrequencia.SelectedValue = (int)FrequenciaSetor.ANUAL;
            tbxSetorInicial.Text = "0";
            tbxSetorFinal.Text = "360";
            cbxEstacao.SelectedValue = (int)Estacao.INVERNO;
            tbxAlbedo.Text = "0,20";
            tbxBowen.Text = "2,00";
            tbxRugosidade.Text = "1,00";
        }

        /// <summary>
        /// Alterar registro.
        /// </summary>
        private void AlterarRegistro()
        {
            if (dgvDados.Focused && dgvDados.CurrentRow != null)
            {
                lstCaracteristicas.Clear();
                int codigo = Convert.ToInt32(dgvDados.CurrentRow.Cells["CODIGO"].Value);

                var registro = classeAERMET.RetornaDados(codigo);

                if (registro != null && registro.Item1.Rows.Count > 0)
                {
                    tbxLocal.Text = registro.Item1.Rows[0]["LOCAL"].ToString();
                    cbxUF.SelectedValue = registro.Item1.Rows[0]["ESTADO"] != null && registro.Item1.Rows[0]["ESTADO"] != DBNull.Value ? registro.Item1.Rows[0]["ESTADO"] : 41;
                    tbxPeriodoInicial.Text = registro.Item1.Rows[0]["PERIODO_INICIAL"].ToString();
                    tbxPeriodoFinal.Text = registro.Item1.Rows[0]["PERIODO_FINAL"].ToString();
                    tbxX.Text = registro.Item1.Rows[0]["X"].ToString();
                    tbxY.Text = registro.Item1.Rows[0]["Y"].ToString();

                    dgvDefinicao.Rows.Clear();

                    DataTable dtCaracteristica = registro.Item2;

                    foreach (DataRow item in dtCaracteristica.Rows)
                    {
                        dgvDefinicao.Rows.Add(false,
                                              item["SEQUENCIA"],
                                              item["FREQUENCIA"],
                                           $"{item["SETOR_INICIAL"]} - {item["SETOR_FINAL"]}",                                              
                                              item["ESTACAO"],
                                              item["ALBEDO"],
                                              item["BOWEN"],
                                              item["RUGOSIDADE"]);

                        dynamic dados = new ExpandoObject();
                        dados.CODIGO = codigo;
                        dados.SEQUENCIA = Convert.ToInt32(item["SEQUENCIA"]);
                        dados.FREQUENCIA = Convert.ToInt32(item["FREQUENCIA"]);
                        dados.ESTACAO = Convert.ToInt32(item["ESTACAO"]);
                        dados.SETOR_INICIAL = Convert.ToInt32(item["SETOR_INICIAL"]);
                        dados.SETOR_FINAL = Convert.ToInt32(item["SETOR_FINAL"]);
                        dados.ALBEDO = Convert.ToDecimal(item["ALBEDO"]);
                        dados.BOWEN = Convert.ToDecimal(item["BOWEN"]);
                        dados.RUGOSIDADE = Convert.ToDecimal(item["RUGOSIDADE"]);

                        lstCaracteristicas.Add(dados);
                    }                    
                }
                
                dgvDados.Tag = "ALTERAR";
                tabControlDados.SelectedTab = tabPageCadastro;
                tabControlCadastro.SelectedTab = tabPageDadosBasicos;

                tbxLocal.Focus();
            }
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

                                    classeAERMET.Excluir(Convert.ToInt32(dgvDados.Rows[i].Cells["CODIGO"].Value));

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

            if (string.IsNullOrEmpty(tbxLocal.Text))
            {
                MessageBox.Show(this, $"Local - {classeHelp.BuscarMensagem(3)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxLocal.Focus();
                return;
            }

            if (string.IsNullOrEmpty(tbxPeriodoInicial.Text))
            {
                MessageBox.Show(this, $"Período inicial - {classeHelp.BuscarMensagem(3)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxPeriodoInicial.Focus();
                return;
            }

            if (string.IsNullOrEmpty(tbxPeriodoFinal.Text))
            {
                MessageBox.Show(this, $"Período final - {classeHelp.BuscarMensagem(3)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxPeriodoFinal.Focus();
                return;
            }

            if (tbxPeriodoInicial.Text.ValidarData() == false)
            {
                MessageBox.Show(this, $"Período inicial - {classeHelp.BuscarMensagem(18)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxPeriodoInicial.Focus();
                return;
            }

            if (tbxPeriodoFinal.Text.ValidarData() == false)
            {
                MessageBox.Show(this, $"Período final - {classeHelp.BuscarMensagem(18)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxPeriodoFinal.Focus();
                return;
            }

            if (Convert.ToDateTime(tbxPeriodoInicial.Text) > Convert.ToDateTime(tbxPeriodoFinal.Text))
            {
                MessageBox.Show(this, $"Período inicial - {classeHelp.BuscarMensagem(19)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxPeriodoInicial.Focus();
                return;
            }

            if (Convert.ToDecimal(tbxX.Text) == 0)
            {
                MessageBox.Show(this, $"X (UTM) - {classeHelp.BuscarMensagem(3)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxX.Focus();
                return;
            }

            if (Convert.ToDecimal(tbxY.Text) == 0)
            {
                MessageBox.Show(this, $"Y (UTM) - {classeHelp.BuscarMensagem(3)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxY.Focus();
                return;
            }

            int codigo = dgvDados.Tag.ToString() == "ALTERAR" ? Convert.ToInt32(dgvDados.CurrentRow.Cells["CODIGO"].Value) : 0;


            int retorno = classeAERMET.VerificarDuplicidade(codigo, tbxLocal.Text, Convert.ToDateTime(tbxPeriodoInicial.Text), Convert.ToDateTime(tbxPeriodoFinal.Text), Convert.ToDecimal(tbxX.Text), Convert.ToDecimal(tbxY.Text));
            if (retorno > 0)
            {
                string ID = retorno.ToString();
                string msg = $"{classeHelp.BuscarMensagem(20)}\nID: {ID}";

                MessageBox.Show(this, msg, classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxLocal.Focus();
                return;
            }

            if (lstCaracteristicas.Count == 0)
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(21), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);

                tabControlDados.SelectedTab = tabPageCadastro;
                tabControlCadastro.SelectedTab = tabPageCaracteristicas;

                InserirCaracteristica();
                return;
            }

            #endregion

            dynamic dados = new ExpandoObject();
            dados.CODIGO = codigo;
            dados.LOCAL = tbxLocal.Text;
            dados.ESTADO = Convert.ToInt32(cbxUF.SelectedValue);
            dados.PERIODO_INICIAL = Convert.ToDateTime(tbxPeriodoInicial.Text);
            dados.PERIODO_FINAL = Convert.ToDateTime(tbxPeriodoFinal.Text);
            dados.X = Convert.ToDecimal(tbxX.Text);
            dados.Y = Convert.ToDecimal(tbxY.Text);

            codigo = classeAERMET.Atualizar(dados, lstCaracteristicas);
            if (codigo > 0)
            {
                if (dgvDados.Tag.ToString() == "INSERIR")
                {
                    dgvDados.Rows.Add(false,
                                      codigo,
                                      tbxLocal.Text,
                                      ((UF)Convert.ToInt32(cbxUF.SelectedValue)).GetEnumDescription(), 
                                      $"{Convert.ToDateTime(tbxPeriodoInicial.Text).ToString("dd/MM/yyyy")} - {Convert.ToDateTime(tbxPeriodoFinal.Text).ToString("dd/MM/yyyy")}"
                                      );

                    MessageBox.Show(this, classeHelp.BuscarMensagem(7), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    InserirRegistro(false);

                    if (dgvDados.Rows.Count == 1)
                    {
                        classeAERMET.AtualizarUso(codigo);
                    }
                }
                else
                {
                    dgvDados.CurrentRow.Cells["LOCAL"].Value = tbxLocal.Text;
                    dgvDados.CurrentRow.Cells["ESTADO"].Value = ((UF)Convert.ToInt32(cbxUF.SelectedValue)).GetEnumDescription();
                    dgvDados.CurrentRow.Cells["PERIODO"].Value = $"{Convert.ToDateTime(tbxPeriodoInicial.Text).ToString("dd/MM/yyyy")} - {Convert.ToDateTime(tbxPeriodoFinal.Text).ToString("dd/MM/yyyy")}";

                    dgvDados.Focus();
                    tabControlDados.SelectedTab = tabPageConsulta;
                }
            }
            else
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(5), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxLocal.Focus();
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

                classeAERMET.AtualizarUso(codigo);
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
            if (tabControlDados.SelectedTab == tabPageCadastro && tabControlCadastro.SelectedTab == tabPageCaracteristicas)
            {
                if (dgvDefinicao.Rows.Count > 0)
                {
                    if (dgvDefinicao.Focused == false)
                    {
                        dgvDefinicao.Focus();
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
            #region Frequência

            DataTable dtFrequencia = Enumeradores.RetornarFrequenciaSetor();

            cbxFrequencia.DisplayMember = "DESCRICAO";
            cbxFrequencia.ValueMember = "CODIGO";
            cbxFrequencia.DataSource = dtFrequencia;

            #endregion

            #region Estação

            DataTable dtEstacao = Enumeradores.RetornarEstacao();            

            cbxEstacao.DisplayMember = "DESCRICAO";
            cbxEstacao.ValueMember = "CODIGO";
            cbxEstacao.DataSource = dtEstacao;

            #endregion
        }

        /// <summary>
        /// Carregar todos os estados.
        /// </summary>
        private void CarregarEstado()
        {
            DataTable dtEstado = Enumeradores.RetornarUF();

            cbxUF.DisplayMember = "DESCRICAO";
            cbxUF.ValueMember = "CODIGO";
            cbxUF.DataSource = dtEstado;
            cbxUF.SelectedValue = 41;
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

        #region Características

        /// <summary>
        /// Habilitar/Desabilitar campos.
        /// </summary>
        /// <param name="habilitar">Habilitar/Desabilitar</param>
        private void HabilitarCamposCaracteristica(bool habilitar)
        {
            cbxFrequencia.Enabled = habilitar;
            tbxSetorInicial.Enabled = habilitar;
            tbxSetorFinal.Enabled = habilitar;
            cbxEstacao.Enabled = habilitar;
            tbxAlbedo.Enabled = habilitar;
            tbxBowen.Enabled = habilitar;
            tbxRugosidade.Enabled = habilitar;

            cbxFrequencia.Focus();

            if (habilitar)
            {
                if (cbxFrequencia.Focused)
                {
                    btnInserirCaracteristica.Enabled = false;
                    btnAlterarCaracteristica.Enabled = false;
                    btnExcluirCaracteristica.Enabled = false;
                    btnSalvarCaracteristica.Enabled = true;
                }
            }
            else
            {
                btnInserirCaracteristica.Enabled = true;
                btnAlterarCaracteristica.Enabled = true;
                btnExcluirCaracteristica.Enabled = true;
                btnSalvarCaracteristica.Enabled = false;
            }
        }

        /// <summary>
        /// Inserir características.
        /// </summary>
        private void InserirCaracteristica()
        {
            if (dgvDefinicao.Rows.Count == 12)
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(25), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            dgvDefinicao.Tag = "INSERIR";

            HabilitarCamposCaracteristica(true);

            if (dgvDefinicao.Rows.Count == 0)
            {
                PreencherCamposAutomatico();
            }
            else
            {
                LimparCamposCaracteristicas();
            }                        

            this.ActiveControl = cbxFrequencia;
            cbxFrequencia.Focus();            
        }

        /// <summary>
        /// Alterar características.
        /// </summary>
        private void AlterarCaracteristica()
        {
            if (dgvDefinicao.CurrentRow != null)
            {
                HabilitarCamposCaracteristica(true);

                dgvDefinicao.Tag = "ALTERAR";

                cbxFrequencia.SelectedValue = dgvDefinicao.CurrentRow.Cells["FREQUENCIA"].Value;
                string setor = dgvDefinicao.CurrentRow.Cells["SETOR"].Value.ToString();
                tbxSetorInicial.Text = setor.Split('-')[0].TrimEnd(' ');
                tbxSetorFinal.Text = setor.Split('-')[1].TrimStart(' ');
                tbxAlbedo.Text = dgvDefinicao.CurrentRow.Cells["ALBEDO"].Value.ToString();
                tbxBowen.Text = dgvDefinicao.CurrentRow.Cells["BOWEN"].Value.ToString();
                tbxRugosidade.Text = dgvDefinicao.CurrentRow.Cells["RUGOSIDADE"].Value.ToString();
            }
        }

        /// <summary>
        /// Salvar características.
        /// </summary>
        private void SalvarCaracteristica()
        {
            if (dgvDefinicao.Tag == null)
            {
                return;
            }

            #region Validação

            FrequenciaSetor frequencia = (FrequenciaSetor)cbxFrequencia.SelectedValue;
            int setorInicial = Convert.ToInt32(tbxSetorInicial.Text);
            int setorFinal = Convert.ToInt32(tbxSetorFinal.Text);
            Estacao estacao = (Estacao)cbxEstacao.SelectedValue;
            decimal albedo = Convert.ToDecimal(tbxAlbedo.Text);
            decimal bowen = Convert.ToDecimal(tbxBowen.Text);
            decimal rugosidade = Convert.ToDecimal(tbxRugosidade.Text);            

            if (setorInicial == 0 && setorFinal == 0)
            {
                MessageBox.Show(this, $"Setor - {classeHelp.BuscarMensagem(3)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxSetorInicial.Focus();
                return;
            }

            if (setorInicial > 360)
            {
                MessageBox.Show(this, $"Setor - {classeHelp.BuscarMensagem(26)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxSetorInicial.Focus();
                return;
            }

            if (setorFinal > 360)
            {
                MessageBox.Show(this, $"Setor - {classeHelp.BuscarMensagem(26)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxSetorFinal.Focus();
                return;
            }

            if (albedo == 0)
            {
                MessageBox.Show(this, $"Albedo - {classeHelp.BuscarMensagem(3)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxAlbedo.Focus();
                return;
            }

            if (bowen == 0)
            {
                MessageBox.Show(this, $"Bowen - {classeHelp.BuscarMensagem(3)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxBowen.Focus();
                return;
            }

            if (rugosidade == 0)
            {
                MessageBox.Show(this, $"Rugosidade - {classeHelp.BuscarMensagem(3)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxRugosidade.Focus();
                return;
            }            

            int sequencia = dgvDefinicao.Tag.ToString() == "ALTERAR" ? Convert.ToInt32(dgvDefinicao.CurrentRow.Cells["ID"].Value) : 0;

            #endregion

            if (sequencia == 0)
            {
                dynamic dados = new ExpandoObject();
                dados.SEQUENCIA = sequencia + 1;
                dados.FREQUENCIA = Convert.ToInt32(cbxFrequencia.SelectedValue);
                dados.ESTACAO = Convert.ToInt32(cbxEstacao.SelectedValue);
                dados.SETOR_INICIAL = Convert.ToInt32(tbxSetorInicial.Text);
                dados.SETOR_FINAL = Convert.ToInt32(tbxSetorFinal.Text);
                dados.ALBEDO = Convert.ToDecimal(tbxAlbedo.Text);
                dados.BOWEN = Convert.ToDecimal(tbxBowen.Text);
                dados.RUGOSIDADE = Convert.ToDecimal(tbxRugosidade.Text);

                lstCaracteristicas.Add(dados);

                dgvDefinicao.Rows.Add(false,
                                      dados.SEQUENCIA,
                                      dados.FREQUENCIA,
                                   $"{dados.SETOR_INICIAL} - {dados.SETOR_FINAL}",                                      
                                      dados.ESTACAO,
                                      dados.ALBEDO,
                                      dados.BOWEN,
                                      dados.RUGOSIDADE
                                      );                

                InserirCaracteristica();
            }
            else
            {
                dynamic dados = lstCaracteristicas.FirstOrDefault(I => I.SEQUENCIA == sequencia);
                if (dados != null)
                {
                    dados.FREQUENCIA = Convert.ToInt32(cbxFrequencia.SelectedValue);
                    dados.ESTACAO = Convert.ToInt32(cbxEstacao.SelectedValue);
                    dados.SETOR_INICIAL = Convert.ToInt32(tbxSetorInicial.Text);
                    dados.SETOR_FINAL = Convert.ToInt32(tbxSetorFinal.Text);
                    dados.ALBEDO = Convert.ToDecimal(tbxAlbedo.Text);
                    dados.BOWEN = Convert.ToDecimal(tbxBowen.Text);
                    dados.RUGOSIDADE = Convert.ToDecimal(tbxRugosidade.Text);

                    dgvDefinicao.CurrentRow.Cells["FREQUENCIA"].Value = dados.FREQUENCIA;
                    dgvDefinicao.CurrentRow.Cells["SETOR"].Value = $"{dados.SETOR_INICIAL} - {dados.SETOR_FINAL}";
                    dgvDefinicao.CurrentRow.Cells["ESTACAO"].Value = dados.ESTACAO;
                    dgvDefinicao.CurrentRow.Cells["ALBEDO"].Value = dados.ALBEDO;
                    dgvDefinicao.CurrentRow.Cells["BOWEN"].Value = dados.BOWEN;
                    dgvDefinicao.CurrentRow.Cells["RUGOSIDADE"].Value = dados.RUGOSIDADE;                    

                    dgvDefinicao.Focus();                    
                }
            }
        }

        /// <summary>
        /// Excluir características.
        /// </summary>
        private void ExcluirCaracteristica()
        {
            if (dgvDefinicao.CurrentRow != null)
            {
                Boolean marcado = dgvDefinicao.Rows.OfType<DataGridViewRow>().Any(f => Convert.ToBoolean(f.Cells[0].Value) == true);

                if (marcado.Equals(false))
                {
                    dgvDefinicao.CurrentRow.Cells[0].Value = true;
                    dgvDefinicao.RefreshEdit();
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

                            for (Int32 i = dgvDefinicao.Rows.Count - 1; i >= 0; i--)
                            {
                                if (Convert.ToBoolean(dgvDefinicao.Rows[i].Cells[0].Value) == true)
                                {
                                    count++;
                                    frmLoading.AtualizarStatus(count);

                                    dynamic registro = lstCaracteristicas.FirstOrDefault(I => I.SEQUENCIA == Convert.ToInt32(dgvDefinicao.Rows[i].Cells["ID"].Value));
                                    if (registro != null)
                                    {
                                        lstCaracteristicas.Remove(registro);
                                    }

                                    CrossThreadOperation.Invoke(this, delegate
                                    {
                                        dgvDefinicao.Rows.Remove(dgvDefinicao.Rows[i]);
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
                    frmLoading.Maximum = dgvDefinicao.Rows.OfType<DataGridViewRow>().Count(f => Convert.ToBoolean(f.Cells[0].Value));
                    frmLoading.ShowDialogFade(this);

                    if (dgvDefinicao.RowCount == 0)
                    {
                        InserirCaracteristica();
                    }
                }
                else if (marcado == false)
                {
                    dgvDefinicao.CurrentRow.Cells[0].Value = false;
                    dgvDefinicao.RefreshEdit();
                }
            }
        }

        /// <summary>
        /// Atualizar código da sequência das características.
        /// </summary>
        private void AtualizarSequencia()
        {
            lstCaracteristicas.Clear();

            int count = 0;
            foreach (DataGridViewRow item in dgvDefinicao.Rows)
            {
                count++;

                item.Cells["ID"].Value = count;

                dynamic dados = new ExpandoObject();
                dados.SEQUENCIA = count;
                dados.FREQUENCIA = item.Cells["FREQUENCIA"].Value;
                string setor = item.Cells["SETOR"].Value.ToString();
                dados.SETOR_INICIAL = setor.Split('-')[0].TrimEnd(' ');
                dados.SETOR_FINAL = setor.Split('-')[1].TrimEnd(' ');
                dados.ESTACAO = item.Cells["ESTACAO"].Value;
                dados.ALBEDO = item.Cells["ALBEDO"].Value;
                dados.BOWEN = item.Cells["BOWEN"].Value;
                dados.RUGOSIDADE = item.Cells["RUGOSIDADE"].Value;
            }
        }

        /// <summary>
        /// Carregar característica selecionada no gridView.
        /// </summary>
        private void CarregarCaracteristicaSelecionada()
        {
            if (dgvDefinicao.CurrentRow != null)
            {
                cbxFrequencia.SelectedValue = dgvDefinicao.CurrentRow.Cells["FREQUENCIA"].Value;
                string setor = dgvDefinicao.CurrentRow.Cells["SETOR"].Value.ToString();
                tbxSetorInicial.Text = setor.Split('-')[0].TrimEnd(' ');
                tbxSetorFinal.Text = setor.Split('-')[1].TrimStart(' ');
                cbxEstacao.SelectedValue = dgvDefinicao.CurrentRow.Cells["ESTACAO"].Value;
                tbxAlbedo.Text = dgvDefinicao.CurrentRow.Cells["ALBEDO"].Value.ToString();
                tbxBowen.Text = dgvDefinicao.CurrentRow.Cells["BOWEN"].Value.ToString();
                tbxRugosidade.Text = dgvDefinicao.CurrentRow.Cells["RUGOSIDADE"].Value.ToString();
            }
        }

        #endregion

        #endregion

        #region Eventos FrmDefinicaoAERMET

        private void FrmDefinicaoAERMET_Load(object sender, EventArgs e)
        {
            dgvDefinicao.ScrollBarVisible(true);
            dgvDados.ScrollBarVisible(true);

            CarregarComboBox();
            CarregarEstado();
            CarregarRegistros();

            if (consulta)
            {
                tabControlDados.SelectedTab = tabPageConsulta;
            }
        }

        private void FrmDefinicaoAERMET_KeyDown(object sender, KeyEventArgs e)
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
                        else if (tabControlCadastro.SelectedTab == tabPageCaracteristicas && dgvDefinicao.Focused)
                        {
                            AlterarCaracteristica();
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
                        else if (tabControlCadastro.SelectedTab == tabPageCaracteristicas && dgvDefinicao.Focused)
                        {
                            InserirCaracteristica();
                        }
                        break;
                    case Keys.Delete:
                        e.SuppressKeyPress = true;

                        if (tabControlDados.SelectedTab == tabPageConsulta)
                        {
                            ExcluirRegistro();
                        }
                        else if (tabControlCadastro.SelectedTab == tabPageCaracteristicas && dgvDefinicao.Focused)
                        {
                            ExcluirCaracteristica();
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
                        else if (tabControlCadastro.SelectedTab == tabPageCaracteristicas && dgvDefinicao.Focused == false)
                        {
                            SalvarCaracteristica();
                        }
                        else
                        {
                            SalvarRegistro();
                        }
                        break;
                }
            }
        }

        private void FrmDefinicaoAERMET_FormClosing(object sender, FormClosingEventArgs e)
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

        private void tabControlDados_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (e.TabPage == tabPageCadastro && consulta)
            {
                dgvDados.Focus();
                e.Cancel = true;
            }
        }

        #endregion

        #region Eventos tabControlCadastro

        private void tabControlCadastro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControlCadastro.SelectedTab == tabPageDadosBasicos)
            {
                tbxLocal.Focus();
            }
            else
            {
                if (dgvDefinicao.Rows.Count > 0)
                {
                    dgvDefinicao.Focus();
                }
                else
                {
                    InserirCaracteristica();
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

        #region Eventos CRUD características

        private void btnInserirCaracteristica_Click(object sender, EventArgs e)
        {
            InserirCaracteristica();
        }

        private void btnAlterarCaracteristica_Click(object sender, EventArgs e)
        {
            AlterarCaracteristica();
        }

        private void btnExcluirCaracteristica_Click(object sender, EventArgs e)
        {
            ExcluirCaracteristica();
        }

        private void btnSalvarCaracteristica_Click(object sender, EventArgs e)
        {
            SalvarCaracteristica();
        }

        #endregion

        #region Eventos dgvDefinicao

        private void dgvDefinicao_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDefinicao.CurrentCell != null && dgvDefinicao.CurrentCell.ColumnIndex == 0)
            {
                dgvDefinicao.ReadOnly = true;
            }
        }

        private void dgvDefinicao_Enter(object sender, EventArgs e)
        {
            dgvDefinicao.Tag = null;
            HabilitarCamposCaracteristica(false);
            CarregarCaracteristicaSelecionada();
        }

        private void dgvDefinicao_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AlterarCaracteristica();
        }

        private void dgvDefinicao_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            switch (dgvDefinicao.Columns[e.ColumnIndex].Name)
            {
                case "FREQUENCIA":
                    if (e.Value != null)
                    {
                        int valor = (int)e.Value;
                        FrequenciaSetor frequencia = (FrequenciaSetor)valor;
                        e.Value = frequencia.GetEnumDescription();
                    }
                    break;
                case "ESTACAO":
                    if (e.Value != null)
                    {
                        int valor = (int)e.Value;
                        Estacao estacao = (Estacao)valor;
                        e.Value = estacao.GetEnumDescription();
                    }
                    break;
                case "ALBEDO":
                case "BOWEN":
                case "RUGOSIDADE":
                    if (e.Value != null) Convert.ToDecimal(e.Value.ToString()).ToString("N2");
                    break;
            }
        }

        private void dgvDefinicao_SelectionChanged(object sender, EventArgs e)
        {
            CarregarCaracteristicaSelecionada();
        }

        #endregion
    }
}
