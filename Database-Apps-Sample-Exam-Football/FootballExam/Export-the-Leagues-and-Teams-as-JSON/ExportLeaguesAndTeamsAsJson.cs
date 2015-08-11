namespace Export_the_Leagues_and_Teams_as_JSON
{
    using System.Linq;
    using EFMappings;
    using System.Web.Script.Serialization;
    using System.IO;

    class ExportLeaguesAndTeamsAsJson
    {
        static void Main()
        {
            var context = new FootballEntities();

            var leaguesAndTeams = context.Leagues
                .OrderBy(l => l.LeagueName)
                .Select(l => new
                {
                    LeaugeName = l.LeagueName,
                    LeagueTeams = l.Teams.OrderBy(t => t.TeamName).Select(t => t.TeamName)
                });

            var json = new JavaScriptSerializer().Serialize(leaguesAndTeams);

            File.WriteAllText(@"leagues-and-teams.json", json);
        }
    }
}
