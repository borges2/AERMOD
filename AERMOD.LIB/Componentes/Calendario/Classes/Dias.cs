using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Drawing;
using System.Windows.Forms;

namespace Netsof.LIB.Componentes.Calendario.Classes
{
    public class Dias
    {
        MonthCalendarStyleLIB identificador = null;

        public Dias(MonthCalendarStyleLIB ident)
        {
            identificador = ident;
        }

        /// <summary>
        /// Lista de LabelX dos dias
        /// </summary>
        private List<Label> listLabel = new List<Label>();

        /// <summary>
        /// Calendario utiliado no Brasil
        /// </summary>
        GregorianCalendar gCalendario = new GregorianCalendar();

        /// <summary>
        /// Label do dia Uteis
        /// </summary>
        public Label labelDiasUteis = null;

        /// <summary>
        /// Lista de Feriados Fixos
        /// </summary>
        private List<FeriadosFixo> listFeriadosFixo = new List<FeriadosFixo>();

        /// <summary>
        /// Lista de Marcações na data
        /// </summary>
        private List<Marcacao> listMarcacao = new List<Marcacao>();        

        /// <summary>
        /// Dia selecionado
        /// </summary>
        private string diaSelect = DateTime.Now.Day.ToString();

        /// <summary>
        /// Variavel de ajuda para saber se os controles ja foram add ao list
        /// </summary>
        public Boolean loadComplet = false;
        /// <summary>
        /// Verifico se a marcação da data é na criação do componente
        /// </summary>
        public Boolean loadCreate = true;

        /// <summary>
        /// Dia selecionado(Utilizado quando se troca de Ano ou Mês)
        /// </summary>
        public string DiaSelect
        {
            get { return diaSelect; }
            set { diaSelect = value; }
        }        

        /// <summary>
        /// Adiciona as labels que mostraram os dias
        /// </summary>
        /// <param name="labels">Coleção de labels</param>
        public void AddLabelDia(Label[] labels)
        {
            if (listLabel.Count == 0)
            {
                listLabel.AddRange(labels);
            }
            loadComplet = true;
        }

        /// <summary>
        /// Preenche os labels com os dias 
        /// </summary>
        /// <param name="mes">Mes por extenso</param>
        /// <param name="mes">Ano numerico 4 digitos</param>
        public void CarregaDias(string mesExtenso, Int32 ano)
        {
            DateTime date = Convert.ToDateTime("01/" + DateTime.ParseExact(mesExtenso, "MMMM", new CultureInfo("pt-BR")).Month.ToString() + "/" + ano);
            Int32 mes = DateTime.ParseExact(mesExtenso, "MMMM", new CultureInfo("pt-BR")).Month;
            Int32 numeroDias = gCalendario.GetDaysInMonth(ano, mes);
            Int32 data = Convert.ToInt32(date.DayOfWeek);
            DateTime dataAnterior = date.AddMonths(-1);
            Int32 dia = 1;

            //Seleciona os labels que receberam os dias do mes
            List<Label> listDiasDoMes = listLabel.Where(f => f.TabIndex >= data && f.TabIndex < numeroDias + data).ToList();
            foreach (Label label in listDiasDoMes)
            {
                label.Text = dia.ToString();
                label.ForeColor = SystemColors.ControlText;
                label.BackColor = Color.Transparent;
                label.BorderStyle = BorderStyle.None;
                label.BackgroundImage = null;
                dia++;

                //verifico se o dia cai no domingo
                if (label.TabIndex == 0 || label.TabIndex == 7 || label.TabIndex == 14 || label.TabIndex == 21 || label.TabIndex == 28 || label.TabIndex == 35)
                {
                    label.ForeColor = Color.Red;
                }
            }

            //Seleciona os dias do Mes anterior
            List<Label> listDiasDoMesAnterior = listLabel.Where(f => f.TabIndex < data).ToList();
            dia = 0;
            for (Int32 i = listDiasDoMesAnterior.Count - 1; i >= 0; i--)
            {
                listDiasDoMesAnterior[i].Text = (gCalendario.GetDaysInMonth(dataAnterior.Year, dataAnterior.Month) - dia).ToString();
                listDiasDoMesAnterior[i].ForeColor = Color.Silver;
                listDiasDoMesAnterior[i].BackColor = Color.Transparent;
                listDiasDoMesAnterior[i].BorderStyle = BorderStyle.None;
                listDiasDoMesAnterior[i].BackgroundImage = null;
                dia++;
            }

            //Seleciona as data do Mes seguinte
            List<Label> listDiasDoProximoMes = listLabel.Where(f => f.TabIndex >= numeroDias + data).ToList();
            dia = 1;
            foreach (Label label in listDiasDoProximoMes)
            {
                label.Text = dia.ToString();
                label.ForeColor = Color.Silver;
                label.BackColor = Color.Transparent;
                label.BorderStyle = BorderStyle.None;
                label.BackgroundImage = null;
                dia++;
            }
        }        

        /// <summary>
        /// Marca a data selecionada
        /// </summary>
        public void MarcaDataSelecionada()
        {
            while (!listLabel.Exists(f => f.ForeColor != Color.Silver && f.Text == diaSelect))
            {
                diaSelect = (Convert.ToInt32(diaSelect)- 1 ).ToString();
            }            

            listLabel.Find(f => f.ForeColor != Color.Silver && f.Text == diaSelect).BackColor = Color.LightSkyBlue;
            listLabel.Find(f => f.ForeColor != Color.Silver && f.Text == diaSelect).BorderStyle = BorderStyle.FixedSingle;
            listLabel.Find(f => f.ForeColor != Color.Silver && f.Text == diaSelect).BackgroundImage = null;

            if (loadCreate == false)
            {
                Base.RetornaMonthCalendar(identificador).SetDateIsSelected();
                labelDiasUteis.Text = Base.RetornaMonthCalendar(identificador).DiasUteis.ToString();

                //Marca a data de hoje no calendario
                if (Base.RetornaMonthCalendar(identificador).DateIsSelected.Month == Base.RetornaMonthCalendar(identificador).DateNow.Month && Base.RetornaMonthCalendar(identificador).DateIsSelected.Year == Base.RetornaMonthCalendar(identificador).DateNow.Year && Base.RetornaMonthCalendar(identificador).DateIsSelected.Day != Base.RetornaMonthCalendar(identificador).DateNow.Day)
                {
                    MarcaDateNow(Base.RetornaMonthCalendar(identificador).DateNow.Day.ToString());
                }

                Base.RetornaMonthCalendar(identificador).DateChanged(new DateChangedEventArgs());
            }             

            //Descrição os feriados, quando a mais que um utiliza o separador(-)
            string descricaoFeriado = string.Empty;

            #region Marca os feriados fixos no calendario

            List<FeriadosFixo> listFeriados = listFeriadosFixo.Where(f => f.mes ==Base.RetornaMonthCalendar(identificador).DateIsSelected.Month).ToList();
            foreach (FeriadosFixo feriados in listFeriados)
            {
                if (listLabel.Any(f => f.ForeColor == SystemColors.ControlText && f.Text == feriados.dia.ToString()))
                {
                    listLabel.Find(f => f.ForeColor == SystemColors.ControlText && f.Text == feriados.dia.ToString()).ForeColor = Color.Red;
                }
            }

            #endregion

            #region Feriados NÃO fixo: Pascaoa, carnaval e Corpus Christ

            List<FeriadosAlternantes> listFeriadosAlt = null;
            if (Base.RetornaMonthCalendar(identificador).feriadosNaoFixo == true)
            {
                Int32 mesIsSelected =Base.RetornaMonthCalendar(identificador).DateIsSelected.Month;
                if (mesIsSelected == 2 || mesIsSelected == 3 || mesIsSelected == 4 || mesIsSelected == 5 || mesIsSelected == 6)
                {
                    //Todos os feridos nao fixo do mes selecionado
                    listFeriadosAlt = CalculaFeriado(Base.RetornaMonthCalendar(identificador).DateIsSelected.Year.ToString()).Where(f => f.mes == mesIsSelected).ToList();
                    foreach (FeriadosAlternantes feriados in listFeriadosAlt)
                    {
                        if (listLabel.Any(f => f.ForeColor == SystemColors.ControlText && f.Text == feriados.dia.ToString()))
                        {
                            listLabel.Find(f => f.ForeColor == SystemColors.ControlText && f.Text == feriados.dia.ToString()).ForeColor = Color.Red;
                        }
                    }                    
                }
            }

            #endregion         

            #region Verifico se a data selecionada é feriado

            if (listFeriados.Count > 0 || listFeriadosAlt != null)
            {
                DateTime dateIsSelected =Base.RetornaMonthCalendar(identificador).DateIsSelected;
                //Feriados fixos
                if (listFeriados.Count > 0)
                {
                    if (listFeriados.Any(f => f.dia == dateIsSelected.Day && f.mes == dateIsSelected.Month))
                    {
                        foreach (FeriadosFixo feriadosFixo in listFeriados.Where(f => f.dia == dateIsSelected.Day && f.mes == dateIsSelected.Month))
                        {
                            if (descricaoFeriado == string.Empty)
                            {
                                descricaoFeriado = feriadosFixo.nomeFeriado;
                            }
                            else
                            {
                                descricaoFeriado = string.Format("{0}{1}{2}",descricaoFeriado,"-",feriadosFixo.nomeFeriado);
                            }
                        }
                    }
                }
                if (listFeriadosAlt != null && listFeriadosAlt.Count > 0)
                {
                    if (listFeriadosAlt.Any(f => f.dia == dateIsSelected.Day && f.mes == dateIsSelected.Month))
                    {
                        foreach (FeriadosAlternantes feriadosAlt in listFeriadosAlt.Where(f => f.dia == dateIsSelected.Day && f.mes == dateIsSelected.Month))
                        {
                            if (descricaoFeriado == string.Empty)
                            {
                                descricaoFeriado = feriadosAlt.nomeFeriado;
                            }
                            else
                            {
                                descricaoFeriado = string.Format("{0}{1}{2}", descricaoFeriado, "-", feriadosAlt.nomeFeriado);
                            }
                        }
                    }
                }

                //Verifica se a data possui algum feriado para disparar o evento
                if (descricaoFeriado != string.Empty)
                {
                    //Dispara o evento que mostra a descrição do feriado quando o mesmo é selecionado
                    FeriadoChangedEventsArgs e = new FeriadoChangedEventsArgs();
                    e.descricaoFeriado = descricaoFeriado;
                   Base.RetornaMonthCalendar(identificador).FeriadoChanged(e);
                }
            }

            #endregion

            #region Verifica Marcação nos dias do Mês

            DateTime dataIsSelected = Base.RetornaMonthCalendar(identificador).DateIsSelected;
            List<Marcacao> listMarcacaoFiltro = listMarcacao.Where(f => (f.typeFixo == EnumFixo.NaoFixo && f.data.Month == dataIsSelected.Month && f.data.Year == dataIsSelected.Year) || (f.typeFixo == EnumFixo.FixoMensal && Convert.ToDateTime("01/" + dataIsSelected.ToString("MM/yyyy")) >= Convert.ToDateTime("01/" + f.data.ToString("MM/yyyy"))) || (f.typeFixo == EnumFixo.FixoAnual && f.data.Month == dataIsSelected.Month && dataIsSelected.Year >= f.data.Year)).ToList();
            listMarcacaoFiltro.OrderBy(F => F.data);

            if (listMarcacaoFiltro.Count > 0)
            {
                DateTime? dataPesquisa = null;
                foreach (Marcacao marcacao in listMarcacaoFiltro)
                {
                    if (marcacao.data != dataPesquisa)
                    {
                        Int32 qtdMarcacoes = listMarcacaoFiltro.Count(f => f.data == marcacao.data && f.typeFixo == EnumFixo.NaoFixo);
                        Int32 qtdFixoMensal = listMarcacaoFiltro.Count(f => f.typeFixo == EnumFixo.FixoMensal && f.data.Day == marcacao.data.Day);
                        Int32 qtdFixoAnual = listMarcacaoFiltro.Count(f => f.typeFixo == EnumFixo.FixoAnual && f.data.Day == marcacao.data.Day && f.data.Month == marcacao.data.Month);
                        if (listLabel.Exists(f => f.ForeColor != Color.Silver && f.Text == marcacao.data.Day.ToString()))
                        {
                            MarcaDataCompromissos(listLabel.Find(f => f.ForeColor != Color.Silver && f.Text == marcacao.data.Day.ToString()), qtdMarcacoes + qtdFixoAnual + qtdFixoMensal);
                        }
                    }
                    dataPesquisa = marcacao.data;
                }

                if (listLabel.Find(f => f.ForeColor != Color.Silver && f.Text == diaSelect).BackgroundImage != null)
                {
                    listLabel.Find(f => f.ForeColor != Color.Silver && f.Text == diaSelect).BackColor = Color.Transparent;
                }
            }

            #endregion

            loadCreate = false;
        }

        /// <summary>
        /// Add a figura de porcentagem de acordo de compromissos naquela data
        /// </summary>
        /// <param name="qtd">Numero de compromissos</param>
        private void MarcaDataCompromissos(Label labelDia, Int32 qtd)
        {
            if(qtd > 0 && qtd < 4)
            {
                labelDia.BackgroundImage = AERMOD.LIB.Properties.Resources.vinte_cinco;
            }
            else if(qtd >= 4 && qtd < 6)
            {
                labelDia.BackgroundImage = AERMOD.LIB.Properties.Resources.cinquenta;
            }
            else if (qtd >= 6 && qtd < 9)
            {
                labelDia.BackgroundImage = AERMOD.LIB.Properties.Resources.setenta_cinco;
            }
            else if (qtd >= 9)
            {
                labelDia.BackgroundImage = AERMOD.LIB.Properties.Resources.cem;
            }            
        }

        /// <summary>
        /// Retira a seleção do dia
        /// </summary>
        public void LimparSelecao()
        {
            listLabel.Find(f => f.ForeColor != Color.Silver && f.Text == diaSelect).BackColor = Color.Transparent;
            listLabel.Find(f => f.ForeColor != Color.Silver && f.Text == diaSelect).BorderStyle = BorderStyle.None;            
        }

        /// <summary>
        /// Quando o usuario aperta a tecla UP
        /// </summary>
        public void TeclaUp()
        {
            //Não está na primeira linha
            if (listLabel.Find(f => f.BorderStyle == BorderStyle.FixedSingle).TabIndex > 6)
            {               
                Int32 index = listLabel.Find(f => f.BorderStyle == BorderStyle.FixedSingle).TabIndex;
                Base.RetornaClassDias(identificador).LimparSelecao();
                Base.RetornaClassDias(identificador).DiaSelect = listLabel.Find(f => f.TabIndex == index - 7).Text;
                if (listLabel.Find(f => f.TabIndex == index - 7).ForeColor == Color.Silver)
                {
                    Base.RetornaClassMes(identificador).VoltarMes();
                }
                Base.RetornaClassDias(identificador).MarcaDataSelecionada();
            }
        }

        /// <summary>
        /// Quando o usuario aperta a tecla Down
        /// </summary>
        public void TeclaDown()
        {
            //Não está na ultima linha
            if (listLabel.Find(f => f.BorderStyle == BorderStyle.FixedSingle).TabIndex < 35)
            {
                Int32 index = listLabel.Find(f => f.BorderStyle == BorderStyle.FixedSingle).TabIndex;
                Base.RetornaClassDias(identificador).LimparSelecao();
                Base.RetornaClassDias(identificador).DiaSelect = listLabel.Find(f => f.TabIndex == index + 7).Text;
                if (listLabel.Find(f => f.TabIndex == index + 7).ForeColor == Color.Silver)
                {
                    Base.RetornaClassMes(identificador).AvancarMes();
                }
                Base.RetornaClassDias(identificador).MarcaDataSelecionada();
            }
        }

        /// <summary>
        /// Quando o usuario aperta a tecla Left
        /// </summary>
        public void TeclaLeft()
        {
            //Não está no primeiro dia
            if (listLabel.Find(f => f.BorderStyle == BorderStyle.FixedSingle).TabIndex != 0)
            {
                Int32 index = listLabel.Find(f => f.BorderStyle == BorderStyle.FixedSingle).TabIndex;
                Base.RetornaClassDias(identificador).LimparSelecao();
                Base.RetornaClassDias(identificador).DiaSelect = listLabel.Find(f => f.TabIndex == index - 1).Text;
                if (listLabel.Find(f => f.TabIndex == index - 1).ForeColor == Color.Silver)
                {
                    Base.RetornaClassMes(identificador).VoltarMes();
                }
                Base.RetornaClassDias(identificador).MarcaDataSelecionada();
            }
        }

        /// <summary>
        /// Quando o usuario aperta a tecla Right
        /// </summary>
        public void TeclaRight()
        {
            //Não está no ultimo dia
            if (listLabel.Find(f => f.BorderStyle == BorderStyle.FixedSingle).TabIndex != 41)
            {
                Int32 index = listLabel.Find(f => f.BorderStyle == BorderStyle.FixedSingle).TabIndex;
                Base.RetornaClassDias(identificador).LimparSelecao();
                Base.RetornaClassDias(identificador).DiaSelect = listLabel.Find(f => f.TabIndex == index + 1).Text;
                if (listLabel.Find(f => f.TabIndex == index + 1).ForeColor == Color.Silver)
                {
                    Base.RetornaClassMes(identificador).AvancarMes();
                }
                Base.RetornaClassDias(identificador).MarcaDataSelecionada();
            }
        }        

        /// <summary>        
        /// Retorna o numero de dias uteis entre duas datas , sendo a data seleciona menor que a data atual
        /// </summary>
        /// <param name="initialDate">Data inicial</param>
        /// <param name="finalDate">Data Final</param>
        /// <returns>Inteiro</returns>
        public Int32 GetDiasUteisBack(DateTime initialDate, DateTime finalDate)
        {
            Int32 days = 0;
            Int32 daysCount = 0;
            days = initialDate.Subtract(finalDate).Days;

            //Módulo
            if (days < 0)
                days = days * -1;

            for (int i = 1; i <= days; i++)
            {
                Boolean existeFeriado = false;

                finalDate = finalDate.AddDays(-1);

                #region Verifico se o dia não é feriado(FIXO)

                if (listFeriadosFixo.Count > 0)
                {
                    if (listFeriadosFixo.Any(f => f.dia == finalDate.Day && f.mes == finalDate.Month))
                    {
                        if (finalDate.DayOfWeek != DayOfWeek.Sunday && finalDate.DayOfWeek != DayOfWeek.Saturday)
                        {
                            existeFeriado = true;
                        }
                    }
                }

                #endregion

                #region Verifico se o dia não é feriado(Alternante) e se não é um feriado fixo
                if (Base.RetornaMonthCalendar(identificador).feriadosNaoFixo == true && existeFeriado == false)
                {
                    Int32 mesIsSelected = finalDate.Month;
                    if (mesIsSelected == 2 || mesIsSelected == 3 || mesIsSelected == 4 || mesIsSelected == 5 || mesIsSelected == 6)
                    {
                        //Todos os feridos nao fixo do mes selecionado
                        List<FeriadosAlternantes> listFeriadosAlt = CalculaFeriado(finalDate.Year.ToString()).Where(f => f.mes == finalDate.Month && f.dia == finalDate.Day).ToList();
                        foreach (FeriadosAlternantes feriados in listFeriadosAlt)
                        {
                            if (finalDate.DayOfWeek != DayOfWeek.Sunday && finalDate.DayOfWeek != DayOfWeek.Saturday)
                            {
                                existeFeriado = true;
                            }
                        }
                    }
                }

                #endregion

                //Se a data não for feriado é contado como dia util
                if (existeFeriado == false)
                {
                    //Conta apenas dias da semana.
                    if (finalDate.DayOfWeek != DayOfWeek.Sunday && finalDate.DayOfWeek != DayOfWeek.Saturday)
                        daysCount++;
                }

            }
            return daysCount;            
        }

        /// <summary>        
        /// Retorna o numero de dias uteis entre duas datas , sendo a data seleciona maior que a data atual
        /// </summary>
        /// <param name="initialDate">Data inicial</param>
        /// <param name="finalDate">Data Final</param>
        /// <returns>Inteiro</returns>
        public Int32 GetDiasUteisNext(DateTime initialDate, DateTime finalDate)
        {           
            Int32 days = 0;
            Int32 daysCount = 0;
            days = initialDate.Subtract(finalDate).Days;

            //Módulo
            if (days < 0)
                days = days * -1;

            for (int i = 1; i <= days; i++)
            {
                Boolean existeFeriado = false;

                initialDate = initialDate.AddDays(1);

                #region Verifico se o dia não é feriado(FIXO)

                if (listFeriadosFixo.Count > 0)
                {
                    if (listFeriadosFixo.Any(f => f.dia == initialDate.Day && f.mes == initialDate.Month))
                    {
                        if (initialDate.DayOfWeek != DayOfWeek.Sunday && initialDate.DayOfWeek != DayOfWeek.Saturday)
                        {
                            existeFeriado = true;
                        }
                    }
                }

                #endregion

                #region Verifico se o dia não é feriado(Alternante) e se não é um feriado fixo

                if (Base.RetornaMonthCalendar(identificador).feriadosNaoFixo == true && existeFeriado == false)
                {
                    Int32 mesIsSelected = initialDate.Month;
                    if (mesIsSelected == 2 || mesIsSelected == 3 || mesIsSelected == 4 || mesIsSelected == 5 || mesIsSelected == 6)
                    {
                        //Todos os feridos nao fixo do mes selecionado
                        List<FeriadosAlternantes> listFeriadosAlt = CalculaFeriado(initialDate.Year.ToString()).Where(f => f.mes == initialDate.Month && f.dia == initialDate.Day).ToList();
                        foreach (FeriadosAlternantes feriados in listFeriadosAlt)
                        {
                            if (initialDate.DayOfWeek != DayOfWeek.Sunday && initialDate.DayOfWeek != DayOfWeek.Saturday)
                            {
                                existeFeriado = true;
                            }
                        }
                    }
                }

                #endregion

                //Se a data não for feriado é contado como dia util
                if (existeFeriado == false)
                {
                    //Conta apenas dias da semana.
                    if (initialDate.DayOfWeek != DayOfWeek.Sunday && initialDate.DayOfWeek != DayOfWeek.Saturday)
                        daysCount++;
                }
            }
            return daysCount;
        }

        /// <summary>
        /// Marca a data de hoje no calendario
        /// </summary>
        /// <param name="hoje">Dia de hj</param>
        public void MarcaDateNow(string hoje)
        {
            listLabel.Find(f => f.ForeColor != Color.Silver && f.Text == hoje).BorderStyle = BorderStyle.Fixed3D;
        }

        /// <summary>
        /// Adiciona o feriado a lista de feriados
        /// </summary>
        /// <param name="dia">dia do feriado</param>
        /// <param name="mes">Mes do feriado</param>
        /// <param name="nomeFeriado">Descrição do feriado</param>
        public void AddFeriadosFixo(Int32 dia, Int32 mes,string nomeFeriado)
        {
            FeriadosFixo feriados = new FeriadosFixo();
            feriados.dia = dia;
            feriados.mes = mes;
            feriados.nomeFeriado = nomeFeriado;
            listFeriadosFixo.Add(feriados);
        }

        /// <summary>
        /// Remove o feriado
        /// </summary>
        /// <param name="dia">dia do feriado</param>
        /// <param name="mes">Mes do feriado</param>
        public void RemoverFeriado(Int32 dia, Int32 mes)
        {
            listFeriadosFixo.RemoveAll(f => f.dia == dia && f.mes == mes);
        }

        /// <summary>
        /// Remove todos os feriados cadastrados
        /// </summary>
        public void RemoverTodosFeriados()
        {
            listFeriadosFixo.Clear();
        }

        /// <summary>
        /// Calculo da Pascoa,Carnaval e Corpus Christ 
        /// </summary>
        /// <param name="Ano">Ano dos feriados</param>
        /// <returns>Retorna os feriados</returns>
        public List<FeriadosAlternantes> CalculaFeriado(string Ano)
        {            
            List<FeriadosAlternantes> listFeriadosAlter = new List<FeriadosAlternantes>();

            FeriadosAlternantes feriadoPascoa = new FeriadosAlternantes();
            FeriadosAlternantes feriadoCarnaval = new FeriadosAlternantes();
            FeriadosAlternantes feriadoCorpusCrist = new FeriadosAlternantes();

            int ano = Convert.ToInt32(Ano.Substring(0, 4));
            int x, y;
            int a, b, c, d, e;
            int dia, mes;

            DateTime pascoa, carnaval, corpus;

            if (ano >= 1900 & ano <= 2099)
            {
                x = 24;
                y = 5;
            }
            else
                if (ano >= 2100 & ano <= 2199)
                {
                    x = 24;
                    y = 6;
                }
                else
                    if (ano >= 2200 & ano <= 2299)
                    {
                        x = 25;
                        y = 7;
                    }
                    else
                    {
                        x = 24;
                        y = 5;
                    }


            a = ano % 19;
            b = ano % 4;
            c = ano % 7;
            d = (19 * a + x) % 30;
            e = (2 * b + 4 * c + 6 * d + y) % 7;


            if ((d + e) > 9)
            {
                dia = (d + e - 9);
                mes = 4;
            }
            else
            {
                dia = (d + e + 22);
                mes = 3;
            }


            // PASCOA
            feriadoPascoa.dia = dia;
            feriadoPascoa.mes = mes;
            feriadoPascoa.ano = ano;
            feriadoPascoa.nomeFeriado = "Pascoa";
            pascoa = Convert.ToDateTime((Convert.ToString(dia) + "/" + Convert.ToString(mes) + "/" + ano));
            listFeriadosAlter.Add(feriadoPascoa);
            //Data[0] = pascoa;

            // CARNAVAL ( PASCOA - 47 dias )
            carnaval = pascoa.AddDays(-47);
            feriadoCarnaval.dia = carnaval.Day;
            feriadoCarnaval.mes = carnaval.Month;
            feriadoCarnaval.ano = carnaval.Year;
            feriadoCarnaval.nomeFeriado = "Carnaval";
            listFeriadosAlter.Add(feriadoCarnaval);
            //Data[1] = carnaval;

            // CORPUS CHRISTI ( PASCOA + 60 dias )
            corpus = pascoa.AddDays(60);
            feriadoCorpusCrist.dia = corpus.Day;
            feriadoCorpusCrist.mes = corpus.Month;
            feriadoCorpusCrist.ano = corpus.Year;
            feriadoCorpusCrist.nomeFeriado = "Corpus Christi";
            listFeriadosAlter.Add(feriadoCorpusCrist);
            //Data[2] = corpus;           

            return listFeriadosAlter;

        }       

        /// <summary>
        /// Adiciona marcacao a respectiva data
        /// </summary>
        /// <param name="data">Data da marcacao</param>
        /// <param name="fixar">Fixar True Não fixar False</param>
        public void AddMarcacao(DateTime data,EnumFixo typeFixo)
        {
            Marcacao marcacao = new Marcacao();
            marcacao.data = data;
            marcacao.typeFixo = typeFixo;

            listMarcacao.Add(marcacao);
        }       

        /// <summary>
        /// Remove uma marcação de determinada data
        /// </summary>
        /// <param name="data">Data da marcação</param>
        public void RemoverMarcacao(DateTime data)
        {
            if (listMarcacao.Any(f => f.data == data))
            {
                listMarcacao.Remove(listMarcacao.First(f => f.data == data));
                CarregaDias(Base.RetornaClassMes(identificador).MesExtenso(Base.RetornaMonthCalendar(identificador).DateIsSelected.Month), Base.RetornaMonthCalendar(identificador).DateIsSelected.Year);
                loadCreate = true;
                MarcaDataSelecionada();
            }
        }

        /// <summary>
        /// Remove todas as marcações cadastradas
        /// </summary>
        public void RemoverTodasMarcacoes()
        {
            listMarcacao.Clear();
            CarregaDias(Base.RetornaClassMes(identificador).MesExtenso(Base.RetornaMonthCalendar(identificador).DateIsSelected.Month), Base.RetornaMonthCalendar(identificador).DateIsSelected.Year);
            loadCreate = true;
            MarcaDataSelecionada();
        }        
    }
}
