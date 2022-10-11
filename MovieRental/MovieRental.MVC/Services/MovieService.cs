using MovieRental.MVC.Models;
using System.Collections.Generic;
using System.Linq;

namespace MovieRental.MVC.Services
{
    public class MovieService : IMovieService
    {
        private readonly MovieRentalContext _dbContext;
        public MovieService(MovieRentalContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddMovie(Movie movie)
        {
            _dbContext.Movies.Add(movie);
            _dbContext.SaveChanges();
        }

        public void DeleteMovie(int id)
        {
            var movie = _dbContext.Movies.Find(id);
            _dbContext.Movies.Remove(movie);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Movie> GetAllFantasyMovies()
        {
            var fantasy = _dbContext.Movies
                .Where(f => f.Type == "Fantasy")
                .ToList();

            return fantasy;
        }

        public IEnumerable<Movie> GetAllHorrorMovies()
        {
            var horrors = _dbContext.Movies
                .Where(m => m.Type == "Horror")
                .ToList();
            return horrors;

        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return from m in _dbContext.Movies
                   orderby m.Name
                   select m;
        }

        public Movie GetMovieById(int id)
        {
            return _dbContext.Movies.FirstOrDefault(m => m.MovieId == id);
        }

        public IEnumerable<Movie> GetMoviesByName(string MovieName)
        {
            var movie = from m in _dbContext.Movies
                        where m.Name.StartsWith(MovieName) || string.IsNullOrEmpty(MovieName)
                        select m;

            return movie;
        }

        public void UpdateMovie(Movie movie)
        {
            var entry = _dbContext.Entry(movie);
            entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
