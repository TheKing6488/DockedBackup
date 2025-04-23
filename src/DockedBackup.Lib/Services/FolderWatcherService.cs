using KopiaBackup.Lib.Interfaces.Services;
using Microsoft.Extensions.Hosting;

namespace KopiaBackup.Lib.Services;

public class FolderWatcherService(string watchPath, IBackupService backupService) : IHostedService
{
    private FileSystemWatcher? _watcher;
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _watcher = new FileSystemWatcher(watchPath);
        _watcher.Created += OnChanged;
        _watcher.Deleted += OnDeleted;
        _watcher.EnableRaisingEvents = true;
        return Task.CompletedTask;
    }

    private void OnChanged(object sender, FileSystemEventArgs e)
    {
         backupService.TriggerBackups(e.FullPath);
    }
    
    private void OnDeleted(object sender, FileSystemEventArgs e)
    {
        backupService.TriggerBackups(e.FullPath);
    }
    
    public Task StopAsync(CancellationToken cancellationToken)
    {
        _watcher?.Dispose();
        return Task.CompletedTask;
    }
}