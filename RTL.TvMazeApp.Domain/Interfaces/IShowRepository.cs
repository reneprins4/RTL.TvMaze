using System.Threading.Tasks;
using RTL.TvMazeApp.Domain.Models;

namespace RTL.TvMazeApp.Domain.Interfaces
{
    public interface IShowRepository
    {
        Task<Show> GetShowAsync(int showId);
        Task<Show> GetLastShowAsync();
        Task CreateIfNotExistsAsync(Show show);
        Task SaveAsync();
    }
}
