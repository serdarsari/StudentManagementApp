using FluentValidation;
using StudentManagement.DTO.ManagerDTO;

namespace StudentManagementApp.API.Validations.ManagerValidations
{
    public class CreateManagerRequestValidator : AbstractValidator<CreateManagerRequest>
    {
        public CreateManagerRequestValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x=> x.LastName).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty();
        }
    }
}
