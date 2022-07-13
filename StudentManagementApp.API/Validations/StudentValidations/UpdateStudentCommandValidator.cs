using FluentValidation;
using StudentManagement.Service.Core.Features.Commands.UpdateStudent;

namespace StudentManagementApp.API.Validations.StudentValidations
{
    public class UpdateStudentCommandValidator : AbstractValidator<UpdateStudentCommand>
    {
        public UpdateStudentCommandValidator()
        {
            RuleFor(x => x.EmergencyCall).NotEmpty();
            RuleFor(x => x.ClassBranch).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.GPA).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100);
        }
    }
}
