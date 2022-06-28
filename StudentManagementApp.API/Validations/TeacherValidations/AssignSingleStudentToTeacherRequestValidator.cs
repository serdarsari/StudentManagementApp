using FluentValidation;
using StudentManagement.DTO.TeacherDTO;

namespace StudentManagementApp.API.Validations.TeacherValidations
{
    public class AssignSingleStudentToTeacherRequestValidator : AbstractValidator<AssignSingleStudentToTeacherRequest>
    {
        public AssignSingleStudentToTeacherRequestValidator()
        {
            RuleFor(x => x.StudentId).GreaterThanOrEqualTo(1);
            RuleFor(x => x.TeacherId).GreaterThanOrEqualTo(1);
        }
    }
}
