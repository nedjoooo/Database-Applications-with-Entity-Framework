using Db_Mapping;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Export_Characters_and_Players_as_JSON
{
    class ExportCharacterAndPlayersAsJson
    {
        static void Main(string[] args)
        {
            var context = new DiabloEntities();

            var charactersAndPlayers = context.Characters
                .OrderBy(c => c.Name)
                .Select(c => new
                {
                    name = c.Name,
                    playedBy = c.UsersGames.Select(ug => ug.User.Username)
                });

            var json = new JavaScriptSerializer().Serialize(charactersAndPlayers);

            File.WriteAllText(@"characters.json", json);
        }
    }
}
