using System.Text.Json.Serialization;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace open_ai_chat_infrastructure;

public class DbHealthCheck : IHealthCheck
{
    private readonly ApplicationDbContext _context;

    public DbHealthCheck(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            // todo: move to configuration
            var deadline = Task.Delay(TimeSpan.FromSeconds(10), cancellationToken);
            var canConnectTask = _context.Database.CanConnectAsync(cancellationToken);
            var completedTask = await Task.WhenAny(deadline, canConnectTask);

            return completedTask == canConnectTask
                ? HealthCheckResult.Healthy()
                : HealthCheckResult.Unhealthy();
        }
        catch (Exception e)
        {
            // todo: log exception
            Console.WriteLine(e);
            return HealthCheckResult.Unhealthy();
        }
    }
}