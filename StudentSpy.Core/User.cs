using Microsoft.AspNetCore.Identity;

namespace StudentSpy.Core
{
    public class User: IdentityUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}