using System.Diagnostics;
using System.Text.Json;
using KopiaBackup.Lib.Interfaces.Helpers;
using KopiaBackup.Lib.Interfaces.Repositories;
using KopiaBackup.Lib.Interfaces.Services;
using KopiaBackup.Lib.Models;
using KopiaBackup.Lib.Models.Backups;
using KopiaBackup.Lib.Models.Kopia;


namespace KopiaBackup.Lib.Services;

public class BackupService(ISettingsManager settingsManager, IKopiaHelper kopiaHelper) : IBackupService
{
    private const string MetaDataFileName = "metadata.json";

    public void InitializeDevice(string devicePath)
    {
        if (CheckMetaExists(devicePath)) return;
        MetaData metaData = new()
        {
            LastBackup = null
        };
        var jsonString = JsonSerializer.Serialize(metaData);
        File.WriteAllText(FullPath(devicePath), jsonString);
    }

    public void AddBackupTask(BackupTask backupTask)
    {
        var settings = settingsManager.GetUserSettings();
        settings.BackupTasks.Add(backupTask);
        settingsManager.SaveUserSettings(settings);
    }

    public void TriggerBackups(string devicePath)
    {
        if (!CheckMetaExists(devicePath))
            return;
        var settings = settingsManager.GetUserSettings();
        var validBackupTasks = settings.BackupTasks
            .Select(task => new
            {
                Task = task,
                Credentials = settings.MigrateCredentialsStore.SingleOrDefault(mcs => mcs.Id == task.AccessDataId)
            })
            .Where(x => x.Credentials != null);
        
        foreach (var item in validBackupTasks)
        {
            if (item.Credentials is null) continue;
            kopiaHelper.MigrateRepository(item.Credentials);
        }
    }

    // helpers
    private static bool CheckMetaExists(string devicePath)
    {
        return File.Exists(FullPath(devicePath));
    }

    private static string FullPath(string devicePath)
    {
        return Path.Combine(devicePath, MetaDataFileName);
    }
}