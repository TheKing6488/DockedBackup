using Tmds.DBus;

namespace DockedBackup.Interfaces.DBus;

[DBusInterface("org.freedesktop.systemd1.Manager")]
public interface IManager : IDBusObject
{
    Task<ObjectPath> StartUnitAsync(string unit, string mode);
    Task<(bool carriesInstallInfo, (string origFile, string linkedFile, string state)[] changes)>
        EnableUnitFilesAsync(string[] files, bool runtime, bool force);
    Task<(string origFile, string linkedFile, string state)[]>
        DisableUnitFilesAsync(string[] files, bool runtime);
}