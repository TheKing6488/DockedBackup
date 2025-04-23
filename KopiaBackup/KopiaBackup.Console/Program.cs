using CommandLine;
using KopiaBackup.Console.Commands;
using KopiaBackup.Console.Interfaces.Commands;
using KopiaBackup.Console.Models.Backups;
using KopiaBackup.Console.Models.Kopia;
using KopiaBackup.Console.Models.Watchers;
using KopiaBackup.Lib.DependencyInjection;
using KopiaBackup.Lib.Interfaces.Helpers;
using KopiaBackup.Lib.Interfaces.Services;
using KopiaBackup.Lib.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddKopiaBackupServices();
        services.AddSingleton<IWatcherCommands, WatcherCommands>();
        services.AddHostedService<FolderWatcherService>(provider => new FolderWatcherService($"/run/media/{Environment.UserName}", provider.GetRequiredService<IBackupService>()));
    })
    .Build();


var rcloneHelper = host.Services.GetRequiredService<IRcloneHelper>();
var kopiaHelper = host.Services.GetRequiredService<IKopiaHelper>();
var backupService = host.Services.GetRequiredService<IBackupService>();
var watcherCommands = host.Services.GetRequiredService<IWatcherCommands>();



Parser.Default
    .ParseArguments<KopiaRepositoryConnect, CreateFilesystem, MigrateRepository, BackupTask,
        GetKopiaCredentialsOptions, FileWatcher>(args)
    .MapResult(
        (KopiaRepositoryConnect repositoryConnect) =>
            KopiaCommands.RunCreateExternalS3Config(kopiaHelper, repositoryConnect),
        (CreateFilesystem createFilesystem) => KopiaCommands.RunCreateRepository(kopiaHelper, createFilesystem),
        (MigrateRepository migrateRepository) => KopiaCommands.RunAddKopiaMigration(kopiaHelper, migrateRepository),
        (BackupTask backupTask) => BackupCommands.RunAddBackupTask(backupService, backupTask),
        (GetKopiaCredentialsOptions _) => KopiaCommands.RunGetAllKopiaMigrations(kopiaHelper),
        (FileWatcher _) => watcherCommands.RunHostedService(host),
        _ => 1);



        
// await host.RunAsync();