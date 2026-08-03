

namespace InnoClinic.Auth.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(string userId, string email);
    }
}
