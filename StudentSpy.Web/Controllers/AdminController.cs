using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentSpy.Core;

namespace StudentSpy.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<User> userManager;

        public AdminController(UserManager<User> userM)
        {
            userManager = userM;
        }

        [HttpGet]
        [Route("get-students")]
        public Task<IList<User>> GetStudents()
        {
            return userManager.GetUsersInRoleAsync("User");
        }
    }
}
