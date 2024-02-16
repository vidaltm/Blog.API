using Blog.Api.Models;

namespace Blog.Api.Services.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(Usuario user);
    }
}
