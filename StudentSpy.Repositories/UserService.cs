using Microsoft.AspNetCore.Identity;
using StudentSpy.Core;
using StudentSpy.Core.Requests;

namespace StudentSpy.Repositories
{
    public class UserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public Task<IList<User>> GetAllStudents()
        {
            return _userManager.GetUsersInRoleAsync("User");
        }

        //public string GetUserName(string email)
        //{
        //    var username = email.Split('@');
        //    return username[0];
        //}

        //public async Task<User> UserExists(string email)
        //{            
        //    var user = await _userManager.FindByNameAsync(GetUserName(email));
        //    return user;
        //}

        //public User CreateUser(RegisterRequest model)
        //{
        //    User user = new()
        //    {
        //        Name = model.Name,
        //        LastName = model.LastName,
        //        Email = model.Email,
        //        UserName = GetUserName(model.Email),
        //        Age = Convert.ToInt32(model.Age),
        //        PhotoPath = model.PhotoPath,
        //        RegisterDate = DateTime.Now,
        //        SecurityStamp = Guid.NewGuid().ToString()
        //    };
        //    return user;
        //}

        //public Response CreateResponse(string status, string message)
        //{
        //    return new Response { Status = status, Message = message };
        //}
    }
}
