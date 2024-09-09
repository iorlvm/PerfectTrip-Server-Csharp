using PerfectTrip.Application.DTOs.Member;
using PerfectTrip.Common.Entities.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfectTrip.Application.Services.Member.Interface
{
    public interface IUserService
    {
        UserDto? Login(LoginRequest loginRequest);

        UserDto? RefreshToken(int userId, bool rememberMe = false);

        (string token, string refreshToken) CreateToken(UserDto userDto, bool rememberMe = false);
    }
}
