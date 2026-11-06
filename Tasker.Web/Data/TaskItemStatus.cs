namespace Tasker.Web.Data;

/// <summary>
/// Represents the status of a task item.
/// Indicates the current state of a task in its lifecycle.
/// </summary>
public enum TaskItemStatus
{
    /// <summary>
    /// The task is pending and has not started yet.
    /// </summary>
    ToDo = 0,

    /// <summary>
    /// The task is currently being worked on.
    /// </summary>
    InProgress,

    /// <summary>
    /// The task has been completed.
    /// </summary>
    Done,
}