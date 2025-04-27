using DockedBackup.Models.Kopia;

namespace DockedBackup.Interfaces.Helpers;

public interface IKopiaHelper
{
    IEnumerable<MigrateCredentialsStore> GetAllKopiaMigrateConfigs();
    void AddKopiaMigration(MigrateCredentialsStore migrateCredentialsStore, CancellationToken cancellationToken);
    Task<string> CreateExternalS3ConfigAsync(S3Credentials s3Credentials, CancellationToken cancellationToken);
    Task<string> CreateRepositoryFilesystemAsync(FilesystemCredentials filesystemCredentials,
        CancellationToken cancellationToken);
    Task<string> MigrateRepositoryAsync(MigrateCredentialsStore migrateCredentialsStore,
        CancellationToken cancellationToken);
}