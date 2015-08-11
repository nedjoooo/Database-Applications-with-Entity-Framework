namespace ForumSystem.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Models;
    using ForumSystem.Data.Migrations;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class ForumContext : DbContext
    {
        public ForumContext()
            : base("ForumContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ForumContext, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<User>()
                .HasMany(u => u.Friends)
                .WithMany()
                .Map(m =>
                {
                    m.MapLeftKey("UserId");
                    m.MapRightKey("FriendId");
                    m.ToTable("UserFriends");
                });

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
    }
}