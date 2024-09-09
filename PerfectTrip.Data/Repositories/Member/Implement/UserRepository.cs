using PerfectTrip.Common.Entities.Member;
using PerfectTrip.Data.Repositories.Member.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Data.Repositories.Member.Implement
{
    public class UserRepository : IUserRepository
    {
        private readonly PerfectTripDbContext _dbContext;

        public UserRepository(PerfectTripDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User FindByUserId(int userId)
        {
            return _dbContext.Users
                .Where(user => user.UserId == userId)
                .FirstOrDefault();
        }

        public User FindByUsernameAndPassword(string username, string password)
        {
            return _dbContext.Users
                .Where(user => user.Username == username && user.Password == password)
                .FirstOrDefault();
        }
    }
}
