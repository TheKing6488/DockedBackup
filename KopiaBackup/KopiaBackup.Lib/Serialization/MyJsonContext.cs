using KopiaBackup.Lib.Models;
using KopiaBackup.Lib.Models.Backups;
using KopiaBackup.Lib.Models.Kopia;

namespace KopiaBackup.Lib.Serialization;

using System.Text.Json.Serialization;

[JsonSerializable(typeof(UserSettings))]
public partial class MyJsonContext : JsonSerializerContext
{
}