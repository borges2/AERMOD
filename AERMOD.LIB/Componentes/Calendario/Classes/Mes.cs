using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;

namespace Netsof.LIB.Componentes.Calendario.Classes
{
    public class Mes
    {
         MonthCalendarStyleLIB identificador = null;

        public Mes(MonthCalendarStyleLIB ident)
        {
            identificador = ident;
        }

        List<Control> listControles = new List<Control>();        

        /// <summary>
        /// Adiciona os controles utilizado no Mês
        /// </summary>
        /// <param name="controles"></param>
        public void AddControles(Control[] controles)
        {
            listControles.AddRange(controles);
        }
        
        /// <summary>
        /// Retorna o nome do mes de acordo com sua numeração
        /// </summary>
        /// <param name="i">Número do Mes</param>
        /// <returns>Retorna uma string</returns>
        public String MesExtenso(int i)
        {
            CultureInfo culture = new CultureInfo("pt-BR");
            DateTimeFormatInfo dtfi = culture.DateTimeFormat;
            return culture.TextInfo.ToTitleCase(dtfi.GetMonthName(i));
        }

        /// <summary>
        /// Ação realizada pelo botâo voltar do mês
        /// </summary>        
        public void VoltarMes()
        {
            if (DateTime.ParseExact(listControles.Find(f => f.Name == "lbMes").Text, "MMMM", new CultureInfo("pt-BR")).Month == 1)
            {
                listControles.Find(f => f.Name == "lbMes").Text = MesExtenso(12);
                if (listControles.Find(f => f.Name == "lbAno").Text != "1753")
                    listControles.Find(f => f.Name == "lbAno").Text = (Convert.ToInt32(listControles.Find(f => f.Name == "lbAno").Text) - 1).ToString();
            }
            else
                listControles.Find(f => f.Name == "lbMes").Text = MesExtenso(DateTime.ParseExact(listControles.Find(f => f.Name == "lbMes").Text, "MMMM", new CultureInfo("pt-BR")).Month - 1);
            Base.RetornaClassDias(identificador).CarregaDias(listControles.Find(f => f.Name == "lbMes").Text, Convert.ToInt32(listControles.Find(f => f.Name == "lbAno").Text));

            Base.RetornaClassDias(identificador).MarcaDataSelecionada();
        }

        /// <summary>
        /// Ação realizada pelo botão avançar mês
        /// </summary>
        public void AvancarMes()
        {
            if (DateTime.ParseExact(listControles.Find(f => f.Name == "lbMes").Text, "MMMM", new CultureInfo("pt-BR")).Month == 12)
            {
                listControles.Find(f => f.Name == "lbMes").Text = MesExtenso(1);
                if (listControles.Find(f => f.Name == "lbAno").Text != "2099")
                    listControles.Find(f => f.Name == "lbAno").Text = (Convert.ToInt32(listControles.Find(f => f.Name == "lbAno").Text) + 1).ToString();
            }
            else
                listControles.Find(f => f.Name == "lbMes").Text = MesExtenso(DateTime.ParseExact(listControles.Find(f => f.Name == "lbMes").Text, "MMMM", new CultureInfo("pt-BR")).Month + 1);
            Base.RetornaClassDias(identificador).CarregaDias(listControles.Find(f => f.Name == "lbMes").Text, Convert.ToInt32(listControles.Find(f => f.Name == "lbAno").Text));

            Base.RetornaClassDias(identificador).MarcaDataSelecionada();
        }
    }
}
