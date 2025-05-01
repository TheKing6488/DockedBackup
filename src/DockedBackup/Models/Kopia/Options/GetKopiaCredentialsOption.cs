using System.Diagnostics.CodeAnalysis;
using CommandLine;

namespace DockedBackup.Models.Kopia.Options;


[Verb("get-kopia-credentials", HelpText = "Establishing a connection to the repository.")]
public class GetKopiaCredentialsOption
{
    [Option('p', "password", Required = false, HelpText = "Passwort für das Repository")]
    public string Password { get; set; }
}