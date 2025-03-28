using KopiaBackup.Lib.Models.Backups;

namespace KopiaBackup.Lib.Interfaces.Services;

public interface IBackupService
{
    void InitializeDevice(string devicePath);
    public void AddBackupTask(BackupTask backupTask);
    Task TriggerBackupsAsync(string devicePath);
}