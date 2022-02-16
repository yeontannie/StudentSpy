using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;

namespace StudentSpy.Core.Requests
{
    public class SubscriptionRequest
    {
        public int CourseId { get; set; }
        public DateTime StartDate { get; set; }
    }
}
