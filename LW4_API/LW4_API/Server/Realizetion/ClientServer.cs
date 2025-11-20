using AutoMapper;
using LW4_API.Data;
using LW4_API.Model.DTO;
using LW4_API.Model.Entity;
using LW4_API.MongoDB;
using LW4_API.Repository.Interface;
using LW4_API.Server.Interface;
using MongoDB.Driver;

namespace LW4_API.Server.Realizetion
{
    public class ClientServer : IClientService
    {
        private readonly IClientRepository _repo;
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Client> _clientCollection;

        public ClientServer(IClientRepository repo, IMapper mapper, IMongoDatabase database)
        {
            _repo = repo;
            _mapper = mapper;
            _clientCollection = database.GetCollection<Client>("Clients");
        }

        public async Task<List<ClientDTO>> GetAllAsync()
        {

            return _mapper.Map<List<ClientDTO>>(await _repo.GetAllAsync());
        }
        public async Task<ClientDTO?> GetByIdAsync(int id)
        {
            ClientDTO client = _mapper.Map<ClientDTO>(await _repo.GetByIdAsync(id));
            if (client == null)
                throw new NullReferenceException($"Client with this {id} doesn`t exist");
            return client;
        }
        public async Task CreateAsync(ClientDTO clientDto)
        {
            var client = _mapper.Map<Client>(clientDto);

            // Знайти найбільший Id у колекції
            var lastClient = await _clientCollection
                .Find(_ => true)
                .SortByDescending(c => c.Id)
                .Limit(1)
                .FirstOrDefaultAsync();

            client.Id = lastClient != null ? lastClient.Id + 1 : 1;

            await _repo.CreateAsync(client);
        }
        public async Task UpdateAsync(int id, ClientDTO clientDto)
        {
            var client = _mapper.Map<Client>(clientDto);
            client.Id = id;
            await _repo.UpdateAsync(client);
        }
        public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);
    }
}
