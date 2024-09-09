using PerfectTrip.Common.Entities.Member;
using PerfectTrip.Data.Repositories.Member.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Data.Repositories.Member.Implement
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly PerfectTripDbContext _dbContext;

        public CustomerRepository(PerfectTripDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Customer FindByUserId(int userId)
        {
            return _dbContext.Customers
                .Where(a => a.UserId == userId)
                .FirstOrDefault();
        }
    }
}
