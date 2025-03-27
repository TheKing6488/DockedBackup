using CommandLine;

namespace KopiaBackup.Console.Models.Kopia;

[Verb("migrate", HelpText = "Establishing a connection to the repository")]
public record MigrateRepository
{
    [Option('t', "type", HelpText = "Path to the config remote config file", Required = true)]
    public required string SourceConfig { get; set; }
    
    [Option('p', "password", HelpText = "Specifies the repository password", Required = true)]
    public required string Passwort { get; set; }
}