using FluentValidation;
using StudentSpy.Core.Requests;

namespace StudentSpy.Core.Validators
{
    public class UnsubscribeRequestValidator : AbstractValidator<UnsubscribeRequest>
    {
        public UnsubscribeRequestValidator()
        {
            RuleFor(unsub => unsub.CourseId).NotNull().NotEmpty();
        }
    }
}
