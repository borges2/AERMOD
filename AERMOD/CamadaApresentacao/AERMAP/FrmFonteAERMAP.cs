using AERMOD.LIB.Componentes.GridView;
using AERMOD.LIB.Desenvolvimento;
using AERMOD.LIB.Forms;
using CamadaLogicaNegocios;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace AERMOD
{
    public partial class FrmFonteAERMAP : Form
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

        #endregion

        #region Construtor

        public FrmFonteAERMAP()
        {
            InitializeComponent();

            this.Icon = Properties.Resources.form2.ConvertImageToIcon();
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Carregar fuso horário.
        /// </summary>
        private void CarregarTipoFonte()
        {
            DataTable dtDados = Enumeradores.RetornarTipoFonte();

            cbxTipoFonte.DisplayMember = "DESCRICAO";
            cbxTipoFonte.ValueMember = "CODIGO";
            cbxTipoFonte.DataSource = dtDados;           
        }

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
                                          item["CODIGO"].ToString().PadLeft(2, '0'),
                                          item["DESCRICAO"],
                                          RetornaTipoFonte(item["TIPO"]), 
                                          item["X"], 
                                          item["Y"], 
                                          item["CODIGO"],
                                          item["EM_USO"]);
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
        /// Atualizar (ID) sequência dos registros.
        /// </summary>
        private void AtualizarSequencia()
        {
            int count = 0;

            foreach (DataGridViewRow linha in dgvFonte.Rows)
            {
                count++;
                CrossThreadOperation.Invoke(this, delegate { linha.Cells["ID"].Value = count.ToString().PadLeft(2, '0'); });
            }
        }

        /// <summary>
        /// Excluir fonte emissora.
        /// </summary>
        private void ExcluirFonte()
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

                                    classeFonte.Excluir(Convert.ToInt32(dgvFonte.Rows[i].Cells["CODIGO"].Value));

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
                        InserirFonte();
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
        /// Inserir fonte emissora.
        /// </summary>
        private void InserirFonte()
        {
            HabilitarCampos(true);
            LimparCampos();

            tbxDescricao.Focus();
            dgvFonte.Tag = "INSERIR";
        }

        /// <summary>
        /// Alterar fonte emissora.
        /// </summary>
        private void AlterarFonte()
        {
            if (dgvFonte.Focused && dgvFonte.CurrentRow != null && cbxTipoFonte.Enabled == false)
            {
                HabilitarCampos(true);

                TipoFonte tipoFonte = dgvFonte.CurrentRow.Cells["TIPO"].Value.ToString().GetEnumFromDescription<TipoFonte>();
                cbxTipoFonte.SelectedValue = (int)tipoFonte;
                tbxDescricao.Text = dgvFonte.CurrentRow.Cells["DESCRICAO"].Value.ToString();
                tbxX.Text = dgvFonte.CurrentRow.Cells["X"].Value.ToString();
                tbxY.Text = dgvFonte.CurrentRow.Cells["Y"].Value.ToString();

                dgvFonte.Tag = "ALTERAR";
                tbxDescricao.Focus();
            }
        }

        /// <summary>
        /// Habilitar/Desabilitar campos.
        /// </summary>
        /// <param name="valor">True/False</param>
        private void HabilitarCampos(bool valor)
        {
            tbxDescricao.Enabled = valor;
            cbxTipoFonte.Enabled = valor;
            tbxX.Enabled = valor;
            tbxY.Enabled = valor;
            btnSalvar.Enabled = valor;
        }

        /// <summary>
        /// Salvar fonte emissora em edição.
        /// </summary>
        private void SalvarFonte()
        {
            if (tbxDescricao.Enabled == false)
            {
                return;
            }

            #region Validação

            if (tbxDescricao.Text.Trim().Length == 0)
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(3), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxDescricao.Focus();
                return;
            }

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

            int codigo = dgvFonte.Tag.ToString() == "ALTERAR" ? Convert.ToInt32(dgvFonte.CurrentRow.Cells["CODIGO"].Value) : 0;
            string descricao = tbxDescricao.Text;
            decimal X = Convert.ToDecimal(tbxX.Text);
            decimal Y = Convert.ToDecimal(tbxY.Text);

            int retorno = classeFonte.VerificarDuplicidade(codigo, X, Y, descricao);
            if (retorno > 0)
            {
                var row = dgvFonte.Rows.OfType<DataGridViewRow>().FirstOrDefault(I => Convert.ToInt32(I.Cells["CODIGO"].Value) == retorno);
                if (row != null)
                {
                    string ID = row.Cells["ID"].Value.ToString();
                    string msg = $"{classeHelp.BuscarMensagem(4)}\nID: {ID}";
                    
                    MessageBox.Show(this, msg, classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tbxX.Focus();
                    return;
                }                
            }

            #endregion

            dynamic fonte = new ExpandoObject();
            fonte.CODIGO = codigo;
            fonte.TIPO = Convert.ToInt32(cbxTipoFonte.SelectedValue);
            fonte.X = Convert.ToDecimal(tbxX.Text);
            fonte.Y = Convert.ToDecimal(tbxY.Text);
            fonte.DESCRICAO = descricao;
            fonte.EM_USO = dgvFonte.Tag.ToString() == "INSERIR" ? false : Convert.ToBoolean(dgvFonte.CurrentRow.Cells["EM_USO"].Value);

            codigo = classeFonte.Atualizar(fonte);
            if (codigo > 0)
            {
                if (dgvFonte.Tag.ToString() == "INSERIR")
                {
                    int ID = dgvFonte.Rows.Count > 0 ? dgvFonte.Rows.OfType<DataGridViewRow>().Max(I => Convert.ToInt32(I.Cells["ID"].Value)) : 0;
                    ID++;

                    dgvFonte.Rows.Add(false, 
                                      ID.ToString().PadLeft(2, '0'), 
                                      descricao,
                                      RetornaTipoFonte(fonte.TIPO), 
                                      fonte.X, 
                                      fonte.Y, 
                                      codigo,
                                      false);

                    InserirFonte();
                }
                else
                {
                    dgvFonte.CurrentRow.Cells["DESCRICAO"].Value = fonte.DESCRICAO;
                    dgvFonte.CurrentRow.Cells["TIPO"].Value = RetornaTipoFonte(fonte.TIPO);
                    dgvFonte.CurrentRow.Cells["X"].Value = fonte.X;
                    dgvFonte.CurrentRow.Cells["Y"].Value = fonte.Y;                    
                    dgvFonte.Focus();
                }                
            }
            else
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(5), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxX.Focus();
            }            
        }

        /// <summary>
        /// Limpar campos.
        /// </summary>
        private void LimparCampos()
        {
            tbxDescricao.ResetText();
            cbxTipoFonte.SelectedValue = 0;
            tbxX.ResetText();
            tbxY.ResetText();
        }

        /// <summary>
        /// Sair da edição ou fechar o cadastro. 
        /// </summary>
        private void SairForm()
        {
            if (dgvFonte.Rows.Count > 0 && tbxDescricao.Enabled)
            {
                dgvFonte.Focus();
            }
            else
            {
                this.Close();
            }
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
        /// Atualizar uso do registro.
        /// </summary>
        private void AtualizarUso()
        {
            if (dgvFonte.CurrentRow != null)
            {
                Boolean marcado = dgvFonte.Rows.OfType<DataGridViewRow>().Any(f => Convert.ToBoolean(f.Cells[0].Value) == true);

                if (marcado.Equals(false))
                {
                    dgvFonte.CurrentRow.Cells[0].Value = true;
                    dgvFonte.RefreshEdit();
                }

                FrmLoading frmLoading = new FrmLoading(this);

                Thread thrExcluir = new Thread(delegate ()
                {
                    try
                    {
                        int count = 0;
                        List<int> lista = new List<int>();

                        foreach (DataGridViewRow row in dgvFonte.Rows)
                        {
                            row.DefaultCellStyle.ForeColor = Color.Black;

                            if (Convert.ToBoolean(row.Cells[0].Value))
                            {
                                count++;
                                frmLoading.AtualizarStatus(count);

                                lista.Add(Convert.ToInt32(row.Cells["CODIGO"].Value));

                                CrossThreadOperation.Invoke(this, delegate 
                                {
                                    row.Cells[0].Value = false;
                                    row.DefaultCellStyle.ForeColor = Color.Blue;
                                });
                            }
                        }

                        frmLoading.Style = ProgressBarStyle.Marquee;

                        classeFonte.AtualizarUso(lista);                        
                    }
                    catch { }
                });
                thrExcluir.Start();

                frmLoading.thread = thrExcluir;
                frmLoading.Texto = "Atualizando registro(s)...";
                frmLoading.Style = ProgressBarStyle.Blocks;
                frmLoading.Maximum = dgvFonte.Rows.OfType<DataGridViewRow>().Count(f => Convert.ToBoolean(f.Cells[0].Value));
                frmLoading.ShowDialogFade(this);                                
            }
        }

        #endregion

        #region Eventos FrmFonteAERMAP

        private void FrmFonteAERMAP_Load(object sender, EventArgs e)
        {
            dgvFonte.ScrollBarVisible(true);

            CarregarTipoFonte();
        }

        protected override void OnShown(EventArgs e)
        {
            CarregarFontes();

            base.OnShown(e);
        }

        private void FrmFonteAERMAP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt)
            {
                switch (e.KeyCode)
                {
                    case Keys.A:
                        e.SuppressKeyPress = true;
                        AlterarFonte();
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

                        if (tbxDescricao.Enabled)
                        {
                            SalvarFonte();
                        }
                        else
                        {
                            AtualizarUso();
                        }
                        break;
                    case Keys.Escape:
                        e.SuppressKeyPress = true;
                        SairForm();
                        break;
                    case Keys.Insert:
                        e.SuppressKeyPress = true;
                        InserirFonte();
                        break;
                    case Keys.Delete:
                        e.SuppressKeyPress = true;
                        ExcluirFonte();
                        break;
                }
            }
        }

        #endregion

        #region Eventos dgvFonte

        private void dgvFonte_Enter(object sender, EventArgs e)
        {
            if (dgvFonte.Rows.Count > 0)
            {
                dgvFonte.Tag = null;
                HabilitarCampos(false);
            }
            else
            {
                tbxX.Focus();
                dgvFonte.Tag = "INSERIR";
            }            
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
                TipoFonte tipoFonte = dgvFonte.CurrentRow.Cells["TIPO"].Value.ToString().GetEnumFromDescription<TipoFonte>();

                tbxDescricao.Text = dgvFonte.CurrentRow.Cells["DESCRICAO"].Value.ToString();
                cbxTipoFonte.SelectedValue = (int)tipoFonte;
                tbxX.Text = dgvFonte.CurrentRow.Cells["X"].Value.ToString();
                tbxY.Text = dgvFonte.CurrentRow.Cells["Y"].Value.ToString();
            }
        }

        private void dgvFonte_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AlterarFonte();
        }

        private void dgvFonte_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var row = dgvFonte.Rows[e.RowIndex];

            if (Convert.ToBoolean(row.Cells["EM_USO"].Value))
            {
                row.DefaultCellStyle.ForeColor = Color.Blue;
            }
        }

        #endregion

        #region Eventos btnSalvar

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            SalvarFonte();
        }

        #endregion

        #region Eventos statusStripConsulta

        private void btnAjuda_Click(object sender, EventArgs e)
        {
            AbrirAjuda();
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            InserirFonte();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            AlterarFonte();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            ExcluirFonte();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            SairForm();
        }

        #endregion
    }
}
