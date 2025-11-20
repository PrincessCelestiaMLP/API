using LW4_API.Model.Entity;
using LW4_API.Model.DTO;

namespace LW4_API.Server.Interface
{
    public interface IUserService
    {
        Task CreateAsync(UserDTO todoItem);
        Task<List<User>> GetAsync();
        Task<User?> GetByEmailAsync(string email);
        Task<User> GetAsync(string id);
        Task UpdateAsync(User todoItem);
        Task DeleteAsync(string id);
    }
}
