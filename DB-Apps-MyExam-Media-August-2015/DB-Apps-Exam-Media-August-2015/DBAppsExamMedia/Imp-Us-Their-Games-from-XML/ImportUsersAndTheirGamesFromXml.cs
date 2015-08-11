using Db_Mapping;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Imp_Us_Their_Games_from_XML
{
    class ImportUsersAndTheirGamesFromXml
    {
        static void Main(string[] args)
        {
            var context = new DiabloEntities();

            XDocument xmlDoc = XDocument.Load("../../../../users-and-games.xml");

            var usersElements = xmlDoc.Root.Elements();

            foreach (var userElement in usersElements)
            {
                var userEntity = new User();
                userEntity.Username = userElement.Attribute("username").Value;
                string registrationDate = userElement.Attribute("registration-date").Value;
                userEntity.RegistrationDate = DateTime.ParseExact(registrationDate, "dd/mm/yyyy", CultureInfo.InstalledUICulture);
                userEntity.IpAddress = userElement.Attribute("ip-address").Value;
                userEntity.IsDeleted = userElement.Attribute("is-deleted").Value == "1";
                if (userElement.Attribute("first-name") != null)
                {
                    userEntity.FirstName = userElement.Attribute("first-name").Value;
                }
                if (userElement.Attribute("last-name") != null)
                {
                    userEntity.FirstName = userElement.Attribute("last-name").Value;
                }
                if (userElement.Attribute("email") != null)
                {
                    userEntity.FirstName = userElement.Attribute("email").Value;
                }

                context.Users.Add(userEntity);
                Console.WriteLine("Successfully added user {0}", userEntity.Username);

                context.SaveChanges();
            }
        }
    }
}
