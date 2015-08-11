namespace Export_Rivers_as_Json
{
    using System.Linq;
    using EF_Mappings;
    using System.Web.Script.Serialization;
    using System;
    using System.IO;

    class ExportRiversAsJson
    {
        static void Main(string[] args)
        {
            var context = new GeographyEntities();

            var riversQuery = context.Rivers
                .OrderByDescending(r => r.Length)
                .Select(r => new
                {
                    riverName = r.RiverName,
                    riverLength = r.Length,
                    countries = r.Countries
                        .OrderBy(c => c.CountryName)
                        .Select(c => c.CountryName)
                });

            var json = new JavaScriptSerializer().Serialize(riversQuery);

            File.WriteAllText(@"rivers.json", json);
        }
    }
}
