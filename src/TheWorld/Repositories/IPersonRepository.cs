using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;

namespace TheWorld.Repositories
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetPersons();
        IEnumerable<Person> GetPersonsByName(string name);
    }
}
