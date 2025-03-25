using System.Diagnostics;

namespace KopiaBackup.Lib.Helpers;

public class RcloneHelper
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
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using var process = new Process();
            process.StartInfo = processInfo;
            process.EnableRaisingEvents = true;

            // Event-Handler registrieren, um die Ausgabe zu überwachen
            process.OutputDataReceived += (sender, args) =>
            {
                if (!string.IsNullOrEmpty(args.Data))
                {
                    Console.WriteLine($"Output: {args.Data}");
                }
            };

            process.ErrorDataReceived += (sender, args) =>
            {
                if (!string.IsNullOrEmpty(args.Data))
                {
                    Console.WriteLine($"Error: {args.Data}");
                }
            };

            // Prozess starten und asynchron die Ausgaben lesen
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            // Warten, bis der Prozess beendet ist
            await process.WaitForExitAsync();

            Console.WriteLine($"Prozess beendet mit ExitCode: {process.ExitCode}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Fehler beim Starten des Prozesses: {ex}");
            throw;
        }
    }
}