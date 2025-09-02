using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Reflection;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AERMOD.LIB.Formatacao;

namespace AERMOD.LIB.Desenvolvimento
{
    public static class Funcoes
    {
        #region Declarações

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = CharSet.Auto)]
        extern static bool DestroyIcon(IntPtr handle);

        private const ushort GZIP_LEAD_BYTES = 0x8b1f;

        private static int BUFFER_SIZE = 64 * 1024; //64kB

        #endregion

        #region Métodos

        /// <summary>
        /// Convert imagem para icone
        /// </summary>
        /// <param name="image">Imagem</param>
        /// <returns></returns>
        public static Icon ConvertImageToIcon(this Image image)
        {
            IntPtr iconHandle = ((Bitmap)image).GetHicon();
            Icon tempManagedRes = Icon.FromHandle(iconHandle);
            Icon icon = (Icon)tempManagedRes.Clone();
            tempManagedRes.Dispose();
            DestroyIcon(iconHandle);

            return icon;
        }

        public static string GetStringValue(this Enum value)
        {
            string output = null;
            Type type = value.GetType();

            FieldInfo fi = type.GetField(value.ToString());
            StringValueAttribute[] attrs =
               fi.GetCustomAttributes(typeof(StringValueAttribute),
                                       false) as StringValueAttribute[];
            if (attrs.Length > 0)
            {
                output = attrs[0].Value;
            }

            return output;
        }

        public static string GetEnumDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            if (fi == null)
            {
                return null;
            }

            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                string stringValue = GetStringValue(value);
                if (string.IsNullOrEmpty(stringValue) == true)
                {
                    stringValue = value.ToString();
                }

                return stringValue;
            }
        }

        public static T GetEnumFromDescription<T>(this String value)
        {
            FieldInfo[] arrFi = typeof(T).GetFields();
            String enumName = "";

            arrFi.ToList().ForEach(delegate (FieldInfo fi)
            {
                DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attributes != null && attributes.Length > 0)
                {
                    if (attributes[0].Description == value)
                        enumName = fi.Name;
                }
            });

            return (T)Enum.Parse(typeof(T), enumName);
        }

        public static Boolean IsNullable(this Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static DbType ToDbType(this Type t)
        {
            Type tipo = t.IsNullable() ? Nullable.GetUnderlyingType(t) : t;

            DbType dbt;
            try
            {
                dbt = (DbType)Enum.Parse(typeof(DbType), tipo.Name);
            }
            catch
            {
                dbt = DbType.Object;
            }
            return dbt;
        }

        public static bool VerTXT(char TX)
        {
            char[] _matriz = @"0123456789!@#$%¨&*()_+-=/\|*.,ABCDEFGHIJKLMNOPQRSTUVXWYZÇabcdefghijklmnopqrstuvxwyzÂÃÁÀÊÉÈÎÍÌÔÕÓÒÛÚÙâãáàêéèîìíôõóòûúù`´{}[]ªº<>;:?°'""".ToCharArray();
            List<char> matriz = new List<char>();

            foreach (char c in _matriz)
            {
                matriz.Add(c);
            }

            //matriz.Exists(delegate(char _tx) { return _tx = TX[i]; });
            if (TX != 0)
            {
                if (matriz.Exists(delegate (char _tx) { return _tx == TX; }))
                {
                    return true;
                }
            }

            return false;
        }

        public static string RemoverCaracterEspecial(this string strIn)
        {
            // Replace invalid characters with empty strings.
            try
            {
                return Regex.Replace(strIn, @"[^\w\.@-]", "",
                                     RegexOptions.None, TimeSpan.FromSeconds(1.5));
            }
            // If we timeout when replacing invalid characters,
            // we should return Empty.
            catch (RegexMatchTimeoutException)
            {
                return String.Empty;
            }
        }

        public static bool IsGZipCompressedData(byte[] data)
        {
            // if the first 2 bytes of the array are theG ZIP signature then it is compressed data;
            return (BitConverter.ToUInt16(data, 0) == GZIP_LEAD_BYTES);
        }

        private static byte[] CompressGZip(byte[] inputData)
        {
            if (inputData == null)
                throw new ArgumentNullException("inputData must be non-null");

            using (var compressIntoMs = new MemoryStream())
            {
                using (var gzs = new BufferedStream(new GZipStream(compressIntoMs, CompressionMode.Compress), BUFFER_SIZE))
                {
                    gzs.Write(inputData, 0, inputData.Length);
                }
                return compressIntoMs.ToArray();
            }
        }

        private static byte[] DecompressGZip(byte[] inputData)
        {
            if (inputData == null)
                throw new ArgumentNullException("inputData must be non-null");

            using (var compressedMs = new MemoryStream(inputData))
            {
                using (var decompressedMs = new MemoryStream())
                {
                    using (var gzs = new BufferedStream(new GZipStream(compressedMs, CompressionMode.Decompress), BUFFER_SIZE))
                    {
                        gzs.CopyTo(decompressedMs);
                    }
                    return decompressedMs.ToArray();
                }
            }
        }

        /// <summary>
        /// Retorna arquivo compactado.
        /// </summary>
        /// <param name="arquivo">Arquivo a ser compactado</param>
        /// <returns>Retorna arquivo compactado</returns>
        public static byte[] CompressedGZip(byte[] arquivo)
        {
            byte[] byteData = arquivo;
            if (arquivo != null && AERMOD.LIB.Desenvolvimento.Funcoes.IsGZipCompressedData(byteData) == false)
            {
                byteData = AERMOD.LIB.Desenvolvimento.Funcoes.CompressGZip(byteData);
            }

            return byteData;
        }

        /// <summary>
        /// Retorna arquivo descompactado.
        /// </summary>
        /// <param name="arquivo">Arquivo a ser descompactado</param>
        /// <returns>Retorna arquivo descompactado</returns>
        public static byte[] DecompressedGZip(byte[] arquivo)
        {
            if (arquivo != null && AERMOD.LIB.Desenvolvimento.Funcoes.IsGZipCompressedData(arquivo) == true)
            {
                arquivo = AERMOD.LIB.Desenvolvimento.Funcoes.DecompressGZip(arquivo);
            }

            return arquivo;
        }

        public static T ValidarValor<T>(this object valor, object valorDefault, bool zeroIsNull = false)
        {
            T retorno = (typeof(T) == typeof(String)) ? (T)Convert.ChangeType(String.Empty, typeof(T)) : default(T);
            if (valorDefault != null && valorDefault != DBNull.Value)
            {
                if (typeof(T).IsEnum)
                {
                    retorno = (T)Enum.Parse(typeof(T), valorDefault.ToString());
                }
                else if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    retorno = (T)Convert.ChangeType(valorDefault, Nullable.GetUnderlyingType(typeof(T)));
                }
                else
                {
                    retorno = (T)Convert.ChangeType(valorDefault, typeof(T));
                }
            }

            if (valor != null && valor != DBNull.Value && valor.ToString() != "null")
            {
                if (zeroIsNull == false || (zeroIsNull && valor.ToString().Equals("0") == false))
                {
                    if (typeof(T).IsEnum)
                    {
                        retorno = (T)Enum.Parse(typeof(T), valor.ToString());
                    }
                    else if (typeof(T).IsGenericType && typeof(T).GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        if (typeof(T).GetGenericArguments().Count() > 0 && typeof(T).GetGenericArguments()[0].IsEnum)
                        {
                            retorno = (T)Enum.Parse(Nullable.GetUnderlyingType(typeof(T)), valor.ToString());
                        }
                        else
                        {
                            retorno = (T)Convert.ChangeType(valor, Nullable.GetUnderlyingType(typeof(T)));
                        }
                    }
                    else
                    {
                        retorno = (T)Convert.ChangeType(valor, typeof(T));
                    }
                }
            }

            return retorno;
        }

        public static void SetStyle(Control control, ControlStyles style, bool value)
        {
            // Prevent flickering, only if our assembly 
            // has reflection permission. 
            Type type = control.GetType();
            BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Instance;
            MethodInfo method = type.GetMethod("SetStyle", flags);

            if (method != null)
            {
                object[] param = { style, value };
                method.Invoke(control, param);
            }
        }

        #region Detecta se é tempo de design

        public static Boolean IsDesignTime()
        {
            return System.Diagnostics.Process.GetCurrentProcess().ProcessName == "devenv";
        }

        #endregion

        #region Clonador de Objetos

        public static T CriarCopia<T>(this T obj)
        {
            if (obj == null)
                throw new ArgumentNullException();

            return (T)ClonarObjeto(obj);
        }

        public static object ClonarObjeto(object obj, object objOut = null)
        {
            if (obj == null)
                return null;

            Type type = obj.GetType();

            if (objOut != null && type.Equals(objOut.GetType()) == false)
                return null;

            if (type.IsClass)
            {
                objOut = objOut ?? Activator.CreateInstance(obj.GetType());

                FieldInfo[] fields = type.GetFields(BindingFlags.Public |
                            BindingFlags.NonPublic | BindingFlags.Instance);



                foreach (FieldInfo field in fields)
                {
                    if (field.GetCustomAttributes(false).Any(i => i.GetType() == typeof(NaoCopiarAttribute)))
                        continue;

                    try
                    {
                        object fieldValue = field.GetValue(obj);

                        // Caso campo seja nulo, nao copiar
                        if (fieldValue == null)
                            continue;

                        field.SetValue(objOut, ClonarObjeto(fieldValue));
                    }
                    catch { }
                }

                return objOut;
            }

            return null;
        }

        private static object ClonarObjeto(object obj)
        {
            if (obj == null)
                return null;

            Type type = obj.GetType();
            if (type.IsValueType || type == typeof(string))
            {
                return obj;
            }
            else if (type.IsArray)
            {
                Type elementType = Type.GetType(
                     type.FullName.Replace("[]", string.Empty));
                var array = obj as Array;
                Array copied = Array.CreateInstance(elementType, array.Length);
                for (int i = 0; i < array.Length; i++)
                {
                    try
                    {
                        copied.SetValue(ClonarObjeto(array.GetValue(i)), i);
                    }
                    catch { }
                }
                return Convert.ChangeType(copied, obj.GetType());
            }
            else if (type.IsClass)
            {
                object toret = Activator.CreateInstance(obj.GetType());
                FieldInfo[] fields = type.GetFields(BindingFlags.Public |
                            BindingFlags.NonPublic | BindingFlags.Instance);



                foreach (FieldInfo field in fields)
                {
                    if (field.GetCustomAttributes(false).Any(i => i.GetType() == typeof(NaoCopiarAttribute)))
                        continue;

                    try
                    {
                        object fieldValue = field.GetValue(obj);
                        if (fieldValue == null)
                            continue;

                        field.SetValue(toret, ClonarObjeto(fieldValue));
                    }
                    catch { }
                }


                return toret;
            }
            else
                throw new ArgumentException("Tipo desconhecido");
        }

        #endregion

        #region Formata Data

        /// <summary>
        /// Formata em estilo data
        /// </summary>
        /// <param name="x">String a ser formatada</param>
        /// <returns></returns>
        public static string FormatDATA(this string x)
        {
            if (!String.IsNullOrEmpty(x) || x != null)
            {
                if (x.Length == 8)
                {
                    x = x.Insert(2, "/").Insert(5, "/");
                }
                else if (x.Length == 4)
                {
                    x = x.Insert(0, "0").Insert(2, "0");
                    //DateTime DT = DateTime.Parse(x.Insert(2, "/").Insert(5, "/"));
                    x = x.Insert(2, "/").Insert(5, "/");
                    //x = DT.ToString("dd/MM/yyyy");
                }
                else if (x.Length == 6)
                {
                    try
                    {
                        x = x.Insert(2, "/").Insert(5, "/");
                        //DateTime DT = DateTime.Parse(x.Insert(2, "/").Insert(5, "/"));
                        //x = DT.ToString("dd/MM/yyyy");
                    }
                    catch
                    {
                        x = x.Insert(0, "0").Insert(2, "0");
                        x = x.Insert(2, "/").Insert(5, "/");
                        //DateTime DT = DateTime.Parse(x.Insert(2, "/").Insert(5, "/"));
                        //x = DT.ToString("dd/MM/yyyy");
                    }
                }
            }
            return x;
        }

        #endregion

        #region Formata Mes/Ano

        /// <summary>
        /// Formata em estilo mês/ano
        /// </summary>
        /// <param name="x">String a ser formatada</param>
        /// <returns></returns>
        public static string FormatMesAno(this string x)
        {
            if (!String.IsNullOrEmpty(x) || x != null)
            {
                if (x.Length == 6)
                {
                    x = x.Insert(2, "/");
                }
                else if (x.Length == 3)
                {
                    x = x.Insert(0, "0");
                    x = x.Insert(2, "/");
                }
                else if (x.Length == 5)
                {
                    try
                    {
                        x = x.Insert(2, "/");
                    }
                    catch
                    {
                        x = x.Insert(0, "0");
                        x = x.Insert(2, "/");
                    }
                }
            }

            return x;
        }

        #endregion

        #region Formata Moeda e porcentagem

        /// <summary>
        /// Formata em estilo dinheiro
        /// </summary>
        /// <param name="x">String a ser formatada</param>
        /// <returns></returns>
        public static String FormatMOEDA_PORCENTAGEM(this string x)
        {
            if (x.Contains(',') && x.DeFormat().Length > 0 && !String.IsNullOrEmpty(x) && x != null)
            {
                string unidades = x.Split(',')[0].Trim();
                string decimais = x.Split(',')[1].Trim();
                if (unidades.Length >= 4)
                {
                    x = Convert.ToString(decimal.Parse(x).ToString("N" + decimais.Length));
                }
            }
            return x;
        }

        #endregion

        #region ValidarData

        /// <summary>
        /// Verifica se é uma data válida(True - válida)
        /// </summary>
        /// <param name="data">Data a ser validada</param>
        /// <returns></returns>
        public static Boolean ValidarData(this string data)
        {
            DateTime dataValida;
            return DateTime.TryParse(data, out dataValida);
        }

        #endregion

        #region ValidarDataNasc

        /// <summary>
        /// Valida data de nascimento
        /// </summary>
        /// <param name="_data"></param>
        /// <param name="_idade"></param>
        /// <returns></returns>
        public static Boolean ValidarDataNasc(this string _data, bool _idade)
        {
            bool retorno = true;
            DateTime result;
            if (DateTime.TryParse(_data, out result))
            {
                int idade = 0;
                idade = DateTime.Now.Year - DateTime.Parse(_data).Year;
                if (DateTime.Now.Month < DateTime.Parse(_data).Month ||
                (DateTime.Now.Month == DateTime.Parse(_data).Month &&
                DateTime.Now.Day < DateTime.Parse(_data).Day))
                {
                    idade--;
                }
                if (_idade == true)
                {
                    if (idade > 115 || idade < 0)
                    {
                        retorno = false;
                    }
                }
                else
                {
                    if (idade < 0)
                    {
                        retorno = false;
                    }
                }
            }
            else
            {
                retorno = false;
            }
            return retorno;
        }

        #endregion

        #region Coordenadas

        public static void UTMToLatLon(double Easting, double Northing, double Zone, double Hemi, out double latitude, out double longitude)
        {
            double DtoR = Math.PI / 180, RtoD = 180 / Math.PI;
            double a = 6378137, f = 0.00335281066474748071984552861852, northernN0 = 0, southernN0 = 10000000, E0 = 500000,
                n = f / (2 - f), k0 = 0.9996,
                A = a * (1 + (1 / 4) * Math.Pow(n, 2) + (1 / 64) * Math.Pow(n, 4) + (1 / 256) * Math.Pow(n, 6) + (25 / 16384) * Math.Pow(n, 8) + (49 / 65536) * Math.Pow(n, 10)) / (1 + n),
                beta1 = n / 2 - (2 / 3) * Math.Pow(n, 2) + (37 / 96) * Math.Pow(n, 3) - (1 / 360) * Math.Pow(n, 4) - (81 / 512) * Math.Pow(n, 5) + (96199 / 604800) * Math.Pow(n, 6) - (5406467 / 38707200) * Math.Pow(n, 7) + (7944359 / 67737600) * Math.Pow(n, 8) - (7378753979 / 97542144000) * Math.Pow(n, 9) + (25123531261 / 804722688000) * Math.Pow(n, 10),
                beta2 = (1 / 48) * Math.Pow(n, 2) + (1 / 15) * Math.Pow(n, 3) - (437 / 1440) * Math.Pow(n, 4) + (46 / 105) * Math.Pow(n, 5) - (1118711 / 3870720) * Math.Pow(n, 6) + (51841 / 1209600) * Math.Pow(n, 7) + (24749483 / 348364800) * Math.Pow(n, 8) - (115295683 / 1397088000) * Math.Pow(n, 9) + (5487737251099 / 51502252032000) * Math.Pow(n, 10),
                beta3 = (17 / 480) * Math.Pow(n, 3) - (37 / 840) * Math.Pow(n, 4) - (209 / 4480) * Math.Pow(n, 5) + (5569 / 90720) * Math.Pow(n, 6) + (9261899 / 58060800) * Math.Pow(n, 7) - (6457463 / 17740800) * Math.Pow(n, 8) + (2473691167 / 9289728000) * Math.Pow(n, 9) - (852549456029 / 20922789888000) * Math.Pow(n, 10),
                beta4 = (4397 / 161280) * Math.Pow(n, 4) - (11 / 504) * Math.Pow(n, 5) - (830251 / 7257600) * Math.Pow(n, 6) + (466511 / 2494800) * Math.Pow(n, 7) + (324154477 / 7664025600) * Math.Pow(n, 8) - (937932223 / 3891888000) * Math.Pow(n, 9) - (89112264211 / 5230697472000) * Math.Pow(n, 10),
                beta5 = (4583 / 161280) * Math.Pow(n, 5) - (108847 / 3991680) * Math.Pow(n, 6) - (8005831 / 63866880) * Math.Pow(n, 7) + (22894433 / 124540416) * Math.Pow(n, 8) + (112731569449 / 557941063680) * Math.Pow(n, 9) - (5391039814733 / 10461394944000) * Math.Pow(n, 10),
                beta6 = (20648693 / 638668800) * Math.Pow(n, 6) - (16363163 / 518918400) * Math.Pow(n, 7) - (2204645983 / 12915302400) * Math.Pow(n, 8) + (4543317553 / 18162144000) * Math.Pow(n, 9) + (54894890298749 / 167382319104000) * Math.Pow(n, 10),
                beta7 = (219941297 / 5535129600) * Math.Pow(n, 7) - (497323811 / 12454041600) * Math.Pow(n, 8) - (79431132943 / 332107776000) * Math.Pow(n, 9) + (4346429528407 / 12703122432000) * Math.Pow(n, 10),
                beta8 = (191773887257 / 3719607091200) * Math.Pow(n, 8) - (17822319343 / 336825216000) * Math.Pow(n, 9) - (497155444501631 / 1422749712384000) * Math.Pow(n, 10),
                beta9 = (11025641854267 / 158083301376000) * Math.Pow(n, 9) - (492293158444691 / 6758061133824000) * Math.Pow(n, 10),
                beta10 = (7028504530429621 / 72085985427456000) * Math.Pow(n, 10),
                delta1 = 2 * n - (2 / 3) * Math.Pow(n, 2) - 2 * Math.Pow(n, 3),
                delta2 = (7 / 3) * Math.Pow(n, 2) - (8 / 5) * Math.Pow(n, 3),
                delta3 = (56 / 15) * Math.Pow(n, 3),
                ksi = (Northing / 100 - northernN0) / (k0 * A), eta = (Easting / 100 - E0) / (k0 * A),
                ksi_prime = ksi - (beta1 * Math.Sin(2 * ksi) * Math.Cosh(2 * eta) + beta2 * Math.Sin(4 * ksi) * Math.Cosh(4 * eta) + beta3 * Math.Sin(6 * ksi) * Math.Cosh(6 * eta) + beta4 * Math.Sin(8 * ksi) * Math.Cosh(8 * eta) + beta5 * Math.Sin(10 * ksi) * Math.Cosh(10 * eta) +
                            beta6 * Math.Sin(12 * ksi) * Math.Cosh(12 * eta) + beta7 * Math.Sin(14 * ksi) * Math.Cosh(14 * eta) + beta8 * Math.Sin(16 * ksi) * Math.Cosh(16 * eta) + beta9 * Math.Sin(18 * ksi) * Math.Cosh(18 * eta) + beta10 * Math.Sin(20 * ksi) * Math.Cosh(20 * eta)),
                eta_prime = eta - (beta1 * Math.Cos(2 * ksi) * Math.Sinh(2 * eta) + beta2 * Math.Cos(4 * ksi) * Math.Sinh(4 * eta) + beta3 * Math.Cos(6 * ksi) * Math.Sinh(6 * eta)),
                sigma_prime = 1 - (2 * beta1 * Math.Cos(2 * ksi) * Math.Cosh(2 * eta) + 2 * beta2 * Math.Cos(4 * ksi) * Math.Cosh(4 * eta) + 2 * beta3 * Math.Cos(6 * ksi) * Math.Cosh(6 * eta)),
                taw_prime = 2 * beta1 * Math.Sin(2 * ksi) * Math.Sinh(2 * eta) + 2 * beta2 * Math.Sin(4 * ksi) * Math.Sinh(4 * eta) + 2 * beta3 * Math.Sin(6 * ksi) * Math.Sinh(6 * eta),
                ki = Math.Asin(Math.Sin(ksi_prime) / Math.Cosh(eta_prime));

            latitude = (ki + delta1 * Math.Sin(2 * ki) + delta2 * Math.Sin(4 * ki) + delta3 * Math.Sin(6 * ki)) * RtoD;
            double longitude0 = Zone * 6 * DtoR - 183 * DtoR;
            longitude = (longitude0 + Math.Atan(Math.Sinh(eta_prime) / Math.Cos(ksi_prime))) * RtoD;
        }

        public static void ToLatLon(double utmX, double utmY, string utmZone, out double latitude, out double longitude)
        {
            bool isNorthHemisphere = utmZone.Last() >= 'N';

            var diflat = -0.00066286966871111111111111111111111111;
            var diflon = -0.0003868060578;

            var zone = int.Parse(utmZone.Remove(utmZone.Length - 1));
            var c_sa = 6378137.000000;
            var c_sb = 6356752.314245;
            var e2 = Math.Pow((Math.Pow(c_sa, 2) - Math.Pow(c_sb, 2)), 0.5) / c_sb;
            var e2cuadrada = Math.Pow(e2, 2);
            var c = Math.Pow(c_sa, 2) / c_sb;
            var x = utmX - 500000;
            var y = isNorthHemisphere ? utmY : utmY - 10000000;

            var s = ((zone * 6.0) - 183.0);
            var lat = y / (c_sa * 0.9996);
            var v = (c / Math.Pow(1 + (e2cuadrada * Math.Pow(Math.Cos(lat), 2)), 0.5)) * 0.9996;
            var a = x / v;
            var a1 = Math.Sin(2 * lat);
            var a2 = a1 * Math.Pow((Math.Cos(lat)), 2);
            var j2 = lat + (a1 / 2.0);
            var j4 = ((3 * j2) + a2) / 4.0;
            var j6 = ((5 * j4) + Math.Pow(a2 * (Math.Cos(lat)), 2)) / 3.0;
            var alfa = (3.0 / 4.0) * e2cuadrada;
            var beta = (5.0 / 3.0) * Math.Pow(alfa, 2);
            var gama = (35.0 / 27.0) * Math.Pow(alfa, 3);
            var bm = 0.9996 * c * (lat - alfa * j2 + beta * j4 - gama * j6);
            var b = (y - bm) / v;
            var epsi = ((e2cuadrada * Math.Pow(a, 2)) / 2.0) * Math.Pow((Math.Cos(lat)), 2);
            var eps = a * (1 - (epsi / 3.0));
            var nab = (b * (1 - epsi)) + lat;
            var senoheps = (Math.Exp(eps) - Math.Exp(-eps)) / 2.0;
            var delt = Math.Atan(senoheps / (Math.Cos(nab)));
            var tao = Math.Atan(Math.Cos(delt) * Math.Tan(nab));

            longitude = ((delt * (180.0 / Math.PI)) + s) + diflon;
            latitude = ((lat + (1 + e2cuadrada * Math.Pow(Math.Cos(lat), 2) - (3.0 / 2.0) * e2cuadrada * Math.Sin(lat) * Math.Cos(lat) * (tao - lat)) * (tao - lat)) * (180.0 / Math.PI)) + diflat;
        }

        #endregion

        # region Decimal

        public static Decimal Truncar(this Decimal num, Int32 Decimais, Boolean Arredonda = false)
        {
            decimal valorReturn = 0m;

            if (Arredonda)
            {
                valorReturn = Math.Round(num, Decimais, MidpointRounding.ToEven);
            }
            else
            {
                Decimal potencia = Convert.ToDecimal(Math.Pow(10, Decimais));

                valorReturn = Math.Truncate(num * potencia) / potencia;
            }

            return valorReturn;
        }

        public static Decimal ArredondaParaCima(this Decimal num, Int32 Decimais)
        {
            Decimal potencia = Convert.ToDecimal(Math.Pow(10, Decimais));

            Decimal ret = Math.Round(num, Decimais, MidpointRounding.ToEven);

            if (ret < num)
            {
                ret += 1M / potencia;
            }

            return ret;
        }

        #endregion

        #endregion
    }

    public class StringValueAttribute : System.Attribute
    {
        private string _value;

        public StringValueAttribute(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }
    }

    public class NaoCopiarAttribute : Attribute
    {
        public NaoCopiarAttribute()
        {

        }
    }
}
