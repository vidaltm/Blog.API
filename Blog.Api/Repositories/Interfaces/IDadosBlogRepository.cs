using Blog.Api.Data;
using Blog.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Api.Repositories.Interfaces
{
    public interface IDadosBlogRepository
    {
        Task<List<DadosBlog>> GetAllAsync();
        Task<DadosBlog> GetByIdAsync(int id);
        Task PostAsync(DadosBlog dadosBlog);
        Task PutAsync(DadosBlog dadosBlog);
        Task DeleteAsync(DadosBlog dadosBlog);
    }
}
