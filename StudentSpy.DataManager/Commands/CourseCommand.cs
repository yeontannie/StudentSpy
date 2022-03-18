using StudentSpy.Core;
using StudentSpy.DataManager.Data;
using StudentSpy.DataManager.Helpers;
using StudentSpy.DataManager.Queries;

namespace StudentSpy.DataManager.Commands
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

        public string Delete(int courseId)
        {
            var subs = context.Subscriptions.Where(i => i.CourseId == courseId).ToList();
            if(subs.Capacity == 0)
            {
                context.Courses.Remove(courseQue.GetCourseById(courseId));
                context.SaveChanges();
                return "Deleted successfully";
            }
            return "Can't delete course with subscriptions";

        }

        public void Edit(Course model, int id)
        {
            var course = context.Courses.Find(id);
            if (course.Name != model.Name.Trim())
            {
                course.Name = model.Name.Trim();
            }
            if (course.Description != model.Description.Trim())
            {
                course.Description = model.Description.Trim();
            }
            if (course.Duration != model.Duration)
            {
                course.Duration = model.Duration;
            }
            if (course.PhotoPath != model.PhotoPath)
            {
                if (!string.IsNullOrEmpty(model.PhotoPath))
                {
                    course.PhotoPath = model.PhotoPath;
                }                
            }
            context.Courses.Update(course);
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
