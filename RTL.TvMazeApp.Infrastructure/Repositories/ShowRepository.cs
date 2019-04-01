using RTL.TvMazeApp.Domain.Interfaces;
using RTL.TvMazeApp.Domain.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RTL.TvMazeApp.Infrastructure.Contexts;

namespace RTL.TvMazeApp.Infrastructure.Repositories
{
    public sealed class ShowRepository : IShowRepository
    {
        private readonly TvMazeContext _context;

        public ShowRepository(TvMazeContext context) => _context = context;

        public async Task CreateIfNotExistsAsync(Show show)
        {
            if (show == null)
                throw new ArgumentNullException($"{nameof(show)}");

            var exists = await _context.Shows.Where(s => s.ShowId == show.ShowId).AsNoTracking().FirstOrDefaultAsync();
            if (exists == null)
            {
                _context.Entry(show).State = EntityState.Detached;
                await _context.Shows.AddAsync(show);
            }
        }

        public async Task<Show> GetLastShowAsync() => await _context.Shows.AsNoTracking().OrderByDescending(s => s.ShowId).FirstOrDefaultAsync();

        public async Task<Show> GetShowAsync(int showId)
        {
            var show = await _context.Shows.AsNoTracking().FirstOrDefaultAsync(s => s.ShowId == showId);
            if (show == null) throw new ArgumentNullException(nameof(show));

            var cast = await _context.Persons.AsNoTracking().Where(p => p.ShowId == show.ShowId).ToListAsync();
            show.Cast = cast;

            return show;
        }

        public async Task SaveAsync() => await _context.SaveChangesAsync();
    }
}
