using FilmesWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesWebApi.Data
{
    public class FilmeContext : DbContext
    {
        public FilmeContext(DbContextOptions<FilmeContext> opts)
            : base(opts) {}
        
        public DbSet<Filme> Filmes {get; set;}

    }
}