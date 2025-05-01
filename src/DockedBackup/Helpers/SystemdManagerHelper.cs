using DockedBackup.Interfaces.DBus;
using DockedBackup.Models.Systemctl.Options;
using Tmds.DBus;


namespace DockedBackup.Helpers;

public class SystemdManagerHelper
{
    static async Task<int> RunSystemctlAsync(SystemctlOption systemctlOption)
    {
        string unitName = $"dockedbackup@{systemctlOption.DeviceId}.service";

        using var connection = new Connection(Address.System);
        await connection.ConnectAsync();

        var manager = connection.CreateProxy<IOrgFreedesktopSystemd1Manager>(
            "org.freedesktop.systemd1",
            "/org/freedesktop/systemd1");
        try
        {
                await manager.StartUnitAsync(unitName, "replace");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error controlling {unitName}: {ex.Message}");
            return 1;
        }
        return 0;
    }
}