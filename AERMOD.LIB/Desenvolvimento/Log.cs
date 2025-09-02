using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AERMOD.LIB.Desenvolvimento
{
    public static class Log
    {
        private static System.Threading.Semaphore semaforo = new System.Threading.Semaphore(1, 1);
        public static Boolean LogHabilitado = true;
        public static Boolean LogDetalhado = true;

        public static void Escrever(string tipo, String txt, Int32 paddingLevel = 0, Boolean importante = true)
        {
            if (LogDetalhado == false && importante == false)
            {
                return;
            }

            semaforo.WaitOne();

            var leftPadding = new String(' ', paddingLevel * 4);

            String diretorio = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "Logs");

            if (Directory.Exists(diretorio) == false)
                Directory.CreateDirectory(diretorio);

            FileStream arquivo = File.Open(Path.Combine(diretorio, String.Format("{0}.txt", DateTime.Now.ToString("yyyy-MM-dd"))), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Delete);
            TextWriter escritor = new StreamWriter(arquivo);
            arquivo.Position = arquivo.Length;
            string[] linhas = txt.Split(new String[] { System.Environment.NewLine }, StringSplitOptions.None);
            foreach (string linha in linhas)
            {
                escritor.WriteLine(String.Format("[{0}][{1}]: {2}{3}", DateTime.Now.ToString(), tipo, leftPadding, linha));
            }

            escritor.Flush();
            arquivo.Flush();
            Win32.FlushFileBuffers(arquivo.SafeFileHandle);
            escritor.Close();
            arquivo.Close();
            arquivo.Dispose();

            semaforo.Release();
        }

        public static void EscreverException(String tipo, Exception x, Int32 paddingLevel = 0)
        {
            if (paddingLevel > 0)
            {
                Escrever(tipo, "INNER EXCEPTION: " + x.GetType().FullName, paddingLevel);
            }
            else
            {
                Escrever(tipo, "EXCEPTION: " + x.GetType().FullName);
            }

            Escrever(tipo, "----------------------------- INICIO ----------------------------");
            Escrever(tipo, "MESSAGE: ", paddingLevel);
            Escrever(tipo, x.Message, paddingLevel);
            Escrever(tipo, "");
            Escrever(tipo, "STACK TRACE: ", paddingLevel);
            Escrever(tipo, x.StackTrace, paddingLevel);

            if (x.InnerException != null)
            {
                EscreverException(tipo, x.InnerException, paddingLevel + 1);
            }

            Escrever(tipo, "------------------------------ FIM ------------------------------");


        }

        public static void EscreverObjeto<T>(String tipo, String txt, T objeto, Int32 paddingLevel = 0, Boolean importante = true)
        {
            if (LogDetalhado == false && importante == false)
            {
                return;
            }

            var campos = typeof(T).GetFields();

            var sb = new StringBuilder();

            foreach (var campoAtual in campos)
            {
                if (campoAtual.FieldType.Module.ScopeName != "CommonLanguageRuntimeLibrary")
                {
                    var extrairDadosClasse = (Func<FieldInfo, object, String>)null;

                    extrairDadosClasse = (Func<FieldInfo, object, String>)((FieldInfo fieldInfo, object objetoClasse) =>
                    {
                        var camposClasse = fieldInfo.FieldType.GetFields();

                        var sbClasse = new StringBuilder();

                        foreach (var campoClasseAtual in camposClasse)
                        {
                            if (campoClasseAtual.FieldType.Module.ScopeName != "CommonLanguageRuntimeLibrary")
                            {
                                sbClasse.AppendFormat("{0} = '{1}', ", campoClasseAtual.Name, extrairDadosClasse.Invoke(campoClasseAtual, objetoClasse));
                            }
                            else
                            {
                                sbClasse.AppendFormat("{0} = '{1}', ", campoClasseAtual.Name, campoClasseAtual.GetValue(fieldInfo.GetValue(objetoClasse)));
                            }
                        }

                        return String.Format("[{0}]", sbClasse.ToString().TrimEnd(',', ' '));
                    });

                    sb.AppendFormat("{0} = {1}, ", campoAtual.Name, extrairDadosClasse.Invoke(campoAtual, objeto));
                }
                else
                {
                    sb.AppendFormat("{0} = '{1}', ", campoAtual.Name, campoAtual.GetValue(objeto));
                }
            }

            Escrever(tipo, String.Format("{0} [{1}]", txt, sb.ToString().TrimEnd(' ', ',')), paddingLevel);
        }

        public static string EscreverException(Exception x)
        {
            StringBuilder sbException = new StringBuilder();
            sbException.AppendLine("EXCEPTION: " + x.GetType().FullName);
            sbException.AppendLine("MESSAGE: " + x.Message);
            sbException.AppendLine("STACK TRACE: " + x.StackTrace);

            if (x.InnerException != null)
            {
                x = x.InnerException;

                sbException.AppendLine("INNER EXCEPTION: " + x.GetType().FullName);
                sbException.AppendLine("MESSAGE: " + x.Message);
                sbException.AppendLine("STACK TRACE: " + x.StackTrace);
            }

            return sbException.ToString();
        }
    }
}
