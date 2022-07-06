
namespace StudentManagement.DTO.ParentDTO
{
    public class GetParentDetailResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
