using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_CodeFirst_Movies.Data
{
    public class RatingDTO
    {
        public string User { get; set; }
        public string Movie { get; set; }
        public int Rating { get; set; }
    }
}
