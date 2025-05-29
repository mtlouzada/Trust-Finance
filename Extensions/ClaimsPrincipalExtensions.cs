using System.Security.Claims;

namespace TF.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var id = user.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.TryParse(id, out var userId) ? userId : 0;
        }
    }
}
