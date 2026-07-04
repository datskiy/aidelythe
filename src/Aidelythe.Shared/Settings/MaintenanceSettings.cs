namespace Aidelythe.Shared.Settings;

/// <summary>
/// Represents settings for configuring maintenance.
/// </summary>
public sealed class MaintenanceSettings
{
    /// <summary>
    /// Gets the number of days to retain deleted data.
    /// </summary>
    [Required]
    public int DeletedEntityRetentionDays { get; init; }
}