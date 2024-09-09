using PerfectTrip.Application.DTOs.Member;
using PerfectTrip.Application.Services.Member.Interface;
using PerfectTrip.Application.Services.Redis.Interface;
using PerfectTrip.Common.Constant;
using PerfectTrip.Common.Entities.Member;
using PerfectTrip.Common.Enums;
using PerfectTrip.Data;
using PerfectTrip.Data.Repositories.Member.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerfectTrip.Common.Utils;
using PerfectTrip.Application.Services.Jwt.Interface;
using PerfectTrip.Application.DTOs;
using AutoMapper;
using StackExchange.Redis;

namespace PerfectTrip.Application.Services.Member.Implement
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICustomerService _customerService;
        private readonly ICompanyService _companyService;
        private readonly IAdminService _adminService;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public UserService(
            IUserRepository userRepository,
            IAdminService adminService, 
            ICompanyService companyService, 
            ICustomerService customerService,
            IJwtService jwtService,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _adminService = adminService;
            _companyService = companyService;
            _customerService = customerService;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        public (string token, string refreshToken) CreateToken(UserDto userDto, bool rememberMe = false)
        {
            string userId = userDto.UserId.ToString();
            (string token, string refreshToken) = _jwtService.GenerateToken(userDto, rememberMe);
            string? issuedAt = _jwtService.GetIssuedAtFromToken(refreshToken);

            if (issuedAt == null)
            {
                throw new InvalidOperationException("Failed to retrieve 'iat' claim from the token.");
            }

            // 更新token發行的時間
            _jwtService.StoreTokenAsync(userId, issuedAt);
            return (token, refreshToken);
        }

        public UserDto? Login(LoginRequest loginRequest)
        {
            User user = _userRepository.FindByUsernameAndPassword(loginRequest.Username, loginRequest.Password);

            if (user == null) return null;

            UserDto userDto = _mapper.Map<UserDto>(user);

            switch (user.Role)
            {
                case UserRole.Customer:
                    var customer = _customerService.FindByUserId(user.UserId);
                    if (customer != null) _mapper.Map(customer, userDto);
                    break;

                case UserRole.Company:
                    var company = _companyService.FindByUserId(user.UserId);
                    if (company != null) _mapper.Map(company, userDto);
                    break;

                case UserRole.Admin:
                    var admin = _adminService.FindByUserId(user.UserId);
                    if (admin != null) _mapper.Map(admin, userDto);
                    break;
            }

            return userDto;
        }

        public UserDto? RefreshToken(int userId, bool rememberMe = false)
        {
            User user = _userRepository.FindByUserId(userId);

            if (user == null) return null;

            UserDto userDto = _mapper.Map<UserDto>(user);

            switch (user.Role)
            {
                case UserRole.Customer:
                    var customer = _customerService.FindByUserId(user.UserId);
                    if (customer != null) _mapper.Map(customer, userDto);
                    break;

                case UserRole.Company:
                    var company = _companyService.FindByUserId(user.UserId);
                    if (company != null) _mapper.Map(company, userDto);
                    break;

                case UserRole.Admin:
                    var admin = _adminService.FindByUserId(user.UserId);
                    if (admin != null) _mapper.Map(admin, userDto);
                    break;
            }

            (string token, string refreshToken) = CreateToken(userDto, rememberMe);
            userDto.Token = token;
            userDto.RefreshToken = refreshToken;

            return userDto;
        }
    }
}
