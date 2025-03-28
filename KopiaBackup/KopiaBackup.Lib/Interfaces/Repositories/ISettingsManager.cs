using KopiaBackup.Lib.Models;

namespace KopiaBackup.Lib.Interfaces.Repositories;

public interface ISettingsManager
{
    UserSettings GetUserSettings();
    void SaveUserSettings(UserSettings userSettings);
}
