using Microsoft.EntityFrameworkCore;
using Tasker.Web.Data;

namespace Tasker.Web.Features.Organizations;

/// <summary>
/// Handles retrieval of organizations for a specific user.
/// </summary>
public class GetUserOrganizationsHandler(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    /// <summary>
    /// Retrieves all organizations that the specified user is a member of.
    /// </summary>
    /// <param name="userId">The ID of the user whose organizations to retrieve.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A list of <see cref="OrganizationDto"/> objects representing the user's organizations.</returns>
    public async Task<List<OrganizationDto>> HandleAsync(string userId, CancellationToken cancellationToken = default)
    {
        var memberships = await _context.OrganizationMembers
            .AsNoTracking()
            .Include(om => om.Organization)
            .Where(om => om.UserId == userId)
            .ToListAsync(cancellationToken);

        var result = memberships.Select(m => new OrganizationDto(
            m.OrganizationId,
            m.Organization?.Name ?? "Unknown organization",
            m.Role
        )).ToList();

        return result;
    }
}