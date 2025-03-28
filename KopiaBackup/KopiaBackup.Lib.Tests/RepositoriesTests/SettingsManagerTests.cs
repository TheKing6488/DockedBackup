using KopiaBackup.Lib.Models;
using KopiaBackup.Lib.Models.Backups;
using KopiaBackup.Lib.Models.Kopia;
using KopiaBackup.Lib.Repositories;
using Shouldly;

namespace KopiaBackup.Lib.Tests.RepositoriesTests;

public class SettingsManagerTests : IDisposable
{
    private const string SettingsFileName = "UserSettings.json";
    private readonly SettingsManager _settingsManager;

    public SettingsManagerTests()
    {
        if (File.Exists(SettingsFileName))
            File.Delete(SettingsFileName);
        _settingsManager = new SettingsManager();
    }

    public void Dispose()
    {
        if (File.Exists(SettingsFileName))
            File.Delete(SettingsFileName);
    }

    [Fact]
    public void GetUserSettingsAsync_FileExists_ShouldReturnNewUserSettings()
    {
        var userSettings = new UserSettings(new List<BackupTask>(), new List<MigrateCredentialsStore>());
        _settingsManager.SaveUserSettings(userSettings);

        var result = _settingsManager.GetUserSettings();

        result.ShouldNotBeNull();
        result.BackupTasks.ShouldBeEmpty();
        result.MigrateCredentialsStore.ShouldBeEmpty();
    }

    [Fact]
    public async Task SaveUserSettingsAsync_ShouldWriteFile()
    {
        var userSettings = new UserSettings(new List<BackupTask>(), new List<MigrateCredentialsStore>());
        _settingsManager.SaveUserSettings(userSettings);

        File.Exists(SettingsFileName).ShouldBeTrue();

        var json = await File.ReadAllTextAsync(SettingsFileName);
        json.ShouldNotBeNullOrWhiteSpace();
    }
}