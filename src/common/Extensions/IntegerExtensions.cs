namespace TServices.Comum.Extensions
{
    public static class IntegerExtensions
    {
        /// <summary>
        ///     Extrai string, se for nulo, retorna  string.Empty
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static string ExtractStringOrEmpty(this int? i)
        {
            return i?.ToString() ?? string.Empty;
        }
    }
}