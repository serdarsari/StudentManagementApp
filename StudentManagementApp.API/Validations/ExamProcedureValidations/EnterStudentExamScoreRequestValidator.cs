using FluentValidation;
using StudentManagement.DTO.ExamProcedureDTO;

namespace StudentManagementApp.API.Validations.ExamProcedureValidations
{
    public class EnterStudentExamScoreRequestValidator : AbstractValidator<EnterStudentExamScoreRequest>
    {
        public EnterStudentExamScoreRequestValidator()
        {
            RuleFor(x => x.StudentId).GreaterThanOrEqualTo(1);
            RuleFor(x => x.LessonId).GreaterThanOrEqualTo(1);
            RuleFor(x=> x.ExamScore).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100);
        }
    }
}
