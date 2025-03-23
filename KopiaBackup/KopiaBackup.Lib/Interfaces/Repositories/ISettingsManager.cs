using KopiaBackup.Lib.Models;

namespace KopiaBackup.lib.Interfaces.Repositories;

public interface ISettingsManager
{
    Task LoadSettingsFromJsonAsync();
    Task SaveSettingsToJsonAsync(UserSettings userSettings);
}