using FluentValidation;
using StudentManagement.Service.Core.Features.Commands.UpdateTeacher;

namespace StudentManagementApp.API.Validations.TeacherValidations
{
    public class UpdateTeacherRequestValidator : AbstractValidator<UpdateTeacherCommand>
    {
        public UpdateTeacherRequestValidator()
        {
            RuleFor(x => x.PhoneNumber).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.Profession).NotEmpty();
        }
    }
}
