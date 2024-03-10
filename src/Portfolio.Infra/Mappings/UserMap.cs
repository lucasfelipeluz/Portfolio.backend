using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Core.Enums;
using Portfolio.Domain.Entities;

namespace Portfolio.Infra.Mappings;

public class UserMap : BaseMap<User>
{
	public UserMap()
		: base("users") { }

	public override void Configure(EntityTypeBuilder<User> builder)
	{
		base.Configure(builder);

		builder.Property(e => e.Name).IsRequired().HasColumnName("name").HasColumnType("VARCHAR(100)");

		builder.Property(e => e.NickName).IsRequired().HasColumnName("nickname").HasColumnType("VARCHAR(100)");

		builder.Property(e => e.Password).IsRequired().HasColumnName("password").HasColumnType("VARCHAR(200)");

		builder
			.Property(e => e.IsActive)
			.IsRequired()
			.HasColumnName("is_active")
			.HasColumnType("TINYINT")
			.HasDefaultValue(true);

		builder
			.Property(e => e.Role)
			.IsRequired()
			.HasColumnName("role")
			.HasColumnType("INT")
			.HasDefaultValue(UserRole.User);

		builder
			.Property(e => e.CreatedAt)
			.IsRequired()
			.HasColumnName("created_at")
			.HasDefaultValueSql("CURRENT_TIMESTAMP")
			.HasColumnType("TIMESTAMP")
			.ValueGeneratedOnAdd();
	}
}
