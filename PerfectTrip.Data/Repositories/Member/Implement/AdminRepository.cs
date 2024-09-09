using PerfectTrip.Common.Entities.Member;
using PerfectTrip.Data.Repositories.Member.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Data.Repositories.Member.Implement
{
    public class AdminRepository : IAdminRepository
    {
        private readonly PerfectTripDbContext _dbContext;

        public AdminRepository(PerfectTripDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Admin FindByUserId(int userId)
        {
            return _dbContext.Admins
                .Where(a => a.UserId == userId)
                .FirstOrDefault();
        }
    }
}
