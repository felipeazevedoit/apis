using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace TServices.Comum.Extensions
{
    public static class TokenClaimsExtensions
    {
        public static string GetValueItemKey(this IEnumerable<Claim> claim, string key)
        {
            return claim?.FirstOrDefault(x => x.Type == key)?.Value;
        }

        public static Claim GetItemKey(this IEnumerable<Claim> claim, string key)
        {
            return claim?.FirstOrDefault(x => x.Type == key);
        }
    }
}