namespace MovieRental.MVC.Models
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public Movie Movie { get; set; }
        public int MovieId { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public int Amount { get; set; }
        public double Price { get; set; }
    }
}
