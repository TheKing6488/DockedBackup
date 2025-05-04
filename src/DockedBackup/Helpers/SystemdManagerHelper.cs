using DockedBackup.Interfaces.DBus;
using DockedBackup.Interfaces.Helpers;
using DockedBackup.Models.Systemctl.Options;
using Tmds.DBus;

namespace DockedBackup.Helpers;

public class SystemdManagerHelper : ISystemdManagerHelper
{
    public async Task<string> StartSystemctlService(SystemctlOption systemctlOption)
    {
        var unitName = CreateService(systemctlOption.DeviceId);
        var manager = await InitDBusHelper();

        try
        {
            await manager.StartUnitAsync(unitName, "replace");
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

        return $"{unitName} started";
    }


    public async Task<string> EnableSystemctlService(SystemctlOption systemctlOption)
    {
        var unitName = CreateService(systemctlOption.DeviceId);

        using var connection = new Connection(Address.System);
        await connection.ConnectAsync();

        var manager = connection.CreateProxy<IManager>(
            "org.freedesktop.systemd1",
            "/org/freedesktop/systemd1");

        try
        {
            await manager.EnableUnitFilesAsync
            (
                [unitName],
                runtime: false,
                force: false
            );
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

        return $"{unitName} created";
    }

    public async Task<string> DisableSystemctlService(SystemctlOption systemctlOption)
    {
        var unitName = CreateService(systemctlOption.DeviceId);
        var manager = await InitDBusHelper();

        try
        {
            await manager.DisableUnitFilesAsync
            (
                [unitName],
                runtime: false
            );
        }
        catch (Exception ex)
        {
            return ex.Message;
        }

        return $"{unitName} disabled";
    }


    public async Task<string> StatusSystemctlService(SystemctlOption systemctlOption)
    {
        var conn = new Connection(Address.System);
        await conn.ConnectAsync();

        var manager = conn.CreateProxy<IManager>(
            "org.freedesktop.systemd1",
            "/org/freedesktop/systemd1");

        // lädt die Unit und bekommt ihren Objektpfad
        var unitPath = await manager.LoadUnitAsync("dockedbackup@test.service");

        var unit = conn.CreateProxy<IUnit>(
            "org.freedesktop.systemd1",
            unitPath);

        // Properties auslesen
        string desc      = await unit.DescriptionAsync();
        string active    = await unit.ActiveStateAsync();
        string sub       = await unit.SubStateAsync();
        uint   pid       = await unit.MainPIDAsync();
        ulong  startedMs = await unit.ExecMainStartTimestampAsync();

        Console.WriteLine($"Unit: dockedbackup@test.service");
        Console.WriteLine($"  Description: {desc}");
        Console.WriteLine($"  Active: {active} ({sub})");
        Console.WriteLine($"  PID: {pid}");
        Console.WriteLine($"  Started: {DateTimeOffset.FromUnixTimeMilliseconds((long)startedMs)}");
        return "";
    }

    public Task<string> EnableAllSystemctlServices(SystemctlOption systemctlOption)
    {
        throw new NotImplementedException();
    }

    public Task<string> DisableAllSystemctlServices(SystemctlOption systemctlOption)
    {
        throw new NotImplementedException();
    }

    public Task<string> StartAllSystemctlServices(SystemctlOption systemctlOption)
    {
        throw new NotImplementedException();
    }


    public Task<string> StatusAllSystemctlServices(SystemctlOption systemctlOption)
    {
        throw new NotImplementedException();
    }


    // Helpers
    private async Task<IManager> InitDBusHelper()
    {
        var connection = new Connection(Address.System);
        await connection.ConnectAsync();

        return connection.CreateProxy<IManager>(
            "org.freedesktop.systemd1",
            "/org/freedesktop/systemd1");
    }

    private string CreateService(string deviceId)
    {
        return $"dockedbackup@{deviceId}.service";
    }
}