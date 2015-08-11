using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_CodeFirst_Movies.Models
{
    public class Movie
    {
        private ICollection<User> users;
        private ICollection<Rating> ratings;
        public Movie()
        {
            this.ratings = new HashSet<Rating>();
            this.users = new HashSet<User>();
        }
        public int Id { get; set; }
        [Required]
        public string Isbn { get; set; }
        [Required]
        [MinLength(2)]
        [MaxLength(100)]
        public string Title { get; set; }
        [EnumDataType(typeof(AgeRestriction))]
        public AgeRestriction AgeRestriction { get; set; }
        public virtual ICollection<Rating> Ratings
        {
            get { return this.ratings; }
            set { this.ratings = value; }
        }
        public virtual ICollection<User> Users
        {
            get { return this.users; }
            set { this.users = value; }
        }
    }
}
