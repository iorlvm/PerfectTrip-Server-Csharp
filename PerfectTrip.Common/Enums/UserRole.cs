using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Common.Enums
{
    public enum UserRole
    {
        [EnumMember(Value = "admin")]
        Admin,

        [EnumMember(Value = "company")]
        Company,

        [EnumMember(Value = "user")]
        Customer
    }
}
