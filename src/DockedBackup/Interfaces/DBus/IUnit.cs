using Tmds.DBus;

namespace DockedBackup.Interfaces.DBus;

[DBusInterface("org.freedesktop.systemd1.Unit")]
public interface IUnit : IDBusObject
{
    // systemd-Properties, automatisch übersetzt:
    Task<string> DescriptionAsync();          
    Task<string> ActiveStateAsync();          
    Task<string> SubStateAsync();             
    Task<uint>   MainPIDAsync();              
    Task<ulong>  ExecMainStartTimestampAsync();
}