using PerfectTrip.Application.DTOs.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Application.Services.Jwt.Interface
{
    public interface IJwtService
    {
        (string ShortLivedToken, string LongLivedToken) GenerateToken(UserDto userDto, bool rememberMe);
        Task StoreTokenAsync(string userId, string token);
        Task<long> GetLastTokenIssuedAtAsync(string userId);
        Task<bool> IsTokenValidAsync(string userId, long issuedAt);
        string? GetIssuedAtFromToken(string token);
    }
}
