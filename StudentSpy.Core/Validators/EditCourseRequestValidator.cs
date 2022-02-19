using FluentValidation;
using StudentSpy.Core.Requests;

namespace StudentSpy.Core.Validators
{
    public class EditCourseRequestValidator : AbstractValidator<EditCourseRequest>
    {
        public EditCourseRequestValidator()
        {
            RuleFor(edit => edit.CourseModel).NotNull().NotEmpty();
            RuleFor(edit => edit.CourseId).NotNull().NotEmpty();
        }
    }
}
