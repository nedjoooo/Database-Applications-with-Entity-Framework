namespace _5.EF_CodeFirst_Movies.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class MovieContext : DbContext
    {
        public MovieContext()
            : base("name=MovieContext")
        {
        }

        public virtual IDbSet<Country> Countries { get; set; }
    }
}