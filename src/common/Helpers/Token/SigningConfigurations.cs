using System;
using System.Security.Cryptography;

using Microsoft.IdentityModel.Tokens;

namespace TServices.Comum.Helpers.Token
{
    public class SigningConfigurations
    {
        public SigningConfigurations()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            SigningCredentials = new SigningCredentials(
                Key, SecurityAlgorithms.RsaSha256Signature);
        }

        public SigningConfigurations(string secret)
        {
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Convert.FromBase64String(secret)),
                SecurityAlgorithms.HmacSha256Signature);

            var symmetricKey = Convert.FromBase64String(secret);
            Key = new SymmetricSecurityKey(symmetricKey);

            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature);
        }

        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }
    }
}