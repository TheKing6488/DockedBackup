using DockedBackup.Commands;
using DockedBackup.Helpers;
using DockedBackup.Interfaces.Helpers;
using DockedBackup.Interfaces.Repositories;
using DockedBackup.Interfaces.Services;
using DockedBackup.Repositories;
using DockedBackup.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DockedBackup.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static void AddDockedBackupServices(this IServiceCollection services)
    {
        //Commands
        services.AddSingleton<SystemdCommands>();
        services.AddSingleton<KopiaCommands>();
        services.AddSingleton<BackupCommands>();
        
        //Helpers
        services.AddSingleton<IKopiaHelper, KopiaHelper>();
        services.AddSingleton<IRcloneHelper, RcloneHelper>();
        services.AddSingleton<ISystemctlHelper, SystemctlHelper>();

        //Services
        services.AddSingleton<ISettingsManager, SettingsManager>();
        services.AddSingleton<IBackupService, BackupService>();

    }
}