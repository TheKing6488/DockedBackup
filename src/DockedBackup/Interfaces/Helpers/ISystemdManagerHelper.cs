using DockedBackup.Models.Systemctl.Options;

namespace DockedBackup.Interfaces.Helpers;

public interface ISystemdManagerHelper
{
    // One Service
    Task<string> EnableSystemctlService(SystemctlOption systemctlOption);
    Task<string> DisableSystemctlService(SystemctlOption systemctlOption);
    Task<string> StartSystemctlService(SystemctlOption systemctlOption);
    Task<string> StatusSystemctlService(SystemctlOption systemctlOption);
    
    //All Services
    Task<string> EnableAllSystemctlServices(SystemctlOption systemctlOption);
    Task<string> DisableAllSystemctlServices(SystemctlOption systemctlOption);
    Task<string> StartAllSystemctlServices(SystemctlOption systemctlOption);
    Task<string> StatusAllSystemctlServices(SystemctlOption systemctlOption);
}