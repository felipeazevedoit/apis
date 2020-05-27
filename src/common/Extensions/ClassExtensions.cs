using System;

namespace TServices.Comum.Extensions
{
    /// <summary>
    ///     Métodos de extensão.
    /// </summary>
    public static class ClassExtensions
    {
        /// <summary>
        ///     Verifica se a referência é null e joga exception.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="variableName"></param>
        /// <returns></returns>
        public static T ThrowIfNull<T>(this T value, string variableName) where T : class
        {
            return value ?? throw new NullReferenceException($"Valor é nulo: {variableName}");
        }
    }
}