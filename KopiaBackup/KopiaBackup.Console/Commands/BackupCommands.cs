using KopiaBackup.Console.Models.Backups;
using KopiaBackup.Lib.Interfaces.Services;

namespace KopiaBackup.Console.Commands;

public class BackupCommands
{
    public static int RunAddBackupTask(IBackupService backupService, BackupTask backupTask)
    {
        var  backupTaskLib = new KopiaBackup.Lib.Models.Backups.BackupTask
        {
            Name = backupTask.Name,
            Day = backupTask.Day,
            IsEnabled = true,
            AccessDataId = backupTask.AccessDataId
        };
         backupService.AddBackupTask(backupTaskLib);
        return 0;
    }
}