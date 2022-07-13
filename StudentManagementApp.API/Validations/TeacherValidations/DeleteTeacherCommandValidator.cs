using FluentValidation;
using StudentManagement.Service.Core.Features.Commands.DeleteTeacher;

namespace StudentManagementApp.API.Validations.TeacherValidations
{
    public class DeleteTeacherCommandValidator : AbstractValidator<DeleteTeacherCommand>
    {
        public DeleteTeacherCommandValidator()
        {
            RuleFor(x => x.TeacherId).GreaterThanOrEqualTo(1);
        }
    }
}
