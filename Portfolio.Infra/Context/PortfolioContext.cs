using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Entities;

namespace Portfolio.Infra.Context
{
  public class PortfolioContext : DbContext
  {
    public PortfolioContext(DbContextOptions<PortfolioContext> options) : base(options) { }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<AboutMe> AboutMe { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<ProjectSkill> ProjectsSkills { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      string connectionString = "Server=localhost;Database=portfolio;Uid=root;Pwd=Telegram2012*;";
      optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
  }
}