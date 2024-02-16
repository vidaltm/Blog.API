using Blog.Api.Models;

namespace Blog.Api.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Usuario Get(string userName, string password);
    }
}
