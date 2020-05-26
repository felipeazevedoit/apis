using System;
using System.Collections.Generic;

namespace TServices.Comum.Helpers.Reflection
{
    public class GetProperty<T> where T : class
    {
        private readonly T _item;
        private readonly Type _myType;

        public GetProperty()
        {
            _myType = typeof(T);
        }

        public GetProperty(T item)
        {
            _item = item;
            _myType = item.GetType();
        }

        /// <summary>
        /// Retorna o(s) nomes(s) da(s) propriedade(s)
        /// </summary>
        /// <returns></returns>
        public List<string> GetName()
        {
            var retorno = new List<string>();

            var myProperty = _myType.GetProperties();

            foreach (var property in myProperty)
            {
                retorno.Add(property.Name);
            }

            return retorno;
        }

        /// <summary>
        /// Retorna o(s) nomes(s) e valor(es) da(s) propriedade(s)
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, string> GetNameValue()
        {
            var retorno = new Dictionary<string, string>();

            var myProperty = _myType.GetProperties();

            foreach (var property in myProperty)
            {
                var value = property.GetValue(_item, null);

                retorno.Add(property.Name.ToLower(), value != null ? value.ToString() : string.Empty);
            }

            return retorno;
        }

        /// <summary>
        /// Retorna o valor pelo nome da propriedade
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetValueByName(string name) => _myType?.GetProperty(name)?.GetValue(_item, null)?.ToString();
    }
}