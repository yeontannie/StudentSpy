using Microsoft.EntityFrameworkCore;
using StudentSpy.Models;

namespace StudentSpy.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users {get; set;}
        public DbSet<Course> Courses {get; set; }
    }
}
