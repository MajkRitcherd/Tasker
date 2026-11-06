namespace Tasker.Web.Data;

/// <summary>
/// Represents the role of a member within an organization.
/// Defines the permission levels for organization members.
/// </summary>
public enum OrganizationMemberRole
{
    /// <summary>
    /// The owner of the organization. Has full control and can manage all aspects.
    /// </summary>
    Owner = 0,

    /// <summary>
    /// An administrator with elevated permissions to manage members and settings.
    /// </summary>
    Admin,

    /// <summary>
    /// A standard member with basic access and permissions.
    /// </summary>
    Member,
}