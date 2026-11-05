using Tasker.Web.Features.Organizations;

namespace Tasker.Web.Features;

/// <summary>
/// Provides extension methods for registering feature services.
/// </summary>
public static class FeaturesServices
{
    /// <summary>
    /// Registers the feature services with the dependency injection container.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to register services with.</param>
    /// <returns>The <see cref="IServiceCollection"/> for chaining.</returns>
    public static IServiceCollection Register(this IServiceCollection services)
    {
        services.AddScoped<CreateOrganizationHandler>();
        services.AddScoped<GetUserOrganizationsHandler>();

        return services;
    }
}