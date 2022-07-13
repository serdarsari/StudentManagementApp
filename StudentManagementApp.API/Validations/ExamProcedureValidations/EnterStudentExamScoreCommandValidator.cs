using FluentValidation;
using StudentManagement.Service.Core.Features.Commands.EnterStudentExamScore;

namespace StudentManagementApp.API.Validations.ExamProcedureValidations
{
    public class EnterStudentExamScoreCommandValidator : AbstractValidator<EnterStudentExamScoreCommand>
    {
        public EnterStudentExamScoreCommandValidator()
        {
            RuleFor(x => x.StudentId).GreaterThanOrEqualTo(1);
            RuleFor(x => x.LessonId).GreaterThanOrEqualTo(1);
            RuleFor(x=> x.Score).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100);
        }
    }
}
