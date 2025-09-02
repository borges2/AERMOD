using AERMOD.LIB.Componentes.GridView;
using AERMOD.LIB.Desenvolvimento;
using AERMOD.LIB.Forms;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AERMOD.CamadaApresentacao.PRINCIPAL
{
    public partial class FrmCoordenadasAERMOD : Form
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

        ClsAERMOD clsAERMOD = null;

        /// <summary>
        /// Get a classe de parametro
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
        public List<Tuple<decimal, decimal>> LstCoordenadas { get; set; }

        /// <summary>
        /// Código da fonte.
        /// </summary>
        int codigoFonte;

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
        /// <param name="codigoFonte">Código da fonte</param>
        /// <param name="lstCoordenadas">Lista de coordenadas</param>
        public FrmCoordenadasAERMOD(int codigoFonte, int codigoPeriodo, int codigoPoluente, List<Tuple<decimal, decimal>> lstCoordenadas)
        {
            InitializeComponent();

            this.Icon = Properties.Resources.form2.ConvertImageToIcon();
            this.codigoFonte = codigoFonte;
            this.codigoPeriodo = codigoPeriodo;
            this.codigoPoluente = codigoPoluente;
            this.LstCoordenadas = lstCoordenadas;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Carregar coordenadas.
        /// </summary>
        private void CarregarCoordenadas()
        {
            FrmLoading frmLoading = new FrmLoading(this);

            Thread thr = new Thread(delegate ()
            {
                DataTable dt = classeAERMOD.RetornaParametroAreaPolyCoordenadas(codigoFonte, codigoPeriodo, codigoPoluente);

                frmLoading.Style = ProgressBarStyle.Blocks;
                frmLoading.Maximum = dt.Rows.Count > 0 ? dt.Rows.Count : LstCoordenadas.Count;

                CrossThreadOperation.Invoke(this, delegate
                {
                    int count = 0;

                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow item in dt.Rows)
                        {
                            count++;
                            frmLoading.AtualizarStatus(count);

                            dgvFonte.Rows.Add(false, item["SEQUENCIA"], item["X"], item["Y"]);
                        }
                    }
                    else
                    {
                        foreach (var item in LstCoordenadas)
                        {
                            count++;
                            frmLoading.AtualizarStatus(count);

                            dgvFonte.Rows.Add(false, count, item.Item1, item.Item2);
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

            if (dgvFonte.Rows.Count > 0)
            {
                HabilitarCampos(false);

                dgvFonte.Rows[0].Cells[0].Selected = true;
                dgvFonte.Focus();
            }
            else
            {
                dgvFonte.Focus();
                InserirCoordenada();
            }
        }

        /// <summary>
        /// Atualizar (ID) sequência dos registros.
        /// </summary>
        private void AtualizarSequencia()
        {
            int count = 0;

            foreach (DataGridViewRow linha in dgvFonte.Rows)
            {
                count++;
                CrossThreadOperation.Invoke(this, delegate { linha.Cells["SEQUENCIA"].Value = count.ToString().PadLeft(2, '0'); });
            }
        }

        /// <summary>
        /// Excluir fonte emissora.
        /// </summary>
        private void ExcluirCoordenada()
        {
            if (dgvFonte.Focused && dgvFonte.Rows.Count > 0)
            {
                Boolean marcado = dgvFonte.Rows.OfType<DataGridViewRow>().Any(f => Convert.ToBoolean(f.Cells[0].Value) == true);

                if (marcado.Equals(false))
                {
                    dgvFonte.CurrentRow.Cells[0].Value = true;
                    dgvFonte.RefreshEdit();
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

                            for (Int32 i = dgvFonte.Rows.Count - 1; i >= 0; i--)
                            {
                                if (Convert.ToBoolean(dgvFonte.Rows[i].Cells[0].Value) == true)
                                {
                                    count++;
                                    frmLoading.AtualizarStatus(count);

                                    CrossThreadOperation.Invoke(this, delegate
                                    {
                                        dgvFonte.Rows.Remove(dgvFonte.Rows[i]);
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
                    frmLoading.Maximum = dgvFonte.Rows.OfType<DataGridViewRow>().Count(f => Convert.ToBoolean(f.Cells[0].Value));
                    frmLoading.ShowDialogFade(this);

                    if (dgvFonte.RowCount == 0)
                    {
                        InserirCoordenada();
                    }
                }
                else if (marcado == false)
                {
                    dgvFonte.CurrentRow.Cells[0].Value = false;
                    dgvFonte.RefreshEdit();
                }
            }
        }

        /// <summary>
        /// Inserir coordenada.
        /// </summary>
        private void InserirCoordenada()
        {
            HabilitarCampos(true);
            LimparCampos();

            tbxX.Focus();
            tbxX.SelectAll();
            dgvFonte.Tag = "INSERIR";
        }

        /// <summary>
        /// Alterar coordenada.
        /// </summary>
        private void AlterarCoordenada()
        {
            if (dgvFonte.Focused && dgvFonte.CurrentRow != null && tbxX.Enabled == false)
            {
                HabilitarCampos(true);

                tbxX.Text = dgvFonte.CurrentRow.Cells["X"].Value.ToString();
                tbxY.Text = dgvFonte.CurrentRow.Cells["Y"].Value.ToString();

                dgvFonte.Tag = "ALTERAR";
                tbxX.Focus();
            }
        }

        /// <summary>
        /// Habilitar/Desabilitar campos.
        /// </summary>
        /// <param name="valor">True/False</param>
        private void HabilitarCampos(bool valor)
        {
            tbxX.Enabled = valor;
            tbxY.Enabled = valor;

            tbxX.Focus();

            if (valor)
            {
                if (tbxX.Focused)
                {
                    btnInserirCoordenada.Enabled = false;
                    btnAlterarCoordenada.Enabled = false;
                    btnExcluirCoordenada.Enabled = false;
                }
            }
            else
            {
                btnInserirCoordenada.Enabled = true;
                btnAlterarCoordenada.Enabled = true;
                btnExcluirCoordenada.Enabled = true;
            }
        }

        /// <summary>
        /// Salvar coordenadas.
        /// </summary>
        private void SalvarCoordenadas()
        {
            if (tbxX.Enabled == false)
            {
                LstCoordenadas = new List<Tuple<decimal, decimal>>();

                foreach (DataGridViewRow linha in dgvFonte.Rows)
                {
                    LstCoordenadas.Add(Tuple.Create(Convert.ToDecimal(linha.Cells["X"].Value), Convert.ToDecimal(linha.Cells["Y"].Value)));
                }

                this.Close();
                return;
            }

            #region Validação

            if (Convert.ToDecimal(tbxX.Text) == 0)
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(3), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxX.Focus();
                return;
            }

            if (Convert.ToDecimal(tbxY.Text) == 0)
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(3), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxY.Focus();
                return;
            }

            int sequencia = dgvFonte.Tag.ToString() == "ALTERAR" ? Convert.ToInt32(dgvFonte.CurrentRow.Cells["SEQUENCIA"].Value) : 0;
            decimal X = Convert.ToDecimal(tbxX.Text);
            decimal Y = Convert.ToDecimal(tbxY.Text);

            int retorno = classeAERMOD.VerificarDuplicidadeAreaPolyCoordenadas(codigoFonte, codigoPeriodo, codigoPoluente, sequencia, X, Y);
            if (retorno > 0)
            {
                var row = dgvFonte.Rows.OfType<DataGridViewRow>().FirstOrDefault(I => Convert.ToInt32(I.Cells["SEQUENCIA"].Value) == retorno);
                if (row != null)
                {
                    string ID = row.Cells["SEQUENCIA"].Value.ToString();
                    string msg = $"{classeHelp.BuscarMensagem(70)}\nID: {ID}";

                    MessageBox.Show(this, msg, classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tbxX.Focus();
                    return;
                }
            }

            #endregion

            if (sequencia == 0)
            {
                int ID = dgvFonte.Rows.Count > 0 ? dgvFonte.Rows.OfType<DataGridViewRow>().Max(I => Convert.ToInt32(I.Cells["SEQUENCIA"].Value)) : 0;
                ID++;

                dgvFonte.Rows.Add(false, ID.ToString().PadLeft(2, '0'), Convert.ToDecimal(tbxX.Text), Convert.ToDecimal(tbxY.Text));

                InserirCoordenada();
            }
            else
            {
                dgvFonte.CurrentRow.Cells["X"].Value = Convert.ToDecimal(tbxX.Text);
                dgvFonte.CurrentRow.Cells["Y"].Value = Convert.ToDecimal(tbxY.Text);
                dgvFonte.Focus();
            }
        }

        /// <summary>
        /// Limpar campos.
        /// </summary>
        private void LimparCampos()
        {
            tbxX.ResetText();
            tbxY.ResetText();
        }

        /// <summary>
        /// Sair da edição ou fechar o cadastro. 
        /// </summary>
        private bool SairForm(bool fechando = false)
        {
            if (dgvFonte.Rows.Count > 0 && tbxX.Enabled)
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
        /// Abrir tela de ajuda.
        /// </summary>
        private void AbrirAjuda()
        {
            string caminho = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "Help");
            Help.ShowHelp(this, $"{caminho}\\AERMOD.chm", HelpNavigator.TopicId, "50");
            //Help.ShowHelp(this, $"{caminho}\\AERMOD.chm", HelpNavigator.TableOfContents, "10");
            //Help.ShowHelp(this, $"{caminho}\\AERMOD.chm", HelpNavigator.Index, "10");
        }

        #endregion

        #region Eventos FrmCoordenadasAERMOD

        private void FrmCoordenadasAERMOD_Load(object sender, EventArgs e)
        {
            dgvFonte.ScrollBarVisible(true);
            CarregarCoordenadas();
        }

        private void FrmCoordenadasAERMOD_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt)
            {
                switch (e.KeyCode)
                {
                    case Keys.A:
                        e.SuppressKeyPress = true;
                        AlterarCoordenada();
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
                    case Keys.Enter:
                        e.SuppressKeyPress = true;
                        SalvarCoordenadas();
                        break;
                    case Keys.Escape:
                        e.SuppressKeyPress = true;
                        SairForm();
                        break;
                    case Keys.Insert:
                        e.SuppressKeyPress = true;
                        InserirCoordenada();
                        break;
                    case Keys.Delete:
                        e.SuppressKeyPress = true;
                        ExcluirCoordenada();
                        break;
                }
            }
        }

        private void FrmCoordenadasAERMOD_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (SairForm(true) == false)
            {
                e.Cancel = true;
            }
        }

        #endregion

        #region Eventos dgvFonte

        private void dgvFonte_Enter(object sender, EventArgs e)
        {
            HabilitarCampos(false);
            dgvFonte.Tag = null;
        }

        private void dgvFonte_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvFonte.CurrentCell != null && dgvFonte.CurrentCell.ColumnIndex == 0)
            {
                dgvFonte.ReadOnly = true;
            }
        }

        private void dgvFonte_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvFonte.CurrentRow != null)
            {
                tbxX.Text = dgvFonte.CurrentRow.Cells["X"].Value.ToString();
                tbxY.Text = dgvFonte.CurrentRow.Cells["Y"].Value.ToString();
            }
        }

        private void dgvFonte_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AlterarCoordenada();
        }

        #endregion        

        #region Eventos btnInserirCoordenada

        private void btnInserirCoordenada_Click(object sender, EventArgs e)
        {
            InserirCoordenada();
        }

        #endregion

        #region Eventos btnAlterarCoordenada
        private void btnAlterarCoordenada_Click(object sender, EventArgs e)
        {
            AlterarCoordenada();
        }

        #endregion

        #region Eventos btnExcluirCoordenada

        private void btnExcluirCoordenada_Click(object sender, EventArgs e)
        {
            ExcluirCoordenada();
        }

        #endregion

        #region Eventos statusStripConsulta

        private void btnAjuda_ButtonClick(object sender, EventArgs e)
        {
            AbrirAjuda();
        }

        private void btnSalvar_ButtonClick(object sender, EventArgs e)
        {
            SalvarCoordenadas();
        }

        private void btnSair_ButtonClick(object sender, EventArgs e)
        {
            SairForm();
        }

        #endregion
    }
}
