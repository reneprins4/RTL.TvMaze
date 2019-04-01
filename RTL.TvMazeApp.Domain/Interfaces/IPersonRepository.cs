using System.Threading.Tasks;
using RTL.TvMazeApp.Domain.Models;

namespace RTL.TvMazeApp.Domain.Interfaces
{
    public interface IPersonRepository
    {
        Task CreateIfNotExistsAsync(Person person);
        Task SaveAsync();
    }
}
