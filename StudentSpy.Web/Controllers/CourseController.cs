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
    public class CourseController : ControllerBase
    {
        private readonly CourseQuery courseQ;
        private readonly CourseCommand courseC;
        private readonly UserCommand userC;
        private readonly UserManager<User> userManager;
        private readonly IWebHostEnvironment environment;

        public CourseController(CourseQuery csQ, CourseCommand csC,
            UserCommand usC, UserManager<User> userM, IWebHostEnvironment env)
        {
            courseQ = csQ;
            courseC = csC;
            userC = usC;
            userManager = userM;
            environment = env;
        }

        [HttpPost]
        [Route("save-file")]
        public async Task<IActionResult> SavePhoto()
        {
            var httpRequest = Request.Form;
            var postedFile = httpRequest.Files[0];
            var splitName = postedFile.FileName.Split('.');
            var extention = "." + splitName.Last();

            var fileName = Guid.NewGuid() + extention;
            var physicalPath = environment.ContentRootPath + "/Photos/" + fileName.ToString();

            using(var stream = new FileStream(physicalPath, FileMode.Create))
            {
                postedFile.CopyTo(stream);
            }

            return Ok(fileName.ToString());
        }

        [HttpGet]
        [Route("get-by-id")]
        public Course GetById(int courseId)
        {
            return courseQ.GetCourseById(courseId);
        }

        [HttpGet]
        [Route("get-all-courses")]
        public IList<Course> GetAllCourses()
        {
            return courseQ.GetAllCourses();
        }

        [HttpGet]
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
        public async Task<IActionResult> EditCourse([FromBody] EditCourseRequest model)
        {
            if (ModelState.IsValid)
            {
                courseC.Edit(model.CourseModel, model.CourseId);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("delete-course")]
        public async Task<IActionResult> DeleteCourse([FromBody] UnsubscribeRequest model)
        {
            if (ModelState.IsValid)
            {
                var response = courseC.Delete(model.CourseId);
                return Ok(response);
            }
            return BadRequest();
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
                    throw new KeyNotFoundException("Course do not exist");
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
