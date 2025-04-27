using DockedBackup.Models.Backups;
using DockedBackup.Interfaces.Services;

namespace DockedBackup.Commands;

// TODO change to a instance for better handling and interface support

public class BackupCommands
{
    public static int RunAddBackupTask(IBackupService backupService, BackupTask backupTask)
    {
        //TODO fix this funktion
        // var backupTaskLib = new BackupTask
        // {
        //     Name = backupTask.Name,
        //     Day = backupTask.Day,
        //     IsEnabled = true,
        //     AccessDataId = backupTask.AccessDataId
        // };
        //  backupService.AddBackupTask(backupTaskLib);
        return 0;
    }
}