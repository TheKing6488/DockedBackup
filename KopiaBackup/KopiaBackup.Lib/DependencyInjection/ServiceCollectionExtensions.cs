using KopiaBackup.Lib.Helpers;
using KopiaBackup.Lib.Interfaces.Helpers;
using KopiaBackup.lib.Interfaces.Repositories;
using KopiaBackup.Lib.Interfaces.Services;
using KopiaBackup.Lib.Repositories;
using KopiaBackup.Lib.Services;
using Microsoft.Extensions.DependencyInjection;

namespace KopiaBackup.Lib.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddKopiaBackupServices(this IServiceCollection services)
    {
        
        var localizer = new ResourceLocalizerHelper();
        if (!RcloneHelper.IsRcloneInstalled())
        {
            throw new InvalidOperationException(localizer.GetString("RcloneNotFound"));
        }
        
        //Helpers
        services.AddSingleton<IResourceLocalizerHelper, ResourceLocalizerHelper>();
        //Services
        services.AddSingleton<ISettingsManager, SettingsManager>();
        services.AddSingleton<IBackupService, BackupService>();
        return services;
    }
}