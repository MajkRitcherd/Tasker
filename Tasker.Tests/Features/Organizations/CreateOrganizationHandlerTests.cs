using Microsoft.EntityFrameworkCore;
using Tasker.Web.Data;
using Tasker.Web.Features.Organizations;

namespace Tasker.Tests.Features.Organizations;

/// <summary>
/// Tests for the <see cref="CreateOrganizationHandler"/> class.
/// </summary>
public class CreateOrganizationHandlerTests
{
    /// <summary>
    /// Tests that creating an organization with a duplicate name fails and does not create any records.
    /// </summary>
    /// <returns>A task representing the asynchronous test operation.</returns>
    [Fact]
    public async Task HandleAsync_DuplicateName_ReturnsResultFailureAndDoesNotCreateOrganizationAndMember()
    {
        // ARRANGE
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var context = new ApplicationDbContext(options);

        var testOrgName = "My test organization";

        // Act: Add initial organization with the same name
        context.Organizations.Add(new Organization()
        {
            Name = testOrgName,
            CreatedByUserId = "user-999"
        });
        await context.SaveChangesAsync();

        var handler = new CreateOrganizationHandler(context);
        var anotherTestUserId = "user-1000";

        // ACT
        var result = await handler.HandleAsync(anotherTestUserId, testOrgName);

        // ASSERT
        // Verify the result indicates failure
        Assert.False(result.IsSuccess);
        Assert.Null(result.Value);
        Assert.NotNull(result.ErrorMessage);

        // Verify no additional organization was created
        var organizationsCount = await context.Organizations.CountAsync();
        Assert.Equal(1, organizationsCount);

        // Verify no organization member was created for the new user
        var anotherMember = await context.OrganizationMembers.FirstOrDefaultAsync(m => m.UserId == anotherTestUserId);
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
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        using var context = new ApplicationDbContext(options);

        var handler = new CreateOrganizationHandler(context);

        var testUserId = "user-999";
        var testOrgName = "My test organization";

        // ACT
        var result = await handler.HandleAsync(testUserId, testOrgName);

        // ASSERT
        // Check the result indicates success and contains the created organization
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(testOrgName, result.Value.Name);
        Assert.Equal(testUserId, result.Value.CreatedByUserId);

        // Verify the organization was saved to the database
        var savedOrganization = await context.Organizations.FirstOrDefaultAsync();
        Assert.NotNull(savedOrganization);
        Assert.Equal(testOrgName, savedOrganization.Name);

        // Verify the organization member was created with owner role
        var savedMember = await context.OrganizationMembers.FirstOrDefaultAsync();
        Assert.NotNull(savedMember);
        Assert.Equal(testUserId, savedMember.UserId);
        Assert.Equal("Owner", savedMember.Role);

        // Verify the organization and member are linked
        Assert.Equal(savedOrganization.Id, savedMember.OrganizationId);
    }
}
