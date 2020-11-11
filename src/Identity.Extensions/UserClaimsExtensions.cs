using System.Security.Claims;

namespace Identity.Extensions
{
    public static class UserClaimsExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal) =>
            (principal.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier") ??
             principal.FindFirst("sub")).Value;
    }
}