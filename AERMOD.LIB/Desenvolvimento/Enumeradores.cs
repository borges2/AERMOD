using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AERMOD.LIB.Desenvolvimento
{
    public class Enumeradores
    {
        /// <summary>
        /// Retorna fuso horário.
        /// </summary>
        /// <returns>Retorna DataTable</returns>
        public static DataTable RetornarFusoHorario()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CODIGO", typeof(int));
            dt.Columns.Add("DESCRICAO", typeof(string));

            foreach (FusoHorario item in Enum.GetValues(typeof(FusoHorario)))
            {
                dt.Rows.Add((int)item, Funcoes.GetEnumDescription(item));
            }

            return dt;           
        }

        /// <summary>
        /// Retorna o tipo da fonte.
        /// </summary>
        /// <returns>Retorna DataTable</returns>
        public static DataTable RetornarTipoFonte()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CODIGO", typeof(int));
            dt.Columns.Add("DESCRICAO", typeof(string));

            foreach (TipoFonte item in Enum.GetValues(typeof(TipoFonte)))
            {
                dt.Rows.Add((int)item, Funcoes.GetEnumDescription(item));
            }

            return dt;
        }

        /// <summary>
        /// Retorna a frequência do setor.
        /// </summary>
        /// <returns>Retorna DataTable</returns>
        public static DataTable RetornarFrequenciaSetor()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CODIGO", typeof(int));
            dt.Columns.Add("DESCRICAO", typeof(string));

            foreach (FrequenciaSetor item in Enum.GetValues(typeof(FrequenciaSetor)))
            {
                dt.Rows.Add((int)item, Funcoes.GetEnumDescription(item));
            }

            return dt;
        }

        /// <summary>
        /// Retorna a frequência do setor.
        /// </summary>
        /// <returns>Retorna DataTable</returns>
        public static DataTable RetornarEstacao()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CODIGO", typeof(int));
            dt.Columns.Add("DESCRICAO", typeof(string));

            foreach (Estacao item in Enum.GetValues(typeof(Estacao)))
            {
                dt.Rows.Add((int)item, Funcoes.GetEnumDescription(item));
            }

            return dt;
        }

        /// <summary>
        /// Retorna poluentes.
        /// </summary>
        /// <returns>Retorna DataTable</returns>
        public static DataTable RetornarPoluentes()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CODIGO", typeof(int));
            dt.Columns.Add("DESCRICAO", typeof(string));

            foreach (Poluentes item in Enum.GetValues(typeof(Poluentes)))
            {
                dt.Rows.Add((int)item, Funcoes.GetEnumDescription(item));
            }

            return dt;
        }

        /// <summary>
        /// Retorna média horária.
        /// </summary>
        /// <returns>Retorna DataTable</returns>
        public static DataTable RetornarMediaHoraria()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CODIGO", typeof(int));
            dt.Columns.Add("DESCRICAO", typeof(string));

            foreach (MediaHoraria item in Enum.GetValues(typeof(MediaHoraria)))
            {
                dt.Rows.Add((int)item, Funcoes.GetEnumDescription(item));
            }

            return dt;
        }

        /// <summary>
        /// Retorna média do período.
        /// </summary>
        /// <returns>Retorna DataTable</returns>
        public static DataTable RetornarMediaPeriodo()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CODIGO", typeof(int));
            dt.Columns.Add("DESCRICAO", typeof(string));

            foreach (MediaPeriodo item in Enum.GetValues(typeof(MediaPeriodo)))
            {
                dt.Rows.Add((int)item, Funcoes.GetEnumDescription(item));
            }

            return dt;
        }

        /// <summary>
        /// Retorna tipo de saída.
        /// </summary>
        /// <returns>Retorna DataTable</returns>
        public static DataTable RetornarTipoSaida()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CODIGO", typeof(int));
            dt.Columns.Add("DESCRICAO", typeof(string));

            foreach (TipoSaida item in Enum.GetValues(typeof(TipoSaida)))
            {
                dt.Rows.Add((int)item, Funcoes.GetEnumDescription(item));
            }

            return dt;
        }

        /// <summary>
        /// Retorna critério do receptor.
        /// </summary>
        /// <returns>Retorna DataTable</returns>
        public static DataTable RetornarCriterioReceptor()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CODIGO", typeof(int));
            dt.Columns.Add("DESCRICAO", typeof(string));

            foreach (CriterioReceptor item in Enum.GetValues(typeof(CriterioReceptor)))
            {
                dt.Rows.Add((int)item, Funcoes.GetEnumDescription(item));
            }

            return dt;
        }

        /// <summary>
        /// Retorna UF.
        /// </summary>
        /// <returns>Retorna DataTable</returns>
        public static DataTable RetornarUF()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CODIGO", typeof(int));
            dt.Columns.Add("DESCRICAO", typeof(string));

            foreach (UF item in Enum.GetValues(typeof(UF)))
            {
                dt.Rows.Add((int)item, Funcoes.GetEnumDescription(item));
            }

            return dt;
        }
    }

    /// <summary>
    /// Fuso horário.
    /// </summary>
    public enum FusoHorario
    {
        [Description("UTC+0 (GMT)")]
        UTC_GMT,

        [Description("UTC-3 (Brasília)")]
        UTC_BRASILIA
    }

    /// <summary>
    /// Tipo do arquivo.
    /// </summary>
    public enum TipoArquivo
    {
        [Description("Arquivo (.SAM)")]
        SAM,

        [Description("Arquivo (.FSL)")]
        FSL,

        [Description("Arquivo (.TIF)")]
        TIF,

        [Description("Arquivo (.PLT)")]
        PLT
    }

    public enum TipoFonte
    {
        [Description("POINT")]
        PONTO,

        [Description("AREA")]
        AREA,

        [Description("AREAPOLY")]
        AREAPOLY,

        [Description("AREACIRC")]
        AREACIRC,

        [Description("VOLUME")]
        VOLUME,

        [Description("LINE")]
        LINE,

        [Description("OPENPIT")]
        OPENPIT
    }

    public enum FrequenciaSetor
    {
        [Description("ANUAL")]
        ANUAL,

        [Description("SAZONAL")]
        SAZONAL,

        [Description("MENSAL")]
        MENSAL
    }

    public enum Estacao
    {
        [Description("INVERNO")]
        INVERNO = 1,

        [Description("PRIMAVERA")]
        PRIMAVERA = 2,

        [Description("VERÃO")]
        VERAO = 3,

        [Description("OUTONO")]
        OUTONO = 4
    }

    public enum Poluentes
    {
        [Description("Total de particulas suspensas")]
        TSP = 1,

        [Description("PM10")]
        PM10 = 2,

        [Description("PM25")]
        PM25 = 3,

        [Description("PM")]
        PM = 4,

        [Description("Chumbo")]
        PB = 5,

        [Description("Dióxido de enxofre")]
        SO2 = 6,

        [Description("Enxofre Reduzido Total")]
        ERT = 7,

        [Description("Fumaça")]
        FMC = 8,

        [Description("Óxidos de enxofre")]
        SOX = 9,

        [Description("Óxidos de nitrogênio")]
        NOX = 10,

        [Description("Dióxido de nitrogênio")]
        NO2 = 11,

        [Description("Monóxido de nitrogênio")]
        NO = 12,

        [Description("Monóxido de carbono")]
        CO = 13,

        [Description("Dióxido de carbono")]
        CO2 = 14,

        [Description("Metano")]
        CH4 = 15,

        [Description("Hidrocarbonetos")]
        CxHy = 16,

        [Description("Acetona - CH3(CO)CH3")]
        CH3COCH3 = 17,

        [Description("Benzeno")]
        C6H6 = 18,

        [Description("Formaldeído")]
        H2CO = 19,

        [Description("Naftaleno")]
        C10H8 = 20,

        [Description("Alumínio")]
        Al = 21,

        [Description("Cálcio")]
        Ca = 22,

        [Description("Cloreto - Cl-1")]
        Cl_1 = 23,

        [Description("Carbono")]
        C = 24,

        [Description("Ferro")]
        Fe = 25,

        [Description("Potássio")]
        K = 26,

        [Description("Magnésio")]
        Mg = 27,

        [Description("Manganês")]
        Mn = 28,

        [Description("Sódio")]
        Na = 29,

        [Description("Ozônio")]
        O3 = 30,

        [Description("Nitrato")]
        NO3 = 31,

        [Description("Propano")]
        C3H8 = 32,

        [Description("Dióxido de silício")]
        SiO2 = 33,

        [Description("Tolueno")]
        C7H8 = 34,

        [Description("Xileno")]
        C8H10 = 35,

        [Description("Compostos orgânicos voláteis")]
        COVS = 36
    }

    public enum MediaHoraria
    {
        [Description("1")]
        UM,

        [Description("2")]
        DOIS,

        [Description("3")]
        TRES,

        [Description("4")]
        QUATRO,

        [Description("6")]
        SEIS,

        [Description("8")]
        OITO,

        [Description("12")]
        DOZE,

        [Description("24")]
        VINTE_E_QUATRO
    }

    public enum MediaPeriodo
    {
        [Description("Período")]
        PERIOD,

        [Description("Anual")]
        ANNUAL,

        [Description("Mensal")]
        MONTH
    }

    public enum TipoSaida
    {
        [Description("MAXIFILE")]
        MAXIFILE,

        [Description("PLOTFILE")]
        PLOTFILE,

        [Description("POSTFILE")]
        POSTFILE,

        [Description("RANKFILE")]
        RANKFILE
    }

    public enum CriterioReceptor
    {
        [Description("PRIMEIRO")]
        FIRST,

        [Description("SEGUNDO")]
        SECOND
    }

    public enum UF
    {
        [Description("RO")]
        RO = 11,
        [Description("AC")]
        AC = 12,
        [Description("AM")]
        AM = 13,
        [Description("RR")]
        RR = 14,
        [Description("PA")]
        PA = 15,
        [Description("AP")]
        AP = 16,
        [Description("TO")]
        TO = 17,
        [Description("MA")]
        MA = 21,
        [Description("PI")]
        PI = 22,
        [Description("CE")]
        CE = 23,
        [Description("RN")]
        RN = 24,
        [Description("PB")]
        PB = 25,
        [Description("PE")]
        PE = 26,
        [Description("AL")]
        AL = 27,
        [Description("SE")]
        SE = 28,
        [Description("BA")]
        BA = 29,
        [Description("MG")]
        MG = 31,
        [Description("ES")]
        ES = 32,
        [Description("RJ")]
        RJ = 33,
        [Description("SP")]
        SP = 35,
        [Description("PR")]
        PR = 41,
        [Description("SC")]
        SC = 42,
        [Description("RS")]
        RS = 43,
        [Description("MS")]
        MS = 50,
        [Description("MT")]
        MT = 51,
        [Description("GO")]
        GO = 52,
        [Description("DF")]
        DF = 53        
    }

    public enum TipoGrade
    {
        [Description("CARTESIANO")]
        CARTESIANO,

        [Description("CARTESIANO ELEVAÇÃO/ALTITUDE")]
        CARTESIANO_ELEVACAO,

        [Description("CARTESIANO DISCRETO")]
        CARTESIANO_DISCRETO,

        [Description("CARTESIANO DISCRETO EVALFILE")]
        CARTESIANO_EVALFILE
    }
}
