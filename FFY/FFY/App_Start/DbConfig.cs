using FFY.Data;
using FFY.Data.Migrations;
using System.Data.Entity;

namespace FFY.Web.App_Start
{
    public class DbConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<FFYDbContext, Configuration>());
            FFYDbContext.Create().Database.Initialize(true);
        }
    }
}