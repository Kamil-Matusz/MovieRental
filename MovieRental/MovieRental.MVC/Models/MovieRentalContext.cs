using Microsoft.EntityFrameworkCore;

namespace MovieRental.MVC.Models
{
    public class MovieRentalContext : DbContext
    {
        public MovieRentalContext(DbContextOptions<MovieRentalContext> options) : base(options)
        {

        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasData(new User()
                {
                    UserId = 1,
                    UserName = "admin",
                    Password = "admin",
                    Email = "admin@admin.com",
                    Role = "Admin",
                    HashPassword = "AQAAAAEAACcQAAAAEGn3IGsBmSOp78M8hKtd9kLvuvl3U4PjkuR7uOp7iC2U1qsZQhL34+OzILMC4r9NoA=="
                },
                new User()
                {
                    UserId = 2,
                    UserName = "user",
                    Password = "user",
                    Email = "user@user.com",
                    Role = "User",
                    HashPassword = "AQAAAAEAACcQAAAAEGn3IGsBmSOp78M8hKtd9kLvuvl3U4PjkuR7uOp7iC2U1qsZQhL34+OzILMC4r9NoA=="
                });
        }

    }
}
