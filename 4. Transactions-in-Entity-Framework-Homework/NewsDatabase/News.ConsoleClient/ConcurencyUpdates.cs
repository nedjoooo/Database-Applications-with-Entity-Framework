namespace News.ConsoleClient
{
    using System.Linq;
    using News.Data;
    using System;
    using System.Transactions;

    public class ConcurencyUpdates
    {
        public static void Update()
        {
            var context = new NewsContext();
            var news = context.News.FirstOrDefault();

            Console.WriteLine("System news content: {0}", news.Content);

            using(var dbContextTransaction = context.Database.BeginTransaction())
            {
                try
                {
                    Console.WriteLine("Update news with new content");
                    string updatedText = Console.ReadLine();

                    news.Content = updatedText;
                    context.SaveChanges();
                    dbContextTransaction.Commit();

                    Console.WriteLine("This news content will succesfully updated!");
                }
                catch(Exception ex)
                {
                    dbContextTransaction.Rollback();
                    Console.WriteLine("This news content not updated.");

                    Update();
                }
            }
        }
    }
}
