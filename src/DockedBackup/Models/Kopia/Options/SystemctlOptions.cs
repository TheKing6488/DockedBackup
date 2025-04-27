using CommandLine;

namespace DockedBackup.Models.Kopia.Options;

[Verb("systemctl", HelpText = "Führt systemctl-Befehle aus (start|stop|status).")]
public class SystemctlOptions
{
    [Option('d', "device-id", HelpText = "Name of the specific device", Required = true)]
    public required string DeviceId { get; set; }
    }