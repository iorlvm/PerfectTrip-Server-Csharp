using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Common.Utils
{
    public class ClaimHelper
    {
        public static T GetClaimValue<T>(IEnumerable<Claim> claims, string claimType)
        {
            var claimValue = claims.FirstOrDefault(c => c.Type == claimType)?.Value;
            if (claimValue == null)
            {
                throw new Exception($"Claim {claimType} not found.");
            }
            return (T)Convert.ChangeType(claimValue, typeof(T));
        }
    }
}
