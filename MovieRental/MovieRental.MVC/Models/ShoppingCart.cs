using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;

namespace MovieRental.MVC.Models
{
    public class ShoppingCart
    {
        private readonly MovieRentalContext _dbContext;
        public ShoppingCart(MovieRentalContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            var context = services.GetService<MovieRentalContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddToCart(Movie movie, int amount)
        {
            var shoppingCartItem =
                    _dbContext.ShoppingCartItems.SingleOrDefault(s => s.Movie.MovieId == movie.MovieId && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
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

        public int RemoveFromCart(Movie movie)
        {
            var shoppingCartItem =_dbContext.ShoppingCartItems.SingleOrDefault(s => s.Movie.MovieId ==movie.MovieId && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _dbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }
            _dbContext.SaveChanges();
            return localAmount;
        }

        public List<ShoppingCartItem> GetShoppingCartItems()
        {
            return ShoppingCartItems ??(ShoppingCartItems =_dbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId).Include(s => s.Movie).ToList());
        }

        public void ClearCart()
        {
            var cartItems = _dbContext.ShoppingCartItems.Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _dbContext.ShoppingCartItems.RemoveRange(cartItems);

            _dbContext.SaveChanges();
        }

        public double GetShoppingCartTotal()
        {
            var total = _dbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId).Select(c => c.Movie.Price * c.Amount).Sum();
            return total;
        }
    }
}
