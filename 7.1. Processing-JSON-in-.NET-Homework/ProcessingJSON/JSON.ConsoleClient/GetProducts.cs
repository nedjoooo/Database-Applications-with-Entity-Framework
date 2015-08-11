namespace JSON.ConsoleClient
{
    using JSON.Data;
    using Newtonsoft.Json;
    using System;
    using System.Linq;
    using System.Web.Script.Serialization;

    class GetProducts
    {
        public static void ProductsInRange(JsonContext context)
        {
            var productsByRange = context.Products
                .Where(p => (p.Price >= 500 && p.Price <= 1000) && p.BuyerId == null)
                .Select(u => new
                {
                    name = u.Name,
                    price = u.Price,
                    seller = u.Seller.FirstName + " " + u.Seller.LastName
                });

            var serializer = new JavaScriptSerializer();
            var digitsInJson = serializer.Serialize(productsByRange);
            Console.WriteLine(digitsInJson);

            System.IO.File.WriteAllText(@"E:\SoftUni\Database Applications\7.1. Processing-JSON-in-.NET-Homework/products-in-range.json", digitsInJson);
        }
    }
}
