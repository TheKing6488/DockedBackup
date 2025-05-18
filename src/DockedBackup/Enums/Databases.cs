using System.Runtime.Serialization;

namespace DockedBackup.Enums;

public enum Databases
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

