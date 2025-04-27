namespace DockedBackup.Interfaces.Services;

public interface ISystemctlHelper
{
    public string StartService();
    public string StopService();
    public string EnableService();
    public string DisableService();
}