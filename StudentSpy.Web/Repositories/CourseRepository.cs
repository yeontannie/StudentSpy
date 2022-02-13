using StudentSpy.Core;
using StudentSpy.Web.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace StudentSpy.Web.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext context;

        public CourseRepository(AppDbContext ctx)
        {
            context = ctx;
        }

        public string GetUserId(string token)
        {
            var securityTokenHandler = new JwtSecurityTokenHandler();
            if (securityTokenHandler.CanReadToken(token))
            {
                var decriptedToken = securityTokenHandler.ReadJwtToken(token);
                var claims = decriptedToken.Claims;
                //At this point you can get the claims in the token, in the example I am getting the expiration date claims
                //this step depends of the claims included at the moment of the token is generated
                //and what you are trying to accomplish
                return claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value;
            }
            return "NotFound";
        }

        public void Add(Course course)
        {
            context.Courses.Add(course);
            context.SaveChanges();
        }

        public void Delete(int courseId)
        {
            // Add deleting from subscription
            context.Courses.Remove(GetCourseById(courseId));
            context.SaveChanges();
        }

        public void Edit(Course courseEdit, int idToEdit)
        {
            var course = context.Courses.Find(idToEdit);
            if (course.Name != courseEdit.Name)
            {
                course.Name = courseEdit.Name;
            }
            if (course.Description != courseEdit.Description)
            {
                course.Description = courseEdit.Description;
            }
            if (course.Duration != courseEdit.Duration)
            {
                course.Duration = courseEdit.Duration;
            }
            if (course.PhotoPath != courseEdit.PhotoPath)
            {
                course.PhotoPath = courseEdit.PhotoPath;
            }
            context.SaveChanges();
        }

        public Course GetCourseById(int id)
        {
            return context.Courses.Find(id);
        }


        public void Subscribe(Subscription sub)
        {            
            context.Subscriptions.Add(sub);
            context.SaveChanges();
        }

        public void Unsubscribe(string userId, int courseId)
        {
            Subscription sub = context.Subscriptions.Where(x => x.UserId.Equals(userId) &&
            x.CourseId.Equals(courseId)) as Subscription;
            context.Subscriptions.Remove(sub);
            context.SaveChanges();
        }
    }
}
