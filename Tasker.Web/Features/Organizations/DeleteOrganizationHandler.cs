using Microsoft.EntityFrameworkCore;
using Tasker.Web.Common;
using Tasker.Web.Data;

namespace Tasker.Web.Features.Organizations;

/// <summary>
/// Handles the deletion of an organization. Only organization owners can delete their organizations.
/// </summary>
public class DeleteOrganizationHandler(ApplicationDbContext context)
{
    /// <summary>
    /// Gets or sets the application database context.
    /// </summary>
    public ApplicationDbContext _context = context;

    /// <summary>
    /// Handles the asynchronous deletion of an organization.
    /// </summary>
    /// <param name="userId">The ID of the user requesting the deletion.</param>
    /// <param name="organizationId">The ID of the organization to delete.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A <see cref="Result"/> indicating success or failure.</returns>
    public async Task<Result> HandleAsync(string userId, Guid organizationId, CancellationToken cancellationToken = default)
    {
        bool isOwner = await _context.OrganizationMembers.AnyAsync(m =>
            m.OrganizationId == organizationId &&
            m.UserId == userId
            && m.Role == OrganizationMemberRole.Owner,
            cancellationToken);

        if (!isOwner)
            return Result.Failure($"You are not allowed to delete organization.");

        var organization = await _context.Organizations.FirstOrDefaultAsync(o => o.Id == organizationId, cancellationToken);

        if (organization == null)
            return Result.Failure($"Organization does not exists");

        _context.Organizations.Remove(organization);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
