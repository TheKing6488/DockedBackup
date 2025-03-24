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
        if (CheckFileExists(devicePath)) return;
        MetaData metaData = new()
        {
            LastBackup = null
        };
        var jsonString = JsonSerializer.Serialize(metaData);
        File.WriteAllText(FullPath(devicePath), jsonString);
    }
    
    public void CreateBackup()
    {
        
    }

    public bool CheckDevice(string devicePath)
    {
        return CheckFileExists(devicePath);
    }
    
    //helpers
    private static bool CheckFileExists(string devicePath)
    {
        return File.Exists(FullPath(devicePath));
    }

    private static string FullPath(string devicePath)
    {
        return Path.Combine(devicePath, MetaDataFileName);
    }
}