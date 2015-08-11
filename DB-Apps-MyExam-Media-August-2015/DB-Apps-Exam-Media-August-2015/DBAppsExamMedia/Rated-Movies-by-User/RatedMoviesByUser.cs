using EF_CodeFirst_Movies.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Rated_Movies_by_User
{
    class RatedMoviesByUser
    {
        static void Main(string[] args)
        {
            var context = new MovieContext();

            var ratedMoviesByUser = context.Users
                .Where(u => u.Username == "jmeyery")
                .Select(u => new
                {
                    username = u.Username,
                    ratedMovies = u.Ratings.Where(r => r.UserId == u.Id)
                        .Select(r => new
                        {
                            title = r.Movie.Title,
                            userRating = r.Stars,
                            averageRating = r.Movie.Ratings.Select(mr => mr.Stars).Average()
                        })
                        .OrderBy(m => m.title)
                });

            var json = new JavaScriptSerializer().Serialize(ratedMoviesByUser);

            File.WriteAllText(@"rated-movies-by-jmeyery.json", json);
        }
    }
}
