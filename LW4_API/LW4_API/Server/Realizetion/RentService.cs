using AutoMapper;
using LW4_API.Data;
using LW4_API.Model.DTO;
using LW4_API.Model.View;
using LW4_API.Model.Entity;
using LW4_API.Repository.Interface;
using LW4_API.Server.Interface;
using MongoDB.Driver;
using LW4_API.Model.Domain;

namespace LW4_API.Server.Realizetion
{
    public class RentService: IRentService
    {
        private readonly IRentRepository _repo;
        private readonly IParkingService _parkingServise;
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Rent> _rentCollection;

        public RentService(IRentRepository repo,IParkingService parkingService,IClientService clientService,IMapper mapper,IMongoDatabase database)
        {
            _repo = repo;
            _parkingServise = parkingService;
            _clientService = clientService;
            _mapper = mapper;
            _rentCollection = database.GetCollection<Rent>("Rent");
        }

        public async Task<List<RentView>> GetAllAsync()
        {
            var rents = await _repo.GetAllAsync();
            var rentViews = new List<RentView>(); // пустий список

            foreach (var rent in rents)
            {
                // Отримуємо клієнта
                var client = await _clientService.GetByIdAsync(rent.ClientId);
                // Отримуємо парковку
                var space = await _parkingServise.GetByIdAsync(rent.ParkingSpaceId);

                // Мапимо основні поля Rent → RentView
                var rentView = _mapper.Map<RentView>(rent);

                // Встановлюємо дані клієнта
                rentView.ClientName = client?.Name ?? "Unknown";
                rentView.ClientSurname = client?.Surname ?? "";

                // Встановлюємо дані парковки
                rentView.IsVip = space?.IsVip ?? false;
                rentView.PriceForHour = space?.PriceForHour ?? 0;

                rentViews.Add(rentView);
            }

            return rentViews;
        }

        public async Task<RentView?> GetByIdAsync(int id)
        {
            var rentEntity = await _repo.GetByIdAsync(id);
            if (rentEntity == null)
                throw new NullReferenceException($"Rent with id {id} doesn't exist");

            var rentView = _mapper.Map<RentView>(rentEntity);

            var client = await _clientService.GetByIdAsync(rentEntity.ClientId);
            rentView.ClientName = client?.Name ?? "Unknown";
            rentView.ClientSurname = client?.Surname ?? "";

            var space = await _parkingServise.GetByIdAsync(rentEntity.ParkingSpaceId);
            rentView.IsVip = space?.IsVip ?? false;
            rentView.PriceForHour = space?.PriceForHour ?? 0;

            return rentView;
        }

        public async Task CreateAsync(RentDTO rentDto) 
        {
            var rentDomain = _mapper.Map<RentDomain>(rentDto);

            // Генерація нового Id
            var lastRent = await _rentCollection
                .Find(_ => true)
                .SortByDescending(c => c.Id)
                .Limit(1)
                .FirstOrDefaultAsync();

            rentDomain.Id = lastRent != null ? lastRent.Id + 1 : 1;

            // Отримуємо паркувальне місце
            var space = await _parkingServise.GetByIdAsync(rentDomain.ParkingSpaceId);
            if (space == null)
                throw new NullReferenceException($"Parking space {rentDomain.ParkingSpaceId} not found");

            space.UserId = rentDomain.ClientId;

            rentDomain.Price = rentDomain.Sum(space);

            // Оновлюємо паркувальне місце
            await _parkingServise.UpdateAsync(rentDomain.ParkingSpaceId,space);

            // Мапимо в Rent перед збереженням
            var rentEntity = _mapper.Map<Rent>(rentDomain);

            await _repo.CreateAsync(rentEntity);
        }
        public async Task UpdateAsync(int id, RentDTO rentDto)
        {
            var rent = _mapper.Map<Rent>(rentDto);
            rent.Id = id;
            await _repo.UpdateAsync(rent);
        }
        public async Task DeleteAsync(int id) => _repo.DeleteAsync(id);
    }
}
