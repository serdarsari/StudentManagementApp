
using StudentManagement.Entity.Enums;

namespace StudentManagement.DTO.BaseClasses
{
    public class TokenResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; }
        public Role Role { get; set; }
    }
}
