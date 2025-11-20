using LW4_API.Model.Entity;
using LW4_API.MongoDB;
using LW4_API.Repository.Interface;
using MongoDB.Driver;

namespace LW4_API.Repository.Realization
{
    public class RentRepository : IRentRepository
    {
        private readonly IMongoCollection<Rent> _collection;


        public RentRepository()
        {
            _collection = MongoDBClient.Instance.GetCollection<Rent>("Rent");
        }
        public async Task CreateAsync(Rent rent) =>
       await _collection.InsertOneAsync(rent);

        public async Task<List<Rent>> GetAllAsync() =>
            await _collection.Find(_ => true).ToListAsync();

        public async Task<Rent?> GetByIdAsync(int id) =>
            await _collection.Find(u => u.Id == id).FirstOrDefaultAsync();

        public async Task UpdateAsync(Rent rent) =>
            await _collection.ReplaceOneAsync(u => u.Id == rent.Id, rent);

        public async Task DeleteAsync(int id) =>
            await _collection.DeleteOneAsync(u => u.Id == id);
    }
}
