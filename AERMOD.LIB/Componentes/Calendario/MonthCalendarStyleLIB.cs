using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Globalization;
using Netsof.LIB.Componentes.Calendario.Classes;

namespace Netsof.LIB.Componentes.Calendario
{
    [ToolboxBitmapAttribute(typeof(MonthCalendar))]
    public partial class MonthCalendarStyleLIB : UserControl
    {
        /// <summary>
        /// Ocorre quando seleciona uma determinada data.
        /// </summary>
        [Description("Ocorre quando seleciona uma determinada data.")]
        public event DateChangedEventHandler OnDateChanged;
        public virtual void DateChanged(DateChangedEventArgs e)
        {
            if(OnDateChanged != null)
            {                
                OnDateChanged(this, e);
            }
        }

        /// <summary>
        /// Ocorre quando se seleciona um feriado
        /// </summary>
        [Description("Ocorre quando se seleciona um feriado")]
        public event FeriadoChangedEventHandler OnFeriadoChanged;
        public virtual void FeriadoChanged(FeriadoChangedEventsArgs e)
        {
            if (OnFeriadoChanged != null)
            {                
                OnFeriadoChanged(this, e);
            }
        }

        /// <summary>
        /// Variavel do programador(Não setar valor)
        /// </summary>
        public Boolean feriadosNaoFixo = false;
        /// <summary>
        /// Ativa e desativa os feriados alterantes: Pascoa,Corpus Christ e Carnaval 
        /// </summary>
        [Description("Ativar os feriados não fixo do Brasil: Pascoa,Corpus Christ e Carnaval")]        
        public Boolean FeriadosNaoFixo
        {
            get { return feriadosNaoFixo; }
            set 
            {
                if (feriadosNaoFixo != value)
                {
                    feriadosNaoFixo = value;

                    Base.RetornaClassDias(identificador).CarregaDias(lbMes.Text, Convert.ToInt32(lbAno.Text));
                    if(Base.RetornaClassDias(identificador).loadCreate == false)
                        Base.RetornaClassDias(identificador).MarcaDataSelecionada();

                }
            }
        }

        /// <summary>
        /// Field ativar grade
        /// </summary>
        private Boolean ativarGrade = false;
        /// <summary>
        /// Ativa e desativa a grade separador de dias
        /// </summary>
        [Description("Ativa e desativa a grade separador de dias")] 
        public Boolean AtivarGrade
        {
            get { return ativarGrade; }
            set 
            { 
                ativarGrade = value;
                if (ativarGrade == true)
                {
                    tableLayoutPanelDias.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                }
                else
                {
                    tableLayoutPanelDias.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;
                }
            }
        }
        
        /// <summary>
        /// Form para seleção do Mês
        /// </summary>
        FrmMes frmMes = null;        

        private Boolean _Focused;
        /// <summary>
        /// Retorna True para controle focado e False para não focado
        /// </summary>
        public override Boolean Focused
        {
            get { return _Focused; }
        }

        private DateTime dateIsSelected = DateTime.Now;

        /// <summary>
        /// Get ou Set a data selecionada
        /// </summary>
        [Description("Get ou Set a data selecionada")] 
        public DateTime DateIsSelected
        {
            get { return dateIsSelected; }
            set 
            { 
                dateIsSelected = value;
                if (Base.RetornaClassDias(identificador).loadComplet == true)
                {
                    lbMes.Text = Base.RetornaClassMes(identificador).MesExtenso(dateIsSelected.Month);
                    lbAno.Text = dateIsSelected.Year.ToString();
                    Base.RetornaClassDias(identificador).DiaSelect = dateIsSelected.Day.ToString();
                    Base.RetornaClassDias(identificador).CarregaDias(lbMes.Text, Convert.ToInt32(lbAno.Text));
                    Base.RetornaClassDias(identificador).MarcaDataSelecionada();
                }
            }
        }
        
        /// <summary>
        /// Data atual
        /// </summary>
        [Description("Data atual")] 
        public DateTime DateNow
        {
            get { return Convert.ToDateTime(lbDataHoje.Text); }           
        }
        
        /// <summary>
        /// Dias uteis entre a data selecionada e a data atual
        /// </summary>
        public Int32 DiasUteis
        {
            get 
            {
                return CalculoDiasUteis(); 
            }
        }

        /// <summary>
        /// Números de dias entre a data selecionada e data atual
        /// </summary>
        public Int32 DiasCorridos
        {
            get { return CalculoDiasTranscorridos(); }            
        }        

        private MonthCalendarStyleLIB identificador = null;
        public MonthCalendarStyleLIB()
        {
            InitializeComponent();
            identificador = this;

            Base.AddMonthCalendar(identificador, new Dias(identificador), new Ano(identificador), new Mes(identificador));                           
        }

        /// <summary>
        /// Seta a data na variavel dateIsSelected(Utilizado pelo programador).
        /// </summary>
        public void SetDateIsSelected()
        {
            dateIsSelected = Convert.ToDateTime(string.Format("{0}/{1}/{2}", Base.RetornaClassDias(identificador).DiaSelect, DateTime.ParseExact(lbMes.Text, "MMMM", new CultureInfo("pt-BR")).Month.ToString(),lbAno.Text)); 
        }

        /// <summary>
        /// Labels que representão os dias
        /// </summary>
        private void LabelsDias()
        {
            Base.RetornaClassDias(identificador).AddLabelDia(new Label[]{lb0,lb1,lb2,lb3,lb4,lb5,lb6,lb7,lb8,lb9,lb10,lb11,lb12,lb13,lb14,lb15,lb16,lb17,lb18,lb19,lb20,
            lb21,lb22,lb23,lb24,lb25,lb26,lb27,lb28,lb29,lb30,lb31,lb32,lb33,lb34,lb35,lb36,lb37,lb38,lb39,lb40,lb41});
        }

        /// <summary>
        /// Controle do mes e ano
        /// </summary>
        private void ControleHeader()
        {
            Base.RetornaClassMes(identificador).AddControles(new Control[] {lbMes,lbAno});
        }

        /// <summary>
        /// Controle do ano
        /// </summary>
        private void ControleAno()
        {
            Base.RetornaClassAno(identificador).AddControles(new Control[] {lbAno,lbMes, pictureBoxBaixo,pictureBoxCima});
        }

        /// <summary>
        /// Seta as configuraçõe iniciais
        /// </summary>
        private void ConfiguracoesIniciais()
        {
            lbDataHoje.Text = DateTime.Now.ToString("dd/MM/yyyy");
            lbAno.Text = DateTime.Now.Year.ToString();
            lbMes.Text = Base.RetornaClassMes(identificador).MesExtenso(DateTime.Now.Month);

            dateIsSelected = DateNow;

            LabelsDias();
            ControleHeader();
            ControleAno();
            Base.RetornaClassDias(identificador).labelDiasUteis = lbDiasUteis;

            Base.RetornaClassDias(identificador).CarregaDias(lbMes.Text, Convert.ToInt32(lbAno.Text));
            Base.RetornaClassDias(identificador).MarcaDataSelecionada();

            //this.MinimumSize = new Size(198, 187);
            //this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //this.panelHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //this.panelHeader.MinimumSize = new Size(200, 30);
            //this.tableLayoutPanelDiasSemanas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //this.tableLayoutPanelDias.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));            
        }

        /// <summary>
        /// Carrega a data atual dias,mes e ano
        /// </summary>
        private void CarregaDataAtual()
        {
            lbAno.Text = DateTime.Now.Year.ToString();
            lbMes.Text = Base.RetornaClassMes(identificador).MesExtenso(DateTime.Now.Month);

            Base.RetornaClassDias(identificador).CarregaDias(lbMes.Text, Convert.ToInt32(lbAno.Text));
            Base.RetornaClassDias(identificador).DiaSelect = DateTime.Now.Day.ToString();
            Base.RetornaClassDias(identificador).MarcaDataSelecionada();
        }

        /// <summary>
        /// Calcula os dias uteis entre duas datas(Utilizado pelo programador).
        /// </summary>       
        public Int32 CalculoDiasUteis()
        {
            if (DateIsSelected <= DateNow)
            {
                return Base.RetornaClassDias(identificador).GetDiasUteisBack(DateIsSelected, DateNow);                
            }
            else
            {
                return Base.RetornaClassDias(identificador).GetDiasUteisNext(DateNow, DateIsSelected);            
            }
        }

        /// <summary>
        /// Calcula os dias transcorridos entre duas datas
        /// </summary> 
        private Int32 CalculoDiasTranscorridos()
        {
            TimeSpan diff = DateNow - DateIsSelected;
            Int32 dias = diff.Days;
            if (dias < 0)
                dias = dias * -1;
            return dias;
        }

        /// <summary>
        /// Adiciona os feriados fixos
        /// </summary>
        /// <param name="dia">Dia do feriado</param>
        /// <param name="mes">Mes do feriado</param>
        /// <param name="descricao">Descrição do feriado</param>
        public void AddFeriadoFixo(Int32 dia, Int32 mes, String descricao)
        {
            Base.RetornaClassDias(identificador).AddFeriadosFixo(dia, mes, descricao);

            Base.RetornaClassDias(identificador).CarregaDias(lbMes.Text, Convert.ToInt32(lbAno.Text));
            Base.RetornaClassDias(identificador).loadCreate = true;
            Base.RetornaClassDias(identificador).MarcaDataSelecionada();
        }

        /// <summary>
        /// Adiciona marcação a data
        /// Menor 4 : 25 Porcento
        /// Maior e igual 4 e Menor 6 : 50 Porcento
        /// Maior e igual 6 e Menor 9 : 75 Porcento
        /// Maior e igual 9 : 100 Porcento 
        /// </summary>
        /// <param name="dataMarcacao">Data da marcação</param>
        /// <param name="qtdMarcacao">Quantidade de marcação no minimo uma</param>
        /// <param name="fixar">Fixar True .. Não fixar False</param>
        public void AddMarcacao(DateTime dataMarcacao,Int32 qtdMarcacao, EnumFixo typeFixo)
        {
            for (Int32 i = 0; i < qtdMarcacao; i++)
            {
                Base.RetornaClassDias(identificador).AddMarcacao(dataMarcacao, typeFixo);
            }

            Base.RetornaClassDias(identificador).CarregaDias(lbMes.Text, Convert.ToInt32(lbAno.Text));
            Base.RetornaClassDias(identificador).loadCreate = true;
            Base.RetornaClassDias(identificador).MarcaDataSelecionada();
        }      

        /// <summary>
        /// Remove uma marcação de determinada data
        /// </summary>
        /// <param name="data">Data da marcação</param>
        public void RemoverMarcacao(DateTime data)
        {
            Base.RetornaClassDias(identificador).RemoverMarcacao(data);
        }

        /// <summary>
        /// Remove todas as marcações cadastradas
        /// </summary>
        public void RemoverTodasMarcacoes()
        {
            Base.RetornaClassDias(identificador).RemoverTodasMarcacoes();
        }

        /// <summary>
        /// Remove o feriado
        /// </summary>
        /// <param name="dia">dia do feriado</param>
        /// <param name="mes">Mes do feriado</param>
        public void RemoverFeriado(Int32 dia, Int32 mes)
        {
            Base.RetornaClassDias(identificador).RemoverFeriado(dia, mes);

            Base.RetornaClassDias(identificador).CarregaDias(lbMes.Text, Convert.ToInt32(lbAno.Text));
            Base.RetornaClassDias(identificador).loadCreate = true;
            Base.RetornaClassDias(identificador).MarcaDataSelecionada();
        }

        /// <summary>
        /// Remove todos os feriados cadastrados
        /// </summary>
        public void RemoverTodosFeriados()
        {
            Base.RetornaClassDias(identificador).RemoverTodosFeriados();

            Base.RetornaClassDias(identificador).CarregaDias(lbMes.Text, Convert.ToInt32(lbAno.Text));
            Base.RetornaClassDias(identificador).loadCreate = true;
            Base.RetornaClassDias(identificador).MarcaDataSelecionada();
        }

        private void pictureBoxCima_MouseDown(object sender, MouseEventArgs e)
        {
            Base.RetornaClassAno(identificador).MouseDownCima();
        }

        private void pictureBoxCima_MouseUp(object sender, MouseEventArgs e)
        {
            Base.RetornaClassAno(identificador).MouseUpCima();
        }

        private void pictureBoxCima_MouseLeave(object sender, EventArgs e)
        {
            Base.RetornaClassAno(identificador).MouseUpCima();
        }

        private void pictureBoxBaixo_MouseDown(object sender, MouseEventArgs e)
        {
            Base.RetornaClassAno(identificador).MouseDownBaixo();
        }

        private void pictureBoxBaixo_MouseUp(object sender, MouseEventArgs e)
        {
            Base.RetornaClassAno(identificador).MouseUpBaixo();
        }

        private void pictureBoxBaixo_MouseLeave(object sender, EventArgs e)
        {
            Base.RetornaClassAno(identificador).MouseUpBaixo();
        }

        private void lbMes_Click(object sender, EventArgs e)
        {
            this.Focus();

            frmMes = new FrmMes();
            frmMes.Location = lbMes.PointToScreen(new Point());
            frmMes.Deactivate += new EventHandler(frmMes_Deactivate);
            frmMes.Show();
        }

        void frmMes_Deactivate(object sender, EventArgs e)
        {
            if (frmMes.mes != string.Empty)
            {
                lbMes.Text = frmMes.mes;
                Base.RetornaClassDias(identificador).CarregaDias(lbMes.Text, Convert.ToInt32(lbAno.Text));
                Base.RetornaClassDias(identificador).MarcaDataSelecionada();
            }
            frmMes.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Focus();
            Base.RetornaClassMes(identificador).VoltarMes();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            this.Focus();
            Base.RetornaClassMes(identificador).AvancarMes();
        }

        private void MonthCalenderLIB_Enter(object sender, EventArgs e)
        {
            _Focused = true;
        }

        private void MonthCalenderLIB_Leave(object sender, EventArgs e)
        {
            _Focused = false;
        }

        protected override void OnCreateControl()
        {
            ConfiguracoesIniciais();

            timerUpdateData.Enabled = true;
            this.TabStop = true;
            base.OnCreateControl();
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {            
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            base.OnControlAdded(e);
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            Base.RetornaMonthCalendar(identificador);
            base.OnHandleDestroyed(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Up:
                    Base.RetornaClassDias(identificador).TeclaUp();
                    return true;                    
                case Keys.Down:
                    Base.RetornaClassDias(identificador).TeclaDown();
                    return true;                  
                case Keys.Left:
                    Base.RetornaClassDias(identificador).TeclaLeft();
                    return true;                    
                case Keys.Right:
                    Base.RetornaClassDias(identificador).TeclaRight();
                    return true;                  
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ribbonBarDataAtual_Click(object sender, EventArgs e)
        {
            this.Focus();
            CarregaDataAtual();
        }

        private void lb0_Click(object sender, EventArgs e)
        {
            this.Focus();
            //Verifico se não está selecionando a data ja selecionada
            if (((Label)sender).BorderStyle != BorderStyle.FixedSingle)
            {
                //Dias do Mes
                if (((Label)sender).ForeColor == SystemColors.ControlText || ((Label)sender).ForeColor == Color.Red)
                {
                    Base.RetornaClassDias(identificador).LimparSelecao();
                    Base.RetornaClassDias(identificador).DiaSelect = ((Label)sender).Text;
                    Base.RetornaClassDias(identificador).MarcaDataSelecionada();
                }
                //Dias do Mes anterior ou do seguinte
                else
                {
                    Base.RetornaClassDias(identificador).LimparSelecao();
                    Base.RetornaClassDias(identificador).DiaSelect = ((Label)sender).Text;
                    
                    //Mes anterior
                    if (((Label)sender).TabIndex < 7)
                    {                        
                        btnBack.PerformClick();                      
                    }
                    //Mes seguinte
                    else
                    {
                        btnNext.PerformClick();
                    }
                    Base.RetornaClassDias(identificador).MarcaDataSelecionada();
                }
            }
        }

        private void lbDe_Click(object sender, EventArgs e)
        {
            this.Focus();
        }

        private void MonthCalendarStyleLIB_Resize(object sender, EventArgs e)
        {
            if (this.Size.Width > 256 && this.Size.Height > 249)
            {
                tableLayoutPanelDias.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                tableLayoutPanelDiasSemanas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }
            else
            {
                tableLayoutPanelDias.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                tableLayoutPanelDiasSemanas.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }
        }

        private void timerUpdateData_Tick(object sender, EventArgs e)
        {
            if (DateTime.Now.Date != this.DateNow.Date)
            {
                lbDataHoje.Text = DateTime.Now.ToString("dd/MM/yyyy");
                this.DateIsSelected = dateIsSelected;
            }
        }             
    }

    public class DateChangedEventArgs : EventArgs{}
    public delegate void DateChangedEventHandler (object sender , DateChangedEventArgs e);

    public class FeriadoChangedEventsArgs : EventArgs
    {
        /// <summary>
        /// Descrição do feriado
        /// </summary>
        public string descricaoFeriado { get; set; }
    }
    public delegate void FeriadoChangedEventHandler(object sender, FeriadoChangedEventsArgs e);

}
