using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db_Mapping
{
    class DbMapping
    {
        static void Main(string[] args)
        {
            var context = new DiabloEntities();

            var characterNames = context.Characters
                .Select(c => c.Name);

            foreach (var characterName in characterNames)
            {
                Console.WriteLine(characterName);
            }
        }
    }
}
