using FluentValidation;
using StudentManagement.Service.Core.Features.Queries.GetParentsByTeacher;

namespace StudentManagementApp.API.Validations.ParentValidations
{
    public class GetParentsByTeacherQueryValidator : AbstractValidator<GetParentsByTeacherQuery>
    {
        public GetParentsByTeacherQueryValidator()
        {
            RuleFor(x => x.TeacherId).GreaterThanOrEqualTo(1);
            RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1);
            RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);
        }
    }
}
