#region Using

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Globalization;

#endregion

namespace Netsof.LIB.Componentes.Calendario
{
    [ToolboxBitmapAttribute(typeof(MonthCalendar))]
    public partial class MonthCalendarLIB : UserControl
    {
        #region Variáveis

        CultureInfo culture = new CultureInfo("pt-BR");
        GregorianCalendar gCalendario = new GregorianCalendar();
        Form form = null;
        Form frmPai; //usado para receber instancia do form principal do calendario...
        NumericUpDown numericUpDown = null;
        DataTable DtMarcacao = new DataTable();
        DataTable DtFeriado = new DataTable();
        DataTable DtFeriadoFixo = new DataTable();
        public bool frmMesAberto = false; //usado para controlar qdo frmMes esta aberto/fechado...
        public bool Focado = false; //usado para controlar qdo controla está focado ou não

        Panel panel1;
        Panel panel2;
        Panel panel3;
        Panel panel4;
        Panel panel5;
        Panel panel6;

        Panel panel7;
        Panel panel8;
        Panel panel9;
        Panel panel10;
        Panel panel11;
        Panel panel12;

        #endregion

        #region Construtor

        public MonthCalendarLIB()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, false);
            this.UpdateStyles();

            DtMarcacao.Columns.Add("DATA", typeof(DateTime));           

            DtFeriado.Columns.Add("DATA", typeof(DateTime));
            DtFeriado.Columns.Add("COR", typeof(Color));

            DtFeriadoFixo.Columns.Add("DATA", typeof(String));
        }

        #endregion

        #region Propriedades

        [Category("My Options")]
        [Description("A data de hoje.")]
        public DateTime DataAtual
        {
            get { return Convert.ToDateTime(lbData.Text); }
        }

        [Category("My Options")]
        [Description("Dia uteis(Sem feriados)")]
        public Int32 DiasUteis
        {
            get { return Convert.ToInt32(lbDiaUteis.Text); }
        }

        [Category("My Options")]
        [Description("Data que esta selecionada")]
        public DateTime DataSelecionada
        {              
            get {  return Convert.ToDateTime(DiaSelecionado() + "/" + DateTime.ParseExact(lbMes.Text, "MMMM", new CultureInfo("pt-BR")).Month.ToString() + "/" + lbAno.Text);}
        }
                
        [DefaultValue(false)]
        [Category("My Options")]
        [Description("Ativa e Desativa a grade")]
        public Boolean Grade
        {
            get { return _Valor; }
            set
            {                
                _Valor = value;
                if (_Valor == true)
                { 
                    panel7 = new Panel();
                    panel7.BackColor = Color.Black;
                    panel7.Size = new Size(panelLinha.Size.Width , 1);
                    panel7.Location = new Point(panelLinha.Location.X, lb8.Location.Y - 2);
                    this.Controls.Add(panel7);
                    panel7.BringToFront();

                    panel8 = new Panel();
                    panel8.BackColor = Color.Black;
                    panel8.Size = new Size(panelLinha.Size.Width, 1);
                    panel8.Location = new Point(panelLinha.Location.X, lb17.Location.Y - 3);
                    this.Controls.Add(panel8);
                    panel8.BringToFront();

                    panel9 = new Panel();
                    panel9.BackColor = Color.Black;
                    panel9.Size = new Size(panelLinha.Size.Width, 1);
                    panel9.Location = new Point(panelLinha.Location.X, lb25.Location.Y - 2);
                    this.Controls.Add(panel9);
                    panel9.BringToFront();

                    panel10 = new Panel();
                    panel10.BackColor = Color.Black;
                    panel10.Size = new Size(panelLinha.Size.Width, 1);
                    panel10.Location = new Point(panelLinha.Location.X, lb29.Location.Y - 2);
                    this.Controls.Add(panel10);
                    panel10.BringToFront();

                    panel11 = new Panel();
                    panel11.BackColor = Color.Black;
                    panel11.Size = new Size(panelLinha.Size.Width, 1);
                    panel11.Location = new Point(panelLinha.Location.X, lb36.Location.Y - 2);
                    this.Controls.Add(panel11);
                    panel11.BringToFront();

                    panel12 = new Panel();
                    panel12.BackColor = Color.Black;
                    panel12.Size = new Size(panelLinha.Size.Width, 1);
                    panel12.Location = new Point(panelLinha.Location.X, lb36.Location.Y + 15);
                    this.Controls.Add(panel12);
                    panel12.BringToFront();

                    panel1 = new Panel();
                    panel1.BackColor = Color.Black;
                    panel1.Size = new Size(1, panel12.Location.Y - 43);
                    panel1.Location = new Point(lb2.Location.X - 1, panelLinha.Location.Y);
                    this.Controls.Add(panel1);
                    panel1.BringToFront();

                    panel2 = new Panel();
                    panel2.BackColor = Color.Black;
                    panel2.Size = new Size(1, panel12.Location.Y - 43);
                    panel2.Location = new Point(lb3.Location.X - 1, panelLinha.Location.Y);
                    this.Controls.Add(panel2);
                    panel2.BringToFront();

                    panel3 = new Panel();
                    panel3.BackColor = Color.Black;
                    panel3.Size = new Size(1, panel12.Location.Y - 43);
                    panel3.Location = new Point(lb4.Location.X - 1, panelLinha.Location.Y);
                    this.Controls.Add(panel3);
                    panel3.BringToFront();

                    panel4 = new Panel();
                    panel4.BackColor = Color.Black;
                    panel4.Size = new Size(1, panel12.Location.Y - 43);
                    panel4.Location = new Point(lb5.Location.X - 1, panelLinha.Location.Y);
                    this.Controls.Add(panel4);
                    panel4.BringToFront();

                    panel5 = new Panel();
                    panel5.BackColor = Color.Black;
                    panel5.Size = new Size(1, panel12.Location.Y - 43);
                    panel5.Location = new Point(lb6.Location.X - 1, panelLinha.Location.Y);
                    this.Controls.Add(panel5);
                    panel5.BringToFront();

                    panel6 = new Panel();
                    panel6.BackColor = Color.Black;
                    panel6.Size = new Size(1, panel12.Location.Y - 43);
                    panel6.Location = new Point(lb7.Location.X - 1, panelLinha.Location.Y);
                    this.Controls.Add(panel6);
                    panel6.BringToFront();
                }
                else
                {
                    this.Controls.Remove(panel1);
                    this.Controls.Remove(panel2);
                    this.Controls.Remove(panel3);
                    this.Controls.Remove(panel4);
                    this.Controls.Remove(panel5);
                    this.Controls.Remove(panel6);
                    this.Controls.Remove(panel7);
                    this.Controls.Remove(panel8);
                    this.Controls.Remove(panel9);
                    this.Controls.Remove(panel10);
                    this.Controls.Remove(panel11);
                    this.Controls.Remove(panel12);
                }

            }
        }

        protected Boolean _Valor = false;
        

        #endregion                

        [Category("My Options")]
        [Description("Seleciona uma data especifica.")]
        public void SelecionarData(DateTime data)
        {           
            lbMes.Text = MesExtenso(data.Month);
            lbAno.Text = data.Year.ToString();
            DiaSemana(Convert.ToDateTime("01/" + data.ToString("MM/yyyy")));
            foreach (Control labels in Controls)
            {
                if (labels.GetType() == typeof(Label))
                {
                    Label lbselecao = ((Label)(labels));
                    if (lbselecao.Text == data.Day.ToString())
                    {
                        selecionaData(lbselecao);
                        break;
                    }
                }
            }
        }

        [Category("My Options")]
        [Description("Adiciona marcação em determinada data.")]
        public void AddMarcacao(DateTime data)
        {
            DataRow dataRow = DtMarcacao.NewRow();
            dataRow["DATA"] = data;           
            DtMarcacao.Rows.Add(dataRow);
            //selecionaData(MarcaData());
            foreach (Control labels in Controls)
            {
                if (labels.GetType() == typeof(Label))
                {
                    Label lbselecao = ((Label)(labels));
                    if (lbselecao.BackColor == SystemColors.MenuHighlight)
                    {
                        selecionaData(lbselecao);
                        break;
                    }
                }
            }
        }

        [Category("My Options")]
        [Description("Adiciona feriado em determinada data.")]
        public void AddFeriado(DateTime data)
        {
            DataRow dataRow = DtFeriado.NewRow();
            dataRow["DATA"] = data;
            dataRow["COR"] = Color.Red;
            DtFeriado.Rows.Add(dataRow);
            //selecionaData(MarcaData());
            foreach (Control labels in Controls)
            {
                if (labels.GetType() == typeof(Label))
                {
                    Label lbselecao = ((Label)(labels));
                    if (lbselecao.BackColor == SystemColors.MenuHighlight)
                    {
                        selecionaData(lbselecao);
                        break;
                    }
                }
            }
        }

        [Category("My Options")]
        [Description("Adiciona feriado(Fixo) em determinada data.")]
        public void AddFeriadoFixo(String ddMM)
        {
            ddMM = ddMM.Insert(2, "-");
            DataRow dataRow = DtFeriadoFixo.NewRow();
            dataRow["DATA"] = ddMM;
            DtFeriadoFixo.Rows.Add(dataRow);
            foreach (Control labels in Controls)
            {
                if (labels.GetType() == typeof(Label))
                {
                    Label lbselecao = ((Label)(labels));
                    if (lbselecao.BackColor == SystemColors.MenuHighlight)
                    {
                        selecionaData(lbselecao);
                        break;
                    }
                }
            }
        }

        [Category("My Options")]
        [Description("Remove feriado de uma data.")]
        public void RemoverFeriado(DateTime data)
        {
            if (DtFeriado.Rows.Count > 0)
            {
                for (Int32 i = 0; i < DtFeriado.Rows.Count; i++)
                {
                    if((DateTime)DtFeriado.Rows[i]["DATA"] == data)
                    {
                        DtFeriado.Rows.RemoveAt(i);                        
                        DiaSemana(Convert.ToDateTime("01/" + DateTime.ParseExact(lbMes.Text, "MMMM", new CultureInfo("pt-BR")).Month.ToString() + "/" + lbAno.Text));
                        SelecionaDia(DiaSelecionado());
                        break;
                    }
                }
            }
        }

        [Category("My Options")]
        [Description("Remove o feriado fixo.")]
        public void RemoverFeriadoFixo(String ddMM)
        {
            ddMM = ddMM.Insert(2, "-");
            if (DtFeriadoFixo.Rows.Count > 0)
            {
                for (Int32 i = 0; i < DtFeriadoFixo.Rows.Count; i++)
                {
                    if (DtFeriadoFixo.Rows[i]["DATA"].ToString() == ddMM)
                    {
                        DtFeriadoFixo.Rows.RemoveAt(i);
                        DiaSemana(Convert.ToDateTime("01/" + DateTime.ParseExact(lbMes.Text, "MMMM", new CultureInfo("pt-BR")).Month.ToString() + "/" + lbAno.Text));
                        SelecionaDia(DiaSelecionado());
                        break;
                    }
                }
            }
        }

        [Category("My Options")]
        [Description("Remove  marcação de uma data.")]
        public void RemoverMarcacao(DateTime data)
        {
            if (DtMarcacao.Rows.Count > 0)
            {
                for (Int32 i = 0; i < DtMarcacao.Rows.Count; i++)
                {
                    if ((DateTime)DtFeriado.Rows[i]["DATA"] == data)
                    {
                        DtMarcacao.Rows.RemoveAt(i);
                        DiaSemana(Convert.ToDateTime("01/" + DateTime.ParseExact(lbMes.Text, "MMMM", new CultureInfo("pt-BR")).Month.ToString() + "/" + lbAno.Text));
                        SelecionaDia(DiaSelecionado());
                        break;
                    }
                }
            }
        }

        [Category("My Options")]
        [Description("Remove todas as marcações.")]
        public void RemoverTodasMarcacoes()
        {
            DtMarcacao.Rows.Clear();
            DiaSemana(Convert.ToDateTime("01/" + DateTime.ParseExact(lbMes.Text, "MMMM", new CultureInfo("pt-BR")).Month.ToString() + "/" + lbAno.Text));
            SelecionaDia(DiaSelecionado());
        }

        #region Evento DateChanged

        public delegate void Calendario_DateChanged(object sender, EventArgs e);       

        [Category("Bartender - CustomEvents"), Description("Ocorre quando seleciona uma determinada data.")]
        public event Calendario_DateChanged DateChanged;

        public virtual void RaiseData()
        {            
            DateChanged(this, new EventArgs());
        }

        #endregion

        #region Métodos

        //Seleciona a data
        private void selecionaData(Label label)
        {            
            foreach (Control labels in Controls)
            {
                if (labels.GetType() == typeof(Label))
                {
                    Label lbselecao = ((Label)(labels));
                    if (lbselecao.BackColor == SystemColors.MenuHighlight)
                    {
                        //Retira a seleção                       
                        lbselecao.BackColor = Color.White;
                        if (lbselecao.ForeColor != Color.Red && lbselecao.ForeColor != Color.LightGray)
                        {
                            lbselecao.ForeColor = Color.Black;
                        }
                        break;
                    }                   
                }
            }            

            //Seleciona
            Color cor = Color.Empty;
            if (label != null)
            {
                label.BackColor = SystemColors.MenuHighlight;
                if (label.ForeColor != Color.Red)
                {
                    cor = label.ForeColor;
                    label.ForeColor = Color.White;
                }
                if (numericUpDown == null)
                    lb1.Focus();
            }

            //Parte superior (data em cinza)
            if (cor == Color.LightGray && label.TabIndex < 8)
            {
                btnVoltar.PerformClick();
            }
            //Parte inferior (data em cinza)
            if (cor == Color.LightGray && label.TabIndex > 24)
            {
                btnAvancar.PerformClick();
            }            

            #region Calcula o numero de Dia uteis

            if (DataSelecionada <= DataAtual)
                lbDiaUteis.Text = GetDiasUteisAtras(DataSelecionada, DataAtual).ToString();
            else
                lbDiaUteis.Text = GetDiasUteis(DataAtual, DataSelecionada).ToString();
            #endregion

            #region Marca o feriado no dia

            if (DtFeriado.Rows.Count > 0)
            {
                for (Int32 i = 0; i < DtFeriado.Rows.Count; i++)
                {
                    if (((DateTime)DtFeriado.Rows[i]["DATA"]).ToString("MM/yyyy") == DataSelecionada.ToString("MM/yyyy"))
                    {
                        foreach (Control labels in Controls)
                        {
                            if (labels.GetType() == typeof(Label))
                            {
                                Label lbselecao = ((Label)(labels));
                                if (lbselecao.Text.PadLeft(2, '0') == ((DateTime)DtFeriado.Rows[i]["DATA"]).ToString("dd") && lbselecao.TabIndex != 10000 && lbselecao.ForeColor != Color.LightGray)
                                {
                                    lbselecao.ForeColor = (Color)DtFeriado.Rows[i]["COR"];                                   
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            #endregion

            #region Marca os feriado Fixo

            //DateTime.ParseExact(lbMes.Text, "MMMM", new CultureInfo("pt-BR")).Month.ToString()
            if (DtFeriadoFixo.Rows.Count > 0)
            {
                for (Int32 i = 0; i < DtFeriadoFixo.Rows.Count; i++)
                {
                    if (DtFeriadoFixo.Rows[i]["DATA"].ToString().Split('-')[1] == DataSelecionada.ToString("MM"))
                    {
                        foreach (Control labels in Controls)
                        {
                            if (labels.GetType() == typeof(Label))
                            {
                                Label lbselecao = ((Label)(labels));
                                if (lbselecao.Text.PadLeft(2, '0') == DtFeriadoFixo.Rows[i]["DATA"].ToString().Split('-')[0] && lbselecao.TabIndex != 10000 && lbselecao.ForeColor != Color.LightGray)
                                {
                                    lbselecao.ForeColor = Color.Red;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            #endregion

            #region Define as marcações

            Int32 count = 1;
            List<DateTime> datasList = new List<DateTime>();

            if (DtMarcacao.Rows.Count > 0)
            {
                for (Int32 i = 0; i < DtMarcacao.Rows.Count; i++)
                {
                    if (!datasList.Exists(f => f == Convert.ToDateTime(DtMarcacao.Rows[i]["DATA"])))
                    {
                        count = 1;

                        datasList.Add(Convert.ToDateTime(DtMarcacao.Rows[i]["DATA"]));


                        for (Int32 y = i + 1; y < DtMarcacao.Rows.Count; y++)
                        {
                            if (Convert.ToDateTime(DtMarcacao.Rows[i]["DATA"]) == Convert.ToDateTime(DtMarcacao.Rows[y]["DATA"]))
                            {
                                count += 1;
                            }
                        }

                        if (count > 0 && count < 4)
                        {
                            LabelMarcacao(label, new Bitmap[2] { AERMOD.LIB.Properties.Resources.vinte_cinco, AERMOD.LIB.Properties.Resources.vinte_cinco_selecionado }, Convert.ToDateTime(DtMarcacao.Rows[i]["DATA"]));
                        }
                        else if (count >= 4 && count < 6)
                        {
                            LabelMarcacao(label, new Bitmap[2] { AERMOD.LIB.Properties.Resources.cinquenta, AERMOD.LIB.Properties.Resources.cinquenta_selecionado }, Convert.ToDateTime(DtMarcacao.Rows[i]["DATA"]));
                        }
                        else if (count >= 6 && count < 9)
                        {
                            LabelMarcacao(label, new Bitmap[2] { AERMOD.LIB.Properties.Resources.setenta_cinco, AERMOD.LIB.Properties.Resources.setenta_cinco_selecionado }, Convert.ToDateTime(DtMarcacao.Rows[i]["DATA"]));
                        }
                        else if (count >= 9)
                        {
                            LabelMarcacao(label, new Bitmap[2] { AERMOD.LIB.Properties.Resources.cem, AERMOD.LIB.Properties.Resources.cem_selecionado }, Convert.ToDateTime(DtMarcacao.Rows[i]["DATA"]));
                        }
                    }
                }
            }

            datasList.Clear();
            #endregion

                        
            if(DateChanged != null)
                RaiseData();
        }

        //Retorna a label para a marcação
        private void LabelMarcacao(Label label, Bitmap[] image, DateTime data)
        {
            Label lbselecao = null;

            foreach (Control labels in Controls)
            {
                if (labels.GetType() == typeof(Label))
                {
                    lbselecao = ((Label)(labels));
                    if (lbselecao.Text.PadLeft(2, '0') == data.ToString("dd") && data.ToString("MMyyyy") == DataSelecionada.ToString("MMyyyy") && lbselecao.TabIndex != 10000 && lbselecao.ForeColor != Color.LightGray)
                    {
                        lbselecao.BackgroundImageLayout = ImageLayout.Stretch;
                        if (lbselecao != label)
                            lbselecao.BackgroundImage = image[0];
                        else
                            lbselecao.BackgroundImage = image[1];
                    }
                }
            }
        }

        //Retorna o numero de dias uteis
        public Int32 GetDiasUteis(DateTime initialDate, DateTime finalDate)
        {
            Int32 FeriadoCount = 0;
            Int32 days = 0;
            Int32 daysCount = 0;
            days = initialDate.Subtract(finalDate).Days;

            //Módulo
            if (days < 0)
                days = days * -1;

            for (int i = 1; i <= days; i++)
            {
                initialDate = initialDate.AddDays(1);
                if (DtFeriado.Rows.Count > 0)
                {
                    for (int count = 0; count < DtFeriado.Rows.Count; count++)
                    {
                        if (initialDate == (DateTime)DtFeriado.Rows[count]["DATA"])
                        {
                            FeriadoCount += 1;
                        }
                    }
                    //Conta apenas dias da semana.
                    if (initialDate.DayOfWeek != DayOfWeek.Sunday && initialDate.DayOfWeek != DayOfWeek.Saturday && FeriadoCount == 0)
                        daysCount++;
                    FeriadoCount = 0;
                    
                }
                else
                {
                    //Conta apenas dias da semana.
                    if (initialDate.DayOfWeek != DayOfWeek.Sunday && initialDate.DayOfWeek != DayOfWeek.Saturday)
                        daysCount++;
                }
            }
            return daysCount;
        }

        //Retorna o numero de dias uteis
        public Int32 GetDiasUteisAtras(DateTime initialDate, DateTime finalDate)
        {
            Int32 FeriadoCount = 0;
            Int32 days = 0;
            Int32 daysCount = 0;
            days = initialDate.Subtract(finalDate).Days;

            //Módulo
            if (days < 0)
                days = days * -1;

            for (int i = 1; i <= days; i++)
            {
                finalDate = finalDate.AddDays(-1);
                if (DtFeriado.Rows.Count > 0)
                {
                    for (int count = 0; count < DtFeriado.Rows.Count; count++)
                    {
                        if (finalDate == (DateTime)DtFeriado.Rows[count]["DATA"])
                        {
                            FeriadoCount += 1;
                        }
                    }
                    //Conta apenas dias da semana.
                    if (finalDate.DayOfWeek != DayOfWeek.Sunday && finalDate.DayOfWeek != DayOfWeek.Saturday && FeriadoCount == 0)
                        daysCount++;
                    FeriadoCount = 0;

                }
                else
                {
                    //Conta apenas dias da semana.
                    if (finalDate.DayOfWeek != DayOfWeek.Sunday && finalDate.DayOfWeek != DayOfWeek.Saturday)
                        daysCount++;
                }
            }
            return daysCount;
        }
        
        //Retorna Mês por extenso
        private String MesExtenso(int i)
        {
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;            
            return culture.TextInfo.ToTitleCase(dtfi.GetMonthName(i));
        }

        //Captura o dia selecionado
        private String DiaSelecionado()
        {
            String txt = "";
            foreach (Control labels in Controls)
            {
                if (labels.GetType() == typeof(Label))
                {
                    Label lbselecao = ((Label)(labels));
                    if (lbselecao.BackColor == SystemColors.MenuHighlight)
                    {                        
                        txt = lbselecao.Text;
                        break;
                    }    
                }
            }
            if (txt == "")
            {
                txt = DateTime.Now.ToString("dd");
            }
            return txt;
        }

        //Seleciona data atual
        private void SelecionaDataAtual()
        {
            if (lbData.Text != DateTime.Now.ToString("dd/MM/yyyy"))
            {
                foreach (Control labels in Controls)
                {
                    if (labels.GetType() == typeof(Label))
                    {
                        Label lbselecao = ((Label)(labels));
                        if (lbselecao.BorderStyle == BorderStyle.FixedSingle)
                        {
                            lbselecao.BorderStyle = BorderStyle.None;
                            break;
                        }
                    }
                }
                lbData.Text =  DateTime.Now.ToString("dd/MM/yyyy");               
            }

            lbMes.Text = MesExtenso(Convert.ToInt32(lbData.Text.Split('/')[1]));
            lbAno.Text = lbData.Text.Split('/')[2];
            DiaSemana(Convert.ToDateTime("01/" + DateTime.ParseExact(lbMes.Text, "MMMM", new CultureInfo("pt-BR")).Month.ToString() + "/" + lbAno.Text));
            foreach (Control labels in Controls)
            {
                if (labels.GetType() == typeof(Label))
                {
                    Label lbselecao = ((Label)(labels));
                    if (lbselecao.ForeColor == Color.Black && lbselecao.Text.PadLeft(2, '0') == lbData.Text.Split('/')[0] && lbselecao.ForeColor != SystemColors.MenuHighlight)
                    {                        
                        selecionaData(lbselecao);
                        break;
                    }
                }
            }                      
        }

        //Selecioa o dia
        private void SelecionaDia(String txt)
        {
            Int32 numeroDias = gCalendario.GetDaysInMonth(Convert.ToInt32(lbAno.Text), DateTime.ParseExact(lbMes.Text, "MMMM", new CultureInfo("pt-BR")).Month);
            Int32 dia = Convert.ToInt32(txt);
            while (dia > numeroDias)
            {
                dia -= 1;
                txt = dia.ToString();
            }
            foreach (Control labels in Controls)
            {
                if (labels.GetType() == typeof(Label))
                {
                    Label lbselecao = ((Label)(labels));
                    if (lbselecao.Text == txt && lbselecao.ForeColor != Color.LightGray && lbselecao.TabIndex < 10000)
                    {
                        selecionaData(lbselecao);
                        break;
                    }
                }
            }
        }

        //Marca a data na Troca de Mes e Ano(data de Hoje)
        private void MarcaDataTroca()
        {
            if (DateTime.ParseExact(lbMes.Text, "MMMM", new CultureInfo("pt-BR")).Month == Convert.ToInt32(lbData.Text.Split('/')[1]) && lbAno.Text == lbData.Text.Split('/')[2])
            {
                MarcaData();
            }
            else
            {
                foreach (Control labels in Controls)
                {
                    if (labels.GetType() == typeof(Label))
                    {
                        Label lbselecao = ((Label)(labels));
                        if (lbselecao.BorderStyle == BorderStyle.FixedSingle)
                        {
                            lbselecao.BorderStyle = BorderStyle.None;
                            break;
                        }
                    }
                }
            }
        }

        //Marca a data atual no calendario
        private Label MarcaData()
        {
            Label lbRetorno = null;
            foreach (Control labels in Controls)
            {
                if (labels.GetType() == typeof(Label))
                {
                    Label lbselecao = ((Label)(labels));
                    if (lbselecao.Text == Convert.ToInt32(lbData.Text.Split('/')[0]).ToString() && lbselecao.ForeColor != Color.LightGray)
                    {
                        lbselecao.BorderStyle = BorderStyle.FixedSingle;
                        lbRetorno = lbselecao; 
                        break;
                    }
                }
            }
            return lbRetorno;
        }

        //Remove numericUpDown
        private void RemoveNumeric()
        {
            if (numericUpDown != null)
            {
                lbAno.Text = numericUpDown.Value.ToString();
                lbAno.Controls.Remove(numericUpDown);
                numericUpDown = null;

                //Foca no label
                foreach (Control labels in Controls)
                {
                    if (labels.GetType() == typeof(Label))
                    {
                        Label lbselecao = ((Label)(labels));
                        if (lbselecao.BackColor == SystemColors.MenuHighlight)
                        {
                            lbselecao.Focus();
                            break;
                        }
                    }
                }   
            }
        }

        //Cancela o evento padrao da tecla delete
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (numericUpDown != null && numericUpDown.Focused && keyData == Keys.Delete)
                return true;
            if (numericUpDown == null)
            {
                int index = 0;
                switch (keyData)
                {
                    case Keys.Down:
                        foreach (Control labels in Controls)
                        {
                            if (labels.GetType() == typeof(Label))
                            {
                                Label lbselecao = ((Label)(labels));
                                if (lbselecao.BackColor == SystemColors.MenuHighlight)
                                {
                                    index = lbselecao.TabIndex;
                                    break;
                                }
                            }
                        }
                        foreach (Control labels in Controls)
                        {
                            if (labels.GetType() == typeof(Label))
                            {
                                Label lbselecao = ((Label)(labels));
                                if (lbselecao.TabIndex == index + 7)
                                {
                                    selecionaData(lbselecao);
                                    return true;
                                }
                            }
                        }
                        break;
                    case Keys.Up:
                        foreach (Control labels in Controls)
                        {
                            if (labels.GetType() == typeof(Label))
                            {
                                Label lbselecao = ((Label)(labels));
                                if (lbselecao.BackColor == SystemColors.MenuHighlight)
                                {
                                    index = lbselecao.TabIndex;
                                    break;
                                }
                            }
                        }
                        foreach (Control labels in Controls)
                        {
                            if (labels.GetType() == typeof(Label))
                            {
                                Label lbselecao = ((Label)(labels));
                                if (lbselecao.TabIndex == index - 7)
                                {
                                    selecionaData(lbselecao);
                                    return true;
                                }
                            }
                        }
                        break;
                    case Keys.Left:
                        foreach (Control labels in Controls)
                        {
                            if (labels.GetType() == typeof(Label))
                            {
                                Label lbselecao = ((Label)(labels));
                                if (lbselecao.BackColor == SystemColors.MenuHighlight)
                                {
                                    index = lbselecao.TabIndex;
                                    break;
                                }
                            }
                        }
                        foreach (Control labels in Controls)
                        {
                            if (labels.GetType() == typeof(Label))
                            {
                                Label lbselecao = ((Label)(labels));
                                if (lbselecao.TabIndex == index - 1)
                                {
                                    selecionaData(lbselecao);
                                    return true;
                                }
                            }
                        }
                        break;
                    case Keys.Right:
                        foreach (Control labels in Controls)
                        {
                            if (labels.GetType() == typeof(Label))
                            {
                                Label lbselecao = ((Label)(labels));
                                if (lbselecao.BackColor == SystemColors.MenuHighlight)
                                {
                                    index = lbselecao.TabIndex;
                                    break;
                                }
                            }
                        }
                        foreach (Control labels in Controls)
                        {
                            if (labels.GetType() == typeof(Label))
                            {
                                Label lbselecao = ((Label)(labels));
                                if (lbselecao.TabIndex == index + 1)
                                {
                                    selecionaData(lbselecao);
                                    return true;                                   
                                }
                            }
                        }
                        break;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        //Seta a data atual
        protected override void OnLoad(EventArgs e)
        {
            this.TabStop = true;           
            lbData.Text = DateTime.Now.ToString("dd/MM/yyyy");           
            lbMes.Text = MesExtenso(DateTime.Now.Month);
            lbAno.Text = DateTime.Now.Year.ToString();
            DiaSemana(Convert.ToDateTime("01/" + DateTime.Now.ToString("MM/yyyy")));
            SelecionaDataAtual();
            base.OnLoad(e);
        }

        protected override void OnCreateControl()
        {
            frmPai = this.FindForm();

            this.MinimumSize = new Size(180, 171);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
       
            base.OnCreateControl();
        }

        //Dia da semana
        private void DiaSemana(DateTime date)
        {            
            Int32 numeroDias = gCalendario.GetDaysInMonth(date.Year, date.Month);
            Int32 data = Convert.ToInt32(date.DayOfWeek);
            DateTime dataAnterior = date.AddMonths(-1);
            switch (data)
            {
                case 0:
                    lb1.Text = "1";
                    PreencherCalendario(lb1.TabIndex, numeroDias);
                    break;
                case 1:
                    lb2.Text = "1";
                    PreencherCalendario(lb2.TabIndex, numeroDias);
                    lb1.Text = gCalendario.GetDaysInMonth(dataAnterior.Year, dataAnterior.Month).ToString();
                    lb1.ForeColor = Color.LightGray;
                    break;
                case 2:
                    lb3.Text = "1";
                    PreencherCalendario(lb3.TabIndex, numeroDias);
                    lb2.Text = gCalendario.GetDaysInMonth(dataAnterior.Year, dataAnterior.Month).ToString();
                    lb1.Text = (gCalendario.GetDaysInMonth(dataAnterior.Year, dataAnterior.Month) - 1).ToString();
                    lb1.ForeColor = Color.LightGray;
                    lb2.ForeColor = Color.LightGray;
                    break;
                case 3:
                    lb4.Text = "1";
                    PreencherCalendario(lb4.TabIndex, numeroDias);
                    lb3.Text = gCalendario.GetDaysInMonth(dataAnterior.Year, dataAnterior.Month).ToString();
                    lb2.Text = (gCalendario.GetDaysInMonth(dataAnterior.Year, dataAnterior.Month) - 1).ToString();
                    lb1.Text = (gCalendario.GetDaysInMonth(dataAnterior.Year, dataAnterior.Month) - 2).ToString();
                    lb1.ForeColor = Color.LightGray;
                    lb2.ForeColor = Color.LightGray;
                    lb3.ForeColor = Color.LightGray;
                    break;
                case 4:
                    lb5.Text = "1";
                    PreencherCalendario(lb5.TabIndex, numeroDias);
                    lb4.Text = gCalendario.GetDaysInMonth(dataAnterior.Year, dataAnterior.Month).ToString();
                    lb3.Text = (gCalendario.GetDaysInMonth(dataAnterior.Year, dataAnterior.Month) - 1).ToString();
                    lb2.Text = (gCalendario.GetDaysInMonth(dataAnterior.Year, dataAnterior.Month) - 2).ToString();
                    lb1.Text = (gCalendario.GetDaysInMonth(dataAnterior.Year, dataAnterior.Month) - 3).ToString();
                    lb1.ForeColor = Color.LightGray;
                    lb2.ForeColor = Color.LightGray;
                    lb3.ForeColor = Color.LightGray;
                    lb4.ForeColor = Color.LightGray;
                    break;
                case 5:
                    lb6.Text = "1";
                    PreencherCalendario(lb6.TabIndex, numeroDias);
                    lb5.Text = gCalendario.GetDaysInMonth(dataAnterior.Year, dataAnterior.Month).ToString();
                    lb4.Text = (gCalendario.GetDaysInMonth(dataAnterior.Year, dataAnterior.Month) - 1).ToString();
                    lb3.Text = (gCalendario.GetDaysInMonth(dataAnterior.Year, dataAnterior.Month) - 2).ToString();
                    lb2.Text = (gCalendario.GetDaysInMonth(dataAnterior.Year, dataAnterior.Month) - 3).ToString();
                    lb1.Text = (gCalendario.GetDaysInMonth(dataAnterior.Year, dataAnterior.Month) - 4).ToString();
                    lb1.ForeColor = Color.LightGray;
                    lb2.ForeColor = Color.LightGray;
                    lb3.ForeColor = Color.LightGray;
                    lb4.ForeColor = Color.LightGray;
                    lb5.ForeColor = Color.LightGray;
                    break;
                case 6:
                    lb7.Text = "1";
                    PreencherCalendario(lb7.TabIndex, numeroDias);
                    lb6.Text = gCalendario.GetDaysInMonth(dataAnterior.Year, dataAnterior.Month).ToString();
                    lb5.Text = (gCalendario.GetDaysInMonth(dataAnterior.Year, dataAnterior.Month) - 1).ToString();
                    lb4.Text = (gCalendario.GetDaysInMonth(dataAnterior.Year, dataAnterior.Month) - 2).ToString();
                    lb3.Text = (gCalendario.GetDaysInMonth(dataAnterior.Year, dataAnterior.Month) - 3).ToString();
                    lb2.Text = (gCalendario.GetDaysInMonth(dataAnterior.Year, dataAnterior.Month) - 4).ToString();
                    lb1.Text = (gCalendario.GetDaysInMonth(dataAnterior.Year, dataAnterior.Month) - 5).ToString();
                    lb1.ForeColor = Color.LightGray;
                    lb2.ForeColor = Color.LightGray;
                    lb3.ForeColor = Color.LightGray;
                    lb4.ForeColor = Color.LightGray;
                    lb5.ForeColor = Color.LightGray;
                    lb6.ForeColor = Color.LightGray;
                    break;
            }
            MarcaDataTroca();
        }

        //Preenche o calendario com os dias
        private void PreencherCalendario(Int32 tabIndex,Int32 total)
        {
            Int32 ultimoIndex = 0;
            for (Int32 i = 1; i <= total; i++)
            {
                foreach (Control labels in Controls)
                {
                    if (labels.GetType() == typeof(Label))
                    {
                        Label lbselecao = ((Label)(labels));
                        if (lbselecao.TabIndex == tabIndex)
                        {
                            lbselecao.Text = i.ToString();
                            if (lbselecao.TabIndex != 1 && lbselecao.TabIndex != 8 && lbselecao.TabIndex != 15 && lbselecao.TabIndex != 22 && lbselecao.TabIndex != 29 && lbselecao.TabIndex != 36)
                            {
                                lbselecao.ForeColor = Color.Black;
                            }
                            else
                                lbselecao.ForeColor = Color.Red;                            
                            tabIndex += 1;
                            ultimoIndex = lbselecao.TabIndex + 1;
                            break;
                        }
                    }
                }
            }

            //Preenche os dia do outro mes
            for (Int32 i = 1; i <= 42 - total; i++)
            {
                foreach (Control labels in Controls)
                {
                    if (labels.GetType() == typeof(Label))
                    {
                        Label lbselecao = ((Label)(labels));
                        if (lbselecao.TabIndex == ultimoIndex)
                        {
                            lbselecao.Text = i.ToString();
                            lbselecao.ForeColor = Color.LightGray;
                            ultimoIndex += 1;
                            break;
                        }
                    }
                }
            }

            //Limpa as Marcações
            foreach (Control labels in Controls)
            {
                if (labels.GetType() == typeof(Label))
                {
                    Label lbselecao = ((Label)(labels));
                    if (lbselecao.BackgroundImage != null)
                    {
                        lbselecao.BackgroundImage = null;
                    }
                }
            }
        }

        /// <summary>
        /// Melhora o desempenho na pintura do form.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        #endregion    

        #region Seleciona as datas

        private void lb1_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }

        private void lb2_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }

        private void lb3_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }

        private void lb4_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }

        private void lb5_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }

        private void lb6_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }

        private void lb7_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }

        private void lb8_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }        

        private void lb9_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }

        private void lb10_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }

        private void lb11_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }

        private void lb12_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }

        private void lb13_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }

        private void lb14_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }
        
        private void lb15_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }

        private void lb16_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }

        private void lb17_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }

        private void lb18_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }

        private void lb19_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);            
        }

        private void lb20_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }

        private void lb21_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }

        private void lb22_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }

        private void lb23_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }

        private void lb24_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }

        private void lb25_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }

        private void lb26_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }

        private void lb27_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }

        private void lb28_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }

        private void lb29_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }

        private void lb30_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }

        private void lab31_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }

        private void lb32_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }

        private void lb33_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }

        private void lb34_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }

        private void lb35_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);

        }

        private void lb36_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }

        private void lb37_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }

        private void lb38_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }

        private void lb39_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }

        private void lb40_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }

        private void lb41_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }

        private void lb42_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            selecionaData((Label)sender);
        }

        #endregion

        #region Eventos

        private void lbMes_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            form = new Form();
            form.FormBorderStyle = FormBorderStyle.None;
            form.Size = new Size(125,246);
            form.BackColor = Color.White;
            form.StartPosition = FormStartPosition.Manual;
            form.ShowInTaskbar = false;
            form.Location = new Point(lbTer.PointToScreen(new Point()).X, lbMes.PointToScreen(new Point()).Y + 10);
            
            Panel panel = new Panel();
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Size = form.Size;

            form.Controls.Add(panel);            
            Int32 y = 0;
            Int32 x = 0;
            for (Int32 i = 1; i < 13; i++)
            {
                Label label = new Label();
                label.Text = MesExtenso(i);
                label.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                label.Location = new Point(x, y);
                label.AutoSize = false;
                label.TextAlign = ContentAlignment.BottomCenter;
                label.Size = new Size(panel.Size.Width, 23);
                label.MouseHover += new EventHandler(label_MouseHover);
                label.MouseLeave += new EventHandler(label_MouseLeave);
                label.MouseClick += new MouseEventHandler(label_MouseClick);
                y += 20;
                panel.Controls.Add(label);
            }

            form.Deactivate += new EventHandler(form_Deactivate);
            form.Load += new EventHandler(form_Load);
            form.FormClosing += new FormClosingEventHandler(form_FormClosing);
            form.Show();
        }

        void form_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmMesAberto = false;
            frmPai.BringToFront();
        }

        void form_Load(object sender, EventArgs e)
        {
            frmMesAberto = true;
        }

        void label_MouseClick(object sender, MouseEventArgs e)
        {
            Label label = (Label)sender;
            lbMes.Text = label.Text;
            form.Close();
        }

        void label_MouseLeave(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            label.BackColor = Color.Transparent;
        }

        void label_MouseHover(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            label.BackColor = SystemColors.MenuHighlight;
        }

        void form_Deactivate(object sender, EventArgs e)
        {
            String diaSelecionado = DiaSelecionado();   
            DiaSemana(Convert.ToDateTime("01/" + DateTime.ParseExact(lbMes.Text, "MMMM", new CultureInfo("pt-BR")).Month.ToString() + "/" + lbAno.Text));
            SelecionaDia(diaSelecionado);
            Form form = (Form)sender;
            form.Dispose();
        }

        private void panelDataAtual_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            SelecionaDataAtual();
        }

        private void panelBordaVer_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            SelecionaDataAtual();
        }

        private void lbData_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            SelecionaDataAtual();
        }        

        private void lbAno_Click(object sender, EventArgs e)
        {
            numericUpDown = new NumericUpDown();
            numericUpDown.Minimum = Convert.ToDecimal(1753);
            numericUpDown.Maximum = Convert.ToDecimal(9998);            
            numericUpDown.KeyPress += new KeyPressEventHandler(numericUpDown_KeyPress);
            numericUpDown.KeyDown += new KeyEventHandler(numericUpDown_KeyDown);
            numericUpDown.ValueChanged += new EventHandler(numericUpDown_ValueChanged);
            numericUpDown.Size = lbAno.Size;
            numericUpDown.Value = Convert.ToDecimal(lbAno.Text);
            numericUpDown.Font = new System.Drawing.Font("Nina", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //numericUpDown.Location = lbAno.Location;

            lbAno.Controls.Add(numericUpDown);
            numericUpDown.Focus();            
            
        }

        void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown != null)
            {
                String diaSelecionado = DiaSelecionado();
                lbAno.Text = numericUpDown.Value.ToString();
                DiaSemana(Convert.ToDateTime("01/" + DateTime.ParseExact(lbMes.Text, "MMMM", new CultureInfo("pt-BR")).Month.ToString() + "/" + numericUpDown.Value.ToString()));
                SelecionaDia(diaSelecionado);
            }
        }

        void numericUpDown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                RemoveNumeric();
            }
        }       
        
        void numericUpDown_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            String diaSelecionado = DiaSelecionado();            
            if (DateTime.ParseExact(lbMes.Text, "MMMM", new CultureInfo("pt-BR")).Month == 1)
            {
                lbMes.Text = MesExtenso(12);
                if (lbAno.Text != "1753")
                    lbAno.Text = (Convert.ToInt32(lbAno.Text) - 1).ToString();
            }
            else
                lbMes.Text = MesExtenso(DateTime.ParseExact(lbMes.Text, "MMMM", new CultureInfo("pt-BR")).Month - 1);
            DiaSemana(Convert.ToDateTime("01/" + DateTime.ParseExact(lbMes.Text, "MMMM", new CultureInfo("pt-BR")).Month.ToString() + "/" + lbAno.Text));
            SelecionaDia(diaSelecionado);
                        
        }

        private void btnAvancar_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
            String diaSelecionado = DiaSelecionado();    
            if (DateTime.ParseExact(lbMes.Text, "MMMM", new CultureInfo("pt-BR")).Month == 12)
            {
                lbMes.Text = MesExtenso(1);
                if (lbAno.Text != "9998")
                    lbAno.Text = (Convert.ToInt32(lbAno.Text) + 1).ToString();
            }
            else
                lbMes.Text = MesExtenso(DateTime.ParseExact(lbMes.Text, "MMMM", new CultureInfo("pt-BR")).Month + 1);
            DiaSemana(Convert.ToDateTime("01/" + DateTime.ParseExact(lbMes.Text, "MMMM", new CultureInfo("pt-BR")).Month.ToString() + "/" + lbAno.Text));
            SelecionaDia(diaSelecionado);
        }

        private void Calendario_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
        }

        private void panelCabecalho_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
        }

        private void lbDe_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
        }

        private void lbDom_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
        }

        private void lbSeg_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
        }

        private void lbHoje_Click(object sender, EventArgs e)
        {
            RemoveNumeric();
        }
        
        private void Calendario_Resize(object sender, EventArgs e)
        {            
            if (this.Size.Width > 213 && this.Size.Height > 196)
            {
                //Panel azul            
                panelCabecalho.Size = new Size(this.Size.Width, this.Size.Height / 5);
                //Botão Voltar
                btnVoltar.Location = new Point(btnVoltar.Location.X, (panelCabecalho.Size.Height / 2) - 15);
                //Label Domingo
                lbDom.Location = new Point(panelLinha.Location.X, panelCabecalho.Size.Height + 4);
            }
            else
            {
                //Panel azul            
                panelCabecalho.Size = new Size(this.Size.Width, (this.Size.Height / 5) - 4);
                btnVoltar.Location = new Point(btnVoltar.Location.X, (panelCabecalho.Size.Height / 2) - 12);
                //Label Domingo
                lbDom.Location = new Point(0, panelCabecalho.Size.Height);
            }
            //Linha divisória
            panelLinha.Size = new Size(this.Size.Width - 6, panelLinha.Size.Height);            

            //Maior Calendario
            if (this.Size.Width > 303 && this.Size.Height > 274)
            {
                //Botões
                btnVoltar.Font = new System.Drawing.Font("Franklin Gothic Demi", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                btnVoltar.Size = new Size(30, 28);
                btnAvancar.Font = new System.Drawing.Font("Franklin Gothic Demi", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                btnAvancar.Size = new Size(30, 28);
                //Botão Avançar           
                btnAvancar.Location = new Point(panelCabecalho.Size.Width - 35, (panelCabecalho.Size.Height / 2) - 15);
                //Label Ano                
                lbAno.Location = new Point(btnAvancar.Location.X - 45, (panelCabecalho.Size.Height / 2) - 13);
                lbAno.Size = new Size(67, 22);
                //Label Mês 
                lbMes.Location = new Point(btnVoltar.Location.X + 38, (panelCabecalho.Size.Height / 2) - 14);
                lbDe.Location = new Point((panelCabecalho.Size.Width / 2) + 6, (panelCabecalho.Size.Height / 2) - 14);
                //Label Ano 
                lbAno.Location = new Point(btnAvancar.Location.X - 70, (panelCabecalho.Size.Height / 2) - 12);
                //Labels dias da semana
                lbSab.Location = new Point(panelLinha.Size.Width - 34, panelCabecalho.Size.Height + 4);
                lbMes.AutoSize = true;
                lbMes.Font = new System.Drawing.Font("Nina", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbDe.AutoSize = true;
                lbDe.Font = new System.Drawing.Font("Nina", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbAno.Font = new System.Drawing.Font("Nina", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbDom.AutoSize = true;
                lbDom.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbSeg.AutoSize = true;
                lbSeg.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbTer.AutoSize = true;
                lbTer.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbQua.AutoSize = true;
                lbQua.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbQui.AutoSize = true;
                lbQui.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbSex.AutoSize = true;
                lbSex.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbSab.AutoSize = true;
                lbSab.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                //Linha divisória            
                panelLinha.Location = new Point(panelLinha.Location.X, lbDom.Location.Y + 17);
                //Label Hoje
                lbHoje.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                //Label Data
                lbData.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                //Label Data
                lbData.Location = new Point(89, this.Size.Height - 26);
                //Label Dias Uteis
                lbDiaUteis.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                //Altera o tamanho da fonte dos dias
                foreach (Control labels in Controls)
                {
                    if (labels.GetType() == typeof(Label))
                    {
                        Label lbselecao = ((Label)(labels));
                        if (lbselecao.TabIndex >= 1 && lbselecao.TabIndex <= 42)
                        {
                            lbselecao.Font = new System.Drawing.Font("Courier New", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            lbselecao.Size = new Size(38, 27);
                        }
                    }
                }

            }
            else if (this.Size.Width > 213 && this.Size.Height > 196)
            {
                //Botões
                btnVoltar.Font = new System.Drawing.Font("Franklin Gothic Demi", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                btnVoltar.Size = new Size(30, 28);
                btnAvancar.Font = new System.Drawing.Font("Franklin Gothic Demi", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                btnAvancar.Size = new Size(30, 28);
                //Botão Avançar           
                btnAvancar.Location = new Point(panelCabecalho.Size.Width - 35, (panelCabecalho.Size.Height / 2) - 15);
                //Label Ano 
                lbAno.Size = new Size(50, 22);
                lbAno.Location = new Point(btnAvancar.Location.X - 45, (panelCabecalho.Size.Height / 2) - 12);                
                //Label Mês                 
                lbMes.Location = new Point(btnVoltar.Location.X + 38, (panelCabecalho.Size.Height / 2) - 9);
                //lbMes.Size = new Size(73,18);
                //Label De            
                lbDe.Location = new Point((panelCabecalho.Size.Width / 2) + 6, (panelCabecalho.Size.Height / 2) - 9);
                lbDe.Size = new Size(29,16);                

                //Labels dias da semana
                lbSab.Location = new Point(panelLinha.Size.Width - 24, panelCabecalho.Size.Height + 4);
                //lbMes.AutoSize = false;
                lbMes.Font = new System.Drawing.Font("Nina", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbDe.Font = new System.Drawing.Font("Nina", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbAno.Font = new System.Drawing.Font("Nina", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbDom.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbSeg.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbTer.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbQua.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbQui.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbSex.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbSab.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                //Linha divisória            
                panelLinha.Location = new Point(panelLinha.Location.X, lbDom.Location.Y + 14);
                //Label Hoje
                lbHoje.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                //Label Data
                lbData.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                //Label Data
                lbData.Location = new Point(lbHoje.Location.X + 35, this.Size.Height - 26);
                lbData.Size = new Size(lbData.Size.Width + 16, lbData.Size.Height);
                //Label Dias Uteis
                lbDiaUteis.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                //Altera o tamanho da fonte dos dias
                foreach (Control labels in Controls)
                {
                    if (labels.GetType() == typeof(Label))
                    {
                        Label lbselecao = ((Label)(labels));
                        if (lbselecao.TabIndex >= 1 && lbselecao.TabIndex <= 42)
                        {
                            lbselecao.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                            lbselecao.Size = new Size(30, 17);
                        }
                    }
                }

            }
            //Menor tamanho do calendario
            else
            {
                btnAvancar.Font = btnAvancar.Font = new System.Drawing.Font("Franklin Gothic Demi", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                btnVoltar.Font = btnAvancar.Font = new System.Drawing.Font("Franklin Gothic Demi", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                btnVoltar.Size = new Size(25, 22);
                btnAvancar.Size = new Size(25, 22);
                //Botão Avançar           
                btnAvancar.Location = new Point(panelCabecalho.Size.Width - 28, (panelCabecalho.Size.Height / 2) - 12);
                //Label Ano 
                lbAno.Size = new Size(47, 20);
                lbAno.Location = new Point(btnAvancar.Location.X - 39, (panelCabecalho.Size.Height / 2) - 11);
                //Label Mês                 
                lbMes.Location = new Point(btnVoltar.Location.X + 25, (panelCabecalho.Size.Height / 2) - 8);
                //lbMes.Size = new Size(73, 18);
                //Label De            
                lbDe.Location = new Point((panelCabecalho.Size.Width / 2) - 1, (panelCabecalho.Size.Height / 2) - 8);
                lbDe.Size = new Size(23, 16);
                //Labels dias da semana
                lbSab.Location = new Point(panelLinha.Size.Width - 23, panelCabecalho.Size.Height);
                //Fonte Dias da semana
                lbMes.Font = new System.Drawing.Font("Nina", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbDe.Font = new System.Drawing.Font("Nina", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbAno.Font = new System.Drawing.Font("Nina", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbDom.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbSeg.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbTer.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbQua.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbQui.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbSex.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lbSab.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                //Linha divisória            
                panelLinha.Location = new Point(panelLinha.Location.X, lbDom.Location.Y + 14);
                //Label Hoje
                lbHoje.Font = lbHoje.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                //Label Data
                lbData.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                //Label Data
                //lbData.Location = new Point(lbHoje.Location.X + 11, this.Size.Height - 26);
                //lbData.Size = new Size(lbData.Size.Width + 16, lbData.Size.Height);
                //Label Dias Uteis
                lbDiaUteis.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

                //Altera o tamanho da fonte dos dias
                foreach (Control labels in Controls)
                {
                    if (labels.GetType() == typeof(Label))
                    {
                        Label lbselecao = ((Label)(labels));
                        if (lbselecao.TabIndex >= 1 && lbselecao.TabIndex <= 42)
                        {
                            lbselecao.Font = new System.Drawing.Font("Courier New", 9F);
                            lbselecao.Size = new System.Drawing.Size(23, 15);
                        }
                    }
                }
            }
            if (this.Size.Width > 213 && this.Size.Height > 196)
            {
                lbDiaUteis.Size = new Size(60, 15);
            }
            else
                lbDiaUteis.Size = new Size(42, 15);

            //Label Dias Uteis
            lbDiaUteis.Location = new Point(this.Size.Width - lbDiaUteis.Size.Width, this.Size.Height - 22);
            if (this.Size.Width > 303 && this.Size.Height > 274)
            {                
                //Panel Quadro Vermelho
                panelBordaVer.Location = new Point(panelBordaVer.Location.X, this.Size.Height - 25);
                //Panel Quadro branco
                panelDataAtual.Location = new Point(panelDataAtual.Location.X, this.Size.Height - 23);
                //Panel Quadro Vermelho
                panelBordaVer.Size = new Size(33, 20);
                //Panel Quadro branco
                panelDataAtual.Size = new Size(29, 16);
                //Label hoje
                lbHoje.Location = new Point(lbHoje.Location.X, this.Size.Height - 23);
                //Label Data
                lbData.Location = new Point(lbHoje.Location.X + 50, this.Size.Height - 23);
                //lbData.AutoSize = true;                
            }
            else
            {
                //Panel Quadro Vermelho
                panelBordaVer.Location = new Point(panelBordaVer.Location.X, this.Size.Height - 22);
                //Panel Quadro branco
                panelDataAtual.Location = new Point(panelDataAtual.Location.X, this.Size.Height - 20);
                //Panel Quadro Vermelho
                panelBordaVer.Size = new Size(28, 15);                
                //Panel Quadro branco
                panelDataAtual.Size = new Size(24, 11);
                //Label hoje
                lbHoje.Location = new Point(lbHoje.Location.X, this.Size.Height - 22);
                //Label Data
                //lbData.AutoSize = false;
                if (this.Size.Width > 213 && this.Size.Height > 196)
                {
                    lbData.Location = new Point(lbHoje.Location.X + 35, this.Size.Height - 22);                    
                }
                else
                {
                    lbData.Location = new Point(lbHoje.Location.X + 30, this.Size.Height - 22);  
                }
            }
            if (this.Size.Width > 213 && this.Size.Height > 196)
            {
                //Label Segunda
                lbSeg.Location = new Point((lbSab.Location.X - lbDom.Location.X + 10) / 6, panelCabecalho.Size.Height + 4);
                //Label Terça
                lbTer.Location = new Point(((lbSab.Location.X - lbDom.Location.X + 10) / 6) * 2, panelCabecalho.Size.Height + 4);
                //Label Quarta
                lbQua.Location = new Point(((lbSab.Location.X - lbDom.Location.X + 10) / 6) * 3, panelCabecalho.Size.Height + 4);
                //Label Quinta
                lbQui.Location = new Point(((lbSab.Location.X - lbDom.Location.X + 10) / 6) * 4, panelCabecalho.Size.Height + 4);
                //Label Sexta
                lbSex.Location = new Point(((lbSab.Location.X - lbDom.Location.X + 10) / 6) * 5, panelCabecalho.Size.Height + 4);

            }
            else
            {
                //Label Segunda
                lbSeg.Location = new Point((lbSab.Location.X - lbDom.Location.X + 7) / 6, panelCabecalho.Size.Height );
                //Label Terça
                lbTer.Location = new Point(((lbSab.Location.X - lbDom.Location.X + 4) / 6) * 2, panelCabecalho.Size.Height);
                //Label Quarta
                lbQua.Location = new Point(((lbSab.Location.X - lbDom.Location.X + 4) / 6) * 3, panelCabecalho.Size.Height);
                //Label Quinta
                lbQui.Location = new Point(((lbSab.Location.X - lbDom.Location.X + 4) / 6) * 4, panelCabecalho.Size.Height);
                //Label Sexta
                lbSex.Location = new Point(((lbSab.Location.X - lbDom.Location.X + 1) / 6) * 5, panelCabecalho.Size.Height);

            }
            if (this.Size.Width > 213 && this.Size.Height > 196)
            {
                //Label 36
                lb36.Location = new Point(lbDom.Location.X - 2, panelBordaVer.Location.Y - 28);
            }
            else
            {
                //Label 36
                lb36.Location = new Point(lbDom.Location.X - 2, panelBordaVer.Location.Y - 19);
            }
            //Label 1
            lb1.Location = new Point(lbDom.Location.X, panelLinha.Location.Y + 5);


            if (this.Size.Width > 303 && this.Size.Height > 274)
            {
                //Label 2
                lb2.Location = new Point(lbSeg.Location.X, lbSeg.Location.Y + 22);
                //Label 3
                lb3.Location = new Point(lbTer.Location.X, lbTer.Location.Y + 22);
                //Label 4
                lb4.Location = new Point(lbQua.Location.X, lbQua.Location.Y + 22);
                //Label 5
                lb5.Location = new Point(lbQui.Location.X, lbQui.Location.Y + 22);
                //Label 6
                lb6.Location = new Point(lbSex.Location.X, lbSex.Location.Y + 22);
                //Label 7
                lb7.Location = new Point(lbSab.Location.X, lbSab.Location.Y + 22);
            }
            else
            {
                //Label 2
                lb2.Location = new Point(lbSeg.Location.X, lbSeg.Location.Y + 19);
                //Label 3
                lb3.Location = new Point(lbTer.Location.X, lbTer.Location.Y + 19);
                //Label 4
                lb4.Location = new Point(lbQua.Location.X, lbQua.Location.Y + 19);
                //Label 5
                lb5.Location = new Point(lbQui.Location.X, lbQui.Location.Y + 19);
                //Label 6
                lb6.Location = new Point(lbSex.Location.X, lbSex.Location.Y + 19);
                //Label 7
                lb7.Location = new Point(lbSab.Location.X, lbSab.Location.Y + 19);
            }
            //Label 8
            lb8.Location = new Point(lbDom.Location.X - 2, ((lb36.Location.Y - lb1.Location.Y) / 5) + lb1.Location.Y);
            //Label 9
            lb9.Location = new Point(lbSeg.Location.X - 2, lb8.Location.Y);
            //Label 10
            lb10.Location = new Point(lbTer.Location.X, lb8.Location.Y);
            //Label 11
            lb11.Location = new Point(lbQua.Location.X, lb8.Location.Y);
            //Label 12
            lb12.Location = new Point(lbQui.Location.X, lb8.Location.Y);
            //Label 13
            lb13.Location = new Point(lbSex.Location.X, lb8.Location.Y);
            //Label 14
            lb14.Location = new Point(lbSab.Location.X, lb8.Location.Y);
            //Label 15
            lb15.Location = new Point(lbDom.Location.X - 2, ((lb36.Location.Y - lb1.Location.Y) / 5) + lb8.Location.Y);
            //Label 16
            lb16.Location = new Point(lbSeg.Location.X - 2, lb15.Location.Y);
            //Label 17
            lb17.Location = new Point(lbTer.Location.X, lb15.Location.Y);
            //Label 18
            lb18.Location = new Point(lbQua.Location.X, lb15.Location.Y);
            //Label 19
            lb19.Location = new Point(lbQui.Location.X, lb15.Location.Y);
            //Label 20
            lb20.Location = new Point(lbSex.Location.X, lb15.Location.Y);
            //Label 21
            lb21.Location = new Point(lbSab.Location.X, lb15.Location.Y);
            //Label 22
            lb22.Location = new Point(lbDom.Location.X - 2, ((lb36.Location.Y - lb1.Location.Y) / 5) + lb15.Location.Y);
            //Label 23
            lb23.Location = new Point(lbSeg.Location.X - 2, lb22.Location.Y);
            //Label 24
            lb24.Location = new Point(lbTer.Location.X, lb22.Location.Y);
            //Label 25
            lb25.Location = new Point(lbQua.Location.X, lb22.Location.Y);
            //Label 26
            lb26.Location = new Point(lbQui.Location.X, lb22.Location.Y);
            //Label 27
            lb27.Location = new Point(lbSex.Location.X, lb22.Location.Y);
            //Label 28
            lb28.Location = new Point(lbSab.Location.X, lb22.Location.Y);
            //Label 29
            lb29.Location = new Point(lbDom.Location.X - 2, ((lb36.Location.Y - lb1.Location.Y) / 5) + lb22.Location.Y);
            //Label 30
            lb30.Location = new Point(lbSeg.Location.X - 2, lb29.Location.Y);
            //Label 31
            lb31.Location = new Point(lbTer.Location.X, lb29.Location.Y);
            //Label 32
            lb32.Location = new Point(lbQua.Location.X, lb29.Location.Y);
            //Label 33
            lb33.Location = new Point(lbQui.Location.X, lb29.Location.Y);
            //Label 34
            lb34.Location = new Point(lbSex.Location.X, lb29.Location.Y);
            //Label 35
            lb35.Location = new Point(lbSab.Location.X, lb29.Location.Y);
            //Label 36
            lb36.Location = new Point(lbDom.Location.X - 2, ((lb36.Location.Y - lb1.Location.Y) / 5) + lb29.Location.Y);
            //Label 37
            lb37.Location = new Point(lbSeg.Location.X - 2, lb36.Location.Y);
            //Label 38
            lb38.Location = new Point(lbTer.Location.X, lb36.Location.Y);
            //Label 39
            lb39.Location = new Point(lbQua.Location.X, lb36.Location.Y);
            //Label 40
            lb40.Location = new Point(lbQui.Location.X, lb36.Location.Y);
            //Label 41
            lb41.Location = new Point(lbSex.Location.X, lb36.Location.Y);
            //Label 42
            lb42.Location = new Point(lbSab.Location.X, lb36.Location.Y);

            if (Grade == true)
            {
                panel1.Location = new Point(((lbSeg.Location.X + lbDom.Location.X)/ 2) + 11 , panelLinha.Location.Y);
                panel2.Location = new Point(((lbSeg.Location.X + lbTer.Location.X) / 2) + 11, panelLinha.Location.Y);
                panel3.Location = new Point(((lbTer.Location.X + lbQua.Location.X) / 2) + 11, panelLinha.Location.Y);
                panel4.Location = new Point(((lbQua.Location.X + lbQui.Location.X) / 2) + 12, panelLinha.Location.Y);
                panel5.Location = new Point(((lbQui.Location.X + lbSex.Location.X) / 2) + 11, panelLinha.Location.Y);
                panel6.Location = new Point(((lbSex.Location.X + lbSab.Location.X) / 2) + 11, panelLinha.Location.Y);
                
                if (this.Size.Width > 303 && this.Size.Height > 274)
                {
                    panel12.Location = new Point(panelLinha.Location.X, panelBordaVer.Location.Y - 5);
                    panel7.Location = new Point(panelLinha.Location.X, ((lb1.Location.Y + lb8.Location.Y) / 2) + 13);
                    panel8.Location = new Point(panelLinha.Location.X, ((lb8.Location.Y + lb15.Location.Y) / 2) + 13);
                    panel9.Location = new Point(panelLinha.Location.X, ((lb15.Location.Y + lb22.Location.Y) / 2) + 13);
                    panel10.Location = new Point(panelLinha.Location.X, ((lb22.Location.Y + lb29.Location.Y) / 2) + 13);
                    panel11.Location = new Point(panelLinha.Location.X, ((lb29.Location.Y + lb36.Location.Y) / 2) + 13);
                }
                else if (this.Size.Width > 213 && this.Size.Height > 196)
                {
                    panel12.Location = new Point(panelLinha.Location.X, (panel11.Location.Y - panel10.Location.Y) + lb36.Location.Y);
                    panel7.Location = new Point(panelLinha.Location.X, ((lb1.Location.Y + lb8.Location.Y) / 2) + 9);
                    panel8.Location = new Point(panelLinha.Location.X, ((lb8.Location.Y + lb15.Location.Y) / 2) + 9);
                    panel9.Location = new Point(panelLinha.Location.X, ((lb15.Location.Y + lb22.Location.Y) / 2) + 9);
                    panel10.Location = new Point(panelLinha.Location.X, ((lb22.Location.Y + lb29.Location.Y) / 2) + 9);
                    panel11.Location = new Point(panelLinha.Location.X, ((lb29.Location.Y + lb36.Location.Y) / 2) + 9);
                }
                else
                {
                    //panel12.Location = new Point(panelLinha.Location.X, (panel11.Location.Y - lb29.Location.Y) + lb36.Location.Y);
                    panel12.Location = new Point(panelLinha.Location.X, (panel11.Location.Y - panel10.Location.Y) + lb36.Location.Y);
                    panel7.Location = new Point(panelLinha.Location.X, ((lb1.Location.Y + lb8.Location.Y) / 2) + 7);
                    panel8.Location = new Point(panelLinha.Location.X, ((lb8.Location.Y + lb15.Location.Y) / 2) + 7);
                    panel9.Location = new Point(panelLinha.Location.X, ((lb15.Location.Y + lb22.Location.Y) / 2) + 7);
                    panel10.Location = new Point(panelLinha.Location.X, ((lb22.Location.Y + lb29.Location.Y) / 2) + 7);
                    panel11.Location = new Point(panelLinha.Location.X, ((lb29.Location.Y + lb36.Location.Y) / 2) + 7);
                }
                                
                panel1.Size = new Size(1, panel12.Location.Y - panelLinha.Location.Y);
                panel2.Size = new Size(1, panel12.Location.Y - panelLinha.Location.Y);
                panel3.Size = new Size(1, panel12.Location.Y - panelLinha.Location.Y);
                panel4.Size = new Size(1, panel12.Location.Y - panelLinha.Location.Y);
                panel5.Size = new Size(1, panel12.Location.Y - panelLinha.Location.Y);
                panel6.Size = new Size(1, panel12.Location.Y - panelLinha.Location.Y);
                panel7.Size = new Size(panelLinha.Size.Width, 1);
                panel8.Size = new Size(panelLinha.Size.Width, 1);
                panel9.Size = new Size(panelLinha.Size.Width, 1);
                panel10.Size = new Size(panelLinha.Size.Width, 1);
                panel11.Size = new Size(panelLinha.Size.Width, 1);
                panel12.Size = new Size(panelLinha.Size.Width, 1);             
                
            }
        }

        void lb1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            base.OnKeyDown(e);
        }

        #endregion

        private void lb1_DoubleClick(object sender, EventArgs e)
        {
            base.OnDoubleClick(e);
        }

        protected override void OnEnter(EventArgs e)
        {
            Focado = true;

            base.OnEnter(e);
        }
        
        protected override void OnLeave(EventArgs e)
        {
            Focado = false;

            base.OnLeave(e);
        }
    }
}
