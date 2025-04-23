using KopiaBackup.Console.Enums;
using KopiaBackup.Console.Interfaces.Commands;
using KopiaBackup.Lib.Interfaces.Services;
using KopiaBackup.Lib.Models.Kopia;
using Microsoft.Extensions.Hosting;

namespace KopiaBackup.Console.Commands;

public class WatcherCommands(ISystemctlHelper systemctlHelper) : IWatcherCommands
{
    public int RunHostedService(IHost host)
    {
        host.RunAsync();
        return 0;
    }
    
    public int StopHostedService(IHost host)
    {
        Task.Run(() => host.StopAsync());
        return 0;
    }
    
    public int GetServiceStatus(IHost host)
    {
        return 0;
    }

    public int StartService()
    {
        systemctlHelper.StartService();
        return 0;
    }
    
    public int StopService()
    {
        systemctlHelper.StopService();
        return 0;
    }
    
    public int EnableService()
    {
        systemctlHelper.EnableService();
        return 0;
    }
    
    public int DisableService()
    {
        systemctlHelper.StopService();
        return 0;
    }
}