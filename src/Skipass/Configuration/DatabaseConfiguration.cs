using System;

namespace Skipass.Configuration;

class DatabaseConfiguration
{
    public string ConnectionString { get; set; }
    public Version DatabaseVersion { get; set; }
    public bool AutoMigrations { get; set; }
    public string MigrationsTable { get; set; }
}