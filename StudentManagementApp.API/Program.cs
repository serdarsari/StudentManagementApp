using StudentManagement.Entity;
using StudentManagement.Service.TeacherService;
using StudentManagement.Service.ExamProcedureService;
using StudentManagement.Service.StudentService;
using StudentManagement.Service.LessonService;
using StudentManagement.Service.ManagerService;
using System.Reflection;
using FluentValidation.AspNetCore;
using FluentValidation;
using StudentManagementApp.API.Validations.TeacherValidations;
using StudentManagement.DTO.TeacherDTO;
using StudentManagement.DTO.StudentDTO;
using StudentManagementApp.API.Validations.StudentValidations;
using StudentManagement.DTO.ManagerDTO;
using StudentManagementApp.API.Validations.ManagerValidations;
using StudentManagement.DTO.LessonDTO;
using StudentManagementApp.API.Validations.LessonValidations;
using StudentManagement.DTO.ExamProcedureDTO;
using StudentManagementApp.API.Validations.ExamProcedureValidations;
using StudentManagement.Service.ParentService;
using StudentManagement.DTO.ParentDTO;
using StudentManagementApp.API.Validations.ParentValidations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//DbContext
builder.Services.AddDbContext<StudentManagementAppDbContext>();

//Dependency Injection
builder.Services.AddTransient<ITeacherService, TeacherService>();
builder.Services.AddTransient<IExamProcedureService, ExamProcedureService>();
builder.Services.AddTransient<IStudentService, StudentService>();
builder.Services.AddTransient<ILessonService, LessonService>();
builder.Services.AddTransient<IManagerService, ManagerService>();
builder.Services.AddTransient<IParentService, ParentService>();

//Fluent Validation
//Teacher
builder.Services.AddTransient<IValidator<CreateTeacherRequest>, CreateTeacherRequestValidator>();
builder.Services.AddTransient<IValidator<UpdateTeacherRequest>, UpdateTeacherRequestValidator>();
builder.Services.AddTransient<IValidator<GetTeachersRequest>, GetTeachersRequestValidator>();
builder.Services.AddTransient<IValidator<AssignSingleStudentToTeacherRequest>, AssignSingleStudentToTeacherRequestValidator>();
builder.Services.AddTransient<IValidator<AssignMultipleStudentToTeacherRequest>, AssignMultipleStudentToTeacherRequestValidator>();
//Student
builder.Services.AddTransient<IValidator<CreateStudentRequest>, CreateStudentRequestValidator>();
builder.Services.AddTransient<IValidator<UpdateStudentRequest>, UpdateStudentRequestValidator>();
builder.Services.AddTransient<IValidator<GetStudentsRequest>, GetStudentsRequestValidator>();
//Manager
builder.Services.AddTransient<IValidator<CreateManagerRequest>, CreateManagerRequestValidator>();
//Lesson
builder.Services.AddTransient<IValidator<CreateLessonRequest>, CreateLessonRequestValidator>();
//ExamProcedure
builder.Services.AddTransient<IValidator<EnterStudentExamScoreRequest>, EnterStudentExamScoreRequestValidator>();
//Parent
builder.Services.AddTransient<IValidator<CreateParentRequest>, CreateParentRequestValidator>();
builder.Services.AddTransient<IValidator<AssignSingleStudentToParentRequest>, AssignSingleStudentToParentRequestValidator>();




builder.Services.AddControllers().AddFluentValidation(i => i.DisableDataAnnotationsValidation = true);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
