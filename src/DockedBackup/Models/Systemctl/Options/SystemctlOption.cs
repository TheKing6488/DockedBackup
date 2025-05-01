using CommandLine;

namespace DockedBackup.Models.Systemctl.Options;

[Verb("systemctl", HelpText = "Create a systemd service")]
public class SystemctlOption
{
    [Option('d', "device-id", Required = true, HelpText = "Name of the specific device")]
    public required string DeviceId { get; set; }
}