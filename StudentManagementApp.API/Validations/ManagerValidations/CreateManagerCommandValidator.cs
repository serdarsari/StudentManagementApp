using FluentValidation;
using StudentManagement.Service.Core.Features.Commands.CreateManager;

namespace StudentManagementApp.API.Validations.ManagerValidations
{
    public class CreateManagerCommandValidator : AbstractValidator<CreateManagerCommand>
    {
        public CreateManagerCommandValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x=> x.LastName).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty();
        }
    }
}
