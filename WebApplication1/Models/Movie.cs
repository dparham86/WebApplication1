using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Movie
    {
        public int movieID { get; set; }

        public string movieName { get; set; }

        public int movieGenreID { get; set; }

        public string MovieTrailerURL { get; set; }

        public string movieImageName { get; set; }
    }
}
