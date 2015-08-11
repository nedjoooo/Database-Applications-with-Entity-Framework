namespace Rivers_By_Country
{
    using EF_Mappings;
    using System.Xml.Linq;
    using System.Xml.XPath;
    using System.Linq;
    using System;
    using System.Collections.Generic;

    class RiversByCountry
    {
        static void Main()
        {
            var context = new GeographyEntities();          
            XDocument xmlDocInput = XDocument.Load("../../rivers-query.xml");
            var queryResults = new XElement("results");
            var xmlDoc = new XDocument();
            var xmlRoot = new XElement("results");
            xmlDoc.Add(xmlRoot);

            foreach (var queryElement in xmlDocInput.XPathSelectElements("/queries/query"))
            {
                var riversQuery = context.Rivers.AsEnumerable();
                foreach (var countryElement in queryElement.XPathSelectElements("country"))
                {
                    var countryName = countryElement.Value;
                    riversQuery = riversQuery.Where(
                        r => r.Countries.Any(c => c.CountryName == countryName)
                    );
                }
                var maxResultsAttribute = queryElement.Attribute("max-results");
                int maxResults = maxResultsAttribute != null ? int.Parse(maxResultsAttribute.Value) : 0;
                if (maxResultsAttribute != null)
                {                   
                    riversQuery = riversQuery
                        .OrderBy(r => r.RiverName);
                }
                int totalRivers = riversQuery.Count();
                riversQuery = riversQuery.Take(maxResults);
                var riverNames = riversQuery.Select(r => r.RiverName);
                var xmlRiversElement = new XElement("rivers");
                var riverTotalCountAttribute = new XAttribute("total-count", totalRivers);
                var riverListedCount = new XAttribute("listed-count", riverNames.Count());
                xmlRiversElement.Add(riverTotalCountAttribute);
                xmlRiversElement.Add(riverListedCount);
                xmlRoot.Add(xmlRiversElement);

                foreach (var river in riverNames)
                {
                    var riverXmlElement = new XElement("river", river);
                    xmlRiversElement.Add(riverXmlElement);
                }

                Console.WriteLine(xmlRoot);
            }
        }
    }
}
