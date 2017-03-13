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
            context.Roles.Add(new IdentityRole("User"));
            context.Roles.Add(new IdentityRole("Administrator"));
            context.Roles.Add(new IdentityRole("Moderator"));
        }
    }
}
