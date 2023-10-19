using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace open_ai_chat_tools;

public static class HealthCheckConfiguration
{
    public const string IsHealthy = "IsHealthy";

    private static readonly HashSet<string> _endpoints = new();

    /// <summary>
    /// Registers given health check to specified endpoint, by default <see cref="IsHealthy"/>.
    /// Each endpoint can be linked to many health checks.
    /// </summary>
    /// <param name="builder">The <see cref="IHealthChecksBuilder"/> instance</param>
    /// <param name="endpoint">Endpoint of given health check</param>
    /// <typeparam name="TCheck"><see cref="IHealthCheck"/> implementation</typeparam>
    /// <returns>Modified health checks builder</returns>
    public static IHealthChecksBuilder AddHealthCheck<TCheck>(
        this IHealthChecksBuilder builder, string endpoint = IsHealthy)
        where TCheck : class, IHealthCheck
    {
        builder.AddCheck<TCheck>(typeof(TCheck).Name, HealthStatus.Unhealthy, new[] { endpoint });
        _endpoints.Add(endpoint);

        return builder;
    }
    
    /// <summary>
    /// Map health checks to corresponding endpoints
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> instance</param>
    public static void UseHealthChecks(this WebApplication app)
    {
        foreach (var endpoint in _endpoints)
        {
            app.MapHealthChecks(endpoint, new HealthCheckOptions
            {
                Predicate = healthChecks => healthChecks.Tags.Contains(endpoint),
                ResultStatusCodes = new Dictionary<HealthStatus, int>
                {
                    { HealthStatus.Healthy, (int)HttpStatusCode.OK },
                    { HealthStatus.Degraded, (int)HttpStatusCode.ServiceUnavailable },
                    { HealthStatus.Unhealthy, (int)HttpStatusCode.ServiceUnavailable }
                }
            });
        }
    }
}