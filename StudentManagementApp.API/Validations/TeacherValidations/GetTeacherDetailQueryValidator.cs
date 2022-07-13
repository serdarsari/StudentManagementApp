using FluentValidation;
using StudentManagement.Service.Core.Features.Queries.GetTeacherDetail;

namespace StudentManagementApp.API.Validations.TeacherValidations
{
    public class GetTeacherDetailQueryValidator : AbstractValidator<GetTeacherDetailQuery>
    {
        public GetTeacherDetailQueryValidator()
        {
            RuleFor(x => x.TeacherId).GreaterThanOrEqualTo(1);
        }
    }
}
