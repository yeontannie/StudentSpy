using FluentValidation;
using StudentSpy.Core.Requests;

namespace StudentSpy.Core.Validators
{
    public class EditUserRequestValidator : AbstractValidator<EditUserRequest>
    {
        public EditUserRequestValidator()
        {
            RuleFor(edit => edit.Name).NotNull().NotEmpty();
            RuleFor(edit => edit.LastName).NotNull().NotEmpty();
            RuleFor(edit => edit.Age).NotNull().NotEmpty();
            RuleFor(edit => edit.UserName).NotNull().NotEmpty();
        }
    }
}
