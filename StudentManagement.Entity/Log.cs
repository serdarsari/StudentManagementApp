
using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Entity
{
    public class Log
    {
        [Key]
        public int Id { get; set; }
        public string? StackTrace { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
