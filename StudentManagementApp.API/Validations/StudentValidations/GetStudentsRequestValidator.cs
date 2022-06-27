using FluentValidation;
using StudentManagement.DTO.StudentDTO;

namespace StudentManagementApp.API.Validations.StudentValidations
{
    public class GetStudentsRequestValidator : AbstractValidator<GetStudentsRequest>
    {
        public GetStudentsRequestValidator()
        {
            RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1);
            RuleFor(x=> x.PageNumber).GreaterThanOrEqualTo(1);
        }
    }
}
