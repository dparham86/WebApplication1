using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class MovieList
    {
        public List<Movie> listOfMovies { get; set; }

        public string autoPlay { get; set; }

        public List<GenreModel> listOfGenres { get; set; }
    }
}
