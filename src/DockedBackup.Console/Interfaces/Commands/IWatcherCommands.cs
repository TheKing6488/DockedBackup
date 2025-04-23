using Microsoft.Extensions.Hosting;

namespace KopiaBackup.Console.Interfaces.Commands;

public interface IWatcherCommands
{
    int RunHostedService(IHost host);
    int StartService();
    int StopService();
    int EnableService();
    int DisableService();

}