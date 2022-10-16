using MovieRental.MVC.Models;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Threading.Tasks;

namespace MovieRental.MVC.Services
{
    public interface IMovieService
    {
        IEnumerable<Movie> Movies { get; }
        IEnumerable<Movie> GetAllMovies();
        IEnumerable<Movie> GetAllHorrorMovies();
        IEnumerable<Movie> GetAllFantasyMovies();
        IEnumerable<Movie> GetMoviesByName(string MovieName);
        Movie GetMovieById(int id);
        void AddMovie(Movie movie);
        void UpdateMovie(Movie movie);
        void DeleteMovie(int id);
    }
}
