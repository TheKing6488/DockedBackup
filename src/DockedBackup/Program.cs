using CommandLine;
using DockedBackup.Commands;
using DockedBackup.DependencyInjection;
using DockedBackup.Models.Backups.Options;
using DockedBackup.Models.Kopia;
using DockedBackup.Models.Kopia.Options;
using DockedBackup.Models.Systemctl.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .UseConsoleLifetime()
    .ConfigureServices((_, services) => { services.AddDockedBackupServices(); })
    .Build();

var ct = host.Services.GetRequiredService<IHostApplicationLifetime>().ApplicationStopping;

var backupCommands = host.Services.GetRequiredService<BackupCommands>();
var systemdCommands = host.Services.GetRequiredService<SystemdCommands>();
var kopiaCommands = host.Services.GetRequiredService<KopiaCommands>();


await Parser.Default
    .ParseArguments<
        KopiaRepositoryConnectOption,
        CreateFilesystemOption,
        MigrateRepositoryOption,
        BackupTaskOption,
        GetKopiaCredentialsOption, 
        SystemctlOption
    >(args)
    .MapResult(
        (KopiaRepositoryConnectOption kopiaRepositoryConnect) =>
        kopiaCommands.RunCreateExternalS3ConfigAsync(kopiaRepositoryConnect, ct),
        (CreateFilesystemOption createFilesystem) => kopiaCommands.RunCreateRepositoryAsync(createFilesystem, ct),
        (MigrateRepositoryOption migrateRepositoryOption) =>
            kopiaCommands.RunAddKopiaMigrationAsync(migrateRepositoryOption, ct),
        (BackupTaskOption backupTaskOptions) => backupCommands.RunAddBackupTaskAsync(backupTaskOptions, ct),
        (GetKopiaCredentialsOption getKopiaCredentialsOption) =>
        kopiaCommands.RunGetAllKopiaMigrationsAsync(getKopiaCredentialsOption, ct),
        (SystemctlOption systemctlOptions) => systemdCommands.EnableSystemdAsync(systemctlOptions, ct),
        _ => Task.FromResult(1)
    );

await host.StopAsync(ct);