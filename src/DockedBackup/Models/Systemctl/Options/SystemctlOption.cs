using CommandLine;
using DockedBackup.Enums;

namespace DockedBackup.Models.Systemctl.Options;

[Verb("systemctl", HelpText = "Create a systemd service")]
public class SystemctlOption
{

    [Value(0, MetaName = "SystemctlAction", Required = true,
        HelpText = "Action to perform: start, enable, disable")]
    public SystemctlAction SystemctlAction { get; set; }
    
    [Value(1, MetaName = "device-id", Required = true,
        HelpText = "Identifier of the specific device")]
    public string DeviceId { get; set; }
}