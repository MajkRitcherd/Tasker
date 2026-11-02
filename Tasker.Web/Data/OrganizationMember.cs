namespace Tasker.Web.Data;

/// <summary>
/// Represents a user's membership within an organization.
/// </summary>
public class OrganizationMember
{
    /// <summary>
    /// Gets or sets the date and time (in UTC) when the membership was created.
    /// </summary>
    public DateTime CreatedAtUTC { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the unique identifier for the organization member record.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the navigation property for the <see cref="Data.Organization"/> this member belongs to.
    /// </summary>
    public Organization? Organization { get; set; }

    /// <summary>
    /// Gets or sets the foreign key of the <see cref="Data.Organization"/> this member belongs to.
    /// </summary>
    public Guid OrganizationId { get; set; }

    /// <summary>
    /// Gets or sets the role of the member within the organization (e.g., Admin, Member).
    /// </summary>
    public required string Role { get; set; }

    /// <summary>
    /// Gets or sets the navigation property for the associated <see cref="ApplicationUser"/>.
    /// </summary>
    public ApplicationUser? User { get; set; }

    /// <summary>
    /// Gets or sets the foreign key of the user associated with this membership.
    /// </summary>
    public required string UserId { get; set; }
}