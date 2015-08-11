namespace AdsSystem
{
    using System;
    using System.Linq;
    using System.Data.Entity;
    using System.Diagnostics;

    class Program
    {
        static void Main(string[] args)
        {
            var context = new AdsEntities();

            //  Problem 1.	Show Data from Related Tables

            //  Using Entity Framework write a SQL query to select all ads from the database and later print their title, 
            //  status, category, town and user. Do not use Include(…) for the relationships of the Ads. 
            //  Check how many SQL commands are executed with the SQL ExpressProfiler (or a similar tool).

            /*
            foreach (var ad in context.Ads)
            {
                Console.WriteLine("Title: {0}, Status: {1}, Category: {2}, Town: {3}, User: {4}",
                    ad.Title, 
                    ad.AdStatus == null ? "No status": ad.AdStatus.Status,
                    ad.Category == null ? "No category":ad.Category.Name, 
                    ad.Town == null ? "No town":ad.Town.Name, 
                    ad.AspNetUser.Name);
            }
            */

            //  27 commands executed

            //  Add Include(…) to select statuses, categories, towns and users along with all ads. 
            //  Compare the number of executed SQL statements and the performance before and after adding Include(…).

            /*
            foreach (var ad in context.Ads
                .Include(a => a.AdStatus)
                .Include(a => a.Category)
                .Include(a => a.Town)
                .Include(a => a.AspNetUser))
            {
                Console.WriteLine("Title: {0}, Status: {1}, Category: {2}, Town: {3}, User: {4}",
                    ad.Title,
                    ad.AdStatus == null ? "No status" : ad.AdStatus.Status,
                    ad.Category == null ? "No category" : ad.Category.Name,
                    ad.Town == null ? "No town" : ad.Town.Name,
                    ad.AspNetUser.Name);
            }
            */

            // 1 command executed.

            //---------------------------------------------------------------------------------------------------//

            //  Problem 2.	Play with ToList()

            //  Using Entity Framework select all ads from the database, then invoke ToList(), 
            //  then filter the categories whose status is Published; then select the ad title, category and town, 
            //  then invoke ToList() again and finally order the ads by publish date. 
            //  Rewrite the same query in a more optimized way and compare the performance.

            //  var sw = new Stopwatch();
            //  sw.Start();
            //  not optimize version
            /*
            GetAdsWhereStatusPublishedWithToListNonOptimized(context);
            Console.WriteLine(sw.Elapsed);
            */

            //  sw.Restart();
            //  optimize version
            /*
            GetAdsWhereStatusPublishedOptimized(context);
            Console.WriteLine(sw.Elapsed);
            */

            //  Problem 3.	Select Everything vs. Select Certain Columns

            var sw = new Stopwatch();
            sw.Start();
            SelectEverythingOfAds(context);
            Console.WriteLine(sw.Elapsed);

            sw.Restart();
            SelectTitleOfAds(context);
            Console.WriteLine(sw.Elapsed);
            
        }

        private static void SelectTitleOfAds(AdsEntities context)
        {
            var ads = context.Ads
                .Select(a => a.Title)
                .ToList();

            foreach (var ad in ads)
            {
                Console.WriteLine(ad);
            }
            Console.WriteLine();
        }

        private static void SelectEverythingOfAds(AdsEntities context)
        {
            var ads = context.Ads.ToList();

            foreach (var ad in ads)
            {
                Console.WriteLine(ad.Title);
            }
            Console.WriteLine();
        }

        private static void GetAdsWhereStatusPublishedOptimized(AdsEntities context)
        {
            var ads = context.Ads
                .Where(a => a.AdStatus.Status == "Published")
                .Select(a => new
                {
                    Title = a.Title,
                    Category = a.Category == null ? "No category" : a.Category.Name,
                    Town = a.Town == null ? "No town" : a.Town.Name,
                    PublishDate = a.Date
                })
                .OrderBy(a => a.PublishDate);

            foreach (var ad in ads)
            {
                Console.WriteLine("Title: {0}, Category: {1}, Town: {2}, Publish Date: {3}",
                    ad.Title, ad.Category, ad.Town, ad.PublishDate);
            }
        }

        private static void GetAdsWhereStatusPublishedWithToListNonOptimized(AdsEntities context)
        {
            var ads = context.Ads
                .ToList()
                .Where(a => a.AdStatus.Status == "Published")
                .Select(a => new
                {
                    Title = a.Title,
                    Category = a.Category == null ? "No category" : a.Category.Name,
                    Town = a.Town == null ? "No town" : a.Town.Name,
                    PublishDate = a.Date
                })
                .ToList()
                .OrderBy(a => a.PublishDate);

            foreach (var ad in ads)
            {
                Console.WriteLine("Title: {0}, Category: {1}, Town: {2}, Publish Date: {3}",
                    ad.Title, ad.Category, ad.Town, ad.PublishDate);
            }
        }
    }
}
