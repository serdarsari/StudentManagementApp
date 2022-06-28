using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.DTO.TeacherDTO
{
    public class AssignSingleStudentToTeacherRequest
    {
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
    }
}
