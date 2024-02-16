using Microsoft.EntityFrameworkCore;
using Blog.Api.Models;

namespace Blog.Api.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<DadosBlog> DadosBlog { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString: "DataSource=app.db;Cache=Shared");
        }
    }
}
