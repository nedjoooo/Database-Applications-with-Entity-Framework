namespace MusicXML
{
    using System;
    using System.Collections.Generic;
    using System.Xml;

    public class ExtractArtists
    {
        public static void ExtractArtistsAlphabetically(XmlDocument doc)
        {
            doc.Load("../../catalog.xml");
            XmlNode rootNode = doc.DocumentElement;

            var artists = new SortedSet<string>();

            foreach (XmlNode node in rootNode.ChildNodes)
            {
                artists.Add(node.Attributes["name"].Value);
            }

            foreach (var artist in artists)
	        {
                Console.WriteLine(artist);
	        }           
        }

        public static void ExtractArtistsAndNumberOfTheirAlbumsWithDomParser(XmlDocument doc)
        {
            doc.Load("../../catalog.xml");
            XmlNode rootNode = doc.DocumentElement;

            var artists = new Dictionary<string, int>();

            foreach (XmlNode node in rootNode.ChildNodes)
            {
                string currentArtist = node.Attributes["name"].Value;
                int numberOfAlbums = node.ChildNodes.Count;

                if (artists.ContainsKey(currentArtist))
                {
                    artists[currentArtist] += numberOfAlbums;
                }
                else
                {
                    artists.Add(currentArtist, numberOfAlbums);
                }
            }

            foreach (var artist in artists)
            {
                Console.WriteLine("Artist: {0}, Number of albums: {1}", artist.Key, artist.Value);
            }
        }

        public static void ExtractArtistsAndNumberOfTheirAlbumsWithXPath(XmlDocument doc)
        {
            doc.Load("../../catalog.xml");
            var nodes = doc.SelectNodes("/music/artist");

            var artists = new Dictionary<string, int>();

            foreach (XmlNode node in nodes)
            {
                string currentArtist = node.Attributes["name"].Value;
                int numberOfAlbums = node.ChildNodes.Count;

                if(artists.ContainsKey(currentArtist))
                {
                    artists[currentArtist] += numberOfAlbums;
                }
                else
                {
                    artists.Add(currentArtist, numberOfAlbums);
                }
            }

            foreach (var artist in artists)
            {
                Console.WriteLine("Artist: {0}, Number of albums: {1}", artist.Key, artist.Value);
            }    
        }
    }
}
