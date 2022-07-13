using FluentValidation;
using StudentManagement.Service.Core.Features.Commands.DeleteStudent;

namespace StudentManagementApp.API.Validations.StudentValidations
{
    public class DeleteStudentCommandValidator : AbstractValidator<DeleteStudentCommand>
    {
        public DeleteStudentCommandValidator()
        {
            RuleFor(x => x.StudentId).GreaterThanOrEqualTo(1);
        }
    }
}
