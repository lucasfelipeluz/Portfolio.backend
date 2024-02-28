using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Entities;

namespace Portfolio.Infra.Mappings;

public class ImageMap : BaseMap<Image>
{
	public ImageMap()
		: base("images") { }

	public override void Configure(EntityTypeBuilder<Image> builder)
	{
		base.Configure(builder);

		builder.Property(e => e.Name).IsRequired().HasColumnName("name").HasColumnType("VARCHAR(200)");

		builder.Property(e => e.Folder).IsRequired().HasColumnName("folder").HasColumnType("VARCHAR(50)");

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
			.HasDefaultValueSql("CURRENT_TIMESTAMP")
			.HasColumnType("TIMESTAMP")
			.ValueGeneratedOnAddOrUpdate();

		// Defining relationships between Files and Projects
		builder
			.HasMany(x => x.Projects)
			.WithMany(x => x.Images)
			.UsingEntity<ProjectImage>(
				x => x.HasOne(x => x.Project).WithMany().HasForeignKey(x => x.ProjectId),
				x => x.HasOne(x => x.Image).WithMany().HasForeignKey(x => x.ImageId),
				x =>
				{
					x.ToTable("projects_images");

					x.Ignore(x => x.Id);

					x.HasKey(e => new { e.ProjectId, e.ImageId });

					x.Property(e => e.ProjectId).HasColumnName("project_id").IsRequired();
					x.Property(e => e.ImageId).HasColumnName("image_id").IsRequired();
				}
			);

		// Defining relationships between Files and Skills
		builder
			.HasMany(x => x.Skills)
			.WithMany(x => x.Images)
			.UsingEntity<SkillImage>(
				x => x.HasOne(x => x.Skill).WithMany().HasForeignKey(x => x.SkillId),
				x => x.HasOne(x => x.Image).WithMany().HasForeignKey(x => x.ImageId),
				x =>
				{
					x.ToTable("skills_images");

					x.Ignore(x => x.Id);

					x.HasKey(e => new { e.SkillId, e.ImageId });

					x.Property(e => e.SkillId).HasColumnName("skill_id").IsRequired();
					x.Property(e => e.ImageId).HasColumnName("image_id").IsRequired();
				}
			);
	}
}
