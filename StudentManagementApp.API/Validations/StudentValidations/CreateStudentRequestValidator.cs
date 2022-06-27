using FluentValidation;
using StudentManagement.DTO.StudentDTO;

namespace StudentManagementApp.API.Validations.StudentValidations
{
    public class CreateStudentRequestValidator : AbstractValidator<CreateStudentRequest>
    {
        public CreateStudentRequestValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.EmergencyCall).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.ClassBranch).NotEmpty();
            RuleFor(x => x.Grade).GreaterThanOrEqualTo(1);
        }
    }
}
