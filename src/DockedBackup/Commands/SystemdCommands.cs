using DockedBackup.Helpers;
using DockedBackup.Models.Kopia;
using DockedBackup.Models.Kopia.Options;
using DockedBackup.Models.Systemctl.Options;
using Tmds.DBus.Protocol;

namespace DockedBackup.Commands;

public  class SystemdCommands
{
    public async Task<int> CreateSystemdAsync(SystemctlOption systemctlOption, CancellationToken cancellationToken)
    {
        // var instance = $"dockedbackup@{systemctlOption.DeviceId}.service";
        //
        // var connection = new Connection(Address.System!);
        // await connection.ConnectAsync();
        //
        // var systemdManagerHelper = new SystemdManagerHelper(connection);
        // await systemdManagerHelper.ReloadAsync();
        //
        // await systemdManagerHelper.EnableUnitAsync(instance, runtime: false, force: true);
        // Console.WriteLine($"Enabled {instance}");
        //
        // var jobPath = await systemdManagerHelper.StartUnitAsync(instance, "replace");
        // Console.WriteLine($"Started {instance}, job: {jobPath}");
        //
        // return 0;
    }
}