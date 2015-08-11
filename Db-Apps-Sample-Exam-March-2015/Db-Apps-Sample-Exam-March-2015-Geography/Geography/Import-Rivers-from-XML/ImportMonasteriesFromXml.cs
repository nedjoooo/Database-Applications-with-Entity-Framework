namespace Import_Rivers_from_XML
{
    using EF_Mappings;
    using System;
    using System.Xml.Linq;
    using System.Linq;

    class ImportMonasteriesFromXml
    {
        static void Main()
        {
            var context = new GeographyEntities();

            XDocument xmlDoc = XDocument.Load("../../rivers.xml");

            var riversByXml =
                from river in xmlDoc.Descendants("river")
                select new
                {
                    RiverName = river.Element("name").Value,
                    Length = river.Element("length").Value,
                    Outflow = river.Element("outflow").Value,
                    DrainageArea = river.Element("drainage-area") != null ? river.Element("drainage-area").Value : null,
                    AverageDischarge = river.Element("average-discharge") != null ? river.Element("average-discharge").Value : null,
                    Countries = river.Element("countries") != null ? river.Element("countries").Elements() : null
                };

            foreach (var river in riversByXml)
            {
                var riverDb = new River();

                riverDb.RiverName = river.RiverName;
                riverDb.Length = int.Parse(river.Length);
                riverDb.Outflow = river.Outflow;

                if(!string.IsNullOrEmpty(river.DrainageArea))
                {
                    riverDb.DrainageArea = int.Parse(river.DrainageArea);
                }

                if (!string.IsNullOrEmpty(river.AverageDischarge))
                {
                    riverDb.AverageDischarge = int.Parse(river.AverageDischarge);
                }

                if (river.Countries != null)
                {
                    foreach (var country in river.Countries)
                    {
                        var countryDb = context.Countries
                            .Where(c => c.CountryName == country.Value)
                            .FirstOrDefault();

                        riverDb.Countries.Add(countryDb);
                    }
                }

                context.Rivers.Add(riverDb);
            }

            context.SaveChanges();
        }
    }
}
