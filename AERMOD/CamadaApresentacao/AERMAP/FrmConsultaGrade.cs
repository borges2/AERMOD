using AERMOD.LIB.Desenvolvimento;
using AERMOD.LIB.Forms;
using CamadaLogicaNegocios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AERMOD.CamadaApresentacao.AERMAP
{
    public partial class FrmConsultaGrade : Form
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

        /// <summary>
        /// Classe de negócios ClsSamson.
        /// </summary>
        ClsAERMAP_Cartesiano clsCartesiano = null;

        /// <summary>
        /// Get a classe de parametro
        /// </summary>
        ClsAERMAP_Cartesiano classeCartesiano
        {
            get
            {
                if (clsCartesiano == null)
                {
                    clsCartesiano = new ClsAERMAP_Cartesiano(Base.ConfiguracaoRede, codigoDominio);
                }

                return clsCartesiano;
            }
        }

        /// <summary>
        /// Classe de negócios ClsSamson.
        /// </summary>
        ClsAERMAP_CartesianoRede clsCartesianoRede = null;

        /// <summary>
        /// Get a classe de parametro
        /// </summary>
        ClsAERMAP_CartesianoRede classeCartesianoRede
        {
            get
            {
                if (clsCartesianoRede == null)
                {
                    clsCartesianoRede = new ClsAERMAP_CartesianoRede(Base.ConfiguracaoRede, codigoDominio);
                }

                return clsCartesianoRede;
            }
        }

        /// <summary>
        /// Classe de negócios ClsAERMAP_CartesianoDiscreto.
        /// </summary>
        ClsAERMAP_CartesianoDiscreto clsCartesianoDisc = null;

        /// <summary>
        /// Get a classe de parametro
        /// </summary>
        ClsAERMAP_CartesianoDiscreto classeCartesianoDisc
        {
            get
            {
                if (clsCartesianoDisc == null)
                {
                    clsCartesianoDisc = new ClsAERMAP_CartesianoDiscreto(Base.ConfiguracaoRede, codigoDominio);
                }

                return clsCartesianoDisc;
            }
        }

        /// <summary>
        /// Classe de negócios ClsAERMAP_EVALFILE.
        /// </summary>
        ClsAERMAP_EVALFILE clsEVALFILE = null;

        /// <summary>
        /// Get a classe de parametro
        /// </summary>
        ClsAERMAP_EVALFILE classeEVALFILE
        {
            get
            {
                if (clsEVALFILE == null)
                {
                    clsEVALFILE = new ClsAERMAP_EVALFILE(Base.ConfiguracaoRede, codigoDominio);
                }

                return clsEVALFILE;
            }
        }

        /// <summary>
        /// Código do registro retornado.
        /// </summary>
        public int Codigo { get; set; }

        /// <summary>
        /// Tipo da grade.
        /// </summary>
        TipoGrade tipoGrade;

        /// <summary>
        /// Código do domínio.
        /// </summary>
        int codigoDominio;

        #endregion

        #region Construtor

        public FrmConsultaGrade(TipoGrade tipoGrade, int codigoDominio)
        {
            InitializeComponent();

            this.Icon = Properties.Resources.find.ConvertImageToIcon();

            this.tipoGrade = tipoGrade;
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
        }

        /// <summary>
        /// Carregar GridView com registros da grade.
        /// </summary>
        /// <param name="codigoArquivo">Código do arquivo inserido no BD.</param>
        private void CarregarRegistros()
        {
            dgvGrade.Focus();

            FrmLoading frmLoading = new FrmLoading(this);

            Thread thrCarregar = new Thread(delegate ()
            {
                DataTable dt = new DataTable();

                switch (tipoGrade)
                {
                    case TipoGrade.CARTESIANO:
                        dt = classeCartesiano.RetornaDados();
                        break;
                    case TipoGrade.CARTESIANO_ELEVACAO:
                        dt = classeCartesianoRede.RetornaDados();
                        break;
                    case TipoGrade.CARTESIANO_DISCRETO:
                        dt = classeCartesianoDisc.RetornaDados();
                        break;
                    case TipoGrade.CARTESIANO_EVALFILE:
                        dt = classeEVALFILE.RetornaDados();
                        break;
                }

                frmLoading.Maximum = dt.Rows.Count;

                CrossThreadOperation.Invoke(this, delegate
                {
                    dgvGrade.Rows.Clear();
                    int count = 0;

                    foreach (DataRow linha in dt.Rows)
                    {
                        count++;
                        frmLoading.AtualizarStatus(count);                        

                        dgvGrade.Rows.Add(false, 
                                          linha["CODIGO"], 
                                          linha["DESCRICAO"], 
                                          linha["CODIGO_DOMINIO"]);

                        if (Convert.ToBoolean(linha["EM_USO"]))
                        {
                            dgvGrade.Rows[dgvGrade.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Blue;
                        }
                    }
                });
            });
            thrCarregar.Start();

            frmLoading.thread = thrCarregar;
            frmLoading.PermiteAbortarThread = false;
            frmLoading.Texto = "Carregando registro(s)...";
            frmLoading.Style = ProgressBarStyle.Blocks;
            frmLoading.ShowDialogFade(this);
        }

        /// <summary>
        /// Excluir registro selecionado.
        /// </summary>       
        private void ExcluirRegistro()
        {
            if (dgvGrade.Rows.Count > 0)
            {
                Boolean marcado = dgvGrade.Rows.OfType<DataGridViewRow>().Any(f => Convert.ToBoolean(f.Cells[0].Value) == true);

                if (marcado.Equals(false))
                {
                    dgvGrade.CurrentRow.Cells[0].Value = true;
                    dgvGrade.RefreshEdit();
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

                            for (Int32 i = dgvGrade.Rows.Count - 1; i >= 0; i--)
                            {
                                if (Convert.ToBoolean(dgvGrade.Rows[i].Cells[0].Value) == true)
                                {
                                    count++;
                                    frmLoading.AtualizarStatus(count);

                                    switch (tipoGrade)
                                    {
                                        case TipoGrade.CARTESIANO:
                                            #region CARTESIANO

                                            classeCartesiano.Excluir(Convert.ToInt32(dgvGrade.Rows[i].Cells["CODIGO"].Value));

                                            #endregion
                                            break;
                                        case TipoGrade.CARTESIANO_ELEVACAO:
                                            #region CARTESIANO ELEVAÇÃO                                            

                                            classeCartesianoRede.Excluir(Convert.ToInt32(dgvGrade.Rows[i].Cells["CODIGO"].Value));

                                            #endregion
                                            break;
                                        case TipoGrade.CARTESIANO_DISCRETO:
                                            #region CARTESIANO DISCRETO

                                            classeCartesianoDisc.Excluir(Convert.ToInt32(dgvGrade.Rows[i].Cells["CODIGO"].Value));

                                            #endregion
                                            break;
                                        case TipoGrade.CARTESIANO_EVALFILE:
                                            #region CARTESIANO EVALFILE

                                            classeEVALFILE.Excluir(Convert.ToInt32(dgvGrade.Rows[i].Cells["CODIGO"].Value));

                                            #endregion
                                            break;
                                    }

                                    CrossThreadOperation.Invoke(this, delegate { dgvGrade.Rows.Remove(dgvGrade.Rows[i]); });
                                }
                            }
                        }
                        catch { }
                    });
                    thrExcluir.Start();

                    frmLoading.thread = thrExcluir;
                    frmLoading.Texto = "Excluindo registro(s)...";
                    frmLoading.Style = ProgressBarStyle.Blocks;
                    frmLoading.Maximum = dgvGrade.Rows.OfType<DataGridViewRow>().Count(f => Convert.ToBoolean(f.Cells[0].Value));
                    frmLoading.PermiteAbortarThread = false;
                    frmLoading.ShowDialogFade(this);

                    if (dgvGrade.Rows.Count == 0)
                    {
                        this.Close();
                    }
                }
                else if (marcado == false)
                {
                    dgvGrade.CurrentRow.Cells[0].Value = false;
                    dgvGrade.RefreshEdit();
                }
            }
        }

        /// <summary>
        /// Retornar consulta.
        /// </summary>
        private void RetornarConsulta()
        {
            if (dgvGrade.CurrentRow != null)
            {
                Codigo = Convert.ToInt32(dgvGrade.CurrentRow.Cells["CODIGO"].Value);

                this.Close();
            }
        }

        /// <summary>
        /// Atualizar uso do registro.
        /// </summary>
        private void AtualizarUso()
        {
            if (dgvGrade.CurrentRow != null)
            {
                Boolean marcado = dgvGrade.Rows.OfType<DataGridViewRow>().Any(f => Convert.ToBoolean(f.Cells[0].Value) == true);

                if (marcado.Equals(false))
                {
                    dgvGrade.CurrentRow.Cells[0].Value = true;
                    dgvGrade.RefreshEdit();
                }

                FrmLoading frmLoading = new FrmLoading(this);

                Thread thrExcluir = new Thread(delegate ()
                {
                    try
                    {
                        int count = 0;
                        List<int> lista = new List<int>();

                        foreach (DataGridViewRow row in dgvGrade.Rows)
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

                        switch (tipoGrade)
                        {
                            case TipoGrade.CARTESIANO:
                                classeCartesiano.AtualizarUso(lista);
                                break;
                            case TipoGrade.CARTESIANO_ELEVACAO:
                                classeCartesianoRede.AtualizarUso(lista);
                                break;
                            case TipoGrade.CARTESIANO_DISCRETO:
                                classeCartesianoDisc.AtualizarUso(lista);
                                break;
                            case TipoGrade.CARTESIANO_EVALFILE:
                                classeEVALFILE.AtualizarUso(lista);
                                break;
                        }                        
                    }
                    catch { }
                });
                thrExcluir.Start();

                frmLoading.thread = thrExcluir;
                frmLoading.Texto = "Atualizando registro(s)...";
                frmLoading.Style = ProgressBarStyle.Blocks;
                frmLoading.Maximum = dgvGrade.Rows.OfType<DataGridViewRow>().Count(f => Convert.ToBoolean(f.Cells[0].Value));
                frmLoading.ShowDialogFade(this);
            }
        }

        #endregion

        #region Eventos FrmConsultaGrade

        protected override void OnShown(EventArgs e)
        {
            CarregarRegistros();

            base.OnShown(e);
        }
        private void FrmConsultaGrade_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        e.SuppressKeyPress = true;
                        AtualizarUso();
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
                    case Keys.Delete:
                        e.SuppressKeyPress = true;
                        ExcluirRegistro();
                        break;
                    case Keys.Enter:
                        e.SuppressKeyPress = true;
                        RetornarConsulta();
                        break;
                    case Keys.Escape:
                        this.Close();
                        break;
                }
            }
        }

        #endregion

        #region Eventos dgvGrade

        private void dgvGrade_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            RetornarConsulta();
        }

        private void dgvGrade_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvGrade.CurrentCell != null && dgvGrade.CurrentCell.ColumnIndex == 0) dgvGrade.ReadOnly = true;
        }

        #endregion
    }
}
