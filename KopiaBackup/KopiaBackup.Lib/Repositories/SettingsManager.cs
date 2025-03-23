using System.Text.Json;
using KopiaBackup.Lib.Commons;
using KopiaBackup.lib.Interfaces.Repositories;
using KopiaBackup.Lib.Models;

namespace KopiaBackup.Lib.Repositories;

public class SettingsManager : ISettingsManager
{
    public UserSettings UserSettings { get; private set; } = new();
    private const string SettingsFileName = "UserSettings.json";

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = true
    };


    public async Task LoadSettingsFromJsonAsync()
    {
        if (File.Exists(SettingsFileName))
        {
            string json = await File.ReadAllTextAsync(SettingsFileName);
            UserSettings = JsonSerializer.Deserialize<UserSettings>(json, JsonOptions) ?? new UserSettings();
        }
    }

    public async Task SaveSettingsToJsonAsync(UserSettings userSettings)
    {
        var json = JsonSerializer.Serialize(userSettings, JsonOptions);
        await File.WriteAllTextAsync(SettingsFileName, json);
        UserSettings = userSettings;
    }
}