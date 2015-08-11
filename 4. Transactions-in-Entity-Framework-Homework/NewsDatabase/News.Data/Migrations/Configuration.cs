namespace News.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<News.Data.NewsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "News.Data.NewsContext";
        }

        protected override void Seed(News.Data.NewsContext context)
        {
            SeedNews(context);
        }

        private void SeedNews(News.Data.NewsContext context)
        {
            //for (int i = 0; i < 20; i++)
            //{
            //    var news1 = new Models.News
            //    {
            //        Content = "News" + i
            //    };

            //    context.News.Add(news1);
            //}

            //context.SaveChanges();
        }
    }
}
