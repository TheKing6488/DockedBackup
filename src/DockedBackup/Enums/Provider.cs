using System.Runtime.Serialization;

namespace DockedBackup.Enums;

public enum Provider
{
    [EnumMember(Value = "s3")]
    S3,
    [EnumMember(Value = "filesystem")]
    Filesystem
}