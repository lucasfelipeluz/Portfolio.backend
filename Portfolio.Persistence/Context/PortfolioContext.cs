using Microsoft.EntityFrameworkCore;
using Portfolio.Domain;

namespace Portfolio.Persistence.Context
{
    public class PortfolioContext : DbContext
    {
        public PortfolioContext (
            DbContextOptions<PortfolioContext> options
            ): base(options) { }

        public DbSet<Projeto> Projeto { get; set; }
        public DbSet<Conhecimento> Conhecimento { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
    }
}
