using InventoryManagementAPI.Dtos.Supplier;
using InventoryManagementAPI.Interfaces;
using InventoryManagementAPI.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagementAPI.Controllers
{
    [Route("api/suppliers")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierRepository _supplierRepo;
        public SupplierController(ISupplierRepository supplierRepo)
        {
            _supplierRepo = supplierRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var suppliers = await _supplierRepo.GetAllAsync();

            var suppliersDto = suppliers.Select(s => s.ToSupplierDto());

            return Ok(suppliersDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var supplier = await _supplierRepo.GetByIdAsync(id);

            if (supplier is null) return NotFound("Supplier not found");

            return Ok(supplier.ToSupplierDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSupplierDto createSupplierDto)
        {
            var supplierModel = createSupplierDto.ToSupplierFromCreateDto();

            await _supplierRepo.CreateAsync(supplierModel);

            return CreatedAtAction(nameof(GetById), new { id = supplierModel.Id }, supplierModel.ToSupplierDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateSupplierDto updateSupplierDto)
        {
            var supplier = await _supplierRepo.UpdateAsync(id, updateSupplierDto);

            if (supplier is null) return NotFound("Supplier not found");

            return Ok(supplier.ToSupplierDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var supplier = await _supplierRepo.DeleteAsync(id);

            if (supplier is null) return NotFound("Supplier not found");

            return NoContent();
        }
    }
}
