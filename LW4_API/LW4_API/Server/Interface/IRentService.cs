using LW4_API.Model.Domain;
using LW4_API.Model.DTO;
using LW4_API.Model.View;

namespace LW4_API.Server.Interface
{
    public interface IRentService
    {
        Task<List<RentView>> GetAllAsync();

        Task<RentView?> GetByIdAsync(int id);

        Task CreateAsync(RentDTO rentDto);

        Task UpdateAsync(int id, RentDTO rentDto);

        Task DeleteAsync(int id);
    }
}
