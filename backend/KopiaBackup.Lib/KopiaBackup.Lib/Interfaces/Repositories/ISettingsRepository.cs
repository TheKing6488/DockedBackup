using KopiaBackup.Lib.Model;

namespace KopiaBackup.Lib.Interfaces;

public interface ISettingsRepository
{
    Task<UserSettings> GetSettings();
    Task GetSettingsAsync();
    Task DeleteSettings();
}