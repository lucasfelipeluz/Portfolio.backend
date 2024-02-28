using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Entities;

namespace Portfolio.Infra.Mappings;

public class ServicesMap : BaseMap<Activity>
{
	public ServicesMap()
		: base("activities") { }

	public override void Configure(EntityTypeBuilder<Activity> builder)
	{
		base.Configure(builder);

		builder.Property(e => e.Title).IsRequired().HasColumnName("title").HasColumnType("VARCHAR(100)");

		builder.Property(e => e.Description).IsRequired().HasColumnName("description").HasColumnType("VARCHAR(500)");

		builder
			.Property(e => e.Icon)
			.IsRequired()
			.HasColumnName("icon")
			.HasColumnType("VARCHAR(50)")
			.HasComment("Font Awesome Icon (https://fontawesome.com/v5/search)");

		builder
			.Property(e => e.CreatedAt)
			.IsRequired()
			.HasColumnName("created_at")
			.HasDefaultValueSql("CURRENT_TIMESTAMP")
			.HasColumnType("TIMESTAMP")
			.ValueGeneratedOnAdd();
	}
}
