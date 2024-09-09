using InventoryManagementAPI.Dtos.User;
using InventoryManagementAPI.Interfaces;
using InventoryManagementAPI.Mappers;
using InventoryManagementAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public UserController(IUserRepository userRepo, UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
        {
            _userRepo = userRepo;
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        [Authorize(Policy = "AdminAccess")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepo.GetAllAsync();
            var usersDto = new List<UserDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.FirstOrDefault();
                usersDto.Add(user.ToUserDto(role));
            }

            return Ok(usersDto);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "AdminAccess")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            var user = await _userRepo.GetByIdAsync(id);

            if (user is null) return NotFound();

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            return Ok(user.ToUserDto(role));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserDto createDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var user = new User
                {
                    UserName = createDto.Username
                };

                var createdUser = await _userManager.CreateAsync(user, createDto.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, createDto.Role);

                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto
                            {
                                Id = user.Id,
                                Username = user.UserName,
                                Token = _tokenService.CreateToken(user)
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == loginDto.Username.ToLower());

            if (user is null) return Unauthorized("Invalid username and/or password");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Invalid username and/or password");

            return Ok(
                new NewUserDto
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Token = _tokenService.CreateToken(user)
                }
            );
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "AdminAccess")]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateUserDto updateUserDto)
        {
            var user = await _userRepo.UpdateAsync(id, updateUserDto);

            if (user is null) return BadRequest("Failed to update user");

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            return Ok(user.ToUserDto(role));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminAccess")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var user = await _userRepo.DeleteAsync(id);

            if (user is null) return NotFound();

            return NoContent();
        }
    }
}
