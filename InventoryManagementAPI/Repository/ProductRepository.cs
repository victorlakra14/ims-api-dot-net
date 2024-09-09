using InventoryManagementAPI.Data;
using InventoryManagementAPI.Dtos.Product;
using InventoryManagementAPI.Interfaces;
using InventoryManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDBContext _context;
        public ProductRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateAsync(Product productModel)
        {
            await _context.Products.AddAsync(productModel);
            await _context.SaveChangesAsync();

            return productModel;
        }

        public async Task<Product?> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product is null) return null;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            var products = await _context.Products.ToListAsync();

            return products;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product is null) return null;

            return product;
        }

        public async Task<Product?> UpdateAsync(int id, UpdateProductDto updateProductDto)
        {
            var product = await _context.Products.FindAsync(id);

            if (product is null) return null;

            product.Name = updateProductDto.Name;
            product.Description = updateProductDto.Description;
            product.Price = updateProductDto.Price;
            product.SupplierId = updateProductDto.SupplierId;

            await _context.SaveChangesAsync();

            return product;
        }
    }
}
