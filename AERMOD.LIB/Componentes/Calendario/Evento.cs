using System;
using System.ComponentModel;

namespace Netsof.LIB.Componentes.Calendario
{
    public class Evento
    {
        public int Codigo_Evento { get; set; }

        public string Titulo { get; set; }

        public string Descricao { get; set; }

        public DateTime Data_Hora { get; set; }

        public Fixar Fixar { get; set; }

        public bool Postergar { get; set; }

        public bool Aviso_Sonoro { get; set; }

        public bool Aviso_Email { get; set; }

        public bool Aviso_Sms { get; set; }

        public bool Avisado { get; set; }

        public int Dias_Antecipar { get; set; }

        public int duplicar = 1;
    }

    public class EventoExclusao
    {
        public int Codigo_Evento { get; set; }

        public DateTime DATA_EXCLUSAO { get; set; }
    }

    public enum Fixar
    {
        [Description("Não fixar")]
        Nao_fixar,
        [Description("Mensal")]
        Mensal,
        [Description("Anual")]
        Anual
    }
}
