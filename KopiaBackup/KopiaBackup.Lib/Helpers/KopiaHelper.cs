using System.Diagnostics;

namespace KopiaBackup.Lib.Helpers;

public static class KopiaHelper
{
    public static bool IsKopiaInstalled()
    {
        try
        {
            var processInfo = new ProcessStartInfo
            {
                FileName = "kopia",
                Arguments = "version",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            using var process = Process.Start(processInfo);
            process?.WaitForExitAsync();
            return process is { ExitCode: 0 };
        }
        catch (Exception)
        {
            return false;
        }
    }
}