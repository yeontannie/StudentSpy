using FluentValidation;
using StudentSpy.Core.Requests;

namespace StudentSpy.Core.Validators
{
    public class SubRequestValidation : AbstractValidator<SubscriptionRequest>
    {
        public SubRequestValidation()
        {
            RuleFor(subscription => subscription.Token).NotNull();
            RuleFor(subscription => subscription.CourseId).NotNull();
            RuleFor(subscription => subscription.StartDate).NotNull();
        }
    }
}
