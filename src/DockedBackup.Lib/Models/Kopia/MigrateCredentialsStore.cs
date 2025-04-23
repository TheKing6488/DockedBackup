namespace KopiaBackup.Lib.Models.Kopia;
public record MigrateCredentialsStore
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public  required string Name { get; init; }
    public required string SourceConfig { get; init; }
    public required string ConfigFile { get; init; }
    public required string Password { get; init; }

}
