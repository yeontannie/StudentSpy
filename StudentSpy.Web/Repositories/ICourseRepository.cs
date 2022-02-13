using StudentSpy.Core;
using System.IdentityModel.Tokens.Jwt;

namespace StudentSpy.Web.Repositories
{
    public interface ICourseRepository
    {
        public void Add(Course course);
        public void Edit(Course courseEdit, int idToEdit);
        public void Delete(int courseId);
        public Course GetCourseById(int id);
        public string GetUserId(string token);
        public void Subscribe(Subscription sub);
        public void Unsubscribe(string userId, int courseId);
    }
}
