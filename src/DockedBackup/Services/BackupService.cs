using System.Text.Json;
using DockedBackup.Interfaces.Repositories;
using DockedBackup.Interfaces.Services;
using DockedBackup.Models;
using DockedBackup.Models.Backups;
using DockedBackup.Interfaces.Helpers;


namespace DockedBackup.Services;

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


//TODO fix this function
    // public async Task TriggerBackups(string devicePath)
    // {
    //     if (!CheckMetaExists(devicePath))
    //         return;
    //     var settings = settingsManager.GetUserSettings();
    //     var validBackupTasks = settings.BackupTasks
    //         .Select(task => new
    //         {
    //             Task = task,
    //             Credentials = settings.MigrateCredentialsStore.SingleOrDefault(mcs => mcs.Id == task.AccessDataId)
    //         })
    //         .Where(x => x.Credentials != null);
    //     
    //     foreach (var item in validBackupTasks)
    //     {
    //         if (item.Credentials is null) continue;
    //         kopiaHelper.MigrateRepositoryAsync(item.Credentials);
    //     }
    // }

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