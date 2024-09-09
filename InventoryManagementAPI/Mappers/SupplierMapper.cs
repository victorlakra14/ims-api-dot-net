using InventoryManagementAPI.Dtos.Supplier;
using InventoryManagementAPI.Models;

namespace InventoryManagementAPI.Mappers
{
    public static class SupplierMapper
    {
        public static SupplierDto ToSupplierDto(this Supplier supplier)
        {
            return new SupplierDto
            {
                Id = supplier.Id,
                Name = supplier.Name,
                ContactInfo = supplier.ContactInfo
            };
        }

        public static Supplier ToSupplierFromCreateDto(this CreateSupplierDto supplierModel)
        {
            return new Supplier
            {
                Name = supplierModel.Name,
                ContactInfo = supplierModel.ContactInfo
            };
        }
    }
}
