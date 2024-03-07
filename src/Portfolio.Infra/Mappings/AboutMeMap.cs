using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Entities;

namespace Portfolio.Infra.Mappings;

public class AboutMeMap : BaseMap<AboutMe>
{
	public AboutMeMap()
		: base("about_me") { }

	public override void Configure(EntityTypeBuilder<AboutMe> builder)
	{
		base.Configure(builder);

		builder.Property(e => e.Name).IsRequired().HasColumnName("name").HasColumnType("VARCHAR(100)");

		builder.Property(e => e.Text).IsRequired().HasColumnName("text").HasColumnType("VARCHAR(500)");

		builder
			.Property(e => e.TextEnglish)
			.HasColumnName("text_en")
			.HasColumnType("VARCHAR(500)")
			.HasDefaultValue(null);

		builder.Property(e => e.JobTitle).IsRequired().HasColumnName("job_title").HasColumnType("VARCHAR(50)");

		builder
			.Property(e => e.JobTitleEnglish)
			.HasColumnName("job_title_en")
			.HasColumnType("VARCHAR(50)")
			.HasDefaultValue(null);

		builder.Property(e => e.TelegramLink).IsRequired().HasColumnName("telegram_link").HasColumnType("VARCHAR(200)");

		builder
			.Property(e => e.InstagramLink)
			.IsRequired()
			.HasColumnName("instagram_link")
			.HasColumnType("VARCHAR(200)");

		builder.Property(e => e.LinkedinLink).IsRequired().HasColumnName("linkedin_link").HasColumnType("VARCHAR(200)");

		builder.Property(e => e.GithubLink).IsRequired().HasColumnName("github_link").HasColumnType("VARCHAR(200)");

		builder.Property(e => e.IsAvailable).IsRequired().HasColumnName("is_available").HasColumnType("TINYINT");

		builder.Property(e => e.Address).IsRequired().HasColumnName("address").HasColumnType("VARCHAR(200)");

		builder
			.Property(e => e.CreatedAt)
			.IsRequired()
			.HasColumnName("created_at")
			.HasDefaultValueSql("CURRENT_TIMESTAMP")
			.HasColumnType("TIMESTAMP")
			.ValueGeneratedOnAdd();
	}
}
