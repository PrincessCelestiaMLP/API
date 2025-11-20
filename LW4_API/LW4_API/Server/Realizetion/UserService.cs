using AutoMapper;
using LW4_API.Model.Entity;
using LW4_API.Model.DTO;
using LW4_API.Repository.Interface;
using LW4_API.Server.Interface;

namespace LW4_API.Server.Realizetion
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task CreateAsync(UserDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _userRepository.CreateAsync(user);
        }
        public async Task DeleteAsync(string id) => await _userRepository.DeleteAsync(id);
        public async Task<List<User>> GetAsync() => await _userRepository.GetAsync();
        public async Task<User?> GetByEmailAsync(string email) => await _userRepository.GetByEmailAsync(email);
        public async Task<User> GetAsync(string id) => await _userRepository.GetAsync(id);
        public async Task UpdateAsync(User user) => await _userRepository.UpdateAsync(user);
    }
}
