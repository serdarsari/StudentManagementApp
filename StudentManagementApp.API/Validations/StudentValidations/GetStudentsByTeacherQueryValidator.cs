using FluentValidation;
using StudentManagement.Service.Core.Features.Queries.GetstudentsByTeacher;

namespace StudentManagementApp.API.Validations.StudentValidations
{
    public class GetStudentsByTeacherQueryValidator : AbstractValidator<GetStudentsByTeacherQuery>
    {
        public GetStudentsByTeacherQueryValidator()
        {
            RuleFor(x => x.TeacherId).GreaterThanOrEqualTo(1);
            RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1);
        }
    }
}
