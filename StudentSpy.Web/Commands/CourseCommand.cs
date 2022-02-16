using StudentSpy.Core;
using StudentSpy.Web.Data;
using StudentSpy.Web.Queries;

namespace StudentSpy.Web.Commands
{
    public class CourseCommand
    {
        private readonly AppDbContext context;
        private readonly CourseQuery courseQue;

        public CourseCommand(AppDbContext ctx, CourseQuery courseQ)
        {
            context = ctx;
            courseQue = courseQ;
        }

        public void Add(Course course)
        {
            context.Courses.Add(course);
            context.SaveChanges();
        }

        public void Delete(int courseId)
        {
            var subs = context.Subscriptions.Where(i => i.CourseId == courseId).ToList();
            if(subs.Capacity == 0)
            {
                context.Courses.Remove(courseQue.GetCourseById(courseId));
                context.SaveChanges();
            }
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

        public void Subscribe(Subscription sub)
        {
            context.Subscriptions.Add(sub);
            context.SaveChanges();
        }

        public void Unsubscribe(string userId, int courseId)
        {
            Subscription sub = context.Subscriptions.First(x => x.UserId == userId &&
            x.CourseId == courseId);
            context.Subscriptions.Remove(sub);
            context.SaveChanges();
        }
    }
}
