using MovieRental.MVC.Models;
using MovieRental.MVC.Models.Cart;
using System.Collections.Generic;

namespace MovieRental.MVC.Services
{
    public interface IShoppingCartService
    {
        List<ShoppingCartItem> GetShoppingCartItems();
        void AddMovieToCart(Movie movie);
        void RemoveMovieFromCart(Movie movie);
    }
}
