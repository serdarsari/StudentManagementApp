using StudentManagement.DTO.BaseClasses;

namespace StudentManagement.DTO.UserDTO
{
    public class CreateTokenResponse : TokenResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
