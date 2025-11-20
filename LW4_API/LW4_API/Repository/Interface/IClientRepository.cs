using LW4_API.Model.Entity;
namespace LW4_API.Repository.Interface
{
    public interface IClientRepository
    {
        Task CreateAsync(Client client);
        Task<List<Client>> GetAllAsync();
        Task<Client?> GetByIdAsync(int id);
        Task UpdateAsync(Client  client);
        Task DeleteAsync(int id);

    }
}
