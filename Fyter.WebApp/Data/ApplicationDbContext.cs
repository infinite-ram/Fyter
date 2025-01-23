using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fyter.WebApp.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<UserRequestedRole> UserRequestedRoles { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder
            .Entity<UserRequestedRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.RequestedRoles)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();
    }
}
