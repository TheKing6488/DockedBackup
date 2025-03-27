using System.Text.Json;
using KopiaBackup.lib.Interfaces.Repositories;
using KopiaBackup.Lib.Interfaces.Services;
using KopiaBackup.Lib.Models;

namespace KopiaBackup.Lib.Services;

public class BackupService(ISettingsManager settingsManager) : IBackupService
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

    public void AddBackupJob()
    {
        
    }
    
    public void TriggerBackups(string devicePath)
    {
        if (!CheckMetaExists(devicePath)) return;
        File.WriteAllText(devicePath + "test.txt", "Dies ist ein Test String");
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