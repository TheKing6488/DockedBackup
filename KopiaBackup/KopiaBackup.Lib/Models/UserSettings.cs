namespace KopiaBackup.Lib.Models;

public class UserSettings 
{
    public DayOfWeek Day { get; set; }
    public List<string> BackupDeviceNames {
        get;
        set;
    }
}