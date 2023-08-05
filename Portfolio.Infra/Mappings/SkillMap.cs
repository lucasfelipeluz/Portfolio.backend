using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Entities;

namespace Portfolio.Infra.Mappings
{
  public class SkillMap : BaseMap<Skill>
  {
    public SkillMap() : base("skills") { }

    public override void Configure(EntityTypeBuilder<Skill> builder)
    {

      base.Configure(builder);

      builder.Property(e => e.Title)
        .IsRequired()
        .HasColumnName("title")
        .HasColumnType("VARCHAR(100)");

      builder.Property(e => e.Description)
        .IsRequired()
        .HasColumnName("description")
        .HasColumnType("VARCHAR(100)");

      builder.Property(e => e.Experience)
        .IsRequired()
        .HasColumnName("experience")
        .HasColumnType("DATETIME");

      builder.Property(e => e.Color)
        .IsRequired()
        .HasColumnName("color")
        .HasColumnType("VARCHAR(50)");

      builder.Property(e => e.Icon)
        .IsRequired()
        .HasColumnName("icon")
        .HasColumnType("VARCHAR(50)");

      builder.Property(e => e.ViewPriority)
        .IsRequired()
        .HasColumnName("view_priority")
        .HasColumnType("INT");

      builder.Property(e => e.IsActive)
        .IsRequired()
        .HasColumnName("is_active")
        .HasColumnType("TINYINT")
        .HasDefaultValueSql("1");

      builder.Property(e => e.CreatedAt)
        .IsRequired()
        .HasColumnName("created_at")
        .HasColumnType("TIMESTAMP")
        .ValueGeneratedOnAdd();

      builder.Property(e => e.UpdatedAt)
        .HasColumnName("updated_at")
        .HasColumnType("TIMESTAMP")
        .ValueGeneratedOnAddOrUpdate();

    }
  }
}