using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieRental.MVC.Models
{
    public class Order
    {

        [Key]
        [Required]
        public int OrderId { get; set; }
        public string Email { get; set; }

        public List<OrderItem> OrderItems { get; set; }
        /*public Movie Movie { get; set; }
        public int MovieId { get; set; }
        public Client Client { get; set; }
        public int ClientId { get; set; } */
    }
}
