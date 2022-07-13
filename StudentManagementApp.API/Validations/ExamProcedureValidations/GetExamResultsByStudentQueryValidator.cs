using FluentValidation;
using StudentManagement.Service.Core.Features.Queries.GetExamResultsByStudent;

namespace StudentManagementApp.API.Validations.ExamProcedureValidations
{
    public class GetExamResultsByStudentQueryValidator : AbstractValidator<GetExamResultsByStudentQuery>
    {
        public GetExamResultsByStudentQueryValidator()
        {
            RuleFor(x => x.StudentId).GreaterThanOrEqualTo(1);
        }
    }
}
