using System.Collections.Generic;

namespace TheWorld.Models.Repositories
{
    public interface IGenreRepository
    {
        IEnumerable<Genre> GetAll();
    }
}