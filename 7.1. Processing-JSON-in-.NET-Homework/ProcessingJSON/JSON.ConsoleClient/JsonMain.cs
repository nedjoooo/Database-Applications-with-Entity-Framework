namespace JSON.ConsoleClient
{
    using JSON.Data;
    using System;
    using System.Linq;

    class JsonMain
    {
        static void Main()
        {
            var context = new JsonContext();

            GetProducts.ProductsInRange(context);          
        }
    }
}
