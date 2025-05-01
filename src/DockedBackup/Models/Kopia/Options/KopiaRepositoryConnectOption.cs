using System.Diagnostics.CodeAnalysis;
using CommandLine;
using DockedBackup.Enums;
using DockedBackup.Models.Kopia.Options;

namespace DockedBackup.Models.Kopia.Options;

[Verb("connect", HelpText = "Establishing a connection to the repository")]
public class KopiaRepositoryConnectOption
{
    [Option('t', "type", HelpText = "Specifies the repository type (e.g., S3 = 0, filesystem = 1)", Required = true)]
    public required Provider Type { get; set; }
    
    [Option('b', "bucket", HelpText = "Specifies the S3 bucket name (required for S3 repositories)", Required = true)]
    public required string Bucket { get; set; }

    [Option('e', "endpoint", HelpText = "Specifies the endpoint URL for the S3 service (e.g., nbg1.your-objectstorage.com  )", Required = true)]
    public required string Endpoint { get; set; }
    
    [Option('a', "access-key", HelpText = "Specifies the access key for S3 authentication", Required = true)]
    public required string AccessKey { get; set; }
    
    [Option('s', "secret-access-key", HelpText = "Specifies the secret access key for S3 authentication", Required = true)]
    public required string SecretAccessKey { get; set; }
    
    [Option('p', "password", HelpText = "Specifies the repository password", Required = true)]
    public required string Passwort { get; set; }

    [Option('c', "config-file", HelpText = "Specifies the path to the repository configuration file", Required = true)]
    public required string ConfigFile { get; set; }
    

    [DynamicDependency(
        DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicProperties,
        typeof(KopiaRepositoryConnectOption))]
    public KopiaRepositoryConnectOption()
    {
    }
}