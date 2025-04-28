 using System.Diagnostics.CodeAnalysis;
 using CommandLine;

 namespace DockedBackup.Models.Systemctl.Options;

 [Verb("systemctl", HelpText = "Create a systemd service")]
 [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors|DynamicallyAccessedMemberTypes.PublicProperties)]
 public record SystemctlOptions
 {
     [Option('d', "device-id", HelpText = "Name of the specific device", Required = true)]
     public required string DeviceId { get; set; }
 }
// [Verb("systemctl", HelpText = "Create a systemd service")]
// public record SystemctlOptions(
//     [property: Option('d',"device-id", Required = true)] string DeviceId);
