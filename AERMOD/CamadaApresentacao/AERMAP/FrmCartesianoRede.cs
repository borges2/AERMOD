using AERMOD.LIB.Componentes.GridView;
using AERMOD.LIB.Componentes.Splash;
using AERMOD.LIB.Desenvolvimento;
using AERMOD.LIB.Forms;
using CamadaLogicaNegocios;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace AERMOD.CamadaApresentacao.AERMAP
{
    public partial class FrmCartesianoRede : Form
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

        ClsAERMAP_CartesianoRede clsGradeCartesianaRede = null;

        /// <summary>
        /// Get classe de negócios ClsAERMAP_CartesianoRede.
        /// </summary>
        ClsAERMAP_CartesianoRede classeCartesianaRede
        {
            get
            {
                if (clsGradeCartesianaRede == null)
                {
                    clsGradeCartesianaRede = new ClsAERMAP_CartesianoRede(Base.ConfiguracaoRede, codigoDominio);
                }

                return clsGradeCartesianaRede;
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

        public FrmCartesianoRede(int codigoDominio)
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
            dgvRede.Rows.Clear();
            dgvElevacao.Rows.Clear();
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
                Int64 PrimeiroRegistro = classeCartesianaRede.BuscarPrimeiroId();
                Int64 UltimoRegistro = classeCartesianaRede.BuscarUltimoId();
                Int64 ProximoRegistro = classeCartesianaRede.BuscarIdProximo(CodigoAtual);
                Int64 AnteriorRegistro = classeCartesianaRede.BuscarIdAnterior(CodigoAtual);

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
        private void CarregaDados(Tuple<DataTable, DataTable> dtDados)
        {
            if (dtDados.Item1.Rows.Count > 0)
            {
                tbxCodigo.Text = dtDados.Item1.Rows[0]["CODIGO"].ToString();
                tbxDescricao.Text = dtDados.Item1.Rows[0]["DESCRICAO"].ToString();

                HabilitaDesabilitaCoordenadas(false);
                HabilitaDesabilitaElevacao(false);

                dgvRede.Rows.Clear();

                foreach (DataRow item in dtDados.Item1.Rows)
                {
                    dgvRede.Rows.Add(false, item["SEQUENCIA"], item["XPNTS"], item["YPNTS"]);
                }                

                dgvElevacao.Rows.Clear();

                foreach (DataRow item in dtDados.Item2.Rows)
                {
                    dgvElevacao.Rows.Add(false, item["SEQUENCIA"], item["ELEV"], item["FLAG"]);
                }                
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
            if (dgvRede.Tag != null)
            {
                SalvarCoordenadas();
                return;
            }
            else if (dgvElevacao.Tag != null)
            {
                SalvarElevacao();
                return;
            }

            #region Validação

            ///Lista de Controles que possui erro
            List<Control> listControlErro = new List<Control>();
            StringBuilder mensagemErro = new StringBuilder();

            if (dgvRede.Rows.OfType<DataGridViewRow>().Any(I => Convert.ToDecimal(I.Cells["XPNTS"].Value) == 0))
            {
                listControlErro.Add(dgvRede);
                mensagemErro.AppendLine(string.Format("Eixo X - {0}", classeHelp.BuscarMensagem(3)));
            }
            else
            {
                DataGridViewRow gridRow = dgvRede.Rows.OfType<DataGridViewRow>().Where(A => Convert.ToDecimal(A.Cells["XPNTS"].Value) == dgvRede.Rows.OfType<DataGridViewRow>().Min(B => Convert.ToDecimal(B.Cells["XPNTS"].Value))).First();
                decimal coordenadaGrid_X = Convert.ToDecimal(gridRow.Cells["XPNTS"].Value);
                decimal menorCoordenada_X = classeFonte.MenorCoordenada_X();
                decimal coordenada = menorCoordenada_X - DistFonteGrade;

                if (coordenadaGrid_X > coordenada)
                {
                    MessageBox.Show(this, string.Format($"Eixo X - {classeHelp.BuscarMensagem(62)}", coordenada), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    gridRow.Cells["XPNTS"].Value = coordenada;
                    dgvRede.Focus();
                    return;
                }
            }

            if (dgvRede.Rows.OfType<DataGridViewRow>().Any(I => Convert.ToDecimal(I.Cells["YPNTS"].Value) > 0) == false)
            {
                listControlErro.Add(dgvRede);
                mensagemErro.AppendLine(string.Format("Eixo Y - {0}", classeHelp.BuscarMensagem(3)));
            }
            else
            {
                DataGridViewRow gridRow = dgvRede.Rows.OfType<DataGridViewRow>().Where(A => Convert.ToDecimal(A.Cells["YPNTS"].Value) == dgvRede.Rows.OfType<DataGridViewRow>().Min(B => Convert.ToDecimal(B.Cells["YPNTS"].Value))).First();
                decimal coordenadaGrid_Y = Convert.ToDecimal(gridRow.Cells["YPNTS"].Value);
                decimal menorCoordenada_Y = classeFonte.MenorCoordenada_Y();
                decimal coordenada = menorCoordenada_Y - DistFonteGrade;

                if (coordenadaGrid_Y > coordenada)
                {
                    MessageBox.Show(this, string.Format($"Eixo Y - {classeHelp.BuscarMensagem(62)}", coordenada), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    gridRow.Cells["YPNTS"].Value = coordenada;
                    dgvRede.Focus();
                    return;
                }
            }

            if (dgvElevacao.Rows.Count > 0)
            {
                int countGrid = dgvRede.Rows.OfType<DataGridViewRow>().Count(A => Convert.ToDecimal(A.Cells["YPNTS"].Value) > 0);

                if (countGrid != dgvElevacao.Rows.Count)
                {
                    listControlErro.Add(dgvElevacao);
                    mensagemErro.AppendLine(classeHelp.BuscarMensagem(86));
                }
            }

            if (tbxDescricao.Text == string.Empty)
            {
                listControlErro.Add(tbxDescricao);
                mensagemErro.AppendLine(string.Format("Descrição - {0}", classeHelp.BuscarMensagem(3)));
            }
            else
            {
                int codigo = classeCartesianaRede.VerificaDuplicidadeDescricao(Convert.ToInt32(tbxCodigo.Text), tbxDescricao.Text);
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
            
            int codigoRede = Convert.ToInt32(tbxCodigo.Text);            
            string descricao = tbxDescricao.Text;            

            int sequencia = 0;
            List<Tuple<int, decimal, decimal>> lstXY = new List<Tuple<int, decimal, decimal>>();
            List<Tuple<int, decimal, decimal>> lstElevacao = new List<Tuple<int, decimal, decimal>>();            

            foreach (DataGridViewRow item in dgvRede.Rows)
            {
                sequencia ++;               

                if (sequencia > 99999)
                {                   
                    MessageBox.Show(this, $"ID: {sequencia} - {classeHelp.BuscarMensagem(85)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal XPNTS = Convert.ToDecimal(item.Cells["XPNTS"].Value);
                decimal YPNTS = Convert.ToDecimal(item.Cells["YPNTS"].Value);

                lstXY.Add(Tuple.Create(sequencia, XPNTS, YPNTS));
            }

            sequencia = 0;

            foreach (DataGridViewRow item in dgvElevacao.Rows)
            {
                sequencia++;

                if (sequencia > 99999)
                {
                    MessageBox.Show(this, $"ID: {sequencia} - {classeHelp.BuscarMensagem(85)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal ELEV = Convert.ToDecimal(item.Cells["ELEV"].Value);
                decimal FLAG = Convert.ToDecimal(item.Cells["FLAG"].Value);

                lstElevacao.Add(Tuple.Create(sequencia, ELEV, FLAG));
            }

            bool emUso = classeCartesianaRede.VerificaExistencia() ? classeCartesianaRede.VerificaRegistroEmUso(codigoRede)
                                                                   : true; 

            bool retorno = classeCartesianaRede.Atualizar(codigoRede, descricao, emUso, lstXY, lstElevacao);

            if (retorno == false)
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(5), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxDescricao.Focus();
                return;
            }
            else
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(7), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            CodigoAtual = Convert.ToInt32(tbxCodigo.Text);
            HabilitaBotoes(true, false, classeCartesianaRede.VerificaRegistroUnico());
            tbxDescricao.Focus();
        }

        /// <summary>
        /// Mostra o primeiro registro.
        /// </summary>
        private void BotaoPrimeiro()
        {
            CodigoAtual = classeCartesianaRede.BuscarPrimeiroId();

            tbxCodigo.Text = CodigoAtual.ToString();

            var dados = classeCartesianaRede.RetornaDados(Convert.ToInt32(tbxCodigo.Text));
            CarregaDados(dados);

            tbxDescricao.Focus();
        }

        /// <summary>
        /// Mostra o registro anterior do atual selecionado.
        /// </summary>
        private void BotaoAnterior()
        {
            CodigoAtual = CodigoAtual - 1;

            if (classeCartesianaRede.VerificaExistencia(CodigoAtual))
            {
                tbxCodigo.Text = CodigoAtual.ToString();

                var dados = classeCartesianaRede.RetornaDados(CodigoAtual);
                CarregaDados(dados);
            }
            else
            {
                CodigoAtual = classeCartesianaRede.BuscarIdAnterior(CodigoAtual);
                if (CodigoAtual == 0)
                {
                    CodigoAtual = classeCartesianaRede.BuscarPrimeiroId();
                }

                if (CodigoAtual > 0)
                {
                    var dados = classeCartesianaRede.RetornaDados(CodigoAtual);
                    CarregaDados(dados);
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

            if (classeCartesianaRede.VerificaExistencia(CodigoAtual))
            {
                tbxCodigo.Text = CodigoAtual.ToString();

                var dados = classeCartesianaRede.RetornaDados(CodigoAtual);
                CarregaDados(dados);
            }
            else
            {
                CodigoAtual = classeCartesianaRede.BuscarIdProximo(CodigoAtual);
                if (CodigoAtual == 0)
                {
                    CodigoAtual = classeCartesianaRede.BuscarUltimoId();
                }

                if (CodigoAtual > 0)
                {
                    var dados = classeCartesianaRede.RetornaDados(CodigoAtual);
                    CarregaDados(dados);
                }
            }

            tbxDescricao.Focus();
        }

        /// <summary>
        /// Mostra o último registro.
        /// </summary>
        private void BotaoUltimo()
        {
            CodigoAtual = classeCartesianaRede.BuscarUltimoId();

            tbxCodigo.Text = CodigoAtual.ToString();

            var dados = classeCartesianaRede.RetornaDados(CodigoAtual);
            CarregaDados(dados);

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

            int incremento = classeCartesianaRede.IncrementaRede();
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

            dgvRede.Rows.Add(false, 1, GRID_X, GRID_Y);

            HabilitaDesabilitaCoordenadas(false);
            HabilitaDesabilitaElevacao(false);

            tbxDescricao.Focus();
        }

        /// <summary>
        /// Excluir registro.
        /// </summary>
        private void ExcluirRegistro()
        {
            if (string.IsNullOrEmpty(tbxCodigo.Text) == false && classeCartesianaRede.VerificaExistencia(Convert.ToInt32(tbxCodigo.Text)))
            {
                string pergunta = $"{classeHelp.BuscarMensagem(84)} {tbxCodigo.Text}";
                DialogResult dialogResult = MessageBox.Show(this, pergunta, classeHelp.BuscarMensagem(2), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    classeCartesianaRede.Excluir(Convert.ToInt32(tbxCodigo.Text));

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
            FrmConsultaGrade frmConsulta = new FrmConsultaGrade(TipoGrade.CARTESIANO_ELEVACAO, codigoDominio);
            frmConsulta.ShowDialogFade(this);

            if (frmConsulta.Codigo > 0)
            {
                CodigoAtual = frmConsulta.Codigo;
                tbxCodigo.Text = frmConsulta.Codigo.ToString();

                var dados = classeCartesianaRede.RetornaDados(frmConsulta.Codigo);
                CarregaDados(dados);

                tbxDescricao.Focus();
                tbxDescricao.SelectAll();
            }
            else if (classeCartesianaRede.VerificaExistencia(Convert.ToInt32(tbxCodigo.Text)) == false)
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
            if (tbxX.Enabled)
            {
                dgvRede.Focus();
                return;
            }

            if (tbxElevacao.Enabled)
            {
                dgvElevacao.Focus();
                return;
            }

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

        /// <summary>
        /// Habilita/Desabilita coordenadas.
        /// </summary>
        /// <param name="value">True/False</param>
        private void HabilitaDesabilitaCoordenadas(bool value)
        {
            tbxX.Enabled = value;
            tbxY.Enabled = value;
        }

        /// <summary>
        /// Habilita/Desabilita coordenadas.
        /// </summary>
        /// <param name="value">True/False</param>
        private void HabilitaDesabilitaElevacao(bool value)
        {
            tbxElevacao.Enabled = value;
            tbxAltura.Enabled = value;
        }

        /// <summary>
        /// Limpar campos coordenadas.
        /// </summary>
        private void LimparCamposCoordenadas()
        {
            tbxX.ResetText();
            tbxY.ResetText();
        }

        /// <summary>
        /// Limpar campos elevação.
        /// </summary>
        private void LimparCamposElevacao()
        {
            tbxElevacao.ResetText();
            tbxAltura.ResetText();
        }

        /// <summary>
        /// Setar valores do GridView nos campos.
        /// </summary>
        /// <param name="row">Linha atual</param>
        private void SetarCoordenadasGridRow(DataGridViewRow row)
        {
            if (row != null)
            {
                tbxX.Text = row.Cells["XPNTS"].Value.ToString();
                tbxY.Text = row.Cells["YPNTS"].Value.ToString();
            }
            else
            {
                LimparCamposCoordenadas();
            }
        }

        /// <summary>
        /// Setar valores do GridView nos campos.
        /// </summary>
        /// <param name="row">Linha atual</param>
        private void SetarElevacaoGridRow(DataGridViewRow row)
        {
            if (row != null)
            {
                tbxElevacao.Text = row.Cells["ELEV"].Value.ToString();
                tbxAltura.Text = row.Cells["FLAG"].Value.ToString();
            }
            else
            {
                LimparCamposElevacao();
            }
        }

        /// <summary>
        /// Inserir coordenadas.
        /// </summary>
        private void InserirCoordenadas()
        {
            dgvRede.Tag = "INSERIR";
            HabilitaDesabilitaCoordenadas(true);
            LimparCamposCoordenadas();

            if (dgvRede.Rows.Count == 0)
            {
                decimal GRID_X = classeFonte.MenorCoordenada_X();
                decimal GRID_Y = classeFonte.MenorCoordenada_Y();

                GRID_X = GRID_X - 1000;
                GRID_Y = GRID_Y - 1000;

                tbxX.Text = GRID_X.ToString();
                tbxY.Text = GRID_Y.ToString();
            }

            tbxX.Focus();
        }

        /// <summary>
        /// Inserir elevação.
        /// </summary>
        private void InserirElevacao()
        {
            dgvElevacao.Tag = "INSERIR";
            HabilitaDesabilitaElevacao(true);
            LimparCamposElevacao();
            tbxElevacao.Focus();
        }

        /// <summary>
        /// Alterar coordenadas.
        /// </summary>
        private void AlterarCoordenadas()
        {
            dgvRede.Tag = "ALTERAR";
            HabilitaDesabilitaCoordenadas(true);
            tbxX.Text = dgvRede.CurrentRow.Cells["XPNTS"].Value.ToString();
            tbxY.Text = dgvRede.CurrentRow.Cells["YPNTS"].Value.ToString();

            tbxX.Focus();
        }

        /// <summary>
        /// Alterar elevação.
        /// </summary>
        private void AlterarElevacao()
        {
            dgvElevacao.Tag = "ALTERAR";
            HabilitaDesabilitaElevacao(true);
            tbxElevacao.Text = dgvElevacao.CurrentRow.Cells["ELEV"].Value.ToString();
            tbxAltura.Text = dgvElevacao.CurrentRow.Cells["FLAG"].Value.ToString();

            tbxElevacao.Focus();
        }

        /// <summary>
        /// Excluir coordenadas.
        /// </summary>
        private void ExcluirCoordenadas()
        {
            if (dgvRede.Rows.Count > 0)
            {
                Boolean marcado = dgvRede.Rows.OfType<DataGridViewRow>().Any(f => Convert.ToBoolean(f.Cells[0].Value) == true);

                if (marcado.Equals(false))
                {
                    dgvRede.CurrentRow.Cells[0].Value = true;
                    dgvRede.RefreshEdit();
                }

                DialogResult dialogResult = MessageBox.Show(this, "Deseja excluir os registros selecionados ?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    FrmLoading frmLoading = new FrmLoading(this);

                    Thread thrExcluir = new Thread(delegate ()
                    {
                        try
                        {
                            int count = 0;

                            for (Int32 i = dgvRede.Rows.Count - 1; i >= 0; i--)
                            {
                                if (Convert.ToBoolean(dgvRede.Rows[i].Cells[0].Value) == true)
                                {
                                    count++;
                                    frmLoading.AtualizarStatus(count);                                    

                                    CrossThreadOperation.Invoke(this, delegate { dgvRede.Rows.Remove(dgvRede.Rows[i]); });
                                }
                            }
                        }
                        catch { }
                    });
                    thrExcluir.Start();

                    frmLoading.thread = thrExcluir;
                    frmLoading.Texto = "Excluindo registro(s)...";
                    frmLoading.Style = ProgressBarStyle.Blocks;
                    frmLoading.Maximum = dgvRede.Rows.OfType<DataGridViewRow>().Count(f => Convert.ToBoolean(f.Cells[0].Value));
                    frmLoading.PermiteAbortarThread = false;
                    frmLoading.ShowDialogFade(this);

                    if (dgvRede.Rows.Count == 0)
                    {
                        InserirCoordenadas();
                    }
                }
                else if (marcado == false)
                {
                    dgvRede.CurrentRow.Cells[0].Value = false;
                    dgvRede.RefreshEdit();
                }
            }
        }

        /// <summary>
        /// Excluir elevação.
        /// </summary>
        private void ExcluirElevacao()
        {
            if (dgvElevacao.Rows.Count > 0)
            {
                Boolean marcado = dgvElevacao.Rows.OfType<DataGridViewRow>().Any(f => Convert.ToBoolean(f.Cells[0].Value) == true);

                if (marcado.Equals(false))
                {
                    dgvElevacao.CurrentRow.Cells[0].Value = true;
                    dgvElevacao.RefreshEdit();
                }

                DialogResult dialogResult = MessageBox.Show(this, "Deseja excluir os registros selecionados ?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    FrmLoading frmLoading = new FrmLoading(this);

                    Thread thrExcluir = new Thread(delegate ()
                    {
                        try
                        {
                            int count = 0;

                            for (Int32 i = dgvElevacao.Rows.Count - 1; i >= 0; i--)
                            {
                                if (Convert.ToBoolean(dgvElevacao.Rows[i].Cells[0].Value) == true)
                                {
                                    count++;
                                    frmLoading.AtualizarStatus(count);                                    

                                    CrossThreadOperation.Invoke(this, delegate { dgvElevacao.Rows.Remove(dgvElevacao.Rows[i]); });
                                }
                            }
                        }
                        catch { }
                    });
                    thrExcluir.Start();

                    frmLoading.thread = thrExcluir;
                    frmLoading.Texto = "Excluindo registro(s)...";
                    frmLoading.Style = ProgressBarStyle.Blocks;
                    frmLoading.Maximum = dgvElevacao.Rows.OfType<DataGridViewRow>().Count(f => Convert.ToBoolean(f.Cells[0].Value));
                    frmLoading.PermiteAbortarThread = false;
                    frmLoading.ShowDialogFade(this);

                    if (dgvElevacao.Rows.Count == 0)
                    {
                        this.Close();
                    }
                }
                else if (marcado == false)
                {
                    dgvElevacao.CurrentRow.Cells[0].Value = false;
                    dgvElevacao.RefreshEdit();
                }
            }
        }

        /// <summary>
        /// Salvar coordenadas.
        /// </summary>
        private void SalvarCoordenadas()
        {
            if (dgvRede.Tag != null)
            {
                if (Convert.ToDecimal(tbxX.Text) == 0)
                {
                    MessageBox.Show(this, $"X - {classeHelp.BuscarMensagem(3)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dgvRede.Tag.ToString() == "INSERIR")
                {
                    dgvRede.Rows.Add(false, dgvRede.Rows.Count + 1, Convert.ToDecimal(tbxX.Text), Convert.ToDecimal(tbxY.Text));
                    InserirCoordenadas();
                }
                else
                {
                    dgvRede.CurrentRow.Cells["XPNTS"].Value = Convert.ToDecimal(tbxX.Text);
                    dgvRede.CurrentRow.Cells["YPNTS"].Value = Convert.ToDecimal(tbxY.Text);
                    dgvRede.Focus();
                }
            }
        }

        /// <summary>
        /// Salvar elevação.
        /// </summary>
        private void SalvarElevacao()
        {
            if (dgvElevacao.Tag != null)
            {
                if (Convert.ToDecimal(tbxElevacao.Text) == 0 && Convert.ToDecimal(tbxAltura.Text) == 0)
                {
                    MessageBox.Show(this, $"Elevação - {classeHelp.BuscarMensagem(3)}", classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }                

                if (dgvElevacao.Tag.ToString() == "INSERIR")
                {
                    dgvElevacao.Rows.Add(false, dgvElevacao.Rows.Count + 1, Convert.ToDecimal(tbxElevacao.Text), Convert.ToDecimal(tbxAltura.Text));
                    InserirElevacao();
                }
                else
                {
                    dgvElevacao.CurrentRow.Cells["ELEV"].Value = Convert.ToDecimal(tbxElevacao.Text);
                    dgvElevacao.CurrentRow.Cells["FLAG"].Value = Convert.ToDecimal(tbxAltura.Text);
                    dgvElevacao.Focus();
                }
            }
        }

        #endregion

        #region Eventos FrmCartesianoRede

        protected override void OnLoad(EventArgs e)
        {
            SplashScreen.CloseSplashScreen(this);

            dgvRede.ScrollBarVisible(true);
            dgvElevacao.ScrollBarVisible(true);

            if (classeCartesianaRede.VerificaExistencia() == false)
            {
                InserirRegistro();
            }
            else
            {
                BotaoPrimeiro();
            }

            base.OnLoad(e);
        }        

        private void FrmCartesianoRede_KeyDown(object sender, KeyEventArgs e)
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

        #region Eventos dgvRede

        private void dgvRede_Enter(object sender, EventArgs e)
        {
            dgvRede.Tag = null;
            HabilitaDesabilitaCoordenadas(false);

            if (dgvRede.CurrentRow != null)
            {
                SetarCoordenadasGridRow(dgvRede.CurrentRow);
            }
        }

        private void dgvRede_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvRede.CurrentCell != null && dgvRede.CurrentCell.ColumnIndex == 0)
            {
                dgvRede.ReadOnly = true;
            }
        }

        private void dgvRede_SelectionChanged(object sender, EventArgs e)
        {
            SetarCoordenadasGridRow(dgvRede.CurrentRow);
        }

        private void dgvRede_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt)
            {
                switch (e.KeyCode)
                {
                    case Keys.A:
                        e.SuppressKeyPress = true;
                        AlterarCoordenadas();
                        break;
                }
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.Insert:
                        e.SuppressKeyPress = true;
                        InserirCoordenadas();
                        break;
                    case Keys.Delete:
                        e.SuppressKeyPress = true;
                        ExcluirCoordenadas();
                        break;
                    case Keys.Tab:
                        e.SuppressKeyPress = true;
                        dgvElevacao.Focus();
                        break;
                }
            }                    
        }

        #endregion

        #region Eventos dgvElevacao

        private void dgvElevacao_Enter(object sender, EventArgs e)
        {
            dgvElevacao.Tag = null;
            HabilitaDesabilitaElevacao(false);

            if (dgvElevacao.CurrentRow != null)
            {
                SetarElevacaoGridRow(dgvElevacao.CurrentRow);
            }
        }

        private void dgvElevacao_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvElevacao.CurrentCell != null && dgvElevacao.CurrentCell.ColumnIndex == 0)
            {
                dgvElevacao.ReadOnly = true;
            }
        }

        private void dgvElevacao_SelectionChanged(object sender, EventArgs e)
        {
            SetarElevacaoGridRow(dgvElevacao.CurrentRow);
        }

        private void dgvElevacao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt)
            {
                switch (e.KeyCode)
                {
                    case Keys.A:
                        e.SuppressKeyPress = true;
                        AlterarElevacao();
                        break;
                }
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.Insert:
                        e.SuppressKeyPress = true;
                        InserirElevacao();
                        break;
                    case Keys.Delete:
                        e.SuppressKeyPress = true;
                        ExcluirElevacao();
                        break;
                    case Keys.Tab:
                        e.SuppressKeyPress = true;
                        tbxDescricao.Focus();
                        break;
                }
            }
        }

        #endregion

        #region Eventos tbxX

        private void tbxX_Leave(object sender, EventArgs e)
        {
            if (this.ActiveControl != tbxY && tbxY.Focused == false)
            {
                HabilitaDesabilitaCoordenadas(false);
            }
        }

        #endregion

        #region Eventos tbxY

        private void tbxY_Leave(object sender, EventArgs e)
        {
            if (this.ActiveControl != tbxX && tbxX.Focused == false)
            {
                HabilitaDesabilitaCoordenadas(false);
            }
        }

        #endregion

        #region Eventos tbxElevacao

        private void tbxElevacao_Leave(object sender, EventArgs e)
        {
            if (this.ActiveControl != tbxAltura && tbxAltura.Focused == false)
            {
                HabilitaDesabilitaElevacao(false);
            }
        }

        #endregion

        #region Eventos tbxAltura

        private void tbxAltura_Leave(object sender, EventArgs e)
        {
            if (this.ActiveControl != tbxElevacao && tbxElevacao.Focused == false)
            {
                HabilitaDesabilitaElevacao(false);
            }
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
