using System.Collections.Generic;
using System.Threading.Tasks;
using RTL.TvMazeApp.Domain.Models;

namespace RTL.TvMazeApp.Domain.Interfaces
{
    public interface IScrapeService
    {
        Task<IEnumerable<Person>> GetPersonsAsync(int showId);
        Task<List<Show>> GetShowsAsync(int pageId);
    }
}
