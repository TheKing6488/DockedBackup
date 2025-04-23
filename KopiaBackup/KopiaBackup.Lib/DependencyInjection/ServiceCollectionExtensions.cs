using KopiaBackup.Lib.Helpers;
using KopiaBackup.Lib.Interfaces.Helpers;
using KopiaBackup.Lib.Interfaces.Repositories;
using KopiaBackup.Lib.Interfaces.Services;
using KopiaBackup.Lib.Repositories;
using KopiaBackup.Lib.Services;
using Microsoft.Extensions.DependencyInjection;

namespace KopiaBackup.Lib.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddKopiaBackupServices(this IServiceCollection services)
    {
        
        //Helpers
        services.AddSingleton<IKopiaHelper, KopiaHelper>();
        services.AddSingleton<IRcloneHelper, RcloneHelper>();
        services.AddSingleton<ISystemctlHelper, SystemctlHelper>();

        //Services
        services.AddSingleton<ISettingsManager, SettingsManager>();
        services.AddSingleton<IBackupService, BackupService>();
    }
}