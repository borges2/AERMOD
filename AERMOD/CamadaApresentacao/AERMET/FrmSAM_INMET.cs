using AERMOD.CamadaApresentacao;
using AERMOD.LIB;
using AERMOD.LIB.Desenvolvimento;
using AERMOD.LIB.Forms;
using CamadaAcessoDados;
using CamadaLogicaNegocios;
using IronXL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace AERMOD
{
    public partial class FrmSAM_INMET : Form
    {
        #region Instâncias e Propriedades

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
        /// Arquivo informado.
        /// </summary>
        byte[] arquivoSAM = null;

        #endregion

        #region Construtor

        /// <summary>
        /// Método construtor.
        /// </summary>
        /// <param name="codigoUsuario">Código do usuário</param>
        public FrmSAM_INMET()
        {
            InitializeComponent();

            this.Icon = Properties.Resources.document_edit.ConvertImageToIcon();            
        }

        #endregion

        #region Métodos

        /// <summary>
        /// this method will read the excel file and copy its data into a datatable
        /// </summary>
        /// <param name="fileName">name of the file</param>
        /// <returns>DataTable</returns>
        private DataTable ReadExcel(string fileName)
        {
            WorkBook workbook = WorkBook.Load(fileName);
            //// Work with a single WorkSheet.
            ////you can pass static sheet name like Sheet1 to get that sheet
            ////WorkSheet sheet = workbook.GetWorkSheet("Sheet1");
            //You can also use workbook.DefaultWorkSheet to get default in case you want to get first sheet only
            WorkSheet sheet = workbook.DefaultWorkSheet;
            //Convert the worksheet to System.Data.DataTable
            //Boolean parameter sets the first row as column names of your table.
            return sheet.ToDataTable(true);
        }

        /// <summary>
        /// Importar arquivo Excel.
        /// </summary>
        private void ImportarArquivo()
        {
            OpenFileDialog file = new OpenFileDialog(); //open dialog to choose file
            file.Title = "Abrir arquivo";
            file.Multiselect = false;

            if (file.ShowDialog() == DialogResult.OK) //if there is a file chosen by the user
            {
                FrmLoading frmLoading = new FrmLoading(this);

                Thread thr = new Thread(delegate ()
                {
                    string fileExt = Path.GetExtension(file.FileName); //get the file extension
                    if (fileExt.CompareTo(".csv") == 0 || fileExt.CompareTo(".xls") == 0 || fileExt.CompareTo(".xlsx") == 0)
                    {
                        try
                        {
                            DataTable dtExcel = ReadExcel(file.FileName); //read excel file

                            CrossThreadOperation.Invoke(this, delegate
                            {
                                dataGridView.Visible = true;
                                dataGridView.DataSource = dtExcel;

                                dataGridView.Columns[0].Frozen = true;
                                dataGridView.Columns[1].Frozen = true;

                                if (dataGridView.Rows.Count > 0)
                                {
                                    numericUltimaLinha.Value = dataGridView.Rows.Count - 1;
                                }
                            });
                        }
                        catch (Exception ex)
                        {
                            CrossThreadOperation.Invoke(this, delegate { MessageBox.Show(this, ex.Message.ToString(), "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning); });
                        }
                    }
                    else
                    {
                        CrossThreadOperation.Invoke(this, delegate { MessageBox.Show(this, "Selecione .csv ou .xls ou .xlsx apenas.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error); });
                    }
                });
                thr.Start();

                frmLoading.thread = thr;
                frmLoading.PermiteAbortarThread = false;
                frmLoading.Style = ProgressBarStyle.Marquee;
                frmLoading.Maximum = 0;
                frmLoading.Texto = "Carregando registro(s)...";
                frmLoading.ShowDialogFade(this);                
            }
        }

        /// <summary>
        /// Exportar arquivo no formato (.SAM).
        /// </summary>
        private void ExportarArquivo()
        {
            if (arquivoSAM == null)
            {
                MessageBox.Show(this, "Arquivo não gravado no banco.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string descricao = "SAMSON";
            string extencao = ".SAM";

            byte[] result = arquivoSAM;           

            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.FileName = descricao;
                sfd.DefaultExt = extencao;
                sfd.Filter = "SAMSON File|.SAM";

                DialogResult dr = sfd.ShowDialog(this);

                if (dr == DialogResult.OK)
                {
                    if (string.IsNullOrEmpty(sfd.FileName))
                    {
                        return;
                    }
                    else if (sfd.FileName.Contains(extencao) == false)
                    {
                        sfd.FileName = sfd.FileName + extencao;
                    }

                    using (FileStream fs = new FileStream(sfd.FileName, FileMode.Create))
                    {
                        fs.Write(result, 0, result.Length);
                    }

                    MessageBox.Show(this, "Arquivo baixado com sucesso!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);                    
                }
            }
            catch
            {
                MessageBox.Show(this, "Erro ao tentar baixar o arquivo!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Converter arquivo para (.SAM).
        /// </summary>
        private void ConverterArquivo()
        {
            bool confirmado = false;

            if (dataGridView.Rows.Count == 0)
            {
                MessageBox.Show(this, "Não existem dados para conversão.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }            

            FusoHorario horaLocal = (FusoHorario)Convert.ToInt32(cbxFusoHorario.SelectedValue);

            FrmLoading frmLoading = new FrmLoading(this);

            Thread thr = new Thread(delegate ()
            {
                try
                {
                    string nomeEstacao = dataGridView.Columns[0].HeaderText.Split(':')[1].TrimStart(' ');
                    string codigoEstacao = dataGridView.Rows[0].Cells[0].Value.ToString().Split(':')[1].TrimStart(' ');
                    decimal latitude = Convert.ToDecimal(dataGridView.Rows[1].Cells[0].Value.ToString().Split(':')[1].TrimStart(' ').Replace('.', ','));
                    decimal longitude = Convert.ToDecimal(dataGridView.Rows[2].Cells[0].Value.ToString().Split(':')[1].TrimStart(' ').Replace('.', ','));
                    decimal altitude = Convert.ToDecimal(dataGridView.Rows[3].Cells[0].Value.ToString().Split(':')[1].TrimStart(' ').Replace('.', ','));
                    string situacao = dataGridView.Rows[4].Cells[0].Value.ToString().Split(':')[1].TrimStart(' ');
                    DateTime dataInicial = Convert.ToDateTime(dataGridView.Rows[5].Cells[0].Value.ToString().Split(':')[1].TrimStart(' '));
                    DateTime dataFinal = Convert.ToDateTime(dataGridView.Rows[6].Cells[0].Value.ToString().Split(':')[1].TrimStart(' '));
                    string periodicidade = dataGridView.Rows[7].Cells[0].Value.ToString().Split(':')[1].TrimStart(' ');
                    string fusoHorario = "0";

                    string msgErro = "Valor não encontrado para o campo.";
                    string msgTitulo = "Atenção";

                    #region Cabeçalho

                    string texto = codigoEstacao;

                    for (int i = 0; i < texto.Trim().Length; i++)
                    {
                        if (!char.IsNumber(texto.Trim()[i]))
                        {
                            texto = texto.Trim().Remove(i, 1);
                        }
                    }

                    texto = texto.PadLeft(5, '0');
                    codigoEstacao = texto;
                    nomeEstacao = nomeEstacao.Length >= 21 ? nomeEstacao.Substring(0, 22).PadRight(22, ' ') : nomeEstacao.PadRight(22, ' ');
                    fusoHorario = fusoHorario.PadLeft(3, ' ');

                    if (latitude < 0)
                    {
                        latitude = latitude * (-1);
                    }

                    string inteiroLatitude = latitude.ToString().Split(',')[0].ToString();
                    string decimalLatitude = latitude.ToString().Split(',')[1].Substring(0, 2);

                    if (longitude < 0)
                    {
                        longitude = longitude * (-1);
                    }

                    string inteiroLongitude = longitude.ToString().Split(',')[0].ToString();
                    string decimalLongitude = longitude.ToString().Split(',')[1].Substring(0, 2);
                    altitude = Math.Round(altitude, MidpointRounding.AwayFromZero);
                    string inteiroAltitude = altitude.ToString().Split(',')[0].PadLeft(4, ' ');

                    CrossThreadOperation.Invoke(this, delegate { Arquivo.EscreverSAM($"~{codigoEstacao} {nomeEstacao} {cbxUF.Text} {fusoHorario}  S{inteiroLatitude} {decimalLatitude}  W0{inteiroLongitude} {decimalLongitude}  {inteiroAltitude}", 0, true); });

                    #endregion

                    #region Título colunas

                    CrossThreadOperation.Invoke(this, delegate { Arquivo.EscreverSAM("~YR MO DA HR I    1    2       3       4       5  6  7     8     9  10   11  12    13     14     15        16   17     18   19  20      21"); });

                    #endregion

                    #region Valores

                    int linhaInicial = Convert.ToInt32(numericPrimeiraLinha.Value);
                    int linhaFinal = Convert.ToInt32(numericUltimaLinha.Value);

                    for (int i = linhaInicial; i <= linhaFinal; i++)
                    {
                        #region 3 horas INMET representa 0 horas local

                        //Avança 3 linhas e desconta 3 horas nas horas
                        if (horaLocal == FusoHorario.UTC_BRASILIA && i == linhaInicial)
                        {
                            i += 3;
                        }

                        #endregion

                        frmLoading.AtualizarStatus(i);

                        #region YR - Ano

                        string YR = "00";

                        DateTime dataMedicao = DateTime.MinValue;

                        try
                        {
                            int horas = dataGridView.Rows[i].Cells[1].Value.ValidarValor<int>(0);
                            horas = horas / 100;

                            dataMedicao = Convert.ToDateTime($"{dataGridView.Rows[i].Cells[0].Value} {horas}:00:00");
                            dataMedicao = dataMedicao.AddHours(-3);

                            YR = dataMedicao.Year.ToString().Substring(2, 2);
                        }
                        catch
                        {
                            CrossThreadOperation.Invoke(this, delegate { MessageBox.Show(this, $"Data Medição - {msgErro}", msgTitulo, MessageBoxButtons.OK, MessageBoxIcon.Warning); });
                            return;
                        }

                        #endregion

                        #region MO - Mês

                        string MO = dataMedicao.Month.ToString().PadLeft(2, '0');

                        #endregion

                        #region DA - Dia

                        string DA = dataMedicao.Day.ToString().PadLeft(2, '0');

                        #endregion

                        #region HR - Hora

                        string HR = dataMedicao.Hour.ToString().PadLeft(2, '0');

                        #endregion

                        #region I - Indicador de observação

                        string I = "0";

                        #endregion

                        #region 1 - Radiação horizontal extraterrestre

                        string um = "9999";

                        #endregion

                        #region 2 - Radiação normal direta extraterrestre

                        string dois = "9999";

                        #endregion

                        #region 3 - Radiação horizontal global (solar)

                        string tres_1 = "0000";
                        string tres_2 = "?0";
                        string tres_3 = "9999";

                        try
                        {
                            decimal divisor = 3.6M;
                            decimal valor = dataGridView.Rows[i].Cells[7].Value.ValidarValor<decimal>(0);

                            if (valor < 0)
                            {
                                tres_1 = "0000";
                            }
                            else
                            {
                                valor = valor / divisor;
                                //valor = Math.Round(valor, 2, MidpointRounding.ToEven);
                                valor = Math.Round(valor);
                                tres_1 = valor.ToString().PadLeft(4, '0').Replace(',', '.');
                            }
                        }
                        catch
                        {
                            CrossThreadOperation.Invoke(this, delegate { MessageBox.Show(this, $"Radiação global - {msgErro}", msgTitulo, MessageBoxButtons.OK, MessageBoxIcon.Warning); });
                            return;
                        }

                        #endregion

                        #region 4 - Radiação normal direta

                        string quatro_1 = "?0";
                        string quatro_2 = "9999";

                        #endregion

                        #region 5 - Radiação horizontal difusa

                        string cinco = "?0";

                        #endregion

                        #region 6 - Cobertura total de núvens

                        //CC =1-(RAIZ((1-(UR/100))/(1-0.7)))
                        //=SE((UR=99);10;SE((CC<0);0;CC*10))

                        string seis = "99";

                        try
                        {
                            double UR = dataGridView.Rows[i].Cells[18].Value.ValidarValor<double>(0);
                            double peso = 0.7;
                            double RES = 0;

                            //double primeiraParte = UR / 100;
                            //primeiraParte = 1 - primeiraParte;
                            //double segundaParte = 1 - peso;
                            //double valor = primeiraParte / segundaParte;
                            //valor = Math.Sqrt(valor);
                            //var CC = 1 - valor;

                            var CC = 1 - (Math.Sqrt((1 - (UR / 100)) / (1 - peso)));

                            if (UR == 99)
                            {
                                RES = 10;
                            }
                            else if (CC < 0)
                            {
                                RES = 0;
                            }
                            else
                            {
                                RES = CC * 10;
                            }

                            seis = Math.Round(RES).ToString().PadLeft(2, '0');
                        }
                        catch
                        {
                            CrossThreadOperation.Invoke(this, delegate { MessageBox.Show(this, $"Cobertura total de núvens - {msgErro}", msgTitulo, MessageBoxButtons.OK, MessageBoxIcon.Warning); });
                            return;
                        }

                        #endregion

                        #region 7 - Cobertura de núvens opacas

                        string sete = seis;

                        #endregion

                        #region 8 - Temperatura de bulbo seco

                        string oito = "9999.";

                        try
                        {
                            string valorColuna = dataGridView.Rows[i].Cells[9].Value.ToString();

                            if (string.IsNullOrEmpty(valorColuna) == false && valorColuna != "null")
                            {
                                if (valorColuna.StartsWith("-") == false)
                                {
                                    oito = valorColuna.PadLeft(5, '0').Replace(',', '.');

                                    if (oito.Contains(".") == false)
                                    {
                                        int valor = Convert.ToInt32(oito);
                                        oito = valor.ToString().PadLeft(3, '0');
                                        oito += ".0";
                                    }
                                }
                                else
                                {
                                    oito = valorColuna.Replace("-", "");
                                    oito = oito.PadLeft(4, '0').Replace(',', '.');

                                    if (oito.Contains(".") == false)
                                    {
                                        int valor = Convert.ToInt32(oito);
                                        oito = valor.ToString().PadLeft(2, '0');
                                        oito = $"{oito}.0";
                                    }

                                    oito = $"-{oito}";
                                }
                            }
                        }
                        catch
                        {
                            CrossThreadOperation.Invoke(this, delegate { MessageBox.Show(this, $"Temperatura de bulbo seco - {msgErro}", msgTitulo, MessageBoxButtons.OK, MessageBoxIcon.Warning); });
                            return;
                        }

                        #endregion

                        #region 9 - Temperatura do ponto de orvalho

                        string nove = "9999.";

                        try
                        {                            
                            string valorColuna = dataGridView.Rows[i].Cells[10].Value.ToString();

                            if (string.IsNullOrEmpty(valorColuna) == false && valorColuna != "null")
                            {
                                if (valorColuna.StartsWith("-") == false)
                                {
                                    nove = valorColuna.PadLeft(5, '0').Replace(',', '.');

                                    if (nove.Contains(".") == false)
                                    {
                                        int valor = Convert.ToInt32(nove);
                                        nove = valor.ToString().PadLeft(3, '0');
                                        nove += ".0";
                                    }
                                }
                                else
                                {
                                    nove = valorColuna.Replace("-", "");
                                    nove = nove.PadLeft(4, '0').Replace(',', '.');

                                    if (nove.Contains(".") == false)
                                    {
                                        int valor = Convert.ToInt32(nove);
                                        nove = valor.ToString().PadLeft(2, '0');
                                        nove = $"{nove}.0";
                                    }

                                    nove = $"-{nove}";
                                }
                            }
                        }
                        catch
                        {
                            CrossThreadOperation.Invoke(this, delegate { MessageBox.Show(this, $"Temperatura do ponto de orvalho - {msgErro}", msgTitulo, MessageBoxButtons.OK, MessageBoxIcon.Warning); });
                            return;
                        }

                        #endregion

                        #region 10 - Umidade relativa

                        string dez = "999";

                        try
                        {
                            string valorColuna = dataGridView.Rows[i].Cells[18].Value.ToString();

                            if (string.IsNullOrEmpty(valorColuna) == false && valorColuna != "null")
                            {
                                dez = valorColuna.PadLeft(3, '0');
                            }
                        }
                        catch
                        {
                            CrossThreadOperation.Invoke(this, delegate { MessageBox.Show(this, $"Umidade relativa - {msgErro}", msgTitulo, MessageBoxButtons.OK, MessageBoxIcon.Warning); });
                            return;
                        }

                        #endregion

                        #region 11 - Pressão da estação

                        string onze = "9999";

                        try
                        {
                            double valor = dataGridView.Rows[i].Cells[3].Value.ValidarValor<double>(0);

                            if (valor > 0)
                            {
                                onze = Math.Round(valor).ToString().PadLeft(4, '0');
                            }
                        }
                        catch
                        {
                            CrossThreadOperation.Invoke(this, delegate { MessageBox.Show(this, $"Pressão da estação - {msgErro}", msgTitulo, MessageBoxButtons.OK, MessageBoxIcon.Warning); });
                            return;
                        }

                        #endregion

                        #region 12 - Direção do vento

                        string doze = "999";

                        try
                        {
                            string valorColuna = dataGridView.Rows[i].Cells[19].Value.ToString();

                            if (string.IsNullOrEmpty(valorColuna) == false && valorColuna != "null")
                            {
                                doze = valorColuna.PadLeft(3, '0');
                            }
                        }
                        catch
                        {
                            CrossThreadOperation.Invoke(this, delegate { MessageBox.Show(this, $"Direção do vento - {msgErro}", msgTitulo, MessageBoxButtons.OK, MessageBoxIcon.Warning); });
                            return;
                        }

                        #endregion

                        #region 13 - Velocidade do vento

                        string treze = "9999.";

                        try
                        {
                            string valorColuna = dataGridView.Rows[i].Cells[21].Value.ToString();

                            if (string.IsNullOrEmpty(valorColuna) == false && valorColuna != "null")
                            {
                                treze = valorColuna.PadLeft(5, '0').Replace(',', '.');

                                if (treze.Contains(".") == false)
                                {
                                    int valor = Convert.ToInt32(treze);
                                    treze = valor.ToString().PadLeft(3, '0');
                                    treze += ".0";
                                }
                            }
                        }
                        catch
                        {
                            CrossThreadOperation.Invoke(this, delegate { MessageBox.Show(this, $"Velocidade do vento - {msgErro}", msgTitulo, MessageBoxButtons.OK, MessageBoxIcon.Warning); });
                            return;
                        }

                        #endregion

                        #region 14 - Visibilidade

                        string quatorze = "99999.";

                        #endregion

                        #region 15 - Altura do teto (CLT)

                        string quinze = "999999";

                        #endregion

                        #region 16 - Tempo atual

                        string dezesseis = "999999999";

                        #endregion

                        #region 17 - Água precipitável (mm)

                        string dezessete = "9999";

                        try
                        {
                            double valor = dataGridView.Rows[i].Cells[2].Value.ValidarValor<double>(0);
                            if (valor > 0)
                            {
                                dezessete = valor.ToString().PadLeft(4, '0').Replace(',', '.');

                                if (dezessete.Contains('.') == false)
                                {
                                    int valorInt = Convert.ToInt32(dezessete);
                                    dezessete = valorInt.ToString().PadLeft(2, '0');
                                    dezessete += ".0";
                                }
                            }
                        }
                        catch
                        {
                            CrossThreadOperation.Invoke(this, delegate { MessageBox.Show(this, $"Água precipitável (mm) - {msgErro}", msgTitulo, MessageBoxButtons.OK, MessageBoxIcon.Warning); });
                            return;
                        }

                        #endregion

                        #region 18 - Profundidade óptica de aerossol de banda larga	0.0-0.900

                        string dezoito = "99999.";

                        #endregion

                        #region 19 - Profundidade da neve

                        string dezenove = "9999";

                        #endregion

                        #region 20 - Nº de dias desde última queda de neve

                        string vinte = "999";

                        #endregion

                        #region 21 - Qtd de precipitação horária (124-129) (Polegadas e Centésimos)

                        //Utilizar a função (Decimal.Round(value)))
                        //1mm = 0,0393701 polegadas

                        string vinte_e_um = "0";

                        try
                        {
                            decimal valor = dataGridView.Rows[i].Cells[2].Value.ValidarValor<decimal>(0);
                            if (valor > 0)
                            {
                                decimal total = valor * 0.0393701M;
                                total = decimal.Round(total, 2);
                                total = total * 100;
                                int totalInt = Convert.ToInt32(total);

                                vinte_e_um = totalInt.ToString().PadLeft(6, ' ');
                            }
                            else
                            {
                                vinte_e_um = valor.ToString().PadLeft(6, ' ');
                            }
                        }
                        catch
                        {
                            CrossThreadOperation.Invoke(this, delegate { MessageBox.Show(this, $"Qtd de precipitação horária - {msgErro}", msgTitulo, MessageBoxButtons.OK, MessageBoxIcon.Warning); });
                            return;
                        }

                        #endregion

                        CrossThreadOperation.Invoke(this, delegate { Arquivo.EscreverSAM($" {YR} {MO} {DA} {HR} {I} {um} {dois} {tres_1} {tres_2} {tres_3} {quatro_1} {quatro_2} {cinco} {seis} {sete} {oito} {nove} {dez} {onze} {doze} {treze} {quatorze} {quinze} {dezesseis} {dezessete} {dezoito} {dezenove} {vinte} {vinte_e_um}"); });
                    }

                    string nomeArquivo = string.Empty;

                    CrossThreadOperation.Invoke(this, delegate
                    {
                        FrmNomeArquivo frmNome = new FrmNomeArquivo();
                        frmNome.ShowDialogFade(this);

                        if (string.IsNullOrEmpty(frmNome.NomeArquivo) == false)
                        {
                            nomeArquivo = $"{frmNome.NomeArquivo}.SAM";
                        }
                    });

                    if (string.IsNullOrEmpty(nomeArquivo))
                    {
                        return;
                    }

                    String diretorio = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "Arquivos");
                    FileStream arquivo = File.Open(Path.Combine(diretorio, "SAMSON.SAM"), FileMode.Open, FileAccess.Read, FileShare.None);

                    var buffer = new byte[arquivo.Length];
                    using (arquivo)
                    {
                        arquivo.Read(buffer, 0, Convert.ToInt32(arquivo.Length));
                        arquivo.Close();
                    }

                    byte[] arquivoCompactado = Funcoes.CompressedGZip(buffer);
                    FileInfo fi = new FileInfo(Path.Combine(diretorio, "SAMSON.SAM"));

                    int codigoArquivo = classeSamson.VerificaDuplicidadeDescricao(nomeArquivo);
                    if (codigoArquivo > 0)
                    {
                        classeSamson.ExcluirArquivo(codigoArquivo);
                    }
                    else
                    {
                        codigoArquivo = classeSamson.VerificaDuplicidadeArquivo(arquivoCompactado);
                        if (codigoArquivo > 0)
                        {
                            classeSamson.ExcluirArquivo(codigoArquivo);
                        }
                    }

                    if (classeSamson.SalvarArquivo(arquivoCompactado, nomeArquivo) == false)
                    {
                        CrossThreadOperation.Invoke(this, delegate { MessageBox.Show(this, "Erro ao tentar gravar arquivo no banco.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error); });
                        return;
                    }

                    arquivoSAM = buffer;

                    String[] arquivosTemporarios = Directory.GetFiles(diretorio);

                    foreach (var arquivoAtual in arquivosTemporarios)
                    {
                        if (arquivoAtual.EndsWith(".SAM"))
                        {
                            File.Delete(arquivoAtual);
                        }
                    }

                    #endregion

                    confirmado = true;
                }
                catch
                {
                    CrossThreadOperation.Invoke(this, delegate { MessageBox.Show(this, $"Arquivo incompatível para conversão.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning); });
                }
            });
            thr.Start();

            frmLoading.thread = thr;
            frmLoading.PermiteAbortarThread = false;
            frmLoading.Style = ProgressBarStyle.Blocks;
            frmLoading.Maximum = Convert.ToInt32(numericUltimaLinha.Value);
            frmLoading.Texto = "Convertendo registro(s)...";
            frmLoading.ShowDialogFade(this);

            if (confirmado)
            {
                MessageBox.Show(this, "Arquivo gravado com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Abrir tela de ajuda.
        /// </summary>
        private void AbrirAjuda()
        {
            string caminho = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "Help");
            Help.ShowHelp(this, $"{caminho}\\AERMOD.chm", HelpNavigator.TopicId, "10");
            //Help.ShowHelp(this, $"{caminho}\\AERMOD.chm", HelpNavigator.TableOfContents, "10");
            //Help.ShowHelp(this, $"{caminho}\\AERMOD.chm", HelpNavigator.Index, "10");
        }

        /// <summary>
        /// Carregar fuso horário.
        /// </summary>
        private void CarregarFusoHorario()
        {
            DataTable dtDados = Enumeradores.RetornarFusoHorario();            

            cbxFusoHorario.DisplayMember = "DESCRICAO";
            cbxFusoHorario.ValueMember = "CODIGO";
            cbxFusoHorario.DataSource = dtDados;

            cbxFusoHorario.SelectedValue = (int)FusoHorario.UTC_BRASILIA;
        }

        /// <summary>
        /// Carregar todos os estados.
        /// </summary>
        private void CarregarEstado()
        {
            DataTable dtEstado = new DataTable();
            dtEstado.Columns.Add("CODIGO", typeof(int));
            dtEstado.Columns.Add("NOME", typeof(string));

            dtEstado.Rows.Add(11, "RO");
            dtEstado.Rows.Add(12, "AC");
            dtEstado.Rows.Add(13, "AM");
            dtEstado.Rows.Add(14, "RR");
            dtEstado.Rows.Add(15, "PA");
            dtEstado.Rows.Add(16, "AP");
            dtEstado.Rows.Add(17, "TO");
            dtEstado.Rows.Add(21, "MA");
            dtEstado.Rows.Add(22, "PI");
            dtEstado.Rows.Add(23, "CE");
            dtEstado.Rows.Add(24, "RN");
            dtEstado.Rows.Add(25, "PB");
            dtEstado.Rows.Add(26, "PE");
            dtEstado.Rows.Add(27, "AL");
            dtEstado.Rows.Add(28, "SE");
            dtEstado.Rows.Add(29, "BA");
            dtEstado.Rows.Add(31, "MG");
            dtEstado.Rows.Add(32, "ES");
            dtEstado.Rows.Add(33, "RJ");
            dtEstado.Rows.Add(35, "SP");
            dtEstado.Rows.Add(41, "PR");
            dtEstado.Rows.Add(42, "SC");
            dtEstado.Rows.Add(43, "RS");
            dtEstado.Rows.Add(50, "MS");
            dtEstado.Rows.Add(51, "MT");
            dtEstado.Rows.Add(52, "GO");
            dtEstado.Rows.Add(53, "DF");

            cbxUF.DisplayMember = "NOME";
            cbxUF.ValueMember = "CODIGO";
            cbxUF.DataSource = dtEstado;
            cbxUF.SelectedValue = 41;
        }        

        /// <summary>
        /// Consultar arquivos.
        /// </summary>
        private void ConsultarArquivos()
        {
            FrmArquivo frmArquivo = new FrmArquivo(TipoArquivo.SAM);
            frmArquivo.ShowDialogFade(this);

            if (frmArquivo.CodigoArquivo > 0)
            {
                var arquivoBD = classeSamson.RetornarArquivo(frmArquivo.CodigoArquivo);

                if (arquivoBD != null)
                {
                    arquivoSAM = Funcoes.DecompressedGZip(arquivoBD);
                }
            }
        }

        #endregion

        #region Eventos FrmSAM

        private void FrmSAM_Load(object sender, EventArgs e)
        {            
            CarregarEstado();
            CarregarFusoHorario();

            var arquivoBD = classeSamson.RetornarArquivoUso();

            if (arquivoBD != null)
            {
                arquivoSAM = Funcoes.DecompressedGZip(arquivoBD.Item1);                
            }
        }

        private void FrmSAM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.I:
                        e.SuppressKeyPress = true;
                        ImportarArquivo();
                        break;
                    case Keys.E:
                        e.SuppressKeyPress = true;
                        ExportarArquivo();
                        break;
                }
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        this.Close();
                        break;
                    case Keys.Enter:
                        e.SuppressKeyPress = true;
                        ConverterArquivo();
                        break;
                    case Keys.F1:
                        e.SuppressKeyPress = true;
                        AbrirAjuda();
                        break;
                    case Keys.F2:
                        e.SuppressKeyPress = true;
                        ConsultarArquivos();
                        break;
                }
            }
        }

        #endregion

        #region Eventos statusStrip

        private void btnAjuda_Click(object sender, EventArgs e)
        {
            AbrirAjuda();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            ImportarArquivo();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            ExportarArquivo();
        }

        private void btnConverter_Click(object sender, EventArgs e)
        {
            ConverterArquivo();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region Eventos numericPrimeiraLinha

        private void numericPrimeiraLinha_Enter(object sender, EventArgs e)
        {
            NumericUpDown numeric = (NumericUpDown)sender;
            numeric.Select(0, numeric.Value.ToString().Length);
        }

        private void numericPrimeiraLinha_Leave(object sender, EventArgs e)
        {
            decimal valor = numericPrimeiraLinha.Value;
            numericPrimeiraLinha.Value = numericPrimeiraLinha.Maximum;
            numericPrimeiraLinha.Value = valor;
        }

        #endregion

        #region Eventos numericUltimaLinha

        private void numericUltimaLinha_Leave(object sender, EventArgs e)
        {
            decimal valor = numericUltimaLinha.Value;
            numericUltimaLinha.Value = numericUltimaLinha.Maximum;
            numericUltimaLinha.Value = valor;
        }

        #endregion

        #region Eventos btnDefinirPrimeiraLinha

        private void btnDefinirPrimeiraLinha_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow != null)
            {
                int valor = dataGridView.CurrentRow.Index;
                if (valor > 0)
                {
                    numericPrimeiraLinha.Value = valor;
                }
                else
                {
                    MessageBox.Show(this, "Linha inválida", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        #endregion

        #region Eventos btnDefinirUltimaLinha

        private void btnDefinirUltimaLinha_Click(object sender, EventArgs e)
        {
            if (dataGridView.CurrentRow != null)
            {
                int valor = dataGridView.CurrentRow.Index;
                if (valor > 0)
                {
                    numericUltimaLinha.Value = valor;
                }
                else
                {
                    MessageBox.Show(this, "Linha inválida", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        #endregion

        #region Eventos btnArquivos

        private void btnArquivos_Click(object sender, EventArgs e)
        {
            ConsultarArquivos();
        }

        #endregion
    }
}
