using LW4_API.Model.Entity;
using LW4_API.MongoDB;
using LW4_API.Repository.Interface;
using MongoDB.Driver;

namespace LW4_API.Repository.Realization
{
    public class ParkingSpaceRepository : IParkingSpaceRepository
    {
        private readonly IMongoCollection<ParkingSpace> _collection;


        public ParkingSpaceRepository()
        {
            _collection = MongoDBClient.Instance.GetCollection<ParkingSpace>("Space");
        }
        public async Task CreateAsync(ParkingSpace space) =>
       await _collection.InsertOneAsync(space);

        public async Task<List<ParkingSpace>> GetAllAsync() =>
            await _collection.Find(_ => true).ToListAsync();

        public async Task<ParkingSpace?> GetByIdAsync(int id) =>
            await _collection.Find(u => u.Id == id).FirstOrDefaultAsync();

        public async Task UpdateAsync(ParkingSpace space) =>
            await _collection.ReplaceOneAsync(u => u.Id == space.Id, space);

        public async Task DeleteAsync(int id) =>
            await _collection.DeleteOneAsync(u => u.Id == id);
    }
}
