using InventoryManagementAPI.Dtos.User;
using InventoryManagementAPI.Models;

namespace InventoryManagementAPI.Interfaces
{
    public interface IUserRepository
    {
        public Task<List<User>> GetAllAsync();
        public Task<User?> GetByIdAsync(string id);
        public Task<User?> UpdateAsync(string id, UpdateUserDto updateUserDto);
        public Task<User?> DeleteAsync(string id);
    }
}
