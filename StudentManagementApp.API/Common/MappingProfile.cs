using AutoMapper;
using StudentManagement.DTO.ExamProcedureDTO;
using StudentManagement.DTO.ParentDTO;
using StudentManagement.DTO.StudentDTO;
using StudentManagement.DTO.TeacherDTO;
using StudentManagement.Entity;
using StudentManagement.Service.Core.Features.Commands.CreateLesson;
using StudentManagement.Service.Core.Features.Commands.CreateManager;
using StudentManagement.Service.Core.Features.Commands.CreateParent;
using StudentManagement.Service.Core.Features.Commands.CreateStudent;
using StudentManagement.Service.Core.Features.Commands.CreateTeacher;
using StudentManagement.Service.Core.Features.Commands.CreateUser;
using StudentManagement.Service.Core.Features.Commands.EnterStudentExamScore;

namespace StudentManagementApp.API.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Teacher
            CreateMap<Teacher, GetTeacherDetailResponse>();
            CreateMap<CreateTeacherCommand, Teacher>().ForMember(dest => dest.Age, opt => opt.MapFrom(src => DateTime.Now.Year - src.Birthday.Year));

            //Student
            CreateMap<Student, GetStudentDetailResponse>();
            CreateMap<CreateStudentCommand, Student>().ForMember(dest => dest.Age, opt => opt.MapFrom(src => DateTime.Now.Year - src.Birthday.Year));

            //Manager
            CreateMap<CreateManagerCommand, Manager>();

            //Lesson
            CreateMap<CreateLessonCommand, Lesson>();

            //ExamProcedure
            CreateMap<EnterStudentExamScoreCommand, ExamResult>();

            //Parent
            CreateMap<Parent, GetParentDetailResponse>();
            CreateMap<CreateParentCommand, Parent>();

            //User
            CreateMap<CreateUserCommand, User>();
        }
    }
}
