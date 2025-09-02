using System.Collections.Generic;

namespace Netsof.LIB.Componentes.Calendario.Classes
{
    internal static class Base
    {
        private static List<Calendario> listMonthCalendar = new List<Calendario>();

        /// <summary>
        /// Adiciona o componente criado a lista
        /// </summary>
        /// <param name="monthCalendar">This</param>
        /// <param name="classDias">Instancia da classe Dias</param>
        /// <param name="classAno">Instancia da classe Ano</param>
        /// <param name="classMes">Instancia da classe Mes</param>
        public static void AddMonthCalendar(MonthCalendarStyleLIB monthCalendar, Dias classDias, Ano classAno, Mes classMes)
        {
            Calendario calendario = new Calendario();
            calendario.MonthCalendarLIBBase = monthCalendar;
            calendario.classDias = classDias;
            calendario.classAno = classAno;
            calendario.classMes = classMes;
            listMonthCalendar.Add(calendario);
        }

        /// <summary>
        /// Retorna o componente calendario
        /// </summary>
        /// <param name="ident">Componente identificado</param>
        /// <returns></returns>
        public static MonthCalendarStyleLIB RetornaMonthCalendar(MonthCalendarStyleLIB ident)
        {
            return listMonthCalendar.Find(f => f.MonthCalendarLIBBase == ident).MonthCalendarLIBBase;
        }

        /// <summary>
        /// Retorna a classe Dias
        /// </summary>
        /// <param name="ident">Componente identificado</param>
        /// <returns></returns>
        public static Dias RetornaClassDias(MonthCalendarStyleLIB ident)
        {
            return listMonthCalendar.Find(f => f.MonthCalendarLIBBase == ident).classDias;
        }

        /// <summary>
        /// Retorna a classe Mes
        /// </summary>
        /// <param name="ident">Componente identificado</param>
        /// <returns></returns>
        public static Mes RetornaClassMes(MonthCalendarStyleLIB ident)
        {
            return listMonthCalendar.Find(f => f.MonthCalendarLIBBase == ident).classMes;
        }

        /// <summary>
        /// Retorna a classe Ano
        /// </summary>
        /// <param name="ident">Componente identificado</param>
        /// <returns></returns>
        public static Ano RetornaClassAno(MonthCalendarStyleLIB ident)
        {
            return listMonthCalendar.Find(f => f.MonthCalendarLIBBase == ident).classAno;
        }

        /// <summary>
        /// Remove o componente da lista quando o mesmo é destruido
        /// </summary>
        /// <param name="ident">Componente identificado</param>
        public static void RemoverMonthCalendar(MonthCalendarStyleLIB ident)
        {
            listMonthCalendar.Remove(listMonthCalendar.Find(f => f.MonthCalendarLIBBase == ident));
        }
    }

    public class Calendario
    {
        public MonthCalendarStyleLIB MonthCalendarLIBBase { get; set; }
        public Dias classDias { get; set; }
        public Ano classAno { get; set; }
        public Mes classMes { get; set; }
    }
}
