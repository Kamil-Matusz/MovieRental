using System.ComponentModel.DataAnnotations;

namespace MovieRental.MVC.Models.Cart
{
    public class ShoppingCartItem
    {
        [Key]
        public int ShoppingCartItemId { get; set; }
        public Movie Movie { get; set; }
        public int Amount { get; set; }

        public string ShoppingCartId { get; set; }

    }
}
