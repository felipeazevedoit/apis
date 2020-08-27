using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Formulario.Dinamico.Entities
{
    public class Criptografia : Base
    {
        public int CodigoExterno { get; set; }
        public byte[] ChaveExterna { get; set; }

        public Criptografia()
        {

        }

        /// <summary>     
        /// Metodo de criptografia de valor     
        /// </summary>     
        /// <param name="text">valor a ser criptografado</param>     
        /// <returns>valor criptografado</returns>
        public string Encrypt(string text)
        {
            try
            {
                // Se a string não está vazia, executa a criptografia
                if (!string.IsNullOrEmpty(text))
                {
                    // Cria instancias de vetores de bytes com as chaves                
                    var bKey = Convert.FromBase64String(Nome);
                    var bText = new UTF8Encoding().GetBytes(text);

                    // Instancia a classe de criptografia Rijndael
                    var rijndael = new RijndaelManaged
                    {

                        // Define o tamanho da chave "256 = 8 * 32"                
                        // Lembre-se: chaves possíves:                
                        // 128 (16 caracteres), 192 (24 caracteres) e 256 (32 caracteres)                
                        KeySize = 256
                    };

                    // Cria o espaço de memória para guardar o valor criptografado:                
                    var mStream = new MemoryStream();
                    // Instancia o encriptador                 
                    var encryptor = new CryptoStream(
                        mStream,
                        rijndael.CreateEncryptor(bKey, ChaveExterna),
                        CryptoStreamMode.Write);

                    // Faz a escrita dos dados criptografados no espaço de memória
                    encryptor.Write(bText, 0, bText.Length);
                    // Despeja toda a memória.                
                    encryptor.FlushFinalBlock();
                    // Pega o vetor de bytes da memória e gera a string criptografada                
                    return Convert.ToBase64String(mStream.ToArray());
                }
                else
                {
                    // Se a string for vazia retorna nulo                
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Se algum erro ocorrer, dispara a exceção            
                throw new ApplicationException("Erro ao criptografar", ex);
            }
        }

        /// <summary>     
        /// Pega um valor previamente criptografado e retorna o valor inicial 
        /// </summary>     
        /// <param name="text">texto criptografado</param>     
        /// <returns>valor descriptografado</returns>     
        public string Decrypt(string text)
        {
            try
            {
                // Se a string não está vazia, executa a criptografia           
                if (!string.IsNullOrEmpty(text))
                {
                    // Cria instancias de vetores de bytes com as chaves                
                    var bKey = Convert.FromBase64String(Nome);
                    var bText = Convert.FromBase64String(text);

                    // Instancia a classe de criptografia Rijndael                
                    var rijndael = new RijndaelManaged();

                    // Define o tamanho da chave "256 = 8 * 32"                
                    // Lembre-se: chaves possíves:                
                    // 128 (16 caracteres), 192 (24 caracteres) e 256 (32 caracteres)                
                    rijndael.KeySize = 256;

                    // Cria o espaço de memória para guardar o valor DEScriptografado:               
                    var mStream = new MemoryStream();

                    // Instancia o Decriptador                 
                    var decryptor = new CryptoStream(
                        mStream,
                        rijndael.CreateDecryptor(bKey, ChaveExterna),
                        CryptoStreamMode.Write);

                    // Faz a escrita dos dados criptografados no espaço de memória   
                    decryptor.Write(bText, 0, bText.Length);
                    // Despeja toda a memória.                
                    decryptor.FlushFinalBlock();
                    // Instancia a classe de codificação para que a string venha de forma correta         
                    var utf8 = new UTF8Encoding();
                    // Com o vetor de bytes da memória, gera a string descritografada em UTF8       
                    return utf8.GetString(mStream.ToArray());
                }
                else
                {
                    // Se a string for vazia retorna nulo                
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Se algum erro ocorrer, dispara a exceção            
                throw new ApplicationException("Erro ao descriptografar", ex);
            }
        }
    }
}
