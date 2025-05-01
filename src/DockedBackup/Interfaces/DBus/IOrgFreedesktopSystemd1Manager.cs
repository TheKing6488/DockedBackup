using Tmds.DBus;

namespace DockedBackup.Interfaces.DBus;

[DBusInterface("org.freedesktop.systemd1.Manager")]
interface IOrgFreedesktopSystemd1Manager : IDBusObject
{
    Task<ObjectPath> StartUnitAsync(string unit, string mode);
    Task<ObjectPath> StopUnitAsync(string unit, string mode);
}

