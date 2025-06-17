using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GolfApp.Helpers
{
    public static class TokenHelper
    {
        public static async Task<string> GetUserId()
        {
            var token = await SecureStorage.Default.GetAsync("jwt");
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);
            var id = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            return id;
        }
    }
}
