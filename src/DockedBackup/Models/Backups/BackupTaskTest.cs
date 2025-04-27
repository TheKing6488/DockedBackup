
namespace DockedBackup.Models.Backups;

public class BackupTaskTest
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public required DayOfWeek Day { get; set; }
    public required bool IsEnabled  { get; set; }
    public required Guid AccessDataId  { get; set; }
}