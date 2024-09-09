using InventoryManagementAPI.Dtos.User;
using InventoryManagementAPI.Models;

namespace InventoryManagementAPI.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToUserDto(this User userModel, string role)
        {
            return new UserDto
            {
                Id = userModel.Id,
                Username = userModel.UserName,
                Role = role
            };
        }
    }
}
