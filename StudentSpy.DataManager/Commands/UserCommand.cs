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
        private readonly CourseQuery courseQ;

        public UserCommand(IHttpContextAccessor httpContext, AppDbContext ctx,
            UserManager<User> userM, UserQuery userQue, CourseQuery courseQue)
        {
            httpContextAccessor = httpContext;
            context = ctx;
            userManager = userM;
            userQ = userQue;
            courseQ = courseQue;
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

        public async Task<IdentityResult> Delete(User user)
        {
            var findUser = await userManager.FindByNameAsync(user.UserName);
            var userId = userManager.GetUserIdAsync(findUser);
            var subs = context.Subscriptions.Where(i => i.UserId == userId.Result).ToList();
            IdentityResult result;

            if (findUser != null && subs.Capacity == 0)
            {
                result = await userManager.DeleteAsync(findUser);
                return result;
            }
            return IdentityResult.Failed();
        }

        public async Task<IdentityResult> Edit(string name, string lastName, int age, User user)
        {
            if (user.Name != name.Trim())
            {
                user.Name = name.Trim();
            }
            if (user.LastName != lastName.Trim())
            {
                user.LastName = lastName.Trim();
            }
            if (user.Age != age)
            {
                user.Age = age;
            }

            var result = await userManager.UpdateAsync(user);
            return result;
        }

        public Dictionary<string, IList<Course>> CheckSubs()
        {
            var userInfo = new Dictionary<string, IList<Course>>();
            var students = userQ.GetStudents();

            foreach (var student in students.Result)
            {
                var userId = userManager.GetUserIdAsync(student);
                var subscriptions = courseQ.GetSubscribed(userId.Result);

                if (subscriptions.Count != 0)
                {
                    userInfo.Add(student.Email, subscriptions);                    
                }
                //subscriptions.Clear();
            }
            return userInfo;
        }
    }
}
