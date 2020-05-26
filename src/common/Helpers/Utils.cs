using System;
using System.Text;
using TServices.Comum.Extensions;

namespace TServices.Comum.Helpers
{
    public static class Utils
    {
        /// <summary>
        /// </summary>
        /// <param name="serverName"></param>
        /// <param name="dbName"></param>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="chave">chave para Descriptografar</param>
        /// <returns></returns>
        public static string MontarConnString(string serverName, string dbName, string user, string password,
            string chave)
        {
            var server = serverName.DecryptData(chave);
            var bdName = dbName.DecryptData(chave);
            var userName = user.DecryptData(chave);
            var pass = password.DecryptData(chave);
            var strConn =
                $"Data Source={server};Initial Catalog={bdName};Persist Security Info=True;User ID={userName};Password={pass};MultipleActiveResultSets=True";

            return strConn;
        }

        public static string GenerateRandomString(int length = 16)
        {
            var random = new Random();
            var sbuilder = new StringBuilder();

            for (var x = 0; x < length; ++x)
            {
                sbuilder.Append((char) random.Next(33, 126));
            }
            
            return sbuilder.ToString();
        }

        public static string GenerateRandomInteger(int length = 5)
        {
            var random = new Random();
            string numero = string.Empty;

            for (int cont = 0; cont < length; cont++)
                numero += random.Next(0, length);

            return numero;
        }

    }
}