using LW4_API.Model.Entity;
using LW4_API.Model.DTO;
namespace LW4_API.Server.Interface
{
    public interface IClientService
    {
        Task<List<ClientDTO>> GetAllAsync();
        Task<ClientDTO?> GetByIdAsync(int id);
        Task CreateAsync(ClientDTO clientDto);
        Task UpdateAsync(int id,ClientDTO clientDto);
        Task DeleteAsync(int id);
    }
}
