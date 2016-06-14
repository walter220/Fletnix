using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;
using Microsoft.Data.Entity.Internal;
using Microsoft.Data.Entity.Query.Internal;
using Microsoft.Extensions.Caching.Memory;

namespace TheWorld.Models.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly FletnixContext _context;
        private readonly ILogger<MovieRepository> _logger;
        private readonly IMemoryCache _cache;
        private string _popularCacheKey = "popularMovies";

        public MovieRepository(FletnixContext context, ILogger<MovieRepository> logger, IMemoryCache cache)
        {
            _context = context;
            _logger = logger;
            _cache = cache;
        }
        
        public IEnumerable<Movie> GetAllMovies(int pageNumber, string search = "", int pageSize = 10)
        {
            try
            {
                return _context.Movie
                    .Where(m => m.title.Contains(search))
                    .Skip(pageNumber * pageSize - pageSize)
                    .Take(pageSize)
                    .OrderBy(m => m.movie_id)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get stuff from DB", ex);
                return null;
            }
        }

        public Movie GetMovie(int id)
        {
            try
            {
                return _context.Movie
                    .Include(m => m.Movie_Genre)
                    .Include(m => m.Movie_Director)
                    .ThenInclude(d => d.person)
                    .Include(m => m.Movie_Cast)
                    .ThenInclude(d => d.person)
                    .Include(m => m.CustomerFeedback)
                    .Include(m => m.MovieAwards)
                    .First(i => i.movie_id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get stuff from DB", ex);
                return null;
            }
        }

        public void CreateMovie(Movie m)
        {
            _context.Movie.Add(m);
            _context.SaveChanges();
        }

        public Movie EditMovie(int id)
        {
            return GetMovie(id);
        }

        public void UpdateMovie(Movie m)
        {
            _context.Movie.Update(m);
            _context.SaveChanges();
        }

        public void DeleteMovie(Movie m)
        {
            _context.Movie.Remove(m);
            _context.SaveChanges();
        }
        public void DeleteMovie(int id)
        {
            _context.Movie.Remove(GetMovie(id));
            _context.SaveChanges();
        }
        public int GetTotalMovies()
        {
            return _context.Movie.Count();
        }
        public IEnumerable<Movie> GetPopularMovies()
        {
            IEnumerable<Movie> movies;
            if (!_cache.TryGetValue(_popularCacheKey, out movies))
            {
                try
                {
                    movies = _context.Movie
                        .Join(
                            _context.Watchhistory
                                .GroupBy(m => m.movie_id)
                                .Select(w => new { total = w.Count(), wID = w.Key })
                                .OrderByDescending(o => o.total)
                                .Take(10)
                                .Select(a => new { movie_id = a.wID, total = a.total }),
                            m => m.movie_id, w => w.movie_id, (m, w) => m)
                        .ToList();

                    _cache.Set(_popularCacheKey, movies,
                        new MemoryCacheEntryOptions()
                            .SetSlidingExpiration(TimeSpan.FromMinutes(5))
                            .SetAbsoluteExpiration(TimeSpan.FromHours(1)));
                }
                catch (Exception ex)
                {
                    _logger.LogError("Could not get stuff from DB", ex);
                    return null;
                }
            }
            return movies;
        }
    }
}
