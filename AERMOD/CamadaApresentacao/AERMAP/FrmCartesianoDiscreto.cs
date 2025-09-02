using AERMOD.LIB.Desenvolvimento;
using CamadaLogicaNegocios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AERMOD.LIB.Forms;
using AERMOD.LIB.Componentes.Splash;

namespace AERMOD.CamadaApresentacao.AERMAP
{
    public partial class FrmCartesianoDiscreto : Form
    {
        #region Classes e Propriedades

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

        ClsAERMAP_CartesianoDiscreto clsGradeCartesianoDiscreto = null;

        /// <summary>
        /// Get classe de negócios ClsAERMAP_CartesianoDiscreto
        /// </summary>
        ClsAERMAP_CartesianoDiscreto classeCartesianoDisc
        {
            get
            {
                if (clsGradeCartesianoDiscreto == null)
                {
                    clsGradeCartesianoDiscreto = new ClsAERMAP_CartesianoDiscreto(Base.ConfiguracaoRede, codigoDominio);
                }

                return clsGradeCartesianoDiscreto;
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

        int codigoAtual = 0;
        /// <summary>
        /// Códigos do registro selecionado
        /// </summary>
        int CodigoAtual
        {
            get { return codigoAtual; }
            set
            {
                codigoAtual = value;
                ControleBotoesNavegacao(true);
            }
        }

        /// <summary>
        /// Código do domínio.
        /// </summary>
        int codigoDominio;

        /// <summary>
        /// Distância entre a fonte e a grade de modelagem.
        /// </summary>
        public decimal DistFonteGrade { get; set; }

        #endregion

        #region Construtor

        public FrmCartesianoDiscreto(int codigoDominio)
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);

            this.Icon = Properties.Resources.grade.ConvertImageToIcon();
            this.codigoDominio = codigoDominio;
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
        /// Limpa os campos do cadastro
        /// </summary>
        private void LimparCampos(bool limparCodigo)
        {
            if (limparCodigo)
            {
                tbxCodigo.Text = "0";
            }

            tbxDescricao.ResetText();
            tbxCoordenada_X.ResetText();
            tbxCoordenada_Y.ResetText();
            tbxElevacao.ResetText();
            tbxAltura.ResetText();            
        }

        /// <summary>
        /// Habilita e desabilita os botoes
        /// </summary>
        /// <param name="value">True ou False</param>
        private void HabilitaBotoes(bool value, bool inserir, bool salvoUnicoRegistro = false)
        {
            btnConsultar.Enabled = value;
            btnSalvar.Enabled = inserir == false ? value : true;
            btnExcluir.Enabled = value;

            if (inserir)
            {
                btnInserir.Enabled = value;
            }
            else
            {
                btnInserir.Enabled = true;
            }

            ControleBotoesNavegacao(value, salvoUnicoRegistro);
        }

        /// <summary>
        /// Faz o controle dos botoes da navegacao
        /// </summary>
        /// <param name="Habilitar"></param>
        private void ControleBotoesNavegacao(Boolean Habilitar, bool salvoUnicoRegistro = false)
        {
            if (salvoUnicoRegistro == false)
            {
                Int64 PrimeiroRegistro = classeCartesianoDisc.BuscarPrimeiroId();
                Int64 UltimoRegistro = classeCartesianoDisc.BuscarUltimoId();
                Int64 ProximoRegistro = classeCartesianoDisc.BuscarIdProximo(CodigoAtual);
                Int64 AnteriorRegistro = classeCartesianoDisc.BuscarIdAnterior(CodigoAtual);

                btnPrimeiro.Enabled = Habilitar && PrimeiroRegistro > 0 && CodigoAtual > PrimeiroRegistro;
                btnAnterior.Enabled = Habilitar && PrimeiroRegistro > 0 && AnteriorRegistro > 0 && AnteriorRegistro < CodigoAtual;
                btnProximo.Enabled = Habilitar && UltimoRegistro > CodigoAtual && ProximoRegistro > 0 && ProximoRegistro > CodigoAtual;
                btnUltimo.Enabled = Habilitar && UltimoRegistro > CodigoAtual;
            }
            else
            {
                btnPrimeiro.Enabled = false;
                btnAnterior.Enabled = false;
                btnProximo.Enabled = true;
                btnUltimo.Enabled = true;
            }
        }

        /// <summary>
        /// Carrega os dados 
        /// </summary>
        /// <param name="dtDados"></param>
        private void CarregaDados(DataTable dtDados)
        {
            if (dtDados.Rows.Count > 0)
            {
                tbxCodigo.Text = dtDados.Rows[0]["CODIGO"].ToString();
                tbxDescricao.Text = dtDados.Rows[0]["DESCRICAO"].ToString();
                tbxCoordenada_X.Text = dtDados.Rows[0]["XCOORD"].ToString();
                tbxCoordenada_Y.Text = dtDados.Rows[0]["YCOORD"].ToString();
                tbxElevacao.Text = dtDados.Rows[0]["ZELEV"].ToString();
                tbxAltura.Text = dtDados.Rows[0]["ZFLAG"].ToString();                
            }
            else
            {
                LimparCampos(true);
            }
        }

        /// <summary>
        /// Valida os dados e salva
        /// </summary>
        public void Salvar()
        {
            #region Validação

            ///Lista de Controles que possui erro
            List<Control> listControlErro = new List<Control>();
            StringBuilder mensagemErro = new StringBuilder();

            if (Convert.ToDecimal(tbxCoordenada_X.Text) == 0)
            {
                listControlErro.Add(tbxCoordenada_X);
                mensagemErro.AppendLine(string.Format("Eixo X - {0}", classeHelp.BuscarMensagem(3)));
            }
            else
            {
                decimal menorCoordenada_X = classeFonte.MenorCoordenada_X();
                decimal coordenada = menorCoordenada_X - DistFonteGrade;

                if (Convert.ToDecimal(tbxCoordenada_X.Text) > coordenada)
                {
                    MessageBox.Show(this, string.Format($"Eixo X - {classeHelp.BuscarMensagem(62)}", coordenada), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tbxCoordenada_X.Text = coordenada.ToString();
                    tbxCoordenada_X.Focus();
                    return;
                }
            }

            if (Convert.ToDecimal(tbxCoordenada_Y.Text) == 0)
            {
                listControlErro.Add(tbxCoordenada_Y);
                mensagemErro.AppendLine(string.Format("Eixo Y - {0}", classeHelp.BuscarMensagem(3)));
            }
            else
            {
                decimal menorCoordenada_Y = classeFonte.MenorCoordenada_Y();
                decimal coordenada = menorCoordenada_Y - DistFonteGrade;

                if (Convert.ToDecimal(tbxCoordenada_Y.Text) > coordenada)
                {
                    MessageBox.Show(this, string.Format($"Eixo Y - {classeHelp.BuscarMensagem(62)}", coordenada), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tbxCoordenada_Y.Text = coordenada.ToString();
                    tbxCoordenada_Y.Focus();
                    return;
                }
            }
            
            if (tbxDescricao.Text == string.Empty)
            {
                listControlErro.Add(tbxDescricao);
                mensagemErro.AppendLine(string.Format("Descrição - {0}", classeHelp.BuscarMensagem(3)));
            }
            else
            {
                int codigo = classeCartesianoDisc.VerificaDuplicidadeDescricao(Convert.ToInt32(tbxCodigo.Text), tbxDescricao.Text);
                if (codigo != 0)
                {
                    listControlErro.Add(tbxDescricao);
                    mensagemErro.AppendLine(string.Format("Descrição - {0}", string.Format(classeHelp.BuscarMensagem(82), codigo)));
                }
            }

            if (listControlErro.Count > 0)
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(83) + System.Environment.NewLine + mensagemErro.ToString(), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);

                listControlErro[0].Focus();
                return;
            }

            #endregion

            dynamic grade = new ExpandoObject();
            grade.CODIGO = Convert.ToInt32(tbxCodigo.Text);
            grade.XCOORD = Convert.ToDecimal(tbxCoordenada_X.Text);
            grade.YCOORD = Convert.ToDecimal(tbxCoordenada_Y.Text);
            grade.ZELEV = Convert.ToDecimal(tbxElevacao.Text);
            grade.ZFLAG = Convert.ToDecimal(tbxAltura.Text);            
            grade.DESCRICAO = tbxDescricao.Text;
            grade.EM_USO = classeCartesianoDisc.VerificaExistencia() ? classeCartesianoDisc.VerificaRegistroEmUso(grade.CODIGO)
                                                                     : true;

            bool retorno = classeCartesianoDisc.Atualizar(grade);

            if (retorno == false)
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(5), classeHelp.BuscarMensagem(34), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxDescricao.Focus();
                return;
            }
            else
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(7), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            CodigoAtual = Convert.ToInt32(tbxCodigo.Text);
            HabilitaBotoes(true, false, classeCartesianoDisc.VerificaRegistroUnico());
            tbxDescricao.Focus();
        }

        /// <summary>
        /// Mostra o primeiro registro.
        /// </summary>
        private void BotaoPrimeiro()
        {
            CodigoAtual = classeCartesianoDisc.BuscarPrimeiroId();

            tbxCodigo.Text = CodigoAtual.ToString();

            DataTable dtDados = classeCartesianoDisc.RetornaDados(Convert.ToInt64(tbxCodigo.Text));
            CarregaDados(dtDados);

            tbxDescricao.Focus();
        }

        /// <summary>
        /// Mostra o registro anterior do atual selecionado.
        /// </summary>
        private void BotaoAnterior()
        {
            CodigoAtual = CodigoAtual - 1;

            if (classeCartesianoDisc.VerificaExistencia(CodigoAtual))
            {
                tbxCodigo.Text = CodigoAtual.ToString();

                DataTable dtDados = classeCartesianoDisc.RetornaDados(CodigoAtual);
                CarregaDados(dtDados);
            }
            else
            {
                CodigoAtual = classeCartesianoDisc.BuscarIdAnterior(CodigoAtual);
                if (CodigoAtual == 0)
                {
                    CodigoAtual = classeCartesianoDisc.BuscarPrimeiroId();
                }

                if (CodigoAtual > 0)
                {
                    DataTable dtDados = classeCartesianoDisc.RetornaDados(CodigoAtual);
                    CarregaDados(dtDados);
                }
            }

            tbxDescricao.Focus();
        }

        /// <summary>
        /// Mostra o próximo registro do atual selecionado.
        /// </summary>
        private void BotaoProximo()
        {
            CodigoAtual = CodigoAtual + 1;

            if (classeCartesianoDisc.VerificaExistencia(CodigoAtual))
            {
                tbxCodigo.Text = CodigoAtual.ToString();

                DataTable dtDados = classeCartesianoDisc.RetornaDados(CodigoAtual);
                CarregaDados(dtDados);
            }
            else
            {
                CodigoAtual = classeCartesianoDisc.BuscarIdProximo(CodigoAtual);
                if (CodigoAtual == 0)
                {
                    CodigoAtual = classeCartesianoDisc.BuscarUltimoId();
                }

                if (CodigoAtual > 0)
                {
                    DataTable dtDados = classeCartesianoDisc.RetornaDados(CodigoAtual);
                    CarregaDados(dtDados);
                }
            }

            tbxDescricao.Focus();
        }

        /// <summary>
        /// Mostra o último registro.
        /// </summary>
        private void BotaoUltimo()
        {
            CodigoAtual = classeCartesianoDisc.BuscarUltimoId();

            tbxCodigo.Text = CodigoAtual.ToString();

            DataTable dtDados = classeCartesianoDisc.RetornaDados(CodigoAtual);
            CarregaDados(dtDados);

            tbxDescricao.Focus();
        }

        /// <summary>
        /// Inserir registro.
        /// </summary>
        private void InserirRegistro()
        {
            //RE STARTING
            //GRIDCART CART01 STA
            //        XYINC 197748.00 100 500 7237428.00 100 500
            //CART01 END
            //RE FINISHED

            //X = subtrair 5.000 metros da menor coordenada das fontes (X).
            //Y = subtrair 5.000 metros da menor coordenada das fontes (y).

            //DOMAINXY  195748.00 7235428.00 -22 249748.00 7289428.00 -22
            //X1 = subtrair 1.000 metros da coordenada (X) do GRIDCART.
            //Y1 = subtrair 1.000 metros da coordenada (y) do GRIDCART.

            //X2 = acrescentar 5.000 metros da maior coordenada das fontes (x).
            //Y2 = acrescentar 5.000 metros da maior coordenada das fontes (y).

            int incremento = classeCartesianoDisc.Incrementa();
            tbxCodigo.Text = incremento.ToString();
            CodigoAtual = incremento;

            if (string.IsNullOrEmpty(tbxCodigo.Text))
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(85), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            HabilitaBotoes(false, true);
            LimparCampos(false);

            decimal GRID_X = classeFonte.MenorCoordenada_X();
            decimal GRID_Y = classeFonte.MenorCoordenada_Y();

            GRID_X = GRID_X - DistFonteGrade;
            GRID_Y = GRID_Y - DistFonteGrade;

            tbxCoordenada_X.Text = GRID_X.ToString();
            tbxCoordenada_Y.Text = GRID_Y.ToString();           

            tbxDescricao.Focus();
        }

        /// <summary>
        /// Excluir registro.
        /// </summary>
        private void ExcluirRegistro()
        {
            if (string.IsNullOrEmpty(tbxCodigo.Text) == false && classeCartesianoDisc.VerificaExistencia(Convert.ToInt32(tbxCodigo.Text)))
            {
                string pergunta = $"{classeHelp.BuscarMensagem(84)} {tbxCodigo.Text}";
                DialogResult dialogResult = MessageBox.Show(this, pergunta, classeHelp.BuscarMensagem(2), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    classeCartesianoDisc.Excluir(Convert.ToInt32(tbxCodigo.Text));

                    BotaoAnterior();

                    if (CodigoAtual == 0)
                    {
                        BotaoProximo();
                    }

                    if (CodigoAtual == 0)
                    {
                        InserirRegistro();
                    }
                }
            }
        }

        /// <summary>
        /// Abrir consulta.
        /// </summary>
        private void AbrirConsulta()
        {
            FrmConsultaGrade frmConsulta = new FrmConsultaGrade(TipoGrade.CARTESIANO_DISCRETO, codigoDominio);
            frmConsulta.ShowDialogFade(this);

            if (frmConsulta.Codigo > 0)
            {
                CodigoAtual = frmConsulta.Codigo;
                tbxCodigo.Text = frmConsulta.Codigo.ToString();

                DataTable dtDados = classeCartesianoDisc.RetornaDados(frmConsulta.Codigo);
                CarregaDados(dtDados);

                tbxDescricao.Focus();
                tbxDescricao.SelectAll();
            }
            else if (classeCartesianoDisc.VerificaExistencia(Convert.ToInt32(tbxCodigo.Text)) == false)
            {
                BotaoAnterior();

                if (CodigoAtual == 0)
                {
                    BotaoProximo();
                }

                if (CodigoAtual == 0)
                {
                    InserirRegistro();
                }
            }
        }

        /// <summary>
        /// Cancelar inserção ou sair.
        /// </summary>
        private void CancelarSair()
        {
            if (btnInserir.Enabled == false)
            {
                HabilitaBotoes(true, false);
                BotaoAnterior();

                if (CodigoAtual == 0)
                {
                    BotaoProximo();
                }

                if (CodigoAtual == 0)
                {
                    this.Close();
                }
            }
            else
            {
                this.Close();
            }
        }

        #endregion

        #region Eventos FrmCartesiano

        protected override void OnLoad(EventArgs e)
        {
            SplashScreen.CloseSplashScreen(this);

            if (classeCartesianoDisc.VerificaExistencia() == false)
            {
                InserirRegistro();
            }
            else
            {
                BotaoPrimeiro();
            }

            base.OnLoad(e);
        }        

        private void FrmCartesianoDiscreto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.PageUp:
                        e.SuppressKeyPress = true;
                        btnPrimeiro.PerformClick();
                        break;
                    case Keys.PageDown:
                        e.SuppressKeyPress = true;
                        btnUltimo.PerformClick();
                        break;
                }
            }
            else if (e.Shift)
            {
                switch (e.KeyCode)
                {
                    case Keys.Oemplus:
                        e.SuppressKeyPress = true;
                        InserirRegistro();
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
                        AbrirConsulta();
                        break;
                    case Keys.PageUp:
                        e.SuppressKeyPress = true;
                        BotaoAnterior();
                        break;
                    case Keys.PageDown:
                        e.SuppressKeyPress = true;
                        BotaoProximo();
                        break;
                    case Keys.Add:
                        e.SuppressKeyPress = true;
                        InserirRegistro();
                        break;
                    case Keys.OemMinus:
                    case Keys.Subtract:
                        e.SuppressKeyPress = true;
                        ExcluirRegistro();
                        break;
                    case Keys.Enter:
                        e.SuppressKeyPress = true;
                        Salvar();
                        break;
                    case Keys.Escape:
                        e.SuppressKeyPress = true;
                        CancelarSair();
                        break;
                }
            }
        }

        #endregion

        #region Eventos Navegação

        private void btnPrimeiro_Click(object sender, EventArgs e)
        {
            BotaoPrimeiro();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            BotaoAnterior();
        }

        private void btnProximo_Click(object sender, EventArgs e)
        {
            BotaoProximo();
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            BotaoUltimo();
        }

        #endregion

        #region Eventos operações

        private void btnInserir_Click(object sender, EventArgs e)
        {
            InserirRegistro();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            ExcluirRegistro();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Salvar();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            AbrirConsulta();
        }

        #endregion

        #region Eventos statusStripCadastro

        private void btnAjuda_Click(object sender, EventArgs e)
        {
            AbrirAjuda();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            CancelarSair();
        }

        #endregion
    }
}
