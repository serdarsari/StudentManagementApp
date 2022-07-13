using StudentManagement.Entity;
using System.Reflection;
using FluentValidation.AspNetCore;
using FluentValidation;
using StudentManagementApp.API.Validations.TeacherValidations;
using StudentManagementApp.API.Validations.StudentValidations;
using StudentManagementApp.API.Validations.ManagerValidations;
using StudentManagementApp.API.Validations.LessonValidations;
using StudentManagementApp.API.Validations.ExamProcedureValidations;
using StudentManagementApp.API.Validations.ParentValidations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using StudentManagement.Service.LoggerService;
using StudentManagement.Service.Core;
using MediatR;
using StudentManagement.Service.Core.Features.Commands.AssignMultipleStudentToTeacher;
using StudentManagement.Service.Core.Features.Commands.CreateTeacher;
using StudentManagement.Service.Core.Features.Queries.GetTeachers;
using StudentManagement.Service.Core.Features.Commands.UpdateTeacher;
using StudentManagement.Service.Core.Features.Commands.CreateStudent;
using StudentManagement.Service.Core.Features.Commands.UpdateStudent;
using StudentManagement.Service.Core.Features.Queries.GetStudents;
using StudentManagement.Service.Core.Features.Commands.CreateParent;
using StudentManagement.Service.Core.Features.Commands.AssignSingleStudentToParent;
using StudentManagement.Service.Core.Features.Commands.CreateManager;
using StudentManagement.Service.Core.Features.Commands.CreateLesson;
using StudentManagement.Service.Core.Features.Commands.EnterStudentExamScore;
using StudentManagement.Service.Core.Features.Queries.GetParents;
using StudentManagement.Service.Core.IConfigurationRepository;
using StudentManagement.Service.TokenService;
using TokenHandler = StudentManagement.Service.TokenService.TokenHandler;
using StudentManagementApp.API.Authorization;
using StudentManagementApp.API.Validations.UserValidations;
using StudentManagement.Service.Core.Features.Commands.CreateToken;
using StudentManagement.Service.Core.Features.Commands.CreateUser;
using StudentManagement.Service.Core.Features.Commands.DeleteStudent;
using StudentManagement.Service.Core.Features.Commands.DeleteTeacher;
using StudentManagement.Service.Core.Features.Commands.RefreshToken;
using StudentManagement.Service.Core.Features.Queries.GetExamResultsByStudent;
using StudentManagement.Service.Core.Features.Queries.GetLessonsByTeacher;
using StudentManagement.Service.Core.Features.Queries.GetParentDetail;
using StudentManagement.Service.Core.Features.Queries.GetLessons;
using StudentManagement.Service.Core.Features.Queries.GetParentsByTeacher;
using StudentManagement.Service.Core.Features.Queries.GetStudentDetail;
using StudentManagement.Service.Core.Features.Queries.GetStudentsByLesson;
using StudentManagement.Service.Core.Features.Queries.GetstudentsByTeacher;
using StudentManagement.Service.Core.Features.Queries.GetTeacherDetail;

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

builder.Services.AddCors();

//Dependency Injection
builder.Services.AddSingleton<ILoggerService, FileLoggerService>();

//Fluent Validation
//Teacher
builder.Services.AddTransient<IValidator<CreateTeacherCommand>, CreateTeacherQueryValidator>();
builder.Services.AddTransient<IValidator<UpdateTeacherCommand>, UpdateTeacherCommandValidator>();
builder.Services.AddTransient<IValidator<GetTeachersQuery>, GetTeachersQueryValidator>();
builder.Services.AddTransient<IValidator<AssignMultipleStudentToTeacherCommand>, AssignMultipleStudentToTeacherCommandValidator>();
builder.Services.AddTransient<IValidator<DeleteTeacherCommand>, DeleteTeacherCommandValidator>();
builder.Services.AddTransient<IValidator<GetTeacherDetailQuery>, GetTeacherDetailQueryValidator>();
//Student
builder.Services.AddTransient<IValidator<GetStudentDetailQuery>, GetStudentDetailQueryValidator>();
builder.Services.AddTransient<IValidator<CreateStudentCommand>, CreateStudentQueryValidator>();
builder.Services.AddTransient<IValidator<UpdateStudentCommand>, UpdateStudentCommandValidator>();
builder.Services.AddTransient<IValidator<GetStudentsQuery>, GetStudentsQueryValidator>();
builder.Services.AddTransient<IValidator<DeleteStudentCommand>, DeleteStudentCommandValidator>();
builder.Services.AddTransient<IValidator<GetStudentsByLessonQuery>, GetStudentsByLessonQueryValidator>();
builder.Services.AddTransient<IValidator<GetStudentsByTeacherQuery>, GetStudentsByTeacherQueryValidator>();
//Manager
builder.Services.AddTransient<IValidator<CreateManagerCommand>, CreateManagerCommandValidator>();
//Lesson
builder.Services.AddTransient<IValidator<CreateLessonCommand>, CreateLessonCommandValidator>();
builder.Services.AddTransient<IValidator<GetLessonsByTeacherQuery>, GetLessonsByTeacherQueryValidator>();
builder.Services.AddTransient<IValidator<GetLessonsQuery>, GetLessonsQueryValidator>();
//ExamProcedure
builder.Services.AddTransient<IValidator<EnterStudentExamScoreCommand>, EnterStudentExamScoreCommandValidator>();
builder.Services.AddTransient<IValidator<GetExamResultsByStudentQuery>, GetExamResultsByStudentQueryValidator>();
//Parent
builder.Services.AddTransient<IValidator<GetParentsQuery>, GetParentsQueryValidator>();
builder.Services.AddTransient<IValidator<CreateParentCommand>, CreateParentCommandValidator>();
builder.Services.AddTransient<IValidator<AssignSingleStudentToParentCommand>, AssignSingleStudentToParentCommandValidator>();
builder.Services.AddTransient<IValidator<GetParentDetailQuery>, GetParentDetailQueryValidator>();
builder.Services.AddTransient<IValidator<GetParentsByTeacherQuery>, GetParentsByTeacherQueryValidator>();
//User
builder.Services.AddTransient<IValidator<CreateTokenCommand>, CreateTokenCommandValidator>();
builder.Services.AddTransient<IValidator<CreateUserCommand>, CreateUserCommandValidator>();
builder.Services.AddTransient<IValidator<RefreshTokenCommand>, RefreshTokenCommandValidator>();

builder.Services.AddScoped<IJwtUtils, TokenHandler>();




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

app.UseCors(
        options =>
        {
            options.AllowAnyOrigin();
            options.AllowAnyMethod();
            options.AllowAnyHeader();
        }
    );

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
