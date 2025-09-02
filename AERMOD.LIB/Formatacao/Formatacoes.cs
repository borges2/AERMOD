using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AERMOD.LIB.Formatacao
{
    public static class Formatacoes
    {
        #region DeFomart

        /// <summary>
        /// Retira a formatação da String.
        /// </summary>
        /// <param name="x">String com formatação</param>
        /// <returns>Retorna STRING sem nemhum formato.</returns>
        public static String DeFormat(this string x)
        {
            if (String.IsNullOrEmpty(x))
                return "";

            x = x.Replace(".", "").Replace("/", "").Replace("-", "").Replace("(", "").Replace(")", "").Replace(":", "").Replace(",", "").Replace(" ", "").Trim();
            return x;
        }

        #endregion

        # region Concatenar

        public static String Concatenar(this IEnumerable<String> arrayString, string separador, string ultimoSeparador = null)
        {
            List<String> lstString = new List<string>(arrayString);

            Int32 count = lstString.Count;

            if (count == 0)
                return "";

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < count; i++)
            {
                sb.Append(lstString[i]);

                if (i < count - 1)
                {
                    if (ultimoSeparador != null && i == count - 2)
                    {
                        sb.Append(ultimoSeparador);
                    }
                    else
                    {
                        sb.Append(separador);
                    }


                }
            }

            return sb.ToString();
        }

        # endregion
    }
}
