using System.Text.Json;
using DockedBackup.Interfaces.Repositories;
using DockedBackup.Models;
using DockedBackup.Models.Backups;
using DockedBackup.Models.Kopia;
using DockedBackup.Serialization;

namespace DockedBackup.Repositories;

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