namespace Import_Leagues_and_Teams_from_XML
{
    using EFMappings;
    using System.Xml.Linq;
    using System.Linq;
    using System;

    class ImportLeaguesAndTeamsFromXml
    {
        static void Main(string[] args)
        {
            var context = new FootballEntities();

            XDocument xmlDoc = XDocument.Load("../../leagues-and-teams.xml");

            var leaguesAndTeams =
                from league in xmlDoc.Descendants("league")
                select new
                {
                    LeagueName = league.Element("league-name") != null ? league.Element("league-name").Value : null,
                    Teams = league.Element("teams") != null ? league.Element("teams").Elements() : null
                };

            int countXmlLeagues = 0;

            foreach (var league in leaguesAndTeams)
            {
                League leageToDb = null;
                Console.WriteLine("Processing league #{0}", ++countXmlLeagues);
                if(league.LeagueName != null)
                {
                    leageToDb = context.Leagues.FirstOrDefault(l => l.LeagueName == league.LeagueName);
                    if (leageToDb != null)
                    {
                        Console.WriteLine("Existing league: {0}", league.LeagueName);
                    }
                    else
                    {
                        leageToDb = new League() { LeagueName = league.LeagueName };
                        context.Leagues.Add(leageToDb);
                        Console.WriteLine("Created league: {0}", league.LeagueName);
                    }
                }

                if(league.Teams != null)
                {
                    foreach (var team in league.Teams)
                    {                      
                        string teamName = team.Attribute("name").Value;
                        string countryName = "";
                        if (team.Attribute("country") != null)
                        {
                            countryName = team.Attribute("country").Value;
                        }

                        Team teamToDb = context.Teams.FirstOrDefault(t => t.TeamName == teamName && t.Country.CountryName == countryName);
                        if(teamToDb == null)
                        {
                            teamToDb = context.Teams.FirstOrDefault(t => t.TeamName == teamName);
                        }

                        if(teamToDb != null)
                        {
                            Console.WriteLine("Existing team: {0} ({1})",
                                teamName, !string.IsNullOrEmpty(countryName) ? countryName : "no country");
                        }
                        else
                        {
                            Country country = context.Countries.FirstOrDefault(c => c.CountryName == countryName);
                            if(!string.IsNullOrEmpty(countryName))
                            {
                                if(country == null)
                                {
                                    country = new Country()
                                    {
                                        CountryName = countryName
                                    };
                                }                              
                            }
                            teamToDb = new Team()
                                {
                                    TeamName = teamName,
                                    Country = country
                                };
                            context.Teams.Add(teamToDb);

                            Console.WriteLine("Created team: {0} ({1})",
                                teamName, !string.IsNullOrEmpty(countryName) ? countryName : "no country");
                        }

                        if(leageToDb != null)
                        {
                            if(!leageToDb.Teams.Contains(teamToDb))
                            {
                                Console.WriteLine("Existing team in league: {0} belongs to {1}",
                                    teamToDb.TeamName, leageToDb.LeagueName);
                            }
                            else
                            {
                                leageToDb.Teams.Add(teamToDb);
                                Console.WriteLine("Added team to league: {0} to league {1}",
                                    teamToDb.TeamName, leageToDb.LeagueName);
                            }                           
                        }
                    }
                }
                
                
                Console.WriteLine();
                context.SaveChanges();
            }
        }
    }
}
