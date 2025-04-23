using CommandLine;

namespace KopiaBackup.Console.Models.Kopia;

    [Verb("get-kopia-credentials", HelpText = "Establishing a connection to the repository.")]
    public record GetKopiaCredentialsOptions;
