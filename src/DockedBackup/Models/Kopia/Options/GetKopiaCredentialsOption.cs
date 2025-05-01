using System.Diagnostics.CodeAnalysis;
using CommandLine;

namespace DockedBackup.Models.Kopia.Options;

[DynamicallyAccessedMembers(
    DynamicallyAccessedMemberTypes.PublicConstructors |
    DynamicallyAccessedMemberTypes.PublicProperties)]
[Verb("get-kopia-credentials", HelpText = "Establishing a connection to the repository.")]
public class GetKopiaCredentialsOption
{
    [Option('p', "password", Required = false, HelpText = "Passwort für das Repository")]
    public string Password { get; set; }
    
    [DynamicDependency(
        DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicProperties,
        typeof(GetKopiaCredentialsOption))]
    public GetKopiaCredentialsOption()
    {
    }
}