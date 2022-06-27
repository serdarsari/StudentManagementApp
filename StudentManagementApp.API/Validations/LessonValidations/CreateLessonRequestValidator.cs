using FluentValidation;
using StudentManagement.DTO.LessonDTO;

namespace StudentManagementApp.API.Validations.LessonValidations
{
    public class CreateLessonRequestValidator : AbstractValidator<CreateLessonRequest>
    {
        public CreateLessonRequestValidator()
        {
            RuleFor(x => x.LessonCode).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
