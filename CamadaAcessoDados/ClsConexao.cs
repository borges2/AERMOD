using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CamadaAcessoDados
{
    public static class ClsConexao
    {
        public static List<String> TestarConexao(ConfiguracaoRede configuracaoRede)
        {
            List<String> Retorno = null;            

            ClsDados clsDados = new ClsDados(configuracaoRede);
            clsDados.TestarConexao();

            if (clsDados.Status == StatusConnection.ON)
            {
                Retorno = new List<string>();
                DataTable databases = clsDados.RetornarDataTable("SHOW DATABASES");
                databases.Rows.Cast<DataRow>().ToList().ForEach(delegate (DataRow Linha) { if (Linha[0].ToString() != "information_schema" && Linha[0].ToString() != "performance_schema" && Linha[0].ToString() != "mysql") Retorno.Add(Linha[0].ToString()); });
            }

            return Retorno;
        }

        public static string ConnnectionString(ConfiguracaoRede configuracaoRede)
        {
            //cfgRede.Hostname_Servidor = "localhost";
            //cfgRede.DB_Database = "pafecf";
            //cfgRede.Salvar();
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Data Source = {0};", configuracaoRede.Hostname);
            sb.AppendFormat("Port = {0};", configuracaoRede.Porta);
            sb.AppendFormat("User Id = {0};", configuracaoRede.Usuario);
            sb.AppendFormat("Password = {0};", configuracaoRede.Senha);
            sb.AppendFormat("Database = {0};", configuracaoRede.Database);
            //sb.AppendFormat("Respect Binary Flags=False;");
            sb.Append("Default Command Timeout = 3600;");
            sb.AppendFormat("Connection Timeout = 5;");
            sb.AppendFormat("Connection Lifetime = 30;");
            sb.AppendFormat("Keep Alive = 10;");
            //sb.AppendFormat("Connection Reset = False;");
            sb.AppendFormat("Logging = False;");
            sb.AppendFormat("Pooling = True;");
            sb.AppendFormat("Maximum Pool Size = 100;");
            sb.AppendFormat("Minimum Pool Size = 5;");

            return sb.ToString();
        }
    }
}
