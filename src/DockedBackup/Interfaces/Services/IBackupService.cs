using DockedBackup.Models.Backups;

namespace DockedBackup.Interfaces.Services;

public interface IBackupService
{
    void InitializeDevice(string devicePath);
     // public void AddBackupTask(BackupTask backupTask);
    // void TriggerBackups(string devicePath);
}