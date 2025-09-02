using AERMOD.CamadaApresentacao;
using CamadaAcessoDados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AERMOD
{
    public static class Base
    {
        /// <summary>
        /// Configuração da Rede.
        /// </summary>
        public static ConfiguracaoRede ConfiguracaoRede { get; set; }

        public static bool VerificaVersao { get; set; }
    }
}
