using Blog.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Api.Services.Interfaces
{
    public interface IDadosBlogService
    {
        Task<List<DadosBlog>> GetAll();
        Task<DadosBlog> GetById(int id);
        Task Post(DadosBlog dadosBlog);
        Task Put(DadosBlog dadosBlog);
        Task Delete(DadosBlog dadosBlog);
    }
}
