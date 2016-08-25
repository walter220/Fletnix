using System.Collections.Generic;

namespace TheWorld.Models.Repositories
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetAll();
        Genre GetByGenreName(string genre);
        void CreateGenre(Genre g);
        void DeleteGenre(string genre);
    }
}