using Microsoft.EntityFrameworkCore;
using Tasker.Web.Data;
using Tasker.Web.Features.Organizations;

namespace Tasker.Tests.Features.Organizations;

/// <summary>
/// Tests for the <see cref="GetUserOrganizationsHandler"/> class.
/// </summary>
public class GetUserOrganizationsHandlerTests
{
    /// <summary>
    /// Tests that when a user has multiple organizations, only organizations
    /// the user belongs to are returned.
    /// </summary>
    /// <returns>A task representing the asynchronous test operation.</returns>
    [Fact]
    public async Task HandleAsync_UserHasOrganization_ReturnsOnlyUserOrganizations()
    {
        // ARRANGE
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var context = new ApplicationDbContext(options);
        var handler = new GetUserOrganizationsHandler(context);

        var myUserId = "user-999";
        var otherUserId = "user-1000";

        var org1 = new Organization { Name = "My first organization", CreatedByUserId = myUserId };
        context.Organizations.Add(org1);
        context.OrganizationMembers.Add(new OrganizationMember { UserId = myUserId, Organization = org1, Role = "Owner" });

        var org2 = new Organization { Name = "Shared organization", CreatedByUserId = otherUserId };
        context.Organizations.Add(org2);
        context.OrganizationMembers.Add(new OrganizationMember { UserId = myUserId, Organization = org2, Role = "Member" });

        var org3 = new Organization { Name = "Foreign organization", CreatedByUserId = otherUserId };
        context.Organizations.Add(org3);
        context.OrganizationMembers.Add(new OrganizationMember { UserId = otherUserId, Organization = org3, Role = "Owner" });

        await context.SaveChangesAsync();

        // ACT
        var result = await handler.HandleAsync(myUserId);

        // ASSERT
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);

        Assert.Contains(result, dto => dto.Name == "My first organization" && dto.UserRole == "Owner");
        Assert.Contains(result, dto => dto.Name == "Shared organization" && dto.UserRole == "Member");
        Assert.DoesNotContain(result, dto => dto.Name == "Foreign organization");
    }

    /// <summary>
    /// Tests that when a user has no organizations, an empty list is returned.
    /// </summary>
    /// <returns>A task representing the asynchronous test operation.</returns>
    [Fact]
    public async Task HandleAsync_UserHasNoOrganizations_ReturnsEmptyList()
    {
        // ARRANGE
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var context = new ApplicationDbContext(options);
        var handler = new GetUserOrganizationsHandler(context);

        var myUserId = "user-999";
        var otherUserId = "user-1000";

        var org1 = new Organization { Name = "Foreign organization", CreatedByUserId = otherUserId };
        context.Organizations.Add(org1);
        context.OrganizationMembers.Add(new OrganizationMember { UserId = otherUserId, Organization = org1, Role = "Owner" });

        await context.SaveChangesAsync();

        // ACT
        var result = await handler.HandleAsync(myUserId);

        // ASSERT
        Assert.NotNull(result);
        Assert.Empty(result);
    }
}