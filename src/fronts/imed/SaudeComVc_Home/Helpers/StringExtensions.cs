using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace SaudeComVc_Home.Helpers
{
    /// <summary>
    /// Métodos de extensão.
    /// </summary>
    public static class StringExtensions
    {
        static readonly Regex NotDigitsRegex = new Regex("[^0-9]", RegexOptions.Compiled);
        private static readonly CultureInfo ci = CultureInfo.InvariantCulture;

        /// <summary>
        /// Valida se a string é nula, vazia ou espaço em branco
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string s) => string.IsNullOrEmpty(s) ? true : string.IsNullOrWhiteSpace(s);

        public static void ThrowIsNullOrEmpty(this string s, string variableName)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                throw new ArgumentException($"{variableName} é obrigatório");
            }
        }

        /// <summary>
        /// Deixa apenas os números da string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string KeepOnlyNumbersOrNull(this string s) => s.IsNullOrEmpty() ? null : (NotDigitsRegex.Replace(s, string.Empty).TrimAndNullEmpty());

        /// <summary>
        /// Remove todos os caracteres que não sejam números.
        /// Caso a string seja null, retorna string.Empty.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string KeepOnlyNumbers(this string s) => NotDigitsRegex.Replace(s, string.Empty);

        /// <summary>
        /// Realiza trim na string. Caso seja empty, transforma em null.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string TrimAndNullEmpty(this string s) => s.IsNullOrEmpty() ? null : s.Trim();

        /// <summary>
        /// Extrai inteiro ou nulo
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int? ExtractIntOrNull(this string s) => (!s.IsNullOrEmpty()) ? (int.TryParse(s, out int result) ? new int?(result) : null) : null;

        /// <summary>
        /// Extrai inteiro ou zero
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int ExtractIntOrZero(this string s) => ExtractIntOrNull(s) ?? 0;

        /// <summary>
        /// Extrai long ou nulo
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static long? ExtractLongOrNull(this string s) => (!s.IsNullOrEmpty()) ? (long.TryParse(s.KeepOnlyNumbers(), out long retorno)) ? (long?)retorno : throw new InvalidCastException($"Valor inválido para conversão! Valor encontrado: {s}") : null;

        /// <summary>
        /// Extrai long ou zero
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static long ExtractLong(this string s) => s.ExtractLongOrNull() ?? 0;

        /// <summary>
        /// Extrai qtde de caracteres da string
        /// </summary>
        /// <param name="s"></param>
        /// <param name="posicaoIni"></param>
        /// <param name="qtdCaracteres"></param>
        /// <returns></returns>
        public static string ExtractString(this string s, int posicaoIni, int qtdCaracteres) => s.IsNullOrEmpty() ? string.Empty : s.Substring(posicaoIni, qtdCaracteres);

        /// <summary>
        /// Retorna X caracteres à esquerda da string.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Left(this string s, int length) => (length >= s.Length) ? s : s.Substring(0, length);

        /// <summary>
        /// Retorna X caracteres à direita da string.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Right(this string s, int length) => (length >= s.Length) ? s : s.Substring(s.Length - length, length);

        /// <summary>
        /// Completa caracteres à esquerda
        /// </summary>
        /// <param name="numero"></param>
        /// <param name="qtdCaracter"></param>
        /// <returns></returns>
        public static string CompletaCaracterEsquerda(this string numero, int qtdCaracter, char completaCaracter = '0')
        {
            long numeroInt = numero.ExtractLong();

            if (numeroInt < 0) { numeroInt *= -1; }

            return numero.ToString(ci).PadLeft(qtdCaracter, completaCaracter);
        }

        public static bool ArquivoTexto(this string extension)
        {
            return (extension.ToLower().Contains("csv") || extension.ToLower().Contains("txt"));
        }

        public static bool ValidarTamanho(this string extension)
        {
            return (extension.ToLower().Contains("txt"));
        }

        public static bool AesTypesCript(this string extension) => System.IO.Path.GetExtension(extension).ToLower() == ".pgp" ? true : false;

        public static string CleanToken(this string token) => token.Replace("Bearer", "").Trim();

    }
}