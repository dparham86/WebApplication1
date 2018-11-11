using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [HttpPost]
        public IActionResult MainMenu(UserModel UserModel)
        {
            DataLayer dl = new DataLayer();
            //bool UserExists = false;
            if (dl.CheckForExistingUser(UserModel.UserName, UserModel.PassWord))
            {
                //UserModel.listOfFavorites = getAllFavorites();
                //UserModel.listOfMovies = getAllMovies();
                return View("MainMenu", UserModel);
            }
            else
            {
                UserModel.IsActive = "N";
                return View("Index", UserModel);
            }
                
        }

        [HttpPost]
        public IActionResult SaveToFavorites(Movie MovieModel)
        {
            DataLayer dl = new DataLayer();
            dl.SaveToFavorites(MovieModel.movieID);
            return View("Favorites",getAllFavorites());
        }

        [HttpPost]
        public IActionResult DeleteFromFavorites(Movie MovieModel)
        {
            DataLayer dl = new DataLayer();
            dl.DeleteFromFavorites(MovieModel.movieID);
            return View("Favorites", getAllFavorites());
        }

        public MovieList getAllMovies()
        {
            MovieList movieListModel = new MovieList();
            movieListModel.listOfMovies = new List<Movie>();
            movieListModel.listOfGenres = new List<GenreModel>();

            DataLayer dl = new DataLayer();
            DataTable dt = dl.getAllMovies();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Movie movieModel = new Movie();
                movieModel.movieID = int.Parse(dt.Rows[i]["movieID"].ToString());
                movieModel.movieName = dt.Rows[i]["movieName"].ToString();
                movieModel.movieGenreID = int.Parse(dt.Rows[i]["movieGenreID"].ToString());
                movieModel.MovieTrailerURL = dt.Rows[i]["MovieTrailerURL"].ToString();
                movieModel.movieImageName = dt.Rows[i]["movieImageName"].ToString();
                movieListModel.listOfMovies.Add(movieModel);
            }

            DataTable dt2 = dl.getAutoPlaySetting();
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                movieListModel.autoPlay = dt2.Rows[i]["settingActive"].ToString();
            }

            DataTable dt3 = dl.getGenreNames();
            for (int i = 0; i < dt3.Rows.Count; i++)
            {
                GenreModel genreModel = new GenreModel();
                genreModel.genreName = dt3.Rows[i]["genreName"].ToString();
                genreModel.movieGenreID = int.Parse(dt3.Rows[i]["genreID"].ToString());
                movieListModel.listOfGenres.Add(genreModel);
            }

            return movieListModel;
        }

        public FavoritesList getAllFavorites()
        {
            FavoritesList listOfFavorites = new FavoritesList();
            listOfFavorites.listOfMovies = new List<Movie>();
            DataLayer dl = new DataLayer();
            DataTable dt = dl.getAllFavorites();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Movie movieModel = new Movie();
                movieModel.movieID = int.Parse(dt.Rows[i]["movieID"].ToString());
                movieModel.movieName = dt.Rows[i]["movieName"].ToString();
                movieModel.movieGenreID = int.Parse(dt.Rows[i]["movieGenreID"].ToString());
                movieModel.MovieTrailerURL = dt.Rows[i]["MovieTrailerURL"].ToString();
                movieModel.movieImageName = dt.Rows[i]["movieImageName"].ToString();

                //movieModel.genreName = dt.Rows[i]["genreName"].ToString();
                //paymentMethodList.Add(newItem);
                listOfFavorites.listOfMovies.Add(movieModel);
            }

            DataTable dt2 = dl.getAutoPlaySetting();
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                //Movie movieModel = new Movie();
                //paymentMethodList.Add(newItem);

                //movieListModel.listOfMovies.Add(movieModel);
                listOfFavorites.autoPlay = dt2.Rows[i]["settingActive"].ToString();


            }

            return listOfFavorites;
        }


        [HttpPost]
        public IActionResult DeactivateMember(UserModel UserModel)
        {
            DataLayer dl = new DataLayer();
            dl.DeactivateMember(UserModel);
            UserModel = null; 
            return View("Index", UserModel);           
        }

        public IActionResult SignUp()
        {
            return View("SignUp", GetPaymentMethods());
        }

        [HttpPost]
        public IActionResult CreateUser(UserModel UserModel)
        {
            DataLayer dl = new DataLayer();
            if(ModelState.IsValid)
            {
                UserModel.IsActive = "Y";
                dl.CreateNewUser(UserModel);
                UserModel = null;
                return View("Index", UserModel);
            }
            else
            {
                UserModel.IsActive = "N";
                UserModel = null;
                return View("Index", UserModel);
            }

        
        }

        [HttpPost]
        public IActionResult SaveSettings(SettingsListModel SettingsModel)
        {
            DataLayer dl = new DataLayer();
            dl.SaveSettings(SettingsModel);
            SettingsModel.errorFree = "no error";
            return View("Settings", SettingsModel);

        }

        public UserModel GetPaymentMethods()
        {
            DataLayer dl = new DataLayer();
            UserModel UserModel = new UserModel();
            List<SelectListItem> paymentMethodList = new List<SelectListItem>();
            DataTable dt = dl.GetPaymentMethods();
            for(int i = 0; i < dt.Rows.Count; i++)
            {
                if (i == 0)
                {
                    SelectListItem newItem = new SelectListItem();
                    newItem.Text = "--- Choose a payment method---";
                    newItem.Value = "0";
                    paymentMethodList.Add(newItem);
                }
                else;
                {
                    SelectListItem newItem = new SelectListItem();
                    newItem.Text = dt.Rows[i]["paymentName"].ToString();
                    newItem.Value = dt.Rows[i]["paymentID"].ToString();
                    paymentMethodList.Add(newItem);
                }

            }
            UserModel.PayMethodList = paymentMethodList;
            return UserModel;
        }
        
        public IActionResult Settings()
        {
            DataLayer dl = new DataLayer();
            DataTable st = dl.getAllSettings();
            SettingsListModel SettingsListModel = new SettingsListModel();
            List<SettingsModel> listOfSettingsModels = new List<SettingsModel>();

            DataTable dt = dl.getAllSettings();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SettingsModel SettingsModel = new SettingsModel();
                SettingsModel.settingID = int.Parse(dt.Rows[i]["settingID"].ToString());
                SettingsModel.settingText = dt.Rows[i]["settingText"].ToString();
                if(dt.Rows[i]["settingActive"].ToString() == "N")
                {
                    SettingsModel.settingsSelected = false;
                }
                else
                {
                    SettingsModel.settingsSelected = true;
                }

                listOfSettingsModels.Add(SettingsModel);
                SettingsListModel.listOfSettings = listOfSettingsModels;
            }
            return View("Settings", SettingsListModel);
        }

        public IActionResult AccountManagement(UserModel UserModel)
        {
            return View("AccountManagement", UserModel);
        }

        public IActionResult LogOut()
        {
            return View("LogOut");
        }

        public IActionResult Browse()
        {
            DataLayer dl = new DataLayer();
            MovieList MovieListModel = new MovieList();
            MovieListModel = getAllMovies();
            //UserModel.listOfMovies = getAllMovies();
            return View("Browse", MovieListModel);

        }

        public IActionResult GoToGenre(string genre)
        {
            return PartialView(genre);
        }

        public IActionResult Selections()
        {
            return View("Selections");
        }
        public IActionResult Favorites()
        {
            DataLayer dl = new DataLayer();
            FavoritesList MovieListModel = new FavoritesList();
            MovieListModel = getAllFavorites ();
            return View("Favorites", MovieListModel);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
