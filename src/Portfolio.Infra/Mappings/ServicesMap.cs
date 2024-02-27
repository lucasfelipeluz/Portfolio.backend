using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Entities;

namespace Portfolio.Infra.Mappings
{
  public class ServicesMap : BaseMap<Services>
  {
    public ServicesMap() : base("services") { }

    public override void Configure(EntityTypeBuilder<Services> builder)
    {

      base.Configure(builder);

      builder.Property(e => e.Title)
        .IsRequired()
        .HasColumnName("name")
        .HasColumnType("VARCHAR(100)");

      builder.Property(e => e.Description)
        .IsRequired()
        .HasColumnName("text")
        .HasColumnType("VARCHAR(500)");

      builder.Property(e => e.CreatedAt)
        .IsRequired()
        .HasColumnName("created_at")
        .HasDefaultValueSql("CURRENT_TIMESTAMP")
        .HasColumnType("TIMESTAMP")
        .ValueGeneratedOnAdd();

    }
  }
}