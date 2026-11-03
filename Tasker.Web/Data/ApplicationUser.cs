using Microsoft.AspNetCore.Identity;

namespace Tasker.Web.Data;

/// <summary>
/// Represents a user of the application with additional profile information.
/// </summary>
public class ApplicationUser : IdentityUser
{
    /// <summary>
    /// Gets or sets the user's first name.
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the user's last name.
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Gets or sets the collection of organization memberships for this user.
    /// </summary>
    public ICollection<OrganizationMember> Memberships { get; set; } = [];
}

