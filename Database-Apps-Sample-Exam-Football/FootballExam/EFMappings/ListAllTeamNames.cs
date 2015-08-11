namespace EFMappings
{
    using System;
    using System.Linq;

    class ListAllTeamNames
    {
        static void Main()
        {
            var context = new FootballEntities();

            var allTeamNames = context.Teams
                .Select(t => t.TeamName);

            foreach (var teamName in allTeamNames)
            {
                Console.WriteLine(teamName);
            }
        }
    }
}
