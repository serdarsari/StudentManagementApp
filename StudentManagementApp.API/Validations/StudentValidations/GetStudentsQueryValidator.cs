using FluentValidation;
using StudentManagement.Service.Core.Features.Queries.GetStudents;

namespace StudentManagementApp.API.Validations.StudentValidations
{
    public class GetStudentsQueryValidator : AbstractValidator<GetStudentsQuery>
    {
        public GetStudentsQueryValidator()
        {
            RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1);
            RuleFor(x=> x.PageNumber).GreaterThanOrEqualTo(1);
        }
    }
}
