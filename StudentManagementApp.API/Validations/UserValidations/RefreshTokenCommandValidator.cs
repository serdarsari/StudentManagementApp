using FluentValidation;
using StudentManagement.Service.Core.Features.Commands.RefreshToken;

namespace StudentManagementApp.API.Validations.UserValidations
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(x => x.RefreshToken).NotEmpty();
        }
    }
}
