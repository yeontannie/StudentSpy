using FluentValidation;
using StudentSpy.Core.Requests;

namespace StudentSpy.Core.Validators
{
    public class AuthRequestValidator : AbstractValidator<AuthRequest>
    {
        public AuthRequestValidator()
        {
            RuleFor(auth => auth.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(auth => auth.Password).NotNull().NotEmpty();
        }
    }
}
