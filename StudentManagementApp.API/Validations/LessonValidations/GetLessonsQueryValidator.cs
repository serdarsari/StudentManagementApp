using FluentValidation;
using StudentManagement.Service.Core.Features.Queries.GetLessons;

namespace StudentManagementApp.API.Validations.LessonValidations
{
    public class GetLessonsQueryValidator : AbstractValidator<GetLessonsQuery>
    {
        public GetLessonsQueryValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1);
        }
    }
}
