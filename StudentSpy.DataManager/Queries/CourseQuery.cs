using StudentSpy.Core;
using StudentSpy.DataManager.Data;

namespace StudentSpy.DataManager.Queries
{
    public class CourseQuery
    {
        private readonly AppDbContext context;

        public CourseQuery(AppDbContext ctx)
        {
            context = ctx;
        }

        public IList<Course> GetAllCourses()
        {
            return context.Courses.ToList();
        }

        public Course GetCourseById(int id)
        {
            return context.Courses.Find(id);
        }

        public List<Subscription> GetUserSubscription(string userId)
        {
            return context.Subscriptions.Where(x => x.UserId == userId).ToList();
        }

        public IList<Course> GetSubscribed(string userId)
        {
            var subs = new List<Course>();
            var userCourses = GetUserSubscription(userId);
            foreach (var cs in userCourses)
            {
                subs.AddRange(context.Courses.Where(i => i.Id == cs.CourseId));
            }
            return subs;
        }

        public IList<Course> GetUnSubscribed(string userId)
        {
            var notSubs = new List<Course>();
            var userCourses = GetUserSubscription(userId);
            if (userCourses.Capacity == 0)
            {
                notSubs.AddRange(GetAllCourses());
            }
            else
            {
                foreach (var cs in userCourses)
                {
                    notSubs.AddRange(context.Courses.Where(i => i.Id != cs.CourseId));
                }
            }
            return notSubs;
        }
    }
}
