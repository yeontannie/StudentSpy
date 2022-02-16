using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace StudentSpy.Web.Commands
{
    public class UserCommand
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserCommand(IHttpContextAccessor httpContext)
        {
            httpContextAccessor = httpContext;
        }

        public string GetUserId()
        {
            var token = httpContextAccessor.HttpContext.Request.Headers.Authorization;
            string userId = "";

            var securityTokenHandler = new JwtSecurityTokenHandler();
            if (securityTokenHandler.CanReadToken(token))
            {
                var decriptedToken = securityTokenHandler.ReadJwtToken(token);
                userId = decriptedToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            }
            return userId;
        }
    }
}
