using FluentValidation;
using StudentManagement.DTO.ParentDTO;

namespace StudentManagementApp.API.Validations.ParentValidations
{
    public class AssignSingleStudentToParentRequestValidator : AbstractValidator<AssignSingleStudentToParentRequest>
    {
        public AssignSingleStudentToParentRequestValidator()
        {
            RuleFor(x => x.ParentId).GreaterThanOrEqualTo(1);
            RuleFor(x => x.StudentId).GreaterThanOrEqualTo(1);
        }
    }
}
