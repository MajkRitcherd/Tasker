namespace Tasker.Web.Data;

/// <summary>
/// Represents a company (organization).
/// </summary>
public class Organization
{
    /// <summary>
    /// Gets or sets the date and time (in UTC) when the organization was created.
    /// </summary>
    public DateTime CreatedAtUTC { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Gets or sets the unique identifier of the user who created this organization.
    /// </summary>
    public required string CreatedByUserId { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the organization.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the collection of members belonging to this organization.
    /// </summary>
    public ICollection<OrganizationMember> Members { get; set; } = [];

    /// <summary>
    /// Gets or sets the name of the organization.
    /// </summary>
    public required string Name { get; set; }
}