using FluentValidation;
using StudentManagement.Service.Core.Features.Commands.CreateLesson;

namespace StudentManagementApp.API.Validations.LessonValidations
{
    public class CreateLessonCommandValidator : AbstractValidator<CreateLessonCommand>
    {
        public CreateLessonCommandValidator()
        {
            RuleFor(x => x.LessonCode).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
        }
    }
}
