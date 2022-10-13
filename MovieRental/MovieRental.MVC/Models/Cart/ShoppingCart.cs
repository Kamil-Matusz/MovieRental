using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MovieRental.MVC.Models.Cart
{
    public class ShoppingCart
    {
        public MovieRentalContext _dbContext { get; set; }
        public ShoppingCart(MovieRentalContext context)
        {
            _dbContext = context;
        }
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems?? (ShoppingCartItems = _dbContext.ShoppingCartItems.Where( x=> x.ShoppingCartId == ShoppingCartId ).Include(x => x.Movie).ToList()); 
        }

        public double GetShoppingCartTotal()
        {
            var total = _dbContext.ShoppingCartItems.Where(x => x.ShoppingCartId == ShoppingCartId).Select(n => n.Movie.Price * n.Amount).Sum();
            return total;
        }

        public void AddMovieToCart(Movie movie)
        {
            var shoppingCartItem = _dbContext.ShoppingCartItems.FirstOrDefault(x => x.Movie.MovieId == movie.MovieId && x.ShoppingCartId == ShoppingCartId);

            if(shoppingCartItem == null)
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
    }
}
