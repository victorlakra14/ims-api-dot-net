using InventoryManagementAPI.Data;
using InventoryManagementAPI.Dtos.User;
using InventoryManagementAPI.Interfaces;
using InventoryManagementAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace InventoryManagementAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<User> _userManager;
        public UserRepository(ApplicationDBContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<User?> DeleteAsync(string id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user is null) return null;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user is null) return null;

            return user;
        }

        public async Task<User?> UpdateAsync(string id, UpdateUserDto updateUserDto)
        {
            var user = await _context.Users.FindAsync(id);

            if (user is null) return null;

            var currentRoles = await _userManager.GetRolesAsync(user);

            var roleRemovalResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);

            if (!roleRemovalResult.Succeeded) return null;

            var updateRoleResult = await _userManager.AddToRoleAsync(user, updateUserDto.Role);

            if (!updateRoleResult.Succeeded) return null;

            var usernameUpdateResult = await _userManager.SetUserNameAsync(user, updateUserDto.Username);

            if (!usernameUpdateResult.Succeeded) return null;

            var passwordUpdateResult = await _userManager.RemovePasswordAsync(user);

            if (!passwordUpdateResult.Succeeded) return null;

            passwordUpdateResult = await _userManager.AddPasswordAsync(user, updateUserDto.Password);

            if (!passwordUpdateResult.Succeeded) return null;

            await _context.SaveChangesAsync();

            return user;
        }
    }
}
