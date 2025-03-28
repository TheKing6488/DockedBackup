using CommandLine;

namespace KopiaBackup.Console.Models.Kopia
{
    /// <summary>
    /// Defines the options for the "get-kopia-credentials" command.
    /// </summary>
    [Verb("get-kopia-credentials", HelpText = "Establishing a connection to the repository.")]
    public record GetKopiaCredentialsOptions;
}