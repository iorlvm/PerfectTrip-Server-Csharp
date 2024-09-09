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
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Customer FindByUserId(int userId)
        {
            return _customerRepository.FindByUserId(userId);
        }
    }
}
