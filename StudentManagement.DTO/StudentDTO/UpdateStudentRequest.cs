﻿
namespace StudentManagement.DTO.StudentDTO
{
    public class UpdateStudentRequest
    {
        public string EmergencyCall { get; set; }
        public string Address { get; set; }
        public string ClassBranch { get; set; }
        public double GPA { get; set; }
    }
}
