﻿
namespace StudentManagement.DTO.TeacherDTO
{
    public class UpdateTeacherRequest
    {
        public int TeacherId { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Profession { get; set; }
        public string Description { get; set; }
    }
}
