using StudentManagement.Entity;
using StudentManagement.Service.TeacherService;
using StudentManagement.Service.ExamProcedureService;
using StudentManagement.Service.StudentService;
using StudentManagement.Service.LessonService;
using StudentManagement.Service.ManagerService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<StudentManagementAppDbContext>();
builder.Services.AddTransient<ITeacherService, TeacherService>();
builder.Services.AddTransient<IExamProcedureService, ExamProcedureService>();
builder.Services.AddTransient<IStudentService, StudentService>();
builder.Services.AddTransient<ILessonService, LessonService>();
builder.Services.AddTransient<IManagerService, ManagerService>();


builder.Services.AddControllers();
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
