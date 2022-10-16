using Microsoft.AspNetCore.Mvc;
using MovieRental.MVC.Models;
using MovieRental.MVC.Services;
using MovieRental.MVC.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRental.MVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartController(IMovieService movieService, ShoppingCart shoppingCart)
        {
            _movieService = movieService;
            _shoppingCart = shoppingCart;
        }

        public IActionResult Index()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };

            return View(shoppingCartViewModel);
        }

         public IActionResult AddToShoppingCart(int movieId)
        {
             var selectedMovie = _movieService.Movies.FirstOrDefault(p => p.MovieId == movieId);

            if (selectedMovie != null)
            {
                _shoppingCart.AddToCart(selectedMovie, 1);
            }
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromShoppingCart(int movieId)
        {
            var selectedMovie = _movieService.Movies.FirstOrDefault(p => p.MovieId == movieId);

            if (selectedMovie != null)
            {
                _shoppingCart.RemoveFromCart(selectedMovie);
            }
            return RedirectToAction("Index");
        }
    }
}
