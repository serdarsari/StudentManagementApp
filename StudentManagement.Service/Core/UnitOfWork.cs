using AutoMapper;
using StudentManagement.Entity;
using StudentManagement.Service.Core.IConfigurationRepository;
using StudentManagement.Service.Core.IRepositories;
using StudentManagement.Service.Core.Repositories;
using StudentManagement.Service.LoggerService;

namespace StudentManagement.Service.Core
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly StudentManagementAppDbContext _dbContext;
        private readonly ILoggerService _loggerService;
        private readonly IMapper _mapper;

        public ITeacherRepository Teachers { get; private set; }

        public IStudentRepository Students { get; private set; }

        public IParentRepository Parents { get; private set; }

        public IManagerRepository Managers { get; private set; }

        public ILessonRepository Lessons { get; private set; }

        public IExamProcedureRepository ExamProcedures { get; private set; }

        public IUserRepository Users { get; private set; }

        public ILessonStudentRepository LessonStudent { get; private set; }

        public UnitOfWork(StudentManagementAppDbContext dbContext, ILoggerService loggerService, IMapper mapper)
        {
            _dbContext = dbContext;
            _loggerService = loggerService;
            _mapper = mapper;

            Teachers = new TeacherRepository(_dbContext, _loggerService);
            Students = new StudentRepository(_dbContext, _loggerService);
            Parents = new ParentRepository(_dbContext, _loggerService);
            Managers = new ManagerRepository(_dbContext, _loggerService);
            Lessons = new LessonRepository(_dbContext, _loggerService);
            ExamProcedures = new ExamProcedureRepository(_dbContext, _loggerService, _mapper);
            Users = new UserRepository(_dbContext, _loggerService);
            LessonStudent = new LessonStudentRepository(_dbContext, _loggerService);
        }

        public async Task CompleteAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
