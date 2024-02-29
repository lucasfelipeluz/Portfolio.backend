using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Entities;

namespace Portfolio.Infra.Mappings;

public class SystemVariableMapping : BaseMap<SystemVariable>
{
	public SystemVariableMapping()
		: base("system_variables") { }

	public override void Configure(EntityTypeBuilder<SystemVariable> builder)
	{
		base.Configure(builder);

		builder.Ignore(e => e.Id);

		builder.Property(e => e.Name).IsRequired().HasColumnName("name").HasColumnType("VARCHAR(100)");

		builder.Property(e => e.Value).IsRequired().HasColumnName("value").HasColumnType("VARCHAR(100)");
	}
}
