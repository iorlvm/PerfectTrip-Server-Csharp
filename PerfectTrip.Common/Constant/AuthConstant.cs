using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Common.Constant
{
    public class AuthConstant
    {
        public const string CUSTOMER = "users"; 
        public const string COMPANY = "store";
        public const string ADMIN = "admin";

        // TODO: 測試用設定 (未來要改短)
        public const double LOGIN_REFRESH_TTL = 1440; // 分鐘
    }
}
