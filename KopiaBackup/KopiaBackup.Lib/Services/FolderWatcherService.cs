using KopiaBackup.Lib.Interfaces.Services;

namespace KopiaBackup.Lib.Services;

public class FolderWatcherService(string folderPath, IBackupService backupService) : IFolderWatcherService
{
    public void Start()
    {
        var watcher = new FileSystemWatcher(folderPath)
        {
            NotifyFilter = NotifyFilters.DirectoryName | NotifyFilters.Attributes | NotifyFilters.LastWrite ,
            Filter = "*",
            IncludeSubdirectories = false
        };
        
        Console.WriteLine("Start watching");


        // watcher.Changed += OnChanged;
        // watcher.Created += OnChanged;
        // watcher.Deleted += OnChanged;
        
        watcher.Created += OnUsbMounted;
        watcher.Deleted += OnUsbRemoved;
        
        // watcher.Renamed += OnRenamed;
        watcher.EnableRaisingEvents = true;
    }

    // private void OnChanged(object sender, FileSystemEventArgs e)
    // {
    //     Console.WriteLine("File watcher: " + e.FullPath);
    //     backupService.TriggerBackups(e.FullPath);
    // }
    
    
    
    private void OnUsbMounted(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine($"USB-Stick eingesteckt: {e.FullPath}");
        backupService.TriggerBackups(e.FullPath);
    }

    private void OnUsbRemoved(object sender, FileSystemEventArgs e)
    {
        Console.WriteLine($"USB-Stick entfernt: {e.FullPath}");
        // Optional: Aktionen beim Entfernen
    }

    // private void OnRenamed(object sender, RenamedEventArgs e)
    // {
    //     // Handle file renames
    // }
}