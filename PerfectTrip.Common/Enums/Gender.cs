using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Common.Enums
{
    public enum Gender
    {
        [EnumMember(Value = "男")]
        Male,

        [EnumMember(Value = "女")]
        Female,

        [EnumMember(Value = "保密")]
        Secret
    }
}
