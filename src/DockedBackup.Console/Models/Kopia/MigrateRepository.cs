using CommandLine;

namespace KopiaBackup.Console.Models.Kopia;

[Verb("migrate", HelpText = "Establishing a connection to the repository")]
public record MigrateRepository
{
    [Option('s', "name", HelpText = "Name of the specific migration", Required = true)]
    public required string Name { get; set; }
    
    [Option('s', "source-config", HelpText = "Specifies the file path to the source configuration file", Required = true)]
    public required string SourceConfig { get; set; }
    
    [Option('c', "config-file", HelpText = "Specifies the file path to the destination configuration file", Required = true)]
    public required string ConfigFile { get; set; }
    
    [Option('p', "password", HelpText = "Specifies the repository password", Required = true)]
    public required string Passwort { get; set; }
}