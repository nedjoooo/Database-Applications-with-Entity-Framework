using EF_CodeFirst_Movies.Data;
using EF_CodeFirst_Movies.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace _6._1.Adult_Movies
{
    class AdultMovies
    {
        static void Main(string[] args)
        {
            
            var context = new MovieContext();

            var adultMovies = context.Movies
                .OrderBy(m => m.Title)
                .ThenBy(m => m.Ratings.Count)
                .Where(m => m.AgeRestriction == AgeRestriction.Adult)
                .Select(m => new
                {
                    title = m.Title,
                    ratingsGiven = m.Ratings.Count
                });


            var json = new JavaScriptSerializer().Serialize(adultMovies);

            File.WriteAllText(@"adult-movies.json", json);
        }
    }
}
