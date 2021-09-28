using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace PowerDev.Enterprise.Eureka.Extensoes
{
    public static class StringExtensao
    {
        /// <summary>
        /// Transforma a string 'olaMundo' em 'OLA_MUNDO'
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String CamelCaseParaSnakeCase(this String str)
        {
            return str.LimpoNuloBranco() ? str : Regex.Replace(str, "(?<=.)([A-Z])", "_$1").ToUpper().Trim();
        }
        /// <summary>
        /// Converte frases em português normal para o padrão oracle: tudo maiúsculo e separado por underline ('_')
        /// Ex.: 'Olá Mundo' em 'OLA_MUNDO' e 'Terra de ninguém' para 'TERRA_NINGUEM'
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static String NormalParaSnakeCase(this String str)
        {
            if (String.IsNullOrEmpty(str))
                return str;
            str = str.ToUpper();
            str = Regex.Replace(str, @"\s(DO|DE|DA|DOS|DAS|OU|NA|NO|NAS|NOS|PARA|QUE|EM|E|A|O|AS|OS|COM)\s", " ");
            str = str.RemoveAcentos();
            str = str.RemoverCaracteresEspeciais();
            return Regex.Replace(str, @"(?<=.)(\s)", "_").ToUpper().Trim();
        }
        /// <summary>
        /// Separar o padrao camelCase, ex: TesteCamelCase => Teste Camel Case
        /// </summary>
        public static String CamelCaseParaNormal(this String str)
        {
            return String.IsNullOrEmpty(str) ? str : Regex.Replace(str, "([A-Z])", " $1").Trim();
        }
        public static String RemoverEspacosDuplicados(this String str)
        {
            return Regex.Replace(str, @"\s+", " ");
        }
        public static String RemoverCaracteresEspeciais(this String str)
        {
            return String.IsNullOrEmpty(str) ? str : Regex.Replace(str, @"[^0-9a-zA-ZéúíóáÉÚÍÓÁèùìòàÈÙÌÒÀõãñÕÃÑêûîôâÊÛÎÔÂëÿüïöäËYÜÏÖÄçÇ\s]+?", String.Empty);
        }
        public static String RemoveAcentos(this String str)
        {
            const string C_acentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇçºª&";
            const string S_acentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCcoae";

            for (int i = 0; i < C_acentos.Length; i++)
                str = str.Replace(C_acentos[i].ToString(), S_acentos[i].ToString()).Trim();
            return str;
        }
        public static bool LimpoNuloBranco(this String s)
        {
            return String.IsNullOrEmpty(s) || String.IsNullOrWhiteSpace(s);
        }
        public static bool LimpoNuloBrancoOuZero(this String s)
        {
            return s.LimpoNuloBranco() || s == "0";
        }
        public static String ObterSeNuloVazioOuBranco(this String s, String padrao = null)
        {
            if (LimpoNuloBranco(s))
                return padrao;
            return s;
        }
        public static String ObterSeNuloVazioBrancoOuZero(this String s, String padrao = null)
        {
            return s.LimpoNuloBrancoOuZero() ? padrao : s;
        }
        public static bool Verdadeiro(this String str, String comparaCom = "S")
        {
            if (str == null && comparaCom == null)
                return true;

            return str != null && str.Equals(comparaCom, StringComparison.OrdinalIgnoreCase);
        }

        public static String ReduzNofim(this String s, int tamanhoReducao)
        {
            if (String.IsNullOrEmpty(s) || String.IsNullOrWhiteSpace(s) || tamanhoReducao > s.Length || tamanhoReducao < 0)
                return s;
            return s.Substring(0, s.Length - tamanhoReducao);
        }
        public static Byte[] ParaBytes(this String s)
        {
            if (s == null)
                return new byte[0];
            //byte[] bytes = new byte[s.Length * sizeof(char)];
            //Buffer.BlockCopy(s.ToCharArray(), 0, bytes, 0, bytes.Length);
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(s);
            return bytes;
        }
        /// <summary>
        /// Retorna as iniciais do nome. 
        /// Sem o segundo parâmetro serão retornadas apenas a primeira e a última inicial.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="todas">Todas para retornar todas iniciais possíveis</param>
        /// <returns></returns>
        public static string ObterIniciais(this String s, bool todas = false)
        {
            s = s.RemoveAcentos();
            s = Regex.Replace(s, @"( da| das| do| dos)", "");
            s = Regex.Replace(s, @"(\B\w\s*)", "");
            if (todas)
                return s.ToUpperInvariant();
            return string.Format("{0}{1}", s.Substring(0, 1), s.Substring(s.Length - 1, 1));
        }
        public static string ObterPrimeiroUltimoNome(this String s)
        {
            var nomeSeparado = s.Split(' ');

            if (nomeSeparado.Length <= 1)
                return s;

            string primeiroNome = nomeSeparado.First();
            string ultimoNome = nomeSeparado.Last();
            return $"{primeiroNome} {ultimoNome}";
        }
        public static string Capitaliza(this string s)
        {
            if (s.LimpoNuloBranco())
                return s;
            return $"{ s.First().ToString().ToUpper()}{s.Substring(1)}";
        }
    }
}