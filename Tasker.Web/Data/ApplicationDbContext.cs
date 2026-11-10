using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Tasker.Web.Data;

/// <summary>
/// Represents the database context for the Tasker application.
/// </summary>
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    /// <summary>
    /// Gets or sets the <see cref="DbSet{OrganizationMember}"/> for organization member records.
    /// </summary>
    public DbSet<OrganizationMember> OrganizationMembers { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{Organization}"/> for organization records.
    /// </summary>
    public DbSet<Organization> Organizations { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{Project}"/> for project records.
    /// </summary>
    public DbSet<Project> Projects { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="DbSet{TaskItem}"/> for task item records.
    /// </summary>
    public DbSet<TaskItem> TaskItems { get; set; }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<OrganizationMember>()
            .Property(m => m.Role)
            .HasConversion<string>();

        builder.Entity<TaskItem>()
            .Property(ti => ti.Priority)
            .HasConversion<string>();

        builder.Entity<TaskItem>()
            .Property(ti => ti.Status)
            .HasConversion<string>();
    }
}