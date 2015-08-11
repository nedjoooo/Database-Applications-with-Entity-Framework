using EF_CodeFirst_Movies.Data;
using EF_CodeFirst_Movies.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Top_Ten_Favourite_Movies
{
    class TopTenFavoriteMovies
    {
        static void Main(string[] args)
        {
            var context = new MovieContext();

            var topTenFavoriteTeenMovies = context.Movies
                .Where(m => m.AgeRestriction == AgeRestriction.Teen)
                .OrderByDescending(m => m.Users.Count)
                .ThenBy(m => m.Title)
                .Select(m => new
                {
                    isbn = m.Isbn,
                    title = m.Title,
                    favouritedBy = m.Users.Select(u => u.Username)
                });

            var json = new JavaScriptSerializer().Serialize(topTenFavoriteTeenMovies);

            File.WriteAllText(@"top-10-favourite-movies.json", json);
        }
    }
}
