using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Browser
    {
        public ComicList listOfComics { get; set; }

        public MovieList listOfMovies { get; set; }

        public string autoPlay { get; set; }

        public string browserContentSelection { get; set; }
    }
}
