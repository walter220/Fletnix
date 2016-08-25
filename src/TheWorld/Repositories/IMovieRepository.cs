using System.Collections.Generic;

namespace TheWorld.Models.Repositories
{
    public interface IMovieRepository
    {
        // Essential actions
        IEnumerable<Movie> GetAllMovies(int pageNumber, string search = "", int pageSize = 10);
        Movie GetMovie(int id);
        void CreateMovie(Movie m);
        Movie EditMovie(int id);
        void UpdateMovie(Movie m);
        void DeleteMovie(Movie m);
        void DeleteMovie(int id);

        // Addiontional actions
        IEnumerable<Movie> GetPopularMovies();
        int GetNewMovieId();
        int GetTotalMovies(string search);
    }
}