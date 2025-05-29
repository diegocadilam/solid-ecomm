using Ecomm.Application.DTOs;
using Ecomm.Application.DTOs.Users;
using Ecomm.Application.Interfaces;
using Ecomm.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Ecomm.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var result = await _userService.LoginAsync(dto);
            return Ok(result);
        }
    }
}
