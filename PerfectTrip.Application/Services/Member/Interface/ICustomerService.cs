using PerfectTrip.Application.DTOs.Member;
using PerfectTrip.Common.Entities.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Application.Services.Member.Interface
{
    public interface ICustomerService
    {
        Customer FindByUserId(int userId);
    }
}
