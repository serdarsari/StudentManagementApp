using FluentValidation;
using StudentManagement.Service.Core.Features.Queries.GetParents;

namespace StudentManagementApp.API.Validations.ParentValidations
{
    public class GetParentsQueryValidator : AbstractValidator<GetParentsQuery>
    {
        public GetParentsQueryValidator()
        {
            RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1);
            RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);
        }
    }
}
