using Microsoft.AspNetCore.Identity;
using StudentSpy.Core;
using StudentSpy.DataManager.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSpy.DataManager.Queries
{
    public class UserQuery
    {
        private readonly AppDbContext context;
        private readonly UserManager<User> userManager;

        public UserQuery(AppDbContext ctx, UserManager<User> userM)
        {
            context = ctx;
            userManager = userM;
        }

        public User GetUserById(string userId)
        {
            var d = userManager.FindByIdAsync(userId);
        }
    }
}
