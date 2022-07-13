using FluentValidation;
using StudentManagement.Service.Core.Features.Commands.CreateUser;

namespace StudentManagementApp.API.Validations.UserValidations
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
