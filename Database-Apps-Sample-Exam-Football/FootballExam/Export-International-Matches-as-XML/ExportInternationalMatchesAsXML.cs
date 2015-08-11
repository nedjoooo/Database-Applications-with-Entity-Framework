namespace Export_International_Matches_as_XML
{
    using System.Linq;
    using System;
    using System.Xml.Linq;
    using System.IO;
    using EFMappings;
    using System.Globalization;

    class ExportInternationalMatchesAsXML
    {
        static void Main(string[] args)
        {
            var context = new FootballEntities();

            var internationalMatches = context.InternationalMatches
                .OrderBy(m => m.MatchDate)
                .ThenBy(m => m.CountryHome.CountryName)
                .ThenBy(m => m.CountryAway.CountryName)
                .Select(m => new
                {
                    MatchDate = m.MatchDate != null ? m.MatchDate : null,
                    HomeCountryCode = m.HomeCountryCode,
                    AwayCountryCode = m.AwayCountryCode,
                    HomeTeam = m.CountryHome.CountryName,
                    AwayTeam = m.CountryAway.CountryName,
                    Score = (m.HomeGoals != null && m.AwayGoals != null) ? (m.HomeGoals + "-" + m.AwayGoals) : null,
                    League = m.League != null ? m.League.LeagueName : null
                });

            var xmlDoc = new XDocument();
            var xmlRoot = new XElement("matches");
            xmlDoc.Add(xmlRoot);
            foreach (var match in internationalMatches)
            {
                var matchXmlElement = new XElement("match");

                if(match.MatchDate != null)
                {
                    DateTime matchDate = DateTime.Parse(match.MatchDate.ToString());
                    if (matchDate.Hour == 0)
                    {
                        string formattedDate = matchDate.ToString("dd-MMM-yyyy");
                        var matchDateAttribute = new XAttribute("date", formattedDate);
                        matchXmlElement.Add(matchDateAttribute);
                    }
                    else
                    {
                        string formattedDate = matchDate.ToString("dd-MMM-yyyy hh:mm");
                        var matchDateAttribute = new XAttribute("date-time", formattedDate);
                        matchXmlElement.Add(matchDateAttribute);
                    }                   
                }

                var homeCountryXmlElement = new XElement("home-country", match.HomeTeam);
                var homeCountryXmlElementAttirbute = new XAttribute("code", match.HomeCountryCode);
                homeCountryXmlElement.Add(homeCountryXmlElementAttirbute);
                matchXmlElement.Add(homeCountryXmlElement);

                var awayCountryXmlElement = new XElement("away-country", match.AwayTeam);
                var awayCountryXmlElementAttirbute = new XAttribute("code", match.AwayCountryCode);
                awayCountryXmlElement.Add(awayCountryXmlElementAttirbute);
                matchXmlElement.Add(awayCountryXmlElement);

                if(match.League != null)
                {
                    var leagueXmlElement = new XElement("league", match.League);
                    matchXmlElement.Add(leagueXmlElement);
                }

                if (match.Score != null)
                {
                    var scoreXmlElement = new XElement("score", match.Score);
                    matchXmlElement.Add(scoreXmlElement);
                }
                xmlRoot.Add(matchXmlElement);
            }

            xmlDoc.Save("international-matches.xml");
        }
    }
}
