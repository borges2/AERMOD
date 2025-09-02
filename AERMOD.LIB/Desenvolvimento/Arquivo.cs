using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AERMOD.LIB.Desenvolvimento
{
    public static class Arquivo
    {
        #region Declarações

        private static System.Threading.Semaphore semaforo = new System.Threading.Semaphore(1, 1);

        #endregion

        #region Métodos

        public static void EscreverCaminhoDataBase(String txt, bool arquivoNovo = false)
        {
            semaforo.WaitOne();            

            String diretorio = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "DATABASE");

            if (arquivoNovo)
            {
                String[] arquivosTemporarios = Directory.GetFiles(diretorio);

                foreach (var arquivoAtual in arquivosTemporarios)
                {
                    if (arquivoAtual.EndsWith(".txt"))
                    {
                        File.Delete(arquivoAtual);
                    }
                }                
            }

            FileStream arquivo = File.Open(Path.Combine(diretorio, "rede.txt"), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Delete);
            TextWriter escritor = new StreamWriter(arquivo);
            arquivo.Position = arquivo.Length;
            string[] linhas = txt.Split(new String[] { System.Environment.NewLine }, StringSplitOptions.None);
            foreach (string linha in linhas)
            {
                escritor.WriteLine(linha);                
            }            

            escritor.Flush();
            arquivo.Flush();
            Win32.FlushFileBuffers(arquivo.SafeFileHandle);
            escritor.Close();
            arquivo.Close();
            arquivo.Dispose();

            semaforo.Release();
        }

        public static void EscreverSAM(String txt, Int32 paddingLevel = 0, bool arquivoNovo = false)
        {
            semaforo.WaitOne();

            var leftPadding = new String(' ', paddingLevel * 4);

            String diretorio = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "Arquivos");

            if (Directory.Exists(diretorio) == false)
            {
                Directory.CreateDirectory(diretorio);
            }
            else if (arquivoNovo)
            {
                String[] arquivosTemporarios = Directory.GetFiles(diretorio);

                foreach (var arquivoAtual in arquivosTemporarios)
                {
                    if (arquivoAtual.EndsWith(".SAM"))
                    {
                        File.Delete(arquivoAtual);
                    }
                }
            }

            FileStream arquivo = File.Open(Path.Combine(diretorio, "SAMSON.SAM"), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Delete);
            TextWriter escritor = new StreamWriter(arquivo);
            arquivo.Position = arquivo.Length;
            string[] linhas = txt.Split(new String[] { System.Environment.NewLine }, StringSplitOptions.None);
            foreach (string linha in linhas)
            {
                escritor.WriteLine(String.Format("{0}{1}", leftPadding, linha));
                //escritor.WriteLine(String.Format("[{0}][{1}]: {2}{3}", DateTime.Now.ToString(), tipo, leftPadding, linha));
            }

            escritor.Flush();
            arquivo.Flush();
            Win32.FlushFileBuffers(arquivo.SafeFileHandle);
            escritor.Close();
            arquivo.Close();
            arquivo.Dispose();

            semaforo.Release();
        }

        public static void EscreverAERMAP(String txt, Int32 paddingLevel = 0, bool arquivoNovo = false)
        {
            semaforo.WaitOne();

            var leftPadding = new String(' ', paddingLevel * 4);

            String diretorio = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "AERMOD_BACKEND\\AERMAP");

            if (arquivoNovo)
            {
                String[] arquivosTemporarios = Directory.GetFiles(diretorio);

                foreach (var arquivoAtual in arquivosTemporarios)
                {
                    if (arquivoAtual.EndsWith(".INP"))
                    {
                        File.Delete(arquivoAtual);
                    }
                }
            }

            FileStream arquivo = File.Open(Path.Combine(diretorio, "AERMAP.INP"), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Delete);
            TextWriter escritor = new StreamWriter(arquivo);
            arquivo.Position = arquivo.Length;
            string[] linhas = txt.Split(new String[] { System.Environment.NewLine }, StringSplitOptions.None);
            foreach (string linha in linhas)
            {
                escritor.WriteLine(String.Format("{0}{1}", leftPadding, linha));
                //escritor.WriteLine(String.Format("[{0}][{1}]: {2}{3}", DateTime.Now.ToString(), tipo, leftPadding, linha));
            }

            escritor.Flush();
            arquivo.Flush();
            Win32.FlushFileBuffers(arquivo.SafeFileHandle);
            escritor.Close();
            arquivo.Close();
            arquivo.Dispose();

            semaforo.Release();
        }

        public static void EscreverAERMET_1(String txt, Int32 paddingLevel = 0, bool arquivoNovo = false)
        {
            semaforo.WaitOne();

            var leftPadding = new String(' ', paddingLevel * 4);

            String diretorio = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "AERMOD_BACKEND\\AERMET");

            if (arquivoNovo)
            {
                String[] arquivosTemporarios = Directory.GetFiles(diretorio);

                foreach (var arquivoAtual in arquivosTemporarios)
                {
                    if (arquivoAtual.EndsWith(".INP"))
                    {
                        File.Delete(arquivoAtual);
                    }
                }
            }

            FileStream arquivo = File.Open(Path.Combine(diretorio, "AERMET_1.INP"), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Delete);
            TextWriter escritor = new StreamWriter(arquivo);
            arquivo.Position = arquivo.Length;
            string[] linhas = txt.Split(new String[] { System.Environment.NewLine }, StringSplitOptions.None);
            foreach (string linha in linhas)
            {
                escritor.WriteLine(String.Format("{0}{1}", leftPadding, linha));
                //escritor.WriteLine(String.Format("[{0}][{1}]: {2}{3}", DateTime.Now.ToString(), tipo, leftPadding, linha));
            }

            escritor.Flush();
            arquivo.Flush();
            Win32.FlushFileBuffers(arquivo.SafeFileHandle);
            escritor.Close();
            arquivo.Close();
            arquivo.Dispose();

            semaforo.Release();
        }

        public static void EscreverAERMET_2(String txt, Int32 paddingLevel = 0, bool arquivoNovo = false)
        {
            semaforo.WaitOne();

            var leftPadding = new String(' ', paddingLevel * 4);

            String diretorio = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "AERMOD_BACKEND\\AERMET");

            if (arquivoNovo)
            {
                String[] arquivosTemporarios = Directory.GetFiles(diretorio);

                foreach (var arquivoAtual in arquivosTemporarios)
                {
                    if (arquivoAtual.EndsWith(".INP"))
                    {
                        File.Delete(arquivoAtual);
                    }
                }
            }

            FileStream arquivo = File.Open(Path.Combine(diretorio, "AERMET_2.INP"), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Delete);
            TextWriter escritor = new StreamWriter(arquivo);
            arquivo.Position = arquivo.Length;
            string[] linhas = txt.Split(new String[] { System.Environment.NewLine }, StringSplitOptions.None);
            foreach (string linha in linhas)
            {
                escritor.WriteLine(String.Format("{0}{1}", leftPadding, linha));
                //escritor.WriteLine(String.Format("[{0}][{1}]: {2}{3}", DateTime.Now.ToString(), tipo, leftPadding, linha));
            }

            escritor.Flush();
            arquivo.Flush();
            Win32.FlushFileBuffers(arquivo.SafeFileHandle);
            escritor.Close();
            arquivo.Close();
            arquivo.Dispose();

            semaforo.Release();
        }

        public static void EscreverAERMOD(String txt, Int32 paddingLevel = 0, bool arquivoNovo = false)
        {
            semaforo.WaitOne();

            var leftPadding = new String(' ', paddingLevel * 4);

            String diretorio = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location), "AERMOD_BACKEND\\AERMOD");

            if (arquivoNovo)
            {
                String[] arquivosTemporarios = Directory.GetFiles(diretorio);

                foreach (var arquivoAtual in arquivosTemporarios)
                {
                    if (arquivoAtual.EndsWith(".INP"))
                    {
                        File.Delete(arquivoAtual);
                    }
                }
            }

            FileStream arquivo = File.Open(Path.Combine(diretorio, "AERMOD.INP"), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Delete);
            TextWriter escritor = new StreamWriter(arquivo);
            arquivo.Position = arquivo.Length;
            string[] linhas = txt.Split(new String[] { System.Environment.NewLine }, StringSplitOptions.None);
            foreach (string linha in linhas)
            {
                escritor.WriteLine(String.Format("{0}{1}", leftPadding, linha));
                //escritor.WriteLine(String.Format("[{0}][{1}]: {2}{3}", DateTime.Now.ToString(), tipo, leftPadding, linha));
            }

            escritor.Flush();
            arquivo.Flush();
            Win32.FlushFileBuffers(arquivo.SafeFileHandle);
            escritor.Close();
            arquivo.Close();
            arquivo.Dispose();

            semaforo.Release();
        }

        #endregion
    }
}
