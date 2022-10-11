using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieRental.MVC.Models
{
    public class Client
    {
        [Key]
        [Required]
        public int ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        public List<Order> Orders { get; set; } = new List<Order>();
    }
}
