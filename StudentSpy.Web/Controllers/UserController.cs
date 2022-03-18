using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentSpy.Core;
using StudentSpy.Core.Requests;
using StudentSpy.DataManager.Commands;
using StudentSpy.DataManager.Queries;

namespace StudentSpy.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly UserCommand userC;
        private readonly UserQuery userQ;

        public UserController(UserManager<User> userM, UserCommand userCom, UserQuery userQue)
        {
            userManager = userM;
            userC = userCom;
            userQ = userQue;
        }

        [HttpGet]
        [Route("get-students")]
        public async Task<IActionResult> GetStudents()
        {
            var subs = userC.CheckSubs();
            var students = userQ.GetStudents();
            return Ok(new
            {
                courses = subs,
                students = students
            });
        }

        [HttpPost]
        [Route("delete-user")]
        public async Task<IActionResult> DeleteUser([FromBody] DeleteUserRequest model)
        {
            if (ModelState.IsValid)
            {
                var response = await userC.Delete(model.User);
                if (response.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return Ok("Can't delete user with subscriptions.");
                }
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("edit-user")]
        public async Task<IActionResult> EditUser([FromBody] EditUserRequest model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.UserName);
                var response = await userC.Edit(model.Name, model.LastName, model.Age, user);
                if (response.Succeeded)
                {
                    return Ok();
                }
            }
            return BadRequest();
        }
    }
}
