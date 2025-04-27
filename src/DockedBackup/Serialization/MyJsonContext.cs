using DockedBackup.Models;

namespace DockedBackup.Serialization;

using System.Text.Json.Serialization;

[JsonSerializable(typeof(UserSettings))]
public partial class MyJsonContext : JsonSerializerContext
{
}