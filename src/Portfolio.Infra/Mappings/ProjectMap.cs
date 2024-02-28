using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Entities;

namespace Portfolio.Infra.Mappings;

public class ProjectMap : BaseMap<Project>
{
	public ProjectMap()
		: base("projects") { }

	public override void Configure(EntityTypeBuilder<Project> builder)
	{
		base.Configure(builder);

		builder.Property(e => e.Title).IsRequired().HasColumnName("title").HasColumnType("VARCHAR(100)");

		builder.Property(e => e.Description).IsRequired().HasColumnName("description").HasColumnType("VARCHAR(100)");

		builder.Property(e => e.UrlGithub).IsRequired().HasColumnName("url_github").HasColumnType("VARCHAR(200)");

		builder.Property(e => e.UrlWebsite).IsRequired().HasColumnName("url_website").HasColumnType("VARCHAR(200)");

		builder.Property(e => e.ViewPriority).IsRequired().HasColumnName("view_priority").HasColumnType("INT");

		builder
			.Property(e => e.IsActive)
			.IsRequired()
			.HasColumnName("is_active")
			.HasColumnType("TINYINT")
			.HasDefaultValue(true);

		builder
			.Property(e => e.CreatedAt)
			.IsRequired()
			.HasColumnName("created_at")
			.HasDefaultValueSql("CURRENT_TIMESTAMP")
			.HasColumnType("TIMESTAMP")
			.ValueGeneratedOnAdd();

		builder
			.Property(e => e.UpdatedAt)
			.HasColumnName("updated_at")
			.HasColumnType("TIMESTAMP")
			.HasDefaultValueSql("CURRENT_TIMESTAMP")
			.ValueGeneratedOnAddOrUpdate();

		builder.Property(e => e.StartedAt).IsRequired().HasColumnName("started_at").HasColumnType("DATETIME");

		builder.Property(e => e.FinishedAt).HasColumnName("finished_at").HasColumnType("DATETIME");

		// Defining relationships between Project and Skills
		builder
			.HasMany(x => x.Skills)
			.WithMany(x => x.Projects)
			.UsingEntity<ProjectSkill>(
				x => x.HasOne(x => x.Skill).WithMany().HasForeignKey(x => x.SkillId),
				x => x.HasOne(x => x.Project).WithMany().HasForeignKey(x => x.ProjectId),
				// Mapping the Project and Skill relationship table.
				x =>
				{
					x.ToTable("projects_skills");

					x.Ignore(x => x.Id);

					x.HasKey(e => new { e.ProjectId, e.SkillId });

					x.Property(e => e.ProjectId).HasColumnName("project_id").IsRequired();
					x.Property(e => e.SkillId).HasColumnName("skill_id").IsRequired();
				}
			);
	}
}
