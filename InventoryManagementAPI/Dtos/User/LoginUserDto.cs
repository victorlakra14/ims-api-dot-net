using System.ComponentModel.DataAnnotations;

namespace InventoryManagementAPI.Dtos.User
{
    public class LoginUserDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
