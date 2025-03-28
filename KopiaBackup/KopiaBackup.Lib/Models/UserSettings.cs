using System.ComponentModel;
using System.Runtime.CompilerServices;
using KopiaBackup.Lib.Models.Backups;
using KopiaBackup.Lib.Models.Kopia;

namespace KopiaBackup.Lib.Models;

public record UserSettings(IList<BackupTask> BackupTasks, IList<MigrateCredentialsStore> MigrateCredentialsStore);

