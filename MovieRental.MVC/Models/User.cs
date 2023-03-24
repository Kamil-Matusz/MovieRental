using System.ComponentModel.DataAnnotations;

namespace MovieRental.MVC.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string? HashPassword { get; set; } = "";
        public string Role { get; set; } = "User";
    }
}
