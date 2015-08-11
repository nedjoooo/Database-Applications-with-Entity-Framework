namespace EF_CodeFirst_Movies.Data.Migrations
{
    using EF_CodeFirst_Movies.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EF_CodeFirst_Movies.Data.MovieContext>
    {
        public Configuration()
        {
            AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = true;
            ContextKey = "EF_CodeFirst_Movies.Data.MovieContext";
        }

        protected override void Seed(EF_CodeFirst_Movies.Data.MovieContext context)
        {
            if(context.Countries.Count() == 0)
            {
                SeedCountries(context);
            }

            if (context.Users.Count() == 0)
            {
                SeedUsers(context);
            }

            if (context.Movies.Count() == 0)
            {
                SeedMovies(context);
            }

            if (context.Ratings.Count() == 0)
            {
                SeedMovieRatings(context);
            }

            if(context.Users.FirstOrDefault(u => u.Username == "pmoore0").Movies.Count() == 0)
            {
                List<FavoriteMoviesDTO> favoriteMovies = new List<FavoriteMoviesDTO>();
                using (StreamReader r = new StreamReader("../../../../users-and-favourite-movies.json"))
                {
                    string json = r.ReadToEnd();
                    favoriteMovies = JsonConvert.DeserializeObject<List<FavoriteMoviesDTO>>(json);
                }

                foreach (var userFavoriteMovie in favoriteMovies)
                {
                    var currentUser = context.Users.FirstOrDefault(u => u.Username == userFavoriteMovie.Username);
                    foreach (var favoriteMovie in userFavoriteMovie.FavouriteMovies)
                    {
                        var movie = context.Movies.FirstOrDefault(m => m.Isbn == favoriteMovie);
                        currentUser.Movies.Add(movie);
                    }
                }

                context.SaveChanges();
            }
        }

        private static void SeedMovieRatings(EF_CodeFirst_Movies.Data.MovieContext context)
        {
            List<RatingDTO> movieRatings = new List<RatingDTO>();
            using (StreamReader r = new StreamReader("../../../../movie-ratings.json"))
            {
                string json = r.ReadToEnd();
                movieRatings = JsonConvert.DeserializeObject<List<RatingDTO>>(json);
            }

            foreach (var rating in movieRatings)
            {
                var ratingDb = new Rating()
                {
                    User = context.Users.FirstOrDefault(u => u.Username == rating.User),
                    Movie = context.Movies.FirstOrDefault(m => m.Isbn == rating.Movie),
                    Stars = rating.Rating
                };

                context.Ratings.Add(ratingDb);
            }

            context.SaveChanges();
        }

        private static void SeedMovies(EF_CodeFirst_Movies.Data.MovieContext context)
        {
            List<Movie> movies = new List<Movie>();
            using (StreamReader r = new StreamReader("../../../../movies.json"))
            {
                string json = r.ReadToEnd();
                movies = JsonConvert.DeserializeObject<List<Movie>>(json);
            }

            foreach (var movie in movies)
            {
                var movieDb = new Movie()
                {
                    Isbn = movie.Isbn,
                    Title = movie.Title,
                    AgeRestriction = movie.AgeRestriction
                };

                context.Movies.Add(movieDb);
            }

            context.SaveChanges();
        }

        private static void SeedUsers(EF_CodeFirst_Movies.Data.MovieContext context)
        {
            List<UserDTO> users = new List<UserDTO>();
            using (StreamReader r = new StreamReader("../../../../users.json"))
            {
                string json = r.ReadToEnd();
                users = JsonConvert.DeserializeObject<List<UserDTO>>(json);
            }

            foreach (var user in users)
            {
                var userDb = new User()
                {
                    Username = user.Username
                };

                if (user.Email != null)
                {
                    userDb.Email = user.Email;
                }

                if (user.Age != null)
                {
                    userDb.Age = user.Age;
                }

                if (user.Country != null)
                {
                    var country = context.Countries.FirstOrDefault(c => c.Name == user.Country);
                    userDb.Country = country;
                }

                context.Users.Add(userDb);
            }

            context.SaveChanges();
        }

        private static void SeedCountries(EF_CodeFirst_Movies.Data.MovieContext context)
        {
            List<Country> countries = new List<Country>();
            using (StreamReader r = new StreamReader("../../../../countries.json"))
            {
                string json = r.ReadToEnd();
                countries = JsonConvert.DeserializeObject<List<Country>>(json);
            }

            foreach (var country in countries)
            {
                var countryDb = new Country()
                {
                    Name = country.Name
                };
                context.Countries.Add(countryDb);
            }

            context.SaveChanges();
        }
    }
}
