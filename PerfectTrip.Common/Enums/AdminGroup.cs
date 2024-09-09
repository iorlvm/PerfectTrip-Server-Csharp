using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Common.Enums
{
    public enum AdminGroup
    {
        [EnumMember(Value = "系統管理員")]
        System,

        [EnumMember(Value = "一般管理員")]
        General,

        [EnumMember(Value = "僅供檢視")]
        Viewer
    }
}
