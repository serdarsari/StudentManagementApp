using FluentValidation;
using StudentManagement.Service.Core.Features.Commands.CreateStudent;

namespace StudentManagementApp.API.Validations.StudentValidations
{
    public class CreateStudentQueryValidator : AbstractValidator<CreateStudentCommand>
    {
        public CreateStudentQueryValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.EmergencyCall).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.ClassBranch).NotEmpty();
            RuleFor(x => x.Grade).GreaterThanOrEqualTo(1);
            RuleFor(x => x.Email).NotEmpty();
        }
    }
}
