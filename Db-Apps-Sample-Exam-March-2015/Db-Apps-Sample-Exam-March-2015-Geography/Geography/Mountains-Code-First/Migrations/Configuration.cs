namespace Mountains_Code_First.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MountainsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(MountainsContext context)
        {
            if(context.Countries.Count() == 0)
            {
                SeedData(context);
            }          
        }

        private static void SeedData(MountainsContext context)
        {
            var bulgaria = new Country { Code = "BG", Name = "Bulgaria" };
            context.Countries.Add(bulgaria);
            var germany = new Country { Code = "DE", Name = "Germany" };
            context.Countries.Add(germany);

            var pirin = new Mountain { Name = "Pirin", Countries = { bulgaria } };
            context.Mountains.Add(pirin);
            var rila = new Mountain { Name = "Rila", Countries = { bulgaria } };
            context.Mountains.Add(rila);
            var rodopes = new Mountain { Name = "Rodopi", Countries = { bulgaria } };
            context.Mountains.Add(rodopes);

            var musala = new Peak { Name = "Musala", Elevation = 2925, Mountain = rila };
            context.Peaks.Add(musala);
            var malyovitsa = new Peak { Name = "Malyovitsa", Elevation = 2729, Mountain = rila };
            context.Peaks.Add(malyovitsa);
            var vihren = new Peak { Name = "Vihren", Elevation = 2914, Mountain = pirin };
            context.Peaks.Add(vihren);

            context.SaveChanges();
        }
    }
}
