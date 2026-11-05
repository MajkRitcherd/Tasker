using Microsoft.EntityFrameworkCore;
using Tasker.Web.Data;

namespace Tasker.Tests.Helpers;

/// <summary>
/// Provides a test database context using an in-memory database.
/// Implements <see cref="IDisposable"/> to properly dispose of the context.
/// </summary>
public class DbTest : IDisposable
{
    /// <summary>
    /// Gets the <see cref="ApplicationDbContext"/> instance for testing.
    /// </summary>
    protected readonly ApplicationDbContext Context;

    /// <summary>
    /// Initializes a new instance of the <see cref="DbTest"/> class
    /// with a new in-memory database.
    /// </summary>
    protected DbTest()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        Context = new ApplicationDbContext(options);
    }

    /// <summary>
    /// Releases all resources used by the <see cref="DbTest"/>.
    /// </summary>
    public void Dispose()
    {
        Context.Dispose();
        GC.SuppressFinalize(this);
    }
}