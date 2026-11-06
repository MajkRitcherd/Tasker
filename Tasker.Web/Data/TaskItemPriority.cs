namespace Tasker.Web.Data;

/// <summary>
/// Represents the priority level of a task.
/// Determines the importance and urgency of task items.
/// </summary>
public enum TaskItemPriority
{
    /// <summary>
    /// Low priority task.
    /// </summary>
    Low = 0,

    /// <summary>
    /// Medium priority task.
    /// </summary>
    Medium,

    /// <summary>
    /// High priority task.
    /// </summary>
    High,

    /// <summary>
    /// Critical priority task. Should be addressed immediately.
    /// </summary>
    Critical,
}