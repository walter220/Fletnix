using System.Collections.Generic;

namespace TheWorld.Models.Repositories
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetMoviesOnPage(int pageNumber, string search = "", int pageSize = 20);
        IEnumerable<Movie> GetAll();
        IEnumerable<Movie> GetAllMoviesWithDirectors();
        Movie GetById(int id);
        IEnumerable<Movie> GetPopularMovies();
        IEnumerable<Movie> GetPopularMoviesNew();
        int GetTotalMovies();
    }
}