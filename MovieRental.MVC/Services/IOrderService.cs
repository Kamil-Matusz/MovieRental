using MovieRental.MVC.Models;

namespace MovieRental.MVC.Services
{
    public interface IOrderService
    {
        void CreateOrder(Order order);
    }
}
