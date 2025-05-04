using System.Text;
using DockedBackup.Enums;
using DockedBackup.Interfaces.Helpers;
using DockedBackup.Models.Systemctl.Options;

namespace DockedBackup.Commands;

public  class SystemdCommands(ISystemdManagerHelper systemdManagerHelper)
{
    public async Task<int> EnableSystemdAsync(SystemctlOption systemctlOption, CancellationToken cancellationToken)
    {
        var result = systemctlOption.SystemctlAction switch
        {
            SystemctlAction.start => await systemdManagerHelper.StartSystemctlService(systemctlOption),
            SystemctlAction.enable => await systemdManagerHelper.EnableSystemctlService(systemctlOption),
            SystemctlAction.disable => await systemdManagerHelper.DisableSystemctlService(systemctlOption),
            SystemctlAction.status => await systemdManagerHelper.StatusSystemctlService(systemctlOption),
            _ => $"{systemctlOption.SystemctlAction} is unsupported"
        };
        Console.WriteLine(result);
        return 0;
    }
}