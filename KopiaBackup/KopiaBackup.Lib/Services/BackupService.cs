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
        // if (!CheckMetaExists(devicePath)) return;
        try
        {
            string testFilePath = Path.Combine(devicePath, "test.txt");
            var dirInfo = new DirectoryInfo(devicePath);
            Console.WriteLine($"Owner: {dirInfo.GetAccessControl().GetOwner(typeof(System.Security.Principal.NTAccount))}");

            
            File.WriteAllText(testFilePath, "Dies ist ein Test String");
            Console.WriteLine("Datei geschrieben: " + testFilePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Fehler beim Schreiben: " + ex.Message);
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