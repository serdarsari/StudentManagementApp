using FluentValidation;
using StudentManagement.Service.Core.Features.Queries.GetTeachers;

namespace StudentManagementApp.API.Validations.TeacherValidations
{
    public class GetTeachersRequestValidator : AbstractValidator<GetTeachersQuery>
    {
        public GetTeachersRequestValidator()
        {
            RuleFor(x=> x.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(x=> x.PageSize).GreaterThanOrEqualTo(1);
        }
    }
}
