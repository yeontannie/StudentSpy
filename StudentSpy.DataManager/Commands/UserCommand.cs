using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using StudentSpy.Core;
using StudentSpy.DataManager.Data;
using StudentSpy.DataManager.Queries;

namespace StudentSpy.DataManager.Commands
{
    public class UserCommand
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly AppDbContext context;
        private readonly UserManager<User> userManager;
        private readonly UserQuery userQ;

        public UserCommand(IHttpContextAccessor httpContext, AppDbContext ctx,
            UserManager<User> userM, UserQuery userQue)
        {
            httpContextAccessor = httpContext;
            context = ctx;
            userManager = userM;
            userQ = userQue;
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

        public async Task<string> Delete(User user)
        {
            var findUser = await userManager.FindByNameAsync(user.UserName);
            //var userId = userQ.GetUserIdd(findUser);
            //var subs = context.Subscriptions.Where(i => i.UserId == userId.Result).ToList();
            
            if (findUser != null) //&& subs.Capacity == 0
            {
                userManager.DeleteAsync(findUser);
                return "Deleted successfully";
            }                       
            return "Cannot delete student with subscriptions";
        }
    }
}
