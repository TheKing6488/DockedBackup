using DockedBackup.Models.Systemctl.Options;

namespace DockedBackup.Interfaces.Commands;

public interface ISystemdCommands
{
    Task<int> EnableSystemdAsync(SystemctlOption systemctlOption, CancellationToken cancellationToken);

}