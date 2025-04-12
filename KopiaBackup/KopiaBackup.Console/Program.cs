using CommandLine;
using KopiaBackup.Console.Commands;
using KopiaBackup.Console.Models.Backups;
using KopiaBackup.Console.Models.Kopia;
using KopiaBackup.Lib.DependencyInjection;
using KopiaBackup.Lib.Interfaces.Helpers;
using KopiaBackup.Lib.Interfaces.Services;
using KopiaBackup.Lib.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddKopiaBackupServices();
        services.AddHostedService<FolderWatcherService>(provider => new FolderWatcherService($"/run/media/{Environment.UserName}", provider.GetRequiredService<IBackupService>()));
    })
    .Build();


var serviceProvider = host.Services;
var rcloneHelper = serviceProvider.GetRequiredService<IRcloneHelper>();
var kopiaHelper = serviceProvider.GetRequiredService<IKopiaHelper>();
var backupService = serviceProvider.GetRequiredService<IBackupService>();



Parser.Default.ParseArguments<KopiaRepositoryConnect, CreateFilesystem, MigrateRepository, BackupTask, GetKopiaCredentialsOptions>(args)
    .MapResult(
        (KopiaRepositoryConnect repositoryConnect) => KopiaCommands.RunCreateExternalS3Config(kopiaHelper, repositoryConnect),
        (CreateFilesystem createFilesystem) => KopiaCommands.RunCreateRepository(kopiaHelper, createFilesystem),
        (MigrateRepository migrateRepository) => KopiaCommands.RunAddKopiaMigration(kopiaHelper, migrateRepository),
        (BackupTask backupTask) => BackupCommands.RunAddBackupTask(backupService, backupTask),
        (GetKopiaCredentialsOptions _) => KopiaCommands.RunGetAllKopiaMigrations(kopiaHelper),
        _ => 1);



        
// await host.RunAsync();