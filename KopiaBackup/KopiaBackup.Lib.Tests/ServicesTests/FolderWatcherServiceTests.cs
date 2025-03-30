using System.Reflection;
using KopiaBackup.Lib.Interfaces.Services;
using KopiaBackup.Lib.Services;
using Moq;
using Shouldly;

namespace KopiaBackup.Lib.Tests.ServicesTests;

public class FolderWatcherServiceTests
{
    [Fact]
    public async Task OnChanged_Should_TriggerBackup()
    {
        // Arrange
        string watchPath = Path.GetTempPath();
        var backupServiceMock = new Mock<IBackupService>();
        var folderWatcher = new FolderWatcherService(watchPath, backupServiceMock.Object);
        await folderWatcher.StartAsync(CancellationToken.None);

        // Act
        string fileName = "testFile.txt";
        string expectedFullPath = Path.Combine(watchPath, fileName);
        var eventArgs = new FileSystemEventArgs(WatcherChangeTypes.Created, watchPath, fileName);

        // Use reflection to invoke the private OnChanged method.
        var onChangedMethod = typeof(FolderWatcherService)
            .GetMethod("OnChanged", BindingFlags.NonPublic | BindingFlags.Instance);
        onChangedMethod?.Invoke(folderWatcher, [null, eventArgs]);

        // Assert
        backupServiceMock.Verify(s => s.TriggerBackups(expectedFullPath), Times.Once);
    }

    [Fact]
    public async Task OnDeleted_Should_TriggerBackup()
    {
        // Arrange
        string watchPath = Path.GetTempPath();

        var backupServiceMock = new Mock<IBackupService>();
        var folderWatcher = new FolderWatcherService(watchPath, backupServiceMock.Object);
        await folderWatcher.StartAsync(CancellationToken.None);

        // Act
        string fileName = "testFile.txt";
        string expectedFullPath = Path.Combine(watchPath, fileName);
        var eventArgs = new FileSystemEventArgs(WatcherChangeTypes.Deleted, watchPath, fileName);

        // Use reflection to invoke the private OnDeleted method.
        var onDeletedMethod = typeof(FolderWatcherService)
            .GetMethod("OnDeleted", BindingFlags.NonPublic | BindingFlags.Instance);
        onDeletedMethod?.Invoke(folderWatcher, [null, eventArgs]);

        // Assert
        backupServiceMock.Verify(s => s.TriggerBackups(expectedFullPath), Times.Once);
    }
}

