using System.Diagnostics;
using System.Text;
using KopiaBackup.Lib.Interfaces.Helpers;
using KopiaBackup.Lib.Interfaces.Repositories;
using KopiaBackup.Lib.Models.Kopia;
using KopiaBackup.Lib.Repositories;

namespace KopiaBackup.Lib.Helpers;

public class KopiaHelper(ISettingsManager settingsManager) : IKopiaHelper
{

    public static bool IsKopiaInstalled()
    {
        var processInfo = new ProcessStartInfo
        {
            FileName = "kopia",
            Arguments = "version",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };
        using var process = Process.Start(processInfo);
        process?.WaitForExitAsync();
        return process is { ExitCode: 0 };
    }

    public  IEnumerable<MigrateCredentialsStore> GetAllKopiaMigrateConfigs()
    {
        var settings =  settingsManager.GetUserSettings();
        return settings.MigrateCredentialsStore;
    }

    public void AddKopiaMigration(MigrateCredentialsStore migrateCredentialsStore)
    {
        var settings =  settingsManager.GetUserSettings();
        settings.MigrateCredentialsStore.Add( migrateCredentialsStore );
         settingsManager.SaveUserSettings(settings);
    }

    public string CreateExternalS3Config(S3Credentials s3Credentials)
    {
        var errorBuilder = new StringBuilder();
        
        var processInfo = new ProcessStartInfo
        {
            FileName = "kopia",
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true,
        };
        processInfo.ArgumentList.Add("repository");
        processInfo.ArgumentList.Add("connect");
        processInfo.ArgumentList.Add("s3");
        processInfo.ArgumentList.Add("--bucket");
        processInfo.ArgumentList.Add(s3Credentials.Bucket);
        processInfo.ArgumentList.Add("--endpoint");
        processInfo.ArgumentList.Add(s3Credentials.Endpoint);
        processInfo.ArgumentList.Add("--access-key");
        processInfo.ArgumentList.Add(s3Credentials.AccessKey);
        processInfo.ArgumentList.Add("--secret-access-key");
        processInfo.ArgumentList.Add(s3Credentials.SecretAccessKey);
        processInfo.ArgumentList.Add("--password");
        processInfo.ArgumentList.Add(s3Credentials.Passwort);
        processInfo.ArgumentList.Add("--config-file");
        processInfo.ArgumentList.Add(s3Credentials.ConfigFile);
        
        using var process = Process.Start(processInfo);
        if (process == null)
        {
            return "Process could not be started";
        }

        process.ErrorDataReceived += (_, e) =>
        {
            if (!string.IsNullOrWhiteSpace(e.Data))
            {
                errorBuilder.AppendLine(e.Data);
            }
        };

        process.BeginErrorReadLine();
        process.WaitForExit();
        if (process.ExitCode != 0)
        {
            return errorBuilder.ToString();
        }

        return "Connected to repository";
    }

    public string CreateRepositoryFilesystem(FilesystemCredentials filesystemCredentials)
    {     
        var errorBuilder = new StringBuilder();

        
        var processInfo = new ProcessStartInfo
        {
            FileName = "kopia",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        processInfo.ArgumentList.Add("repository");
        processInfo.ArgumentList.Add("create");
        processInfo.ArgumentList.Add("filesystem");
        processInfo.ArgumentList.Add("--path");
        processInfo.ArgumentList.Add(filesystemCredentials.Path);
        processInfo.ArgumentList.Add("--password");
        processInfo.ArgumentList.Add(filesystemCredentials.Password);
        using var process = Process.Start(processInfo);
        
        if (process == null)
        {
            return "Process could not be started";
        }

        process.ErrorDataReceived += (_, e) =>
        {
            if (!string.IsNullOrWhiteSpace(e.Data))
            {
                errorBuilder.AppendLine(e.Data);
            }
        };

        process.BeginErrorReadLine();
        process.WaitForExit();
        
        if (process.ExitCode != 0)
        {
            return errorBuilder.ToString();
        }
        //Todo change return
        return "Connected to repository";

    }

    public string MigrateRepository(MigrateCredentialsStore migrateCredentialsStore)
    {
        var errorBuilder = new StringBuilder();
        
        var processInfo = new ProcessStartInfo
        {
            FileName = "kopia",
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        processInfo.ArgumentList.Add("snapshot");
        processInfo.ArgumentList.Add("migrate");
        processInfo.ArgumentList.Add("--all");
        processInfo.ArgumentList.Add("--source-config");
        processInfo.ArgumentList.Add(migrateCredentialsStore.SourceConfig);
        processInfo.ArgumentList.Add("--config-file");
        processInfo.ArgumentList.Add(migrateCredentialsStore.ConfigFile);
        processInfo.ArgumentList.Add("--password");
        processInfo.ArgumentList.Add(migrateCredentialsStore.Password);

        using var process = Process.Start(processInfo);
        
        if (process == null)
        {
            return "Process could not be started";
        }
        
        process.ErrorDataReceived += (_, e) =>
        {
            if (!string.IsNullOrWhiteSpace(e.Data))
            {
                errorBuilder.AppendLine(e.Data);
            }
        };
        
        process.BeginErrorReadLine();
        process.WaitForExit();
        
        if (process.ExitCode != 0)
        {
            return errorBuilder.ToString();
        }
//TODO change return
        return "Connected to repository";
    }
}