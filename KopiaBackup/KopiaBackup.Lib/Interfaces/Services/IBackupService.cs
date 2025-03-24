namespace KopiaBackup.Lib.Interfaces.Services;

public interface IBackupService
{
    public void InitializeDevice(string devicePath);
    public bool CheckDevice(string devicePath);
}