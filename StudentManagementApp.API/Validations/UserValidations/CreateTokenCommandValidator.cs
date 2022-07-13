using FluentValidation;
using StudentManagement.Service.Core.Features.Commands.CreateToken;

namespace StudentManagementApp.API.Validations.UserValidations
{
    public class CreateTokenCommandValidator : AbstractValidator<CreateTokenCommand>
    {
        public CreateTokenCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
            RuleFor(x => x.UserType).NotEmpty();
        }
    }
}
