using System.ComponentModel;
using System.Runtime.CompilerServices;
using DockedBackup.Models.Backups;
using DockedBackup.Models.Kopia;

namespace DockedBackup.Models;

public record UserSettings(IList<BackupTaskTest> BackupTasks, IList<MigrateCredentialsStore> MigrateCredentialsStore);

