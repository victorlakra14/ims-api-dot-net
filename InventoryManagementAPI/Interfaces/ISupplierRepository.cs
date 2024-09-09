using InventoryManagementAPI.Dtos.Supplier;
using InventoryManagementAPI.Models;

namespace InventoryManagementAPI.Interfaces
{
    public interface ISupplierRepository
    {
        Task<List<Supplier>> GetAllAsync();
        Task<Supplier?> GetByIdAsync(int id);
        Task<Supplier> CreateAsync(Supplier supplierModel);
        Task<Supplier?> UpdateAsync(int id, UpdateSupplierDto updateSupplierDto);
        Task<Supplier?> DeleteAsync(int id);
    }
}
