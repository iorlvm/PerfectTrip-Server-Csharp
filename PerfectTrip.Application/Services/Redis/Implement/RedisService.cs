using PerfectTrip.Application.Services.Redis.Interface;
using PerfectTrip.Common.Enums;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Application.Services.Redis.Implement
{
    public class RedisService : IRedisService
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisService(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public void Save(string key, string value, double time, TimeUnit unit)
        {
            var db = _redis.GetDatabase();
            TimeSpan expiration;

            // 根據不同的時間單位設置過期時間
            switch (unit)
            {
                case TimeUnit.Milliseconds:
                    expiration = TimeSpan.FromMilliseconds(time);
                    break;
                case TimeUnit.Seconds:
                    expiration = TimeSpan.FromSeconds(time);
                    break;
                case TimeUnit.Minutes:
                    expiration = TimeSpan.FromMinutes(time);
                    break;
                case TimeUnit.Hours:
                    expiration = TimeSpan.FromHours(time);
                    break;
                case TimeUnit.Days:
                    expiration = TimeSpan.FromDays(time);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(unit), "無效的時間單位");
            }

            // 保存數據到 Redis，並設置過期時間
            db.StringSet(key, value, expiration);
        }
    }
}
