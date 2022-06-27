using FluentValidation;
using StudentManagement.DTO.TeacherDTO;

namespace StudentManagementApp.API.Validations.TeacherValidations
{
    public class GetTeachersRequestValidator : AbstractValidator<GetTeachersRequest>
    {
        public GetTeachersRequestValidator()
        {
            RuleFor(x=> x.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(x=> x.PageSize).GreaterThanOrEqualTo(1);
        }
    }
}
