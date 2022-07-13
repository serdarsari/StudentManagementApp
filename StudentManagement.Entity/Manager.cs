﻿using System.ComponentModel.DataAnnotations;


namespace StudentManagement.Entity
{
    public class Manager
    {
        [Key]
        public int Id { get; set; }
        public string RegistrationNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }

    }
}
