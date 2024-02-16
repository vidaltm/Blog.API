using Blog.Api.Data;
using Blog.Api.Models;
using Blog.Api.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blog.Api.Repositories
{
    public class DadosBlogRepository : IDadosBlogRepository
    {
        AppDbContext context;
        public DadosBlogRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<List<DadosBlog>> GetAllAsync()
        {
            var dadosBlog = await context.DadosBlog.AsNoTracking().ToListAsync();
            return dadosBlog;
        }

        public async Task<DadosBlog> GetByIdAsync(int id)
        {
            var dadoBlog = await context.DadosBlog.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return dadoBlog;
        }

        public async Task PostAsync(DadosBlog dadosBlog)
        { 
            await context.DadosBlog.AddAsync(dadosBlog);
            await context.SaveChangesAsync();
        }
        public async Task PutAsync(DadosBlog dadosBlog)
        {
            context.DadosBlog.Update(dadosBlog);
            await context.SaveChangesAsync();
        }
        public async Task DeleteAsync(DadosBlog dadosBlog)
        {
            context.DadosBlog.Remove(dadosBlog);
            await context.SaveChangesAsync();
        }
    }
}
