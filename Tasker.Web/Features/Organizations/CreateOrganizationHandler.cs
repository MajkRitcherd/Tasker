using Microsoft.EntityFrameworkCore;
using Tasker.Web.Common;
using Tasker.Web.Data;

namespace Tasker.Web.Features.Organizations;

/// <summary>
/// Provides operations for managing organizations.
/// </summary>
public class CreateOrganizationHandler(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;

    /// <summary>
    /// Creates a new organization and adds the creating user as the owner.
    /// </summary>
    /// <param name="userId">The ID of the user creating the organization.</param>
    /// <param name="organizationName">The name of the organization.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A <see cref="Result{Organization}"/> indicating success or failure.</returns>
    public async Task<Result<Organization>> HandleAsync(string userId, string organizationName, CancellationToken cancellationToken = default)
    {
        bool exists = await _context.Organizations
            .AnyAsync(o => o.Name == organizationName, cancellationToken);

        if (exists)
        {
            return Result<Organization>.Failure($"Organization with name '{organizationName}' already exists.");
        }

        var organization = new Organization
        {
            CreatedByUserId = userId,
            Name = organizationName,
        };

        var organizationMember = new OrganizationMember
        {
            UserId = userId,
            Role = "Owner",
            OrganizationId = organization.Id,
        };

        _context.Organizations.Add(organization);
        _context.OrganizationMembers.Add(organizationMember);

        await _context.SaveChangesAsync(cancellationToken);

        return Result<Organization>.Success(organization);
    }
}