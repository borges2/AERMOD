using System;

namespace Netsof.LIB.Componentes.Calendario.Classes
{
    public class FeriadosAlternantes
    {
        public Int32 dia { get; set; }
        public Int32 mes { get; set; }
        public Int32 ano { get; set; }
        public string nomeFeriado { get; set; }

        public DateTime GetData
        {
            get { return Convert.ToDateTime(string.Format("{0}/{1}/{2}", dia.ToString().PadLeft(2, '0'), mes.ToString().PadLeft(2, '0'), ano.ToString().PadLeft(4, '0'))); }
        }
    }
}
