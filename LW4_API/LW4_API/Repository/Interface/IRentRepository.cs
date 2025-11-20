using LW4_API.Model.Entity;

namespace LW4_API.Repository.Interface
{
    public interface IRentRepository
    {
        Task CreateAsync(Rent rent);
        Task<List<Rent>> GetAllAsync();
        Task<Rent?> GetByIdAsync(int id);
        Task UpdateAsync(Rent rent);
        Task DeleteAsync(int id);
    }
}
