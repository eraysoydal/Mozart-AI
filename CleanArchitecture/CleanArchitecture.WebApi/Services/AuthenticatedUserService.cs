using CleanArchitecture.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Linq;

namespace CleanArchitecture.WebApi.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");
            
            var roles = httpContextAccessor.HttpContext?.User?.FindAll("roles") ?? System.Array.Empty<Claim>();
            IsAdmin = roles.Any(c => c.Value == "Admin" || c.Value == "SuperAdmin");
        }

        public string UserId { get; }
        public bool IsAdmin { get; }
    }
}
