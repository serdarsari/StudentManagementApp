using FluentValidation;
using StudentManagement.DTO.TeacherDTO;

namespace StudentManagementApp.API.Validations.TeacherValidations
{
    public class AssignMultipleStudentToTeacherRequestValidator : AbstractValidator<AssignMultipleStudentToTeacherRequest>
    {
        public AssignMultipleStudentToTeacherRequestValidator()
        {
            RuleFor(x => x.TeacherId).GreaterThanOrEqualTo(1);
            RuleFor(x=> x.StudentIds).NotEmpty();
        }
    }
}
