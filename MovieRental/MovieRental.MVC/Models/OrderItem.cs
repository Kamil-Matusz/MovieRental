using System.Collections.Generic;

namespace MovieRental.MVC.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
        public Movie Movie { get; set; }
        public int MovieId { get; set; }

       public Order Order { get; set; }
       public int OrderId { get; set; }

    }
}
