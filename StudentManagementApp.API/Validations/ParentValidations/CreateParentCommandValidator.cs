using FluentValidation;
using StudentManagement.Service.Core.Features.Commands.CreateParent;

namespace StudentManagementApp.API.Validations.ParentValidations
{
    public class CreateParentCommandValidator : AbstractValidator<CreateParentCommand>
    {
        public CreateParentCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
        }
    }
}
