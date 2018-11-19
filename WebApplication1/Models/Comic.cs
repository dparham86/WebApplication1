using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Comic
    {
        public int comicID { get; set; }

        public string comicName { get; set; }

        public int comicGenreID { get; set; }

        public string comicImageName { get; set; }
    }
}
