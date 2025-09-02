using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Netsof.LIB.Componentes.Calendario
{
    public class CalendarCell
    {
        public List<Evento> listEventos = new List<Evento>();

        public Rectangle Bounds { get; set; }

        public DateTime? Date { get; set; }

        public object Tag { get; set; }

        public bool IsActive { get; set; }

        public int Row { get; set; }

        public string Column { get; set; }

        public int ColumnIndex
        {
            get
            {
                return string.IsNullOrEmpty(Column) == true ? -1 : Convert.ToInt32(Column.Substring(Column.Length - 1, 1));
            }
         }

        internal void AddEvento(Evento evento)
        {
            if (listEventos.Any(f => f.Codigo_Evento == evento.Codigo_Evento) == false)
            {
                listEventos.Add(evento);
                listEventos.Sort(delegate (Evento ps1, Evento ps2)
                {
                    return DateTime.Compare(new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month, DateTime.MinValue.Day, ps1.Data_Hora.Hour, ps1.Data_Hora.Minute, 0),
                                            new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month, DateTime.MinValue.Day, ps2.Data_Hora.Hour, ps2.Data_Hora.Minute, 0));
                });
            }
        }

        internal void RemoverEvento(Evento evento)
        {
            listEventos.RemoveAll(f => f.Codigo_Evento == evento.Codigo_Evento);
        }

        internal void Clear()
        {
            listEventos.Clear();
        }
    }
}
