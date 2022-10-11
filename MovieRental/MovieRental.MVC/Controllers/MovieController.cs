using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieRental.MVC.Models;
using MovieRental.MVC.Services;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace MovieRental.MVC.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            this._movieService = movieService;
        }

        [HttpGet]
        /* public IActionResult Index()
         {
             var movies = _movieService.GetAllMovies();
             return View(movies);
         } */

        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var movies = from m in _movieService.GetAllMovies()
                         select m;
            if (!String.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(m => m.Name.Contains(searchString));
            }
            return View(movies);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var movie = _movieService.GetMovieById(id);
            return View(movie);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(Movie movie)
        {
            if (String.IsNullOrEmpty(movie.Name))
            {
                ModelState.AddModelError(nameof(movie.Name), "The Name is required");
            }

            if (ModelState.IsValid)
            {
                _movieService.AddMovie(movie);
                return RedirectToAction("Details", new { id = movie.MovieId });
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var movie = _movieService.GetMovieById(id);
            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection form)
        {
            _movieService.DeleteMovie(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var movie = _movieService.GetMovieById(id);
            return View(movie);
        }

        [HttpGet]
        public IActionResult HorrorMovieList()
        {
            var movies = _movieService.GetAllHorrorMovies();
            return View(movies);
        }

        [HttpGet]
        public IActionResult FantasyMovieList()
        {
            var movies = _movieService.GetAllFantasyMovies();
            return View(movies);
        }
    }
}
