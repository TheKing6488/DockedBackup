using KopiaBackup.Lib.Models;

namespace KopiaBackup.lib.Interfaces.Repositories;

public interface ISettingsManager
{
    public UserSettings UserSettings { get; }
    Task LoadSettingsFromJsonAsync();
    Task SaveSettingsToJsonAsync(UserSettings userSettings);
}