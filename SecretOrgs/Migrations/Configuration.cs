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



            foreach (string role in new[] { "KeeperOfSecrets", "Big Boss", "Boss", "Grunt", "Intern" })
            {
                if (!roleManager.RoleExists(role))
                {
                    roleManager.Create(new IdentityRole { Name = role });
                }
            }


            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);

            int i = 0;
            
            foreach (string email in new[] {"vitastjern@googlemail.com", 
                                            "bigboss@mi5.org", 
                                            "boss@gru.ru", 
                                            "grunt@cia.org", 
                                            "intern@foa.se" })
            {

                if (!context.Users.Any(u => u.UserName == email))
                {
                    i += 5;
                    manager.Create(new ApplicationUser { UserName = email, Email = email, BirthDate = DateTime.Now.AddYears(-(50-i)), Title = "00"+i }, "foobar"); // sätt lösenordet till foobar, men hashat...
                }
            }

            var keeper = manager.FindByName("vitastjern@googlemail.com");
            foreach (string role in new[] { "KeeperOfSecrets", "Big Boss", "Boss", "Grunt", "Intern" })
            {
                if (!keeper.Roles.Any(r => r.RoleId == roleManager.FindByName(role).Id))
                {
                    manager.AddToRoles(keeper.Id, role);
                }
            }


            keeper = manager.FindByName("bigboss@mi5.org");
            foreach (string role in new[] { "Big Boss", "Boss", "Grunt", "Intern" })
            {
                if (!keeper.Roles.Any(r => r.RoleId == roleManager.FindByName(role).Id))
                {
                    manager.AddToRole(keeper.Id, role);
                }
            }

            keeper = manager.FindByName("boss@gru.ru");
            foreach (string role in new[] { "Boss", "Grunt", "Intern" })
            {
                if (!keeper.Roles.Any(r => r.RoleId == roleManager.FindByName(role).Id))
                {
                    manager.AddToRole(keeper.Id, role);
                }
            }

            keeper = manager.FindByName("grunt@cia.org");
            foreach (string role in new[] { "Grunt", "Intern" })
            {
                if (!keeper.Roles.Any(r => r.RoleId == roleManager.FindByName(role).Id))
                {
                    manager.AddToRole(keeper.Id, role);
                }
            }

            keeper = manager.FindByName("intern@foa.se");
            foreach (string role in new[] { "Intern" })
            {
                if (!keeper.Roles.Any(r => r.RoleId == roleManager.FindByName(role).Id))
                {
                    manager.AddToRole(keeper.Id, role);
                }
            }



            context.Organizations.AddOrUpdate(o => o.OrganizationName,
                                              new Organization { OrganizationName = "MI5" },
                                              new Organization { OrganizationName = "CIA" },
                                              new Organization { OrganizationName = "FOA" },
                                              new Organization { OrganizationName = "GRU" },
                                              new Organization { OrganizationName = "FDA" }
                );

        }
    }
}
