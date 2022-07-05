using FluentValidation;
using StudentManagement.Service.Core.Features.Commands.AssignSingleStudentToParent;

namespace StudentManagementApp.API.Validations.ParentValidations
{
    public class AssignSingleStudentToParentCommandValidator : AbstractValidator<AssignSingleStudentToParentCommand>
    {
        public AssignSingleStudentToParentCommandValidator()
        {
            RuleFor(x => x.ParentId).GreaterThanOrEqualTo(1);
            RuleFor(x => x.StudentId).GreaterThanOrEqualTo(1);
        }
    }
}
