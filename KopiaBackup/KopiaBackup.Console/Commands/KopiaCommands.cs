using KopiaBackup.Console.Enums;
using KopiaBackup.Console.Models.Kopia;
using KopiaBackup.Lib.Helpers;
using KopiaBackup.Lib.Interfaces.Helpers;
using KopiaBackup.Lib.Models.Kopia;

namespace KopiaBackup.Console.Commands;

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
        var migrateCredentials = new MigrateCredentials(
            migrateRepository.SourceConfig,
            migrateRepository.Passwort
        );
        var output = kopiaHelper.MigrateRepository(migrateCredentials);
        System.Console.WriteLine(output);
        return 0;
    }
}