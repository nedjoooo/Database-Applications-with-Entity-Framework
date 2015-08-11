namespace Mountains_Code_First
{
    using Mountains_Code_First.Migrations;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class MountainsContext : DbContext
    {
        public MountainsContext()
            : base("name=MountainsContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MountainsContext, Configuration>());
        }

       public virtual DbSet<Country> Countries { get; set; }

       public virtual DbSet<Mountain> Mountains { get; set; }

       public virtual DbSet<Peak> Peaks { get; set; }
    }
}