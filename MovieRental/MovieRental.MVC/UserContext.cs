using Microsoft.AspNetCore.Http;
using MovieRental.MVC.Models;
using System.Security.Claims;

namespace MovieRental.MVC
{
    public class UserContext
    {
        public int UserId { get; private set; }
        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            UserId = GetUserId(httpContextAccessor.HttpContext);
        }

        public static int GetUserId(HttpContext? httpContext)
        {
            int result = -1;

            if (httpContext?.User?.Identity is ClaimsIdentity identity)
            {
                string? nameIdentifierValue = identity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                int.TryParse(nameIdentifierValue, out result);
            }

            return result;
        }
    }
}
