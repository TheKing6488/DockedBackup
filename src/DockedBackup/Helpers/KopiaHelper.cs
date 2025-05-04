using System.Diagnostics;
using System.Text;
using CliWrap;
using CliWrap.Buffered;
using DockedBackup.Interfaces.Helpers;
using DockedBackup.Interfaces.Repositories;
using DockedBackup.Models.Kopia;

namespace DockedBackup.Helpers;

public class KopiaHelper(ISettingsManager settingsManager) : IKopiaHelper
{
    
    public IEnumerable<MigrateCredentialsStore> GetAllKopiaMigrateConfigs()
    {
        var settings = settingsManager.GetUserSettings();
        return settings.MigrateCredentialsStore;
    }

    public void AddKopiaMigration(MigrateCredentialsStore migrateCredentialsStore, CancellationToken cancellationToken)
    {
        var settings = settingsManager.GetUserSettings();
        settings.MigrateCredentialsStore.Add(migrateCredentialsStore);
        settingsManager.SaveUserSettings(settings);
    }


    public async Task<string> CreateExternalS3ConfigAsync(
        S3Credentials s3Credentials,
        CancellationToken cancellationToken)
    {
        //TODO change arg to env with gnome keyring
        var args = new[]
        {
            "repository", "connect", "s3",
            "--bucket", s3Credentials.Bucket,
            "--endpoint", s3Credentials.Endpoint,
            "--config-file", s3Credentials.ConfigFile,
            "--access-key", s3Credentials.SecretAccessKey,
            "--secret-access-key", s3Credentials.SecretAccessKey,
            "--password", s3Credentials.Passwort,
            "--config-file", s3Credentials.ConfigFile
        };
        return await KopiaCliWrapAsync(args, cancellationToken);
    }

    public async Task<string> CreateRepositoryFilesystemAsync(FilesystemCredentials filesystemCredentials,
        CancellationToken cancellationToken)
    {
        //TODO change arg to env with gnome keyring
        var args = new[]
        {
            "repository", "create", "filesystem",
            "--path", filesystemCredentials.Path,
            "--password", filesystemCredentials.Password,
        };
        return await KopiaCliWrapAsync(args, cancellationToken);
    }


    public async Task<string> MigrateRepositoryAsync(MigrateCredentialsStore migrateCredentialsStore, CancellationToken cancellationToken)
    {
        //TODO change arg to env with gnome keyring
        var args = new[]
        {
            "migrateCredentialsStore", "migrate", "--all", 
            "--source-config", migrateCredentialsStore.SourceConfig,
            "--config-file", migrateCredentialsStore.ConfigFile, 
            "--password", migrateCredentialsStore.Password
        };
        
        return await KopiaCliWrapAsync(args, cancellationToken);
    }


    private async Task<string> KopiaCliWrapAsync(string[] args, CancellationToken cancellationToken)
    {
        var cmd = Cli.Wrap("kopia")
            .WithArguments(args)
            .WithValidation(CommandResultValidation.None);

        try
        {
            var result = await cmd.ExecuteBufferedAsync(cancellationToken);
            return !string.IsNullOrWhiteSpace(result.StandardError) ? result.StandardError : result.StandardOutput;
        }
        catch (OperationCanceledException)
        {
            return "The process was canceled.";
        }
    }
}