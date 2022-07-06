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
using StudentManagement.Service.Core.Features.Commands.CreateStudent;
using StudentManagement.Service.Core.Features.Commands.UpdateStudent;
using StudentManagement.Service.Core.Features.Queries.GetStudents;
using StudentManagement.Service.Core.Features.Commands.CreateParent;
using StudentManagement.Service.Core.Features.Commands.AssignSingleStudentToParent;
using StudentManagement.Service.Core.Features.Commands.CreateManager;
using StudentManagement.Service.Core.Features.Commands.CreateLesson;
using StudentManagement.Service.Core.Features.Commands.EnterStudentExamScore;
using StudentManagement.Service.Core.Features.Queries.GetParents;

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
builder.Services.AddTransient<IUserService, UserService>();

//Fluent Validation
//Teacher
builder.Services.AddTransient<IValidator<CreateTeacherCommand>, CreateTeacherQueryValidator>();
builder.Services.AddTransient<IValidator<UpdateTeacherCommand>, UpdateTeacherQueryValidator>();
builder.Services.AddTransient<IValidator<GetTeachersQuery>, GetTeachersQueryValidator>();
builder.Services.AddTransient<IValidator<AssignMultipleStudentToTeacherCommand>, AssignMultipleStudentToTeacherQueryValidator>();
//Student
builder.Services.AddTransient<IValidator<CreateStudentCommand>, CreateStudentQueryValidator>();
builder.Services.AddTransient<IValidator<UpdateStudentCommand>, UpdateStudentQueryValidator>();
builder.Services.AddTransient<IValidator<GetStudentsQuery>, GetStudentsQueryValidator>();
//Manager
builder.Services.AddTransient<IValidator<CreateManagerCommand>, CreateManagerCommandValidator>();
//Lesson
builder.Services.AddTransient<IValidator<CreateLessonCommand>, CreateLessonCommandValidator>();
//ExamProcedure
builder.Services.AddTransient<IValidator<EnterStudentExamScoreCommand>, EnterStudentExamScoreCommandValidator>();
//Parent
builder.Services.AddTransient<IValidator<GetParentsQuery>, GetParentsQueryValidator>();
builder.Services.AddTransient<IValidator<CreateParentCommand>, CreateParentCommandValidator>();
builder.Services.AddTransient<IValidator<AssignSingleStudentToParentCommand>, AssignSingleStudentToParentCommandValidator>();




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
