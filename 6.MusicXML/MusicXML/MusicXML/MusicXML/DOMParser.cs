namespace MusicXML
{
    using System;
    using System.Globalization;
    using System.Xml;
    using System.Xml.Linq;
    using System.Linq;

    public class DOMParser
    {
        public static void DeleteAlbums(XmlDocument doc)
        {
            doc.Load("../../catalog.xml");
            var artists = doc.SelectNodes("/music/artist");
            foreach (XmlNode artist in artists)
            {
                foreach (XmlNode album in artist.ChildNodes)
                {
                    if (Decimal.Parse(album.Attributes["price"].Value) > 20)
                    {
                        artist.RemoveChild(album);
                    }
                }
            }

            doc.Save("cheap-albums-catalog.xml");
        }

        public static void OldAlbums(XmlDocument doc)
        {
            doc.Load("../../catalog.xml");
            var albums = doc.SelectNodes("/music/artist/album");

            foreach (XmlNode album in albums)
            {
                DateTime albumDate = DateTime.ParseExact(album.Attributes["date"].Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                DateTime compareDate = DateTime.Now.AddYears(-5);
                if(albumDate <= compareDate)
                {
                    Console.WriteLine("{0}, {1}", album.Attributes["title"].Value, album.Attributes["date"].Value);
                }
            }
        }

        public static void OldAlbumsLINQ()
        {
            XDocument xmlDoc = XDocument.Load("../../catalog.xml");
            var compareDate = DateTime.Now.AddYears(-5);

            var oldAlbums =
                from album in xmlDoc.Descendants("album")
                where DateTime.Parse(album.Attribute("date").Value) <= compareDate
                select new
                {
                    Name = album.Attribute("title").Value,
                    Price = album.Attribute("price").Value,
                    Date = album.Attribute("date").Value
                };

            foreach (var album in oldAlbums)
            {
                Console.WriteLine("Album: {0}; Price: {1}; Date: {2}", album.Name, album.Price, album.Date);
            }
        }
    }
}
