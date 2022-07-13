using FluentValidation;
using StudentManagement.Service.Core.Features.Commands.UpdateTeacher;

namespace StudentManagementApp.API.Validations.TeacherValidations
{
    public class UpdateTeacherCommandValidator : AbstractValidator<UpdateTeacherCommand>
    {
        public UpdateTeacherCommandValidator()
        {
            RuleFor(x => x.PhoneNumber).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.Profession).NotEmpty();
        }
    }
}
