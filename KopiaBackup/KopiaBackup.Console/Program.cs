using CommandLine;
using KopiaBackup.Console.Commands;
using KopiaBackup.Console.Models.Backups;
using KopiaBackup.Console.Models.Kopia;
using KopiaBackup.Lib.DependencyInjection;
using KopiaBackup.Lib.Interfaces.Helpers;
using KopiaBackup.Lib.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;


var service = new ServiceCollection();

service.AddKopiaBackupServices();
var serviceProvider = service.BuildServiceProvider();
var rcloneHelper = serviceProvider.GetRequiredService<IRcloneHelper>();
var kopiaHelper = serviceProvider.GetRequiredService<IKopiaHelper>();
var backupService = serviceProvider.GetRequiredService<IBackupService>();
// var folderWatcherService = serviceProvider.GetRequiredService<IFolderWatcherService>();
// folderWatcherService.Start();

// Globaler Handler für nicht abgefangene Ausnahmen auf dem Hauptthread
AppDomain.CurrentDomain.UnhandledException += (sender, eventArgs) =>
{
    var ex = (Exception)eventArgs.ExceptionObject;
    Console.Error.WriteLine("Unhandled exception: " + ex.Message);
    // Hier kannst du auch Logging oder Cleanup durchführen
};


Parser.Default.ParseArguments<KopiaRepositoryConnect, CreateFilesystem, MigrateRepository, BackupTask, GetKopiaCredentialsOptions>(args)
    .MapResult(
        (KopiaRepositoryConnect repositoryConnect) => KopiaCommands.RunCreateExternalS3Config(kopiaHelper, repositoryConnect),
        (CreateFilesystem createFilesystem) => KopiaCommands.RunCreateRepository(kopiaHelper, createFilesystem),
        (MigrateRepository migrateRepository) => KopiaCommands.RunAddKopiaMigration(kopiaHelper, migrateRepository),
        (BackupTask backupTask) => BackupCommands.RunAddBackupTask(backupService, backupTask),
        (GetKopiaCredentialsOptions _) => KopiaCommands.RunGetAllKopiaMigrations(kopiaHelper),
        
        _ => 1);