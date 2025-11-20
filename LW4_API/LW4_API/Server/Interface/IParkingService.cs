using LW4_API.Model.DTO;

namespace LW4_API.Server.Interface
{
    public interface IParkingService
    {
        Task<List<ParkingSpaceDTO>> GetAllAsync();
        Task<ParkingSpaceDTO?> GetByIdAsync(int id);

        Task CreateAsync(ParkingSpaceDTO spaceDto);
        Task UpdateAsync(int id, ParkingSpaceDTO spaceDto);
        Task DeleteAsync(int id);
    }
}

