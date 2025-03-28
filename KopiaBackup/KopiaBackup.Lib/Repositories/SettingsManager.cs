using System.Text.Json;
using KopiaBackup.Lib.Interfaces.Repositories;
using KopiaBackup.Lib.Models;
using KopiaBackup.Lib.Models.Backups;
using KopiaBackup.Lib.Models.Kopia;
using KopiaBackup.Lib.Serialization;

namespace KopiaBackup.Lib.Repositories;

public class SettingsManager : ISettingsManager
{
    private const string SettingsFileName = "UserSettings.json";
    private static readonly Mutex FileMutex = new Mutex(false, "Global\\KopiaBackup_SettingsMutex");

    public UserSettings GetUserSettings()
    {
        try
        {
            FileMutex.WaitOne();

            if (File.Exists(SettingsFileName))
            {
                var json = File.ReadAllText(SettingsFileName);
                return JsonSerializer.Deserialize(json, MyJsonContext.Default.UserSettings) ??
                       throw new NullReferenceException("UserSettings is null");
            }
            else
            {
                IList<BackupTask> backupTasks = new List<BackupTask>();
                IList<MigrateCredentialsStore> migrateCredentialsStores = new List<MigrateCredentialsStore>();
                return new UserSettings(backupTasks, migrateCredentialsStores);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occured while loading the settings file: {ex.Message}");
        }
        finally
        {
            FileMutex.ReleaseMutex();
        }
    }

    public void SaveUserSettings(UserSettings userSettings)
    {
        try
        {
            FileMutex.WaitOne();
            var json = JsonSerializer.Serialize(userSettings, MyJsonContext.Default.UserSettings);
            File.WriteAllText(SettingsFileName, json);
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occured while saving the settings file: {ex.Message}");
        }
        finally
        {
            FileMutex.ReleaseMutex();
        }
    }
}