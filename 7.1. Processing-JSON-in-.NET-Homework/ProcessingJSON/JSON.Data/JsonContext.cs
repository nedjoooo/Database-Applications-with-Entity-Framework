namespace JSON.Data
{
    using JSON.Data.Migrations;
    using JSON.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;

    public class JsonContext : DbContext
    {
        public JsonContext()
            : base("name=JsonContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<JsonContext, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<User>()
                .HasMany(u => u.Friends)
                .WithMany()
                .Map(m =>
                {
                    m.MapLeftKey("Id");
                    m.MapRightKey("Friend_Id");
                    m.ToTable("UserFriends");
                });

            base.OnModelCreating(modelBuilder);
        }

        public virtual IDbSet<User> Users { get; set; }
        public virtual IDbSet<Category> Categories { get; set; }
        public virtual IDbSet<Product> Products { get; set; }
    }
}