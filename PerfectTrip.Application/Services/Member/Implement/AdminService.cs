using PerfectTrip.Application.Services.Member.Interface;
using PerfectTrip.Common.Entities.Member;
using PerfectTrip.Data.Repositories.Member.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Application.Services.Member.Implement
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;

        public AdminService(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        public Admin FindByUserId(int userId)
        {
            return _adminRepository.FindByUserId(userId);
        }
    }
}
