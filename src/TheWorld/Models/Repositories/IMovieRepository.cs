using System.Collections.Generic;

namespace TheWorld.Models.Repositories
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetAll();
        IEnumerable<Movie> GetAllMoviesWithDirectors();
        Movie GetById(int id);
        IEnumerable<Movie> GetPopularMovies();
        IEnumerable<Movie> GetPopularMoviesNew();
    }
}