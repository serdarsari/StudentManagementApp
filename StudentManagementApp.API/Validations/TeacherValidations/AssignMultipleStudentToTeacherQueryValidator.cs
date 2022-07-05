using FluentValidation;
using StudentManagement.Service.Core.Features.Commands.AssignMultipleStudentToTeacher;

namespace StudentManagementApp.API.Validations.TeacherValidations
{
    public class AssignMultipleStudentToTeacherQueryValidator : AbstractValidator<AssignMultipleStudentToTeacherCommand>
    {
        public AssignMultipleStudentToTeacherQueryValidator()
        {
            RuleFor(x => x.TeacherId).GreaterThanOrEqualTo(1);
            RuleFor(x=> x.StudentIds).NotEmpty();
        }
    }
}
