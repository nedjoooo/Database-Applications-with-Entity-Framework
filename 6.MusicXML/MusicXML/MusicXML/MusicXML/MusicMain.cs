namespace MusicXML
{
    using System.Xml;

    class MusicMain
    {
        static void Main()
        {
            XmlDocument doc = new XmlDocument();

            //  CatalogOfMusicalAlbums.GetAlbums(doc);
            //  ExtractArtists.ExtractArtistsAlphabetically(doc);
            //  ExtractArtists.ExtractArtistsAndNumberOfTheirAlbumsWithDomParser(doc);
            //  ExtractArtists.ExtractArtistsAndNumberOfTheirAlbumsWithXPath(doc);
            //  DOMParser.DeleteAlbums(doc);
            //  DOMParser.OldAlbums(doc);
            DOMParser.OldAlbumsLINQ();
        }
    }
}
