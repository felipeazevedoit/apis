using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using TServices.Comum.Helpers;
using TServices.Comum.Helpers.Encrypt;

namespace TServices.Comum.Extensions
{
    /// <summary>
    ///     Métodos de extensão.
    /// </summary>
    public static class StringExtensions
    {
        private static readonly Regex NotDigitsRegex = new Regex("[^0-9]", RegexOptions.Compiled);
        private static readonly CultureInfo Ci = CultureInfo.InvariantCulture;

        /// <summary>
        ///     Valida se a string é nula, vazia ou espaço em branco
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s);
        }

        public static void ThrowIsNullOrEmpty(this string s, string variableName)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                throw new ArgumentException($"{variableName} é obrigatório");
            }
        }

        /// <summary>
        ///     Deixa apenas os números da string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string KeepOnlyNumbersOrNull(this string s)
        {
            return s.IsNullOrEmpty() ? null : NotDigitsRegex.Replace(s, string.Empty).TrimAndNullEmpty();
        }

        /// <summary>
        ///     Remove todos os caracteres que não sejam números.
        ///     Caso a string seja null, retorna string.Empty.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string KeepOnlyNumbers(this string s)
        {
            return NotDigitsRegex.Replace(s, string.Empty);
        }

        /// <summary>
        ///     Realiza trim na string. Caso seja empty, transforma em null.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string TrimAndNullEmpty(this string s)
        {
            return s.IsNullOrEmpty() ? null : s.Trim();
        }

        /// <summary>
        ///     Extrai inteiro ou nulo
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int? ExtractIntOrNull(this string s)
        {
            return !s.IsNullOrEmpty() ? int.TryParse(s, out var result) ? new int?(result) : null : null;
        }

        /// <summary>
        ///     Extrai inteiro ou zero
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int ExtractIntOrZero(this string s)
        {
            return ExtractIntOrNull(s) ?? 0;
        }

        /// <summary>
        ///     Extrai long ou nulo
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static long? ExtractLongOrNull(this string s)
        {
            return !s.IsNullOrEmpty()
                ? long.TryParse(s.KeepOnlyNumbers(), out var retorno) ? (long?) retorno :
                throw new InvalidCastException($"Valor inválido para conversão! Valor encontrado: {s}")
                : null;
        }

        /// <summary>
        ///     Extrai long ou zero
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static long ExtractLong(this string s)
        {
            return s.ExtractLongOrNull() ?? 0;
        }

        /// <summary>
        ///     Extrai qtde de caracteres da string
        /// </summary>
        /// <param name="s"></param>
        /// <param name="posicaoIni"></param>
        /// <param name="qtdCaracteres"></param>
        /// <returns></returns>
        public static string ExtractString(this string s, int posicaoIni, int qtdCaracteres)
        {
            return s.IsNullOrEmpty() ? string.Empty : s.Substring(posicaoIni, qtdCaracteres);
        }

        /// <summary>
        ///     Retorna X caracteres à esquerda da string.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Left(this string s, int length)
        {
            return length >= s.Length ? s : s.Substring(0, length);
        }

        /// <summary>
        ///     Retorna X caracteres à direita da string.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Right(this string s, int length)
        {
            return length >= s.Length ? s : s.Substring(s.Length - length, length);
        }

        /// <summary>
        ///     Completa caracteres à esquerda
        /// </summary>
        /// <param name="numero"></param>
        /// <param name="qtdCaracter"></param>
        /// <param name="completaCaracter"></param>
        /// <returns></returns>
        public static string CompletaCaracterEsquerda(this string numero, int qtdCaracter, char completaCaracter = '0')
        {
            return numero.ToString(Ci).PadLeft(qtdCaracter, completaCaracter);
        }

        public static bool ArquivoTexto(this string extension)
        {
            return extension.ToLower().Contains("csv") || extension.ToLower().Contains("txt");
        }

        public static bool ValidarTamanho(this string extension)
        {
            return extension.ToLower().Contains("txt");
        }

        public static string DecryptData(this string valor, string chaveDescrypt)
        {
            return Encrypt.Decrypt(valor, chaveDescrypt);
        }

        public static string EncryptData(this string valor, string chaveCrypt)
        {
            return Encrypt.Crypt(valor, chaveCrypt);
        }

        public static string EncryptDataMd5(this string valor)
        {
            return Encrypt.GetMd5(valor);
        }

        public static bool AesTypesCript(this string extension)
        {
            return Path.GetExtension(extension).ToLower() == ".pgp";
        }

        public static string CleanToken(this string token)
        {
            return token.Replace("Bearer", "").Trim();
        }

        public static string ToCamelCase(this string str)
        {
            TextInfo cultInfo = new CultureInfo("en-US", false).TextInfo;
            str = cultInfo.ToTitleCase(str);
            str = str.Replace(" ", "");
            return str;
        }
        public static string GenerateRandomString(this string value) => Utils.GenerateRandomString();
    }
}