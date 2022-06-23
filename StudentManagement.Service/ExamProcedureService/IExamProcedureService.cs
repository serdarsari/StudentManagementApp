using StudentManagement.DTO.ExamProcedureDTO;

namespace StudentManagement.Service.ExamProcedureService
{
    public interface IExamProcedureService
    {
        Task<EnterStudentExamScoreResponse> EnterStudentExamScoreAsync(EnterStudentExamScoreRequest request);
    }
}
