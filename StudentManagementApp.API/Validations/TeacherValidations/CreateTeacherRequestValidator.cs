using FluentValidation;
using StudentManagement.DTO.TeacherDTO;

namespace StudentManagementApp.API.Validations.TeacherValidations
{
    public class CreateTeacherRequestValidator : AbstractValidator<CreateTeacherRequest>
    {
        public CreateTeacherRequestValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty();
            RuleFor(x => x.Address).NotEmpty();
            RuleFor(x => x.Profession).NotEmpty();
            RuleFor(x => x.LessonId).GreaterThan(0);
            RuleFor(x => x.Birthday).LessThan(DateTime.Today);
        }
    }
}
