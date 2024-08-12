namespace TaskManagementSystem.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TaskManagementSystem.Data;
    using TaskManagementSystem.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            if (!context.Users.Any())
            {
                context.Users.Add(new Users
                {
                    Username = "admin",
                    Password = "admin" ,
                    RoleId=2,
                });

                context.SaveChanges();

            }
        }
    }
}
