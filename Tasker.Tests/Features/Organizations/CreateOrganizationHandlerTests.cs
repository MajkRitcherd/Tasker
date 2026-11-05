using Microsoft.EntityFrameworkCore;
using Tasker.Tests.Helpers;
using Tasker.Web.Data;
using Tasker.Web.Features.Organizations;

namespace Tasker.Tests.Features.Organizations;

/// <summary>
/// Tests for the <see cref="CreateOrganizationHandler"/> class.
/// </summary>
public class CreateOrganizationHandlerTests : DbTest
{
    /// <summary>
    /// Tests that creating an organization with a duplicate name fails and does not create any records.
    /// </summary>
    /// <returns>A task representing the asynchronous test operation.</returns>
    [Fact]
    public async Task HandleAsync_DuplicateName_ReturnsResultFailureAndDoesNotCreateOrganizationAndMember()
    {
        // ARRANGE
        var testOrgName = "My test organization";

        Context.Organizations.Add(new Organization()
        {
            Name = testOrgName,
            CreatedByUserId = "user-999"
        });
        await Context.SaveChangesAsync();

        var handler = new CreateOrganizationHandler(Context);
        var anotherTestUserId = "user-1000";

        // ACT
        var result = await handler.HandleAsync(anotherTestUserId, testOrgName);

        // ASSERT
        Assert.False(result.IsSuccess);
        Assert.Null(result.Value);
        Assert.NotNull(result.ErrorMessage);

        var organizationsCount = await Context.Organizations.CountAsync();
        Assert.Equal(1, organizationsCount);

        var anotherMember = await Context.OrganizationMembers.FirstOrDefaultAsync(m => m.UserId == anotherTestUserId);
        Assert.Null(anotherMember);
    }

    /// <summary>
    /// Tests that creating an organization with valid data succeeds, creates the organization,
    /// and assigns the creating user as the owner.
    /// </summary>
    /// <returns>A task representing the asynchronous test operation.</returns>
    [Fact]
    public async Task HandleAsync_ValidData_CreatesOrganizationAndAssignsOwnerRole()
    {
        // ARRANGE
        var handler = new CreateOrganizationHandler(Context);

        var testUserId = "user-999";
        var testOrgName = "My test organization";

        // ACT
        var result = await handler.HandleAsync(testUserId, testOrgName);

        // ASSERT
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(testOrgName, result.Value.Name);
        Assert.Equal(testUserId, result.Value.CreatedByUserId);

        // Verify the organization was saved to the database
        var savedOrganization = await Context.Organizations.FirstOrDefaultAsync();
        Assert.NotNull(savedOrganization);
        Assert.Equal(testOrgName, savedOrganization.Name);

        // Verify the organization member was created with owner role
        var savedMember = await Context.OrganizationMembers.FirstOrDefaultAsync();
        Assert.NotNull(savedMember);
        Assert.Equal(testUserId, savedMember.UserId);
        Assert.Equal("Owner", savedMember.Role);

        // Verify the organization and member are linked
        Assert.Equal(savedOrganization.Id, savedMember.OrganizationId);
    }
}
