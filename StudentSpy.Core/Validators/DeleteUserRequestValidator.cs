using FluentValidation;
using StudentSpy.Core.Requests;

namespace StudentSpy.Core.Validators
{
    public class DeleteUserRequestValidator : AbstractValidator<DeleteUserRequest>
    {
        public DeleteUserRequestValidator()
        {
            RuleFor(user => user.User).NotNull().NotEmpty();
        }        
    }
}
