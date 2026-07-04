using Aidelythe.Application._System.Maintenance.Commands;

namespace Aidelythe.Api._System.Scheduling;

/// <summary>
/// Provides background job scheduling methods.
/// </summary>
public static class BackgroundJobScheduler
{
    /// <summary>
    /// Schedules recurring background jobs.
    /// </summary>
    public static void ScheduleRecurringJobs()
    {
        AddMaintenanceJobs();
    }

    private static void AddMaintenanceJobs()
    {
        RecurringJob.AddOrUpdate<IMediator>(
            "mtn-purge-deleted-entities",
            mediator => mediator.Send(
                new PurgeDeletedEntitiesCommand(),
                CancellationToken.None),
            Cron.Daily);
    }
}