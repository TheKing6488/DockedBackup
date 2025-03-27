using KopiaBackup.Lib.Interfaces.Services;

namespace KopiaBackup.Lib.Services;

public class FolderWatcherService(string folderPath, IBackupService backupService) : IFolderWatcherService
{
    public void Start()
    {
        var watcher = new FileSystemWatcher(folderPath)
        {
            NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite,
            Filter = "*.*"
        };

        // watcher.Changed += OnChanged;
        watcher.Created += OnChanged;
        // watcher.Deleted += OnChanged;
        // watcher.Renamed += OnRenamed;
        watcher.EnableRaisingEvents = true;
    }

    private void OnChanged(object sender, FileSystemEventArgs e)
    {
        backupService.TriggerBackups(e.FullPath);
    }

    // private void OnRenamed(object sender, RenamedEventArgs e)
    // {
    //     // Handle file renames
    // }
}