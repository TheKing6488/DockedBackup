﻿// using System.Diagnostics.CodeAnalysis;
// using CommandLine;
// using DockedBackup.Commands;
// using DockedBackup.Models.Kopia;
// using DockedBackup.DependencyInjection;
// using DockedBackup.Models.Kopia.Options;
// using DockedBackup.Models.Systemctl.Options;
// using Microsoft.Extensions.DependencyInjection;
// using Microsoft.Extensions.Hosting;
//
//
//
// var host = Host.CreateDefaultBuilder(args)
//     .UseConsoleLifetime()
//     .ConfigureServices((_, services) => { services.AddDockedBackupServices(); })
//     .Build();
//
// var ct = host.Services.GetRequiredService<IHostApplicationLifetime>().ApplicationStopping;
//
// // var rCloneHelper = host.Services.GetRequiredService<IRcloneHelper>();
// // var kopiaHelper = host.Services.GetRequiredService<IKopiaHelper>();
// // var backupService = host.Services.GetRequiredService<IBackupService>();
//
// var systemdCommands = host.Services.GetRequiredService<SystemdCommands>();
// var kopiaCommands = host.Services.GetRequiredService<KopiaCommands>();
//
//
// // await Parser.Default
// //     .ParseArguments<
// //         KopiaRepositoryConnect,
// //         CreateFilesystem,
// //         MigrateRepositoryOptions,
// //         // BackupTask,
// //         GetKopiaCredentialsOptions,
// //         SystemctlOptions
// //     >(args)
// //     .MapResult(
// //         (KopiaRepositoryConnect kopiaRepositoryConnect) =>
// //             kopiaCommands.RunCreateExternalS3ConfigAsync(kopiaRepositoryConnect, ct),
// //         (CreateFilesystem createFilesystem) => kopiaCommands.RunCreateRepositoryAsync(createFilesystem, ct),
// //         (MigrateRepositoryOptions migrateRepositoryOptions) =>
// //             kopiaCommands.RunAddKopiaMigrationAsync(migrateRepositoryOptions, ct),
// //         // (BackupTask backupTask)                => backupCommands.RunAddBackupTaskAsync(backupTask, ct),
// //         (GetKopiaCredentialsOptions getKopiaCredentialsOptions) =>
// //             kopiaCommands.RunGetAllKopiaMigrationsAsync(getKopiaCredentialsOptions, ct),
// //         (SystemctlOptions systemctlOptions) => systemdCommands.CreateSystemdAsync(systemctlOptions, ct),
// //         _ => Task.FromResult(1)
// //     );
//
//
// await Parser.Default
//     .ParseArguments<
//         SystemctlOptions
//     >(args)
//     .MapResult(
//         (SystemctlOptions systemctlOptions) => systemdCommands.CreateSystemdAsync(systemctlOptions, ct),
//         _ => Task.FromResult(1)
//     );
//
// await host.StopAsync(ct);


using CommandLine;
using DockedBackup.Commands;
using DockedBackup.DependencyInjection;
using DockedBackup.Interfaces.Helpers;
using DockedBackup.Interfaces.Services;
using DockedBackup.Models.Backups;
using DockedBackup.Models.Kopia;
using DockedBackup.Models.Systemctl.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


// var host = Host.CreateDefaultBuilder(args)
//     .ConfigureServices((_, services) =>
//     {
//         services.AddKopiaBackupServices();
//         services.AddSingleton<IWatcherCommands, WatcherCommands>();
//         services.AddHostedService<FolderWatcherService>(provider => new FolderWatcherService($"/run/media/{Environment.UserName}", provider.GetRequiredService<IBackupService>()));
//     })
//     .Build();

// var host = Host.CreateDefaultBuilder(args)
//     .ConfigureServices((_, services) => { services.AddDockedBackupServices(); })
//     .Build();


// var rcloneHelper = host.Services.GetRequiredService<IRcloneHelper>();
// var kopiaHelper = host.Services.GetRequiredService<IKopiaHelper>();
// var backupService = host.Services.GetRequiredService<IBackupService>();
// var watcherCommands = host.Services.GetRequiredService<IWatcherCommands>();

// var systemdCommands = host.Services.GetRequiredService<SystemdCommands>();


Parser.Default
    .ParseArguments<SystemctlOptions>(args)
    .MapResult(
                 (SystemctlOptions systemctl) => SystemdCommands.CreateSystemd(systemctl),
                 _ => 1);
