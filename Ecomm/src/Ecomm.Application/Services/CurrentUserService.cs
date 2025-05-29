using Ecomm.Application.Interfaces;
using Ecomm.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Ecomm.Application.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _accessor;

        public CurrentUserService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public string? UserId =>
            _accessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        public string? Email =>
            _accessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
    }
}
