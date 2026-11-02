using Microsoft.AspNetCore.Identity;

namespace Tasker.Web.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    /// <summary>
    /// Gets or sets the user's first name.
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Gets or sets the user's last name. 
    /// </summary>
    public required string LastName { get ;set; }
}

