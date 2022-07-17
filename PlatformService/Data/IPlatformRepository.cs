using PlatformService.Models;

namespace PlatformService.Data
{
    public interface IPlatformRepository
    {
        Task SaveChangesAsync();
        Task<IEnumerable<Platform>> GetALlAsync();
        Task<Platform> GetByIdAsync(int id);
        Task CreateAsync(Platform platform);
    }
}
