namespace News.Data
{
    using News.Data.Migrations;
    using News.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;  

    public class NewsContext : DbContext
    {
        public NewsContext()
            : base("name=NewsContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<NewsContext, Configuration>());
        }

        public virtual DbSet<News> News { get; set; }
    }
}