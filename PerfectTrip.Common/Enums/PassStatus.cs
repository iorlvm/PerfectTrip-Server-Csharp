using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Common.Enums;

public enum PassStatus
{
    [EnumMember(Value = "待審核")]
    Pending,

    [EnumMember(Value = "已通過")]
    Passed,

    [EnumMember(Value = "未通過")]
    NotPassed
}
