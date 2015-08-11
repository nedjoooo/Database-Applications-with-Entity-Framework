using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_CodeFirst_Movies.Data
{
    public class FavoriteMoviesDTO
    {
        public string Username { get; set; }
        public string[] FavouriteMovies { get; set; }
    }
}
