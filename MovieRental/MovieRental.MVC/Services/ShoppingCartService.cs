using MovieRental.MVC.Models.Cart;
using MovieRental.MVC.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MovieRental.MVC.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        public MovieRentalContext _dbContext { get; set; }
        public ShoppingCartService(MovieRentalContext context)
        {
            _dbContext = context;
        }
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ?? (ShoppingCartItems = _dbContext.ShoppingCartItems.Where(x => x.ShoppingCartId == ShoppingCartId).Include(x => x.Movie).ToList());
        }

        public double GetShoppingCartTotal()
        {
            var total = _dbContext.ShoppingCartItems.Where(x => x.ShoppingCartId == ShoppingCartId).Select(n => n.Movie.Price * n.Amount).Sum();
            return total;
        }

        public void AddMovieToCart(Movie movie)
        {
            var shoppingCartItem = _dbContext.ShoppingCartItems.FirstOrDefault(x => x.Movie.MovieId == movie.MovieId && x.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    ShoppingCartId = ShoppingCartId,
                    Movie = movie,
                    Amount = 1
                };

                _dbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }

            _dbContext.SaveChanges();
        }

        public void RemoveMovieFromCart(Movie movie)
        {
            var shoppingCartItem = _dbContext.ShoppingCartItems.FirstOrDefault(n => n.Movie.MovieId == movie.MovieId && n.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    _dbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            _dbContext.SaveChanges();
        }
    }
}
