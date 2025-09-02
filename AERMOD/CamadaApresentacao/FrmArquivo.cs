using AERMOD.LIB;
using AERMOD.LIB.Desenvolvimento;
using AERMOD.LIB.Forms;
using CamadaLogicaNegocios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AERMOD.CamadaApresentacao
{
    public partial class FrmArquivo : Form
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
        ClsSamson clsSamson = null;

        /// <summary>
        /// Get a classe de parametro
        /// </summary>
        ClsSamson classeSamson
        {
            get
            {
                if (clsSamson == null)
                {
                    clsSamson = new ClsSamson(Base.ConfiguracaoRede);
                }

                return clsSamson;
            }
        }

        /// <summary>
        /// Classe de negócios ClsFSL.
        /// </summary>
        ClsFSL clsFSL = null;

        /// <summary>
        /// Get a classe de parametro
        /// </summary>
        ClsFSL classeFSL
        {
            get
            {
                if (clsFSL == null)
                {
                    clsFSL = new ClsFSL(Base.ConfiguracaoRede);
                }

                return clsFSL;
            }
        }

        /// <summary>
        /// Classe de negócios clsTIF.
        /// </summary>
        ClsTIF clsTIF = null;

        /// <summary>
        /// Get a classe de parametro
        /// </summary>
        ClsTIF classeTIF
        {
            get
            {
                if (clsTIF == null)
                {
                    clsTIF = new ClsTIF(Base.ConfiguracaoRede);
                }

                return clsTIF;
            }
        }

        ClsAERMOD clsAERMOD = null;

        /// <summary>
        /// Get classe de negócios ClsAERMAP
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
        /// Tipo do arquivo.
        /// </summary>
        TipoArquivo tipoArquivo;

        /// <summary>
        /// Código do arquivo.
        /// </summary>
        public int CodigoArquivo { get; set; }

        /// <summary>
        /// Visualizar arquivo (.PLT) no mapa.
        /// </summary>
        bool mapa;

        #endregion

        #region Construtor

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="tipoArquivo">Tipo do arquivo</param>
        /// <param name="mapa">Visualização de arquivo (.PLT) no mapa</param>
        public FrmArquivo(TipoArquivo tipoArquivo, bool mapa = false)
        {
            InitializeComponent();

            this.Icon = Properties.Resources.UploadDonwload.ConvertImageToIcon();

            this.tipoArquivo = tipoArquivo;
            this.mapa = mapa;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Abrir tela de ajuda.
        /// </summary>
        private void AbrirAjuda()
        {
            string caminho = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "Help");

            switch(tipoArquivo)
            {
                case TipoArquivo.TIF:
                    Help.ShowHelp(this, $"{caminho}\\AERMOD.chm", HelpNavigator.TopicId, "70");
                    break;
                case TipoArquivo.FSL:
                    Help.ShowHelp(this, $"{caminho}\\AERMOD.chm", HelpNavigator.TopicId, "80");
                    break;
                case TipoArquivo.SAM:
                    Help.ShowHelp(this, $"{caminho}\\AERMOD.chm", HelpNavigator.TopicId, "90");
                    break;
            }            
        }

        /// <summary>
        /// Carregar GridView com arquivos do tipo informado.
        /// </summary>
        /// <param name="codigoArquivo">Código do arquivo inserido no BD.</param>
        private void CarregarArquivos()
        {
            dgvArquivo.Focus();

            FrmLoading frmLoading = new FrmLoading(this);

            Thread thrCarregar = new Thread(delegate ()
            {
                DataTable dt = new DataTable();

                switch (tipoArquivo)
                {
                    case TipoArquivo.SAM:
                        dt = classeSamson.RetornarArquivos();
                        break;
                    case TipoArquivo.FSL:
                        dt = classeFSL.RetornarArquivos();
                        break;
                    case TipoArquivo.TIF:
                        dt = classeTIF.RetornarArquivos();
                        break;
                    case TipoArquivo.PLT:
                        dt = classeAERMOD.RetornarSaidasUso();
                        break;
                }

                frmLoading.Maximum = dt.Rows.Count;

                CrossThreadOperation.Invoke(this, delegate
                {
                    dgvArquivo.Rows.Clear();
                    int count = 0;

                    foreach (DataRow linha in dt.Rows)
                    {
                        count++;
                        frmLoading.AtualizarStatus(count);

                        int sequencia = tipoArquivo == TipoArquivo.PLT ? Convert.ToInt32(linha["SEQUENCIA"]) : 0;
                        bool emUso = tipoArquivo != TipoArquivo.PLT ? Convert.ToBoolean(linha["EM_USO"]) : false;

                        dgvArquivo.Rows.Add(false, linha["DESCRICAO"], linha["CODIGO"], sequencia);

                        if (emUso)
                        {
                            dgvArquivo.Rows[dgvArquivo.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Blue;
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
        /// Importar arquivo.
        /// </summary>
        private void ImportarArquivo()
        {
            if (tipoArquivo == TipoArquivo.PLT)
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(58), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string extensao = string.Empty;

            switch (tipoArquivo)
            {
                case TipoArquivo.SAM:
                    extensao = ".SAM";
                    break;
                case TipoArquivo.FSL:
                    extensao = ".FSL";
                    break;
                case TipoArquivo.TIF:
                    extensao = ".TIF";
                    break;
            }

            //
            // Gets the file to be uploaded
            //
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Selecionar arquivo";
            ofd.Multiselect = false;
            DialogResult dr = ofd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                if (ofd.FileName == "" || !File.Exists(ofd.FileName))
                {
                    //
                    // If the requested file is not ok...
                    //
                    return;
                }

                #region Salvar arquivo

                string fileExt = Path.GetExtension(ofd.FileName); //get the file extension
                if (fileExt.ToUpper() != extensao.ToUpper())
                {
                    switch (tipoArquivo)
                    {
                        case TipoArquivo.SAM:
                            MessageBox.Show(this, "Selecione .SAM apenas.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case TipoArquivo.FSL:
                            MessageBox.Show(this, "Selecione .FSL apenas.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        case TipoArquivo.TIF:
                            MessageBox.Show(this, "Selecione .TIF apenas.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }

                    return;
                }

                try
                {
                    #region Reading file

                    var arquivo = File.Open(ofd.FileName, FileMode.Open, FileAccess.Read, FileShare.None);
                    var buffer = new byte[arquivo.Length];
                    using (arquivo)
                    {
                        arquivo.Read(buffer, 0, Convert.ToInt32(arquivo.Length));
                        arquivo.Close();
                    }

                    #endregion

                    bool retorno = false;
                    FrmLoading frmLoading = new FrmLoading(this);

                    Thread thr = new Thread(delegate ()
                    {
                        buffer = Funcoes.CompressedGZip(buffer);
                        string nomeArquivo = Path.GetFileName(ofd.FileName);

                        retorno = SalvarArquivo(nomeArquivo, buffer);
                    });
                    thr.Start();

                    frmLoading.thread = thr;
                    frmLoading.PermiteAbortarThread = false;
                    frmLoading.Texto = "Importando arquivo...";
                    frmLoading.Style = ProgressBarStyle.Marquee;
                    frmLoading.Maximum = 0;
                    frmLoading.ShowDialogFade(this);                    

                    if (retorno)
                    {
                        CarregarArquivos();
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(this, e.Message + " - " + e.StackTrace, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                #endregion
            }
        }

        /// <summary>
        /// Baixar arquivo PC.
        /// </summary>
        private void ExportarArquivo()
        {
            if (dgvArquivo.CurrentRow != null)
            {
                if (tipoArquivo == TipoArquivo.PLT && mapa == true)
                {
                    MessageBox.Show(this, classeHelp.BuscarMensagem(58), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                //
                // Downloads the selected file.
                //

                //
                // Finds the unique id of the file.
                //

                int id = Convert.ToInt32(dgvArquivo.CurrentRow.Cells["CODIGO"].Value);
                string descricao = dgvArquivo.CurrentRow.Cells["DESCRICAO"].Value.ToString().Split('.')[0];
                string extensao = $".{dgvArquivo.CurrentRow.Cells["DESCRICAO"].Value.ToString().Split('.')[1]}";

                //
                // the id is stored in text. The structure is: id - FileName.
                //

                byte[] result = null;

                switch (tipoArquivo)
                {
                    case TipoArquivo.SAM:
                        #region SAM

                        result = classeSamson.RetornarArquivo(id);

                        #endregion
                        break;
                    case TipoArquivo.FSL:
                        #region FSL

                        result = classeFSL.RetornarArquivo(id);

                        #endregion
                        break;
                    case TipoArquivo.TIF:
                        #region TIF

                        result = classeTIF.RetornarArquivo(id);

                        #endregion
                        break;
                    case TipoArquivo.PLT:
                        #region PLT

                        int sequencia = Convert.ToInt32(dgvArquivo.CurrentRow.Cells["SEQUENCIA"].Value);
                        result = classeAERMOD.RetornarArquivoPLT(id, sequencia);

                        #endregion
                        break;
                }

                result = Funcoes.DecompressedGZip(result);

                try
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.FileName = descricao;
                    sfd.DefaultExt = extensao;
                    sfd.Filter = $"{descricao} File|{extensao}";
                    //saveFileDialog1.Title = "Save an Image File";
                    DialogResult dr = sfd.ShowDialog();

                    if (dr == DialogResult.OK)
                    {
                        if (string.IsNullOrEmpty(sfd.FileName))
                        {
                            return;
                        }
                        else if (sfd.FileName.Contains(extensao) == false)
                        {
                            sfd.FileName = sfd.FileName + extensao;
                        }

                        using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create))
                        {
                            fs.Write(result, 0, result.Length);
                        }

                        MessageBox.Show(this, "Arquivo baixado com sucesso!", "Sucesso !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch
                {
                    MessageBox.Show(this, "Erro ao tentar baixar o arquivo!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// Salvar documento no BD.
        /// </summary>
        /// <param name="descricao">Descrição do arquivo</param>
        /// <param name="arquivo">Arquivo compactado informado</param>
        private bool SalvarArquivo(string descricao, byte[] arquivo)
        {
            switch (tipoArquivo)
            {
                case TipoArquivo.SAM:
                    {
                        #region SAM

                        if (classeSamson.VerificaDuplicidadeDescricao(descricao) > 0)
                        {
                            CrossThreadOperation.Invoke(this, delegate { MessageBox.Show(this, "Essa descrição já existe na base de dados.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning); });
                        }
                        else if (classeSamson.VerificaDuplicidadeArquivo(arquivo) > 0)
                        {
                            CrossThreadOperation.Invoke(this, delegate { MessageBox.Show(this, "Esse arquivo já existe na base de dados.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning); });
                        }
                        else
                        {
                            bool retorno = classeSamson.SalvarArquivo(arquivo, descricao);

                            if (retorno)
                            {
                                return true;
                            }
                            else
                            {
                                CrossThreadOperation.Invoke(this, delegate { MessageBox.Show(this, "Erro ao tentar inserir no banco.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); });
                            }
                        }

                        #endregion
                    }
                    break;
                case TipoArquivo.FSL:
                    {
                        #region FSL

                        if (classeFSL.VerificaDuplicidadeDescricao(descricao) > 0)
                        {
                            CrossThreadOperation.Invoke(this, delegate { MessageBox.Show(this, "Essa descrição já existe na base de dados.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning); });
                        }
                        else if (classeFSL.VerificaDuplicidadeArquivo(arquivo) > 0)
                        {
                            CrossThreadOperation.Invoke(this, delegate { MessageBox.Show(this, "Esse arquivo já existe na base de dados.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning); });
                        }
                        else
                        {
                            bool retorno = classeFSL.SalvarArquivo(arquivo, descricao);

                            if (retorno)
                            {
                                return true;
                            }
                            else
                            {
                                CrossThreadOperation.Invoke(this, delegate { MessageBox.Show(this, "Erro ao tentar inserir no banco.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); });
                            }
                        }

                        #endregion
                    }
                    break;
                case TipoArquivo.TIF:
                    {
                        #region TIF

                        if (classeTIF.VerificaDuplicidadeDescricao(descricao) > 0)
                        {
                            CrossThreadOperation.Invoke(this, delegate { MessageBox.Show(this, "Essa descrição já existe na base de dados.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning); });
                        }
                        else if (classeTIF.VerificaDuplicidadeArquivo(arquivo) > 0)
                        {
                            CrossThreadOperation.Invoke(this, delegate { MessageBox.Show(this, "Esse arquivo já existe na base de dados.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning); });
                        }
                        else
                        {
                            bool retorno = classeTIF.SalvarArquivo(arquivo, descricao);

                            if (retorno)
                            {
                                return true;
                            }
                            else
                            {
                                CrossThreadOperation.Invoke(this, delegate { MessageBox.Show(this, "Erro ao tentar inserir no banco.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); });
                            }
                        }

                        #endregion
                    }
                    break;
            }

            return false;
        }

        /// <summary>
        /// Atualizar arquivo selecionado.
        /// </summary>
        private void AtualizarArquivo()
        {
            if (dgvArquivo.CurrentRow != null)
            {
                CodigoArquivo = Convert.ToInt32(dgvArquivo.CurrentRow.Cells["CODIGO"].Value);

                switch (tipoArquivo)
                {
                    case TipoArquivo.SAM:
                        classeSamson.AtualizarUsoArquivo(CodigoArquivo);
                        break;
                    case TipoArquivo.FSL:
                        classeFSL.AtualizarUsoArquivo(CodigoArquivo);
                        break;
                    case TipoArquivo.TIF:
                        Boolean marcado = dgvArquivo.Rows.OfType<DataGridViewRow>().Any(f => Convert.ToBoolean(f.Cells[0].Value) == true);

                        if (marcado.Equals(false))
                        {
                            dgvArquivo.CurrentRow.Cells[0].Value = true;
                            dgvArquivo.RefreshEdit();
                        }

                        List<int> lista = new List<int>();

                        foreach (DataGridViewRow row in dgvArquivo.Rows)
                        {
                            if (Convert.ToBoolean(row.Cells[0].Value))
                            {
                                lista.Add(Convert.ToInt32(row.Cells["CODIGO"].Value));
                            }
                        }

                        classeTIF.AtualizarUsoArquivo(lista);
                        break;
                    case TipoArquivo.PLT:
                        if (mapa)
                        {
                            CodigoArquivo = Convert.ToInt32(dgvArquivo.CurrentRow.Cells["SEQUENCIA"].Value);                            
                        }
                        break;
                }

                this.Close();
            }
        }

        /// <summary>
        /// Excluir arquivo informado.
        /// </summary>       
        private void ExcluirArquivo()
        {
            if (dgvArquivo.Rows.Count > 0)
            {
                Boolean marcado = dgvArquivo.Rows.OfType<DataGridViewRow>().Any(f => Convert.ToBoolean(f.Cells[0].Value) == true);

                if (marcado.Equals(false))
                {
                    dgvArquivo.CurrentRow.Cells[0].Value = true;
                    dgvArquivo.RefreshEdit();
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

                            for (Int32 i = dgvArquivo.Rows.Count - 1; i >= 0; i--)
                            {
                                if (Convert.ToBoolean(dgvArquivo.Rows[i].Cells[0].Value) == true)
                                {
                                    count++;
                                    frmLoading.AtualizarStatus(count);

                                    switch (tipoArquivo)
                                    {
                                        case TipoArquivo.SAM:
                                            #region SAM

                                            classeSamson.ExcluirArquivo(Convert.ToInt32(dgvArquivo.Rows[i].Cells["CODIGO"].Value));

                                            #endregion
                                            break;
                                        case TipoArquivo.FSL:
                                            #region FSL

                                            classeFSL.ExcluirArquivo(Convert.ToInt32(dgvArquivo.Rows[i].Cells["CODIGO"].Value));

                                            #endregion
                                            break;
                                        case TipoArquivo.TIF:
                                            #region TIF

                                            classeTIF.ExcluirArquivo(Convert.ToInt32(dgvArquivo.Rows[i].Cells["CODIGO"].Value));

                                            #endregion
                                            break;
                                        case TipoArquivo.PLT:
                                            #region PLT

                                            classeAERMOD.ExcluirArquivoPLT(Convert.ToInt32(dgvArquivo.Rows[i].Cells["CODIGO"].Value), Convert.ToInt32(dgvArquivo.Rows[i].Cells["SEQUENCIA"].Value));

                                            #endregion
                                            break;
                                    }

                                    CrossThreadOperation.Invoke(this, delegate { dgvArquivo.Rows.Remove(dgvArquivo.Rows[i]); });
                                }
                            }
                        }
                        catch { }
                    });
                    thrExcluir.Start();

                    frmLoading.thread = thrExcluir;
                    frmLoading.Texto = "Excluindo registro(s)...";
                    frmLoading.Style = ProgressBarStyle.Blocks;
                    frmLoading.Maximum = dgvArquivo.Rows.OfType<DataGridViewRow>().Count(f => Convert.ToBoolean(f.Cells[0].Value));
                    frmLoading.PermiteAbortarThread = false;
                    frmLoading.ShowDialogFade(this);

                    if (dgvArquivo.Rows.Count == 0)
                    {
                        btnImportarArquivo.Focus();
                    }
                }
                else if (marcado == false)
                {
                    dgvArquivo.CurrentRow.Cells[0].Value = false;
                    dgvArquivo.RefreshEdit();
                }
            }
        }

        #endregion

        #region Eventos FrmArquivo

        protected override void OnShown(EventArgs e)
        {
            CarregarArquivos();

            if (tipoArquivo == TipoArquivo.PLT)
            {
                btnImportarArquivo.Enabled = false;
            }

            if (tipoArquivo == TipoArquivo.PLT && mapa == true)
            {
                btnExportarArquivo.Enabled = false;
            }

            base.OnShown(e);
        }

        private void FrmArquivo_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.F1:
                    e.SuppressKeyPress = true;
                    AbrirAjuda();
                    break;
                case Keys.F5:
                    e.SuppressKeyPress = true;
                    ImportarArquivo();
                    break;
                case Keys.F6:
                    e.SuppressKeyPress = true;
                    ExportarArquivo();
                    break;
                case Keys.Delete:
                    e.SuppressKeyPress = true;
                    ExcluirArquivo();
                    break;
            }
        }

        #endregion

        #region Eventos dgvArquivo

        private void dgvArquivo_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvArquivo.CurrentCell != null && dgvArquivo.CurrentCell.ColumnIndex == 0) dgvArquivo.ReadOnly = true;
        }

        private void dgvArquivo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            AtualizarArquivo();
        }

        private void dgvArquivo_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Delete:
                    e.SuppressKeyPress = true;
                    ExcluirArquivo();
                    break;
                case Keys.Enter:
                    e.SuppressKeyPress = true;
                    AtualizarArquivo();
                    break;
            }
        }        

        #endregion

        #region Eventos btnImportarArquivo

        private void btnImportarArquivo_Click(object sender, EventArgs e)
        {
            ImportarArquivo();
        }

        #endregion

        #region Eventos btnExportarArquivo

        private void btnExportarArquivo_Click(object sender, EventArgs e)
        {
            ExportarArquivo();
        }

        #endregion
    }
}
