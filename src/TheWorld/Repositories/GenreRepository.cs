using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace TheWorld.Models.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly FletnixContext _context;
        private readonly ILogger<GenreRepository> _logger;

        private IMemoryCache _cache;
        private string _cacheKey = "GenreTestCache";
        private IEnumerable<Genre> _cacheGenre;

        public GenreRepository(FletnixContext context, ILogger<GenreRepository> logger, IMemoryCache cache)
        {
            _context = context;
            _logger = logger;
            _cache = cache;
        }

        public IEnumerable<Genre> GetAll()
        {
            IEnumerable<Genre> cacheGenre;

            if (!_cache.TryGetValue(_cacheKey, out _cacheGenre))
            {
                try
                {
                    cacheGenre = _context.Genre.OrderBy(g => g.genre_name).ToList();
                }
                catch (Exception ex)
                {
                    _logger.LogError("Could not get stuff from DB", ex);
                    return null;
                }
                _cache.Set(_cacheKey, cacheGenre,
                    new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(1))
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(10)));
            }
            else
            {
                cacheGenre = _cacheGenre;
            }

            return cacheGenre;
        }

        public Genre GetByGenreName(string genre)
        {
            try
            {
                return _context.Genre.First(g => g.genre_name == genre);
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get stuff from DB", ex);
                return null;
            }
        }

        public void CreateGenre(Genre g)
        {
            _context.Genre.Add(g);
            _context.SaveChanges();
        }

        public void DeleteGenre(string id)
        {
            _context.Genre.Remove(GetByGenreName(id));
            _context.SaveChanges();
        }
    }
}
