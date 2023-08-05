using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Entities;

namespace Portfolio.Infra.Context
{
  public class PortfolioContext : DbContext
  {
    public PortfolioContext(DbContextOptions<PortfolioContext> options) : base(options) { }
    DbSet<Project> Projects { get; set; }
    DbSet<Skill> Skills { get; set; }
    DbSet<AboutMe> AboutMe { get; set; }
    DbSet<User> Users { get; set; }
    DbSet<ProjectSkill> ProjectsSkills { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
  }
}