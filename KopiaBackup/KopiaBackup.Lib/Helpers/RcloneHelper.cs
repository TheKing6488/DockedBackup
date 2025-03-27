using System.Diagnostics;
using KopiaBackup.Lib.Interfaces.Helpers;

namespace KopiaBackup.Lib.Helpers;

public class RcloneHelper : IRcloneHelper
{
    public static bool IsRcloneInstalled()
    {
        try
        {
            var processInfo = new ProcessStartInfo
            {
                FileName = "rclone",
                Arguments = "version",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using var process = Process.Start(processInfo);
            process?.WaitForExit();
            return process is { ExitCode: 0 };
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task RcloneConfigAsync()
    {
        try
        {
            var processInfo = new ProcessStartInfo
            {
                FileName = "rclone",
                Arguments = "config",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = new Process();
            process.StartInfo = processInfo;
            process.EnableRaisingEvents = true;

            process.OutputDataReceived += (_, args) =>
            {
                if (!string.IsNullOrEmpty(args.Data))
                {
                    Console.WriteLine($"Output: {args.Data}");
                }
            };

            process.ErrorDataReceived += (_, args) =>
            {
                if (!string.IsNullOrEmpty(args.Data))
                {
                    Console.WriteLine($"Error: {args.Data}");
                }
            };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            while (!process.HasExited)
            {
                var input = Console.ReadLine();
                if (input != null)
                {
                    await process.StandardInput.WriteLineAsync(input);
                }
            }
            Console.WriteLine($"Prozess beendet mit ExitCode: {process.ExitCode}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler beim Starten des Prozesses: {ex}");
            throw;
        }
    }
}