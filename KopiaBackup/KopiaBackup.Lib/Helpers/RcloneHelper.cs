using System.Diagnostics;

namespace KopiaBackup.Lib.Helpers;

public static class RcloneHelper
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
}