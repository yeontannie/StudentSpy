using FluentValidation;
using StudentSpy.Core.Requests;

namespace StudentSpy.Core.Validators
{
    public class SubRequestValidation : AbstractValidator<SubscriptionRequest>
    {
        public SubRequestValidation()
        {
            RuleFor(subscription => subscription.CourseId).NotNull().NotEmpty();
            RuleFor(subscription => subscription.StartDate).NotNull().NotEmpty();
        }
    }
}
