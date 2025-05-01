using DockedBackup.Enums;
using DockedBackup.Models.Kopia;
using DockedBackup.Helpers;
using DockedBackup.Interfaces.Helpers;
using DockedBackup.Models.Kopia;
using DockedBackup.Models.Kopia.Options;

namespace DockedBackup.Commands;
// TODO change to a instance for better handling and interface support
public class KopiaCommands(IKopiaHelper kopiaHelper)
{
    public async Task<int> RunCreateExternalS3ConfigAsync(KopiaRepositoryConnectOption kopiaRepositoryConnectOption, CancellationToken cancellationToken)
    {
        var s3Credentials = new S3Credentials(
            kopiaRepositoryConnectOption.Bucket,
            kopiaRepositoryConnectOption.Endpoint,
            kopiaRepositoryConnectOption.AccessKey,
            kopiaRepositoryConnectOption.SecretAccessKey,
            kopiaRepositoryConnectOption.Passwort,
            kopiaRepositoryConnectOption.ConfigFile
        );

        switch (kopiaRepositoryConnectOption.Type)
        {
            case Provider.S3:
                var output = await kopiaHelper.CreateExternalS3ConfigAsync(s3Credentials, cancellationToken);
                Console.WriteLine(output);
                break;
        }
        return 0;
    }
    
    public async Task<int> RunAddKopiaMigrationAsync(MigrateRepositoryOption migrateRepositoryOption, CancellationToken cancellationToken)
    {
        var migrateCredentialsStore = new MigrateCredentialsStore
        {
            Name = migrateRepositoryOption.Name,
            SourceConfig = migrateRepositoryOption.SourceConfig,
            ConfigFile = migrateRepositoryOption.ConfigFile,
            Password = migrateRepositoryOption.Passwort
        };
        kopiaHelper.AddKopiaMigration(migrateCredentialsStore, cancellationToken);
        return 0;
    }
    public async Task<int> RunCreateRepositoryAsync(CreateFilesystemOption createFilesystemOption, CancellationToken cancellationToken)
    {
        var filesystemCredentials = new FilesystemCredentials(
            createFilesystemOption.Path,
            createFilesystemOption.Passwort
        );

        switch (createFilesystemOption.Type)
        {
            case Provider.Filesystem:
                var output = await kopiaHelper.CreateRepositoryFilesystemAsync(filesystemCredentials, cancellationToken);
                Console.WriteLine(output);
                break;
        }
        return 0;
    }
    public async Task<int> RunMigrateRepositoryAsync(MigrateRepositoryOption migrateRepositoryOption, CancellationToken cancellationToken)
    {
        var migrateCredentials = new MigrateCredentialsStore
        {
            Name = migrateRepositoryOption.Name,
            SourceConfig = migrateRepositoryOption.SourceConfig,
            ConfigFile = migrateRepositoryOption.ConfigFile,
            Password = migrateRepositoryOption.Passwort
        };

        var output = await kopiaHelper.MigrateRepositoryAsync(migrateCredentials, cancellationToken);
        Console.WriteLine(output);
        return 0;
    }
    public async Task<int> RunGetAllKopiaMigrationsAsync(GetKopiaCredentialsOption getKopiaCredentialsOptions, CancellationToken cancellationToken)
    {
        var output = kopiaHelper.GetAllKopiaMigrateConfigs();

        const int idWidth = 36;
        const int nameWidth = 20;
        const int sourceConfigWidth = 20;
        const int configFileWidth = 20;
        const int passwordWidth = 20;

        var header = $"{ "ID",-idWidth} | {"Name",-nameWidth} | {"Source Config",-sourceConfigWidth} | {"Config File",-configFileWidth} | {"Password",-passwordWidth}";
        Console.WriteLine(header);
        Console.WriteLine(new string('-', header.Length));

        foreach (var migrateConfig in output)
        {
            var line = $"{migrateConfig.Id,-idWidth} | {migrateConfig.Name,-nameWidth} | {migrateConfig.SourceConfig,-sourceConfigWidth} | {migrateConfig.ConfigFile,-configFileWidth} | {migrateConfig.Password,-passwordWidth}";
            Console.WriteLine(line);
        }
        return 0;
    }
}