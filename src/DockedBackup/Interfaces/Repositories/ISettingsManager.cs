using DockedBackup.Models;

namespace DockedBackup.Interfaces.Repositories;

public interface ISettingsManager
{
    UserSettings GetUserSettings();
    void SaveUserSettings(UserSettings userSettings);
}
