namespace EF_Mappings
{
    public class ListContinents
    {
        static void Main(string[] args)
        {
            var context = new GeographyEntities();
            foreach (var counntry in context.Continents)
            {
                System.Console.WriteLine(counntry.ContinentName);
            }
        }
    }
}
