using CliWrap;
using CliWrap.Buffered;
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


    public async Task<string> StatusSystemctlService(SystemctlOption systemctlOption, CancellationToken cancellationToken)
    {
        var args = new[]
        {
            "status", systemctlOption.DeviceId
        };
        
        var cmd = Cli.Wrap("systemctl")
            .WithArguments(args)
            .WithValidation(CommandResultValidation.None);

        try
        {
            var result = await cmd.ExecuteBufferedAsync(cancellationToken);
            
            return !string.IsNullOrWhiteSpace(result.StandardError) ? result.StandardError : result.StandardOutput;
        }
        catch (OperationCanceledException)
        {
            return "The process was canceled.";
        }
        
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