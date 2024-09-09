using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PerfectTrip.Application.DTOs.Member;
using PerfectTrip.Application.Services.Jwt.Interface;
using PerfectTrip.Common.Constant;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Application.Services.Jwt.Implement
{
    public class JwtService : IJwtService
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly string _jwtSecret;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly string _expires;
        private readonly string _rememberMeExpires;

        public JwtService(IConnectionMultiplexer redis, IConfiguration configuration)
        {
            _redis = redis;
            _jwtSecret = configuration["JwtSettings:Secret"];
            _issuer = configuration["JwtSettings:Issuer"];
            _audience = configuration["JwtSettings:Audience"];
            _expires = configuration["JwtSettings:ExpiresInMinutes"];
            _rememberMeExpires = configuration["JwtSettings:RememberMeExpiresInMinutes"];
        }

        public (string ShortLivedToken, string LongLivedToken) GenerateToken(UserDto userDto, bool rememberMe = false)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);

            // 創建短效型的聲明列表
            var shortLivedClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userDto.UserId.ToString()),
                new Claim(ClaimTypes.Role, userDto.Role.ToString())
            };

            if (userDto.AdminGroup.HasValue)
            {
                // 管理員權限
                shortLivedClaims.Add(new Claim("AdminGroup", userDto.AdminGroup.Value.ToString()));
            }

            if (userDto.Pass.HasValue)
            {
                // 商家驗證是否通過
                shortLivedClaims.Add(new Claim("Pass", userDto.Pass.Value.ToString()));
            }

            if (userDto.CustomerGroup.HasValue)
            {
                // 客戶的身分 (VIP)
                shortLivedClaims.Add(new Claim("CustomerGroup", userDto.CustomerGroup.Value.ToString()));
            }

            var shortLivedExpiresInMinutes = 1.5;
            var longLivedExpiresInMinutes = rememberMe ? int.Parse(_rememberMeExpires) : int.Parse(_expires);

            // 生成短期token
            var shortLivedTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(shortLivedClaims),
                Expires = DateTime.UtcNow.AddMinutes(shortLivedExpiresInMinutes),
                Issuer = _issuer,
                Audience = _audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var shortLivedToken = tokenHandler.CreateToken(shortLivedTokenDescriptor);
            var shortLivedTokenString = tokenHandler.WriteToken(shortLivedToken);

            // 創建長效型的聲明列表, 角色設置為"Refresh", 並攜帶"RememberMe"做為下次token的刷新策略
            var longLivedClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userDto.UserId.ToString()),
                new Claim(ClaimTypes.Role, "Refresh"),
                new Claim("RememberMe", rememberMe.ToString())
            };

            // 生成長期token
            var longLivedTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(longLivedClaims),
                Expires = DateTime.UtcNow.AddMinutes(longLivedExpiresInMinutes),
                Issuer = _issuer,
                Audience = _audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var longLivedToken = tokenHandler.CreateToken(longLivedTokenDescriptor);
            var longLivedTokenString = tokenHandler.WriteToken(longLivedToken);

            return (shortLivedTokenString, longLivedTokenString);
        }

        public async Task StoreTokenAsync(string userId, string token)
        {
            var db = _redis.GetDatabase();

            string key = RedisConstant.LOGIN_PREFIX + userId;
            await db.StringSetAsync(key, token, TimeSpan.FromMinutes(RedisConstant.LOGIN_TTL)); // redis中 token的過期時間 (後續請求進行刷新延長)
        }

        public async Task<long> GetLastTokenIssuedAtAsync(string userId)
        {
            var db = _redis.GetDatabase();

            string key = RedisConstant.LOGIN_PREFIX + userId;
            var issuedAtString = await db.StringGetAsync(key);

            // 嘗試將字符串轉換為 long
            if (long.TryParse(issuedAtString, out var issuedAtUnixTime))
            {
                return issuedAtUnixTime;
            }

            return -1;
        }

        public async Task<bool> IsTokenValidAsync(string userId, long issuedAt)
        {
            var lastTokenIssued = await GetLastTokenIssuedAtAsync(userId);
            return lastTokenIssued == issuedAt;
        }

        public string? GetIssuedAtFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // 確保這個 token 是一個有效的 JWT 字串
            if (tokenHandler.CanReadToken(token))
            {
                var jwtToken = tokenHandler.ReadJwtToken(token);

                // 嘗試從 claims 中取得 `iat` (issued at)
                var iatClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Iat);

                if (iatClaim != null)
                {
                    return iatClaim.Value;
                }
            }

            return null;
        }
    }
}
