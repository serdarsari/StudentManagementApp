using StudentManagement.Entity;

namespace StudentManagement.Service.TokenService
{
    public interface IJwtUtils
    {
        Token CreateAccessToken(User user);
        string CreateRefreshToken();
        int? ValidateJwtToken(string token);
    }
}
