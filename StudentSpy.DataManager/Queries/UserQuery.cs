using Microsoft.AspNetCore.Identity;
using StudentSpy.Core;

namespace StudentSpy.DataManager.Queries
{
    public class UserQuery
    {
        private readonly UserManager<User> userManager;

        public UserQuery(UserManager<User> userM)
        {
            userManager = userM;
        }

        public async Task<IList<User>> GetStudents()
        {
            return await userManager.GetUsersInRoleAsync("User");
        }
    }
}
