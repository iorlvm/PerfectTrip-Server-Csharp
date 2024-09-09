using PerfectTrip.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Application.Services.Redis.Interface
{
    public interface IRedisService
    {
        void Save(string key, string value, double time, TimeUnit unit);
    }
}
