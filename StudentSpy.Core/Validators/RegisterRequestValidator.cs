using FluentValidation;
using StudentSpy.Core.Requests;

namespace StudentSpy.Core.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(register => register.Name).NotNull().NotEmpty();
            RuleFor(register => register.LastName).NotNull().NotEmpty();
            RuleFor(register => register.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(register => register.Password).NotNull().NotEmpty();
            RuleFor(register => register.Age).NotNull().NotEmpty();
        }
    }
}
