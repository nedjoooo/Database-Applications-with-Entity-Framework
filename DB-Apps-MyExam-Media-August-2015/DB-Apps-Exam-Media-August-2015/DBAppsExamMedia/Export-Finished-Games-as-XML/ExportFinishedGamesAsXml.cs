using Db_Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Export_Finished_Games_as_XML
{
    class ExportFinishedGamesAsXml
    {
        static void Main(string[] args)
        {
            var context = new DiabloEntities();

            var finishedGames = context.Games
                .OrderBy(g => g.Name)
                .ThenBy(g => g.Duration)
                .Where(g => g.IsFinished == true)
                .Select(g => new
                {
                    Name = g.Name,
                    Duration = g.Duration != null ? g.Duration : null,
                    Users = g.UsersGames.Select(ug => new
                    {
                        Username = ug.User.Username,
                        IpAddress = ug.User.IpAddress
                    })
                });

            var xmlDoc = new XDocument();
            var xmlRoot = new XElement("games");
            xmlDoc.Add(xmlRoot);

            foreach (var game in finishedGames)
            {
                var gamesXmlElement = new XElement("game");
                var gameXmlaNameAttribute = new XAttribute("name", game.Name);
                gamesXmlElement.Add(gameXmlaNameAttribute);
                if(game.Duration != null)
                {
                    var gameXmlDurationAtrribute = new XAttribute("duration", game.Duration);
                    gamesXmlElement.Add(gameXmlDurationAtrribute);
                }

                var usersXmlElement = new XElement("users");
                foreach (var user in game.Users)
                {
                    var userXmlElement = new XElement("user");
                    var userXmlUsernameAttribute = new XAttribute("username", user.Username);
                    var userXmlIpAdressAttribute = new XAttribute("ip-address", user.IpAddress);
                    userXmlElement.Add(userXmlUsernameAttribute);
                    userXmlElement.Add(userXmlIpAdressAttribute);
                    usersXmlElement.Add(userXmlElement);
                }
                gamesXmlElement.Add(usersXmlElement);
                xmlRoot.Add(gamesXmlElement);
            }

            xmlDoc.Save("finished-games.xml");
        }
    }
}
