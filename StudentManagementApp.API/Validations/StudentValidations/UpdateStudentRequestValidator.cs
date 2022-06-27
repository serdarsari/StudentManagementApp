using FluentValidation;
using StudentManagement.DTO.StudentDTO;

namespace StudentManagementApp.API.Validations.StudentValidations
{
    public class UpdateStudentRequestValidator : AbstractValidator<UpdateStudentRequest>
    {
        public UpdateStudentRequestValidator()
        {
            RuleFor(x => x.EmergencyCall).NotEmpty();
            RuleFor(x => x.ClassBranch).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.GPA).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100);
        }
    }
}
