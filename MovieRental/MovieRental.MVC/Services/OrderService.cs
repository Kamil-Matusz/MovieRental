using MovieRental.MVC.Models;
using System;

namespace MovieRental.MVC.Services
{
    public class OrderService : IOrderService
    {
        private readonly MovieRentalContext _dbContext;
        private readonly ShoppingCart _shoppingCart;

        public OrderService(MovieRentalContext dbContext, ShoppingCart shoppingCart)
        {
            _dbContext = dbContext;
            _shoppingCart = shoppingCart;
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;
            _dbContext.SaveChanges();
            var shoppingCartItems = _shoppingCart.ShoppingCartItems;

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    Amount = shoppingCartItem.Amount,
                    MovieId = shoppingCartItem.Movie.MovieId,
                    OrderId = order.OrderId,
                    Price = shoppingCartItem.Movie.Price
                };

                _dbContext.OrderDetails.Add(orderDetail);
            }

            _dbContext.SaveChanges();
        }
    }
}
