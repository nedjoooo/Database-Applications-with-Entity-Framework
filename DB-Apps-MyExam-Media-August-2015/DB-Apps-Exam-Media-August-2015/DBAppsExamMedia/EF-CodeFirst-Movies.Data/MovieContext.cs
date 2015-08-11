namespace EF_CodeFirst_Movies.Data
{
    using EF_CodeFirst_Movies.Data.Migrations;
    using EF_CodeFirst_Movies.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class MovieContext : DbContext
    {
        public MovieContext()
            : base("name=MovieContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MovieContext, Configuration>());
        }

        public virtual IDbSet<Country> Countries { get; set; }
        public virtual IDbSet<Movie> Movies { get; set; }
        public virtual IDbSet<Rating> Ratings { get; set; }
        public virtual IDbSet<User> Users { get; set; }
    }
}