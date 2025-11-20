using LW4_API.Model.Entity;

namespace LW4_API.Repository.Interface
{
    public interface IParkingSpaceRepository
    {
        Task CreateAsync(ParkingSpace space);
        Task<List<ParkingSpace>> GetAllAsync();
        Task<ParkingSpace?> GetByIdAsync(int id);
        Task UpdateAsync(ParkingSpace space);
        Task DeleteAsync(int id);
    }
}
