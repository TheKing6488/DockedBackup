using DockedBackup.Enums;

namespace DockedBackup.Interfaces.Services;

public interface ICoolifyService
{
    public void RestoreDbBackupAsync(Databases databases);
    public void RestoreVolumeBackupAsync();
}