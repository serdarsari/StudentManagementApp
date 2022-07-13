using FluentValidation;
using StudentManagement.Service.Core.Features.Queries.GetStudentDetail;

namespace StudentManagementApp.API.Validations.StudentValidations
{
    public class GetStudentDetailQueryValidator : AbstractValidator<GetStudentDetailQuery>
    {
        public GetStudentDetailQueryValidator()
        {
            RuleFor(x => x.StudentId).GreaterThanOrEqualTo(1);
        }
    }
}
