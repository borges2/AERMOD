using AERMOD.LIB.Componentes.Splash;
using AERMOD.LIB.Desenvolvimento;
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

namespace AERMOD.CamadaApresentacao.AERMAP
{
    public partial class FrmDominio : Form
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

        ClsAERMAP_Dominio clsDominioModelagem = null;

        /// <summary>
        /// Get classe de negócios ClsDominioModelagem
        /// </summary>
        ClsAERMAP_Dominio classeDominio
        {
            get
            {
                if (clsDominioModelagem == null)
                {
                    clsDominioModelagem = new ClsAERMAP_Dominio(Base.ConfiguracaoRede);
                }

                return clsDominioModelagem;
            }
        }

        ClsFonteAERMAP clsFonteAERMAP = null;

        /// <summary>
        /// Get classe de negócios ClsDominioModelagem
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

        /// <summary>
        /// Código do domínio.
        /// </summary>
        int codigoDominio;

        #endregion

        #region Construtor

        public FrmDominio()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);

            InitializeComponent();            

            this.Icon = Properties.Resources.form2.ConvertImageToIcon();
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
                DataTable dt = classeDominio.RetornarRegistros();

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
                                          item["DESCRICAO"],                                          
                                          item["DOMINIO_X1"],
                                          item["DOMINIO_Y1"],
                                          item["DOMINIO_X2"],
                                          item["DOMINIO_Y2"],
                                          item["ZONA_UTM"],
                                          item["ZONA_UTM_LETRA"],
                                          item["ZONA_GMT"],
                                          item["FONTE_GRADE"],
                                          item["GRADE_DOMINIO"]
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
            codigoDominio = classeDominio.Incrementar();

            if (codigoDominio == 0)
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(85), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            classeDominio.Inserir(codigoDominio);

            if (dadosDefault)
            {
                PreencherCamposAutomatico();
            }
            else
            {
                LimparCampos();
            }

            dgvDados.Tag = "INSERIR";
            tabControlDados.SelectedTab = tabPageCadastro;
            this.ActiveControl = tbxDescricao;
            tbxDescricao.Focus();
            tbxDescricao.SelectAll();
        }

        /// <summary>
        /// Alterar registro.
        /// </summary>
        private void AlterarRegistro()
        {
            if (dgvDados.Focused && dgvDados.CurrentRow != null)
            {
                tbxDescricao.Text = dgvDados.CurrentRow.Cells["DESCRICAO"].Value.ToString();
                tbxDominio_X1.Text = dgvDados.CurrentRow.Cells["DOMINIO_X1"].Value.ToString();
                tbxDominio_Y1.Text = dgvDados.CurrentRow.Cells["DOMINIO_Y1"].Value.ToString();
                tbxDominio_X2.Text = dgvDados.CurrentRow.Cells["DOMINIO_X2"].Value.ToString();
                tbxDominio_Y2.Text = dgvDados.CurrentRow.Cells["DOMINIO_Y2"].Value.ToString();
                tbxZonaUTM.Text = dgvDados.CurrentRow.Cells["ZONA_UTM"].Value.ToString();
                tbxZonaUTM_letra.Text = dgvDados.CurrentRow.Cells["ZONA_UTM_LETRA"].Value.ToString();
                tbxZonaGMT.Text = dgvDados.CurrentRow.Cells["ZONA_GMT"].Value.ToString();
                tbxFonteGrade.Text = dgvDados.CurrentRow.Cells["FONTE_GRADE"].Value.ToString();
                tbxGradeDominio.Text = dgvDados.CurrentRow.Cells["GRADE_DOMINIO"].Value.ToString();

                tbxDescricao.Focus();
                dgvDados.Tag = "ALTERAR";
                tabControlDados.SelectedTab = tabPageCadastro;

                codigoDominio = Convert.ToInt32(dgvDados.CurrentRow.Cells["CODIGO"].Value);
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

                                    classeDominio.Excluir(Convert.ToInt32(dgvDados.Rows[i].Cells["CODIGO"].Value));

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

            decimal dominio_x1 = Convert.ToDecimal(tbxDominio_X1.Text);
            decimal dominio_y1 = Convert.ToDecimal(tbxDominio_Y1.Text);
            decimal dominio_x2 = Convert.ToDecimal(tbxDominio_X2.Text);
            decimal dominio_y2 = Convert.ToDecimal(tbxDominio_Y2.Text);
            int zona_UTM = Convert.ToInt32(tbxZonaUTM.Text);
            char zona_UTM_letra = tbxZonaUTM_letra.Text.Trim().Length > 0 ? Convert.ToChar(tbxZonaUTM_letra.Text) : (char)0;
            int zona_GMT = tbxZonaGMT.Text.ValidarValor<int>(0);
            decimal fonteGrade = Convert.ToDecimal(tbxFonteGrade.Text);
            decimal gradeDominio = Convert.ToDecimal(tbxGradeDominio.Text);

            decimal menorCoordenadaGrade_X = classeDominio.MenorCoordenadaGrade_X(codigoDominio);
            decimal menorCoordenadaGrade_Y = classeDominio.MenorCoordenadaGrade_Y(codigoDominio);

            decimal maiorCoordenadaGrade_X = classeFonte.MaiorCoordenada_X() + fonteGrade;
            decimal maiorCoordenadaGrade_Y = classeFonte.MaiorCoordenada_Y() + fonteGrade;            

            if (classeDominio.RetornaQtdGrade(codigoDominio) == 0)
            {
                MessageBox.Show(this, $"Grade - {classeHelp.BuscarMensagem(87)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnGrade.Focus();
                return;
            }

            if (fonteGrade == 0)
            {
                MessageBox.Show(this, $"Fonte/Grade - {classeHelp.BuscarMensagem(3)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxFonteGrade.Focus();
                return;
            }

            if (gradeDominio == 0)
            {
                MessageBox.Show(this, $"Grade/Domínio - {classeHelp.BuscarMensagem(3)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxGradeDominio.Focus();
                return;
            }

            decimal coordenada = menorCoordenadaGrade_X - gradeDominio;

            if (dominio_x1 == 0)
            {
                tbxDominio_X1.Text = coordenada.ToString();
                dominio_x1 = Convert.ToDecimal(tbxDominio_X1.Text);
            }
            else if (dominio_x1 > coordenada)
            {
                MessageBox.Show(this, $"Eixo X inferior esquerda - {classeHelp.BuscarMensagem(62)} {coordenada}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxDominio_X1.Text = coordenada.ToString();
                tbxDominio_X1.Focus();
                return;
            }

            coordenada = menorCoordenadaGrade_Y - gradeDominio;

            if (dominio_y1 == 0)
            {
                tbxDominio_Y1.Text = coordenada.ToString();
                dominio_y1 = Convert.ToDecimal(tbxDominio_Y1.Text);
            }
            else if (dominio_y1 > coordenada)
            {
                MessageBox.Show(this, $"Eixo Y inferior esquerda - {classeHelp.BuscarMensagem(62)} {coordenada}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxDominio_X1.Text = coordenada.ToString();
                tbxDominio_Y1.Focus();
                return;               
            }

            coordenada = maiorCoordenadaGrade_X + gradeDominio;

            if (dominio_x2 == 0)
            {
                tbxDominio_X2.Text = coordenada.ToString();
                dominio_x2 = Convert.ToDecimal(tbxDominio_X2.Text);
            }
            else if (dominio_x2 < coordenada)
            {
                MessageBox.Show(this, $"Eixo X superior direita - {classeHelp.BuscarMensagem(88)} {coordenada}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxDominio_X2.Text = coordenada.ToString();
                tbxDominio_X2.Focus();
                return;                
            }

            coordenada = maiorCoordenadaGrade_Y + gradeDominio;

            if (dominio_y2 == 0)
            {
                tbxDominio_Y2.Text = coordenada.ToString();
                dominio_y2 = Convert.ToDecimal(tbxDominio_Y2.Text);
            }
            else if (dominio_y2 < coordenada)
            {
                MessageBox.Show(this, $"Eixo Y superior direita - {classeHelp.BuscarMensagem(88)} {coordenada}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxDominio_Y2.Text = coordenada.ToString();
                tbxDominio_Y2.Focus();
                return;                
            }

            if (zona_UTM == 0)
            {
                MessageBox.Show(this, $"Zona UTM-N - {classeHelp.BuscarMensagem(3)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxZonaUTM.Focus();
                return;
            }

            if (zona_UTM_letra == (char)0)
            {
                MessageBox.Show(this, $"Zona UTM-L - {classeHelp.BuscarMensagem(3)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxZonaUTM_letra.Focus();
                return;
            }

            if (zona_GMT == 0)
            {
                MessageBox.Show(this, $"Zona GMT - {classeHelp.BuscarMensagem(3)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxZonaGMT.Focus();
                return;
            }

            if (tbxDescricao.Text == string.Empty)
            {
                MessageBox.Show(this, string.Format("Descrição - {0}", classeHelp.BuscarMensagem(3)), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxDescricao.Focus();                
            }
            else
            {
                int codigoExistente = classeDominio.VerificarDuplicidade(codigoDominio, tbxDescricao.Text);
                if (codigoExistente > 0)
                {
                    string ID = codigoExistente.ToString();
                    string msg = $"{classeHelp.BuscarMensagem(6)}\nID: {ID}";

                    MessageBox.Show(this, msg, classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tbxDescricao.Focus();
                    return;
                }
            }            

            #endregion

            dynamic dominio = new ExpandoObject();
            dominio.CODIGO = codigoDominio;
            dominio.DESCRICAO = tbxDescricao.Text;            
            dominio.DOMINIO_X1 = dominio_x1;
            dominio.DOMINIO_Y1 = dominio_y1;
            dominio.DOMINIO_X2 = dominio_x2;
            dominio.DOMINIO_Y2 = dominio_y2;
            dominio.ZONA_UTM = zona_UTM;
            dominio.ZONA_UTM_LETRA = zona_UTM_letra;
            dominio.ZONA_GMT = zona_GMT;
            dominio.FONTE_GRADE = fonteGrade;
            dominio.GRADE_DOMINIO = gradeDominio;

            int retorno = classeDominio.Atualizar(dominio);
            if (retorno > 0)
            {
                if (dgvDados.Tag.ToString() == "INSERIR")
                {
                    dgvDados.Rows.Add(false,
                                      codigoDominio,
                                      tbxDescricao.Text,
                                      dominio_x1,
                                      dominio_y1,
                                      dominio_x2,
                                      dominio_y2,
                                      zona_UTM,
                                      zona_UTM_letra,
                                      zona_GMT,
                                      fonteGrade,
                                      gradeDominio
                                     );

                    MessageBox.Show(this, classeHelp.BuscarMensagem(7), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Information);

                    InserirRegistro(false);

                    if (dgvDados.Rows.Count == 1)
                    {
                        classeDominio.AtualizarUso(retorno);
                        dgvDados.Rows[0].DefaultCellStyle.ForeColor = Color.Blue;
                    }
                }
                else
                {
                    dgvDados.CurrentRow.Cells["DESCRICAO"].Value = tbxDescricao.Text;                    
                    dgvDados.CurrentRow.Cells["DOMINIO_X1"].Value = dominio_x1;
                    dgvDados.CurrentRow.Cells["DOMINIO_Y1"].Value = dominio_y1;
                    dgvDados.CurrentRow.Cells["DOMINIO_X2"].Value = dominio_x2;
                    dgvDados.CurrentRow.Cells["DOMINIO_Y2"].Value = dominio_y2;
                    dgvDados.CurrentRow.Cells["ZONA_UTM"].Value = zona_UTM;
                    dgvDados.CurrentRow.Cells["ZONA_UTM_LETRA"].Value = zona_UTM_letra;
                    dgvDados.CurrentRow.Cells["ZONA_GMT"].Value = zona_GMT;
                    dgvDados.CurrentRow.Cells["FONTE_GRADE"].Value = fonteGrade;
                    dgvDados.CurrentRow.Cells["GRADE_DOMINIO"].Value = gradeDominio;

                    dgvDados.Focus();
                    tabControlDados.SelectedTab = tabPageConsulta;
                }
            }
            else
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(5), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxDescricao.Focus();
            }
        }

        /// <summary>
        /// Limpar campos.
        /// </summary>
        private void LimparCampos()
        {
            tbxDescricao.ResetText();            
            tbxDominio_X1.ResetText();
            tbxDominio_Y1.ResetText();
            tbxDominio_X2.ResetText();
            tbxDominio_Y2.ResetText();
            tbxZonaUTM.ResetText();
            tbxZonaUTM_letra.ResetText();
            tbxZonaGMT.ResetText();
        }

        /// <summary>
        /// Atualizar uso.
        /// </summary>
        private void AtualizarUso()
        {
            if (dgvDados.CurrentRow != null)
            {
                int codigo = Convert.ToInt32(dgvDados.CurrentRow.Cells["CODIGO"].Value);

                classeDominio.AtualizarUso(codigo);
                this.Close();
            }
        }

        /// <summary>
        /// Sair do form.
        /// </summary>
        private void SairForm()
        {
            if (tabControlDados.SelectedTab == tabPageConsulta || dgvDados.Rows.Count == 0)
            {
                if (dgvDados.Rows.Count == 0)
                {
                    classeDominio.Excluir(codigoDominio);
                }

                this.Close();
            }
            else
            {
                if (dgvDados.Tag.ToString() == "INSERIR")
                {
                    classeDominio.Excluir(codigoDominio);
                }

                dgvDados.Focus();
                tabControlDados.SelectedTab = tabPageConsulta;
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
        /// Preenchimento de campos automático.
        /// </summary>
        private void PreencherCamposAutomatico()
        {
            //RE STARTING
            //GRIDCART CART01 STA
            //        XYINC 197748.00 100 500 7237428.00 100 500
            //CART01 END
            //RE FINISHED

            //X = subtrair 5.000 metros da menor coordenada das fontes (X).
            //Y = subtrair 5.000 metros da menor coordenada das fontes (y).

            //decimal GRID_X = classeFonte.MenorCoordenada_X();
            //decimal GRID_Y = classeFonte.MenorCoordenada_Y();

            //GRID_X = GRID_X - 5000;
            //GRID_Y = GRID_Y - 5000;

            //////////////////////////////////////////////////////////////

            //DOMAINXY  195748.00 7235428.00 -22 249748.00 7289428.00 -22
            //X1 = subtrair 1.000 metros da coordenada (X) do GRIDCART.
            //Y1 = subtrair 1.000 metros da coordenada (y) do GRIDCART.

            //X2 = acrescentar 5.000 metros da maior coordenada das fontes (x).
            //Y2 = acrescentar 5.000 metros da maior coordenada das fontes (y).

            //decimal DOMINIO_X1 = GRID_X - 1000;
            //decimal DOMINIO_Y1 = GRID_Y - 1000;

            //tbxDominio_X1.Text = DOMINIO_X1.ToString();
            //tbxDominio_Y1.Text = DOMINIO_Y1.ToString();

            //decimal DOMINIO_X2 = classeFonte.MaiorCoordenada_X();
            //decimal DOMINIO_Y2 = classeFonte.MaiorCoordenada_Y();

            //DOMINIO_X2 = DOMINIO_X2 + 6000;
            //DOMINIO_Y2 = DOMINIO_Y2 + 6000;

            //tbxDominio_X2.Text = DOMINIO_X2.ToString();
            //tbxDominio_Y2.Text = DOMINIO_Y2.ToString();

            tbxZonaUTM.Text = "-22";
            tbxZonaGMT.Text = "3";
            tbxZonaUTM_letra.Text = "J";
            tbxFonteGrade.Text = "1000";
            tbxGradeDominio.Text = "1000";

            //Descobrir letra no google earth
        }

        /// <summary>
        /// Abrir cadastro de grade.
        /// </summary>
        private void AbrirGrade()
        {
            FrmGrade frmGrade = new FrmGrade(codigoDominio);
            frmGrade.DistFonteGrade = Convert.ToDecimal(tbxFonteGrade.Text);
            frmGrade.ShowDialogFade(this);
        }

        #endregion

        #region Eventos FrmDominio        
        private void FrmDominio_Load(object sender, EventArgs e)
        {
            SplashScreen.CloseSplashScreen(this);

            CarregarRegistros();
        }

        private void FrmDominio_KeyDown(object sender, KeyEventArgs e)
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
                    case Keys.F3:
                        e.SuppressKeyPress = true;

                        if (tabControlDados.SelectedTab == tabPageCadastro)
                        {
                            AbrirGrade();
                        }
                        break;
                    case Keys.Insert:
                        e.SuppressKeyPress = true;

                        if (tabControlDados.SelectedTab == tabPageConsulta)
                        {
                            InserirRegistro(false);
                        }
                        break;
                    case Keys.Delete:
                        e.SuppressKeyPress = true;

                        if (tabControlDados.SelectedTab == tabPageConsulta)
                        {
                            ExcluirRegistro();
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
                            AtualizarUso();
                        }
                        else
                        {
                            SalvarRegistro();
                        }
                        break;
                }
            }
        }

        private void FrmDominio_FormClosing(object sender, FormClosingEventArgs e)
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
            if (e.TabPage == tabPageCadastro)
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

        #region Eventos tbxZonaUTM_letra

        private void tbxZonaUTM_letra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) == false && e.KeyChar != (char)8) ////permite apenas inserção de letras e (char)8) = tecla backapace
            {
                e.Handled = true;
            }
        }

        private void tbxZonaUTM_letra_TextChanged(object sender, EventArgs e)
        {
            TextBox tbx = (TextBox)sender;

            //Deixa incluir apenas números qdo clica com botão direto e cola algum conteúdo
            for (int i = 0; i < tbx.Text.Trim().Length; i++)
            {
                if (!char.IsLetter(tbx.Text.Trim()[i]))
                {
                    tbx.Text = tbx.Text.Trim().Remove(i, 1);
                    tbx.SelectionStart = tbx.Text.Trim().Length;
                }
            }
        }

        #endregion

        #region Eventos btnGrade

        private void btnGrade_Click(object sender, EventArgs e)
        {
            AbrirGrade();
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
            AlterarRegistro();
        }

        #endregion

        #region Eventos statusStripConsulta

        private void btnAjudaConsulta_Click(object sender, EventArgs e)
        {
            AbrirAjuda();
        }

        private void btnInserirConsulta_Click(object sender, EventArgs e)
        {
            InserirRegistro(false);
        }

        private void btnAlterarConsulta_Click(object sender, EventArgs e)
        {
            AlterarRegistro();
        }

        private void btnExcluirConsulta_Click(object sender, EventArgs e)
        {
            ExcluirRegistro();
        }

        private void btnSairConsulta_Click(object sender, EventArgs e)
        {
            SairForm();
        }

        #endregion

        #region Eventos statusStripCadastro

        private void btnAjudaCadastro_Click(object sender, EventArgs e)
        {
            AbrirAjuda();
        }

        private void btnConsultaCadastro_Click(object sender, EventArgs e)
        {
            ConsultarRegistros();
        }

        private void btnSalvarCadastro_Click(object sender, EventArgs e)
        {
            SalvarRegistro();
        }

        private void btnSairCadastro_Click(object sender, EventArgs e)
        {
            SairForm();
        }

        #endregion
    }
}
