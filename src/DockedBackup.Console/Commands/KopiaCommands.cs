using KopiaBackup.Console.Enums;
using KopiaBackup.Console.Models.Kopia;
using KopiaBackup.Lib.Helpers;
using KopiaBackup.Lib.Interfaces.Helpers;
using KopiaBackup.Lib.Models.Kopia;

namespace KopiaBackup.Console.Commands;
// TODO change to a instance for better handling and interface support
public static class KopiaCommands
{
    public static int RunCreateExternalS3Config(IKopiaHelper kopiaHelper, KopiaRepositoryConnect kopiaRepositoryConnect)
    {
        var s3Credentials = new S3Credentials(
            kopiaRepositoryConnect.Bucket,
            kopiaRepositoryConnect.Endpoint,
            kopiaRepositoryConnect.AccessKey,
            kopiaRepositoryConnect.SecretAccessKey,
            kopiaRepositoryConnect.Passwort,
            kopiaRepositoryConnect.ConfigFile
        );

        switch (kopiaRepositoryConnect.Type)
        {
            case Provider.S3:
                var output = kopiaHelper.CreateExternalS3Config(s3Credentials);
                System.Console.WriteLine(output);
                break;
        }
        return 0;
    }
    public static int RunAddKopiaMigration(IKopiaHelper kopiaHelper, MigrateRepository migrateRepository)
    {
        var migrateCredentialsStore = new MigrateCredentialsStore
        {
            Name = migrateRepository.Name,
            SourceConfig = migrateRepository.SourceConfig,
            ConfigFile = migrateRepository.ConfigFile,
            Password = migrateRepository.Passwort
        };
        kopiaHelper.AddKopiaMigration(migrateCredentialsStore);
        return 0;
    }
    public static int RunCreateRepository(IKopiaHelper kopiaHelper, CreateFilesystem createFilesystem)
    {
        var filesystemCredentials = new FilesystemCredentials(
            createFilesystem.Path,
            createFilesystem.Passwort
        );

        switch (createFilesystem.Type)
        {
            case Provider.Filesystem:
                var output = kopiaHelper.CreateRepositoryFilesystem(filesystemCredentials);
                System.Console.WriteLine(output);
                break;
        }

        return 0;
    }
    public static int RunMigrateRepository(IKopiaHelper kopiaHelper, MigrateRepository migrateRepository)
    {
        var migrateCredentials = new MigrateCredentialsStore
        {
            Name = migrateRepository.Name,
            SourceConfig = migrateRepository.SourceConfig,
            ConfigFile = migrateRepository.ConfigFile,
            Password = migrateRepository.Passwort
        };

        var output = kopiaHelper.MigrateRepository(migrateCredentials);
        System.Console.WriteLine(output);
        return 0;
    }
    public static int RunGetAllKopiaMigrations(IKopiaHelper kopiaHelper)
    {
        var output = kopiaHelper.GetAllKopiaMigrateConfigs();

        const int idWidth = 36;
        const int nameWidth = 20;
        const int sourceConfigWidth = 20;
        const int configFileWidth = 20;
        const int passwordWidth = 20;

        var header = $"{ "ID",-idWidth} | {"Name",-nameWidth} | {"Source Config",-sourceConfigWidth} | {"Config File",-configFileWidth} | {"Password",-passwordWidth}";
        System.Console.WriteLine(header);
        System.Console.WriteLine(new string('-', header.Length));

        foreach (var migrateConfig in output)
        {
            var line = $"{migrateConfig.Id,-idWidth} | {migrateConfig.Name,-nameWidth} | {migrateConfig.SourceConfig,-sourceConfigWidth} | {migrateConfig.ConfigFile,-configFileWidth} | {migrateConfig.Password,-passwordWidth}";
            System.Console.WriteLine(line);
        }
        return 0;
    }
}