using InventoryManagementAPI.Data;
using InventoryManagementAPI.Dtos.Supplier;
using InventoryManagementAPI.Interfaces;
using InventoryManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementAPI.Repository
{
    
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ApplicationDBContext _context;
        public SupplierRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Supplier> CreateAsync(Supplier supplierModel)
        {
            await _context.Suppliers.AddAsync(supplierModel);
            await _context.SaveChangesAsync();

            return supplierModel;
        }

        public async Task<Supplier?> DeleteAsync(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier is null) return null;

            _context.Suppliers.Remove(supplier);
            await _context.SaveChangesAsync();

            return supplier;
        }

        public async Task<List<Supplier>> GetAllAsync()
        {
            return await _context.Suppliers.ToListAsync();
        }

        public async Task<Supplier?> GetByIdAsync(int id)
        {
            var supplier = await _context.Suppliers.FindAsync(id);

            if (supplier is null) return null;

            return supplier;
        }

        public async Task<Supplier?> UpdateAsync(int id, UpdateSupplierDto updateSupplierDto)
        {
            var existingSupplier = await _context.Suppliers.FindAsync(id);

            if (existingSupplier is null) return null;

            existingSupplier.Name = updateSupplierDto.Name;
            existingSupplier.ContactInfo = updateSupplierDto.ContactInfo;

            await _context.SaveChangesAsync();

            return existingSupplier;
        }
    }
}
