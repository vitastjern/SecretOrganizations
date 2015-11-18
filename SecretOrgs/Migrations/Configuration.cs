namespace SecretOrgs.Migrations
{
    using Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SecretOrgs.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SecretOrgs.Models.ApplicationDbContext context)
        {

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            if (!roleManager.RoleExists("KeeperOfSecrets"))
            {
                var role = new IdentityRole { Name = "KeeperOfSecrets" };
                roleManager.Create(role);
            }

            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);
            foreach (string email in new[] { "vitastjern@googlemail.com", "secret@mi5.org", "secret@cia.org" })  //skapar anonym lista med 2 strängar och loopar över denna
            {
                if (!context.Users.Any(u => u.UserName == email))
                {
                    var user = new ApplicationUser { UserName = email, Email = email };
                    manager.Create(user, "foobar"); // sätt lösenordet till foobar, men hashat...
                }
            }

            var keeper = manager.FindByName("secret@mi5.org");
            var result = manager.AddToRole(keeper.Id, "KeeperOfSecrets");


            context.Organizations.AddOrUpdate(o => o.OrganizationName,
                                              new Organization { OrganizationName = "MI5" },
                                              new Organization { OrganizationName = "CIA" },
                                              new Organization { OrganizationName = "FOA" }
                );

        }
    }
}
