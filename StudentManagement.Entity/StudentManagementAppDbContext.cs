using Microsoft.EntityFrameworkCore;

namespace StudentManagement.Entity
{
    public class StudentManagementAppDbContext : DbContext
    {
        public StudentManagementAppDbContext(DbContextOptions<StudentManagementAppDbContext> options):base(options){}

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<ExamResult> ExamResults { get; set; }
        public DbSet<ParentStudent> ParentStudent { get; set; }
        public DbSet<StudentTeacher> StudentTeacher { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Log> Logs { get; set; }
    }
}
