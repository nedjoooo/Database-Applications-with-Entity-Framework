namespace News.ConsoleClient
{
    using System.Linq;
    using News.Data;
    using System;

    public class NewsMain
    {
        public static void Main(string[] args)
        {
            var context = new NewsContext();
            Console.WriteLine("Connecting to database ...\n");
            context.News.Count();

            Console.WriteLine("Application started.\n");
            ConcurencyUpdates.Update();     
        }
    }
}
