using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentSpy.Core;
using StudentSpy.Core.Requests;
using StudentSpy.Core.Validators;
using StudentSpy.Web.Repositories;

namespace StudentSpy.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly ILogger<AuthController> logger;
        private readonly ICourseRepository courseRepo;
        

        public UserController(UserManager<User> userM,
            ILogger<AuthController> log, ICourseRepository courseR)
        {
            userManager = userM;
            logger = log;
            courseRepo = courseR;
        }



        [HttpPost]
        [Route("add-course")]
        public async Task<IActionResult> AddCourse(Course model)
        {
            try
            {
                courseRepo.Add(model);
                return StatusCode(StatusCodes.Status200OK);
            }
            catch(Exception e)
            {
                logger.LogError(e.Message.ToString());
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost]
        //[Authorize]
        [Route("subscribe")]
        public async Task<IActionResult> Subscribe([FromBody] SubscriptionRequest model)
        {
            SubRequestValidation validator = new SubRequestValidation();
            var result = validator.Validate(model);
            if (result.IsValid)
            {
                //var user = await userManager.FindByIdAsync(courseRepo.GetUserId(model.Token));
                var course = courseRepo.GetCourseById(model.CourseId);
                if (course != null)
                {
                    var sub = new Subscription
                    {
                        UserId = courseRepo.GetUserId(model.Token),
                        CourseId = model.CourseId,
                        DateStarted = Convert.ToDateTime(model.StartDate)
                    };

                    courseRepo.Subscribe(sub);
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }            
            return BadRequest();
        }

        [HttpPost]
        [Route("unsubscribe")]
        public async Task<IActionResult> Unsubscribe(string userId, int courseId)
        {
            try
            {
                var user = await userManager.FindByIdAsync(userId);
                var course = courseRepo.GetCourseById(courseId);
                if(user != null && course != null)
                {
                    courseRepo.Unsubscribe(userId, courseId);
                    return Ok();
                }
            }
            catch(Exception e)
            {
                logger.LogError(e.Message);
            }
            return BadRequest();
        }
    }
}
