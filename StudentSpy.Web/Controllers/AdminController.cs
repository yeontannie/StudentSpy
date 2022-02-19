using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentSpy.Core;
using StudentSpy.DataManager.Commands;

namespace StudentSpy.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly UserCommand userC;

        public AdminController(UserManager<User> userM, UserCommand userCom)
        {
            userManager = userM;
            userC = userCom;
        }

        [HttpGet]
        [Route("get-students")]
        public Task<IList<User>> GetStudents()
        {
            return userManager.GetUsersInRoleAsync("User");
        }

        [HttpPost]
        [Route("delete-user")]
        public async Task<IActionResult> DeleteCourse(int userId)
        {
            var response = userC.Delete(userId);
            return Ok(response);
        }

        [HttpPost]
        [Route("edit-user")]
        public async Task<IActionResult> EditCourse(User user, int userid)
        {
            courseC.Edit(model.CourseModel, model.CourseId);
            return Ok();            
        }
    }
}
