
using StudentManagement.DTO.BaseClasses;

namespace StudentManagement.DTO.ExamProcedureDTO
{
	public class GetExamResultsByStudentResponse : BaseResponse
	{
        public double GPA { get; set; }
        public List<ExamResultsByStudent> ExamResults { get; set; }
	}
}
