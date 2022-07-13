using FluentValidation;
using StudentManagement.Service.Core.Features.Queries.GetLessonsByTeacher;

namespace StudentManagementApp.API.Validations.LessonValidations
{
    public class GetLessonsByTeacherQueryValidator : AbstractValidator<GetLessonsByTeacherQuery>
    {
        public GetLessonsByTeacherQueryValidator()
        {
            RuleFor(x => x.TeacherId).GreaterThanOrEqualTo(1);
            RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1);
        }
    }
}
