using CommandLine;

namespace KopiaBackup.Console.Models.Backups;

[Verb("job", HelpText = "Establishing a connection to the repository")]
public class BackupTask
{
   [Option('n', "name", HelpText = "Specifies the name used for this operation", Required = true)]
    public required string Name { get; set; }
    [Option('d', "day", HelpText = "Specifies the day of the week associated with the operation (e.g., Monday, Tuesday, etc.)", Required = true)]
    public required DayOfWeek Day { get; set; }
    //TODO erstelle einen besseren hilfe Text
    [Option('a', "access-data-id", HelpText = "Represents the unique GUID for access data", Required = true)]
    public required Guid AccessDataId  { get; set; }
}