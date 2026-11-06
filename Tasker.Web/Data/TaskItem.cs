namespace Tasker.Web.Data;

/// <summary>
/// Represents a task item within a project.
/// Tasks can be assigned, prioritized, and tracked through different statuses.
/// </summary>
public class TaskItem
{
    /// <summary>
    /// The user assigned to this task.
    /// </summary>
    public ApplicationUser? AssignedToUser { get; set; }

    /// <summary>
    /// The ID of the user assigned to this task.
    /// </summary>
    public string? AssignedToUserId { get; set; }

    /// <summary>
    /// The optional date and time when the task was completed.
    /// </summary>
    public DateTime? CompletedAt { get; set; }

    /// <summary>
    /// The date and time when the task was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// The user who created the task.
    /// </summary>
    public ApplicationUser? CreatedByUser { get; set; }

    /// <summary>
    /// The ID of the user who created the task.
    /// </summary>
    public required string CreatedByUserId { get; set; }

    /// <summary>
    /// An optional detailed description of the task.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// The optional due date for the task.
    /// </summary>
    public DateTime? DueDate { get; set; }

    /// <summary>
    /// The unique identifier for the task.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// The priority level of the task.
    /// </summary>
    public required TaskItemPriority Priority { get; set; }

    /// <summary>
    /// The project that contains this task.
    /// </summary>
    public Project? Project { get; set; }

    /// <summary>
    /// The ID of the project that contains this task.
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    /// The current status of the task.
    /// </summary>
    public required TaskItemStatus Status { get; set; }

    /// <summary>
    /// The title or summary of the task.
    /// </summary>
    public required string Title { get; set; }
}