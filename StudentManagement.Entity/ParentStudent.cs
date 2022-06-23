using System.ComponentModel.DataAnnotations;


namespace StudentManagement.Entity
{
    public class ParentStudent
    {
        [Key]
        public int Id { get; set; }
        public int ParentId { get; set; }
        public Parent Parent { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
