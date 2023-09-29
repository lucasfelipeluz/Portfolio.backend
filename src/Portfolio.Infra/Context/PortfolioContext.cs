using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Portfolio.Domain.Entities;
using Portfolio.Infra.Context;

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


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(typeof(PortfolioContext).Assembly);
    }
  }
}