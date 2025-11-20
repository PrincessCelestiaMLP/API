using AutoMapper;
using LW4_API.Data;
using LW4_API.Model.DTO;
using LW4_API.Model.Entity;
using LW4_API.Repository.Interface;
using LW4_API.Server.Interface;
using MongoDB.Driver;
namespace LW4_API.Server.Realizetion
{
    public class ParkingSpaceService: IParkingService
    {
        private readonly IParkingSpaceRepository _repo;
        private readonly IMapper _mapper;
        private readonly IMongoCollection<ParkingSpace> _spaceCollection;
        public ParkingSpaceService(IParkingSpaceRepository repo,IMapper mapper, IMongoDatabase database)
        {
            _repo = repo;
            _mapper = mapper;
            _spaceCollection = database.GetCollection<ParkingSpace>("Space");
        }

        public async Task<List<ParkingSpaceDTO>> GetAllAsync()
        {
           return _mapper.Map<List<ParkingSpaceDTO>>(await _repo.GetAllAsync());
        }
        public async Task<ParkingSpaceDTO?> GetByIdAsync(int id) { 
            ParkingSpaceDTO? space = _mapper.Map<ParkingSpaceDTO>(await _repo.GetByIdAsync(id));
            if (space == null)
                throw new Exception("Parking space not found");
            return space; 
        }

        public async Task CreateAsync(ParkingSpaceDTO spaceDto)
        {
            var space = _mapper.Map<ParkingSpace>(spaceDto);

            // Знайти найбільший Id у колекції
            var lastSpace = await _spaceCollection
                .Find(_ => true)
                .SortByDescending(c => c.Id)
                .Limit(1)
                .FirstOrDefaultAsync();

            space.Id = lastSpace != null ? lastSpace.Id + 1 : 1;

            await _repo.CreateAsync(space);
        }
        public async Task UpdateAsync(int id, ParkingSpaceDTO spaceDto)
        {
            var space = _mapper.Map<ParkingSpace>(spaceDto);
            space.Id = id;
            await _repo.UpdateAsync(space);
        }
        public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);
    }
}
