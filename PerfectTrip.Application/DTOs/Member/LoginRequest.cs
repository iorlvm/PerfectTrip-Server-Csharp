using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Application.DTOs.Member
{
    public class LoginRequest
    {
        public string Username {  get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;
    }
}
