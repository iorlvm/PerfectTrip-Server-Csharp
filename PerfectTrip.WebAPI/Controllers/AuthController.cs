using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PerfectTrip.Data;
using PerfectTrip.Common.Entities.Member;

using PerfectTrip.Application.DTOs.Member;
using PerfectTrip.Common.Constant;
using PerfectTrip.Application.Services.Member.Interface;
using PerfectTrip.Application.DTOs;
using System.Data;
using AutoMapper;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using PerfectTrip.Common.Utils;

namespace PerfectTrip.WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("chat/uid")]
        [Authorize]
        public async Task<IActionResult> Test()
        {
            var user = HttpContext.User;
            var claims = user.Claims.ToList();

            if (!claims.Any())
            {
                return Unauthorized("找不到任何聲明");
            }

            Console.WriteLine("通過權限");

            return Ok(Result.Ok( new string[]{ claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, "1" }));
        }


        [HttpPost]
        [Route("{role}/login")]
        public async Task<IActionResult> Login([FromRoute] string role, [FromBody] LoginRequest loginRequest)
        {
            // 確認 role 是否有效
            if (role != RoleConstant.CUSTOMER && role != RoleConstant.COMPANY && role != RoleConstant.ADMIN)
            {
                return NotFound();
            }

            UserDto? userDto = _userService.Login(loginRequest);
            if (userDto == null) {
                return Ok(Result.Fail("帳號或密碼錯誤"));
            }

            // 登入成功 (創造一個token, 儲存登入狀態)
            (string token, string refreshToken) = _userService.CreateToken(userDto, loginRequest.RememberMe);
            userDto.Token = token;
            userDto.RefreshToken = refreshToken;

            return Ok(Result.Ok(userDto));
        }

        [HttpGet]
        [Route("fresh-token")]
        [Authorize(Roles = "Refresh")]
        public async Task<IActionResult> RefreshToken()
        {
            var claims = HttpContext.User.Claims.ToList();

            var userId = ClaimHelper.GetClaimValue<int>(claims, ClaimTypes.NameIdentifier);
            var rememberMe = ClaimHelper.GetClaimValue<bool>(claims, "RememberMe");

            UserDto? userDto = _userService.RefreshToken(userId, rememberMe);
            if (userDto == null) {
                return Unauthorized();
            }

            return Ok(Result.Ok(userDto));
        }

        [HttpGet]
        [Route("auth-token")]
        public async Task<IActionResult> AuthToken()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                // 解析成功: 表示長效token沒有過期, 可以繼續使用
                return Ok(Result.Ok(new { expired = false }));
            } else
            {
                // 解析失敗: 表示長效token過期, 回傳告知前端token已經過期 (前端接受過期會清空登入狀態)
                return Ok(Result.Ok(new { expired = true }));
            }
        }
    }
}
