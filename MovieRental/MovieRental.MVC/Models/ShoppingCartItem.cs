using System.IO.Pipelines;

namespace MovieRental.MVC.Models
{
    public class ShoppingCartItem
    {
        public int ShoppingCartItemId { get; set; }
        public Movie Movie { get; set; }
         public int MovieId { get; set; }
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; }
    }
}
