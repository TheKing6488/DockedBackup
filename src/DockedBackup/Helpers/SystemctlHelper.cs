using System.Diagnostics;
using System.Text;
using DockedBackup.Interfaces.Services;

namespace DockedBackup.Helpers;

public class SystemctlHelper : ISystemctlHelper
{
    private string ExecuteSystemctl(string action)
    {
        var errorBuilder = new StringBuilder();
        
        var psi = new ProcessStartInfo
        {
            FileName               = "systemctl",
            Arguments              = $"{action} dockedbackup.service",
            RedirectStandardOutput = true,
            RedirectStandardError  = true,
            UseShellExecute        = false,
            CreateNoWindow         = true
        };
        using var process = Process.Start(psi);
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
    
    public string StartService()
        => ExecuteSystemctl("start");

    public string StopService()
        => ExecuteSystemctl("stop");

    public string EnableService()
        => ExecuteSystemctl("enable");

    public string DisableService()
        => ExecuteSystemctl("disable");
}