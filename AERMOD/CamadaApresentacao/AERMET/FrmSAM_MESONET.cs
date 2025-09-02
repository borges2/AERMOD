using AERMOD.LIB.Desenvolvimento;
using AERMOD.LIB.Forms;
using CamadaLogicaNegocios;
using IronXL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace AERMOD.CamadaApresentacao.AERMET
{
    public partial class FrmSAM_MESONET : Form
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

        public FrmSAM_MESONET()
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

                                dataGridView.Columns[0].Width = dataGridView.Width - 20;
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

            if (Convert.ToInt32(tbxCodigo.Text) == 0)
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(92), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxCodigo.Focus();
                return;
            }

            if (string.IsNullOrEmpty(tbxCidade.Text))
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(93), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxCidade.Focus();
                return;
            }

            if (Convert.ToDecimal(tbxLatitude.Text) == 0)
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(94), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxCidade.Focus();
                return;
            }

            if (Convert.ToDecimal(tbxLongitude.Text) == 0)
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(95), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxCidade.Focus();
                return;
            }

            if (Convert.ToInt32(tbxElevacao.Text) == 0)
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(96), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tbxCodigo.Focus();
                return;
            }

            if (dataGridView.Rows.Count == 0)
            {
                MessageBox.Show(this, classeHelp.BuscarMensagem(97), classeHelp.BuscarMensagem(2), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            FusoHorario horaLocal = (FusoHorario)Convert.ToInt32(cbxFusoHorario.SelectedValue);

            FrmLoading frmLoading = new FrmLoading(this);

            Thread thr = new Thread(delegate ()
            {
                try
                {
                    string nomeEstacao = tbxCidade.Text.TrimStart().TrimEnd();
                    string codigoEstacao = tbxCodigo.Text;
                    decimal latitude = Convert.ToDecimal(tbxLatitude.Text);
                    decimal longitude = Convert.ToDecimal(tbxLongitude.Text);
                    decimal altitude = Convert.ToDecimal(tbxElevacao.Text);
                    string fusoHorario = horaLocal == FusoHorario.UTC_BRASILIA ? "-3" : "0";

                    string msgErro = "Valor não encontrado para o campo.";
                    string msgTitulo = "Atenção";

                    #region Cabeçalho

                    //~00820 MAL. CANDIDO RONDON    PR   0  S24 53  W054 01   392

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

                    CrossThreadOperation.Invoke(this, delegate { Arquivo.EscreverSAM($"~{codigoEstacao} {nomeEstacao} {cbxUF.Text} {fusoHorario}  S{inteiroLatitude}.{decimalLatitude}  W{inteiroLongitude}.{decimalLongitude}   {inteiroAltitude}", 0, true); });

                    #endregion

                    #region Título colunas

                    CrossThreadOperation.Invoke(this, delegate { Arquivo.EscreverSAM("~YR MO DA HR I    1    2       3       4       5  6  7     8     9  10   11  12    13     14     15        16   17     18   19  20      21"); });

                    #endregion

                    #region Valores

                    int linhaInicial = 1;
                    int linhaFinal = dataGridView.Rows.Count - 2;

                    Dictionary<string, int> dicColuna6 = new Dictionary<string, int>
                    {
                        { "CLR", 0 },
                        { "FEW", 1 },
                        { "SCT", 3 },
                        { "BKN", 6 },
                        { "OVC", 8 },
                        { "NSC", 99 },
                        { "VV", 99 },
                        {"VVaaa", 99 },
                        { "///", 99 }
                    };

                    DateTime dataHoraAnterior = DateTime.MinValue;

                    for (int i = linhaInicial; i <= linhaFinal; i++)
                    {
                        frmLoading.AtualizarStatus(i);                        

                        string[] valores = dataGridView.Rows[i].Cells[0].Value.ToString().Split(',');

                        DateTime dataHora = Convert.ToDateTime(valores[1]);

                        if (dataHora.Year == dataHoraAnterior.Year &&
                            dataHora.Month == dataHoraAnterior.Month &&
                            dataHora.Day == dataHoraAnterior.Day &&
                            dataHora.Hour == dataHoraAnterior.Hour)
                        {
                            continue;
                        }                        

                        dataHora = new DateTime(dataHora.Year, dataHora.Month, dataHora.Day, dataHora.Hour, 0, 0);

                        #region YR - Ano                        

                        string YR = "00";

                        try
                        {
                            YR = dataHora.Year.ToString().Substring(2, 2);
                        }
                        catch
                        {
                            CrossThreadOperation.Invoke(this, delegate { MessageBox.Show(this, $"Data Medição - {msgErro}", msgTitulo, MessageBoxButtons.OK, MessageBoxIcon.Warning); });
                            return;
                        }

                        #endregion

                        #region MO - Mês

                        string MO = dataHora.Month.ToString().PadLeft(2, '0');

                        #endregion

                        #region DA - Dia

                        string DA = dataHora.Day.ToString().PadLeft(2, '0');

                        #endregion

                        #region HR - Hora

                        string HR = dataHora.Hour.ToString().PadLeft(2, '0');

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

                        string tres_1 = "9999";
                        string tres_2 = "?0";
                        string tres_3 = "9999";

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
                            string valorColuna12 = valores[12].Replace(".", ",").TrimStart().TrimEnd();
                            string valorColuna13 = valores[13].Replace(".", ",").TrimStart().TrimEnd();
                            string valorColuna14 = valores[14].Replace(".", ",").TrimStart().TrimEnd();

                            decimal valor12 = string.IsNullOrEmpty(valorColuna12) == false && valorColuna12 != "null" ? dicColuna6[valorColuna12] : 0;
                            decimal valor13 = string.IsNullOrEmpty(valorColuna13) == false && valorColuna13 != "null" ? dicColuna6[valorColuna13] : 0;
                            decimal valor14 = string.IsNullOrEmpty(valorColuna14) == false && valorColuna14 != "null" ? dicColuna6[valorColuna14] : 0;

                            decimal valor = new List<decimal>() { valor12, valor13, valor14 }.Max();

                            if (valor > 0)
                            {
                                seis = valor.ToString().PadLeft(2, '0');
                            }

                            //string valorColuna = valores[4].ToString().Replace(".", ",");

                            //if (double.TryParse(valorColuna, out _))
                            //{
                            //    double UR = Convert.ToDouble(valorColuna);
                            //    UR = Math.Round(UR);
                            //    double peso = 0.7;
                            //    double RES = 0;

                            //    //double primeiraParte = UR / 100;
                            //    //primeiraParte = 1 - primeiraParte;
                            //    //double segundaParte = 1 - peso;
                            //    //double valor = primeiraParte / segundaParte;
                            //    //valor = Math.Sqrt(valor);
                            //    //var CC = 1 - valor;

                            //    var CC = 1 - (Math.Sqrt((1 - (UR / 100)) / (1 - peso)));

                            //    if (UR == 99)
                            //    {
                            //        RES = 10;
                            //    }
                            //    else if (CC < 0)
                            //    {
                            //        RES = 0;
                            //    }
                            //    else
                            //    {
                            //        RES = CC * 10;
                            //    }

                            //    seis = Math.Round(RES).ToString().PadLeft(2, '0');
                            //}                            
                        }
                        catch
                        {
                            CrossThreadOperation.Invoke(this, delegate { MessageBox.Show(this, $"Cobertura total de núvens - {msgErro}", msgTitulo, MessageBoxButtons.OK, MessageBoxIcon.Warning); });
                            return;
                        }

                        #endregion

                        #region 7 - Cobertura de núvens opacas

                        string sete = "99";

                        try
                        {
                            string valorColuna12 = valores[12].Replace(".", ",").TrimStart().TrimEnd();
                            string valorColuna13 = valores[13].Replace(".", ",").TrimStart().TrimEnd();

                            decimal valor12 = string.IsNullOrEmpty(valorColuna12) == false && valorColuna12 != "null" ? dicColuna6[valorColuna12] : 0;
                            decimal valor13 = string.IsNullOrEmpty(valorColuna13) == false && valorColuna13 != "null" ? dicColuna6[valorColuna13] : 0;

                            decimal valor = new List<decimal>() { valor12, valor13 }.Max();

                            if (valor > 0)
                            {
                                sete = valor.ToString().PadLeft(2, '0');
                            }
                            else
                            {
                                sete = seis;
                            }
                        }
                        catch
                        {
                            CrossThreadOperation.Invoke(this, delegate { MessageBox.Show(this, $"Cobertura de nuvens opacas - {msgErro}", msgTitulo, MessageBoxButtons.OK, MessageBoxIcon.Warning); });
                            return;
                        }

                        #endregion

                        #region 8 - Temperatura de bulbo seco

                        //C=(F-32)/1,8

                        string oito = "9999.";

                        try
                        {
                            string valorColuna = valores[2].Replace(".", ",");

                            if (string.IsNullOrEmpty(valorColuna) == false && valorColuna != "null")
                            {
                                double valor = Convert.ToDouble(valorColuna);
                                double celsius = (valor - 32) / 1.8;

                                valorColuna = Math.Round(celsius).ToString();

                                if (valorColuna.StartsWith("-") == false)
                                {
                                    oito = valorColuna.PadLeft(5, '0').Replace(',', '.');

                                    if (oito.Contains(".") == false)
                                    {
                                        int retorno = Convert.ToInt32(oito);
                                        oito = retorno.ToString().PadLeft(3, '0');
                                        oito += ".0";
                                    }
                                }
                                else
                                {
                                    oito = valorColuna.Replace("-", "");
                                    oito = oito.PadLeft(4, '0').Replace(',', '.');

                                    if (oito.Contains(".") == false)
                                    {
                                        int retorno = Convert.ToInt32(oito);
                                        oito = retorno.ToString().PadLeft(2, '0');
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
                        string teste = string.Empty;
                        try
                        {
                            string valorColuna = valores[3].Replace(".", ",");
                            teste = valorColuna;
                            if (string.IsNullOrEmpty(valorColuna) == false && valorColuna != "null")
                            {
                                double valor = Convert.ToDouble(valorColuna);
                                double celsius = (valor - 32) / 1.8;

                                valorColuna = Math.Round(celsius).ToString();

                                if (valorColuna.StartsWith("-") == false)
                                {
                                    nove = valorColuna.PadLeft(5, '0').Replace(',', '.');

                                    if (nove.Contains(".") == false)
                                    {
                                        int retorno = Convert.ToInt32(nove);
                                        nove = retorno.ToString().PadLeft(3, '0');
                                        nove += ".0";
                                    }
                                }
                                else
                                {
                                    nove = valorColuna.Replace("-", "");
                                    nove = nove.PadLeft(4, '0').Replace(',', '.');

                                    if (nove.Contains(".") == false)
                                    {
                                        int retorno = Convert.ToInt32(nove);
                                        nove = retorno.ToString().PadLeft(2, '0');
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

                        #region Função humidade relativa Leonardo

                        //double temp = Convert.ToDouble(oito.Replace(".", ","));
                        //double dpTemp = Convert.ToDouble(nove.Replace(".", ","));

                        //double RH = 100 * (Math.Exp((17.625 * dpTemp) / (243.04 + dpTemp)) / Math.Exp((17.625 * temp) / (243.04 + temp)));

                        #endregion

                        #endregion

                        #region 10 - Umidade relativa

                        string dez = "999";

                        try
                        {
                            string valorColuna = valores[4].Replace(".", ",");

                            if (string.IsNullOrEmpty(valorColuna) == false && valorColuna != "null")
                            {
                                valorColuna = Math.Round(Convert.ToDecimal(valorColuna)).ToString();

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

                        //metar["p"] = (101325 * Math.Pow(1 - (2.25577 / 100000) * dataFix["alti"], 5.25588)) / 100;

                        string onze = "9999";

                        try
                        {
                            string valorColuna = valores[8].Replace(".", ",");

                            if (string.IsNullOrEmpty(valorColuna) == false && valorColuna != "null")
                            {
                                double valor = valorColuna.ValidarValor<double>(0);

                                if (valor > 0)
                                {
                                    valor = (101325 * Math.Pow(1 - (2.25577 / 100000) * valor, 5.25588)) / 100;
                                    onze = Math.Round(valor).ToString().PadLeft(4, '0');
                                }
                            }

                            //if (string.IsNullOrEmpty(valorColuna) == false && valorColuna != "null")
                            //{
                            //    double valor = valorColuna.ValidarValor<double>(0);

                            //    if (valor > 0)
                            //    {
                            //        onze = Math.Round(valor).ToString().PadLeft(4, '0');
                            //    }
                            //}
                            //else
                            //{
                            //    onze = "1010";
                            //}
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
                            string valorColuna = valores[5].Replace(".", ",");

                            if (string.IsNullOrEmpty(valorColuna) == false && valorColuna != "null")
                            {
                                valorColuna = Math.Round(Convert.ToDecimal(valorColuna)).ToString();
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

                        //1 nó = 0.5144 metros / segundo

                        string treze = "99.0";

                        try
                        {
                            string valorColuna = valores[6].Replace(".", ",");

                            if (string.IsNullOrEmpty(valorColuna) == false && valorColuna != "null")
                            {
                                double valor = Convert.ToDouble(valorColuna) * 0.5144;
                                valorColuna = Math.Round(valor, 1).ToString();

                                //if (valor.ToString().Contains(","))
                                //{
                                //    string valorDecimal = valor.ToString().Split(',')[1];

                                //    if (valorDecimal.StartsWith("0") == false)
                                //    {
                                //        valorColuna = Math.Round(valor, 1).ToString();
                                //    }
                                //    else
                                //    {
                                //        valorColuna = Math.Round(valor, 2).ToString();
                                //        valorColuna = $"{valorColuna.Split(',')[0]},{valorColuna.Split(',')[1].TrimStart('0')}";
                                //    }                                    
                                //}
                                //else
                                //{
                                //    valorColuna = valor.ToString();
                                //}

                                treze = valorColuna.PadLeft(4, '0').Replace(',', '.');

                                if (treze.Contains(".") == false)
                                {
                                    int retorno = Convert.ToInt32(treze);
                                    treze = retorno.ToString().PadLeft(2, '0');
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

                        //1 milhas = 1,609344 quilometros
                        //0.0 - 160.9 km

                        string quatorze = "99999.";

                        try
                        {
                            string valorColuna = valores[10].Replace(".", ",");

                            if (string.IsNullOrEmpty(valorColuna) == false && valorColuna != "null")
                            {
                                double valor = Convert.ToDouble(valorColuna) * 1.609344;
                                valorColuna = Math.Round(valor, 1).ToString();

                                quatorze = valorColuna.PadLeft(6, '0').Replace(',', '.');

                                if (quatorze.Contains(".") == false)
                                {
                                    int retorno = Convert.ToInt32(quatorze);
                                    quatorze = retorno.ToString().PadLeft(4, '0');
                                    quatorze += ".0";
                                }
                            }
                        }
                        catch
                        {
                            CrossThreadOperation.Invoke(this, delegate { MessageBox.Show(this, $"Visibilidade - {msgErro}", msgTitulo, MessageBoxButtons.OK, MessageBoxIcon.Warning); });
                            return;
                        }

                        #endregion

                        #region 15 - Altura do teto (CLT)

                        //1 pé = 0,3048 metros

                        string quinze = "999999";

                        try
                        {
                            string valorColuna16 = valores[16].Replace(".", ",");
                            string valorColuna17 = valores[17].Replace(".", ",");
                            string valorColuna18 = valores[18].Replace(".", ",");

                            decimal valor16 = string.IsNullOrEmpty(valorColuna16) == false && valorColuna16 != "null" ? Convert.ToDecimal(valorColuna16) : 0;
                            decimal valor17 = string.IsNullOrEmpty(valorColuna17) == false && valorColuna17 != "null" ? Convert.ToDecimal(valorColuna17) : 0;
                            decimal valor18 = string.IsNullOrEmpty(valorColuna18) == false && valorColuna18 != "null" ? Convert.ToDecimal(valorColuna18) : 0;

                            decimal valor = new List<decimal>() { valor16, valor17, valor18 }.Max();

                            if (valor > 0)
                            {
                                valor = valor * 0.3048M;
                                valor = Math.Round(valor);

                                quinze = valor.ToString().PadLeft(6, '0');
                            }
                        }
                        catch
                        {
                            CrossThreadOperation.Invoke(this, delegate { MessageBox.Show(this, $"Altura da base da nuvem - {msgErro}", msgTitulo, MessageBoxButtons.OK, MessageBoxIcon.Warning); });
                            return;
                        }

                        #endregion

                        #region 16 - Tempo atual

                        string dezesseis = "999999999";

                        #endregion

                        #region 17 - Água precipitável (mm)

                        //1 polegada = 25,4 milimetros

                        string dezessete = "9999";

                        try
                        {
                            string valorColuna = valores[7].Replace(".", ",");

                            if (double.TryParse(valorColuna, out _))
                            {
                                if (Convert.ToDouble(valorColuna) > 0)
                                {
                                    double valor = Convert.ToDouble(valorColuna) * 25.4;
                                    valorColuna = Math.Round(valor, 1).ToString();

                                    dezessete = valor.ToString().PadLeft(4, '0').Replace(',', '.');

                                    if (dezessete.Contains('.') == false)
                                    {
                                        int valorInt = Convert.ToInt32(dezessete);
                                        dezessete = valorInt.ToString().PadLeft(2, '0');
                                        dezessete += ".0";
                                    }
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

                        string vinte_e_um = "0".PadLeft(6, ' ');

                        try
                        {
                            string valorColuna = valores[7].Replace(".", ","); ;

                            if (decimal.TryParse(valorColuna, out _))
                            {
                                if (Convert.ToDecimal(valorColuna) > 0)
                                {
                                    vinte_e_um = valorColuna.PadLeft(6, ' ');
                                }
                            }
                        }
                        catch
                        {
                            CrossThreadOperation.Invoke(this, delegate { MessageBox.Show(this, $"Qtd de precipitação horária - {msgErro}", msgTitulo, MessageBoxButtons.OK, MessageBoxIcon.Warning); });
                            return;
                        }

                        #endregion

                        CrossThreadOperation.Invoke(this, delegate { Arquivo.EscreverSAM($" {YR} {MO} {DA} {HR} {I} {um} {dois} {tres_1} {tres_2} {tres_3} {quatro_1} {quatro_2} {cinco} {seis} {sete} {oito} {nove} {dez} {onze} {doze}  {treze} {quatorze} {quinze} {dezesseis} {dezessete} {dezoito} {dezenove} {vinte} {vinte_e_um}"); });

                        #region Escrever linha faltante

                        int proximaLinha = i + 1;

                        INICIO_1:

                        if (proximaLinha <= linhaFinal)
                        {
                            string[] proximoValores = dataGridView.Rows[proximaLinha].Cells[0].Value.ToString().Split(',');

                            DateTime dataHoraProximaLinha = Convert.ToDateTime(proximoValores[1]);

                            if (dataHora.Year == dataHoraProximaLinha.Year &&
                                dataHora.Month == dataHoraProximaLinha.Month &&
                                dataHora.Day == dataHoraProximaLinha.Day &&
                                dataHora.Hour == dataHoraProximaLinha.Hour)
                            {
                                proximaLinha++;
                                goto INICIO_1;
                            }

                            DateTime dataHoraProxima = dataHora.AddHours(1);

                        INICIO_2:

                            if (dataHoraProxima.Year != dataHoraProximaLinha.Year ||
                                dataHoraProxima.Month != dataHoraProximaLinha.Month ||
                                dataHoraProxima.Day != dataHoraProximaLinha.Day ||
                                dataHoraProxima.Hour != dataHoraProximaLinha.Hour)
                            {
                                EscreverLinhaDefault(dataHoraProxima);

                                dataHoraProxima = dataHoraProxima.AddHours(1);

                                goto INICIO_2;
                            }
                        }

                        #endregion

                        dataHoraAnterior = dataHora;
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
            frmLoading.Maximum = dataGridView.Rows.Count - 1;
            frmLoading.Texto = "Convertendo registro(s)...";
            frmLoading.ShowDialogFade(this);

            if (confirmado)
            {
                MessageBox.Show(this, "Arquivo gravado com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Escrever linha com valores faltantes.
        /// </summary>        
        /// <param name="dataHora">Data/Hora</param>
        private void EscreverLinhaDefault(DateTime dataHora)
        {
            #region YR - Ano            

            string YR = "00";

            try
            {
                YR = dataHora.Year.ToString().Substring(2, 2);
            }
            catch
            {
                string msgErro = "Valor não encontrado para o campo.";
                string msgTitulo = "Atenção";

                CrossThreadOperation.Invoke(this, delegate { MessageBox.Show(this, $"Data Medição - {msgErro}", msgTitulo, MessageBoxButtons.OK, MessageBoxIcon.Warning); });
                return;
            }

            #endregion

            #region MO - Mês

            string MO = dataHora.Month.ToString().PadLeft(2, '0');

            #endregion

            #region DA - Dia

            string DA = dataHora.Day.ToString().PadLeft(2, '0');

            #endregion

            #region HR - Hora

            string HR = dataHora.Hour.ToString().PadLeft(2, '0');

            #endregion

            string I = "0";
            string um = "9999";
            string dois = "9999";
            string tres_1 = "9999";
            string tres_2 = "?0";
            string tres_3 = "9999";
            string quatro_1 = "?0";
            string quatro_2 = "9999";
            string cinco = "?0";
            string seis = "99";
            string sete = "99";
            string oito = "9999.";
            string nove = "9999.";
            string dez = "999";
            string onze = "9999";
            string doze = "999";
            string treze = "99.0";
            string quatorze = "99999.";
            string quinze = "999999";
            string dezesseis = "999999999";
            string dezessete = "9999";
            string dezoito = "99999.";
            string dezenove = "9999";
            string vinte = "999";
            string vinte_e_um = "     0";

            CrossThreadOperation.Invoke(this, delegate { Arquivo.EscreverSAM($" {YR} {MO} {DA} {HR} {I} {um} {dois} {tres_1} {tres_2} {tres_3} {quatro_1} {quatro_2} {cinco} {seis} {sete} {oito} {nove} {dez} {onze} {doze}  {treze} {quatorze} {quinze} {dezesseis} {dezessete} {dezoito} {dezenove} {vinte} {vinte_e_um}"); });
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

        private void FrmSAM_MESONET_Load(object sender, EventArgs e)
        {
            CarregarEstado();
            CarregarFusoHorario();

            tbxCodigo.Text = "66666";
            tbxCidade.Text = "CASCAVEL";
            cbxUF.SelectedValue = 41;
            cbxFusoHorario.SelectedValue = (int)FusoHorario.UTC_BRASILIA;
            tbxLatitude.Text = "24,96";
            tbxLongitude.Text = "53,46";
            tbxElevacao.Text = "786";

            var arquivoBD = classeSamson.RetornarArquivoUso();

            if (arquivoBD != null)
            {
                arquivoSAM = Funcoes.DecompressedGZip(arquivoBD.Item1);
            }
        }

        private void FrmSAM_MESONET_KeyDown(object sender, KeyEventArgs e)
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

        #region Eventos btnConsulta

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            ConsultarArquivos();
        }

        #endregion
    }
}
