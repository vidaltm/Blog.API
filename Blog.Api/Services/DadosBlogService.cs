using Blog.Api.Models;
using Blog.Api.Repositories.Interfaces;
using Blog.Api.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Api.Services
{
    public class DadosBlogService : IDadosBlogService
    {
        private readonly IDadosBlogRepository _dadosBlogRepository;
        public DadosBlogService(IDadosBlogRepository dadosBlogRepository)
        {
            _dadosBlogRepository = dadosBlogRepository;
        }

        public async Task<List<DadosBlog>> GetAll()
        {
            return await _dadosBlogRepository.GetAllAsync();
        }
        public async Task<DadosBlog> GetById(int id)
        {
            return await _dadosBlogRepository.GetByIdAsync(id);
        }
        public async Task Post(DadosBlog dadosBlog)
        {
            await _dadosBlogRepository.PostAsync(dadosBlog);
        }
        public async Task Put(DadosBlog dadosBlog)
        {
            await _dadosBlogRepository.PutAsync(dadosBlog);
        }
        public async Task Delete(DadosBlog dadosBlog)
        {
            await _dadosBlogRepository.DeleteAsync(dadosBlog);
        }
    }
}
