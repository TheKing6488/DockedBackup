namespace KopiaBackup.Lib.Models.Kopia;

public record MigrateCredentials(
    string SourceConfig,
    string Password
);
