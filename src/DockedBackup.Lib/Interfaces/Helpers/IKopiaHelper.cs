using KopiaBackup.Lib.Models.Kopia;

namespace KopiaBackup.Lib.Interfaces.Helpers;

public interface IKopiaHelper
{
    IEnumerable<MigrateCredentialsStore> GetAllKopiaMigrateConfigs();
    void AddKopiaMigration(MigrateCredentialsStore migrateCredentialsStore);
    string CreateExternalS3Config(S3Credentials s3Credentials);
    string CreateRepositoryFilesystem(FilesystemCredentials filesystemCredentials);
    string MigrateRepository(MigrateCredentialsStore migrateCredentialsStore);
}