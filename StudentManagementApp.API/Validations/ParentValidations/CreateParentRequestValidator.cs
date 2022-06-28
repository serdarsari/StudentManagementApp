using FluentValidation;
using StudentManagement.DTO.ParentDTO;

namespace StudentManagementApp.API.Validations.ParentValidations
{
    public class CreateParentRequestValidator : AbstractValidator<CreateParentRequest>
    {
        public CreateParentRequestValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
        }
    }
}
