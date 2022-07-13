using StudentManagement.Service.Core.IConfigurationRepository;
using StudentManagement.Service.TokenService;

namespace StudentManagementApp.API.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUnitOfWork unitOfWork, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateJwtToken(token);
            if (userId != null)
            {
                context.Items["User"] = await unitOfWork.Users.GetByIdAsync(userId.Value);
            }

            await _next(context);
        }
    }
}
