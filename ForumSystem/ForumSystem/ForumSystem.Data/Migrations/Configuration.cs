namespace ForumSystem.Data.Migrations
{
    using ForumSystem.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ForumSystem.Data.ForumContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationDataLossAllowed = true;
            this.AutomaticMigrationsEnabled = true;
            this.ContextKey = "ForumSystem.Data.ForumContext";
        }

        protected override void Seed(ForumSystem.Data.ForumContext context)
        {
            context.Users.AddOrUpdate(
                u => u.Username,
                new User()
                {  
                    FirstName = "Gosho",
                    LastName = "Penchov",
                    Age = 15,
                    RegisteredOn = DateTime.Now,
                    Username = "peshov",
                    Gender = Gender.Male
                });

            context.SaveChanges();
        }
    }
}
