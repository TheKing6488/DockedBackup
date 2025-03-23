using KopiaBackup.Lib.Models;

namespace KopiaBackup.Lib.Commons;

public class GlobalSettings
{
    public static UserSettings UserSettings { get; set; } = new();
}