using System;
using System.Security.Cryptography;
using System.Text;

namespace TServices.Comum.Helpers.Encrypt
{
    public static class Encrypt
    {
        public static string GetMd5(string valor)
        {
            using (var md5Hash = MD5.Create())
            {
                return GetHash(md5Hash, valor);
            }
        }

        public static bool CompareMd5(string senhabanco, string senhaMd5)
        {
            using (var md5Hash = MD5.Create())
            {
                var senha = GetMd5(senhabanco);

                if (VerifyHash(md5Hash, senhaMd5, senha))
                {
                    return true;
                }

                return false;
            }
        }

        private static string GetHash(MD5 md5Hash, string input)
        {
            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            var sBuilder = new StringBuilder();

            foreach (var t in data)
            {
                sBuilder.Append(t.ToString("x2"));
            }

            return sBuilder.ToString();
        }

        private static bool VerifyHash(MD5 md5Hash, string input, string hash)
        {
            var compara = StringComparer.OrdinalIgnoreCase;

            if (0 == compara.Compare(input, hash))
            {
                return true;
            }

            return false;
        }

        public static string Crypt(string data, string chave)
        {
            byte[] results;
            var utf8 = new UTF8Encoding();
            var hashProvider = new MD5CryptoServiceProvider();
            var tdesKey = hashProvider.ComputeHash(utf8.GetBytes(chave));
            var tdesAlgorithm = new TripleDESCryptoServiceProvider
            {
                Key = tdesKey, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7
            };
            var dataToEncrypt = utf8.GetBytes(data);

            try
            {
                var encryptor = tdesAlgorithm.CreateEncryptor();
                results = encryptor.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
            }
            finally
            {
                tdesAlgorithm.Clear();
                hashProvider.Clear();
            }

            return Convert.ToBase64String(results);
        }

        public static string Decrypt(string data, string chaveDescrypt)
        {
            byte[] results;
            var utf8 = new UTF8Encoding();
            var hashProvider = new MD5CryptoServiceProvider();
            var tdesKey = hashProvider.ComputeHash(utf8.GetBytes(chaveDescrypt));
            var tdesAlgorithm = new TripleDESCryptoServiceProvider
            {
                Key = tdesKey, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7
            };
            var dataToDecrypt = Convert.FromBase64String(data);

            try
            {
                var decryptor = tdesAlgorithm.CreateDecryptor();
                results = decryptor.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
            }
            finally
            {
                tdesAlgorithm.Clear();
                hashProvider.Clear();
            }

            return utf8.GetString(results);
        }
    }
}