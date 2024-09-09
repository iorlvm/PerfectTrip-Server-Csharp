using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Common.Enums
{
    public enum CustomerGroup
    {
        [EnumMember(Value = "一般")]
        Normal,

        [EnumMember(Value = "VIP")]
        VIP
    }
}
