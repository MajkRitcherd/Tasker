using Microsoft.EntityFrameworkCore;
using Tasker.Tests.Helpers;
using Tasker.Web.Data;
using Tasker.Web.Features.Organizations;

namespace Tasker.Tests.Features.Organizations;

/// <summary>
/// Tests for the <see cref="DeleteOrganizationHandler"/> class.
/// </summary>
public class DeleteOrganizationHandlerTests : DbTest
{
    /// <summary>
    /// Tests that a valid owner can successfully delete an organization and its members.
    /// </summary>
    public async Task HandleAsync_ValidRemove_RemovesOrganizationAndItsMembers()
    {
        // ARRANGE
        var userId = "user-999";
        var organizationName = "My test organization";
        var organization = new Organization() { Name = organizationName, CreatedByUserId = userId };

        Context.Organizations.Add(organization);
        Context.OrganizationMembers.Add(new OrganizationMember() { UserId = userId, Organization = organization, Role = OrganizationMemberRole.Owner });

        await Context.SaveChangesAsync();

        var handler = new DeleteOrganizationHandler(Context);

        // ACT
        var result = await handler.HandleAsync(userId, organization.Id);

        // ASSERT
        Assert.True(result.IsSuccess);
        Assert.Null(result.ErrorMessage);

        var removedOrganization = Context.Organizations.FirstOrDefault(o => o.Id == organization.Id);
        Assert.Null(removedOrganization);

        var removedMember = Context.OrganizationMembers.FirstOrDefault(om => om.UserId == userId && om.OrganizationId == organization.Id);
        Assert.Null(removedMember);
    }

    /// <summary>
    /// Tests that a non-owner user cannot delete an organization.
    /// </summary>
    public async Task HandleAsync_NotAllowedUser_ReturnsResultFailureAndDoesNotRemoveOrganization()
    {
        // ARRANGE
        var userId = "user-999";
        var organizationName = "My test organization";
        var organization = new Organization() { Name = organizationName, CreatedByUserId = userId };

        Context.Organizations.Add(organization);
        Context.OrganizationMembers.Add(new OrganizationMember() { UserId = userId, Organization = organization, Role = OrganizationMemberRole.Member });

        await Context.SaveChangesAsync();

        var handler = new DeleteOrganizationHandler(Context);

        // ACT
        var result = await handler.HandleAsync(userId, organization.Id);

        // ASSERT
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.ErrorMessage);

        var organizationExists = await Context.Organizations.AnyAsync(o => o.Id == organization.Id);
        Assert.True(organizationExists);

        var organizationMemberExists = await Context.OrganizationMembers.AnyAsync(om => om.OrganizationId == organization.Id && om.UserId == userId);
        Assert.True(organizationMemberExists);
    }

    /// <summary>
    /// Tests that attempting to delete a non-existent organization returns a failure result.
    /// </summary>
    public async Task HandleAsync_OrganizationDoesNotExists_ReturnsResultFailure()
    {
        // ARRANGE
        var userId = "user-999";
        var organizationId = Guid.NewGuid();

        var handler = new DeleteOrganizationHandler(Context);

        // ACT
        var result = await handler.HandleAsync(userId, organizationId);

        // ASSERT
        Assert.False(result.IsSuccess);
        Assert.NotNull(result.ErrorMessage);
    }
}
