using LW4_API.Model.Entity;

namespace LW4_API.Repository.Interface
{
    public interface IUserRepository
    {
        Task CreateAsync(User user);
        Task<List<User>> GetAsync();
        Task<User?> GetByEmailAsync(string email);
        Task<User> GetAsync(string id);
        Task UpdateAsync(User user);
        Task DeleteAsync(string id);
    }
}
