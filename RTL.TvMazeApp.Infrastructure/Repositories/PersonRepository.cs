using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RTL.TvMazeApp.Domain.Interfaces;
using RTL.TvMazeApp.Domain.Models;
using RTL.TvMazeApp.Infrastructure.Contexts;

namespace RTL.TvMazeApp.Infrastructure.Repositories
{
    public sealed class PersonRepository : IPersonRepository
    {
        private readonly TvMazeContext _context;

        public PersonRepository(TvMazeContext context) => _context = context;

        public async Task CreateIfNotExistsAsync(Person person)
        {
            if (person == null)
                throw new ArgumentNullException($"{nameof(person)}");

            var exists = _context.Persons.AsNoTracking().Any(c => c.Id == person.Id);
            if (!exists) await _context.Persons.AddAsync(person);
        }

        public Task SaveAsync() => _context.SaveChangesAsync();
    }
}
