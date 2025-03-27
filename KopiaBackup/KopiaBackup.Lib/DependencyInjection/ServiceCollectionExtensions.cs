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
        services.AddSingleton<IKopiaHelper, KopiaHelper>();
        services.AddSingleton<IRcloneHelper, RcloneHelper>();

        //Services
        services.AddSingleton<ISettingsManager, SettingsManager>();
        services.AddSingleton<IBackupService, BackupService>();
        services.AddSingleton<IFolderWatcherService>(provider => new FolderWatcherService($"/run/media/{Environment.UserName}", provider.GetRequiredService<IBackupService>()));
        return services;
    }
}