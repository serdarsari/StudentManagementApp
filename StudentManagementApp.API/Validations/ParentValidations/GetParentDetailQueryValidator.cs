using FluentValidation;
using StudentManagement.Service.Core.Features.Queries.GetParentDetail;

namespace StudentManagementApp.API.Validations.ParentValidations
{
    public class GetParentDetailQueryValidator : AbstractValidator<GetParentDetailQuery>
    {
        public GetParentDetailQueryValidator()
        {
            RuleFor(x => x.ParentId).GreaterThanOrEqualTo(1);
        }
    }
}
