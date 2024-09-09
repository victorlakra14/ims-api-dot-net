using InventoryManagementAPI.Dtos.Product;
using InventoryManagementAPI.Interfaces;
using InventoryManagementAPI.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepo;
        public ProductController(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        [HttpGet]
        [Authorize(Policy = "AdminManagerAccess")]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productRepo.GetAllAsync();

            var productsDto = products.Select(p => p.ToProductDto());

            return Ok(productsDto);
        }

        [HttpGet("{id:int}")]
        [Authorize(Policy = "AdminManagerAccess")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var product = await _productRepo.GetByIdAsync(id);

            if (product is null) return NotFound("Product not found");

            return Ok(product.ToProductDto());
        }

        [HttpPost]
        [Authorize(Policy = "AdminManagerAccess")]
        public async Task<IActionResult> Create([FromBody] CreateProductDto createProductDto)
        {
            var productModel = createProductDto.ToProductFromCreateDto();

            await _productRepo.CreateAsync(productModel);

            return CreatedAtAction(nameof(GetById), new { id = productModel.Id }, productModel.ToProductDto());
        }

        [HttpPut("{id:int}")]
        [Authorize(Policy = "AdminManagerAccess")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateProductDto productDto)
        {
            var product = await _productRepo.UpdateAsync(id, productDto);

            if (product is null) return NotFound("Product not found");

            return Ok(product.ToProductDto());
        }

        [HttpDelete("{id:int}")]
        [Authorize(Policy = "AdminManagerAccess")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var product = await _productRepo.DeleteAsync(id);

            if (product is null) return NotFound("Product not found");

            return NoContent();
        }
    }
}
