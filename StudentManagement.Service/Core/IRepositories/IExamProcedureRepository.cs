using StudentManagement.DTO.ExamProcedureDTO;
using StudentManagement.Entity;
using StudentManagement.Service.Core.Features.Commands.EnterStudentExamScore;

namespace StudentManagement.Service.Core.IRepositories
{
    public interface IExamProcedureRepository : IGenericRepository<ExamResult>
    {
        Task<EnterStudentExamScoreResponse> EnterStudentExamScoreAsync(EnterStudentExamScoreCommand request);
    }
}
