using InventoryManagementAPI.Dtos.Product;
using InventoryManagementAPI.Models;

namespace InventoryManagementAPI.Interfaces
{
    public interface IProductRepository
    {
        public Task<List<Product>> GetAllAsync();
        public Task<Product?> GetByIdAsync(int id);
        public Task<Product> CreateAsync(Product productModel);
        public Task<Product?> UpdateAsync(int id, UpdateProductDto updateProductDto);
        public Task<Product?> DeleteAsync(int id);
    }
}
