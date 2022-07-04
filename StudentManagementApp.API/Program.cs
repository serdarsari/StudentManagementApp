using StudentManagement.Entity;
using StudentManagement.Service.ExamProcedureService;
using StudentManagement.Service.StudentService;
using StudentManagement.Service.LessonService;
using StudentManagement.Service.ManagerService;
using System.Reflection;
using FluentValidation.AspNetCore;
using FluentValidation;
using StudentManagementApp.API.Validations.TeacherValidations;
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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using StudentManagement.Service.UserService;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Service.LoggerService;
using StudentManagement.Service.Core.IConfiguration;
using StudentManagement.Service.Core;
using MediatR;
using StudentManagement.Service.Core.Features.Commands.AssignMultipleStudentToTeacher;
using StudentManagement.Service.Core.Features.Commands.CreateTeacher;
using StudentManagement.Service.Core.Features.Queries.GetTeachers;
using StudentManagement.Service.Core.Features.Commands.UpdateTeacher;

var builder = WebApplication.CreateBuilder(args);

//JwtBearer
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Token:Issuer"],
        ValidAudience = builder.Configuration["Token:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        ClockSkew = TimeSpan.Zero
    };
});

// Add services to the container.
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

//DbContext
builder.Services.AddDbContext<StudentManagementAppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StudentManagementAppDbConnStr"), b => b.MigrationsAssembly("StudentManagementApp.API"));
});



//Dependency Injection
builder.Services.AddSingleton<ILoggerService, FileLoggerService>();
builder.Services.AddTransient<IExamProcedureService, ExamProcedureService>();
builder.Services.AddTransient<IStudentService, StudentService>();
builder.Services.AddTransient<ILessonService, LessonService>();
builder.Services.AddTransient<IManagerService, ManagerService>();
builder.Services.AddTransient<IParentService, ParentService>();
builder.Services.AddTransient<IUserService, UserService>();

//Fluent Validation
//Teacher
builder.Services.AddTransient<IValidator<CreateTeacherCommand>, CreateTeacherRequestValidator>();
builder.Services.AddTransient<IValidator<UpdateTeacherCommand>, UpdateTeacherRequestValidator>();
builder.Services.AddTransient<IValidator<GetTeachersQuery>, GetTeachersRequestValidator>();
builder.Services.AddTransient<IValidator<AssignMultipleStudentToTeacherCommand>, AssignMultipleStudentToTeacherRequestValidator>();
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

//UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
