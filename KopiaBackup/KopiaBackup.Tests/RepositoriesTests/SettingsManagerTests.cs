// using System.Text.Json;
// using KopiaBackup.Lib.Models;
// using KopiaBackup.Lib.Repositories;
// using Shouldly;
//
// namespace KopiaBackup.Tests.RepositoriesTests;
//
// public class SettingsManagerTests : IDisposable
// {
//     private readonly string _tempDir;
//     private readonly string _originalDir;
//
//     public SettingsManagerTests()
//     {
//         _originalDir = Directory.GetCurrentDirectory();
//         _tempDir = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
//         Directory.CreateDirectory(_tempDir);
//         Directory.SetCurrentDirectory(_tempDir);
//     }
//
//     public void Dispose()
//     {
//         Directory.SetCurrentDirectory(_originalDir);
//         Directory.Delete(_tempDir, true);
//     }
//
//     [Fact]
//     public async Task SaveSettingsToJsonAsync_ShouldWriteFileAndUpdateUserSettings()
//     {
//         // Arrange
//         var fileName = "UserSettings.json";
//         var settingsManager = new SettingsManager();
//         var testSettings = new UserSettings
//         {
//             BackupDeviceNames = "TestValue",
//         };
//         
//         // Act
//         await settingsManager.SaveSettingsToJsonAsync(testSettings);
//         
//         // Assert
//         File.Exists(fileName).ShouldBeTrue();
//         var fileContent = await File.ReadAllTextAsync(fileName);
//         fileContent.ShouldContain("TestValue");
//
//         settingsManager.UserSettings.ShouldBe(testSettings);
//     }
//
//     [Fact]
//     public async Task LoadSettingsFromJsonAsync_ShouldLoadFileAndUpdateUserSettings()
//     {
//         // Arrange
//         var expectedSettings = new UserSettings
//         {
//             BackupDeviceNames = "LoadedValue",
//         };
//
//         var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
//         var json = JsonSerializer.Serialize(expectedSettings, jsonOptions);
//         await File.WriteAllTextAsync("UserSettings.json", json);
//
//         var settingsManager = new SettingsManager();
//
//         // Act
//         await settingsManager.LoadSettingsFromJsonAsync();
//
//         // Assert
//         settingsManager.UserSettings.ShouldNotBeNull();
//         settingsManager.UserSettings.BackupDeviceNames.ShouldBe("LoadedValue");
//     }
// }