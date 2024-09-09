using PerfectTrip.Common.Entities.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Data.Repositories.Member.Interface
{
    public interface ICustomerRepository
    {
        Customer FindByUserId(int userId);
    }
}
