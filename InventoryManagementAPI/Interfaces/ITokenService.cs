using InventoryManagementAPI.Models;

namespace InventoryManagementAPI.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
