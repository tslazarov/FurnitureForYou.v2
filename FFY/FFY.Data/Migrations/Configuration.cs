using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Migrations;

namespace FFY.Data.Migrations
{ 
    public sealed class Configuration : DbMigrationsConfiguration<FFY.Data.FFYDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(FFY.Data.FFYDbContext context)
        {
            context.Roles.AddOrUpdate(
                p => p.Name,
                new IdentityRole("Administrator"),
                new IdentityRole("Moderator"),
                new IdentityRole("User")
                );
        }
    }
}
