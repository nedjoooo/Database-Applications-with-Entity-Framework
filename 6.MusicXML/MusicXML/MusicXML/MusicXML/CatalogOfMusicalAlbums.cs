namespace MusicXML
{
    using System;
    using System.Xml;

    public static class CatalogOfMusicalAlbums
    {
        public static void GetAlbums(XmlDocument doc)
        {          
            doc.Load("../../catalog.xml");
            XmlNode rootNode = doc.DocumentElement;

            foreach (XmlNode node in rootNode.ChildNodes)
            {
                foreach (XmlNode album in node.ChildNodes)
                {
                    Console.WriteLine(album.Attributes["title"].Value);              
                }               
            }
        }
    }
}
