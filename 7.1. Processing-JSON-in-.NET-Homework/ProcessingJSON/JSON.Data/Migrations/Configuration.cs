namespace JSON.Data.Migrations
{
    using JSON.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<JSON.Data.JsonContext>
    {
        public Configuration()
        {
            AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = true;
            ContextKey = "JSON.Data.JsonContext";
        }

        protected override void Seed(JSON.Data.JsonContext context)
        {
            //  SeedUsers(context);

            //  SeedProducts(context);

            //  SeedCategories(context);

            //  SeedRandomCategoriesInProducts(context);
        }

        private static void SeedRandomCategoriesInProducts(JSON.Data.JsonContext context)
        {
            Random rnd = new Random();
            var products = context.Products.ToList();
            var categories = context.Categories.ToList();

            foreach (var product in products)
            {
                int productCategoriesCount = rnd.Next(0, categories.Count());
                for (int i = 0; i < productCategoriesCount; i++)
                {
                    int categoryPosition = rnd.Next(1, categories.Count());
                    var randomCategory = categories.ElementAt(categoryPosition);
                    product.Categories.Add(randomCategory);
                }
            }
        }

        private static void SeedCategories(JSON.Data.JsonContext context)
        {
            List<Category> categories = new List<Category>();
            using (StreamReader r = new StreamReader("../../../../categories.json"))
            {
                string json = r.ReadToEnd();
                categories = JsonConvert.DeserializeObject<List<Category>>(json);
            }

            foreach (var category in categories)
            {
                var dbCategory = new Category();
                dbCategory.Name = category.Name;

                context.Categories.Add(dbCategory);
            }

            context.SaveChanges();
        }

        private static void SeedProducts(JSON.Data.JsonContext context)
        {
            List<Product> products = new List<Product>();
            using (StreamReader r = new StreamReader("../../../../products.json"))
            {
                string json = r.ReadToEnd();
                products = JsonConvert.DeserializeObject<List<Product>>(json);
            }

            foreach (var product in products)
            {
                var dbProduct = new Product();
                Random rnd = new Random();
                var usersId = context.Users.Select(u => u.Id).ToList();
                int randomSellerPosition = rnd.Next(1, usersId.Count());
                int randomSellerId = usersId.ElementAt(randomSellerPosition);
                dbProduct.SellerId = randomSellerId;

                int checkWouldBuyer = rnd.Next(1, 4);
                if(checkWouldBuyer < 4)
                {
                    int randomBuyerPosition = rnd.Next(1, usersId.Count());
                    if(randomBuyerPosition != randomSellerPosition)
                    {
                        int randomBuyerId = usersId.ElementAt(randomBuyerPosition);
                        dbProduct.BuyerId = randomBuyerId;
                    }
                }

                if (!string.IsNullOrEmpty(product.Name))
                {
                    dbProduct.Name = product.Name;
                }

                dbProduct.Price = product.Price;
 
                context.Products.Add(dbProduct);
            }

            context.SaveChanges();
        }

        private static void SeedUsers(JSON.Data.JsonContext context)
        {
            XDocument xmlDoc = XDocument.Load("../../../../users.xml");

            var users =
                from user in xmlDoc.Descendants("user")
                select new
                {
                    FirstName = user.Attribute("first-name") != null ? user.Attribute("first-name").Value : null,
                    LastName = user.Attribute("last-name").Value,
                    Age = user.Attribute("age") != null ? user.Attribute("age").Value : null
                };

            foreach (var user in users)
            {
                var dbUser = new User();

                if(!string.IsNullOrEmpty(user.FirstName))
                {
                    dbUser.FirstName = user.FirstName;
                }

                if(!string.IsNullOrEmpty(user.Age))
                {
                    dbUser.Age = int.Parse(user.Age);
                }

                dbUser.LastName = user.LastName;

                context.Users.Add(dbUser);
            }

            context.SaveChanges();
        }
    }
}
