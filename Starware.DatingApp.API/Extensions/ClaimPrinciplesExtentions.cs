using System.Security.Claims;

namespace Starware.DatingApp.API.Extensions
{
    public static class ClaimPrinciplesExtentions
    {
        public static string GetUserName(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.FindFirst(ClaimTypes.Name)?.Value;
        }
        public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return Int32.Parse(claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        }
    }
}
