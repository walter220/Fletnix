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
                        .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                        .SetAbsoluteExpiration(TimeSpan.FromMinutes(30)));
            }
            else
            {
                cacheGenre = _cacheGenre;
            }

            return cacheGenre;
        }
    }
}
