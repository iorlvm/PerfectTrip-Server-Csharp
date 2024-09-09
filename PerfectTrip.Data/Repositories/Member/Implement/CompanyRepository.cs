using PerfectTrip.Common.Entities.Member;
using PerfectTrip.Data.Repositories.Member.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Data.Repositories.Member.Implement
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly PerfectTripDbContext _dbContext;

        public CompanyRepository(PerfectTripDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Company FindByUserId(int userId)
        {
            return _dbContext.Companies
                .Where(a => a.UserId == userId)
                .FirstOrDefault();
        }
    }
}
