using FluentValidation;
using StudentManagement.DTO.TeacherDTO;

namespace StudentManagementApp.API.Validations.TeacherValidations
{
    public class UpdateTeacherRequestValidator : AbstractValidator<UpdateTeacherRequest>
    {
        public UpdateTeacherRequestValidator()
        {
            RuleFor(x => x.PhoneNumber).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.Profession).NotEmpty();
        }
    }
}
