namespace KopiaBackup.Lib.Interfaces.Services;

public interface IBackupService
{
    public void InitializeDevice(string devicePath);
    public void AddBackupJob();
    public void TriggerBackups(string devicePath);
}