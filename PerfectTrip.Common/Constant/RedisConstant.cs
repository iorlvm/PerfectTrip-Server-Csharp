using PerfectTrip.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Common.Constant
{
    public class RedisConstant
    {
        public const string LOGIN_PREFIX = "login:";
        public const double LOGIN_TTL = 30;
        public const TimeUnit LOGIN_TTL_UNIT = TimeUnit.Minutes;
    }
}
