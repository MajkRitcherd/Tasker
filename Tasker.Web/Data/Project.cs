namespace Tasker.Web.Data;

/// <summary>
/// Represents a project within an organization.
/// Projects contain tasks that can be managed and tracked.
/// </summary>
public class Project
{
    /// <summary>
    /// The date and time when the project was created.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// The user who created the project.
    /// </summary>
    public ApplicationUser? CreatedByUser { get; set; }

    /// <summary>
    /// The ID of the user who created the project.
    /// </summary>
    public required string CreatedByUserId { get; set; }

    /// <summary>
    /// An optional description providing details about the project.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// The unique identifier for the project.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// A value indicating whether the project is archived.
    /// </summary>
    public bool IsArchived { get; set; } = false;

    /// <summary>
    /// The name of the project.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// The organization that owns this project.
    /// </summary>
    public Organization? Organization { get; set; }

    /// <summary>
    /// The ID of the organization that owns this project.
    /// </summary>
    public Guid OrganizationId { get; set; }

    /// <summary>
    /// The collection of tasks associated with this project.
    /// </summary>
    public ICollection<TaskItem> Tasks { get; set; } = [];
}