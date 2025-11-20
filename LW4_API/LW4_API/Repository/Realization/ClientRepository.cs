using LW4_API.Model.Entity;
using LW4_API.MongoDB;
using LW4_API.Repository.Interface;
using MongoDB.Driver;
namespace LW4_API.Repository.Realization
{
    public class ClientRepository : IClientRepository
    {
        private readonly IMongoCollection<Client> _collection;


        public ClientRepository()
        {
            _collection =  MongoDBClient.Instance.GetCollection<Client>("Client");
        }
        public async Task CreateAsync(Client client)
        {
            client.Id = await MongoDBClient.GetNextId("Client");
            await _collection.InsertOneAsync(client);
        }
        public async Task<List<Client>> GetAllAsync() =>
            await _collection.Find(_ => true).ToListAsync();

        public async Task<Client?> GetByIdAsync(int id) =>
            await _collection.Find(u => u.Id == id).FirstOrDefaultAsync();

        public async Task UpdateAsync(Client user) =>
            await _collection.ReplaceOneAsync(u => u.Id == user.Id, user);

        public async Task DeleteAsync(int id) =>
            await _collection.DeleteOneAsync(u => u.Id == id);
    }
    
}
