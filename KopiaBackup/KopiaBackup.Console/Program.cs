using CommandLine;
using KopiaBackup.Console.Commands;
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
var folderWatcherService = serviceProvider.GetRequiredService<IFolderWatcherService>();
folderWatcherService.Start();

Parser.Default.ParseArguments<KopiaRepositoryConnect, CreateFilesystem, MigrateRepository>(args)
    .MapResult(
        (KopiaRepositoryConnect repositoryConnect) => KopiaCommands.RunCreateExternalS3Config(kopiaHelper, repositoryConnect),
        (CreateFilesystem createFilesystem) => KopiaCommands.RunCreateRepository(kopiaHelper, createFilesystem),
        (MigrateRepository migrateRepository) => KopiaCommands.RunMigrateRepository(kopiaHelper, migrateRepository),
        _ => 1);