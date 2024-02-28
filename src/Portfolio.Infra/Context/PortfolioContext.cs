using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Entities;

namespace Portfolio.Infra.Context;

public class PortfolioContext : DbContext
{
	public PortfolioContext(DbContextOptions<PortfolioContext> options)
		: base(options) { }

	public DbSet<Project> Projects { get; set; }
	public DbSet<Skill> Skills { get; set; }
	public DbSet<AboutMe> AboutMe { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<ProjectSkill> ProjectsSkills { get; set; }
	public DbSet<Image> Images { get; set; }
	public DbSet<SkillImage> SkillsImages { get; set; }
	public DbSet<ProjectImage> ProjectsImages { get; set; }
	public DbSet<Domain.Entities.Activity> Activity { get; set; }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(PortfolioContext).Assembly);
	}
}
