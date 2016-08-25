using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using TheWorld.Models;
using TheWorld.Models.Repositories;

namespace TheWorld.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private IMemoryCache _cache;
        private FletnixContext _context;
        private ILogger<PersonRepository> _logger;

        public PersonRepository(FletnixContext context, ILogger<PersonRepository> logger, IMemoryCache cache)
        {
            _context = context;
            _logger = logger;
            _cache = cache;
        }

        public IEnumerable<Person> GetPersons()
        {
            return _context.Person.Take(100).ToList();
        }

        public IEnumerable<Person> GetPersonsByName(string name)
        {
            return _context.Person
                .Where(p => p.firstname.Contains(name) || p.lastname.Contains(name))
                .Take(100).ToList();
        }
    }
}
