using System.Runtime.Serialization;

namespace DockedBackup.Enums;

public enum Database
{
    [EnumMember(Value = "postgresql")]
    PostgreSql,
    [EnumMember(Value = "mysql")]
    MySql,   
    [EnumMember(Value = "mariadb")]
    MariaDb,   
    [EnumMember(Value = "mongodb")]
    MongoDb
}

