using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portfolio.Domain;
using Portfolio.Domain.Identity;

namespace Portfolio.Persistence.Context
{
    public class PortfolioContext : IdentityDbContext<User, Role, int,
                                                    IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
                                                    IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public PortfolioContext (
            DbContextOptions<PortfolioContext> options
            ): base(options) { }

        public DbSet<Projeto> Projeto { get; set; }
        public DbSet<Conhecimento> Conhecimento { get; set; }
        public DbSet<Perfil> Perfil { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });
        }
    }
}
