using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using TServices.Comum.Extensions;

namespace TServices.Comum.Helpers.Token
{
    public static class TokenServiceUtil
    {
        public static IEnumerable<Claim> GetClaims(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            token = token.Replace("Bearer ", "");

            var tokenAccess = tokenHandler.ReadJwtToken(token);

            return tokenAccess?.Claims;
        }

        public static Claim GetClaimKey(string token, string key)
        {
            return GetClaims(token).GetItemKey(key);
        }

        public static string GetValueClaimKey(string token, string key)
        {
            return GetClaims(token).GetValueItemKey(key);
        }
    }
}