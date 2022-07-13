using FluentValidation;
using StudentManagement.Service.Core.Features.Queries.GetStudentsByLesson;

namespace StudentManagementApp.API.Validations.StudentValidations
{
    public class GetStudentsByLessonQueryValidator : AbstractValidator<GetStudentsByLessonQuery>
    {
        public GetStudentsByLessonQueryValidator()
        {
            RuleFor(x => x.LessonId).GreaterThanOrEqualTo(1);
        }
    }
}
