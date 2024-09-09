﻿using System.ComponentModel.DataAnnotations;

namespace InventoryManagementAPI.Dtos.User
{
    public class CreateUserDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Role { get; set; } = string.Empty;

    }
}
