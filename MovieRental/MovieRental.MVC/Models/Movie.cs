using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MovieRental.MVC.Models
{
    public class Movie
    {
        [Key]
        [Required]
        public int MovieId { get; set; }
        [DisplayName("Nazwa")]
        public string Name { get; set; }
        [DisplayName("Typ filmu")]
        public string Type { get; set; }
        [DisplayName("Rok produkcji")]
        public int Production_year { get; set; }
        [DisplayName("Cena")]
        public double Price { get; set; }
        [DisplayName("Opis")]
        public string Description { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = new List<ShoppingCartItem>();
    }
}
