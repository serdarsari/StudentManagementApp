using AutoMapper;
using StudentManagement.DTO.ExamProcedureDTO;
using StudentManagement.DTO.LessonDTO;
using StudentManagement.DTO.ManagerDTO;
using StudentManagement.DTO.StudentDTO;
using StudentManagement.DTO.TeacherDTO;
using StudentManagement.Entity;

namespace StudentManagementApp.API.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Teacher
            CreateMap<Teacher, GetTeacherDetailResponse>();
            CreateMap<CreateTeacherRequest, Teacher>().ForMember(dest => dest.Age, opt => opt.MapFrom(src => DateTime.Now.Year - src.Birthday.Year));

            //Student
            CreateMap<Student, GetStudentDetailResponse>();
            CreateMap<CreateStudentRequest, Student>().ForMember(dest => dest.Age, opt => opt.MapFrom(src => DateTime.Now.Year - src.Birthday.Year));

            //Manager
            CreateMap<CreateManagerRequest, Manager>();

            //Lesson
            CreateMap<CreateLessonRequest, Lesson>();

            //ExamProcedure
            CreateMap<EnterStudentExamScoreRequest, ExamResult>();
        }
    }
}
