using System.Text.Json;
using KopiaBackup.Lib.Interfaces;
using KopiaBackup.Lib.Model;

namespace KopiaBackup.Lib.Repositories;

public class SettingsRepository : ISettingsRepository
{
    private readonly string _settingsFileName = "userSettings.json";
    public UserSettings UserSettings { get; private set; }
    
    
    public Task<UserSettings> GetSettings()
    {
        throw new NotImplementedException();
    }

    public Task GetSettingsAsync()
    {
        throw new NotImplementedException();
    }

    public async Task DeleteSettingsAsync(UserSettings newSettings)
    {
        UserSettings = newSettings;
        var json = JsonSerializer.Serialize(newSettings, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_settingsFileName, json);
    }
    
    public async Task SaveSettingsAsync(UserSettings newSettings)
    {
        UserSettings = newSettings;
        var json = JsonSerializer.Serialize(newSettings, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_settingsFileName, json);
    }
    
    private async Task<UserSettings> LoadSettings()
    {
        if (File.Exists(_settingsFileName))
        {
            try
            {
                var jsonContent = await File.ReadAllTextAsync(_settingsFileName);
                UserSettings = JsonSerializer.Deserialize<UserSettings>(jsonContent) ?? throw new InvalidOperationException();
            }
            catch (Exception ex)
            {
                //Implement logger
            }
        }
        //Implement Logger
    }
}