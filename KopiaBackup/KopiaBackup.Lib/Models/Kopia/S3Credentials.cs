namespace KopiaBackup.Lib.Models.Kopia;

public record S3Credentials(
    string Bucket,
    string Endpoint,
    string AccessKey,
    string SecretAccessKey,
    string Passwort,
    string ConfigFile);