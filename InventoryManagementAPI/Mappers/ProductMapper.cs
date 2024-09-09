using InventoryManagementAPI.Dtos.Product;
using InventoryManagementAPI.Models;

namespace InventoryManagementAPI.Mappers
{
    public static class ProductMapper
    {
        public static ProductDto ToProductDto(this Product productModel)
        {
            return new ProductDto
            {
                Id = productModel.Id,
                Name = productModel.Name,
                Description = productModel.Description,
                Price = productModel.Price,
                SupplierId = productModel.SupplierId
            };
        }

        public static Product ToProductFromCreateDto(this CreateProductDto productDto)
        {
            return new Product
            {
                Name = productDto.Name,
                Description = productDto.Description,
                Price = productDto.Price,
                SupplierId = productDto.SupplierId
            };
        }
    }
}
