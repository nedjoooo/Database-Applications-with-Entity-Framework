﻿namespace Export_Monasteries_as_Xml
{
    using System.Linq;
    using EF_Mappings;
    using System;
    using System.Xml.Linq;
    using System.IO;

    class ExportMonasteriesAsXml
    {
        static void Main(string[] args)
        {
            var context = new GeographyEntities();

            var countriesQuery = context.Countries
                .Where(c => c.Monasteries.Count() > 0)
                .OrderBy(c => c.CountryName)
                .Select(c => new
                {
                    CountryName = c.CountryName,
                    Monasteries = c.Monasteries
                        .OrderBy(m => m.Name)
                        .Select(m => m.Name)
                });

            var xmlDoc = new XDocument();
            var xmlRoot = new XElement("monasteries");
            xmlDoc.Add(xmlRoot);

            foreach (var country in countriesQuery)
            {
                var countryXml = new XElement("country", new XAttribute("name", country.CountryName));
                xmlRoot.Add(countryXml);
                foreach (var monastery in country.Monasteries)
                {
                    var monasteryXml = new XElement("monastery", monastery);
                    countryXml.Add(monasteryXml);
                }
            }

            xmlDoc.Save("monasteries.xml");
        }
    }
}
