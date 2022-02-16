using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentSpy.Core;
using StudentSpy.Core.Requests;
using StudentSpy.Web.Commands;
using StudentSpy.Web.Queries;

namespace StudentSpy.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly CourseQuery courseQ;
        private readonly CourseCommand courseC;
        private readonly UserCommand userC;
        //private readonly UserManager<User> userManager;

        public CourseController(CourseQuery csQ, CourseCommand csC,
            UserCommand usC)// UserManager<User> userM
        {
            courseQ = csQ;
            courseC = csC;
            userC = usC;
            //userManager = userM;
        }

        [HttpGet]
        //[Authorize]
        [Route("get-sub-courses")]
        public IList<Course> GetSubscribedCourses()
        {
            return courseQ.GetSubscribed(userC.GetUserId());
        }

        [HttpGet]
        [Route("get-unsub-courses")]
        public IList<Course> GetUnSubscribedCourses()
        {
            return courseQ.GetUnSubscribed(userC.GetUserId());
        }

        [HttpPut]
        [Route("edit-course")]
        public async Task<IActionResult> EditCourse(Course course, int courseId)
        {
            courseC.Edit(course, courseId);
            return Ok();
        }

        [HttpPost]
        [Route("delete-course")]
        public async Task<IActionResult> DeleteCourse(int courseId)
        {
            courseC.Delete(courseId);
            return Ok();
        }

        [HttpPost]
        [Route("add-course")]
        public async Task<IActionResult> AddCourse(Course model)
        {
            courseC.Add(model);
            return Ok();
        }

        [HttpPost]
        //[Authorize]
        [Route("subscribe")]
        public async Task<IActionResult> Subscribe([FromBody] SubscriptionRequest model)
        {
            if (ModelState.IsValid)
            {
                var course = courseQ.GetCourseById(model.CourseId);
                if (course != null)
                {
                    var sub = new Subscription
                    {
                        UserId = userC.GetUserId(),
                        CourseId = model.CourseId,
                        DateStarted = model.StartDate
                    };

                    courseC.Subscribe(sub);
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
        public async Task<IActionResult> Unsubscribe([FromBody] UnsubscribeRequest model)
        {
            if (ModelState.IsValid)
            {
                var course = courseQ.GetCourseById(model.CourseId);
                if (course != null)
                {
                    courseC.Unsubscribe(userC.GetUserId(), model.CourseId);
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            return BadRequest();
        }
    }
}
