using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string PassWord { get; set; }

        public string IsActive { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string CCNumber { get; set; }
        [Required]
        public int PaymentMethod { get; set; }

        public List<SelectListItem> PayMethodList {get; set;}

        public MovieList listOfMovies { get; set; }

        public FavoritesList listOfFavorites { get; set; }
        
    }
}
