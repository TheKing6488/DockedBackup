using Tmds.DBus.Protocol;

namespace DockedBackup.Helpers;

public class SystemdManagerHelper(Connection connection)
{
    private const string Destination = "org.freedesktop.systemd1";
    private static readonly ObjectPath ManagerPath = new ObjectPath("/org/freedesktop/systemd1");
    private const string Interface = "org.freedesktop.systemd1.Manager";
    

    public Task ReloadAsync()
    {
        using var w = connection.GetMessageWriter();
        w.WriteMethodCallHeader(Destination, ManagerPath, Interface, member: "Reload");
        return connection.CallMethodAsync(w.CreateMessage());
    }


    public Task EnableUnitAsync(string unitName, bool runtime = false, bool force = false)
    {
        using var w = connection.GetMessageWriter();
        // EnableUnitFiles(a{sa{sv}} files, runtime, force)
        w.WriteMethodCallHeader(
            Destination, ManagerPath, Interface,
            signature: "asbb", member: "EnableUnitFiles");
        // Array of strings: manuell mit WriteArrayStart/WriteString/WriteArrayEnd
        var arrayStart = w.WriteArrayStart(DBusType.String);
        w.WriteString(unitName);
        w.WriteArrayEnd(arrayStart);
        w.WriteBool(runtime);
        w.WriteBool(force);
        return connection.CallMethodAsync(w.CreateMessage());
    }


    public Task<ObjectPath> StartUnitAsync(string unitName, string mode)
    {
        using var w = connection.GetMessageWriter();
        w.WriteMethodCallHeader(
            Destination, ManagerPath, Interface,
            signature: "ss", member: "StartUnit");
        w.WriteString(unitName);
        w.WriteString(mode);
        return connection.CallMethodAsync(
            w.CreateMessage(),
            (msg,_) => msg.GetBodyReader().ReadObjectPath());
    }
}